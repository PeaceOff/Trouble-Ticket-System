using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using WebApplication.Models.AccountViewModels;
using WebApplication.Services;
using static WebApplication.Models.AccountViewModels.RegisterViewModel;

namespace WebApplication.Controllers
{
    public class AccountController : Controller
    {
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
                HttpResponseMessage response = await client.PostAsync(RestService.BaseAddress + "/api/Account/Login", 
                    new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json"));

                if(response.IsSuccessStatusCode)
                {
                    string dataJSON = await response.Content.ReadAsStringAsync();
                    LoginResponse token = JsonConvert.DeserializeObject<LoginResponse>(dataJSON);

                    RestService.StoreLoginResponse(token);

                    return RedirectToAction("Index", "Tickets");
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
                model.Role = "Worker";
                HttpClient client = new HttpClient();
                HttpResponseMessage response = await client.PostAsync(RestService.BaseAddress + "/api/Account/Register",
                    new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json"));

                if (response.IsSuccessStatusCode)
                {
                    string dataJSON = await response.Content.ReadAsStringAsync();
                    RegisterResponse token = JsonConvert.DeserializeObject<RegisterResponse>(dataJSON);

                    RestService.StoreRegisterResponse(token);

                    return RedirectToAction("Index", "Tickets");
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
            RestService.RemoveToken();
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