using MongoDB.Driver;
using Orcamentaria.ConfigBagService.Domain.Models;

namespace Orcamentaria.ConfigBagService.Infrastructure.Contexts
{
    public class MongoContext
    {
        private static readonly string _databaseName = "config-bag-db";
        public IMongoCollection<ConfigurationBag> ConfigurationBags { get; private set; }
        public MongoContext(IMongoClient client)
        {
            ConfigurationBags = 
                client.GetDatabase(_databaseName).GetCollection<ConfigurationBag>("config-bags");
        }

    }
}
