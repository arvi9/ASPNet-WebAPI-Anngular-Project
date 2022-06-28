using System.Linq;
using System.ComponentModel.DataAnnotations;
using AspireOverflow.Controllers;
using AspireOverflow.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;
using AspireOverflow.DataAccessLayer.Interfaces;
using System;
using AspireOverflow.Services;
using AspireOverflow.CustomExceptions;

namespace AspireOverflowTest
{

    public class TokenControllerTest
    {
        private readonly TokenController _TokenController;
        private readonly Mock<ILogger<TokenController>> _logger = new Mock<ILogger<TokenController>>();

        private readonly Mock<ITokenService> _tokenService = new Mock<ITokenService>();


        public TokenControllerTest()
        {
            _TokenController = new TokenController(_tokenService.Object, _logger.Object);
        }

        [Fact]

        public void AuthToken_ShouldReturnStatusCode400_WhenObjectIsInValid()
        {
            Login Credentials = new Login() { Email = "Mani", Password = "Mkajkj@133" };
            var Result = _TokenController.AuthToken(Credentials) as ObjectResult;
            Assert.Equal(400, Result?.StatusCode);
        }
        [Fact]
        public void AuthToken_ShouldReturnStatusCode200_WhenObjectIsPassed()
        {
            Login Credentials = new Login() { Email = "Mani@aspiresys.com", Password = "Mkajkj@133" };
            _tokenService.Setup(obj => obj.GenerateToken(Credentials)).Returns("sampletoken");
            var Result = _TokenController.AuthToken(Credentials) as ObjectResult;
            Assert.Equal("sampletoken", Result?.Value);
        }
  [Fact]
        public void AuthToken_ShouldReturnStatusCode400_WhenAnyExceptionOccurs()
        {
            Login Credentials = new Login() { Email = "Mani@aspiresys.com", Password = "Mkajkj@133" };
            _tokenService.Setup(obj => obj.GenerateToken(Credentials)).Throws(new Exception());
            var Result = _TokenController.AuthToken(Credentials) as ObjectResult;
            Assert.Equal(400, Result?.StatusCode);
        }
        


    }
}