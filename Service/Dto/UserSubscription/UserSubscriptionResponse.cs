using Entities.Enums;
using Service.Dto.CompanyInformation;
using Service.Dto.Subscription;
using Service.Dto.User;

namespace Service.Dto.UserSubscription
{
    public class UserSubscriptionResponse
    {

        public string Id { get; set; }

        public string UserId { get; set; }

        public string CompanyId { get; set; }

        public string SubscriptionId { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public bool IsActive { get; set; }

        public BillingCycle BillingCycle { get; set; }

        public bool IsTrial { get; set; }

        public bool IsCancelled { get; set; }

        public UserResponse User { get; set; }

        public SubscriptionResponse Subscription { get; set; }

        public CompanyResponse Company { get; set; }
    }
}