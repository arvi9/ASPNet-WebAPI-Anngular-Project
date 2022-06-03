using AspireOverflow.Models;

namespace AspireOverflowTest
{
    static class QueryMock
    {

        public static Query GetInValidQuery()
        {
            return new Query();
        }
        public static Query GetValidQuery()
        {
            return new Query()
            {
                Title = "Sample Title",
                Content = "What is meant by Unit Testing?",
                code = "",
                IsSolved = false,
                IsActive = true,
                CreatedBy = 1
            };
        }
    }
}