

using MongoDB.Bson;

namespace Service.Dto.Subscription
{
    public class SubscriptionResponse
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int ProjectsAllowed { get; set; }
        public int UsersAllowed { get; set; }
        public decimal Price { get; set; }
    }
}