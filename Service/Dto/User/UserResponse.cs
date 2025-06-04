using Entities.Enums;
using MongoDB.Bson;
using Service.Dto.Role;

namespace Service.Dto.User
{
    public class UserResponse
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public UserProfileSetUpStep UserProfileSetUpStep { get; set; }
        public RoleResponse Role { get; set; }
    }
}