using Entities.Models;
using MongoDB.Bson;

namespace Data.Interface
{
    public interface ISubscriptionRepository
    {
        Task<Subscription> CreateAsync(Subscription subscription);
        Task<Subscription?> GetByName(string name);
        Task<bool> Delete(ObjectId id);
    }
}