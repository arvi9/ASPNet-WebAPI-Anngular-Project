

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
            Query query = QueryMock.GetValidQuery();
            _queryService.Setup(obj => obj.CreateQuery(query)).Returns(true);

            var Result = _QueryController.CreateQuery(query).Result as ObjectResult;
            // Console.WriteLine(Result);
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
        public void CreateQuery_ShouldReturnStatusCode500_WhenExceptionIsThrown()
        {
            var query = new Query();
            _queryService.Setup(obj => obj.CreateQuery(query)).Throws(new Exception());

            var Result = _QueryController.CreateQuery(query).Result as ObjectResult;

            Assert.Equal(500, Result?.StatusCode);
        }


        //Remove Query By QueryID


        [Theory]
        [InlineData(0)]
        public void RemoveQueryByQueryId_ShouldReturnStatusCode400_WhenQueryId_Zero(int queryId)
        {
            var Result = _QueryController.RemoveQueryByQueryId(queryId).Result as ObjectResult;
            Assert.Equal(400, Result?.StatusCode);

        }



        [Theory]
        [InlineData(1)]
        public void RemoveQueryByQueryId_ShouldReturnStatusCode200_WhenQueryIdIsValid(int queryId)
        {
            _queryService.Setup(obj => obj.RemoveQueryByQueryId(queryId)).Returns(true);

            var Result = _QueryController.RemoveQueryByQueryId(queryId).Result as ObjectResult;

            Assert.Equal(200, Result?.StatusCode);

        }

        [Fact]
        public void RemoveQueryByQueryId_ShouldReturnStatusCode404_WhenItemNotFoundExceptionIsThrown()
        {
            int queryId = 2;
            _queryService.Setup(obj => obj.RemoveQueryByQueryId(2)).Throws(new ItemNotFoundException());

            var Result = _QueryController.RemoveQueryByQueryId(2).Result as ObjectResult;

            Assert.Equal(404, Result?.StatusCode);
        }

        [Fact]
        public void RemoveQueryByQueryId_ShouldReturnStatusCode500_WhenExceptionIsThrown()
        {
            var queryId = 4;
            _queryService.Setup(obj => obj.RemoveQueryByQueryId(queryId)).Throws(new Exception());

            var Result = _QueryController.RemoveQueryByQueryId(queryId).Result as ObjectResult;

            Assert.Equal(500, Result?.StatusCode);
        }


        //CreateComment 


        [Theory]
        [InlineData(null)]
        public void CreateComment_ShouldReturnStatusCode400_WhenQueryObjectIsNull(QueryComment comment)
        {
            var Result = _QueryController.CreateComment(comment).Result as ObjectResult;
            Assert.Equal(400, Result?.StatusCode);

        }

        [Fact]
        public void CreateComment_ShouldReturnStatusCode200_WhenQueryCommentIsValid()
        {
            QueryComment comment = new QueryComment();
            _queryService.Setup(obj => obj.CreateComment(comment)).Returns(true);

            var Result = _QueryController.CreateComment(comment).Result as ObjectResult;
            // Console.WriteLine(Result);
            Assert.Equal(200, Result?.StatusCode);

        }
        [Fact]
        public void CreateComment_ShouldReturnStatusCode400_WhenQueryServiceReturnsFalse()
        {
            QueryComment comment = new QueryComment();
            _queryService.Setup(obj => obj.CreateComment(comment)).Returns(false);

            var Result = _QueryController.CreateComment(comment).Result as ObjectResult;

            Assert.Equal(400, Result?.StatusCode);

        }

        [Fact]
        public void CreateComment_ShouldReturnStatusCode400_WhenValidationExceptionIsThrown()
        {
            QueryComment comment = new QueryComment();
            _queryService.Setup(obj => obj.CreateComment(comment)).Throws(new ValidationException());

            var Result = _QueryController.CreateComment(comment).Result as ObjectResult;

            Assert.Equal(400, Result?.StatusCode);
        }


        [Fact]
        public void CreateComment_ShouldReturnStatusCode500_WhenExceptionIsThrown()
        {
            QueryComment comment = new QueryComment();
            _queryService.Setup(obj => obj.CreateComment(comment)).Throws(new Exception());

            var Result = _QueryController.CreateComment(comment).Result as ObjectResult;

            Assert.Equal(500, Result?.StatusCode);
        }


        //GetQueriesByIsSolved

  [Theory]
  [InlineData(true)]
  [InlineData(false)]
        public void GetQueriesByIsSolved_ShouldReturnListOfQueries_WhenMethodIsCalled(bool IsSolved)
        {
            var Queries=QueryMock.GetListOfQueries().Where(item =>item.IsSolved==IsSolved);
            

            _queryService.Setup(obj => obj.GetQueries(IsSolved)).Returns(Queries);

            var Result = _QueryController.GetQueriesByIsSolved(IsSolved).Result as ObjectResult;

            Assert.Equal(Queries, Result?.Value);
        }

          [Fact]
        public void GetQueriesByIsSolved_ShouldReturnStatusCode500_WhenExceptionIsThrown()
        {
         
            _queryService.Setup(obj => obj.GetQueries(true)).Throws(new Exception());

            var Result = _QueryController.GetQueriesByIsSolved(true).Result as ObjectResult;

            Assert.Equal(500, Result?.StatusCode);
        }

    }
}
