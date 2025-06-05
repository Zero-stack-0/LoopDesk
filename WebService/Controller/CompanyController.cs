using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Service.Dto.CompanyInformation;
using Service.Interface;
using Service.Dto.User;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using WebService.Auth;
using Service.Interface;

namespace WebService.Controller
{
    [ApiController]
    [Route("api/v1/company")]
    public class CompanyController : ControllerBase
    {
        private readonly ICompanyInformationService companyService;
        private readonly UserProfile userProfile;
        public CompanyController(ICompanyInformationService companyService, UserProfile userProfile)
        {
            this.companyService = companyService;
            this.userProfile = userProfile;
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] CreateCompanyInformationRequest request)
        {
            var claimsIdentity = User.Identity as ClaimsIdentity;
            var userResponse = await userProfile.GetUserDetail(claimsIdentity);
            return Ok(await companyService.CreateCompany(request, userResponse));
        }
    }
}