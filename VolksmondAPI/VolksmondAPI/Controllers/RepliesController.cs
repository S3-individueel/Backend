using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Packaging;
using NuGet.Protocol;
using VolksmondAPI.Data;
using VolksmondAPI.Models;

namespace VolksmondAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RepliesController : ControllerBase
    {
        private readonly VolksmondAPIContext _context;

        public RepliesController(VolksmondAPIContext context)
        {
            _context = context;
        }

        // GET: api/Replies
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Reply>>> GetReply()
        {
          if (_context.Reply == null)
          {
              return NotFound();
          }
            return await _context.Reply.ToListAsync();
        }

        // GET: api/Replies/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Reply>> GetReply(int id)
        {
          if (_context.Reply == null)
          {
              return NotFound();
          }
            var reply = await _context.Reply.FindAsync(id);

            if (reply == null)
            {
                return NotFound();
            }

            return reply;
        }

        // PUT: api/Replies/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutReply(int id, Reply reply)
        {
            if (id != reply.Id)
            {
                return BadRequest();
            }

            _context.Entry(reply).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReplyExists(id))
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

        // POST: api/Replies
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Reply>> PostReply(Reply reply)
        {
          if (_context.Reply == null)
          {
              return Problem("Entity set 'VolksmondAPIContext.Reply'  is null.");
          }
            reply.IsDeleted = false;
            reply.IsPinned = false;
            _context.Reply.Add(reply);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw e;
            }

            return CreatedAtAction("GetReply", new { id = reply.Id }, reply);
        }

        // DELETE: api/Replies/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReply(int id)
        {
            if (_context.Reply == null)
            {
                return NotFound();
            }
            var reply = await _context.Reply.FindAsync(id);
            if (reply == null)
            {
                return NotFound();
            }

            _context.Reply.Remove(reply);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/Replies/5
        [HttpPost("{id}/Vote")]
        public async Task<ActionResult<object>> Vote(int id, [FromBody] ReplyVote vote)
        {
            var reply = await _context.Reply.FindAsync(id);
            if (reply == null)
            {
                return NotFound();
            }

            var existingVote = await _context.ReplyVote.FirstOrDefaultAsync(v => v.CitizenId == vote.CitizenId && v.ReplyId == vote.ReplyId);
            if (existingVote == null)
            {
                _context.ReplyVote.Add(vote);
            }
            else
            {
                if (existingVote.Vote == vote.Vote)
                    existingVote.Vote = 0;
                else
                    existingVote.Vote = vote.Vote;

                _context.Entry(existingVote).State = EntityState.Modified;
            }

            await _context.SaveChangesAsync();

            int score = await _context.ReplyVote.Where(r => r.ReplyId == vote.ReplyId).SumAsync(r => r.Vote);
            int voteId = existingVote != null ? existingVote.Id : vote.Id;

            return new { id = voteId, score };
        }

        private bool ReplyExists(int id)
        {
            return (_context.Reply?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
