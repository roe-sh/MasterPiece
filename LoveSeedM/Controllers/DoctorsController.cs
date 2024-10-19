using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LoveSeedM.DTOs;
using LoveSeedM.Models;
using System.Linq;
using System.Collections.Generic;

namespace LoveSeedM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorsController : ControllerBase
    {
        private readonly MyDbContext _context;

        public DoctorsController(MyDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetDoctors()
        {
            var doctors = _context.Doctors
                .Include(d => d.Feedbacks)
                .Select(doctor => new DoctorDTO
                {
                    Id = doctor.Id,
                    UserId = doctor.UserId,
                    Clinic = doctor.Clinic,
                    Specialization = doctor.Specialization,
                    PhoneNumber = doctor.PhoneNumber,
                    Feedbacks = doctor.Feedbacks.Select(fb => new FeedbackDTO
                    {
                        Id = fb.Id,
                        DoctorId = fb.DoctorId ?? 0,
                        Comment = fb.Comments,
                        Rating = fb.Rating ?? 0,
                        Date = fb.CreatedAt
                    }).ToList()
                })
                .ToList();

            return Ok(doctors);
        }

        [HttpGet("{id}")]
        public IActionResult GetDoctor(int id)
        {
            var doctor = _context.Doctors
                .Include(d => d.Feedbacks)
                .FirstOrDefault(d => d.Id == id);

            if (doctor == null)
            {
                return NotFound("Doctor not found.");
            }

            var doctorDTO = new DoctorDTO
            {
                Id = doctor.Id,
                UserId = doctor.UserId,
                Clinic = doctor.Clinic,
                Specialization = doctor.Specialization,
                PhoneNumber = doctor.PhoneNumber,
                Feedbacks = doctor.Feedbacks.Select(fb => new FeedbackDTO
                {
                    Id = fb.Id,
                    DoctorId = doctor.Id,
                    Comment = fb.Comments,
                    Rating = fb.Rating ?? 0,
                    Date = fb.CreatedAt
                }).ToList()
            };

            return Ok(doctorDTO);
        }

        [HttpPost]
        public IActionResult PostDoctor([FromBody] AddDoctorDTO newDoctor)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var doctor = new Doctor
            {
                UserId = newDoctor.UserId,
                Clinic = newDoctor.Clinic,
                Specialization = newDoctor.Specialization,
                PhoneNumber = newDoctor.PhoneNumber
            };

            _context.Doctors.Add(doctor);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetDoctor), new { id = doctor.Id }, new DoctorDTO
            {
                Id = doctor.Id,
                UserId = doctor.UserId,
                Clinic = doctor.Clinic,
                Specialization = doctor.Specialization,
                PhoneNumber = doctor.PhoneNumber,
                Feedbacks = new List<FeedbackDTO>()
            });
        }

        [HttpPut("{id}")]
        public IActionResult PutDoctor(int id, [FromBody] UpdateDoctorDTO updatedDoctor)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var doctor = _context.Doctors.Find(id);

            if (doctor == null)
            {
                return NotFound("Doctor not found.");
            }

            doctor.Clinic = updatedDoctor.Clinic;
            doctor.Specialization = updatedDoctor.Specialization;
            doctor.PhoneNumber = updatedDoctor.PhoneNumber;

            _context.Entry(doctor).State = EntityState.Modified;

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DoctorExists(id))
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

        [HttpDelete("{id}")]
        public IActionResult DeleteDoctor(int id)
        {
            var doctor = _context.Doctors
                .Include(d => d.Feedbacks)
                .FirstOrDefault(d => d.Id == id);

            if (doctor == null)
            {
                return NotFound("Doctor not found.");
            }

            if (doctor.Feedbacks.Any())
            {
                _context.Feedbacks.RemoveRange(doctor.Feedbacks);
            }

            _context.Doctors.Remove(doctor);
            _context.SaveChanges();

            return NoContent();
        }

        private bool DoctorExists(int id)
        {
            return _context.Doctors.Any(e => e.Id == id);
        }
    }
}
