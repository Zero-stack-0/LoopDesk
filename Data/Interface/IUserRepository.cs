using Entities.Models;
using MongoDB.Bson;

namespace Data.Interface
{
    public interface IUserRepository
    {
        Task<Users?> Create(Users user);
        Task<Users?> GetByEmail(string email);
        Task<Users?> GetByEmailAndPassword(string email, string password);
        Task<Users?> Update(Users user);
        Task<Users?> GetById(ObjectId id);
    }
}