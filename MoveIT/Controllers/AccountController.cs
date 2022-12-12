using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using PMS.Domain;
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
        private readonly ILogger _logger;

        public AccountController(IAccount accountService,
                                 IConfiguration configuration,                                 
                                 IFacebookAuth facebookAuthService,
                                 ILogger<AccountController> logger)
        {
            _accountService = accountService;
            _configuration = configuration;
            _facebookAuthService = facebookAuthService;
            _logger = logger;
        }

        [AllowAnonymous]
        [HttpPost]
        public ApiResponseModel<string> Login([FromBody] LoginUserViewModel model)
        {

            User res = null;
            var w = new Wrapper.Wrapper(_logger);
            var errorResponse = w.CheckTheMethod(() => { res = _accountService.CheckUserExistence(model); });

            if (res.UserName == null)
                return new ApiResponseModel<string>() 
                {
                    errorHandler = new ErrorHandler
                    {
                        responseCode = errorResponse.responseCode,
                        responseMessage = errorResponse.responseMessage
                    },
                    response = "Error" 
                };

            if (ModelState.IsValid)
            {
                JwtSecurityToken jwtSecurityToken = _accountService.CreateJwtToken(res);
                return new ApiResponseModel<string>()
                {
                    errorHandler = new ErrorHandler
                    {
                        responseCode = errorResponse.responseCode,
                        responseMessage = errorResponse.responseMessage
                    },
                    response = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken)
                };
            }
            else
            {
                return new ApiResponseModel<string>()
                {
                    errorHandler = new ErrorHandler
                    {
                        responseCode = errorResponse.responseCode,
                        responseMessage = errorResponse.responseMessage
                    },
                    response = "Error"
                };
            }
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<ApiResponseModel<string>> LoginWithFacebook([FromBody]string accessToken)
        {

            Task<User> res = null;
            var w = new Wrapper.Wrapper(_logger);
            var errorResponse = w.CheckTheMethod(() => { res = _facebookAuthService.LoginWithFacebook(accessToken); });

            var user = await res;
            if (user.UserName == null)
                return new ApiResponseModel<string>()
                {
                    errorHandler = new ErrorHandler
                    {
                        responseCode = errorResponse.responseCode,
                        responseMessage = errorResponse.responseMessage
                    },
                    response = "Error"
                };

            if (ModelState.IsValid)
            {
                JwtSecurityToken jwtSecurityToken = _accountService.CreateJwtToken(user);
                return new ApiResponseModel<string>() 
                {
                    errorHandler = new ErrorHandler
                    {
                        responseCode = errorResponse.responseCode,
                        responseMessage = errorResponse.responseMessage
                    },
                    response =new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken)
                };
            }
            else
            {
                return new ApiResponseModel<string>()
                {
                    errorHandler = new ErrorHandler
                    {
                        responseCode = errorResponse.responseCode,
                        responseMessage = errorResponse.responseMessage
                    },
                    response = "Error"
                };
            }
        }

        [AllowAnonymous]
        [HttpPost]
        public ApiResponseModel<string> RegisteringUser([FromBody] UserRegistrationViewModel  model)
        {

            string res = null;
            var w = new Wrapper.Wrapper(_logger);
            var errorResponse = w.CheckTheMethod(() => { res = _accountService.RegisterNewUser(model); });

            return new ApiResponseModel<string>()
            {
                errorHandler = new ErrorHandler
                {
                    responseCode = errorResponse.responseCode,
                    responseMessage = errorResponse.responseMessage
                },
                response = res
        };
        }
       
    }
}
