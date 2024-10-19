using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LoveSeedM.DTOs;
using LoveSeedM.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoveSeedM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly MyDbContext _db;
        private readonly TokenGenerator _tokenGenerator;

        public UsersController(MyDbContext db, TokenGenerator tokenGenerator)
        {
            _db = db;
            _tokenGenerator = tokenGenerator;
        }

        // POST: api/Users/Register
        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromForm] UserRegisterRequestDTO registerRequest)
        {
            byte[] hash, salt;
            PasswordHasher.CreatePasswordHash(registerRequest.Password, out hash, out salt);

            var newUser = new User
            {
                Email = registerRequest.Email,
                Username = registerRequest.Username,
                PasswordSalt = salt,
                PasswordHash = hash
            };

            _db.Users.Add(newUser);
            await _db.SaveChangesAsync();
            return Ok(newUser);
        }

        // POST: api/Users/Login
        [HttpPost("Login")]
        public IActionResult Login([FromForm] UserLoginRequestDTO loginRequest)
        {
            var user = _db.Users.FirstOrDefault(u => u.Email == loginRequest.Email);
            if (user == null || !PasswordHasher.VerifyPasswordHash(loginRequest.Password, user.PasswordHash, user.PasswordSalt))
            {
                return Unauthorized("Invalid credentials.");
            }

            var roles = user.Role.Split(" ").ToList();
            var token = _tokenGenerator.GenerateToken(user.Email, roles);

            return Ok(new { Token = token, UserId = user.Id, UserRole = user.Role });
        }

        // GET: api/Users
        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _db.Users.ToListAsync();
            return Ok(users);
        }

        // GET: api/Users/GetUserById/{id}
        [HttpGet("GetUserById/{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            var user = await _db.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        // PUT: api/Users/EditUser/{id}
        [HttpPut("EditUser/{id}")]
        public async Task<IActionResult> EditUser(int id, [FromBody] UserPutDTO editUser)
        {
            var user = await _db.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            if (user.Role != editUser.Role)
            {
                user.Role = editUser.Role;
            }

            _db.Users.Update(user);
            await _db.SaveChangesAsync();

            return Ok(user);
        }

        // PUT: api/Users/ResetPassword
        [HttpPut("ResetPassword")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDTO newPass)
        {
            var user = _db.Users.FirstOrDefault(u => u.Email == newPass.Email);
            if (user == null)
            {
                return NotFound();
            }

            byte[] newHash, newSalt;
            PasswordHasher.CreatePasswordHash(newPass.NewPassword, out newHash, out newSalt);
            user.PasswordHash = newHash;
            user.PasswordSalt = newSalt;

            _db.Users.Update(user);
            await _db.SaveChangesAsync();

            return Ok(user);
        }

        // POST: api/Users/Google
        [HttpPost("Google")]
        public async Task<IActionResult> RegisterWithGoogle([FromBody] RegisterGoogleDTO googleUser)
        {
            var existingUser = _db.Users.FirstOrDefault(x => x.Email == googleUser.email);

            if (existingUser == null)
            {
                byte[] hash, salt;
                PasswordHasher.CreatePasswordHash(googleUser.id, out hash, out salt);

                var newUser = new User
                {
                    Email = googleUser.email,
                    Username = googleUser.displayName,
                    PasswordSalt = salt,
                    PasswordHash = hash
                };

                _db.Users.Add(newUser);
                await _db.SaveChangesAsync();

                var user = _db.Users.FirstOrDefault(x => x.Email == googleUser.email);
                var roles = user.Role.Split(" ").ToList();
                var token = _tokenGenerator.GenerateToken(user.Email, roles);

                return Ok(new { Token = token, UserId = user.Id, UserRole = user.Role });
            }
            else
            {
                if (!PasswordHasher.VerifyPasswordHash(googleUser.id, existingUser.PasswordHash, existingUser.PasswordSalt))
                {
                    return Unauthorized("Invalid credentials.");
                }

                var roles = existingUser.Role.Split(" ").ToList();
                var token = _tokenGenerator.GenerateToken(existingUser.Email, roles);

                return Ok(new { Token = token, UserId = existingUser.Id, UserRole = existingUser.Role });
            }
        }
    }
}
