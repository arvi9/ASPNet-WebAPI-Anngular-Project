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
            _logger = logger;
            database = QueryRepositoryFactory.GetQueryRepositoryObject(logger);

        }


        public bool CreateQuery(Query query)
        {
            Validation.ValidateQuery(query);
            try
            {
                query.CreatedOn = DateTime.Now;
                return database.AddQuery(query);
            }
            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage("QueryService","CreateQuery", exception, query));
            return false;
            }
        }


        public bool RemoveQueryByQueryId(int QueryId)
        {
            if (QueryId <= 0) throw new ArgumentException($"Query Id must be greater than 0 where QueryId:{QueryId}");
            try
            {
                return database.UpdateQuery(QueryId, IsDelete: true);
            }
            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage("QueryService","RemoveQueryByQueryId", exception, QueryId));

           return false;
            }

        }


        public bool MarkQueryAsSolved(int QueryId)
        {
            if (QueryId <= 0) throw new ArgumentException($"Query Id must be greater than 0 where QueryId:{QueryId}");
            try
            {
                return database.UpdateQuery(QueryId, IsSolved: true);
            }

            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage("QueryService","MarkQueryAsSolved()", exception, QueryId));

            return false;
            }
        }

        public Query GetQuery(int QueryId)
        {
            if (QueryId <= 0) throw new ArgumentException($"Query Id must be greater than 0 where QueryId:{QueryId}");
            try
            {
                return database.GetQueryByID(QueryId);
            }
            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage("QueryService","GetQuery(int QueryId)", exception, QueryId));
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
                _logger.LogError(HelperService.LoggerMessage("QueryService","GetQueries()", exception));
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
                _logger.LogError(HelperService.LoggerMessage("QueryService","GetLatestQueries", exception));
             throw exception;
            }
        }



        public IEnumerable<Query> GetTrendingQueries()
        {
            try
            {

                var data = (database.GetComments().GroupBy(item => item.QueryId)).OrderByDescending(item => item.Count());

                var ListOfQueryId = (from item in data select item.First().QueryId).ToList();
                var ListOfQueries = GetQueries(false).ToList();
                var TrendingQueries = new List<Query>();
                foreach (var id in ListOfQueryId)
                {
                    TrendingQueries.Add(ListOfQueries.Find(item => item.QueryId == id));
                }

                return TrendingQueries;
            }

            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage("QueryService","GetTrendingQueries", exception));
             throw exception;
            }
        }



        public IEnumerable<Query> GetQueriesByUserId(int UserId)
        {
            if (UserId <= 0) throw new ArgumentException($"User Id must be greater than 0 where UserId:{UserId}");
            try
            {
                return GetQueries().Where(query => query.CreatedBy == UserId);
            }
            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage("QueryService","GetQueriesByUserId", exception, UserId));
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
                _logger.LogError(HelperService.LoggerMessage("QueryService","GetQueriesByTitle", exception, Title));
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
                _logger.LogError(HelperService.LoggerMessage("QueryService","GetQueries", exception, IsSolved));
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
                _logger.LogError(HelperService.LoggerMessage("QueryService","CreateComment", exception, comment));
             return false;
            }
        }

        public IEnumerable<QueryComment> GetComments(int QueryId)
        {
            if (QueryId <= 0) throw new ArgumentException($"Query Id must be greater than 0 where QueryId:{QueryId}");
            try
            {
                return database.GetComments().Where(comment => comment.QueryId == QueryId);

            }
            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage("QueryService","GetComments", exception, QueryId));
             throw exception;
            }

        }










    }
}
