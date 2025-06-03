using Entities.Models;

namespace Data.Interface
{
    public interface IUserRepository
    {
        Task<Users?> Create(Users user);
        Task<Users?> GetByEmail(string email);
        Task<Users?> GetByEmailAndPassword(string email, string password);
    }
}