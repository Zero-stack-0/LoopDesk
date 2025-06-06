using Data.Interface;
using Entities.Models;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Data.Repository
{
    public class SubscriptionRepository : ISubscriptionRepository
    {
        private readonly IMongoCollection<Subscription> subscriptionCollection;
        public SubscriptionRepository(MongoDbContext context)
        {
            subscriptionCollection = context.Subscription;
        }

        public async Task<Subscription> CreateAsync(Subscription subscription)
        {
            await subscriptionCollection.InsertOneAsync(subscription);
            return subscription;
        }

        public async Task<Subscription?> GetByName(string name)
        {
            var filter = Builders<Subscription>.Filter.And(
                Builders<Subscription>.Filter.Eq(s => s.Name, name),
                Builders<Subscription>.Filter.Eq(s => s.IsActive, true)
            );
            return await subscriptionCollection.Find(filter).FirstOrDefaultAsync();
        }

        public async Task<bool> Delete(ObjectId id)
        {
            var filter = Builders<Subscription>.Filter.Eq(s => s.Id, id);

            var update = Builders<Subscription>.Update
            .Set(s => s.IsActive, false)
            .Set(s => s.UpdatedAt, DateTime.UtcNow);

            var result = await subscriptionCollection.UpdateOneAsync(filter, update);
            return result.ModifiedCount > 0 ? true : false;
        }

        public async Task<ICollection<Subscription>> GetAllAsync()
        {
            var filter = Builders<Subscription>.Filter.Eq(s => s.IsActive, true);
            return await subscriptionCollection.Find(filter).ToListAsync();
        }

        public async Task<Subscription?> GetById(ObjectId id)
        {
            var filter = Builders<Subscription>.Filter.And(
                Builders<Subscription>.Filter.Eq(s => s.Id, id),
                Builders<Subscription>.Filter.Eq(s => s.IsActive, true)
            );
            return await subscriptionCollection.Find(filter).FirstOrDefaultAsync();
        }
    }
}