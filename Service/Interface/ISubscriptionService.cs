using Service.Dto.Subscription;
using Service.Dto.User;
using Service.Helper;

namespace Service.Interface
{
    public interface ISubscriptionService
    {
        Task<CommonResponse> Create(CreateSubscriptionRequest request, UserResponse requestor);
        Task<CommonResponse> Delete(string id, UserResponse requestor);
        Task<CommonResponse> GetById(string id, UserResponse requestor);
        Task<CommonResponse> GetAll(UserResponse requestor);
    }
}