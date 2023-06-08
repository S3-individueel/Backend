using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VolksmondAPI.Data;
using VolksmondAPI.Models;

namespace VolksmondAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CitizensController : ControllerBase
    {
        private readonly VolksmondAPIContext _context;

        public CitizensController(VolksmondAPIContext context)
        {
            _context = context;
        }

        // GET: api/Citizens
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Citizen>>> GetCitizen()
        {
          if (_context.Citizen == null)
          {
              return NotFound();
          }
            return await _context.Citizen.ToListAsync();
        }

        // GET: api/Citizens/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Citizen>> GetCitizen(int id)
        {
          if (_context.Citizen == null)
          {
              return NotFound();
          }
            var citizen = await _context.Citizen.FindAsync(id);

            if (citizen == null)
            {
                return NotFound();
            }

            return citizen;
        }

        // PUT: api/Citizens/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCitizen(int id, Citizen citizen)
        {
            if (id != citizen.Id)
            {
                return BadRequest();
            }

            _context.Entry(citizen).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CitizenExists(id))
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

        // POST: api/Citizens
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Citizen>> PostCitizen(Citizen citizen)
        {
          if (_context.Citizen == null)
          {
              return Problem("Entity set 'VolksmondAPIContext.Citizen'  is null.");
          }
            _context.Citizen.Add(citizen);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCitizen", new { id = citizen.Id }, citizen);
        }

        // DELETE: api/Citizens/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCitizen(int id)
        {
            if (_context.Citizen == null)
            {
                return NotFound();
            }
            var citizen = await _context.Citizen.FindAsync(id);
            if (citizen == null)
            {
                return NotFound();
            }

            _context.Citizen.Remove(citizen);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CitizenExists(int id)
        {
            return (_context.Citizen?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
