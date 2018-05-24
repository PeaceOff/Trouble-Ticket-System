using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication.Services
{
    public class LoginResponse
    {
        [JsonProperty("token")]
        public LoginToken AccessToken { get; set; }

        [JsonProperty("username")]
        public string Username { get; set; }

        [JsonProperty("role")]
        public string Role { get; set; }

        public class LoginToken
        {
            [JsonProperty("result")]
            public string Token { get; set; }
        }
    }
}
