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
    [Route("api/SecondaryTickets")]
    public class SecondaryTicketsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SecondaryTicketsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IEnumerable<SecondaryTicket> GetSecondaryTicket()
        {
            return _context.SecondaryTicket;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSecondaryTicket([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var secondaryTicket = await _context.SecondaryTicket.SingleOrDefaultAsync(m => m.ID == id);

            if (secondaryTicket == null)
            {
                return NotFound();
            }

            return Ok(secondaryTicket);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutSecondaryTicket([FromRoute] int id, [FromBody] SecondaryTicket secondaryTicket)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != secondaryTicket.ID)
            {
                return BadRequest();
            }

            _context.Entry(secondaryTicket).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SecondaryTicketExists(id))
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
        public async Task<IActionResult> PostSecondaryTicket([FromBody] SecondaryTicket secondaryTicket)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.SecondaryTicket.Add(secondaryTicket);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSecondaryTicket", new { id = secondaryTicket.ID }, secondaryTicket);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSecondaryTicket([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var secondaryTicket = await _context.SecondaryTicket.SingleOrDefaultAsync(m => m.ID == id);
            if (secondaryTicket == null)
            {
                return NotFound();
            }

            _context.SecondaryTicket.Remove(secondaryTicket);
            await _context.SaveChangesAsync();

            return Ok(secondaryTicket);
        }

        private bool SecondaryTicketExists(int id)
        {
            return _context.SecondaryTicket.Any(e => e.ID == id);
        }
    }
}