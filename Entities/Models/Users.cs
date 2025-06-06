using Entities.Enums;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Entities.Models
{
    public class Users
    {
        public Users()
        {

        }

        public Users(string name, string password, string email, ObjectId roleId)
        {
            Name = name;
            Email = email;
            Password = password;
            RoleId = roleId;
            CreatedAt = DateTime.UtcNow;
            IsActive = true;
            UserProfileSetUpStep = UserProfileSetUpStep.BasicInfo;
        }
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId Id { get; set; }
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
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId RoleId { get; set; }

        public Role? Role { get; set; }
        [BsonElement]
        public UserProfileSetUpStep UserProfileSetUpStep { get; set; }
    }
}