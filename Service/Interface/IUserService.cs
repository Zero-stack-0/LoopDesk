using Service.Dto.User;
using Service.Helper;

namespace Service.Interface
{
    public interface IUserService
    {
        Task<CommonResponse> Create(CreateRequest request);
        Task<CommonResponse> Login(LoginRequest request);
        Task<CommonResponse> GetByEmail(string email);
    }
}