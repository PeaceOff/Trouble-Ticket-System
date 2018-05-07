using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebApplication.Models.AccountViewModels;
using WebApplication.Services;

namespace WebApplication.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApiHttpClient _apiHttpClient;

        public AccountController(ApiHttpClient apiHttpClient)
        {
            _apiHttpClient = apiHttpClient;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;

            if (ModelState.IsValid)
            {
                HttpClient client = new HttpClient();
                HttpResponseMessage response = await client.PostAsync(_apiHttpClient.GetBaseAddress() + "/api/account/login", 
                    new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json"));

                if(response.IsSuccessStatusCode)
                {
                    //TokenResponse tokenResponse =
                    //await response.Content.ReadAsAsync<TokenResponse>();

                    return RedirectToLocal(returnUrl);
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    return View(model);
                }
            }

            return View(model);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            if (ModelState.IsValid)
            {
                HttpClient client = new HttpClient();
                HttpResponseMessage response = await client.PostAsync(_apiHttpClient.GetBaseAddress() + "/api/account/register",
                    new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json"));

                if (response.IsSuccessStatusCode)
                {
                    //TokenResponse tokenResponse =
                    //await response.Content.ReadAsAsync<TokenResponse>();

                    return RedirectToLocal(returnUrl);
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid register attempt.");
                    return View(model);
                }

            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Logout()
        {
            _apiHttpClient.RemoveToken();
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }

        private IActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
        }
    }
}