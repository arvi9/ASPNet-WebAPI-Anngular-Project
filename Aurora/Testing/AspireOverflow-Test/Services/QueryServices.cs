
using System.Linq;
using System.ComponentModel.DataAnnotations;
using AspireOverflow.DataAccessLayer.Interfaces;
using AspireOverflow.Models;
using AspireOverflow.Services;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;
using Microsoft.EntityFrameworkCore;
using System;


namespace AspireOverflowTest
{

    public class QueryServiceTest
    {
        private readonly Mock<IQueryRepository> _queryRepository = new Mock<IQueryRepository>();

        private readonly Mock<ILogger<QueryService>> _logger = new Mock<ILogger<QueryService>>();




        private readonly QueryService _queryService;
        public QueryServiceTest()
        {

            _queryService = new QueryService(_logger.Object, null, _queryRepository.Object);

        }
        [Theory]
        [InlineData(null)]
        public void CreateQuery_ShouldThrowValidationException_WhenQueryObjectIsNull(Query query)
        {
            Assert.Throws<ValidationException>(() => _queryService.CreateQuery(query));
        }

        [Fact]
        public void CreateQuery_ShouldThrowValidationException_WhenQueryObjectIsInvalid()

        {
            Query query = QueryMock.GetInValidQuery();
            Assert.Throws<ValidationException>(() => _queryService.CreateQuery(query));
        }


        [Fact]
        public void CreateQuery_ShouldReturnTrue_WhenQueryObjectIsValid()

        {
            Query query = QueryMock.GetValidQuery();
            _queryRepository.Setup(obj => obj.AddQuery(query)).Returns(true);
            var Result = _queryService.CreateQuery(query);

            Assert.True(Result);
        }

        [Fact]
        public void CreateQuery_ShouldReturnFalse_WhenExceptionThrownInQueryRepository()

        {
            Query query = QueryMock.GetValidQuery();
            _queryRepository.Setup(obj => obj.AddQuery(query)).Throws(new DbUpdateException());
            var Result = _queryService.CreateQuery(query);
            Assert.False(Result);
            _queryRepository.Setup(obj => obj.AddQuery(query)).Throws(new Exception());

            Assert.False(_queryService.CreateQuery(query));
            _queryRepository.Setup(obj => obj.AddQuery(query)).Throws(new OperationCanceledException());

            Assert.False(_queryService.CreateQuery(query));
        }

         [Theory]
        [InlineData(null)]
        public void CreateComment_ShouldThrowValidationException_WhenQueryObjectIsNull(QueryComment Comment)
        {
            Assert.Throws<ValidationException>(() => _queryService.CreateComment(Comment));
        }

        [Fact]
        public void CreateComment_ShouldThrowValidationException_WhenQueryObjectIsInvalid()

        {
           QueryComment Comment=new QueryComment();
            Assert.Throws<ValidationException>(() => _queryService.CreateComment(Comment));
        }


        [Fact]
        public void CreateComment_ShouldReturnTrue_WhenQueryObjectIsValid()

        {
           QueryComment Comment=QueryMock.GetValidQueryComment();
            _queryRepository.Setup(obj => obj.AddComment(Comment)).Returns(true);
            var Result = _queryService.CreateComment(Comment);

            Assert.True(Result);
        }

        [Fact]
        public void CreateComment_ShouldReturnFalse_WhenExceptionThrownInQueryRepository()

        {
           QueryComment Comment=QueryMock.GetValidQueryComment();
            _queryRepository.Setup(obj => obj.AddComment(Comment)).Throws(new DbUpdateException());
            var Result = _queryService.CreateComment(Comment);
            Assert.False(Result);
            _queryRepository.Setup(obj => obj.AddComment(Comment)).Throws(new Exception());

            Assert.False(_queryService.CreateComment(Comment));
            _queryRepository.Setup(obj => obj.AddComment(Comment)).Throws(new OperationCanceledException());

            Assert.False(_queryService.CreateComment(Comment));
        }


  [Theory]
        [InlineData(0)]
        public void RemoveQueryByQueryId_ShouldReturnArgumentException_WhenQueryIdIsInValid(int queryId)
        {
            Assert.Throws<ArgumentException>(() =>_queryService.RemoveQueryByQueryId(queryId));
        }

        [Theory]
        [InlineData(1)]
        public void RemoveQueryByQueryId_ShouldReturnTrue_WhenQueryIdIsValid(int queryId)
        {
            _queryRepository.Setup(obj => obj.UpdateQuery(queryId,false,true)).Returns(true);

            Assert.Equal(true,_queryService.RemoveQueryByQueryId(queryId));

        }

        [Fact]
        public void RemoveQueryByQueryId_ShouldReturnFalse_WhenExceptionIsThrown()
        {
            int queryId = 2;
            _queryRepository.Setup(obj => obj.UpdateQuery(queryId,false,true)).Throws(new DbUpdateException());
 
            var Result = _queryService.RemoveQueryByQueryId(queryId);
            Assert.False(Result);
            _queryRepository.Setup(obj => obj.UpdateQuery(queryId,false,true)).Throws(new Exception());

            Assert.False(_queryService.RemoveQueryByQueryId(queryId));
            _queryRepository.Setup(obj =>obj.UpdateQuery(queryId,false,true)).Throws(new OperationCanceledException());

            Assert.False(_queryService.RemoveQueryByQueryId(queryId));
        }

//        [Theory]
//   [InlineData(true)]
//   [InlineData(false)]
//         public void GetQueries_ShouldReturnListOfQueries_WhenMethodIsCalled(bool IsSolved)
//         {
//             var Queries=QueryMock.GetListOfQueries();
//             _queryRepository.Setup(obj => obj.GetQueries()).Returns(Queries);

//             Assert.Equal(Queries.FindAll(item =>item.IsSolved==IsSolved).Select(item =>new {
//                 Title=item.Title,
//                 content=item.Content,
//                 code=item.code,
//                 IsSolved=item.IsSolved

//             }), _queryService.GetQueries(IsSolved));
//         }

    }
} 