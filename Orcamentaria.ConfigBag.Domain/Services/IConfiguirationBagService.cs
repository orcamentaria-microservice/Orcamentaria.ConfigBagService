using Orcamentaria.ConfigBag.Domain.DTOs.ConfigurationBag;
using Orcamentaria.ConfigBag.Domain.Models;
using Orcamentaria.Lib.Domain.Models;

namespace Orcamentaria.ConfigBag.Domain.Services
{
    public interface IConfiguirationBagService
    {
        Task<Response<ConfigurationBag>> GetConfiguirationBagByServiceName(string serviceName);
        Task<Response<ConfigurationBag>> SaveConnectionString(ConfigurationBagConnectionStringInsert dto);
        Task<Response<ConfigurationBag>> SaveConfiguration(ConfigurationBagConfigurationInsert dto);
    }
}
