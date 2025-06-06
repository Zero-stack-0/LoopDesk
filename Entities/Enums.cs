namespace Entities.Enums
{
    public enum Role
    {
        Admin = 1,
        CompanyAdmin = 2,
        User = 3
    }

    public enum UserProfileSetUpStep
    {
        BasicInfo = 1,
        CompanyInfo = 2,
        Subscription = 3,
        Completed = 4
    }

    public enum SubscriptionStatus
    {
        Active = 1,
        Inactive = 2,
        Expired = 3
    }

    public enum PaymentStatus
    {
        Pending = 1,
        Completed = 2,
        Failed = 3
    }

    public enum BillingCycle
    {
        OneTime = 1,
        Monthly = 2,
        Yearly = 3
    }
}


