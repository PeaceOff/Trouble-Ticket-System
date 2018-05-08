using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RestAPI.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using RestAPI.DTO;
using RestAPI.Services;
using Microsoft.AspNetCore.Authorization;

namespace RestAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [Authorize(Roles = "Admin")]
    public class AccountController : Controller
    {
        private readonly JwtHelper _jwtHelper;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _configuration;

        public AccountController
        (
            JwtHelper jwtHelper,
            RoleManager<ApplicationRole> roleManager,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IConfiguration configuration
        )
        {
            _jwtHelper = jwtHelper;
            _roleManager = roleManager;
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<object> Login([FromBody] LoginDTO model)
        {
            var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, false, false);

            if (result.Succeeded)
            {
                var appUser = _userManager.Users.SingleOrDefault(r => r.Email == model.Email);
                return _jwtHelper.GenerateJwtToken(model.Email, appUser);
            }

            return BadRequest("Invalid Login Attempt");
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<object> Register([FromBody] RegisterDTO model)
        {
            var user = new ApplicationUser
            {
                UserName = model.Email,
                Email = model.Email
            };
            var result = await _userManager.CreateAsync(user, model.Password);
            
            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, false);
                return _jwtHelper.GenerateJwtToken(model.Email, user);
            }

            return BadRequest("Invalid Register Attempt");
        }

        [HttpPost]
        public async Task<object> AddApplicationRole([FromBody] AddApplicationRoleDTO model)
        {
            if (!String.IsNullOrEmpty(model.RoleName) && (await _roleManager.FindByNameAsync(model.RoleName) != null))
            {
                return BadRequest("Role name already exists");
            }

            ApplicationRole applicationRole =
            new ApplicationRole
            {
                Name = model.RoleName,
                Description = model.Description,
                CreatedDate = DateTime.UtcNow,
                IPAddress = Request.HttpContext.Connection.RemoteIpAddress.ToString()
            };

            IdentityResult roleResult = await _roleManager.CreateAsync(applicationRole);
            if (roleResult.Succeeded)
            {
                return Ok();
            }
            else
            {
                return StatusCode(500);
            }
        }

        [HttpPost]
        public async Task<object> AddUserRole([FromBody] AddUserRoleDTO model)
        {
            ApplicationUser user = await _userManager.FindByNameAsync(model.User);

            IdentityResult roleResult = await _userManager.AddToRoleAsync(user, model.Role);

            if (roleResult.Succeeded)
            {
                return Ok();
            }
            else
            {
                return StatusCode(500);
            }
        }
    }
}