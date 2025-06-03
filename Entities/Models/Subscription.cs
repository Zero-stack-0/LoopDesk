using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Entities.Models
{
    public class Subscription
    {
        public Subscription(string name, ObjectId userId, string description, int projectsAllowed, int usersAllowed, decimal price)
        {
            Name = name;
            Description = description;
            ProjectsAllowed = projectsAllowed;
            UsersAllowed = usersAllowed;
            Price = price;
            CreatedAt = DateTime.UtcNow;
            IsActive = true;
            UserId = userId;
        }

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId Id { get; set; }

        [BsonElement]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId UserId { get; set; }
        [BsonElement]
        public string Name { get; set; }
        [BsonElement]
        public string Description { get; set; }
        [BsonElement]
        public int ProjectsAllowed { get; set; }
        [BsonElement]
        public int UsersAllowed { get; set; }
        [BsonElement]
        public decimal Price { get; set; }
        [BsonElement]
        public DateTime CreatedAt { get; set; }
        [BsonElement]
        public DateTime? UpdatedAt { get; set; }
        [BsonElement]
        public bool IsActive { get; set; }
    }
}