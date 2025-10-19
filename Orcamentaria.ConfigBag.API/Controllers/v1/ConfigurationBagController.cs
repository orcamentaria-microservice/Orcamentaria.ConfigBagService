using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Orcamentaria.Lib.Domain.Models;
using Orcamentaria.ConfigBag.Domain.Services;
using Orcamentaria.ConfigBag.Domain.Models;
using Orcamentaria.ConfigBag.Domain.DTOs.ConfigurationBag;

namespace Orcamentaria.ConfigBag.API.Controllers.v1
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

        [Authorize(Roles = "CONFIGURATION_BAG:READ")]
        [HttpGet("GetByServiceName/{serviceName}", Name = "ConfigurationBagGetByServiceName")]
        public async Task<Response<ConfigurationBag>> GetByServiceName(string serviceName)
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
		
        [Authorize(Roles = "CONFIGURATION_BAG:INSERT")]
        [HttpPost(Name = "ConfigurationConfigurationsBagInsert")]
        public async Task<Response<ConfigurationBag>> InsertConfigurations([FromBody] ConfigurationBagConfigurationInsert dto)
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

        [Authorize(Roles = "CONFIGURATION_BAG:INSERT")]
        [HttpPost(Name = "ConfigurationConnectionStringsBagInsert")]
        public async Task<Response<ConfigurationBag>> InsertConnectionStrings([FromBody] ConfigurationBagConnectionStringInsert dto)
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
