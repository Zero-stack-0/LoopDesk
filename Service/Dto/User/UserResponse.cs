using MongoDB.Bson;
using Service.Dto.Role;

namespace Service.Dto.User
{
    public class UserResponse
    {
        public ObjectId Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public RoleResponse Role { get; set; }
    }
}