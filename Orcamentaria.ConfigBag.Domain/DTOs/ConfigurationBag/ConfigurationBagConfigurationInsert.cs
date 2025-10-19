using System.Text.Json;

namespace Orcamentaria.ConfigBag.Domain.DTOs.ConfigurationBag
{
    public class ConfigurationBagConfigurationInsert
    {
        public string ServiceName { get; set; }
        public IEnumerable<Dictionary<string, JsonDocument>> Configurations { get; set; }
    }
}
