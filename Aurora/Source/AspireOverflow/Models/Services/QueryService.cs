using System.Net;
namespace AspireOverflow.Models
{

    class QueryService
    {
        private static QueryDatabase database = new QueryDatabase(new AspireOverflowContext());

        public static IEnumerable<Query> GetQueries()
        {

            return database.GetQueries();

        }

        public static HttpStatusCode AddQuery(Query query)
        {
            if (Validation.ValidateQuery(query))
            {
               
                    return database.AddQueryToDatabase(query);
                
            }
            return HttpStatusCode.NoContent;



        }
    }
}
