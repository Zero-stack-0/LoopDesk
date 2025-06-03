using Data.Interface;
using Entities.Models;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Data.Repository
{
    public class RoleRepository : IRoleRepository
    {
        private readonly IMongoCollection<Role> _roleCollection;
        public RoleRepository(MongoDbContext context)
        {
            _roleCollection = context.Role;
        }

        public async Task<Role?> GetByName(string name)
        {
            var filter = Builders<Role>.Filter.And(
                Builders<Role>.Filter.Eq(r => r.Name, name),
                Builders<Role>.Filter.Eq(r => r.IsActive, true));
            return await _roleCollection.Find(filter).FirstOrDefaultAsync();
        }
    }
}