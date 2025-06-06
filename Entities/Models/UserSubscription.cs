using Entities.Enums;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Entities.Models
{
    public class UserSubscription
    {
        public UserSubscription()
        { }
        public UserSubscription(ObjectId userId, ObjectId companyId, ObjectId subscriptionId, DateTime startDate, DateTime? endDate, BillingCycle billingCycle, bool isTrial)
        {
            UserId = userId;
            CompanyId = companyId;
            SubscriptionId = subscriptionId;
            StartDate = startDate;
            EndDate = endDate;
            CreatedAt = DateTime.UtcNow;
            IsActive = true;
            UpdatedAt = null;
            BillingCycle = billingCycle;
            IsTrial = isTrial;
        }
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId Id { get; set; }
        [BsonElement]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId UserId { get; set; }
        [BsonElement]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId CompanyId { get; set; }
        [BsonElement]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId SubscriptionId { get; set; }
        [BsonElement]
        public DateTime StartDate { get; set; }
        [BsonElement]
        public DateTime? EndDate { get; set; }
        [BsonElement]
        public DateTime CreatedAt { get; set; }
        [BsonElement]
        public DateTime? UpdatedAt { get; set; }
        [BsonElement]
        public bool IsActive { get; set; }
        [BsonElement]
        public BillingCycle BillingCycle { get; set; }
        [BsonElement]
        public bool IsTrial { get; set; }
        [BsonElement]
        public bool IsCancelled { get; set; }
        [BsonElement]
        [BsonIgnore]
        public Users User { get; set; }
        [BsonElement]
        [BsonIgnore]
        public Subscription Subscription { get; set; }
        [BsonElement]
        [BsonIgnore]
        public CompanyInformation Company { get; set; }
    }
}