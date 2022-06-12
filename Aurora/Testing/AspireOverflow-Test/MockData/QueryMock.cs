using System.Collections.Generic;
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

        public static List<Query> GetListOfQueries()
        {
            return new List<Query>(){
             new Query(){QueryId=1,Title = "Sample Title", Content = "What is meant by Unit Testing?",code = "", IsSolved = false, IsActive = true, CreatedBy = 1},
              new Query(){QueryId=2,Title = "Sample Title", Content = "What is meant by Unit Testing?",code = "", IsSolved = false, IsActive = true, CreatedBy = 5},
               new Query(){QueryId=3,Title = "Sample Title", Content = "What is meant by Unit Testing?",code = "", IsSolved = true, IsActive = true, CreatedBy = 10},
                new Query(){QueryId=4,Title = "Sample Title", Content = "What is meant by Unit Testing?",code = "", IsSolved = false, IsActive = true, CreatedBy = 11},
                 new Query(){QueryId=5,Title = "Sample Title", Content = "What is meant by Unit Testing?",code = "", IsSolved = true, IsActive = true, CreatedBy = 17},
                  new Query(){QueryId=6,Title = "Sample Title", Content = "What is meant by Unit Testing?",code = "", IsSolved = false, IsActive = true, CreatedBy = 8},
                   new Query(){QueryId=7,Title = "Sample Title", Content = "What is meant by Unit Testing?",code = "", IsSolved = true, IsActive = true, CreatedBy = 14},

            };
        }

         public static QueryComment GetValidQueryComment()
        {
            return new QueryComment(){
                Comment="what is Query",
                CreatedBy=1,
                QueryId=2

            };
        }
      public static List<QueryComment> GetListOfQueryComments(){

      return new List<QueryComment>();
      }
        
    
    }


}