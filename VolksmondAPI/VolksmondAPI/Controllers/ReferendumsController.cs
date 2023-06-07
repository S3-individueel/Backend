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
    public class ReferendumsController : ControllerBase
    {
        private readonly VolksmondAPIContext _context;

        public ReferendumsController(VolksmondAPIContext context)
        {
            _context = context;
        }

        // GET: api/Referendums
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Referendum>>> GetReferendum()
        {
          if (_context.Referendum == null)
          {
              return NotFound();
          }
            return await _context.Referendum.ToListAsync();
        }

        // GET: api/Referendums/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Referendum>> GetReferendum(int id)
        {
          if (_context.Referendum == null)
          {
              return NotFound();
          }
            var referendum = await _context.Referendum.FindAsync(id);

            if (referendum == null)
            {
                return NotFound();
            }

            return referendum;
        }

        // PUT: api/Referendums/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutReferendum(int id, Referendum referendum)
        {
            if (id != referendum.Id)
            {
                return BadRequest();
            }

            _context.Entry(referendum).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReferendumExists(id))
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

        // POST: api/Referendums
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Referendum>> PostReferendum(Referendum referendum)
        {
          if (_context.Referendum == null)
          {
              return Problem("Entity set 'VolksmondAPIContext.Referendum'  is null.");
          }
            _context.Referendum.Add(referendum);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetReferendum", new { id = referendum.Id }, referendum);
        }

        // DELETE: api/Referendums/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReferendum(int id)
        {
            if (_context.Referendum == null)
            {
                return NotFound();
            }
            var referendum = await _context.Referendum.FindAsync(id);
            if (referendum == null)
            {
                return NotFound();
            }

            _context.Referendum.Remove(referendum);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ReferendumExists(int id)
        {
            return (_context.Referendum?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
