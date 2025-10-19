using Orcamentaria.ConfigBag.Domain.Models;

namespace Orcamentaria.ConfigBag.Domain.Repositories
{
    public interface IConfiguirationBagRepository
    {
        Task<ConfigurationBag> GetConfiguirationBagByServiceName(string serviceName);
        Task Save(ConfigurationBag configurationBag);
    }
}
