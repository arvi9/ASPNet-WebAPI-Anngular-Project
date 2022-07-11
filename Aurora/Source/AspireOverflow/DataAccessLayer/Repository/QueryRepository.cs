using AspireOverflow.Models;
using AspireOverflow.Services;
using AspireOverflow.DataAccessLayer.Interfaces;
using AspireOverflow.CustomExceptions;
using Microsoft.EntityFrameworkCore;
namespace AspireOverflow.DataAccessLayer
{
    public class QueryRepository : IQueryRepository
    {
        private readonly AspireOverflowContext _context;
        private readonly ILogger<QueryRepository> _logger;
        private readonly IConfiguration _configuration;
        public QueryRepository(AspireOverflowContext context, ILogger<QueryRepository> logger, IConfiguration configuration)
        {
            _context = context;
            _logger = logger;
            _configuration = configuration;
        }


        //to add query using query object.
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
                return false;
            }
        }


        //to add comments for the query using query oject.
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
                return false;
            }
        }


        //Updating query Either by marking as Solved 
        //Same method using to disable or soft delete the query
        public bool UpdateQuery(int QueryId, bool IsSolved = false, bool IsDelete = false)
        {
            if (QueryId <= 0) throw new ArgumentException($"Query Id must be greater than 0 where QueryId:{QueryId}");
            if (IsSolved == IsDelete) throw new ArgumentException("Both parameter cannot be true/false at the same time");
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
                return false;
            }
        }


        //to get the query using QueryId.
        public Query GetQueryByID(int QueryId)
        {
            if (QueryId <= 0) throw new ArgumentException($"Query Id must be greater than 0 where QueryId:{QueryId}");
            Query? ExistingQuery;
            try
            {
                ExistingQuery = _context.Queries.FirstOrDefault(query => query.QueryId == QueryId);
                return ExistingQuery != null ? ExistingQuery : throw new ItemNotFoundException($"There is no matching Query data with QueryID :{QueryId}");
            }
            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage("QueryRepository", "GetQueryByID(int QueryId)", exception, QueryId));
                throw;
            }
        }


        //to get the list of queries.
        public IEnumerable<Query> GetQueries()
        {
            try
            {
                var ListOfQueries = _context.Queries.Where(item => item.IsActive && item.CreatedOn > DateTime.Now.AddMonths(-GetDuration())).Include(e => e.User).ToList();
                return ListOfQueries;
            }
            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage("QueryRepository", "GetQueries()", exception));
                throw;
            }
        }
        public IEnumerable<Query> GetQueriesByUserId(int UserId)
        {
            if (UserId <= 0) throw new ArgumentException($"User Id must be greater than 0 where UserId:{UserId}");
            try
            {
                return _context.Queries.Where(query => query.CreatedBy == UserId).Include(e => e.User);
            }
            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage("QueryRepository", "GetQueriesByUserId(int UserId)", exception, UserId));
                throw;
            }
        }

        //Fetch the query by the query title.
        public IEnumerable<Query> GetQueriesByTitle(String Title)
        {
            if (String.IsNullOrEmpty(Title)) throw new ArgumentException(" Title value can't be null");
            try
            {
                return _context.Queries.Where(query => query.Title!.Contains(Title)).Include(e => e.User);
            }
            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage("QueryService", "GetQueriesByTitle(String Title)", exception, Title));
                throw;
            }
        }

        public IEnumerable<Query> GetQueriesByIsSolved(bool IsSolved)
        {
            try
            {
                return _context.Queries.Where(item => item.IsSolved == IsSolved).Include(e => e.User);
            }
            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage("QueryService", "GetQueries(bool IsSolved)", exception, IsSolved));
                throw;
            }
        }

        //to get the list of query comments.
        public IEnumerable<QueryComment> GetComments()
        {
            try
            {
                var ListOfComments = _context.QueryComments.Include(e => e.Query).Include(e => e.User).ToList();
                return ListOfComments;
            }
            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage("QueryRepository", "GetComments()", exception));
                throw;
            }
        }


        //to add a query as spam using spam object.
        public bool AddSpam(Spam spam)
        {
            Validation.ValidateSpam(spam);
            try
            {
                if (_context.Spams.Any(item => item.UserId == spam.UserId && item.QueryId == spam.QueryId)) throw new ArgumentException("You have already reported the same query as Spam");
                _context.Spams.AddRange(spam);
                _context.SaveChanges();
                return true;
            }
            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage("QueryRepository", "AddSpam(Spam spam)", exception, spam));
                return false;
            }
        }


        //to get the list of spam queries.
        public IEnumerable<Spam> GetSpams()
        {
            try
            {
                var ListOfSpams = _context.Spams.Include(e => e.Query).Include(e => e.User).ToList();
                return ListOfSpams;
            }
            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage("QueryRepository", "GetSpams()", exception));
                throw;
            }
        }


        //to update the query as spam using QueryId and VerifyStatusId.
        public bool UpdateSpam(int QueryId, int VerifyStatusID)
        {
            if (QueryId <= 0) throw new ArgumentException($"QueryId  must be greater than 0 where QueryId:{QueryId}");
            if (VerifyStatusID <= 0 || VerifyStatusID > 3) throw new ArgumentException($"Verify Status Id must be greater than 0 where VerifyStatusId:{VerifyStatusID}");
            try
            {
                var ExistingSpam = _context.Spams.Where(item => item.QueryId == QueryId).ToList();
                if (ExistingSpam == null) throw new ItemNotFoundException($"There is no matching Spam data with QueryId :{QueryId}");
                ExistingSpam.ForEach(Item => Item.VerifyStatusID = VerifyStatusID);
                _context.Spams.UpdateRange(ExistingSpam);
                _context.SaveChanges();
                return true;
            }
            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage("QueryRepository", "UpdateSpam(int QueryId, int VerifyStatusID)", exception, VerifyStatusID));
                return false;
            }
        }

        public object GetCountOfQueries()
        {
            try
            {
                return new
                {
                    TotalNumberOfQueries = _context.Queries.Count(),
                    SolvedQueries = _context.Queries.Count(item => item.IsSolved && item.IsActive),
                    UnSolvedQueries = _context.Queries.Count(item => !item.IsSolved && item.IsActive),
                };
            }
            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage("QueryRepository", "GetCountOfQueries()", exception));
                throw;
            }
        }

        private int GetDuration()
        {
            try
            {
                var Duration = _configuration["Data_Fetching_Duration:In_months"];
                return Duration != null ? Convert.ToInt32(Duration) : throw new Exception("Data_Fetching_Duration:In_months-> value is Invalif  in AppSettings.json ");
            }
            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage("UserRepository", " GetDuration()", exception));
                throw;

            }
        }
    }
}