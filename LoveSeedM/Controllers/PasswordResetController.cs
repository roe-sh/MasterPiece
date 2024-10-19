using Microsoft.AspNetCore.Mvc;
using LoveSeedM.DTOs;
using LoveSeedM.Models;
using System.Threading.Tasks;
using System.Linq;
using project7.DTOs;

namespace LoveSeedM.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PasswordResetController : ControllerBase
    {
        private readonly MyDbContext _db;
        private readonly EmailService _emailService;
        private readonly OtpService _otpService;

        public PasswordResetController(MyDbContext db, EmailService emailService, OtpService otpService)
        {
            _db = db;
            _emailService = emailService;
            _otpService = otpService;
        }

        // Request to reset password by generating OTP
        [HttpPost("request-reset")]
        public IActionResult RequestPasswordReset([FromForm] RequestPasswordResetDto dto)
        {
            var user = _db.Users.FirstOrDefault(x => x.Email == dto.Email);
            if (user == null)
            {
                return BadRequest("User not found.");
            }

            var otp = _otpService.GenerateOtp(user.Email);
            _emailService.SendOtpEmail(user.Email, otp);
            return Ok("OTP sent to email.");
        }

        // Method to verify OTP
        [HttpPost("verify-otp")]
        public async Task<IActionResult> VerifyOtp([FromBody] VerifyOtpDto dto)
        {
            var user = await GetUserByEmailAsync(dto.Email);
            if (user == null)
            {
                return BadRequest("User not found.");
            }

            if (_otpService.ValidateOtp(dto.Email, dto.Otp))
            {
                _otpService.ClearOtp(dto.Email);
                return Ok("OTP verified, you can now reset your password.");
            }

            return BadRequest("Invalid OTP.");
        }

        // Reset password method
        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDTO dto)
        {
            var user = await GetUserByEmailAsync(dto.Email);
            if (user == null)
            {
                return BadRequest("User not found.");
            }

            // Hash the new password and update the user
            var hashedPassword = HashPassword(dto.NewPassword);
            user.PasswordHash = System.Text.Encoding.UTF8.GetBytes(hashedPassword);
            user.PasswordSalt = null;  // If you're not using salt with BCrypt, you can remove this

            _db.Users.Update(user);
            await _db.SaveChangesAsync();

            return Ok("Password has been successfully reset.");
        }

        // Helper method to get user by email
        private Task<User> GetUserByEmailAsync(string email)
        {
            var user = _db.Users.FirstOrDefault(x => x.Email == email);
            return Task.FromResult(user);
        }

        // Helper method to hash the password using BCrypt
        private string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);  // Use BCrypt to hash the password
        }
    }
}
