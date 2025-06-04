using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Entities.Models
{
    public class Country
    {
        public Country()
        {

        }
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId Id { get; set; }
        [BsonElement]
        public string Name { get; set; }
        [BsonElement]
        public string Code { get; set; }
        [BsonElement]
        public bool IsActive { get; set; }
    }
}