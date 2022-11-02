using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Microsoft.IdentityModel.Tokens;
using PMS.Data.Repositories;
using PMS.Domain;
using PMS.Services.Interface;
using PMS.ViewModels;
using PMS.ViewModels.Enums;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PMS.Services.Implementation
{

    public class AccountService: IAccount
    {
        #region Declaration
        private readonly ILogger _logger;
        private readonly IRepository<User> _usersRepo;
        private readonly IConfiguration _configuration;
        #endregion

        #region ctor
        public AccountService(ILogger<AccountService> logger,
                                 IRepository<User> usersRepo,
                                 IConfiguration configuration)
        {
            _logger = logger;
            _usersRepo = usersRepo;
            //_facebookAuthService = facebookAuthService;
            _configuration = configuration;
        }
        #endregion
        public User CheckUserExistence(LoginUserViewModel model)
        {
            try
            {
                var checkUser = _usersRepo.Query()
                                          .Include(x => x.Role)
                                          .Where(x => x.UserName == model.userName && x.Password == TextToEncrypt(model.password))
                                          .FirstOrDefault();

                if (checkUser != null)
                {
                    return checkUser;
                }
                else
                {
                    var checkSalesUser = _usersRepo.Query()
                                         .Include(x => x.Role)
                                         .Where(x => x.UserName == model.userName && x.Password == model.password)
                                         .FirstOrDefault();
                    
                    if (checkSalesUser != null)
                    {
                        return checkSalesUser;
                    }
                    else
                        return new User();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        public string RegisterNewUser(UserRegistrationViewModel model)
        {
            try
            {
                if(model == null)
                    return "Unvalid data";

                if(IsUsernameTaken(model.userName, _usersRepo) == false)
                    return "Account with this user name alredy exist";

                if (IsValidEmail(model.email) == false)
                    return "Unvalid email addres";

                if (IsPasswordMatch(model.password, model.confirmPassword) == false)
                    return "Passwords do NOT match";

                var user = new User();
                user.Password = TextToEncrypt(model.confirmPassword);
                user.Surname = model.surname;
                user.Name = model.name;
                user.UserName = model.userName;
                user.Email = model.email;
                user.RoleId = (int)RoleEnum.Client;

                _usersRepo.Create(user);

                return "Success";


            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        public JwtSecurityToken CreateJwtToken(User user)
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

        private static string TextToEncrypt(string password)
        {
            return Convert.ToBase64String(System.Security.Cryptography.SHA256.Create().ComputeHash(Encoding.UTF8.GetBytes(password)));
        }
        private static bool IsPasswordMatch(string password, string confirmPassword)
        {
            if(password == confirmPassword)
                return true;
            return false;
        }

        private static bool IsUsernameTaken(string userName, IRepository <User> _userRepo) 
        {
            var chechUserName = _userRepo.Query().Where(x => x.UserName == userName).ToList();
            if (chechUserName.Count == 0)
                return true;
            return false;
        }
        private static bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            try
            {
                // Normalize the domain
                email = Regex.Replace(email, @"(@)(.+)$", DomainMapper,
                                      RegexOptions.None, TimeSpan.FromMilliseconds(200));

                // Examines the domain part of the email and normalizes it.
                string DomainMapper(Match match)
                {
                    // Use IdnMapping class to convert Unicode domain names.
                    var idn = new IdnMapping();

                    // Pull out and process domain name (throws ArgumentException on invalid)
                    string domainName = idn.GetAscii(match.Groups[2].Value);

                    return match.Groups[1].Value + domainName;
                }
            }
            catch (RegexMatchTimeoutException e)
            {
                return false;
            }
            catch (ArgumentException e)
            {
                return false;
            }

            try
            {
                return Regex.IsMatch(email,
                    @"^[^@\s]+@[^@\s]+\.[^@\s]+$",
                    RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
        }

   
    }
}
