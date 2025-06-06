using Entities.Models;
using MongoDB.Bson;

namespace Data.Interface
{
    public interface ISubscriptionRepository
    {
        Task<Subscription> CreateAsync(Subscription subscription);
        Task<Subscription?> GetByName(string name);
        Task<bool> Delete(ObjectId id);
        Task<ICollection<Subscription>> GetAllAsync();
        Task<Subscription?> GetById(ObjectId id);
    }
}