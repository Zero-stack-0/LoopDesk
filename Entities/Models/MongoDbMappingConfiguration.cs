namespace Entities.Models
{
    public class MongoDbMappingConfiguration
    {
        public string? ConnectionString { get; set; }
        public string? DatabaseName { get; set; }
        public List<string>? Collections { get; set; }
    }
}