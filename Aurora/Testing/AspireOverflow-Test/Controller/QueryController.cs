
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
            var Queries = QueryMock.GetListOfQueries().Where(item => item.IsSolved == IsSolved);


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
        //AddSpam
        [Theory]
        [InlineData(null)]
        public void AddSpam_ShouldReturnStatusCode400_WhenSpamObjectIsNull(Spam spam)
        {

            var Result = _QueryController.AddSpam(spam).Result as ObjectResult;
            Assert.Equal(400, Result?.StatusCode);

        }

        [Fact]
        public void AddSpam_ShouldReturnStatusCode200_WhenSpamObjectIsPassed()
        {
            Spam spam = QueryMock.AddValidSpam();
            _queryService.Setup(obj => obj.AddSpam(spam)).Returns(true);

            var Result = _QueryController.AddSpam(spam).Result as ObjectResult;
            Assert.Equal(200, Result?.StatusCode);

        }
        [Fact]
        public void AddSpam_ShouldReturnStatusCode400_WhenQueryServiceReturnsFalse()
        {
            var spam = new Spam();
            _queryService.Setup(obj => obj.AddSpam(spam)).Returns(false);

            var Result = _QueryController.AddSpam(spam).Result as ObjectResult;

            Assert.Equal(400, Result?.StatusCode);

        }

        [Fact]
        public void AddSpam_ShouldReturnStatusCode500_WhenValidationExceptionIsThrown()
        {
            var spam = new Spam();
            _queryService.Setup(obj => obj.AddSpam(spam)).Throws(new ValidationException());

            var Result = _QueryController.AddSpam(spam).Result as ObjectResult;

            Assert.Equal(500, Result?.StatusCode);
        }

        [Fact]
        public void AddSpam_ShouldReturnStatusCode500_WhenExceptionIsThrown()
        {
            var spam = new Spam();
            _queryService.Setup(obj => obj.AddSpam(spam)).Throws(new Exception());

            var Result = _QueryController.AddSpam(spam).Result as ObjectResult;

            Assert.Equal(400, Result?.StatusCode);
        }

        //GetListOfSpams

        [Fact]
        public void GetListOfSpams_ShouldReturnListOfQueries_WhenMethodIsCalled()
        {
            var Spams = QueryMock.GetListOfSpams();


            _queryService.Setup(obj => obj.GetSpams(3)).Returns(Spams);

            var Result = _QueryController.GetListOfSpams().Result as ObjectResult;

            Assert.Equal(Spams, Result?.Value);
        }
        [Fact]
        public void GetListOfSpams_ShouldThrowStatusCode500_WhenExceptionIsThrown()
        {
            _queryService.Setup(obj => obj.GetSpams(3)).Throws(new Exception());

            var Result = _QueryController.GetListOfSpams().Result as ObjectResult;

            Assert.Equal(500, Result?.StatusCode);
        }

        //UpdateSpamStatus

        [Theory]
        [InlineData(0,3)]
        [InlineData(-1,3)]
        public void UpdateSpamStatus_ShouldReturnStatus400_WhenvalidationErrorOccured(int QueryId,int VerifyStatusID)
        {
            
               var Result=_QueryController.UpdateSpamStatus(QueryId,VerifyStatusID)as ObjectResult;
               Assert.Equal(400,Result?.StatusCode);
        }

        [Theory]
        [InlineData(1,4)]
        [InlineData(1,0)]
        public void UpdateSpamStatus_ShouldReturnStatus400_WhenVerifyStatusvalidationErrorOccured(int QueryId,int VerifyStatusID)
        {
            
               var Result=_QueryController.UpdateSpamStatus(QueryId,VerifyStatusID)as ObjectResult;
               Assert.Equal(400,Result?.StatusCode);
        }

        

        [Theory]
        [InlineData(1,3)]
        public void UpdateSpamStatus_ShouldReturnStatus200_WhenSpamStatusUpadtedSuccessfully(int QueryId,int VerifyStatusID)
        {
            _queryService.Setup(obj => obj.ChangeSpamStatus(QueryId,VerifyStatusID)).Returns(true);
            var Result = _QueryController.UpdateSpamStatus(QueryId,VerifyStatusID)as ObjectResult;   
            Assert.Equal(200,Result?.StatusCode);   
        }

        [Theory]
        [InlineData(1,2)]
        public void UpdateSpamStatus_ShouldReturnStatus400_WhenSpamStatusNotUpdated(int QueryId,int VerifyStatusID)
        {
            _queryService.Setup(obj => obj.ChangeSpamStatus(QueryId,VerifyStatusID)).Returns(false);
            var Result = _QueryController.UpdateSpamStatus(QueryId,VerifyStatusID)as ObjectResult;   
            Assert.Equal(400,Result?.StatusCode);   
        }

        [Theory]
        [InlineData(1,2)]
        public void UpdateSpamStatus_ShouldReturnStatus500_WhenExceptionisThrown(int QueryId,int VerifyStatusID)
        {
            _queryService.Setup(obj => obj.ChangeSpamStatus(QueryId,VerifyStatusID)).Throws(new Exception());
            var Result = _QueryController.UpdateSpamStatus(QueryId,VerifyStatusID)as ObjectResult;   
            Assert.Equal(500,Result?.StatusCode);   
        }
        [Theory]
        [InlineData(0)]
        public void MarkQueryAsSolved_ShouldReturnStatusCode400_WhenValidationErrorOccured(int QueryId)
        {
            var Result=_QueryController.MarkQueryAsSolved(QueryId).Result as ObjectResult;
            Assert.Equal(400,Result?.StatusCode);
        }
    
    //MarkQueryAsSolved

        [Theory]
        [InlineData(1)]
        public void MarkQueryAsSolved_ShouldReturnStatusCode200_WhenQueryIsMArkedAsSolvedSucessfully(int QueryId)
        {
            _queryService.Setup(obj => obj.MarkQueryAsSolved(QueryId)).Returns(true);
            var Result=_QueryController.MarkQueryAsSolved(QueryId).Result as ObjectResult;
            Assert.Equal(200,Result?.StatusCode);
        }

        [Theory]
        [InlineData(1)]
        public void MarkQueryAsSolved_ShouldReturnStatusCode400_WhenQueryIsNotMArkedAsSolved(int QueryId)
        {
            _queryService.Setup(obj => obj.MarkQueryAsSolved(QueryId)).Returns(false);
            var Result=_QueryController.MarkQueryAsSolved(QueryId).Result as ObjectResult;
            Assert.Equal(400,Result?.StatusCode);
        }


        [Theory]
        [InlineData(1)]
        public void MarkQueryAsSolved_ShouldReturnStatusCode404_WhenItemNotFoundExceptionOccured(int QueryId)
        {
            _queryService.Setup(obj => obj.MarkQueryAsSolved(QueryId)).Throws(new ItemNotFoundException());
            var Result=_QueryController.MarkQueryAsSolved(QueryId).Result as ObjectResult;
            Assert.Equal(404,Result?.StatusCode);
        }

        [Theory]
        [InlineData(1)]
        public void MarkQueryAsSolved_ShouldReturnStatusCode500_WhenExceptionOccured(int QueryId)
        {
            _queryService.Setup(obj => obj.MarkQueryAsSolved(QueryId)).Throws(new Exception());
            var Result=_QueryController.MarkQueryAsSolved(QueryId).Result as ObjectResult;
            Assert.Equal(500,Result?.StatusCode);
        }

        //GetALL

        [Fact]
        public void GetALL_ShouldReturnStatusCode200_WhenQueryObjectIsPassed()

        {
            var Queries  = QueryMock.GetListOfQueries();
             _queryService.Setup(obj => obj.GetListOfQueries()).Returns(Queries);
            var Result = _QueryController.GetAll().Result as ObjectResult;
            Assert.Equal(200, Result?.StatusCode);

         }

         [Fact]

        public void GetALL_ShouldReturnStatusCode500_WhenExceptionIsThrown()

        {

            _queryService.Setup(obj => obj.GetListOfQueries()).Throws(new Exception());

            var Result = _QueryController.GetAll().Result as ObjectResult;

            Assert.Equal(500, Result?.StatusCode);

        }

        //GetTrendingQueries

        [Theory]
        [InlineData(0)]        
        public void GetTrendingQueries_ShouldReturnListOfQueries_WhenMethodIsCalled(int Range)
        {
            int count = QueryMock.GetListOfQueries().Count();
            var Queries = QueryMock.GetListOfQueries();
            // var Queries=QueryMock.GetListOfQuery().Where(item=>item.Range>);
            _queryService.Setup(obj => obj.GetTrendingQueries()).Returns(Queries);
            var Result = _QueryController.GetTrendingQueries().Result as ObjectResult;
            Assert.Equal(Queries, Result?.Value);
        }

         [Fact]
        public void GetTrendingQueries_ShouldReturnStatusCode400_WhenRangelimitexceeded()
        {
            int range=10;
            var Result = _QueryController.GetTrendingQueries(range).Result as ObjectResult;
            Assert.Equal(400,Result?.StatusCode);
        }

          [Fact]
         public void GetTrendingQueriess_ShouldReturnStatusCode500_WhenExceptionIsThrown()
        {
            _queryService.Setup(obj => obj.GetTrendingQueries()).Throws(new Exception());
            var Result = _QueryController.GetTrendingQueries().Result as ObjectResult;
            Assert.Equal(500, Result?.StatusCode);

        }

        //Get Latest Query

         [Theory]
        [InlineData(0)]        
        public void GetLatestQueries_ShouldReturnListOfQueries_WhenMethodIsCalled(int Range)
        {

            int count = QueryMock.GetListOfQueries().Count();
            var Queries = QueryMock.GetListOfQueries();
            _queryService.Setup(obj => obj.GetLatestQueries()).Returns(Queries);
            var Result = _QueryController.GetLatestQueries().Result as ObjectResult;
            Assert.Equal(Queries, Result?.Value);
        }

         [Fact]
        public void GetLatestQueries_ShouldReturnStatusCode400_WhenRangelimitexceeded()
        {
            int range=10;
            var Result = _QueryController.GetLatestQueries(range).Result as ObjectResult;
            Assert.Equal(400,Result?.StatusCode);
        }
        [Fact]
       public void GetLatestQueries_ShouldReturnStatusCode500_WhenExceptionIsThrown()

        {
            _queryService.Setup(obj => obj.GetLatestQueries()).Throws(new Exception());
            var Result = _QueryController.GetLatestQueries().Result as ObjectResult;
            Assert.Equal(500, Result?.StatusCode);
        }
        //Get Query
        [Theory]
        [InlineData(0)]
        public void GetQuery_ShouldReturnStatusCode400_WhenQueryIdIsZero(int queryId)
        {
            var Result = _QueryController.GetQuery(queryId).Result as ObjectResult;
            Assert.Equal(400, Result?.StatusCode);

        }

        [Theory]
        [InlineData(1)]
        public void GetQuery_ShouldReturnQueries_WhenQueryIdIsValid(int queryId)
        {
            
            _queryService.Setup(obj => obj.GetQuery(queryId)).Returns(true);
            var Result = _QueryController.GetQuery(queryId).Result as ObjectResult; 
            Assert.Equal(200, Result?.StatusCode);
        }

        [Fact]
        public void GetQuery_ShouldReturnStatusCode400_WhenExceptionIsThrown()
        {
            var queryId=4;
            _queryService.Setup(obj => obj.GetQuery(queryId)).Throws(new Exception());
            var Result = _QueryController.GetQuery(queryId).Result as ObjectResult;
            Assert.Equal(400, Result?.StatusCode);
         }

          [Fact]
        public void GetQuery_ShouldReturnStatusCode500_WhenItemNotFoundExceptionIsThrown()
        {
            int queryId = 2;
            _queryService.Setup(obj => obj.GetQuery(queryId)).Throws(new ItemNotFoundException());
            var Result = _QueryController.GetQuery(queryId).Result as ObjectResult;
            Assert.Equal(500, Result?.StatusCode);
        }
        
        // Get Queries By User Id
        
         [Fact]
         public void GetQueriesByUserId_ShouldReturnStatusCode200_WhenQueryObjectIsPassed()
         {
             int UserId=4;
             var ListOfQueriesByUserId = QueryMock.GetListOfQueries;
              _queryService.Setup(obj => obj.GetQueriesByUserId(UserId)).Returns(ListOfQueriesByUserId);
             var Result = _QueryController.GetQueriesByUserId().Result as ObjectResult;
             Assert.Equal(200, Result?.StatusCode);
         }

        [Fact]
        public void GetQueriesByQueryId_ShouldReturnStatusCode500_WhenExceptionIsThrown()
        {
            int UserId = 0;
            _queryService.Setup(obj => obj.GetQueriesByUserId(UserId)).Throws(new Exception());
            var Result = _QueryController.GetQueriesByUserId().Result as ObjectResult;
            Assert.Equal(500, Result?.StatusCode);
        }
    }
}