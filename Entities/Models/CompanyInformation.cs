using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Entities.Models
{
    public class CompanyInformation
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId Id { get; set; }
        [BsonElement]
        public string Name { get; set; }
        [BsonElement]
        public string Domain { get; set; }
        [BsonElement]
        public string Address { get; set; }
        [BsonElement]
        public string Phone { get; set; }
        [BsonElement]
        public string LogoUrl { get; set; }
        [BsonElement]
        public DateTime CreatedAt { get; set; }
        [BsonElement]
        public DateTime? UpdatedAt { get; set; }
        [BsonElement]
        public bool IsActive { get; set; }
        [BsonElement]
        public string Description { get; set; }
        [BsonElement]
        public ObjectId CountryId { get; set; }
        [BsonElement]
        public ObjectId StateId { get; set; }
        [BsonElement]
        public ObjectId CityId { get; set; }
    }
}