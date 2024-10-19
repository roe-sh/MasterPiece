//using System;
//using System.Collections.Generic;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using LoveSeedM.Models;

//namespace LoveSeedM.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class AppointmentsController : ControllerBase
//    {
//        private readonly MyDbContext _context;

//        public AppointmentsController(MyDbContext context)
//        {
//            _context = context;
//        }

//        // GET: api/Appointments
//        [HttpGet]
//        public IActionResult GetAppointments()
//        {
//            var appointments = _context.Appointment
//                .Include(a => a.User)
//                .Include(a => a.Doctor)
//                .Include(a => a.AdminHandledBy)
//                .Include(a => a.AppointmentReminders)
//                .ToList();

//            return Ok(appointments);
//        }

//        // GET: api/Appointments/5
//        [HttpGet("{id}")]
//        public IActionResult GetAppointment(long id)
//        {
//            var appointment = _context.Appointment
//                .Include(a => a.User)
//                .Include(a => a.Doctor)
//                .Include(a => a.AdminHandledBy)
//                .Include(a => a.AppointmentReminders)
//                .FirstOrDefault(a => a.Id == id);

//            if (appointment == null)
//            {
//                return NotFound();
//            }

//            return Ok(appointment);
//        }

//        // PUT: api/Appointments/5
//        [HttpPut("{id}")]
//        public IActionResult PutAppointment(long id, Appointment appointment)
//        {
//            if (id != appointment.Id)
//            {
//                return BadRequest();
//            }

//            var existingAppointment = _context.Appointment
//                .Include(a => a.User)
//                .Include(a => a.Doctor)
//                .FirstOrDefault(a => a.Id == id);

//            if (existingAppointment == null)
//            {
//                return NotFound();
//            }

//            // Update fields
//            existingAppointment.AppointmentDate = appointment.AppointmentDate;
//            existingAppointment.AppointmentType = appointment.AppointmentType;
//            existingAppointment.Status = appointment.Status;
//            existingAppointment.Notes = appointment.Notes;
//            existingAppointment.UserId = appointment.UserId;
//            existingAppointment.DoctorId = appointment.DoctorId;
//            existingAppointment.AdminHandledById = appointment.AdminHandledById;

//            _context.Entry(existingAppointment).State = EntityState.Modified;

//            try
//            {
//                _context.SaveChanges();
//            }
//            catch (DbUpdateConcurrencyException)
//            {
//                if (!AppointmentExists(id))
//                {
//                    return NotFound();
//                }
//                else
//                {
//                    throw;
//                }
//            }

//            return NoContent();
//        }

//        // POST: api/Appointments
//        [HttpPost]
//        public IActionResult PostAppointment([FromForm] Appointment appointment)
//        {
//            _context.Appointment.Add(appointment);
//            _context.SaveChanges();

//            return CreatedAtAction("GetAppointment", new { id = appointment.Id }, appointment);
//        }

//        // DELETE: api/Appointments/5
//        [HttpDelete("{id}")]
//        public IActionResult DeleteAppointment(long id)
//        {
//            var appointment = _context.Appointment.Find(id);
//            if (appointment == null)
//            {
//                return NotFound();
//            }

//            _context.Appointment.Remove(appointment);
//            _context.SaveChanges();

//            return NoContent();
//        }

//        // Helper method to check if an appointment exists
//        private bool AppointmentExists(long id)
//        {
//            return _context.Appointment.Any(e => e.Id == id);
//        }
//    }
//}
