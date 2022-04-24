using System.Reflection;
using System.IO.Enumeration;
using System.Linq;
using System.Net;
using AspireOverflow.Controllers;

namespace AspireOverflow.Models
{

    class QueryService
    {
        private static QueryDatabase database = new QueryDatabase(new AspireOverflowContext());

        public static HttpStatusCode AddQuery(Query query)
        {
            if (Validation.ValidateQuery(query))
            {

                return database.AddQueryToDatabase(query);

            }
            return HttpStatusCode.NoContent;

        }

        public static IEnumerable<Query> GetQueries()
        {

            return database.GetQueriesFromDatabase();

        }

        public static IEnumerable<Query> GetQueriesByUserID(int UserID)
        {

            var ListofQueriesByUserId = from ListOfAllQueries in GetQueries()
                                        where ListOfAllQueries.CreatedBy == UserID
                                        select ListOfAllQueries;

            return ListofQueriesByUserId.ToList();
        }


        public static IEnumerable<Query> GetQueriesByTitle(String Title)
        {

            var ListOfQueriesByTitle = from ListOfAllQueries in GetQueries()
                                       where ListOfAllQueries.Title.Contains(Title)
                                       select ListOfAllQueries;

            return ListOfQueriesByTitle;
        }


        public static IEnumerable<Query> GetQueries(bool IsSolved)
        {

            var ListOfQueriesByTitle = from ListOfAllQueries in GetQueries()
                                       where ListOfAllQueries.IsSolved == IsSolved
                                       select ListOfAllQueries;

            return ListOfQueriesByTitle;
        }


        public static HttpStatusCode AddCommentToQuery(QueryComment comment)
        {
            if (comment == null) return HttpStatusCode.NoContent;
            try
            {
                return database.AddCommentToDatabase(comment);
            }
            catch (Exception exception)
            {
                QueryController._logger.LogError(exception.Message);
                return HttpStatusCode.BadRequest;

            }

        }

        public static IEnumerable<QueryComment> GetComments(int QueryId)
        {
                 var ListOfCommentsByQueryId = from ListOfAllComments in database.GetCommentsFromDatabase()
                                       where ListOfAllComments.QueryId == QueryId
                                       select ListOfAllComments;
            return ListOfCommentsByQueryId ;

        }

        








    }
}
