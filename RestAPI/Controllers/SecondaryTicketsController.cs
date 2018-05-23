using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestAPI.Data;
using RestAPI.Entities;
using RestAPI.Services;

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

        [HttpGet("UnassignedSecondaryTickets")]
        public IEnumerable<SecondaryTicket> GetUnassignedSecondaryTickets()
        {
            return _context.SecondaryTicket.Where(st => st.Answer == null).ToList();
        }

        [HttpGet("SolverUnsolvedSecondaryTickets")]
        public IEnumerable<SecondaryTicket> GetSolverUnsolvedSecondaryTickets()
        {
            string id = HttpContext.User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;

            return _context.SecondaryTicket.Where(st => st.Ticket.SolverId == id && st.Ticket.State != "Solved").ToList();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSecondaryTicket([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var secondaryTicket = await _context.SecondaryTicket.SingleOrDefaultAsync(m => m.Id == id);

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

            if (id != secondaryTicket.Id)
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

        [HttpPut]
        public async Task<IActionResult> AnswerSecondaryTicket([FromRoute] int id, [FromBody] string answer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var secondaryTicket = await _context.SecondaryTicket.SingleOrDefaultAsync(m => m.Id == id);

            if (secondaryTicket == null)
            {
                return NotFound();
            }

            secondaryTicket.Answer = answer;

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

            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> PostSecondaryTicket([FromBody] SecondaryTicket secondaryTicket)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            secondaryTicket.CreatedAt = DateTime.Now;
            _context.SecondaryTicket.Add(secondaryTicket);

            Ticket ticket = _context.Ticket.Single(t => t.Id == secondaryTicket.TicketId);
            ticket.State = "WaitingForAnswers";
            _context.Entry(ticket).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            MessageQueue.SendMessageToDepartment(secondaryTicket.Id.ToString(), secondaryTicket.Title, secondaryTicket.Description);

            return CreatedAtAction("GetSecondaryTicket", new { id = secondaryTicket.Id }, secondaryTicket);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSecondaryTicket([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var secondaryTicket = await _context.SecondaryTicket.SingleOrDefaultAsync(m => m.Id == id);
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
            return _context.SecondaryTicket.Any(e => e.Id == id);
        }
    }
}