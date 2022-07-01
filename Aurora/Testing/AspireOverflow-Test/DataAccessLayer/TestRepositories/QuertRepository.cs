using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using AspireOverflow.DataAccessLayer;
using AspireOverflow.Models;
using AspireOverflow.Services;
using AspireOverflowTest.DataAccessLayer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace AspireOverflowTest
{

    public class QuertRepositoryTest
    {
        private AspireOverflowContext _context;


        private Mock<ILogger<QueryRepository>> _logger = new Mock<ILogger<QueryRepository>>();
        private QueryRepository _queryRepository;

        public QuertRepositoryTest()
        {
            _context = MockDBContext.GetInMemoryDbContext();
            _queryRepository = new QueryRepository(_context, _logger.Object);
             


        }

        [Fact]
        public void AddQuery_ShouldReturnTrue_WhenQueryObjectIsValid()
        {
            Query query = QueryMock.GetValidQuery();

            var Result = _queryRepository.AddQuery(query);

            Assert.True(Result);
        }

        [Theory]
        [InlineData(null)]
        public void AddQuery_ShouldThrowValidationException_WhenQueryObjectIsNull(Query query)
        {

            Assert.Throws<ValidationException>(() => _queryRepository.AddQuery(query));
        }

        [Fact]
        public void AddQuery_ShouldThrowValidationException_WhenQueryObjectIsInvalid()

        {
            Query query = QueryMock.GetInValidQuery();
            Assert.Throws<ValidationException>(() => _queryRepository.AddQuery(query));
        }
        // [Fact]
        // public void AddQuery_ShouldThrowDBUpdateException()
        // {
        //     Query query = QueryMock.GetValidQuery();



        //     // _Mockcontext.Setup(item => item.Queries.Add(query)).Throws(new DbUpdateException());

        //     Assert.Throws<DbUpdateException>(() => _queryRepository.AddQuery(query));

        //     // _queryRepository=new QueryRepository(_context,_logger.Object);
        // }

        // Get Query by id

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void GetQueryById_ShouldThrowArgumentException_WhenQueryIdISLessThanZero(int QueryId){
            Assert.Throws<ArgumentException>(()=> _queryRepository.GetQueryByID(QueryId));
        }

        // Get Queries

        //  [Fact]
        //  public void GetQueries_ShouldReturnListOfQueries_WhenMethodIsCalled()
        //  {
        //      var Queries = QueryMock.GetListOfQueriesForSeeding();
        //      Assert.Equal(Queries.Count()  , _queryRepository.GetQueries().Count());
        //  }

         //  Get Comments
         [Fact]
         public void GetComments_ShouldReturnListOfComments_WhenMethodIsCalled()
         {
        //    MockDBContext.SeedMockDataInMemoryDb(_context);
             var Comments = QueryMock.GetListOfCommentsForSeeding();
             Assert.Equal(Comments.Count() ,_queryRepository.GetComments().Count());
         }  

         //AddSpam

         [Theory]
        [InlineData(null)]
        public void AddSpam_ShouldThrowValidationException_WhenSpamObjectIsNull(Spam spam)
        {
              Assert.Throws<ValidationException>(() => _queryRepository.AddSpam(spam));
        }
        [Fact]
        public void AddSpam_ShouldReturnValidationException_WhenSpamObjectIsInValid()
        {
            Spam spam = new Spam();
            Assert.Throws<ValidationException>(() => _queryRepository.AddSpam(spam));
        }
        [Fact]
        public void AddSpam_ShouldReturnTrue_WhenSpamObjectIsValid()
        {
            Spam spam = QueryMock.AddValidSpam(); 
            var Result = _queryRepository.AddSpam(spam);
            Assert.True(Result);
        }

        // [Fact]
        // public void GetListOfSpams_ReturnList_WhenObjectIsPassed()
        // {
        //     MockDBContext.SeedMockDataInMemoryDb(_context);
        //     var spams=QueryMock.GetListOfSpams();
        //     var Result = _queryRepository.GetSpams();
        //     Assert.Equal(spams.Count(),Result.Count());
        // }
        [Theory]
        [InlineData(0)]
         [InlineData(-1)]
        public void UpdateQuery_ShouldThrowArgumentException_WhenQueryIdISLessThanZero(int QueryId){
            Assert.Throws<ArgumentException>(()=> _queryRepository.UpdateQuery(QueryId,true,false));
        }
        
        [Theory]
        [InlineData(1,false,false)]
        public void UpdateQuery_ShouldThrowArgumentException_WhenIsSolvedAndIsDeleteEqualtoFalse(int QueryId,bool IsSolved,bool IsDelete){
            Assert.Throws<ArgumentException>(()=> _queryRepository.UpdateQuery(QueryId,IsSolved,IsDelete));
        }

          
        [Theory]
        [InlineData(12,true,false)]
        public void UpdateQuery_ShouldThrowItemNotFoundException_WhenQueryIdNotExists(int QueryId,bool IsSolved,bool IsDelete){
            Assert.False( _queryRepository.UpdateQuery(QueryId,IsSolved,IsDelete));
        }
  
        // [Theory]
        // [InlineData(2,true,false)]
        //  [InlineData(3,false,true)]
        // public void UpdateQuery_ShouldReturnTrue_WhenValidDataPassed(int QueryId,bool IsSolved,bool IsDelete){
        //     Assert.True( _queryRepository.UpdateQuery(QueryId,IsSolved,IsDelete));
        // }
    }
}