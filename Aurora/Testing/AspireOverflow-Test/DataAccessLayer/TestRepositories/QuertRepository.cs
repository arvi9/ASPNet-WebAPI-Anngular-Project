using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using AspireOverflow.CustomExceptions;
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
        
          [Fact]
        public void AddComment_ShouldReturnTrue_WhenQueryObjectIsValid()
        {
            QueryComment comment = QueryMock.GetValidQueryComment();

            var Result = _queryRepository.AddComment(comment);

            Assert.True(Result);
        }

        [Theory]
        [InlineData(null)]
        public void AddComment_ShouldThrowValidationException_WhenQueryObjectIsNull(QueryComment comment)
        {

            Assert.Throws<ValidationException>(() => _queryRepository.AddComment(comment));
        }

        [Fact]
        public void AddComment_ShouldThrowValidationException_WhenQueryObjectIsInvalid()

        {
            QueryComment comment = new QueryComment();

            Assert.Throws<ValidationException>(() => _queryRepository.AddComment(comment));
        }
        
         [Fact]
        public void AddCmment_ShouldThrowValidationException_WhenQueryObjectIsInvalid()

        {
            QueryComment comment = new QueryComment();

            Assert.Throws<ValidationException>(() => _queryRepository.AddComment(comment));
        }
        [Fact]
        public void GetQueries_ShouldReturnListOfQueries_WhenMethodIsCalled()
        {
            MockDBContext.SeedMockDataInMemoryDb(_context);

            var Queries = QueryMock.GetListOfQueries();

                      Assert.Equal(Queries.Count() + 1, _queryRepository.GetQueries().Count());
        }


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
  
        [Theory]
        [InlineData(2,true,false)]
         [InlineData(3,false,true)]
        public void UpdateQuery_ShouldReturnTrue_WhenValidDataPassed(int QueryId,bool IsSolved,bool IsDelete){
            Assert.True( _queryRepository.UpdateQuery(QueryId,IsSolved,IsDelete));
        }
  

      


    }
}