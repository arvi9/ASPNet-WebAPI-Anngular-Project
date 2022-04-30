using System.ComponentModel.DataAnnotations;
using AspireOverflow.DataAccessLayer;
using AspireOverflow.Models;
using System;

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
            database = QueryRepositoryFactory.GetQueryRepositoryObject(logger);

        }


        public bool AddQuery(Query query,Enum DevelopmentTeam)
        {
            if (Validation.ValidateQuery(query)) throw new ValidationException("Given data is InValid");
            try
            {
             
                return database.AddQueryToDatabase(query);

            }
           
            catch (Exception exception)
            {
          
                _logger.LogError(HelperService.LoggerMessage(DevelopmentTeam,nameof(AddQuery),exception,query));
                return false;
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
               _logger.LogError(HelperService.LoggerMessage(DevelopmentTeam,nameof(GetQueries),exception));
                throw exception;
            }


        }


        public IEnumerable<Query> GetQueriesByUserID(int UserID,Enum DevelopmentTeam)
        {
            if (UserID <= 0) throw new ArgumentOutOfRangeException("UserID must be greater than 0");
            try
            {
               
                var ListofQueriesByUserId = from ListOfAllQueries in GetQueries(DevelopmentTeam)
                                            where ListOfAllQueries.CreatedBy == UserID
                                            select ListOfAllQueries;
                return ListofQueriesByUserId.ToList();
            }
            catch (Exception exception)
            {
                 _logger.LogError(HelperService.LoggerMessage(DevelopmentTeam,nameof(GetQueriesByUserID),exception,UserID));
                throw exception;
            }

        }


        public IEnumerable<Query> GetQueriesByTitle(String Title,Enum DevelopmentTeam)
        { if(String.IsNullOrEmpty(Title)) throw new ArgumentNullException("Title value can't be null");
            try
            {
                var ListOfQueriesByTitle = from ListOfAllQueries in GetQueries(DevelopmentTeam)
                                           where ListOfAllQueries.Title.Contains(Title)
                                           select ListOfAllQueries;

                return ListOfQueriesByTitle;
            }
            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage(DevelopmentTeam,nameof(GetQueriesByTitle),exception,Title));
                throw exception;
            }
        }


        public IEnumerable<Query> GetQueries(bool IsSolved,Enum DevelopmentTeam)
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
                  _logger.LogError(HelperService.LoggerMessage(DevelopmentTeam,nameof(GetQueries),exception,IsSolved));
                throw exception;
            }
        }


        public bool AddCommentToQuery(QueryComment comment,Enum DevelopmentTeam)
        {
            if (comment == null) throw new ArgumentNullException("comment object can't be null");
            try
            {
                return database.AddCommentToDatabase(comment);
            }
          
            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage(DevelopmentTeam,nameof(AddCommentToQuery),exception),comment);
                return false;
            }

        }

        public IEnumerable<QueryComment> GetComments(int QueryId,Enum DevelopmentTeam)
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
                 _logger.LogError(HelperService.LoggerMessage(DevelopmentTeam,nameof(GetComments),exception,QueryId));
                throw exception;
            }

        }










    }
}
