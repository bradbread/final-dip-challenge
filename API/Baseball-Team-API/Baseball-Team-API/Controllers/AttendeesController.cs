using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Baseball_Team_API.Models;

namespace Baseball_Team_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AttendeesController : ControllerBase
    {
        private readonly TestDBContext _context;

        public AttendeesController(TestDBContext context)
        {
            _context = context;
        }

        // GET: api/Attendees
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Attendee>>> GetAttendee()
        {
            return await _context.Attendee.ToListAsync();
        }

        // GET: api/Attendees/5
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<Attendee>>> GetAttendee(int id)
        {
            var attendee = await (from a in _context.Attendee
                                   //where s.Email == login.Email
                               where a.EventId == id
                               select new Attendee
                               {
                                   EventId = a.EventId,
                                   Username = a.Username,
                                   Paid = a.Paid,
                               }).ToListAsync();

            if (attendee == null)
            {
                return NotFound();
            }

            return attendee;
        }

        // PUT: api/Attendees/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAttendee(int id, Attendee attendee)
        {
            if (id != attendee.EventId)
            {
                return BadRequest();
            }

            _context.Entry(attendee).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AttendeeExists(id))
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

        // POST: api/Attendees
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Attendee>> PostAttendee(Attendee attendee)
        {
            _context.Attendee.Add(attendee);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (AttendeeExists(attendee.EventId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetAttendee", new { id = attendee.EventId }, attendee);
        }

        // DELETE: api/Attendees/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Attendee>> DeleteAttendee(int id)
        {
            var attendee = await _context.Attendee.FindAsync(id);
            if (attendee == null)
            {
                return NotFound();
            }

            _context.Attendee.Remove(attendee);
            await _context.SaveChangesAsync();

            return attendee;
        }

        private bool AttendeeExists(int id)
        {
            return _context.Attendee.Any(e => e.EventId == id);
        }
    }
}
