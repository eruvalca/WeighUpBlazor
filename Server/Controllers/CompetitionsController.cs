using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WeighUpBlazor.Server.Data;
using WeighUpBlazor.Shared.Models;

namespace WeighUpBlazor.Server.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CompetitionsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CompetitionsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Competition>>> GetCompetitions()
        {
            return await _context.Competitions
                .Include(c => c.Contestants)
                .ToListAsync();
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<Competition>> GetCompetition(int id)
        {
            var competition = await _context.Competitions.FindAsync(id);

            if (competition == null)
            {
                return NotFound();
            }

            return await _context.Competitions
                .Include(c => c.Contestants)
                .ThenInclude(c => c.WeightLogs)
                .Include(c => c.WeighInDeadlines)
                .FirstOrDefaultAsync(c => c.CompetitionId == id);
        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCompetition(int id, Competition competition)
        {
            if (id != competition.CompetitionId)
            {
                return BadRequest();
            }

            _context.Entry(competition).State = EntityState.Modified;

            foreach (var deadline in competition.WeighInDeadlines)
            {
                deadline.WeighInDeadlineId = 0;
            }

            await _context.WeighInDeadlines.AddRangeAsync(competition.WeighInDeadlines);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CompetitionExists(id))
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

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<Competition>> PostCompetition(Competition competition)
        {
            _context.Competitions.Add(competition);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCompetition", new { id = competition.CompetitionId }, competition);
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCompetition(int id)
        {
            var competition = await _context.Competitions.FindAsync(id);
            if (competition == null)
            {
                return NotFound();
            }

            _context.Competitions.Remove(competition);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CompetitionExists(int id)
        {
            return _context.Competitions.Any(e => e.CompetitionId == id);
        }
    }
}
