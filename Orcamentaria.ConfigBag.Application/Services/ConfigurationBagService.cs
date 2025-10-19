using Orcamentaria.ConfigBag.Domain.DTOs.ConfigurationBag;
using Orcamentaria.ConfigBag.Domain.Models;
using Orcamentaria.ConfigBag.Domain.Repositories;
using Orcamentaria.ConfigBag.Domain.Services;
using Orcamentaria.Lib.Domain.Exceptions;
using Orcamentaria.Lib.Domain.Models;

namespace Orcamentaria.ConfigBag.Application.Services
{
    public class ConfigurationBagService : IConfiguirationBagService
    {
        private readonly IConfiguirationBagRepository _repository;

        public ConfigurationBagService(IConfiguirationBagRepository repository)
        {
            _repository = repository;
        }

        public async Task<Response<ConfigurationBag>> GetConfiguirationBagByServiceName(string serviceName)
        {
            try
            {
                var data = await _repository.GetConfiguirationBagByServiceName(serviceName);

                return new Response<ConfigurationBag>(data);
            }
            catch (DefaultException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new UnexpectedException(ex.Message, ex);
            }
        }

        public async Task<Response<ConfigurationBag>> SaveConfiguration(ConfigurationBagConfigurationInsert dto)
        {
            try
            {
                var configBag = await _repository.GetConfiguirationBagByServiceName(dto.ServiceName);

                configBag.Configurations = dto.Configurations;

                await _repository.Save(configBag);

                return new Response<ConfigurationBag>(configBag);
            }
            catch (DefaultException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new UnexpectedException(ex.Message, ex);
            }
        }

        public async Task<Response<ConfigurationBag>> SaveConnectionString(ConfigurationBagConnectionStringInsert dto)
        {
            try
            {
                var configBag = await _repository.GetConfiguirationBagByServiceName(dto.ServiceName);

                configBag.ConnectionStrings = dto.ConnectionStrings;

                await _repository.Save(configBag);

                return new Response<ConfigurationBag>(configBag);
            }
            catch (DefaultException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new UnexpectedException(ex.Message, ex);
            }
        }
    }
}
