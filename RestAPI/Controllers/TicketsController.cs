using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestAPI.Data;
using RestAPI.Entities;

namespace RestAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/Tickets")]
    public class TicketsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TicketsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Authorize]
        public IEnumerable<Ticket> GetTicket()
        {
            return _context.Ticket;
        }

        [HttpGet("UnassignedTickets")]
        [Authorize]
        public IEnumerable<Ticket> GetUnassignedTickets()
        {
            return _context.Ticket.Where(t => t.State == "Unassigned").ToList();
        }

        [HttpGet("WorkerTickets")]
        [Authorize]
        public IEnumerable<Ticket> GetWorkerTickets()
        {
            string id = HttpContext.User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;

            return _context.Ticket.Where(t => t.AuthorId == id).ToList();
        }

        [HttpGet("SolverTickets")]
        [Authorize]
        public IEnumerable<Ticket> GetSolverTickets()
        {
            string id = HttpContext.User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
            
            return _context.Ticket.Where(t => t.SolverId == id).ToList();
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetTicket([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var ticket = await _context.Ticket.SingleOrDefaultAsync(m => m.Id == id);

            if (ticket == null)
            {
                return NotFound();
            }

            return Ok(ticket);
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> PutTicket([FromRoute] int id, [FromBody] Ticket ticket)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != ticket.Id)
            {
                return BadRequest();
            }

            _context.Entry(ticket).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TicketExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> PostTicket([FromBody] Ticket ticket)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            ticket.CreatedAt = DateTime.Now;
            ticket.AuthorId = HttpContext.User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
            ticket.State = "Unassigned";

            _context.Ticket.Add(ticket);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTicket", new { id = ticket.Id }, ticket);
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteTicket([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var ticket = await _context.Ticket.SingleOrDefaultAsync(m => m.Id == id);
            if (ticket == null)
            {
                return NotFound();
            }

            _context.Ticket.Remove(ticket);
            await _context.SaveChangesAsync();

            return Ok(ticket);
        }

        private bool TicketExists(int id)
        {
            return _context.Ticket.Any(e => e.Id == id);
        }
    }
}