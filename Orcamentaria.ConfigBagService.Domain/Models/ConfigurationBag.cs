using MongoDB.Bson;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace Orcamentaria.ConfigBagService.Domain.Models
{
    public class ConfigurationBag
    {
        public ObjectId _id { get; set; }
        public string ServiceName { get; set; }
        public DateTime UpdateAt { get; set; }
        public IEnumerable<Dictionary<string, string>> ConnectionStrings { get; set; } = [];
        public IEnumerable<Dictionary<string, BsonDocument>> Configurations { get; set; }
    }
}
