using Microsoft.AspNetCore.Mvc;
using WebApplication.Services;

namespace WebApplication.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            // Worker homepage is the tickets
            if(RestService.Token != null)
            {
                return RedirectToAction("Index", "Tickets");
            }

            return View();
        }

    }
}
