using System.Security.Claims;
using Service.Dto.User;
using Service.Interface;

namespace WebService.Auth
{
    public class UserProfile
    {
        private readonly IUserService userService;
        public UserProfile(IUserService userService)
        {
            this.userService = userService;
        }
        public async Task<UserResponse?> GetUserDetail(ClaimsIdentity? claimsIdentity)
        {
            if (claimsIdentity is not null && !claimsIdentity.Claims.Any())
            {
                return null;
            }

            var requestorEmailId = claimsIdentity.Claims.FirstOrDefault(e => e.Type.Contains("email")).Value;

            try
            {
                var data = await userService.GetByEmail(requestorEmailId);
                return data.StatusCode == StatusCodes.Status200OK ? data.Result as UserResponse : null;
            }
            catch
            {
                return null;
            }
        }
    }
}