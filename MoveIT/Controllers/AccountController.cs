using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using PMS.Domain;
using PMS.Services.Interface;
using PMS.ViewModels;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MoveIT.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class AccountController : ControllerBase
    {
        private readonly IAccount _accountService;

        private readonly IConfiguration _configuration;

        public AccountController(IAccount accountService,
                                 IConfiguration configuration)
        {
            _accountService = accountService;
            _configuration = configuration;
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
