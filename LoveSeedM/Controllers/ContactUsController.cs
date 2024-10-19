using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LoveSeedM.Models;
using LoveSeedM.DTOs;
using System.Net;
using System.Net.Mail;

namespace LoveSeedM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactUsController : ControllerBase
    {
        private readonly MyDbContext _context;
        private readonly IConfiguration _configuration;

        public ContactUsController(MyDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        // GET: api/ContactUs/5 - Admin can view a specific contact form
        [HttpGet("{id}")]
        public async Task<ActionResult<ContactUsDTO>> GetContactForm(int id)
        {
            var contactForm = await _context.ContactUs.FindAsync(id);

            if (contactForm == null)
            {
                return NotFound("Contact form not found.");
            }

            // Map the entity to DTO
            var contactUsDto = new ContactUsDTO
            {
                Name = contactForm.Name,
                PhoneNumber = contactForm.PhoneNumber,
                Subject = contactForm.Subject,
                Email = contactForm.Email,
                Message = contactForm.Message
            };

            return Ok(contactUsDto);
        }

        // GET: api/ContactUs - Example custom endpoint for demonstration
        [HttpGet]
        public IActionResult MultiplyNumber(int a)
        {
            int result = a * a;
            return Ok(result);
        }

        // POST: api/ContactUs - User can submit a contact form
        [HttpPost]
        public IActionResult PostContactForm([FromForm] ContactUsDTO contactUsDto)
        {
            if (contactUsDto == null)
            {
                return BadRequest("Contact form data is required.");
            }

            // Create a new ContactU entity from DTO
            var contactForm = new ContactU
            {
                Name = contactUsDto.Name,
                PhoneNumber = contactUsDto.PhoneNumber,
                Subject = contactUsDto.Subject,
                Email = contactUsDto.Email,
                Message = contactUsDto.Message,
                CreatedAt = DateTime.Now
            };

            _context.ContactUs.Add(contactForm);
            _context.SaveChanges();

            return Ok(contactForm);
        }

        // POST: api/ContactUs/reply/5 - Admin can reply to a contact form and send an email
        [HttpPost("reply/{id}")]
        public async Task<IActionResult> ReplyToContactForm(int id, [FromForm] ContactUsReplyDto replyDto)
        {
            var contactForm = await _context.ContactUs.FindAsync(id);

            if (contactForm == null)
            {
                return NotFound("Contact form not found.");
            }

            // Update the contact form with the admin's reply
            contactForm.ReplyMessage = replyDto.Subject + replyDto.ReplyMessage;
            contactForm.CreatedAt = DateTime.Now;

            _context.Entry(contactForm).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            // Send reply email
            SendEmailAsync(replyDto.Email, replyDto.Subject, replyDto.ReplyMessage);

            return Ok(replyDto);
        }

        // DELETE: api/ContactUs/5 - Admin can delete a contact form
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteContactForm(int id)
        {
            var contactForm = await _context.ContactUs.FindAsync(id);

            if (contactForm == null)
            {
                return NotFound("Contact form not found.");
            }

            _context.ContactUs.Remove(contactForm);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // Helper method to send an email
        private static void SendEmailAsync(string toAddress, string subject, string body)
        {
            try
            {
                string fromAddress = "your-email@example.com";  // Use your email address

                // Set up SMTP client with Gmail settings
                SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587)
                {
                    Credentials = new NetworkCredential("your-email@example.com", "your-email-password"), // Use your email and password
                    EnableSsl = true
                };

                // Create a MailMessage object
                MailMessage mailMessage = new MailMessage(fromAddress, toAddress, subject, body);

                // Send the email
                smtpClient.Send(mailMessage);
                Console.WriteLine("Email sent successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Failed to send email. Error: " + ex.Message);
            }
        }

        // Private method to check if a contact form exists
        private bool ContactFormExists(int id)
        {
            return _context.ContactUs.Any(e => e.Id == id);
        }
    }
}
