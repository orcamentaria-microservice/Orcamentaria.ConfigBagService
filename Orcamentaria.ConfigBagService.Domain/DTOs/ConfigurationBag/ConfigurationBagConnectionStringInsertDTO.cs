namespace Orcamentaria.ConfigBagService.Domain.DTOs.ConfigurationBag
{
    public class ConfigurationBagConnectionStringInsertDTO
    {
        public string ServiceName { get; set; }
        public IEnumerable<Dictionary<string, string>> ConnectionStrings { get; set; }
    }
}
