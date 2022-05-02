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
                return database.AddQuery(query);

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
                return database.AddComment(comment);
            }
            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage(DevelopmentTeam, nameof(CreateComment), exception), comment);
                return false;
            }
        }

        public bool RemoveQueryByQueryId(int QueryId, Enum DevelopmentTeam)
        {
            Validation.ValidateId(QueryId);
            try
            {
                return database.UpdateQuery(QueryId, IsDelete: true);
            }
            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage(DevelopmentTeam, nameof(RemoveQueryByQueryId), exception), QueryId);

                throw exception;
            }

        }


        public bool MarkQueryAsSolved(int QueryId, Enum DevelopmentTeam)
        {
            Validation.ValidateId(QueryId);
            try
            {
                return database.UpdateQuery(QueryId, IsSolved: true);
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
                return database.GetQueryByID(QueryId);
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
                var ListOfQueries = database.GetQueries();
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
              return GetQueries(DevelopmentTeam).Where(query => query.CreatedBy == UserId);
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
               return GetQueries(DevelopmentTeam).Where(query => query.Title.Contains(Title));
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
              return GetQueries(DevelopmentTeam).Where(query => query.IsSolved == IsSolved);
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
                return database.GetComments().Where(comment => comment.QueryId==QueryId);

            }
            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage(DevelopmentTeam, nameof(GetComments), exception, QueryId));
                throw exception;
            }

        }










    }
}
