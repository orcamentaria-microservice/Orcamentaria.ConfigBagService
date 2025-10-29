namespace Orcamentaria.ConfigBagService.Domain.DTOs.ConfigurationBag
{
    public class ConfigurationBagRealTimeConfigurationInsertDTO
    {
        public string ServiceName { get; set; }
        public IEnumerable<Dictionary<string, string>> RealTimeConfigurations { get; set; }
    }
}
