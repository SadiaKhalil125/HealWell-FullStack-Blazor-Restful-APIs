using Application.Services;
using Azure.Core;
using Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace HealWellBackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly PatientService _service;

        public AuthController(PatientService service)
        {
            _service = service;
        }
        [HttpPost("login")]
        public IActionResult Login(LoginModel model)
        {
            if (string.IsNullOrEmpty(model.Email) || string.IsNullOrEmpty(model.Password))
                return BadRequest("Email and password are required.");

            bool isValid = _service.CheckLogin(model.Email, model.Password);
            if (isValid)
            {
                var claims = new[]
                    {
            new Claim(JwtRegisteredClaimNames.Sub, model.Email), // 'Sub' represents the subject of the token, typically the user's unique identifier like username.
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())// 'Jti' (JWT ID) is a unique identifier for the token to prevent replay attacks.
           
                // Additional useful claims in real-world scenarios could include:
                // - 'Email' to store the user's email address.
                // - 'Role' to represent user roles for role-based authorization.
                // - Custom claims for application-specific information, such as 'Department' or 'AccessLevel'.
            };

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("this-is-my-secret-key-that-is-too-long-key-it-is-needed")); //if global settings program.cs and this key diff (will  be problem)
                                                                                                                                       //should write in appsettings.json so no hardcoded
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                var token = new JwtSecurityToken(
                    issuer :"https://localhost:7047/",
                    audience : "https://localhost:7239/",
                    claims: claims,
                    expires: DateTime.Now.AddMinutes(30),
                    signingCredentials: creds);

                return Ok(new { token = new JwtSecurityTokenHandler().WriteToken(token) });
            }
            
            return Unauthorized();
        }
    }
}
