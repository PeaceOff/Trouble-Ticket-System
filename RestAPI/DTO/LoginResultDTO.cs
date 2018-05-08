using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestAPI.DTO
{
    public class LoginResultDTO
    {
        public object Token { get; set; }

        public string Username { get; set; }

        public string Role { get; set; }
    }
}