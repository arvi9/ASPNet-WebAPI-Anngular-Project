using System.ComponentModel.DataAnnotations;
using AspireOverflow.DataAccessLayer;
using AspireOverflow.Models;
using Microsoft.EntityFrameworkCore;

using AspireOverflow.DataAccessLayer.Interfaces;




namespace AspireOverflow.Services
{


    public class QueryService
    {
        private static IQueryRepository database;

        private static ILogger<QueryService> _logger;

        public QueryService(ILogger<QueryService> logger)
        {
            _logger = logger ;
            database = QueryRepositoryFactory.GetQueryRepositoryObject(logger);

        }


        public bool CreateQuery(Query query)
        {
            Validation.ValidateQuery(query);
            try
            {
                Validation.SetUserDefaultPropertyValues(query);
                return database.AddQuery(query);
            }
            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage(nameof(QueryService), nameof(CreateQuery), exception, query));
                throw exception;
            }
        }


        public bool RemoveQueryByQueryId(int QueryId)
        {
            Validation.ValidateId(QueryId);
            try
            {
                return database.UpdateQuery(QueryId, IsDelete: true);
            }
            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage(nameof(QueryService), nameof(RemoveQueryByQueryId), exception), QueryId);

                throw exception;
            }

        }


        public bool MarkQueryAsSolved(int QueryId)
        {
            Validation.ValidateId(QueryId);
            try
            {
                return database.UpdateQuery(QueryId, IsSolved: true);
            }

            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage(nameof(QueryService), nameof(MarkQueryAsSolved), exception), QueryId);

                throw exception;
            }
        }

        public Query GetQuery(int QueryId)
        {
            Validation.ValidateId(QueryId);
            try
            {
                return database.GetQueryByID(QueryId);
            }
            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage(nameof(QueryService), nameof(GetQuery), exception, QueryId));
                throw exception;
            }

        }


        public IEnumerable<Query> GetQueries()
        {
            try
            {
                var ListOfQueries = database.GetQueries();
                return ListOfQueries;
            }
            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage(nameof(QueryService), nameof(GetQueries), exception));
                throw exception;
            }

        }



        public IEnumerable<Query> GetLatestQueries()
        {
            try
            {
                var ListOfQueries = GetQueries().OrderByDescending(query => query.CreatedOn);
                return ListOfQueries;
            }

            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage(nameof(QueryService), nameof(GetLatestQueries), exception));
                throw exception;
            }
        }



        public  IEnumerable<Query> GetTrendingQueries()
        {
            try
            {
               
                var data = (database.GetComments().GroupBy(item => item.QueryId)).OrderByDescending(item => item.Count());

                 var ListOfQueryId = (from item in data select item.First().QueryId).ToList();
                var ListOfQueries = GetQueries(false).ToList();
                var TrendingQueries = new List<Query>();
                  foreach(var id in ListOfQueryId){
                    TrendingQueries.Add(ListOfQueries.Find(item =>item.QueryId ==id));
                  }
              
                return TrendingQueries;
            }

            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage(nameof(QueryService), nameof(GetTrendingQueries), exception));
                throw exception;
            }
        }



        public IEnumerable<Query> GetQueriesByUserId(int UserId)
        {
            Validation.ValidateId(UserId);
            try
            {
                return GetQueries().Where(query => query.CreatedBy == UserId);
            }
            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage(nameof(QueryService), nameof(GetQueriesByUserId), exception, UserId));
                throw exception;
            }

        }


        public IEnumerable<Query> GetQueriesByTitle(String Title)
        {
            if (String.IsNullOrEmpty(Title)) throw new ArgumentNullException("Title value can't be null");
            try
            {
                return GetQueries().Where(query => query.Title.Contains(Title));
            }
            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage(nameof(QueryService), nameof(GetQueriesByTitle), exception, Title));
                throw exception;
            }
        }


        public IEnumerable<Query> GetQueries(bool IsSolved)
        {
            try
            {
                return GetQueries().Where(query => query.IsSolved == IsSolved);
            }
            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage(nameof(QueryService), nameof(GetQueries), exception, IsSolved));
                throw exception;
            }
        }



        public bool CreateComment(QueryComment comment)
        {
            Validation.ValidateComment(comment);
            try
            {
                return database.AddComment(comment);
            }
            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage(nameof(QueryService), nameof(CreateComment), exception), comment);
                throw exception;
            }
        }

        public IEnumerable<QueryComment> GetComments(int QueryId)
        {
            Validation.ValidateId(QueryId);
            try
            {
                return database.GetComments().Where(comment => comment.QueryId == QueryId);

            }
            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage(nameof(QueryService), nameof(GetComments), exception, QueryId));
                throw exception;
            }

        }










    }
}
