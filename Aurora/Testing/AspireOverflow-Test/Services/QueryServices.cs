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
            QueryComment Comment = new QueryComment();
            Assert.Throws<ValidationException>(() => _queryService.CreateComment(Comment));
        }


        [Fact]
        public void CreateComment_ShouldReturnTrue_WhenQueryObjectIsValid()

        {
            QueryComment Comment = QueryMock.GetValidQueryComment();
            _queryRepository.Setup(obj => obj.AddComment(Comment)).Returns(true);
            var Result = _queryService.CreateComment(Comment);

            Assert.True(Result);
        }

        [Fact]
        public void CreateComment_ShouldReturnFalse_WhenExceptionThrownInQueryRepository()

        {
            QueryComment Comment = QueryMock.GetValidQueryComment();
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
            Assert.Throws<ArgumentException>(() => _queryService.RemoveQueryByQueryId(queryId));
        }

        [Theory]
        [InlineData(1)]
        public void RemoveQueryByQueryId_ShouldReturnTrue_WhenQueryIdIsValid(int queryId)
        {
            _queryRepository.Setup(obj => obj.UpdateQuery(queryId, false, true)).Returns(true);

            Assert.Equal(true, _queryService.RemoveQueryByQueryId(queryId));

        }

        [Fact]
        public void RemoveQueryByQueryId_ShouldReturnFalse_WhenExceptionIsThrown()
        {
            int queryId = 2;
            _queryRepository.Setup(obj => obj.UpdateQuery(queryId, false, true)).Throws(new DbUpdateException());

            var Result = _queryService.RemoveQueryByQueryId(queryId);
            Assert.False(Result);
            _queryRepository.Setup(obj => obj.UpdateQuery(queryId, false, true)).Throws(new Exception());

            Assert.False(_queryService.RemoveQueryByQueryId(queryId));
            _queryRepository.Setup(obj => obj.UpdateQuery(queryId, false, true)).Throws(new OperationCanceledException());

            Assert.False(_queryService.RemoveQueryByQueryId(queryId));
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void GetQueries_ShouldReturnListOfQueries_WhenMethodIsCalled(bool IsSolved)
        {
            var Queries = QueryMock.GetListOfQueries();
            _queryRepository.Setup(obj => obj.GetQueries()).Returns(Queries);

            var ExpectedQueries = Queries.Where(item => item.IsSolved == IsSolved).Count();
            Assert.Equal(ExpectedQueries, _queryService.GetQueries(IsSolved).Count());
        }

        [Fact]

        public void GetQueries_ShoultThrowException_WhenAnyExceptionIsRaised()
        {
            _queryRepository.Setup(obj => obj.GetQueries()).Throws(new Exception());

            Assert.Throws<Exception>(() => _queryService.GetQueries(false));
        }
        
        //AddSpam

        [Theory]
        [InlineData(null)]
        public void AddSpam_ShouldThrowValidationException_whenSpamIsNull(Spam spam)
        {            
            Assert.Throws<ValidationException>(() => _queryService.AddSpam (spam));
        }

         [Fact]
        public void AddSpam_ShouldThrowValidationException_whenInValidSpam()
        {
            Spam spam = new Spam();
            
            Assert.Throws<ValidationException>(() => _queryService.AddSpam (spam));
        }

        [Fact]
        public void addSpam_ShouldReturnTrue_WhenSpamObjectIsPassedSucessfully()
        {
            Spam spam=QueryMock.AddValidSpam();
            _queryRepository.Setup(obj => obj.AddSpam(spam)).Returns(true);
            var Result = _queryService.AddSpam(spam);
            Assert.True(Result); 

        }

        [Fact]
        public void addSpam_ShouldReturnfalse_WhenSpamObjectIsNotPassed()
        {
            Spam spam=QueryMock.AddValidSpam();
            _queryRepository.Setup(obj => obj.AddSpam(spam)).Returns(false);
            var Result = _queryService.AddSpam(spam);
            Assert.False(Result); 

        }

        [Fact]
        public void addSpam_ShouldReturnFalse_WhenExceptionIsThrownInDAL()
        {
            Spam spam=QueryMock.AddValidSpam();
            _queryRepository.Setup(obj => obj.AddSpam(spam)).Throws(new Exception());
            var Result = _queryService.AddSpam(spam);
            Assert.False(Result); 
        }

        //MarkQueryAsSolved

        [Theory]
        [InlineData(-1)]
        [InlineData(0)]
        public void MarkQueryAsSolved_ShouldThrowValidationException_WhenInvalidParameterIsPAssed(int QueryId)
        {
             Assert.Throws<ArgumentException>(() => _queryService.MarkQueryAsSolved(QueryId));
        }

        [Theory]
        [InlineData(1)]
        public void MarkQueryAsSolved_ShouldReturnTrue_WhenQueryIsUpdatedAsSolved(int QueryId)
        {
            bool IsSolved=true;
            bool IsDelete=false;
            _queryRepository.Setup(obj => obj.UpdateQuery(QueryId,IsSolved,IsDelete)).Returns(true);
            var Result= _queryService.MarkQueryAsSolved(QueryId);
            Assert.True(Result);
        }

        [Theory]
        [InlineData(1)]
        public void MarkQueryAsSolved_ShouldReturnFalse_WhenExceptionIsOccured(int QueryId)
        {
            bool IsSolved=true;
            bool IsDelete=false;
            _queryRepository.Setup(obj => obj.UpdateQuery(QueryId,IsSolved,IsDelete)).Throws(new Exception());
            var Result= _queryService.MarkQueryAsSolved(QueryId);
            Assert.False(Result);
        }

        [Theory]
        [InlineData(1)]
        public void MarkQueryAsSolved_ShouldReturnFalse_WhenDALReturnsFalse(int QueryId)
        {
            bool IsSolved=true;
            bool IsDelete=false;
            _queryRepository.Setup(obj => obj.UpdateQuery(QueryId,IsSolved,IsDelete)).Returns(false);
            var Result= _queryService.MarkQueryAsSolved(QueryId);
            Assert.False(Result);
        }

        //UpdateSpamStatus

        [Theory]
        [InlineData(0,3)]
        [InlineData(-1,3)]
        public void UpdateSpamStatus_ShouldThrowArgumentException_WhenParameterValuesNotValid(int QueryId,int VerifyStatusID)
        {
             Assert.Throws<ArgumentException>(() => _queryService.ChangeSpamStatus(QueryId,VerifyStatusID));
        }

        [Theory]
        [InlineData(1,4)]
       [InlineData(1,0)]
        public void UpdateSpamStatus_ShouldThrowArgumentException_WhenVerifyStatusParameterValuesNotValid(int QueryId,int VerifyStatusID)
        {
             Assert.Throws<ArgumentException>(() => _queryService.ChangeSpamStatus(QueryId,VerifyStatusID));
        }


        [Theory]
        [InlineData(1,2)]

        public void UpdateSpamStatus_ShouldReturnTrue_WhenStatusChangedSucessfully(int QueryId,int VerifyStatusID)
        {
             _queryRepository.Setup(obj => obj.UpdateSpam(QueryId,VerifyStatusID)).Returns(true);
             var Result = _queryService.ChangeSpamStatus(QueryId,VerifyStatusID);
             Assert.True(Result);
        }

        [Theory]
        [InlineData(1,2)]
        public void UpdateSpamStatus_ShouldThrowException_WhenExceptionOccuredInDAL(int QueryId,int VerifyStatusID)
        {
             _queryRepository.Setup(obj => obj.UpdateSpam(QueryId,VerifyStatusID)).Throws(new Exception());
             Assert.Throws<Exception>(() => _queryService.ChangeSpamStatus(QueryId,VerifyStatusID));
        }

        //GetSpams

        [Theory]
        [InlineData(0)]
        public void GetSpams_ShouldThrowArgumentException_WhenParameterValueIsInValid(int VerifyStatusID)
        {
             Assert.Throws<ArgumentException>(() => _queryService.GetSpams(VerifyStatusID));
        }
        
        [Theory]
        [InlineData(3)]
        public void GetSpams_ReturnsList_WhenMethodIsCalled(int VerifyStatusID)
        {
              var Spams = QueryMock.GetListOfSpams();
            _queryRepository.Setup(obj => obj.GetSpams()).Returns(Spams);

            var FinalSpamList = Spams.Where(item => item.VerifyStatusID==VerifyStatusID) .GroupBy(item => item.QueryId).Count();
            Assert.Equal(FinalSpamList, _queryService.GetSpams(VerifyStatusID).Count());
        }

        [Theory]
        [InlineData(3)]
        public void GetSpams_ShouldThrowException_WhenExceptionThrown(int VerifyStatusID)
        {
            _queryRepository.Setup(obj => obj.GetSpams()).Throws(new Exception());
            Assert.Throws<Exception>(() => _queryService.GetSpams(VerifyStatusID));
        }

        //Get Query

         [Theory]
        [InlineData(0)]
        public void GetQuery_ShouldReturnArgumentException_WhenQueryIdIsInValid(int queryId)
        {
            Assert.Throws<ArgumentException>(() => _queryService.GetQuery(queryId));
        }

   
        // [Theory]
        // [InlineData(1)]
        // public void GetQuerybyId_ShouldReturnQuery_WhenMethodIsCalled(int QueryId)
        // {
        //     var query  =QueryMock.GetValidQuery();
            
        //     _queryRepository.Setup(obj => obj.GetQueryByID(QueryId)).Returns(query);  
        // //    _queryRepository.Setup(obj =>obj.GetComments(QueryId)).Returns(List)                                 
        //     Assert.NotEqual(null,_queryService.GetQuery(QueryId));
        // }


         [Fact]
        public void GetQuery_ShoultThrowException_WhenAnyExceptionIsRaised()
        {
            var QueryId =1;
            _queryRepository.Setup(obj => obj.GetQueryByID(QueryId)).Throws(new Exception());
            Assert.Throws<Exception>(() => _queryService.GetQuery(1));
        }

        //Get All

        [Fact]
        public void GetAll_ShouldReturnTrue_WhenQueryObjectIsValid()
        {
            var Queries= QueryMock.GetListOfQueries();
            _queryRepository.Setup(obj => obj.GetQueries()).Returns(Queries);
            var ExpectedArticle = _queryService.GetListOfQueries();
            Assert.Equal(ExpectedArticle, _queryService.GetListOfQueries());
        }

        [Fact]

        public void GetAll_ShoultThrowException_WhenAnyExceptionIsRaised()
        {
            _queryRepository.Setup(obj => obj.GetQueries()).Throws(new Exception());
            Assert.Throws<Exception>(() => _queryService. GetListOfQueries());
        }

         // Get Latest Query

         [Fact]
        public void GetLatestQueries_ShouldReturnListOfQueries_WhenMethodIsCalled()
        {
            var Queries = QueryMock.GetListOfQueries();
            _queryRepository.Setup(obj => obj.GetQueries()).Returns(Queries);

            var ExpectedQueries = Queries.OrderByDescending(item => item.CreatedOn).Count();
            Assert.Equal(ExpectedQueries, _queryService.GetLatestQueries().Count());
        }

        [Fact]
        public void GetLatestQueries_ShouldThrowException_WhenAnyExceptionIsRaised()
        {
            _queryRepository.Setup(obj => obj.GetQueries()).Throws(new Exception());
            Assert.Throws<Exception>(() => _queryService.GetLatestQueries());
        }

       // Get Trending Queries

        //  [Fact]    
        //  public void GetTrendingQueries_ShouldReturnListOfQueries_WhenMethodIsCalled(){
        //    var QueryComments = QueryMock.GetListOfQueryComments();
        //     var Queries = QueryMock.GetListOfQueries().ToList();        
        //    _queryRepository.Setup(obj => obj.GetComments()).Returns(QueryComments);
        //    _queryRepository.Setup(obj=>obj.GetQueries()).Returns(Queries);
        //    Assert.Equal(3,_queryService.GetTrendingQueries().Count());
        //  }

        [Fact]
        public void GetTrendingQueries_ShouldThrowException_WhenAnyExceptionIsRaised()
        {
            _queryRepository.Setup(obj => obj.GetComments()).Throws(new Exception());
             Assert.Throws<Exception>(() => _queryService.GetTrendingQueries());
        }

        // Get Queries By User Id

        [Theory]
        [InlineData(0)]
        public void GetQueriesByUserId_ShouldReturnArgumentException_WhenQueryIdIsInValid(int UserId)
        {
            Assert.Throws<ArgumentException>(() => _queryService.GetQueriesByUserId(UserId));
        }

        [Theory]
        [InlineData(1)]
        public void GetQueriesByUserId_ShouldReturnListOfQueries_WhenMethodIsCalled(int UserId)
        {
            var Queries = QueryMock.GetListOfQueries();
            _queryRepository.Setup(obj => obj.GetQueries()).Returns(Queries);
            var ExpectedQueries = Queries.Where(item => item.CreatedBy == UserId).Count();
            Assert.Equal(ExpectedQueries, _queryService.GetQueriesByUserId(UserId).Count());
        }

        [Fact]
        public void GetQueriesByUserId_ShoultThrowException_WhenAnyExceptionIsRaised()
        {
            var UserId=1;
            _queryRepository.Setup(obj => obj.GetQueries()).Throws(new Exception());
            Assert.Throws<Exception>(() => _queryService.GetQueriesByUserId(UserId));
        }
    }
}