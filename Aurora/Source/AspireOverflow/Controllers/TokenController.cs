using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AspireOverflow.Models;
using AspireOverflow.DataAccessLayer;
using Microsoft.AspNetCore.Mvc;
using AspireOverflow.Security;
using Microsoft.AspNetCore.Identity;
using AspireOverflow.Services;

namespace AspireOverflow.Controllers
{
    [ApiController]
    [Route("api/token")]

    public class TokenController : ControllerBase
    {
        public IConfiguration _configuration;
        private readonly AspireOverflowContext _context;
        private ILogger<TokenController> _logger;
        public TokenController(IConfiguration config, AspireOverflowContext context,ILogger<TokenController> logger)
        {
            _configuration = config ?? throw new NullReferenceException();
            _context = context?? throw new NullReferenceException();
            _logger=logger?? throw new NullReferenceException();
        }

        [HttpPost]
        public async Task<IActionResult> AuthToken(String Email, string Password)
        {

            if (Email == null && Password == null) return BadRequest();
            try
            {
                var user = await GetUser(Email, Password);

                if (user != null)
                {
                    //create claims details based on the user information
                    var claims = new[] {
                        new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                        new Claim("Email",user.EmailAddress),
                        new Claim("UserId",user.UserId.ToString()),
                        new Claim("RoleId",user.UserRoleId.ToString())


                    };

                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
                    var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                    var token = new JwtSecurityToken(
                        _configuration["Jwt:Issuer"],
                        _configuration["Jwt:Audience"],
                            claims,
                        expires: DateTime.UtcNow.AddMinutes(10),
                        signingCredentials: signIn);

                    return Ok(new JwtSecurityTokenHandler().WriteToken(token));
                }
                else
                {   
                    return BadRequest("Invalid credentials");
                }
            }
            catch (Exception exception)
            {   _logger.LogError(HelperService.LoggerMessage(nameof(TokenController),nameof(AuthToken),exception,Email));
                return BadRequest("Invalid credentials");
            }

        }

        private async Task<User> GetUser(string Email, string Password)
        {
            try
            {
                var Hasher = PasswordHasherFactory.GetPasswordHasherFactory();
                var User = await _context.Users.FirstOrDefaultAsync(user => user.EmailAddress == Email);
                return Hasher.VerifyHashedPassword(User, User.Password, Password) == PasswordVerificationResult.Success ? User : throw new InvalidDataException("Password doesn't match");
            }
            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage(nameof(TokenController),nameof(GetUser),exception,Email));
              
                throw exception;
            }
        }
    }
}