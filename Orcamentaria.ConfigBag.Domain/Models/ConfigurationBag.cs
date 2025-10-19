using System.Text.Json;

namespace Orcamentaria.ConfigBag.Domain.Models
{
    public class ConfigurationBag
    {
        public string ServiceName { get; set; }
        public DateTime UpdateAt { get; set; }
        public IEnumerable<Dictionary<string, string>> ConnectionStrings { get; set; } = [];
        public IEnumerable<Dictionary<string, JsonDocument>> Configurations { get; set; }
    }
}
