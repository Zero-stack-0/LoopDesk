using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Service.Dto.User;
using Service.Interface;

namespace WebService.Controller
{
    [ApiController]
    [Route("api/v1")]
    public class UserController : ControllerBase
    {
        private readonly IUserService userService;
        public UserController(IUserService userService)
        {
            this.userService = userService;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateUser([FromBody] CreateRequest request)
        {
            return Ok(await userService.Create(request));
        }
    }
}