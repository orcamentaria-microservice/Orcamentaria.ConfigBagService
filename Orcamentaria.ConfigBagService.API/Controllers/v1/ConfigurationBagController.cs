using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Orcamentaria.Lib.Domain.Models;
using Orcamentaria.ConfigBagService.Domain.Services;
using Orcamentaria.ConfigBagService.Domain.DTOs.ConfigurationBag;
using Orcamentaria.Lib.Domain.DTOs.ConfigurationBag;

namespace Orcamentaria.ConfigBagService.API.Controllers.v1
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ConfigurationBagController : Controller
    {
        private readonly IConfiguirationBagService _service;

        public ConfigurationBagController(IConfiguirationBagService service)
        {
            _service = service;
        }

        [Authorize(Roles = "MASTER,CONFIGURATION_BAG:READ")]
        [HttpGet("GetByServiceName/{serviceName}", Name = "ConfigurationBagGetByServiceName")]
        public async Task<Response<ConfigurationBagResponseDTO>> GetByServiceName(string serviceName)
        {
            try
            {
                return await _service.GetConfiguirationBagByServiceName(serviceName);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [Authorize(Roles = "MASTER,CONFIGURATION_BAG:READ")]
        [HttpGet("GetConfigurationBagServiceName", Name = "ConfigurationBagGetServiceNames")]
        public async Task<Response<IEnumerable<string>>> GetServiceNames()
        {
            try
            {
                return await _service.GetConfiguirationBagServiceName();
            }
            catch (Exception)
            {
                throw;
            }
        }

        [Authorize(Roles = "MASTER,CONFIGURATION_BAG:INSERT:CONFIGURATIONS")]
        [HttpPost("Configurations", Name = "ConfigurationBagConfigurationsInsert")]
        public async Task<Response<ConfigurationBagResponseDTO>> InsertConfigurations([FromBody] ConfigurationBagConfigurationInsertDTO dto)
        {
            try
            {
                return await _service.SaveConfiguration(dto);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [Authorize(Roles = "MASTER,CONFIGURATION_BAG:INSERT:CONNECTIONSTRINGS")]
        [HttpPost("ConnectionStrings", Name = "ConfigurationBagConnectionStringsInsert")]
        public async Task<Response<ConfigurationBagResponseDTO>> InsertConnectionStrings([FromBody] ConfigurationBagConnectionStringInsertDTO dto)
        {
            try
            {
                return await _service.SaveConnectionString(dto);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
