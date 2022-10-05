using PMS.Domain;
using PMS.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.Services.Interface
{
    public interface IFacebookAuth
    {
        Task<FacebookTokenValidationResult> ValidateAccessTokenAsync(string accessToken);
        Task<FacebookUserInfoResult> GetUserInfoAsync(string accessToken);
        /// <summary>
        /// Login with facebook
        /// </summary>
        /// <param name="accessToken"></param>
        /// <returns></returns>
        Task<User> LoginWithFacebook(string accessToken);
        /// <summary>
        /// Find user by email
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        User FindUserByEmail(string email);
    }
}
