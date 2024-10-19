//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using LoveSeedM.DTOs;
//using LoveSeedM.Models;
//using System.Linq;

//namespace LoveSeedM.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class VaccinationsController : ControllerBase
//    {
//        private readonly MyDbContext _context;

//        public VaccinationsController(MyDbContext context)
//        {
//            _context = context;
//        }

//        // GET: api/Vaccinations
//        [HttpGet]
//        public IActionResult GetVaccinations()
//        {
//            var vaccinations = _context.Vaccinations
//                .Include(v => v.Kid)
//                .Include(v => v.AdministeredBy)
//                .Select(v => new VaccinationDTO
//                {
//                    Id = v.Id,
//                    KidId = v.KidId,
//                    VaccineName = v.VaccineName,
//                    VaccinationDate = v.VaccinationDate,
//                    NextDoseDate = v.NextDoseDate,
//                    AdministeredById = v.AdministeredById,
//                    AdministeredByName = v.AdministeredBy != null ? v.AdministeredBy.Username : null
//                }).ToList();

//            return Ok(vaccinations);
//        }

//        // GET: api/Vaccinations/5
//        [HttpGet("{id}")]
//        public IActionResult GetVaccination(int id)
//        {
//            var vaccination = _context.Vaccinations
//                .Include(v => v.Kid)
//                .Include(v => v.AdministeredBy)
//                .Where(v => v.Id == id)
//                .Select(v => new VaccinationDTO
//                {
//                    Id = v.Id,
//                    KidId = v.KidId,
//                    VaccineName = v.VaccineName,
//                    VaccinationDate = v.VaccinationDate,
//                    NextDoseDate = v.NextDoseDate,
//                    AdministeredById = v.AdministeredById,
//                    AdministeredByName = v.AdministeredBy != null ? v.AdministeredBy.Username : null
//                }).FirstOrDefault();

//            if (vaccination == null)
//            {
//                return NotFound("Vaccination not found.");
//            }

//            return Ok(vaccination);
//        }

//        // PUT: api/Vaccinations/5
//        [HttpPut("{id}")]
//        public IActionResult PutVaccination(int id, [FromBody] UpdateVaccinationDTO vaccinationDto)
//        {
//            var vaccination = _context.Vaccinations.Find(id);

//            if (vaccination == null)
//            {
//                return NotFound("Vaccination not found.");
//            }

//            vaccination.VaccineName = vaccinationDto.VaccineName;
//            vaccination.VaccinationDate = vaccinationDto.VaccinationDate;
//            vaccination.NextDoseDate = vaccinationDto.NextDoseDate;

//            _context.Entry(vaccination).State = EntityState.Modified;

//            try
//            {
//                _context.SaveChanges();
//            }
//            catch (DbUpdateConcurrencyException)
//            {
//                if (!VaccinationExists(id))
//                {
//                    return NotFound("Vaccination not found.");
//                }
//                else
//                {
//                    throw;
//                }
//            }

//            return NoContent();
//        }

//        // POST: api/Vaccinations
//        [HttpPost]
//        public IActionResult PostVaccination([FromBody] CreateVaccinationDTO vaccinationDto)
//        {
//            var vaccination = new Vaccination
//            {
//                KidId = vaccinationDto.KidId,
//                VaccineName = vaccinationDto.VaccineName,
//                VaccinationDate = vaccinationDto.VaccinationDate,
//                NextDoseDate = vaccinationDto.NextDoseDate,
//                AdministeredById = vaccinationDto.AdministeredById
//            };

//            _context.Vaccinations.Add(vaccination);
//            _context.SaveChanges();

//            return CreatedAtAction(nameof(GetVaccination), new { id = vaccination.Id }, vaccination);
//        }

//        // DELETE: api/Vaccinations/5
//        [HttpDelete("{id}")]
//        public IActionResult DeleteVaccination(int id)
//        {
//            var vaccination = _context.Vaccinations.Find(id);

//            if (vaccination == null)
//            {
//                return NotFound("Vaccination not found.");
//            }

//            _context.Vaccinations.Remove(vaccination);
//            _context.SaveChanges();

//            return NoContent();
//        }

//        private bool VaccinationExists(int id)
//        {
//            return _context.Vaccinations.Any(e => e.Id == id);
//        }
//    }
//}
