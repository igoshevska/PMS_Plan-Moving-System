using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using PMS.Data.Repositories;
using PMS.Domain;
using PMS.Services.Interface;
using PMS.ViewModels;
using PMS.ViewModels.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
namespace PMS.Services.Implementation
{
    public class FacebookAuthService : IFacebookAuth
    {
        private const string TokenValidationUrl = "https://graph.facebook.com/debug_token?input_token={0}&access_token={1}|{2}";
        private const string UserInfoUrl = "https://graph.facebook.com/me?fields=first_name,last_name,picture,email&access_token={0}";
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IRepository<User> _usersRepo;
        private readonly ILogger _logger;
        public IConfiguration Configuration { get; }

        public FacebookAuthService(IHttpClientFactory httpClientFactory,
                                   IConfiguration confihuration,
                                   IRepository<User> usersRepo, 
                                   ILogger<FacebookAuthService> logger)
        {
            Configuration = confihuration;
            _httpClientFactory = httpClientFactory;
            _usersRepo = usersRepo;
            _logger = logger;
        }
        public async Task<FacebookTokenValidationResult> ValidateAccessTokenAsync(string accessToken)
        {
            var appId = Configuration["FacebookAuthSettings:AppId"];
            var appSecret = Configuration["FacebookAuthSettings:AppSecret"];

            var formattedUrl = string.Format(TokenValidationUrl, accessToken, appId,
                appSecret);
            var result = await _httpClientFactory.CreateClient().GetAsync(formattedUrl);
            result.EnsureSuccessStatusCode();
            var responseAsString = await result.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<FacebookTokenValidationResult>(responseAsString);
        }

        public async Task<FacebookUserInfoResult> GetUserInfoAsync(string accessToken)
        {
            var formattedUrl = string.Format(UserInfoUrl, accessToken);
            var result = await _httpClientFactory.CreateClient().GetAsync(formattedUrl);
            result.EnsureSuccessStatusCode();
            var responseAsString = await result.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<FacebookUserInfoResult>(responseAsString);
        }

        public User FindUserByEmail(string email)
        {
            try
            {
                var findUser = _usersRepo.Query()
                                         .Include(x => x.Role)
                                         .Where(x => x.Email == email).FirstOrDefault();
                if (findUser != null)
                {
                    return findUser;
                }
                else
                {
                    return new User();
                }
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }

        }

        public async Task<User> LoginWithFacebook(string accessToken)
        {
            try
            {
                var accesstoken = accessToken.Substring(1);
                accessToken = accesstoken.Substring(0, accesstoken.Length - 1);

                var validatedTokenResult = await ValidateAccessTokenAsync(accessToken);

                if (!validatedTokenResult.Data.IsValid)
                {
                    return new User();
                }

                var userInfo = await GetUserInfoAsync(accessToken);
                var user = FindUserByEmail(userInfo.email);

                if (user.Email == null)
                {
                    var newUser = new User()
                    {
                        Email = userInfo.email,
                        UserName = userInfo.email,
                        RoleId = (int)RoleEnum.Client,
                        Name = userInfo.firstName,
                        Surname = userInfo.lastName
                    };

                    _usersRepo.Create(newUser);
                    return newUser;
                }
                return user;

            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }
    }
}
