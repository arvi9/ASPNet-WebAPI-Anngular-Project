using System.ComponentModel.DataAnnotations;
using AspireOverflow.DataAccessLayer;
using AspireOverflow.Models;


using AspireOverflow.DataAccessLayer.Interfaces;




namespace AspireOverflow.Services
{


    public class QueryService 
    {
        private static IQueryRepository database;

        private static ILogger<QueryService> _logger;

        public QueryService(ILogger<QueryService> logger)
        {
            _logger = logger ?? throw new NullReferenceException("logger can't be null");
            database = QueryRepositoryFactory.GetQueryRepositoryObject(logger);

        }


        public bool CreateQuery(Query query, Enum DevelopmentTeam)
        {
            if (!Validation.ValidateQuery(query)) throw new ValidationException("Given data is InValid");
            try
            {
                return database.AddQueryToDatabase(query);

            }

            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage(DevelopmentTeam, nameof(CreateQuery), exception, query));
                return false;
            }
        }

        public bool CreateComment(QueryComment comment, Enum DevelopmentTeam)
        {
           Validation.ValidateComment(comment);
            try
            {
                return database.AddCommentToDatabase(comment);
            }
            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage(DevelopmentTeam, nameof(CreateComment), exception), comment);
                return false;
            }
        }

        public bool RemoveQueryByQueryId(int QueryId,Enum DevelopmentTeam)
        {
          Validation.ValidateId(QueryId);
            try
            {
                return database.UpdateQuery(QueryId,IsDelete:true);
            }
            catch (Exception exception)
            {
                   _logger.LogError(HelperService.LoggerMessage(DevelopmentTeam, nameof(RemoveQueryByQueryId), exception), QueryId);
            
                throw exception;
            }

        }


        public bool MarkQueryAsSolved(int QueryId,Enum DevelopmentTeam)
        {
           Validation.ValidateId(QueryId);
           try
           {
               return database.UpdateQuery(QueryId,IsSolved:true);
           }
        
           catch (Exception exception)
           {
               _logger.LogError(HelperService.LoggerMessage(DevelopmentTeam, nameof(MarkQueryAsSolved), exception), QueryId);
          
               throw exception;
           }
        }

        public Query GetQuery(int QueryId, Enum DevelopmentTeam)
        {
            Validation.ValidateId(QueryId);
        try
        {
            return database.GetQuery(QueryId);
        }
        catch (Exception exception)
        {
            
               _logger.LogError(HelperService.LoggerMessage(DevelopmentTeam, nameof(GetQuery), exception, QueryId));
          
            throw exception;
        }
            
        }


        public IEnumerable<Query> GetQueries(Enum DevelopmentTeam)
        {

            try
            {
                var ListOfQueries = database.GetQueriesFromDatabase();
                return ListOfQueries;
            }

            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage(DevelopmentTeam, nameof(GetQueries), exception));
                throw exception;
            }


        }


        public IEnumerable<Query> GetQueriesByUserId(int UserId, Enum DevelopmentTeam)
        {
           Validation.ValidateId(UserId);
            try
            {

                var ListofQueriesByUserId = from ListOfAllQueries in GetQueries(DevelopmentTeam)
                                            where ListOfAllQueries.CreatedBy == UserId
                                            select ListOfAllQueries;
                return ListofQueriesByUserId.ToList();
            }
            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage(DevelopmentTeam, nameof(GetQueriesByUserId), exception, UserId));
                throw exception;
            }

        }


        public IEnumerable<Query> GetQueriesByTitle(String Title, Enum DevelopmentTeam)
        {
            if (String.IsNullOrEmpty(Title)) throw new ArgumentNullException("Title value can't be null");
            try
            {
                var ListOfQueriesByTitle = from ListOfAllQueries in GetQueries(DevelopmentTeam)
                                           where ListOfAllQueries.Title.Contains(Title)
                                           select ListOfAllQueries;

                return ListOfQueriesByTitle;
            }
            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage(DevelopmentTeam, nameof(GetQueriesByTitle), exception, Title));
                throw exception;
            }
        }


        public IEnumerable<Query> GetQueries(bool IsSolved, Enum DevelopmentTeam)
        {
            try
            {
                var ListOfQueriesByTitle = from ListOfAllQueries in GetQueries(DevelopmentTeam)
                                           where ListOfAllQueries.IsSolved == IsSolved
                                           select ListOfAllQueries;

                return ListOfQueriesByTitle;
            }
            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage(DevelopmentTeam, nameof(GetQueries), exception, IsSolved));
                throw exception;
            }
        }




        public IEnumerable<QueryComment> GetComments(int QueryId, Enum DevelopmentTeam)
        {
          Validation.ValidateId(QueryId);
            try
            {
                var ListOfCommentsByQueryId = from ListOfAllComments in database.GetCommentsFromDatabase()
                                              where ListOfAllComments.QueryId == QueryId
                                              select ListOfAllComments;
                return ListOfCommentsByQueryId;
            }
            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage(DevelopmentTeam, nameof(GetComments), exception, QueryId));
                throw exception;
            }

        }










    }
}
