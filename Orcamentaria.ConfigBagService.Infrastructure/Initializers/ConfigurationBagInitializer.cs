using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using MongoDB.Driver;
using Orcamentaria.ConfigBagService.Domain.Models;
using Orcamentaria.ConfigBagService.Infrastructure.Contexts;
using Orcamentaria.ConfigBagService.Infrastructure.Repositories;
using Orcamentaria.Lib.Domain.Exceptions;
using Orcamentaria.Lib.Domain.Models.Exceptions;
using System.Text.Json;

namespace Orcamentaria.ConfigBagService.Infrastructure.Initializers
{
    public class ConfigurationBagInitializer
    {
        private readonly string _serviceName;

        public ConfigurationBagInitializer(string serviceName)
        {
            _serviceName = serviceName;
        }

        public async Task<IConfigurationRoot> InitializeAsync(IConfiguration configuration)
        {
            try
            {
                var connectionString = configuration.GetConnectionString("DefaultConnection");

                if(string.IsNullOrWhiteSpace(connectionString))
                    throw new ConfigurationException($"A connection string não foi configurada no serviço {_serviceName}.");

                var mongoClient = new MongoClient(connectionString);

                var dbContext = new MongoContext(mongoClient);

                var repository = new ConfigurationBagRepository(dbContext);

                var data = await repository.GetConfiguirationBagByServiceName(_serviceName);

                if (data is null)
                    throw new NotFoundException($"Não foi encontrada configuração para o serviço {_serviceName}.");

                var dict = ToAppsettingsDictionary(data);

                var newConfig = new ConfigurationBuilder()
                    .AddConfiguration(configuration)
                    .AddInMemoryCollection(dict)
                    .Build();

                return newConfig;
            }
            catch (DefaultException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new UnexpectedException(ex.Message, ex);
            }
        }

        #region private methods
        private static Dictionary<string, string> ToAppsettingsDictionary(ConfigurationBag bag)
        {
            var dict = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

            if (!string.IsNullOrWhiteSpace(bag.ServiceName))
                dict["ConfigurationBag:ServiceName"] = bag.ServiceName;
            dict["ConfigurationBag:UpdateAt"] = bag.UpdateAt.ToString("O");

            if (bag.ConnectionStrings is not null)
            {
                foreach (var row in bag.ConnectionStrings)
                {
                    if (row is null) continue;

                    if (row.TryGetValue("Name", out var name) &&
                        (row.TryGetValue("ConnectionString", out var cs) || row.TryGetValue("Value", out cs)))
                    {
                        if (!string.IsNullOrWhiteSpace(name) && !string.IsNullOrWhiteSpace(cs))
                            dict[$"ConnectionStrings:{name}"] = cs;
                        continue;
                    }

                    foreach (var kv in row)
                    {
                        if (!string.IsNullOrWhiteSpace(kv.Key) && !string.IsNullOrWhiteSpace(kv.Value))
                            dict[$"ConnectionStrings:{kv.Key}"] = kv.Value;
                    }
                }
            }
            if (bag.Configurations is not null)
            {
                foreach (var item in bag.Configurations)
                {
                    if (item is null) continue;

                    foreach (var (sectionName, sectionValue) in item)
                    {
                        FlattenObject(dict, (object)sectionValue, sectionName);
                    }
                }
            }

            return dict;
        }

        private static void FlattenObject(Dictionary<string, string> bag, object? value, string prefix)
        {
            if (value is null) return;

            switch (value)
            {
                case string s:
                    bag[prefix] = s;
                    return;

                case bool b:
                    bag[prefix] = b.ToString();
                    return;

                case int or long or short or byte or double or float or decimal:
                    bag[prefix] = Convert.ToString(value, System.Globalization.CultureInfo.InvariantCulture)!;
                    return;

                case DateTime dt:
                    bag[prefix] = dt.ToString("O");
                    return;

                case IDictionary<string, object> dictObj:
                    foreach (var (k, v) in dictObj)
                        FlattenObject(bag, v, $"{prefix}:{k}");
                    return;
                case BsonDocument bson:
                    var jsonB = bson.ToJson();
                    var rootEl = JsonDocument.Parse(jsonB).RootElement;
                    FlattenJsonElement(bag, rootEl, prefix);
                    return;
                case IEnumerable<object> list:
                    int j = 0;
                    foreach (var item in list)
                        FlattenObject(bag, item, $"{prefix}:{j++}");
                    return;
                default:
                    var json = System.Text.Json.JsonSerializer.Serialize(value);
                    var el = System.Text.Json.JsonSerializer.Deserialize<System.Text.Json.JsonElement>(json);
                    FlattenJsonElement(bag, el, prefix);
                    return;
            }
        }

        private static void FlattenJsonElement(Dictionary<string, string> bag, System.Text.Json.JsonElement el, string prefix)
        {
            switch (el.ValueKind)
            {
                case System.Text.Json.JsonValueKind.Object:
                    foreach (var p in el.EnumerateObject())
                        FlattenJsonElement(bag, p.Value, $"{prefix}:{p.Name}");
                    break;

                case System.Text.Json.JsonValueKind.Array:
                    int i = 0;
                    foreach (var item in el.EnumerateArray())
                        FlattenJsonElement(bag, item, $"{prefix}:{i++}");
                    break;

                case System.Text.Json.JsonValueKind.String:
                    bag[prefix] = el.GetString()!;
                    break;

                case System.Text.Json.JsonValueKind.Number:
                    bag[prefix] = el.GetRawText();
                    break;

                case System.Text.Json.JsonValueKind.True:
                case System.Text.Json.JsonValueKind.False:
                    bag[prefix] = el.GetBoolean().ToString();
                    break;
            }
        }

        #endregion
    }
}
