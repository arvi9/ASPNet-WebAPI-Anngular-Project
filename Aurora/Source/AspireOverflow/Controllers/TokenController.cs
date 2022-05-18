
using AspireOverflow.Models;
using Microsoft.AspNetCore.Mvc;
using AspireOverflow.Services;
using System.ComponentModel.DataAnnotations;

namespace AspireOverflow.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]

    public class TokenController : BaseController
    {

        private TokenService _tokenService;
        private ILogger<TokenController> _logger;
        public TokenController(TokenService tokenService, ILogger<TokenController> logger)
        {

            _tokenService = tokenService;
            _logger = logger;
        }

        [HttpPost]
        public IActionResult AuthToken(Login Crendentials)
        {


            try
            {
                if (!Validation.ValidateUserCredentials(Crendentials.Email, Crendentials.Password)) return BadRequest();


                var Result = _tokenService.GenerateToken(Crendentials);
                return Ok(Result);

            }
            catch (ValidationException exception)
            {
                _logger.LogError(HelperService.LoggerMessage("TokenController", "AuthToken(String Email, string Password)", exception, Crendentials.Email));
                return BadRequest(exception.Message);

            }
            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage("TokenController", "AuthToken(String Email, string Password)", exception, Crendentials.Email));
                return BadRequest(Message("Error Occured while Validating your  credentials"));
            }

        }


    }


}
