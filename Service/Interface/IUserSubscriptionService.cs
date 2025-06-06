using Service.Dto.User;
using Service.Dto.UserSubscription;
using Service.Helper;

namespace Service.Interface
{
    public interface IUserSubscriptionService
    {
        Task<CommonResponse> Create(CreateUserSubscription request, UserResponse requestor);
        Task<CommonResponse> GetByCompanyIdAndUserId(GetByCompanyAndUserId request, UserResponse requestor);
    }
}