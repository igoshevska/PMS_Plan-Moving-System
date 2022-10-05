using Microsoft.AspNet.SignalR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using PMS.Domain;
using PMS.Services.Interface;
using PMS.ViewModels;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
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

        public AccountController(IAccount accountService,
                                 IConfiguration configuration,                                 
                                 IFacebookAuth facebookAuthService)
        {
            _accountService = accountService;
            _configuration = configuration;
    
            _facebookAuthService = facebookAuthService;
        }

        [AllowAnonymous]
        [HttpPost]
        public string Login([FromBody] LoginUserViewModel model)
        {
            var user = _accountService.CheckUserExistence(model);

            if (user.UserName == null)
                return "Error";


            if (ModelState.IsValid)
            {
                JwtSecurityToken jwtSecurityToken = CreateJwtToken(user);
                return  new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
            }
            else
            {
                return "Error";
            }
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<string> LoginWithFacebook([FromBody]string accessToken)
        {

            var user = await _facebookAuthService.LoginWithFacebook(accessToken);

            if (user.UserName == null)
                return "Error";


            if (ModelState.IsValid)
            {
                JwtSecurityToken jwtSecurityToken = CreateJwtToken(user);
                return new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
            }
            else
            {
                return "Error";
            }
        }

        [AllowAnonymous]
        [HttpPost]
        public JsonResult RegisteringUser([FromBody] UserRegistrationViewModel  model)
        {
            var result = _accountService.RegisterNewUser(model);
            return new JsonResult(result);
        }

        private JwtSecurityToken CreateJwtToken(User user)
        {

            var claims = new List<Claim>
                        {
                            new Claim(ClaimTypes.Name, user.UserName),
                            new Claim(ClaimTypes.Role, user.Role.Name),
                        };

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Key"]));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);
            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _configuration["JWT:Issuer"],
                audience: _configuration["JWT:Audience"],
                claims: claims,
                signingCredentials: signingCredentials);

            return jwtSecurityToken;
        }
    }
}
