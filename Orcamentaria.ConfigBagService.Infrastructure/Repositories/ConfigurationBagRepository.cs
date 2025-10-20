using MongoDB.Driver;
using Orcamentaria.ConfigBagService.Domain.Models;
using Orcamentaria.ConfigBagService.Domain.Repositories;
using Orcamentaria.ConfigBagService.Infrastructure.Contexts;
using Orcamentaria.Lib.Domain.Exceptions;

namespace Orcamentaria.ConfigBagService.Infrastructure.Repositories
{
    public class ConfigurationBagRepository : IConfiguirationBagRepository
    {
        private readonly MongoContext _dbContext;

        public ConfigurationBagRepository(MongoContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<ConfigurationBag>> GetAll()
        {
            try
            {
                var data = await _dbContext.ConfigurationBags.FindAsync(_ => true);
                return data.ToList();
            }
            catch (Exception ex)
            {
                throw new DatabaseException(ex.Message);
            }
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
