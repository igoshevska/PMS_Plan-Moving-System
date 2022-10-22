using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Localization;
using PMS.Services.Interface;
using PMS.ViewModels;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MoveIT.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    [Microsoft.AspNetCore.Authorization.Authorize]
    public class AccountController : ControllerBase
    {
        private readonly IAccount _accountService;
        private readonly IConfiguration _configuration;
        private readonly IFacebookAuth _facebookAuthService;
        //private readonly IStringLocalizer<AccountController> _localizer;

        public AccountController(IAccount accountService,
                                 IConfiguration configuration,                                 
                                 IFacebookAuth facebookAuthService
                                 /*IStringLocalizer<AccountController> localizer*/)
        {
            _accountService = accountService;
            _configuration = configuration;
            _facebookAuthService = facebookAuthService;
            //_localizer = localizer;
        }

        //[AllowAnonymous]
        //[HttpGet]
        //public string Ivana()
        //{
        //    return _localizer["About Title"];
        //}

        [AllowAnonymous]
        [HttpPost]
        public ApiResponseModel<string> Login([FromBody] LoginUserViewModel model)
        {

            var user = _accountService.CheckUserExistence(model);

            if (user.UserName == null)
                return new ApiResponseModel<string>() 
                { 
                  responseCode = 400,
                  responseMessage = "Error",
                  response = "Error" 
                };

            if (ModelState.IsValid)
            {
                JwtSecurityToken jwtSecurityToken = _accountService.CreateJwtToken(user);
                return new ApiResponseModel<string>()
                {
                    responseCode = 200,
                    responseMessage = "OK",
                    response = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken)
                };
            }
            else
            {
                return new ApiResponseModel<string>()
                {
                    responseCode = 400,
                    responseMessage = "Error",
                    response = "Error"
                };
            }
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<ApiResponseModel<string>> LoginWithFacebook([FromBody]string accessToken)
        {
            
            var user = await _facebookAuthService.LoginWithFacebook(accessToken);

            if (user.UserName == null)
                return new ApiResponseModel<string>()
                {
                    responseCode = 400,
                    responseMessage = "Error",
                    response = "Error"
                };

            if (ModelState.IsValid)
            {
                JwtSecurityToken jwtSecurityToken = _accountService.CreateJwtToken(user);
                return new ApiResponseModel<string>() 
                {  
                    responseCode=200,
                    responseMessage = "OK",
                    response=new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken)
                };
            }
            else
            {
                return new ApiResponseModel<string>()
                {
                    responseCode = 400,
                    responseMessage = "Error",
                    response = "Error"
                };
            }
        }

        [AllowAnonymous]
        [HttpPost]
        public ApiResponseModel<string> RegisteringUser([FromBody] UserRegistrationViewModel  model)
        {
            var result = _accountService.RegisterNewUser(model);
            return new ApiResponseModel<string>()
            {
                responseCode = 200,
                responseMessage = "OK",
                response = result
            };
        }
    }
}
