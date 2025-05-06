using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace OpenLogAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public AuthController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost("token")]
        public IActionResult GenerateToken()
        {

            var claims = new[]
            {
              new Claim(ClaimTypes.Name, "testuser"),
              new Claim(ClaimTypes.Role, "Logger")
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                  claims: claims,
                  expires: DateTime.UtcNow.AddHours(1),
                  signingCredentials: creds);

            return Ok(new { token = new JwtSecurityTokenHandler().WriteToken(token)});
        }

        [Authorize]
        [HttpGet("whoami")]
        public IActionResult WhoAmI()
        {
            var claims = User.Claims.Select(c => new { c.Type, c.Value });

            return Ok(new
            {
                Name = User.Identity?.Name,
                Roles = User.Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value),
                AllClaims = User.Claims.Select(c => new { c.Type, c.Value })

            });
        }

    }
}
