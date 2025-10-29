using System.Text.Json;

namespace Orcamentaria.ConfigBagService.Domain.DTOs.ConfigurationBag
{
    public class ConfigurationBagConfigurationInsertDTO
    {
        public string ServiceName { get; set; }
        public IEnumerable<Dictionary<string, JsonElement>> Configurations { get; set; }
    }
}
