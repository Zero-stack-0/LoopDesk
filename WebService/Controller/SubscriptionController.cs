using Service.Interface;
using WebService.Auth;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Service.Dto.Subscription;
using Microsoft.AspNetCore.Authorization;
using Service.Helper;


namespace WebService.Controller
{
    [Authorize]
    [ApiController]
    [Route("api/v1/subscription")]
    public class SubscriptionController : ControllerBase
    {
        private readonly ISubscriptionService subscriptionService;
        private readonly UserProfile userProfile;
        public SubscriptionController(ISubscriptionService subscriptionService, UserProfile userProfile)
        {
            this.subscriptionService = subscriptionService;
            this.userProfile = userProfile;
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] CreateSubscriptionRequest request)
        {
            var claimsIdentity = User.Identity as ClaimsIdentity;
            var userResponse = await userProfile.GetUserDetail(claimsIdentity);
            if (userResponse == null)
            {
                return Unauthorized(new CommonResponse(StatusCodes.Status401Unauthorized, null, "Unauthorized"));
            }

            var response = await subscriptionService.Create(request, userResponse);
            return Ok(response);
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> Delete([FromQuery] string id)
        {
            var claimsIdentity = User.Identity as ClaimsIdentity;
            var userResponse = await userProfile.GetUserDetail(claimsIdentity);
            if (userResponse == null)
            {
                return Unauthorized(new CommonResponse(StatusCodes.Status401Unauthorized, null, "Unauthorized"));
            }

            var response = await subscriptionService.Delete(id, userResponse);
            return Ok(response);
        }

        [HttpGet("get-by-id")]
        public async Task<IActionResult> GetById([FromQuery] string id)
        {
            return Ok(await subscriptionService.GetById(id, await userProfile.GetUserDetail(User.Identity as ClaimsIdentity)));
        }

        [HttpGet("get-all")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await subscriptionService.GetAll(await userProfile.GetUserDetail(User.Identity as ClaimsIdentity)));
        }
    }
}