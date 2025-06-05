using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Entities.Models
{
    public class CompanyInformation
    {
        public CompanyInformation(string name, string domain, string address, string phone, string logoUrl, string description, ObjectId countryId, ObjectId stateId, ObjectId cityId, ObjectId ownerId)
        {
            Name = name;
            Domain = domain;
            Address = address;
            Phone = phone;
            LogoUrl = logoUrl;
            Description = description;
            CountryId = countryId;
            StateId = stateId;
            CityId = cityId;
            CreatedAt = DateTime.UtcNow;
            IsActive = true;
            UpdatedAt = null;
            OwnerId = ownerId;
        }
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
        [BsonElement]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId OwnerId { get; set; }
    }
}