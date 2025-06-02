using Data.Interface;
using Entities.Models;
using MongoDB.Driver;

namespace Data.Repository
{
    public class UsersRepository : IUserRepository
    {
        private readonly IMongoCollection<Users> _usersCollection;
        public UsersRepository(MongoDbContext context)
        {
            _usersCollection = context.Users;
        }
        public async Task<Users?> Create(Users user)
        {
            return await _usersCollection.InsertOneAsync(user).ContinueWith(task =>
            {
                if (task.IsFaulted || task.IsCanceled)
                {
                    return null;
                }
                return user;
            });
        }

        public async Task<Users?> GetByEmail(string email)
        {
            var cursor = await _usersCollection.FindAsync(_ => _.Email == email && _.IsActive);
            return await cursor.FirstOrDefaultAsync();
        }
    }
}