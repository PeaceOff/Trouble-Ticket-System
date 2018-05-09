using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        public IEnumerable<Ticket> GetTicket()
        {
            return _context.Ticket;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTicket([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var ticket = await _context.Ticket.SingleOrDefaultAsync(m => m.ID == id);

            if (ticket == null)
            {
                return NotFound();
            }

            return Ok(ticket);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutTicket([FromRoute] int id, [FromBody] Ticket ticket)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != ticket.ID)
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
        public async Task<IActionResult> PostTicket([FromBody] Ticket ticket)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Ticket.Add(ticket);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTicket", new { id = ticket.ID }, ticket);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTicket([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var ticket = await _context.Ticket.SingleOrDefaultAsync(m => m.ID == id);
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
            return _context.Ticket.Any(e => e.ID == id);
        }
    }
}