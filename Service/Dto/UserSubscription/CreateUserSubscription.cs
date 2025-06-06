using Entities.Enums;

namespace Service.Dto.UserSubscription
{
    public class CreateUserSubscription
    {
        public string SubscriptionId { get; set; }
        public string CompanyId { get; set; }
        public BillingCycle BillingCycle { get; set; }
    }
}