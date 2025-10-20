using Orcamentaria.ConfigBagService.Domain.DTOs.ConfigurationBag;
using Orcamentaria.Lib.Domain.Models;

namespace Orcamentaria.ConfigBagService.Domain.Services
{
    public interface IConfiguirationBagService
    {
        Task<Response<ConfigurationBagResponseDTO>> GetConfiguirationBagByServiceName(string serviceName);
        Task<Response<IEnumerable<string>>> GetConfiguirationBagServiceName();
        Task<Response<ConfigurationBagResponseDTO>> SaveConnectionString(ConfigurationBagConnectionStringInsertDTO dto);
        Task<Response<ConfigurationBagResponseDTO>> SaveConfiguration(ConfigurationBagConfigurationInsertDTO dto);
    }
}
