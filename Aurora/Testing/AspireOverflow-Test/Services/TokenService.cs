using System;
using System.ComponentModel.DataAnnotations;
using AspireOverflow.DataAccessLayer.Interfaces;
using AspireOverflow.Models;
using AspireOverflow.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace AspireOverflowTest
{

    public class TokenServiceTest
    {


        private TokenService _tokenService;
        private readonly Mock<IUserService> _userService = new Mock<IUserService>();

        private readonly Mock<ILogger<TokenService>> _logger = new Mock<ILogger<TokenService>>();

        private IConfiguration _configuration = new ConfigurationBuilder()
                       .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                       .AddJsonFile("appsettings.json")
                       .Build();
        public TokenServiceTest()
        {
            _tokenService = new TokenService(_configuration, _userService.Object, _logger.Object);
        }
         
        //GenerateToken

        [Theory]
        [InlineData(null)]
        public void GenerateToken_ShouldThrowArgumentException_WhenNullObjectPassedAsNull(Login obj)
        {
            Assert.Throws<ArgumentException>(() => _tokenService.GenerateToken(obj));

        }
        [Fact]
        public void GenerateToken_ShouldThrowArgumentException_WhenObjectIsInValid()
        {
            Login obj = new Login(){Email="",Password=""};
            Assert.Throws<ValidationException>(() => _tokenService.GenerateToken(obj));
        }

        [Fact]
        public void GenerateToken_ShouldReturnToken_WhenValidCredentialsPassed()
        {

            Login _credentials = new Login() { Email = "mani.venkat@aspiresys.com", Password = "Man@**ia112" };
            _userService.Setup(obj => obj.GetUser(_credentials.Email, _credentials.Password)).Returns(QueryMock.GetListOfUsersForSeeding()[0]);
            Assert.NotEqual(null, _tokenService.GenerateToken(_credentials));
        }

        [Fact]
        public void GenerateToken_ShouldThrowException_WhenAnyExceptionOccurs()
        {
            Login _credentials = new Login() { Email = "mani.venkat@aspiresys.com", Password = "Man@**ia112" };
            _userService.Setup(obj => obj.GetUser(_credentials.Email, _credentials.Password)).Throws(new Exception());
            Assert.Throws<Exception>(() => _tokenService.GenerateToken(_credentials));
        }

    }
}