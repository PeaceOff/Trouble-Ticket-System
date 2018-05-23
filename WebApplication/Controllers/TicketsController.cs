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

        // GET: Tickets
        public async Task<IActionResult> Index()
        {
            // Only workers can use this page
            if (RestService.Token == null)
            {
                return RedirectToAction("Index", "Home");
            }

            HttpClient client = RestService.GetClient();
            HttpResponseMessage response = await client.GetAsync("api/Tickets/");

            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                IEnumerable<Ticket> tickets = JsonConvert.DeserializeObject<IEnumerable<Ticket>>(content);

                tickets = tickets.OrderBy(t => t.CreatedAt);
                return View(tickets);
            }
            else
            {
                return RedirectToAction("Error", "Home");
            }
        }

        // GET: Tickets/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            // Only workers can use this page
            if (RestService.Token == null)
            {
                return RedirectToAction("Index", "Home");
            }

            if (id == null)
            {
                return NotFound();
            }

            HttpClient client = RestService.GetClient();
            HttpResponseMessage response = await client.GetAsync("api/Tickets/" + id);

            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                Ticket ticket = JsonConvert.DeserializeObject<Ticket>(content);

                return View(ticket);
            }
            else
            {
                return NotFound();
            }
        }

        // GET: Tickets/Create
        public IActionResult Create()
        {
            // Only workers can use this page
            if (RestService.Token == null)
            {
                return RedirectToAction("Index", "Home");
            }

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
                HttpClient client = RestService.GetClient();
                HttpResponseMessage response = await client.PostAsync(RestService.BaseAddress + "/api/Tickets/",
                    new StringContent(JsonConvert.SerializeObject(ticket), Encoding.UTF8, "application/json"));

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index", "Tickets");
                }
                else
                {
                    return RedirectToAction("Create", "Tickets");
                }
            }

            return View(ticket);
        }
    }
}
