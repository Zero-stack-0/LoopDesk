using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Entities.Models
{
    public class City
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId Id { get; set; }
        [BsonElement]
        public string name { get; set; }
        [BsonElement]
        public bool isActive { get; set; }
        [BsonElement]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId stateId { get; set; }
    }
}