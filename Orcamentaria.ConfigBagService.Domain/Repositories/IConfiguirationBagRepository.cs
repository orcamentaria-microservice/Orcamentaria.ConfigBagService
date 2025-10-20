using Orcamentaria.ConfigBagService.Domain.Models;

namespace Orcamentaria.ConfigBagService.Domain.Repositories
{
    public interface IConfiguirationBagRepository
    {
        Task<IEnumerable<ConfigurationBag>> GetAll();
        Task<ConfigurationBag> GetConfiguirationBagByServiceName(string serviceName);
        Task Save(ConfigurationBag configurationBag);
    }
}
