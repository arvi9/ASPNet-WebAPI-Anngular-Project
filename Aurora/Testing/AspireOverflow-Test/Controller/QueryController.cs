

using System.ComponentModel.DataAnnotations;
using AspireOverflow.Controllers;
using AspireOverflow.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;
using AspireOverflow.DataAccessLayer.Interfaces;
using System;

namespace AspireOverflowTest
{

    public class QueryControllerTest
    {
        private readonly QueryController _QueryController;
        private readonly Mock<ILogger<QueryController>> _logger = new Mock<ILogger<QueryController>>();
        private readonly Mock<IQueryService> _queryService = new Mock<IQueryService>();


        public QueryControllerTest()
        {
            _QueryController = new QueryController(_logger.Object, _queryService.Object);
        }

        [Theory]
        [InlineData(null)]
        public void CreateQuery_ShouldReturnStatusCode400_WhenQueryObjectIsNull(Query query)
        {

            var Result = _QueryController.CreateQuery(query).Result as ObjectResult;
            Assert.Equal(400, Result?.StatusCode);

        }

        [Fact]
        public void CreateQuery_ShouldReturnStatusCode200_WhenQueryObjectIsPassed()
        {
            var query = new Query();
            _queryService.Setup(obj => obj.CreateQuery(query)).Returns(true);

            var Result = _QueryController.CreateQuery(query).Result as ObjectResult;

            Assert.Equal(200, Result?.StatusCode);

        }
        [Fact]
            public void CreateQuery_ShouldReturnStatusCode400_WhenQueryServiceReturnsFalse()
        {
            var query = new Query();
            _queryService.Setup(obj => obj.CreateQuery(query)).Returns(false);

            var Result = _QueryController.CreateQuery(query).Result as ObjectResult;

            Assert.Equal(400, Result?.StatusCode);

        }

        [Fact]
        public void CreateQuery_ShouldReturnStatusCode400_WhenValidationExceptionIsThrown()
        {
            var query = new Query();
            _queryService.Setup(obj => obj.CreateQuery(query)).Throws(new ValidationException());

            var Result = _QueryController.CreateQuery(query).Result as ObjectResult;

            Assert.Equal(400, Result?.StatusCode);
        }


       [Fact]
        public void CreateQuery_ShouldReturnStatusCode400_WhenExceptionIsThrown()
        {
            var query = new Query();
            _queryService.Setup(obj => obj.CreateQuery(query)).Throws(new Exception());

            var Result = _QueryController.CreateQuery(query).Result as ObjectResult;

            Assert.Equal(500, Result?.StatusCode);
        }


    
    }


}
