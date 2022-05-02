

using AspireOverflow.Models;

using AspireOverflow.Services;
using AspireOverflow.DataAccessLayer.Interfaces;
using AspireOverflow.CustomExceptions;

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
                _logger.LogError(HelperService.LoggerMessage(nameof(QueryRepository), nameof(AddQuery), exception, query));

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
                _logger.LogError(HelperService.LoggerMessage(nameof(QueryRepository), nameof(AddComment), exception, comment));
                throw exception;
            }
        }

        //Updating query Either by marking as Solved 
        //Same method using to disable or soft delete the query
        public bool UpdateQuery(int QueryId, bool IsSolved, bool IsDelete)
        {

            Validation.ValidateId(QueryId);
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
                _logger.LogError(HelperService.LoggerMessage(nameof(QueryRepository), nameof(UpdateQuery), exception, IsSolved ? IsSolved : IsDelete));
                throw exception;
            }
        }


        public Query GetQueryByID(int QueryId)
        {
            Validation.ValidateId(QueryId);
            Query ExistingQuery;
            try
            {
                ExistingQuery = _context.Queries.Find(QueryId);
                return ExistingQuery != null ? ExistingQuery : throw new ItemNotFoundException($"There is no matching Query data with QueryID :{QueryId}");
            }
            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage(nameof(QueryRepository), nameof(GetQueryByID), exception, QueryId));
                throw exception;
            }
        }

        public IEnumerable<Query> GetQueries()
        {
            try
            {
                var ListOfQueries = _context.Queries.Where(item => item.IsActive == true);
                return ListOfQueries;

            }
            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage(nameof(QueryRepository), nameof(GetQueries), exception));

                throw exception;
            }


        }


        public IEnumerable<QueryComment> GetComments()
        {

            try
            {
                var ListOfComments = _context.QueryComments;
                return ListOfComments;

            }
            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage(nameof(QueryRepository), nameof(GetComments), exception));

                throw exception;
            }
        }


    }

}
