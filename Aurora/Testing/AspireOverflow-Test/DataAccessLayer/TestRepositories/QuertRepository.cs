using System;
using System.ComponentModel.DataAnnotations;
using AspireOverflow.DataAccessLayer;
using AspireOverflow.Models;
using AspireOverflow.Services;
using AspireOverflowTest.DataAccessLayer;
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
        public void AddQuery_ShouldThrowDBUpdateException()

        {
            Query query = QueryMock.GetInValidQuery();
        
            Assert.Throws<ValidationException>(() => _queryRepository.AddQuery(query));
        }

        

    }
}