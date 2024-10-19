using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LoveSeedM.DTOs;
using LoveSeedM.Models;
using System.Linq;

namespace LoveSeedM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HealthTipsController : ControllerBase
    {
        private readonly MyDbContext _context;

        public HealthTipsController(MyDbContext context)
        {
            _context = context;
        }

        // GET: api/HealthTips
        [HttpGet]
        public IActionResult GetHealthTips()
        {
            var healthTips = _context.HealthTips
                .Select(h => new HealthTipDTO
                {
                    Id = h.Id,
                    Title = h.Title,
                    Content = h.Content,
                    CreatedById = h.CreatedById,
                    CreatedAt = h.CreatedAt,
                    IsActive = h.IsActive
                })
                .ToList();

            return Ok(healthTips);
        }

        // GET: api/HealthTips/5
        [HttpGet("{id}")]
        public IActionResult GetHealthTip(int id)
        {
            var healthTip = _context.HealthTips
                .Where(h => h.Id == id)
                .Select(h => new HealthTipDTO
                {
                    Id = h.Id,
                    Title = h.Title,
                    Content = h.Content,
                    CreatedById = h.CreatedById,
                    CreatedAt = h.CreatedAt,
                    IsActive = h.IsActive
                })
                .FirstOrDefault();

            if (healthTip == null)
            {
                return NotFound("Health Tip not found.");
            }

            return Ok(healthTip);
        }

        // PUT: api/HealthTips/5
        [HttpPut("{id}")]
        public IActionResult PutHealthTip(int id, [FromBody] UpdateHealthTipDTO healthTipDto)
        {
            var healthTip = _context.HealthTips.Find(id);

            if (healthTip == null)
            {
                return NotFound("Health Tip not found.");
            }

            healthTip.Title = healthTipDto.Title;
            healthTip.Content = healthTipDto.Content;
            healthTip.IsActive = healthTipDto.IsActive;

            _context.Entry(healthTip).State = EntityState.Modified;

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HealthTipExists(id))
                {
                    return NotFound("Health Tip not found.");
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/HealthTips
        [HttpPost]
        public IActionResult PostHealthTip([FromForm] CreateHealthTipDTO healthTipDto)
        {
            var healthTip = new HealthTip
            {
                Title = healthTipDto.Title,
                Content = healthTipDto.Content,
                CreatedById = healthTipDto.CreatedById,
                CreatedAt = DateTime.Now,
                IsActive = healthTipDto.IsActive
            };

            _context.HealthTips.Add(healthTip);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetHealthTip), new { id = healthTip.Id }, healthTip);
        }

        // DELETE: api/HealthTips/5
        [HttpDelete("{id}")]
        public IActionResult DeleteHealthTip(int id)
        {
            var healthTip = _context.HealthTips.Find(id);

            if (healthTip == null)
            {
                return NotFound("Health Tip not found.");
            }

            _context.HealthTips.Remove(healthTip);
            _context.SaveChanges();

            return NoContent();
        }

        private bool HealthTipExists(int id)
        {
            return _context.HealthTips.Any(e => e.Id == id);
        }
    }
}
