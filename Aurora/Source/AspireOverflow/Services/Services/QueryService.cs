using AspireOverflow.DataAccessLayer;
using AspireOverflow.Models;
using System.Net;
using AspireOverflow.Controllers;
using AspireOverflow.DataAccessLayer.Interfaces;




namespace AspireOverflow.Services
{


    public class QueryService
    {
        private static IQueryRepository database = QueryRepositoryFactory.GetQueryRepositoryObject();        //Dependency Injection

        private static ILogger<QueryService> _logger;

        public QueryService(ILogger<QueryService> logger)
        {
            _logger = logger;

        }
        public bool AddQuery(Query query)
        {
            if (!Validation.ValidateQuery(query)) return false;
            try
            {

              return database.AddQueryToDatabase(query);

            }
            catch (Exception exception)
            {
                return false;
            }
        }


        public IEnumerable<Query> GetQueries()
        {
            if (database == null) throw new NullReferenceException();

            try
            {
                var ListOfQueries = database.GetQueriesFromDatabase();
                return ListOfQueries;
            }
            catch (NullReferenceException exception)
            {
                QueryController._logger.LogError(exception.Message);
                throw exception;
            }
            catch (Exception exception)
            {
                QueryController._logger.LogError(exception.Message);
                throw exception;
            }


        }


        public IEnumerable<Query> GetQueriesByUserID(int UserID)
        {
            if (UserID <= 0) throw new ArgumentOutOfRangeException();
            try
            {
                _logger.LogInformation("entered query service");
                var ListofQueriesByUserId = from ListOfAllQueries in GetQueries()
                                            where ListOfAllQueries.CreatedBy == UserID
                                            select ListOfAllQueries;
                return ListofQueriesByUserId.ToList();
            }
            catch (Exception exception)
            {
                throw exception;
            }

        }


        public IEnumerable<Query> GetQueriesByTitle(String Title)
        {

            var ListOfQueriesByTitle = from ListOfAllQueries in GetQueries()
                                       where ListOfAllQueries.Title.Contains(Title)
                                       select ListOfAllQueries;

            return ListOfQueriesByTitle;
        }


        public IEnumerable<Query> GetQueries(bool IsSolved)
        {

            var ListOfQueriesByTitle = from ListOfAllQueries in GetQueries()
                                       where ListOfAllQueries.IsSolved == IsSolved
                                       select ListOfAllQueries;

            return ListOfQueriesByTitle;
        }


        public HttpStatusCode AddCommentToQuery(QueryComment comment)
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

        public IEnumerable<QueryComment> GetComments(int QueryId)
        {
            var ListOfCommentsByQueryId = from ListOfAllComments in database.GetCommentsFromDatabase()
                                          where ListOfAllComments.QueryId == QueryId
                                          select ListOfAllComments;
            return ListOfCommentsByQueryId;

        }










    }
}
