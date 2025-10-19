namespace Orcamentaria.ConfigBag.Domain.DTOs.ConfigurationBag
{
    public class ConfigurationBagConnectionStringInsert
    {
        public string ServiceName { get; set; }
        public IEnumerable<Dictionary<string, string>> ConnectionStrings { get; set; }
    }
}
