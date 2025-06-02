using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Attributes;

namespace Entities.Models
{
    public class Users
    {
        public Users()
        {

        }

        public Users(string name, string password, string email)
        {
            Name = name;
            Email = email;
            Password = password;
            CreatedAt = DateTime.UtcNow;
            IsActive = true;
            RoleId = "test";
        }
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string Id { get; set; }
        [BsonElement]
        public string Name { get; set; }
        [BsonElement]
        public string Email { get; set; }
        [BsonElement]
        public string Password { get; set; }
        [BsonElement]
        public DateTime CreatedAt { get; set; }
        [BsonElement]
        public DateTime? LastLoginedAt { get; set; }
        [BsonElement]
        public DateTime? UpdatedAt { get; set; }
        [BsonElement]
        public bool IsActive { get; set; }
        [BsonElement]
        public string RoleId { get; set; }
    }
}