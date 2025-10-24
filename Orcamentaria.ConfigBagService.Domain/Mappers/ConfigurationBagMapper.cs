using AutoMapper;
using MongoDB.Bson;
using Orcamentaria.ConfigBagService.Domain.DTOs.ConfigurationBag;
using Orcamentaria.ConfigBagService.Domain.Models;
using Orcamentaria.Lib.Domain.DTOs.ConfigurationBag;

namespace Orcamentaria.PersonService.Domain.Mappers
{
    public class ConfigurationBagMapper : Profile
    {
        public ConfigurationBagMapper() 
        {
            CreateMap<ConfigurationBag, ConfigurationBagResponseDTO>()
                .ForMember(s => s.ServiceName, opt => opt.MapFrom(d => d.ServiceName))
                .ForMember(s => s.UpdateAt, opt => opt.MapFrom(d => d.UpdateAt))
                .ForMember(s => s.ConnectionStrings, opt => opt.MapFrom(d => d.ConnectionStrings))
                .ForMember(s => s.Configurations, opt => opt.MapFrom(d => ConvertDictionaryList(d.Configurations)))
                .ReverseMap();
        }

        private static IEnumerable<Dictionary<string, object>> ConvertDictionaryList(IEnumerable<Dictionary<string, BsonDocument>>? source)
        {
            if (source is null)
                return Enumerable.Empty<Dictionary<string, object>>();

            return source.Select(dict =>
                dict.ToDictionary(
                    kv => kv.Key,
                    kv => (object)BsonTypeMapper.MapToDotNetValue(kv.Value)
                )
            );
        }
    }
}
