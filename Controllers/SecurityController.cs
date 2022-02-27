using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace JWTToken_App.Controllers
{
    [Route("api/auth")]
    public class SecurityController : ControllerBase
    {
        private ILogger<SecurityController> _logger;
        private IConfiguration _configuration;
        public SecurityController(ILogger<SecurityController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        [HttpGet]
        public IActionResult register()
        {
            return Ok(GenerateJWTToken("Clint"));
        }

        // Only called once authentication is done
        private string GenerateJWTToken(string APIKey)
        {
            var a = _configuration["SECERT_KEY"];
            // Using secret key to generate security key.
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["SECERT_KEY"]));
            // Algorithm and securityKey to create the signing credentials
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            // Claims 
            var claims = new[]
            {
                new Claim("User","Clint"),
                new Claim(JwtRegisteredClaimNames.UniqueName, APIKey)
            };
            // Token Creation
            var token = new JwtSecurityToken(
                "Clint",
                "Clint",
                claims,
                expires: DateTime.UtcNow.AddDays(1),
                signingCredentials: credentials
                );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
