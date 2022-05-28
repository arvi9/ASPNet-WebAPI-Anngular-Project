using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AspireOverflow.Models;
using Microsoft.IdentityModel.Tokens;

namespace AspireOverflow.Services
{

    public class TokenService
    {
        private IConfiguration _configuration;
        private UserService _userService;
        private ILogger<TokenService> _logger;
        public TokenService(IConfiguration configuration, UserService userService, ILogger<TokenService> logger)
        {
            _configuration = configuration;
            _userService = userService;
            _logger = logger;

        }

        public object GenerateToken(Login Credentials)
        {
            var user = _userService.GetUser(Credentials.Email, Credentials.Password);
            if (user == null) throw new ArgumentNullException();
            try
            {
                //create claims details based on the user information
                var claims = new[] {
                        new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                        new Claim(ClaimTypes.Email,user.EmailAddress),
                        new Claim("UserId",user.UserId.ToString()),
                        new Claim(ClaimTypes.Role,user.UserRoleId.ToString()),
                        new Claim("IsReviewer",user.IsReviewer.ToString())
                    };

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
                var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                var token = new JwtSecurityToken(
                    _configuration["Jwt:Issuer"],
                    _configuration["Jwt:Audience"],
                        claims,
                    expires: DateTime.UtcNow.AddMinutes(360),
                    signingCredentials: signIn);

                var Result = new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    ExpiryInMinutes = 360,
                    IsAdmin = user.UserRoleId == 1 ? true : false,
                    IsReviewer = user.IsReviewer
                };

                return Result;

            }
            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage("TokenService", " GenerateToken(String Email, string Password)", exception, Credentials));
                throw exception;

            }
        }
    }
}