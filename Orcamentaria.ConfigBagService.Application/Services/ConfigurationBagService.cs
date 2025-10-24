using AutoMapper;
using MongoDB.Bson;
using Orcamentaria.ConfigBagService.Domain.DTOs.ConfigurationBag;
using Orcamentaria.ConfigBagService.Domain.Models;
using Orcamentaria.ConfigBagService.Domain.Repositories;
using Orcamentaria.ConfigBagService.Domain.Services;
using Orcamentaria.Lib.Domain.DTOs.ConfigurationBag;
using Orcamentaria.Lib.Domain.Exceptions;
using Orcamentaria.Lib.Domain.Models;

namespace Orcamentaria.ConfigBagService.Application.Services
{
    public class ConfigurationBagService : IConfiguirationBagService
    {
        private readonly IConfiguirationBagRepository _repository;
        private readonly IMapper _mapper;

        public ConfigurationBagService(
            IConfiguirationBagRepository repository, 
            IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Response<ConfigurationBagResponseDTO>> GetConfiguirationBagByServiceName(string serviceName)
        {
            try
            {
                var data = await _repository.GetConfiguirationBagByServiceName(serviceName);

                return new Response<ConfigurationBagResponseDTO>(_mapper.Map<ConfigurationBagResponseDTO>(data));
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

        public async Task<Response<IEnumerable<string>>> GetConfiguirationBagServiceName()
        {
            try
            {
                var data = await _repository.GetAll();

                return new Response<IEnumerable<string>>(data.Select(x => x.ServiceName));
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

        public async Task<Response<ConfigurationBagResponseDTO>> SaveConfiguration(ConfigurationBagConfigurationInsertDTO dto)
        {
            try
            {
                var data = await _repository.GetConfiguirationBagByServiceName(dto.ServiceName);

                if(data is null)
                {
                    data = new ConfigurationBag()
                    {
                        _id = ObjectId.GenerateNewId(),
                        ServiceName = dto.ServiceName,
                    };
                }

                data.Configurations = dto.Configurations.Select(dict =>
                {
                    var bsonDict = new Dictionary<string, BsonDocument>();

                    foreach (var kvp in dict)
                    {
                        var rawJson = kvp.Value.GetRawText();
                        bsonDict[kvp.Key] = BsonDocument.Parse(rawJson);
                    }

                    return bsonDict;
                }).ToList();
                data.UpdateAt = DateTime.Now; 

                await _repository.Save(data);

                return new Response<ConfigurationBagResponseDTO>(_mapper.Map<ConfigurationBagResponseDTO>(data));
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

        public async Task<Response<ConfigurationBagResponseDTO>> SaveConnectionString(ConfigurationBagConnectionStringInsertDTO dto)
        {
            try
            {
                var data = await _repository.GetConfiguirationBagByServiceName(dto.ServiceName);

                if (data is null)
                {
                    data = new ConfigurationBag()
                    {
                        _id = ObjectId.GenerateNewId(),
                        ServiceName = dto.ServiceName,
                    };
                }

                data.ConnectionStrings = dto.ConnectionStrings;
                data.UpdateAt = DateTime.Now;

                await _repository.Save(data);

                return new Response<ConfigurationBagResponseDTO>(_mapper.Map<ConfigurationBagResponseDTO>(data));
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
