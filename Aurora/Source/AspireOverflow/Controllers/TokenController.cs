using AspireOverflow.Models;
using Microsoft.AspNetCore.Mvc;
using AspireOverflow.Services;
using System.ComponentModel.DataAnnotations;
using AspireOverflow.DataAccessLayer.Interfaces;
namespace AspireOverflow.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class TokenController : BaseController
    {
        private readonly ITokenService _tokenService;
        private readonly ILogger<TokenController> _logger;
        public TokenController(ITokenService tokenService, ILogger<TokenController> logger)
        {
            _tokenService = tokenService;
            _logger = logger;
        }

        
        /// <summary>
        /// Login to the system
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     url : https://localhost:7197/Token/AuthToken
        ///     body   
        ///     {
        ///        email*: string,
        ///        password*: string  
        ///     }
        /// 
        /// </remarks>
        /// <response code="200">Returns a jwt token. </response>
        /// <response code="401">Invalid credentials. </response>
        /// <response code="500">If there is problem in server. </response>
        /// <param name="Crendentials"></
        [HttpPost]
        public IActionResult AuthToken(Login Crendentials)
        {  if ( Crendentials == null || !Validation.ValidateUserCredentials(Crendentials.Email!, Crendentials.Password!)) return BadRequest(Message("Login Credentials cannot be null"));
          try
            {
                var Result = _tokenService.GenerateToken(Crendentials);
                return Ok(Result);
            }
            catch (ValidationException exception)
            {
                _logger.LogError(HelperService.LoggerMessage("TokenController", "AuthToken(String Email, string Password)", exception, Crendentials.Email!));
                return BadRequest(Message(exception.Message));
            }
            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage("TokenController", "AuthToken(String Email, string Password)", exception, Crendentials.Email!));
                return BadRequest(Message("Error Occured while Validating your credentials"));
            }
        }
    }
}
