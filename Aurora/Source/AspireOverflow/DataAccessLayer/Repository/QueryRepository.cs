

using AspireOverflow.Models;

using AspireOverflow.Services;
using AspireOverflow.DataAccessLayer.Interfaces;
using AspireOverflow.CustomExceptions;
using Microsoft.EntityFrameworkCore;

namespace AspireOverflow.DataAccessLayer
{

    public class QueryRepository : IQueryRepository
    {
        private AspireOverflowContext _context;

        private ILogger<QueryService> _logger;
        public QueryRepository(AspireOverflowContext context, ILogger<QueryService> logger)
        {
            _context = context ?? throw new NullReferenceException("Context can't be Null");
            _logger = logger ?? throw new NullReferenceException("logger can't be Null"); ;


        }




        public bool AddQuery(Query query)
        {
            Validation.ValidateQuery(query);
            try
            {

                _context.Queries.Add(query);
                _context.SaveChanges();

                return true;
            }
            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage("QueryRepository", "AddQuery(Query query)", exception, query));

                throw exception;

            }

        }



        public bool AddComment(QueryComment comment)
        {
            Validation.ValidateComment(comment);
            try
            {
                _context.QueryComments.Add(comment);
                _context.SaveChanges();
                return true;

            }
            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage("QueryRepository", "AddComment(QueryComment comment)", exception, comment));
                throw exception;
            }
        }

        //Updating query Either by marking as Solved 
        //Same method using to disable or soft delete the query
        public bool UpdateQuery(int QueryId, bool IsSolved, bool IsDelete)
        {

            if (QueryId <= 0) throw new ArgumentException($"Query Id must be greater than 0 where QueryId:{QueryId}");
        
            if (IsSolved && IsDelete) throw new ArgumentException("Both parameter cannot be true at the same time");
            try
            {
                var ExistingQuery = GetQueryByID(QueryId);

                if (IsSolved) ExistingQuery.IsSolved = IsSolved;
                if (IsDelete) ExistingQuery.IsActive = false;

                _context.Queries.Update(ExistingQuery);
                _context.SaveChanges();
                return true;
            }
            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage("QueryRepository", "UpdateQuery(int QueryId, bool IsSolved, bool IsDelete)", exception, IsSolved ? IsSolved : IsDelete));
                throw exception;
            }
        }


        public Query GetQueryByID(int QueryId)
        {
             if (QueryId <= 0) throw new ArgumentException($"Query Id must be greater than 0 where QueryId:{QueryId}");
            Query ExistingQuery;
            try
            {
                ExistingQuery = GetQueries().Where(query =>query.QueryId==QueryId).First();
                return ExistingQuery != null ? ExistingQuery : throw new ItemNotFoundException($"There is no matching Query data with QueryID :{QueryId}");
            }
            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage("QueryRepository", "GetQueryByID(int QueryId)", exception, QueryId));
                throw exception;
            }
        }

        public IEnumerable<Query> GetQueries()
        {
            try
            {
                var ListOfQueries = _context.Queries.Where(item => item.IsActive == true).Include("User").ToList();
                return ListOfQueries;

            }
            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage("QueryRepository", "GetQueries()", exception));

                throw exception;
            }


        }


        public IEnumerable<QueryComment> GetComments()
        {

            try
            {
                var ListOfComments = _context.QueryComments.Include("Query").Include("User").ToList();
                return ListOfComments;

            }
            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage("QueryRepository", "GetComments()", exception));

                throw exception;
            }
        }

            
    }

}
