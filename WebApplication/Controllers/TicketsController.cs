using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using WebApplication.Models;
using WebApplication.Services;

namespace WebApplication.Controllers
{
    public class TicketsController : Controller
    {
        private readonly RestService _restService;

        public TicketsController(RestService restService)
        {
            _restService = restService;
        }

        // GET: Tickets
        public async Task<IActionResult> Index()
        {
            HttpClient client = _restService.GetClient();
            HttpResponseMessage response = await client.GetAsync("api/Tickets/");

            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                IEnumerable<Ticket> tickets = JsonConvert.DeserializeObject<IEnumerable<Ticket>>(content);

                return View(tickets);
            }
            else
            {
                return RedirectToAction("Error", "Home");
            }
        }

        // GET: Tickets/Details/5
        /*public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ticket = await _context.Ticket
                .Include(t => t.Author)
                .Include(t => t.Solver)
                .SingleOrDefaultAsync(m => m.ID == id);
            if (ticket == null)
            {
                return NotFound();
            }

            return View(ticket);
        }*/

        // GET: Tickets/Create
        public IActionResult Create()
        {
            ViewData["Username"] = _restService.GetUsername();
            return View();
        }

        // POST: Tickets/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Title,Description")] Ticket ticket)
        {
            
            if (ModelState.IsValid)
            {
                HttpClient client = _restService.GetClient();
                HttpResponseMessage response = await client.PostAsync(_restService.GetBaseAddress() + "/api/Tickets/",
                    new StringContent(JsonConvert.SerializeObject(ticket), Encoding.UTF8, "application/json"));

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index", "Tickets");
                }
                else
                {
                    return RedirectToAction("Error", "Home");
                }
            }

            return View(ticket);
        }
    }
}
