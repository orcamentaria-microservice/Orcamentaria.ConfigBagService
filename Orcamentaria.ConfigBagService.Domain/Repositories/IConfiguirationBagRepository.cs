using Orcamentaria.ConfigBagService.Domain.Models;
using Orcamentaria.Lib.Domain.Repositories;

namespace Orcamentaria.ConfigBagService.Domain.Repositories
{
    public interface IConfiguirationBagRepository
    {
        Task<IEnumerable<ConfigurationBag>> GetAll();
        Task<ConfigurationBag> GetConfiguirationBagByServiceName(string serviceName);
        Task Save(ConfigurationBag configurationBag);
    }
}
