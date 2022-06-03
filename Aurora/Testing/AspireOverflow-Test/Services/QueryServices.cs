
using System.ComponentModel.DataAnnotations;
using AspireOverflow.DataAccessLayer.Interfaces;
using AspireOverflow.Models;
using AspireOverflow.Services;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;
using Microsoft.EntityFrameworkCore;
using System;

namespace AspireOverflowTest
{

    public class QueryServiceTest{
          private  readonly Mock<IQueryRepository> _queryRepository=new Mock<IQueryRepository>();

        private  readonly Mock<ILogger<QueryService>> _logger =new Mock<ILogger<QueryService>>();



       
         private readonly IQueryService _queryService;
        public QueryServiceTest(){

            _queryService=new QueryService(_logger.Object,null,_queryRepository.Object);

        }
         [Theory]
        [InlineData(null)]
        public void CreateQuery_ShouldThrowValidationException_WhenQueryObjectIsNull(Query query)
        {
            Assert.Throws<ValidationException>(()=>_queryService.CreateQuery(query));
        }

      [Fact]
        public void CreateQuery_ShouldThrowValidationException_WhenQueryObjectIsInvalid()

        {   Query query=QueryMock.GetInValidQuery();
            Assert.Throws<ValidationException>(()=>_queryService.CreateQuery(query));
        }


        [Fact]
        public void CreateQuery_ShouldReturnTrue_WhenQueryObjectIsValid()

        {   Query query=QueryMock.GetValidQuery();
        _queryRepository.Setup(obj=> obj.AddQuery(query)).Returns(true);
        var Result=_queryService.CreateQuery(query);

            Assert.True(Result);
        }

[Fact]
        public void CreateQuery_ShouldReturnFalse_WhenExceptionThrownInQueryRepository()

        {   Query query=QueryMock.GetValidQuery();
        _queryRepository.Setup(obj=> obj.AddQuery(query)).Throws(new DbUpdateException());
             var Result=_queryService.CreateQuery(query);
            Assert.False(Result);
            _queryRepository.Setup(obj=> obj.AddQuery(query)).Throws(new Exception());
          
            Assert.False(_queryService.CreateQuery(query));
            _queryRepository.Setup(obj=> obj.AddQuery(query)).Throws(new OperationCanceledException());
           
            Assert.False(_queryService.CreateQuery(query));
        }


    }
}