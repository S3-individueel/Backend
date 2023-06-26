using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using moovdAPI.Data;
using moovdAPI.Models;

namespace moovdAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GPSController : ControllerBase
    {
        private readonly moovdAPIContext _context;

        public GPSController(moovdAPIContext context)
        {
            _context = context;
        }

        // GET: api/GPS
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GPS>>> GetGPS(int pageNumber = 1, int pageSize = 5, string deviceId = null, string deviceType = null)
        {
            if (_context.GPS == null)
            {
                return NotFound();
            }

            if (deviceId != null)
            {
                var filteredData = _context.GPS.Where(x => x.DeviceId == deviceId).ToListAsync();
            }

            var paginatedData = _context.GPS.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
            return await paginatedData;
        }

        // GET: api/GPS/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GPS>> GetGPS(int? id)
        {
          if (_context.GPS == null)
          {
              return NotFound();
          }
            var gPS = await _context.GPS.FindAsync(id);

            if (gPS == null)
            {
                return NotFound();
            }

            return gPS;
        }

        // PUT: api/GPS/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGPS(int? id, GPS gPS)
        {
            if (id != gPS.Id)
            {
                return BadRequest();
            }

            _context.Entry(gPS).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GPSExists(id))
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

        // POST: api/GPS
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<GPS>> PostGPS(GPS gPS)
        {
          if (_context.GPS == null)
          {
              return Problem("Entity set 'moovdAPIContext.GPS'  is null.");
          }
            _context.GPS.Add(gPS);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetGPS", new { id = gPS.Id }, gPS);
        }

        // DELETE: api/GPS/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGPS(int? id)
        {
            if (_context.GPS == null)
            {
                return NotFound();
            }
            var gPS = await _context.GPS.FindAsync(id);
            if (gPS == null)
            {
                return NotFound();
            }

            _context.GPS.Remove(gPS);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool GPSExists(int? id)
        {
            return (_context.GPS?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
