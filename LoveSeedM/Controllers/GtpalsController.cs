using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LoveSeedM.DTOs;
using LoveSeedM.Models;
using System.Linq;

namespace LoveSeedM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GtpalsController : ControllerBase
    {
        private readonly MyDbContext _context;

        public GtpalsController(MyDbContext context)
        {
            _context = context;
        }

        // GET: api/Gtpals
        [HttpGet]
        public IActionResult GetGtpals()
        {
            var gtpals = _context.Gtpals
                .Select(g => new GtpalDTO
                {
                    Id = g.Id,
                    UserId = g.UserId,
                    Gravida = g.Gravida,
                    Term = g.Term,
                    Preterm = g.Preterm,
                    Abortions = g.Abortions,
                    LivingChildren = g.LivingChildren,
                    RecordDate = g.RecordDate
                })
                .ToList();

            return Ok(gtpals);
        }

        // GET: api/Gtpals/5
        [HttpGet("{id}")]
        public IActionResult GetGtpal(int id)
        {
            var gtpal = _context.Gtpals
                .Where(g => g.Id == id)
                .Select(g => new GtpalDTO
                {
                    Id = g.Id,
                    UserId = g.UserId,
                    Gravida = g.Gravida,
                    Term = g.Term,
                    Preterm = g.Preterm,
                    Abortions = g.Abortions,
                    LivingChildren = g.LivingChildren,
                    RecordDate = g.RecordDate
                })
                .FirstOrDefault();

            if (gtpal == null)
            {
                return NotFound("GTPAL record not found.");
            }

            return Ok(gtpal);
        }

        // PUT: api/Gtpals/5
        [HttpPut("{id}")]
        public IActionResult PutGtpal(int id, [FromBody] UpdateGtpalDTO gtpalDto)
        {
            var gtpal = _context.Gtpals.Find(id);

            if (gtpal == null)
            {
                return NotFound("GTPAL record not found.");
            }

            // Update fields
            gtpal.Gravida = gtpalDto.Gravida;
            gtpal.Term = gtpalDto.Term;
            gtpal.Preterm = gtpalDto.Preterm;
            gtpal.Abortions = gtpalDto.Abortions;
            gtpal.LivingChildren = gtpalDto.LivingChildren;
            gtpal.RecordDate = gtpalDto.RecordDate;

            _context.Entry(gtpal).State = EntityState.Modified;

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GtpalExists(id))
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

        // POST: api/Gtpals
        [HttpPost]
        public IActionResult PostGtpal([FromBody] CreateGtpalDTO gtpalDto)
        {
            var gtpal = new Gtpal
            {
                UserId = gtpalDto.UserId,
                Gravida = gtpalDto.Gravida,
                Term = gtpalDto.Term,
                Preterm = gtpalDto.Preterm,
                Abortions = gtpalDto.Abortions,
                LivingChildren = gtpalDto.LivingChildren,
                RecordDate = gtpalDto.RecordDate
            };

            _context.Gtpals.Add(gtpal);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetGtpal), new { id = gtpal.Id }, gtpal);
        }

        // DELETE: api/Gtpals/5
        [HttpDelete("{id}")]
        public IActionResult DeleteGtpal(int id)
        {
            var gtpal = _context.Gtpals.Find(id);

            if (gtpal == null)
            {
                return NotFound("GTPAL record not found.");
            }

            _context.Gtpals.Remove(gtpal);
            _context.SaveChanges();

            return NoContent();
        }

        private bool GtpalExists(int id)
        {
            return _context.Gtpals.Any(e => e.Id == id);
        }
    }
}
