using Entities.Models;

namespace Data.Interface
{
    public interface IRoleRepository
    {
        Task<Role?> GetByName(string name);
    }
}