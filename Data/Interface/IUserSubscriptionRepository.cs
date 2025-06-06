using Entities.Models;
using MongoDB.Bson;

namespace Data.Interface
{
    public interface IUserSubscriptionRepository
    {
        Task<UserSubscription?> GetByUserIdAndCompanyId(ObjectId userId, ObjectId companyId);
        Task<UserSubscription> Create(UserSubscription userSubscription);
        Task<UserSubscription> Get(ObjectId userId, ObjectId companyId);
    }
}