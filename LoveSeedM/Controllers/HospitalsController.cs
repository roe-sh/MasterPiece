using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LoveSeedM.DTOs;
using LoveSeedM.Models;
using System.Linq;

namespace LoveSeedM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HospitalsController : ControllerBase
    {
        private readonly MyDbContext _context;

        public HospitalsController(MyDbContext context)
        {
            _context = context;
        }

        // GET: api/Hospitals
        [HttpGet]
        public IActionResult GetHospitals()
        {
            var hospitals = _context.Hospitals
                .Select(h => new HospitalDTO
                {
                    Id = h.Id,
                    Name = h.Name,
                    Address = h.Address,
                    Phone = h.Phone,
                    Email = h.Email,
                    CreatedAt = h.CreatedAt
                }).ToList();

            return Ok(hospitals);
        }

        // GET: api/Hospitals/5
        [HttpGet("{id}")]
        public IActionResult GetHospital(int id)
        {
            var hospital = _context.Hospitals
                .Where(h => h.Id == id)
                .Select(h => new HospitalDTO
                {
                    Id = h.Id,
                    Name = h.Name,
                    Address = h.Address,
                    Phone = h.Phone,
                    Email = h.Email,
                    CreatedAt = h.CreatedAt
                })
                .FirstOrDefault();

            if (hospital == null)
            {
                return NotFound("Hospital not found.");
            }

            return Ok(hospital);
        }

        // PUT: api/Hospitals/5
        [HttpPut("{id}")]
        public IActionResult PutHospital(int id, [FromBody] UpdateHospitalDTO hospitalDto)
        {
            var hospital = _context.Hospitals.Find(id);

            if (hospital == null)
            {
                return NotFound("Hospital not found.");
            }

            hospital.Name = hospitalDto.Name;
            hospital.Address = hospitalDto.Address;
            hospital.Phone = hospitalDto.Phone;
            hospital.Email = hospitalDto.Email;

            _context.Entry(hospital).State = EntityState.Modified;

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HospitalExists(id))
                {
                    return NotFound("Hospital not found.");
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Hospitals
        [HttpPost]
        public IActionResult PostHospital([FromForm] CreateHospitalDTO hospitalDto)
        {
            var hospital = new Hospital
            {
                Name = hospitalDto.Name,
                Address = hospitalDto.Address,
                Phone = hospitalDto.Phone,
                Email = hospitalDto.Email,
                CreatedAt = DateTime.Now
            };

            _context.Hospitals.Add(hospital);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetHospital), new { id = hospital.Id }, hospital);
        }

        // DELETE: api/Hospitals/5
        [HttpDelete("{id}")]
        public IActionResult DeleteHospital(int id)
        {
            var hospital = _context.Hospitals.Find(id);

            if (hospital == null)
            {
                return NotFound("Hospital not found.");
            }

            _context.Hospitals.Remove(hospital);
            _context.SaveChanges();

            return NoContent();
        }

        private bool HospitalExists(int id)
        {
            return _context.Hospitals.Any(e => e.Id == id);
        }
    }
}
