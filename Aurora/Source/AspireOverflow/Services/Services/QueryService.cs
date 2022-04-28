using AspireOverflow.DataAccessLayer;
using AspireOverflow.Models;
using System.Net;
using AspireOverflow.Controllers;


namespace AspireOverflow.Services
{

    public interface IQueryService
    {
        public HttpStatusCode AddQuery(Query query);
        public IEnumerable<Query> GetQueries();
        public IEnumerable<Query> GetQueriesByUserID(int UserID);
        public IEnumerable<Query> GetQueriesByTitle(String Title);
        public IEnumerable<Query> GetQueries(bool IsSolved);
        public HttpStatusCode AddCommentToQuery(QueryComment comment);
        public IEnumerable<QueryComment> GetComments(int QueryId);

    }

    class QueryService
    {
        private static IQueryRepository database = QueryRepositoryFactory.GetQueryRepositoryObject();        //Dependency Injection

        public static HttpStatusCode AddQuery(Query query)
        {
            try
            {
                if (Validation.ValidateQuery(query))
                {

                    return database.AddQueryToDatabase(query);

                }
                return HttpStatusCode.NoContent;
            }
            catch (Exception exception)
            {
                return HttpStatusCode.BadRequest;
            }
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
            return ListOfCommentsByQueryId;

        }










    }
}
