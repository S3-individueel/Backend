using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using NuGet.Packaging;
using NuGet.Protocol;
using VolksmondAPI.Data;
using VolksmondAPI.Models;
using Solution = VolksmondAPI.Models.Solution;

namespace VolksmondAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SolutionsController : ControllerBase
    {
        private readonly VolksmondAPIContext _context;

        public SolutionsController(VolksmondAPIContext context)
        {
            _context = context;
        }

        // GET: api/Solutions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Solution>>> GetSolution()
        {
            if (_context.Solution == null)
            {
                return NotFound();
            }
            var solutions = await _context.Solution.ToListAsync();
            solutions
                .ForEach(s => s.Score = _context.SolutionVote
                .Where(sv => sv.SolutionId == s.Id)
                .Sum(sv => sv.Vote));
            solutions
                .ForEach(s => s.Votes = _context.SolutionVote
                .Where(sv => sv.SolutionId == s.Id && sv.CitizenId == 2)
                .ToList());
            foreach (var solution in solutions)
            {
                solution.Citizen = await _context.Citizen.FirstAsync(c => c.Id == solution.CitizenId);
            }
            return solutions;
        }



        // GET: api/Solutions/{solutionId}/Replies
        [HttpGet("{solutionId}/Replies")]
        public async Task<ActionResult<IEnumerable<Reply>>> GetRepliesBySolutionId(int solutionId)
        {
            if (_context.Reply == null)
            {
                return NotFound();
            }
            var replies = await _context.Reply.Where(r => r.SolutionId == solutionId).ToListAsync();
            foreach (var reply in replies)
            {
                reply.Score = _context.ReplyVote.Where(sv => sv.ReplyId == reply.Id).Sum(sv => sv.Vote);
                reply.Citizen = await _context.Citizen.FirstAsync(c => c.Id == reply.CitizenId);
            }
            replies.RemoveAll(r => r.ReplyId != null);
            return replies;
        }

        // GET: api/Solutions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Solution>> GetSolution(int id)
        {
          if (_context.Solution == null)
          {
              return NotFound();
            }
            var solution = await _context.Solution.FindAsync(id);
            solution.Score = _context.SolutionVote.Where(sv => sv.SolutionId == id).Sum(sv => sv.Vote);
            solution.Votes = await _context.SolutionVote.Where(sv => sv.SolutionId == id && sv.CitizenId == 2).ToListAsync();
            solution.Citizen = await _context.Citizen.FirstAsync(c => c.Id == solution.CitizenId);

            if (solution == null)
            {
                return NotFound();
            }

            return solution;
        }

        // PUT: api/Solutions/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSolution(int id, Solution solution)
        {
            if (id != solution.Id)
            {
                return BadRequest();
            }

            _context.Entry(solution).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SolutionExists(id))
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

        // POST: api/Solutions
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Solution>> PostSolution(Solution solution)
        {
          if (_context.Solution == null)
          {
              return Problem("Entity set 'VolksmondAPIContext.Solution'  is null.");
          }
            _context.Solution.Add(solution);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSolution", new { id = solution.Id }, solution);
        }

        // DELETE: api/Solutions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSolution(int id)
        {
            if (_context.Solution == null)
            {
                return NotFound();
            }
            var solution = await _context.Solution.FindAsync(id);
            if (solution == null)
            {
                return NotFound();
            }

            _context.Solution.Remove(solution);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/Solutions/5
        [HttpPost("{id}/Vote")]
        public async Task<ActionResult<object>> Vote(int id, [FromBody] SolutionVote _vote)
        {
            var solution = await _context.Solution.FindAsync(id);
            if (solution == null)
            {
                return NotFound();
            }

            var existingVote = await _context.SolutionVote.FirstOrDefaultAsync(v => v.CitizenId == _vote.CitizenId && v.SolutionId == _vote.SolutionId);
            if (existingVote == null)
            {
                _context.SolutionVote.Add(_vote);
            }
            else
            {
                if (existingVote.Vote == _vote.Vote)
                    existingVote.Vote = 0;
                else
                    existingVote.Vote = _vote.Vote;

                _context.Entry(existingVote).State = EntityState.Modified;
            }

            await _context.SaveChangesAsync();

            int score = await _context.SolutionVote.Where(sv => sv.SolutionId == _vote.SolutionId).SumAsync(sv => sv.Vote);
            int voteId = existingVote != null ? existingVote.Id : _vote.Id;

            return new { id = voteId, score };
        }


        private bool SolutionExists(int id)
        {
            return (_context.Solution?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
