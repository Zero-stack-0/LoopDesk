using Data.Interface;
using Entities.Models;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Data.Repository
{
    public class UserSubscriptionRepository : IUserSubscriptionRepository
    {
        private readonly IMongoCollection<UserSubscription> userSubscriptionCollection;
        public UserSubscriptionRepository(MongoDbContext context)
        {
            userSubscriptionCollection = context.UserSubscription;
        }

        public async Task<UserSubscription?> GetByUserIdAndCompanyId(ObjectId userId, ObjectId companyId)
        {
            return await userSubscriptionCollection
                .Find(us => us.UserId == userId && us.CompanyId == companyId)
                .FirstOrDefaultAsync();
        }

        public async Task<UserSubscription> Create(UserSubscription userSubscription)
        {
            await userSubscriptionCollection.InsertOneAsync(userSubscription);
            return userSubscription;
        }

        public async Task<UserSubscription> Get(ObjectId userId, ObjectId companyId)
        {
            var pipeline = new[]
            {
                new BsonDocument("$match", new BsonDocument
                {
                    { "UserId", userId },
                    { "CompanyId", companyId },
                    { "IsActive", true }
                }),
                new BsonDocument("$lookup", new BsonDocument
                {
                    { "from", "subscription" },
                    { "localField", "SubscriptionId" },
                    { "foreignField", "_id" },
                    { "as", "Subscription" }
                }),
                new BsonDocument("$lookup", new BsonDocument
                {
                    { "from", "companyinfo" },
                    { "localField", "CompanyId" },
                    { "foreignField", "_id" },
                    { "as", "Company" }
                }),
                new BsonDocument("$lookup", new BsonDocument
                {
                    { "from", "users" },
                    { "localField", "UserId" },
                    { "foreignField", "_id" },
                    { "as", "User" }
                }),
                new BsonDocument("$unwind", new BsonDocument
                {
                    { "path", "$Company" },
                    { "preserveNullAndEmptyArrays", true }
                }),
                new BsonDocument("$unwind", new BsonDocument
                {
                    { "path", "$User" },
                    { "preserveNullAndEmptyArrays", true }
                }),
                new BsonDocument("$unwind", new BsonDocument
                {
                    { "path", "$Subscription" },
                    { "preserveNullAndEmptyArrays", true }
                })
            };

            return await userSubscriptionCollection.Aggregate<UserSubscription>(pipeline).FirstOrDefaultAsync();
        }
    }
}