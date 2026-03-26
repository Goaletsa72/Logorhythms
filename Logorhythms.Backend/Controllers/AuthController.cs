using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Logorhythms.Backend.Models;
using Logorhythms.Backend.Models.DTOs;

namespace Logorhythms.Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly BocraHackathonContext _context;
        private readonly IConfiguration _configuration;

        public AuthController(BocraHackathonContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginDto login)
        {
            // Find user by email
            var user = _context.Users.FirstOrDefault(u => u.Email == login.Email);
            if (user == null)
                return Unauthorized("Invalid email or password.");

            // Compare password (plain text for simplicity – in production, use hashing)
            if (user.PasswordHash != login.Password)
                return Unauthorized("Invalid email or password.");

            // Check role (managers have role "admin" – adjust as needed)
            if (user.Role != "admin")
                return Unauthorized("You are not authorized as a manager.");

            // Generate JWT token
            var jwtKey = _configuration["Jwt:Key"] ?? "your-very-secure-key-at-least-32-characters-long!";
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(jwtKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.Role, user.Role)
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            return Ok(new { Token = tokenString });
        }
    }
}