using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using VolksmondAPI.Data;
using VolksmondAPI.Models;
using Solution = VolksmondAPI.Models.Solution;

namespace VolksmondAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProblemsController : ControllerBase
    {
        private readonly VolksmondAPIContext _context;

        public ProblemsController(VolksmondAPIContext context)
        {
            _context = context;
        }

        // GET: api/Problems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Problem>>> GetProblem()
        {
            if (_context.Problem == null)
            {
                return NotFound();
            }
            var problems = await _context.Problem.ToListAsync();
            foreach (var problem in problems)
            {
                problem.Citizen = await _context.Citizen.FirstAsync(c => c.Id == problem.CitizenId);
            }
            return problems;
        }

        // GET: api/Problems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Problem>> GetProblem(int id)
        {
          if (_context.Problem == null)
          {
              return NotFound();
          }
            var problem = await _context.Problem.FindAsync(id);
            problem.Citizen = await _context.Citizen.FirstAsync(c => c.Id == problem.CitizenId);

            if (problem == null)
            {
                return NotFound();
            }

            return problem;
        }

        [HttpGet("{id}/Solutions")]
        public async Task<ActionResult<IEnumerable<Solution>>> GetSolutionByProblemId(int id)
        {
            if (_context.Solution == null)
            {
                return NotFound();
            }
            var solutions = await _context.Solution.Where(s => s.ProblemId == id).ToListAsync();
            foreach (var solution in solutions)
            {
                solution.Score = _context.SolutionVote.Where(sv => sv.SolutionId == solution.Id).Sum(sv => sv.Vote);
                solution.Citizen = await _context.Citizen.FirstAsync(c => c.Id == solution.CitizenId);
            }
            //solution.Votes = await _context.SolutionVote.Where(sv => sv.SolutionId == id && sv.CitizenId == 2).ToListAsync();

            if (solutions == null)
            {
                return NotFound();
            }

            return solutions;
        }

        // PUT: api/Problems/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProblem(int id, Problem problem)
        {
            if (id != problem.Id)
            {
                return BadRequest();
            }

            _context.Entry(problem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProblemExists(id))
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

        // POST: api/Problems
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Problem>> PostProblem(Problem problem)
        {
          if (_context.Problem == null)
          {
              return Problem("Entity set 'VolksmondAPIContext.Problem'  is null.");
          }
            _context.Problem.Add(problem);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProblem", new { id = problem.Id }, problem);
        }

        // DELETE: api/Problems/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProblem(int id)
        {
            if (_context.Problem == null)
            {
                return NotFound();
            }
            var problem = await _context.Problem.FindAsync(id);
            if (problem == null)
            {
                return NotFound();
            }

            _context.Problem.Remove(problem);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProblemExists(int id)
        {
            return (_context.Problem?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
