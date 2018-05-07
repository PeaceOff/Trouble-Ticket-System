using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace RestAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    public class TestController : Controller
    {
        [HttpGet]
        public string GetAnonymous()
        {
            return "Anonymous working";
        }

        [HttpGet]
        [Authorize]
        public string GetAuthorized()
        {
            string username = HttpContext.User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
            return "Authorized working with Username: " + username;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public string GetAuthorizedRole()
        {
            string username = HttpContext.User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
            return "Authorized role working with Username: " + username;
        }
    }
}