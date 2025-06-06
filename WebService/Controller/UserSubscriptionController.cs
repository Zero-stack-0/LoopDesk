using Microsoft.AspNetCore.Authorization;
using Service.Interface;
using WebService.Auth;
using Microsoft.AspNetCore.Mvc;
using Service.Dto.UserSubscription;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace WebService.Controller
{
    [Authorize]
    [ApiController]
    [Route("api/v1/user-subscription")]
    public class UserSubscriptionController : ControllerBase
    {
        private readonly IUserSubscriptionService _userSubscriptionService;
        private readonly UserProfile userProfile;
        public UserSubscriptionController(IUserSubscriptionService userSubscriptionService, UserProfile userProfile)
        {
            _userSubscriptionService = userSubscriptionService;
            this.userProfile = userProfile;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateUserSubscription([FromBody] CreateUserSubscription request)
        {
            var requestor = await userProfile.GetUserDetail(User.Identity as ClaimsIdentity);
            return Ok(await _userSubscriptionService.Create(request, requestor));
        }

        [HttpGet("get")]
        public async Task<IActionResult> GetByCompanyIdAndUserId([FromQuery] GetByCompanyAndUserId request)
        {
            return Ok(await _userSubscriptionService.GetByCompanyIdAndUserId(request, await userProfile.GetUserDetail(User.Identity as ClaimsIdentity)));
        }
    }
}