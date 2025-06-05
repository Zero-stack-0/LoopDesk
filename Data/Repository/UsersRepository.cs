using Data.Interface;
using Entities.Models;
using MongoDB.Bson;
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

            var pipeline = new[]
            {
                new BsonDocument("$match", new BsonDocument{
                    { "Email", email },
                    { "IsActive", true }
                }),

                new BsonDocument("$lookup", new BsonDocument
                {
                    { "from", "role" },
                    { "localField", "RoleId" },
                    { "foreignField", "_id" },
                    { "as", "Role" }
                }),

                new BsonDocument("$unwind", new BsonDocument {
                    { "path", "$Role" },
                    { "preserveNullAndEmptyArrays", true }
                })
            };

            var result = await _usersCollection.Aggregate<Users>(pipeline).FirstOrDefaultAsync();

            return result;
        }

        public async Task<Users?> GetByEmailAndPassword(string email, string password)
        {
            var pipeline = new[]
            {
                new BsonDocument("$match", new BsonDocument{
                    { "Email", email },
                    { "Password", password },
                    { "IsActive", true }
                }),

                new BsonDocument("$lookup", new BsonDocument
                {
                    { "from", "role" },
                    { "localField", "RoleId" },
                    { "foreignField", "_id" },
                    { "as", "Role" }
                }),

                new BsonDocument("$unwind", new BsonDocument {
                    { "path", "$Role" },
                    { "preserveNullAndEmptyArrays", true }
                })
            };

            return await _usersCollection.Aggregate<Users>(pipeline).FirstOrDefaultAsync();
        }

        public async Task<Users?> Update(Users user)
        {
            var filter = Builders<Users>.Filter.Eq(u => u.Id, user.Id);
            var update = Builders<Users>.Update
                .Set(u => u.LastLoginedAt, DateTime.UtcNow)
                .Set(u => u.Name, user.Name)
                .Set(u => u.UserProfileSetUpStep, user.UserProfileSetUpStep)
                .Set(u => u.UpdatedAt, DateTime.UtcNow);

            var result = await _usersCollection.UpdateOneAsync(filter, update);
            return result.ModifiedCount > 0 ? user : null;
        }

        public async Task<Users?> GetById(ObjectId id)
        {
            var pipeline = new[]
            {
                new BsonDocument("$match", new BsonDocument{
                    { "_id", id },
                    { "IsActive", true }
                }),

                new BsonDocument("$lookup", new BsonDocument
                {
                    { "from", "role" },
                    { "localField", "RoleId" },
                    { "foreignField", "_id" },
                    { "as", "Role" }
                }),

                new BsonDocument("$unwind", new BsonDocument {
                    { "path", "$Role" },
                    { "preserveNullAndEmptyArrays", true }
                })
            };

            return await _usersCollection.Aggregate<Users>(pipeline).FirstOrDefaultAsync();
        }
    }
}

