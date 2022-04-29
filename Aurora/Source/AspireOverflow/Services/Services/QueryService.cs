using AspireOverflow.DataAccessLayer;
using AspireOverflow.Models;

using AspireOverflow.DataAccessLayer.Interfaces;




namespace AspireOverflow.Services
{


    public class QueryService : IQueryService
    {
        private static IQueryRepository database;     

        private static ILogger<QueryService> _logger;

        public QueryService(ILogger<QueryService> logger)
        {
            _logger = logger ?? throw new NullReferenceException("logger can't be null");
            database = QueryRepositoryFactory.GetQueryRepositoryObject(logger) ?? throw new NullReferenceException("Instance of Query Repository can't be null");
        }




        public bool AddQuery(Query query)
        {
            if (!Validation.ValidateQuery(query)) return false;
            try
            {

                return database.AddQueryToDatabase(query);

            }
            catch (ArgumentNullException exception)
            {
                _logger.LogError($"Argument passed as Null in QueryRepository {exception.Message},{exception.StackTrace}");
               
                return false;
            }
            catch (Exception exception)
            {
                _logger.LogError($"{exception.Message},{exception.StackTrace}");
                return false;
            }
        }


        public IEnumerable<Query> GetQueries()
        {
          

            try
            {
                var ListOfQueries = database.GetQueriesFromDatabase();
                return ListOfQueries;
            }
            
            catch (Exception exception)
            {
                _logger.LogError($"{exception.Message},{exception.StackTrace}");
                throw exception;
            }


        }


        public IEnumerable<Query> GetQueriesByUserID(int UserID)
        {
            if (UserID <= 0) throw new ArgumentOutOfRangeException("UserID must be greater than 0");
            try
            {
               
                var ListofQueriesByUserId = from ListOfAllQueries in GetQueries()
                                            where ListOfAllQueries.CreatedBy == UserID
                                            select ListOfAllQueries;
                return ListofQueriesByUserId.ToList();
            }
            catch (Exception exception)
            {
                _logger.LogError($"{exception.Message},{exception.StackTrace}");
                throw exception;
            }

        }


        public IEnumerable<Query> GetQueriesByTitle(String Title)
        { if(Title ==null) throw new ArgumentNullException("Title value can't be null");
            try
            {
                var ListOfQueriesByTitle = from ListOfAllQueries in GetQueries()
                                           where ListOfAllQueries.Title.Contains(Title)
                                           select ListOfAllQueries;

                return ListOfQueriesByTitle;
            }
            catch (Exception exception)
            {
                _logger.LogError($"{exception.Message},{exception.StackTrace}");
                throw exception;
            }
        }


        public IEnumerable<Query> GetQueries(bool IsSolved)
        {
            try
            {
                var ListOfQueriesByTitle = from ListOfAllQueries in GetQueries()
                                           where ListOfAllQueries.IsSolved == IsSolved
                                           select ListOfAllQueries;

                return ListOfQueriesByTitle;
            }
            catch (Exception exception)
            {
                _logger.LogError($"{exception.Message},{exception.StackTrace}");
                throw exception;
            }
        }


        public bool AddCommentToQuery(QueryComment comment)
        {
            if (comment == null) throw new ArgumentNullException("comment object can't be null");
            try
            {
                return database.AddCommentToDatabase(comment);
            }
            catch (ArgumentNullException exception)
            {
                _logger.LogError($"{exception.Message},{exception.StackTrace}");
                return false;
            }
            catch (Exception exception)
            {
                _logger.LogError($"{exception.Message},{exception.StackTrace}");
                return false;
            }

        }

        public IEnumerable<QueryComment> GetComments(int QueryId)
        {
            if (QueryId <= 0) throw new ArgumentOutOfRangeException("QueryId must be greater than 0");
            try
            {
                var ListOfCommentsByQueryId = from ListOfAllComments in database.GetCommentsFromDatabase()
                                              where ListOfAllComments.QueryId == QueryId
                                              select ListOfAllComments;
                return ListOfCommentsByQueryId;
            }
            catch (Exception exception)
            {
                _logger.LogError($"{exception.Message},{exception.StackTrace}");
                throw exception;
            }

        }










    }
}
