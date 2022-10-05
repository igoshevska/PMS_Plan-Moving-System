using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace PMS.ViewModels
{
    public class LoginUserViewModel
    {
        public string userName { get; set; }
        public string password { get; set; }
    }

    public class UserRegistrationViewModel 
    {
        public string name { get; set; }
        public string userName { get; set; }
        public string surname { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public string confirmPassword { get; set; }
    }

    public class UserViewModel
    {
        public int id { get; set; }
        public string name { get; set; }
        public string surname { get; set; }
        public string userName { get; set; }
        public string password { get; set; }
        public RoleViewModel role { get; set; }
        public string email { get; set; }
    }

    public class ProposalSerchViewModel
    {
        public int page { get; set; }
        public int rows { get; set; }
        public string searchText { get; set; }  
    }

    public class FacebookUserInfoResult
    {
        [JsonProperty("firstName")]
        public string firstName { get; set; }

        [JsonProperty("lastName")]
        public string lastName { get; set; }

        [JsonProperty("facebookPicture")]
        public FacebookPicture facebookPicture { get; set; }

        [JsonProperty("email")]
        public string email { get; set; }

        [JsonProperty("id")]
        public string id { get; set; }
    }

    public class FacebookPicture
    {
        [JsonProperty("data")]
        public FacebookPictureData data { get; set; }
    }

    public class FacebookPictureData
    {
        [JsonProperty("height")]
        public long height { get; set; }

        [JsonProperty("isSilhouette")]
        public bool isSilhouette { get; set; }

        [JsonProperty("url")]
        public Uri url { get; set; }

        [JsonProperty("width")]
        public long width { get; set; }
    }


    public class FacebookTokenValidationResult
    {
        [JsonProperty("data")]
        public FacebookTokenValidationData Data { get; set; }
    }

    public class FacebookTokenValidationData
    {
        [JsonProperty("app_id")]
        public string AppId { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("application")]
        public string Application { get; set; }

        [JsonProperty("data_access_expires_at")]
        public long DataAccessExpiresAt { get; set; }

        [JsonProperty("expires_at")]
        public long ExpiresAt { get; set; }

        [JsonProperty("is_valid")]
        public bool IsValid { get; set; }

        [JsonProperty("scopes")]
        public string[] Scopes { get; set; }

        [JsonProperty("user_id")]
        public string UserId { get; set; }
    }

    public class AccessTokenViewModel
    {
        public string accessToken { get; set; }
    }
     
}






