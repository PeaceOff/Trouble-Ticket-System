using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication.Services
{
    public class TokenResponse
    {
        [JsonProperty("result")]
        public string AcessToken { get; set; }
    }
}
