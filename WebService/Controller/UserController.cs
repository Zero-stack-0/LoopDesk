using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Service.Dto.User;
using Service.Interface;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Service.Helper;
using WebService.Auth;
using System.Reflection.Metadata;
using Entities;

namespace WebService.Controller
{
    [ApiController]
    [Route("api/v1/user")]
    public class UserController : ControllerBase
    {
        private readonly IUserService userService;
        private readonly GenerateJwtToken generateJwtToken;
        private readonly UserProfile userProfile;
        public UserController(IUserService userService, GenerateJwtToken generateJwtToken, UserProfile userProfile)
        {
            this.userService = userService;
            this.generateJwtToken = generateJwtToken;
            this.userProfile = userProfile;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateUser([FromBody] CreateRequest request)
        {
            var data = await userService.Create(request);
            if (data.StatusCode == StatusCodes.Status200OK)
            {
                var userResponse = data.Result as UserResponse;
                if (userResponse != null)
                {
                    var token = generateJwtToken.GenerateToken(userResponse.Name, userResponse.Role.Name, userResponse.Email);
                    return Ok(new CommonResponse(StatusCodes.Status200OK, token, Constants.USER_VALIDATIONS.USER_CREATED_SUCCESSFULLY));
                }
            }
            return Ok(data);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var response = await userService.Login(request);
            if (response.StatusCode == StatusCodes.Status200OK)
            {
                var userResponse = response.Result as UserResponse;
                if (userResponse != null)
                {
                    var token = generateJwtToken.GenerateToken(userResponse.Name, userResponse.Role.Name, userResponse.Email);
                    return Ok(new CommonResponse(StatusCodes.Status200OK, token, "Login successful"));
                }
            }
            return Ok(response);
        }

        [Authorize]
        [HttpGet("profile")]
        public async Task<IActionResult> GetUserProfile()
        {
            var claimsIdentity = User.Identity as ClaimsIdentity;
            if (claimsIdentity == null || !claimsIdentity.Claims.Any())
            {
                return Unauthorized(new CommonResponse(StatusCodes.Status401Unauthorized, null, "Unauthorized"));
            }

            var userDetail = await userProfile.GetUserDetail(claimsIdentity);
            if (userDetail == null)
            {
                return NotFound(new CommonResponse(StatusCodes.Status404NotFound, null, Constants.USER_VALIDATIONS.USER_NOT_FOUND));
            }

            return Ok(new CommonResponse(StatusCodes.Status200OK, userDetail, "User profile retrieved successfully"));
        }
    }
}