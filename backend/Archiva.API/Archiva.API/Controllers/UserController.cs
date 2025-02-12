using Archiva.API.Extensions;
using Archiva.Core.Conracts;
using Archiva.Core.Constants;
using Archiva.Core.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Archiva.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService userService;
        private readonly IConfiguration configuration;
        private readonly JwtSettings jwtSettings;

        public UserController(
            IUserService _userService, 
            IConfiguration _configuration,
            IOptions<JwtSettings> _jwtSettings)
        {
            userService = _userService;
            configuration = _configuration;
            jwtSettings = _jwtSettings.Value;
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync([FromBody] LoginModel model)
        {
            try
            {
                await userService.LoginAsync(model);

                var claims = new[]
                {
                    new Claim(ClaimTypes.Name, model.Email),
                    new Claim(ClaimTypes.Role, "User")
                };

                var tokenHandler = new JwtSecurityTokenHandler();
                var keyBytes = Encoding.UTF8.GetBytes(configuration["Jwt:Key"]!);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(claims),
                    Expires = DateTime.Now.AddHours(12),
                    SigningCredentials = new SigningCredentials(
                        new SymmetricSecurityKey(keyBytes),
                        SecurityAlgorithms.HmacSha256Signature)
                };

                var token = tokenHandler.CreateToken(tokenDescriptor);
                var tokenString = tokenHandler.WriteToken(token);

                return Ok(new { token = tokenString });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterAsync([FromBody] RegisterModel model)
        {
            try
            {
                await userService.RegisterAsync(model);

                return Ok(new { Message = ReturnMessages.SuccessfullyRegistered });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetUserByIdAsync(string userId)
        {
            try
            {
                var user = await userService.GetUserByIdAsync(userId);

                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetUser()
        {
            var token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

            if (string.IsNullOrEmpty(token))
            {
                return Unauthorized(new { Message = "No token provided" });
            }

            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var keyBytes = Encoding.UTF8.GetBytes(jwtSettings.Key);

                var principal = tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    IssuerSigningKey = new SymmetricSecurityKey(keyBytes),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                }, out var validatedToken);

                var email = principal.FindFirst(ClaimTypes.Name)?.Value;

                if (email is null)
                    return BadRequest(new { Message = "Email is not valid!" });

                var userInfo = await userService.GetUserByEmailAsync(email);

                if (userInfo is null)
                    throw new Exception();

                return Ok(userInfo);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
