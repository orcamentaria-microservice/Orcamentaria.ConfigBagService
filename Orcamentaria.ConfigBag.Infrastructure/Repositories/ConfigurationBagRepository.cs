using MongoDB.Driver;
using Orcamentaria.ConfigBag.Domain.Models;
using Orcamentaria.ConfigBag.Domain.Repositories;
using Orcamentaria.ConfigBag.Infrastructure.Contexts;
using Orcamentaria.Lib.Domain.Exceptions;

namespace Orcamentaria.ConfigBag.Infrastructure.Repositories
{
    public class ConfigurationBagRepository : IConfiguirationBagRepository
    {
        private readonly MongoContext _dbContext;

        public ConfigurationBagRepository(MongoContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ConfigurationBag> GetConfiguirationBagByServiceName(string serviceName)
        {
            try
            {
                var filter = Builders<ConfigurationBag>.Filter.Eq(x => x.ServiceName, serviceName);
                var cursor = await _dbContext.ConfigurationBags.FindAsync(filter);
                var data = await cursor.FirstOrDefaultAsync();
                return data;
            }
            catch (Exception ex)
            {
                throw new DatabaseException(ex.Message);
            }
        }

        public async Task Save(ConfigurationBag configurationBag)
        {
            try
            {
                var filter = Builders<ConfigurationBag>.Filter.Eq(x => x.ServiceName, configurationBag.ServiceName);

                await _dbContext.ConfigurationBags.ReplaceOneAsync(
                    filter,
                    configurationBag,
                    new ReplaceOptions { IsUpsert = true }
                );

            }
            catch (Exception ex)
            {
                throw new DatabaseException(ex.Message);
            }
        }
    }
}
