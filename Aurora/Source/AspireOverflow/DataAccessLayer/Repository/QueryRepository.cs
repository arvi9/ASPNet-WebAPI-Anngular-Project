using AspireOverflow.Models;
using AspireOverflow.Services;
using AspireOverflow.DataAccessLayer.Interfaces;
using AspireOverflow.CustomExceptions;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
namespace AspireOverflow.DataAccessLayer
{
    public class QueryRepository : IQueryRepository
    {
        private readonly AspireOverflowContext _context;
        private readonly ILogger<QueryRepository> _logger;
        private readonly IConfiguration _configuration;
        private readonly Stopwatch _stopWatch = new Stopwatch();
        private bool IsTracingEnabled;
        public QueryRepository(AspireOverflowContext context, ILogger<QueryRepository> logger, IConfiguration configuration)
        {
            _context = context;
            _logger = logger;
            _configuration = configuration;
            IsTracingEnabled = GetIsTraceEnabledFromConfiguration();
        }
        //to add query using query object.
        public bool AddQuery(Query query)
        {
            if (IsTracingEnabled) _stopWatch.Start();
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
            finally
            {
                if (IsTracingEnabled)
                {
                    _stopWatch.Stop();
                    _logger.LogInformation($"Tracelog:QueryRepository Elapsed Time for AddQuery(Query query) - {_stopWatch.ElapsedMilliseconds}ms");
                }
            }
        }


        //to add comments for the query using query oject.
        public bool AddComment(QueryComment comment)
        {
            if (IsTracingEnabled) _stopWatch.Start();
            Validation.ValidateQueryComment(comment);
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
            finally
            {
                if (IsTracingEnabled)
                {
                    _stopWatch.Stop();
                    _logger.LogInformation($"Tracelog:QueryRepository Elapsed Time for AddComment(QueryComment comment) - {_stopWatch.ElapsedMilliseconds}ms");
                }
            }
        }


        //Updating query Either by marking as Solved 
        //Same method using to disable or soft delete the query
        public bool UpdateQuery(int QueryId, bool IsSolved = false, bool IsDelete = false)
        {
            if (IsTracingEnabled) _stopWatch.Start();
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
            finally
            {
                if (IsTracingEnabled)
                {
                    _stopWatch.Stop();
                    _logger.LogInformation($"Tracelog:QueryRepository Elapsed Time for UpdateQuery(int QueryId, bool IsSolved, bool IsDelete) - {_stopWatch.ElapsedMilliseconds}ms");
                }
            }
        }


        //to get the query using QueryId.
        public Query GetQueryByID(int QueryId)
        {
            if (IsTracingEnabled) _stopWatch.Start();
            if (QueryId <= 0) throw new ArgumentException($"Query Id must be greater than 0 where QueryId:{QueryId}");
            Query? ExistingQuery;
            try
            {
                ExistingQuery = _context.Queries.Include(e=>e.User).FirstOrDefault(query => query.QueryId == QueryId && query.CreatedOn > DateTime.Now.AddMonths(-GetRange()) && query.IsActive);
                return ExistingQuery != null ? ExistingQuery : throw new ItemNotFoundException($"There is no matching Query data with QueryID :{QueryId}");
            }
            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage("QueryRepository", "GetQueryByID(int QueryId)", exception, QueryId));
                throw;
            }
            finally
            {
                if (IsTracingEnabled)
                {
                    _stopWatch.Stop();
                    _logger.LogInformation($"Tracelog:QueryRepository Elapsed Time for GetQueryByID(int QueryId) - {_stopWatch.ElapsedMilliseconds}ms");
                }
            }
        }


        //to get the list of queries.
        public IEnumerable<Query> GetQueries()
        {
            if (IsTracingEnabled) _stopWatch.Start();
            try
            {
                var ListOfQueries = _context.Queries.Where(item => item.IsActive && item.CreatedOn > DateTime.Now.AddMonths(-GetRange())).Include(e => e.User).ToList();
                return ListOfQueries;
            }
            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage("QueryRepository", "GetQueries()", exception));
                throw;
            }
            finally
            {
                if (IsTracingEnabled)
                {
                    _stopWatch.Stop();
                    _logger.LogInformation($"Tracelog:QueryRepository Elapsed Time for GetQueries() - {_stopWatch.ElapsedMilliseconds}ms");
                }
            }
        }

        //gets the queries by it's UserId (User who created the query).
        public IEnumerable<Query> GetQueriesByUserId(int UserId)
        {
            if (IsTracingEnabled) _stopWatch.Start();
            if (UserId <= 0) throw new ArgumentException($"User Id must be greater than 0 where UserId:{UserId}");
            try
            {
                return _context.Queries.Where(query => query.CreatedBy == UserId && query.IsActive).Include(e => e.User);
            }
            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage("QueryRepository", "GetQueriesByUserId(int UserId)", exception, UserId));
                throw;
            }
            finally
            {
                if (IsTracingEnabled)
                {
                    _stopWatch.Stop();
                    _logger.LogInformation($"Tracelog:QueryRepository Elapsed Time for GetQueriesByUserId(int UserId) - {_stopWatch.ElapsedMilliseconds}ms");
                }
            }
        }

        //Fetch the query by the query title.
        public IEnumerable<Query> GetQueriesByTitle(String Title)
        {
            if (String.IsNullOrEmpty(Title)) throw new ArgumentException(" Title value can't be null");
            try
            {
                return _context.Queries.Where(query => query.Title!.Contains(Title) &&query.IsActive).Include(e => e.User);
            }
            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage("QueryService", "GetQueriesByTitle(String Title)", exception, Title));
                throw;
            }
            finally
            {
                if (IsTracingEnabled)
                {
                    _stopWatch.Stop();
                    _logger.LogInformation($"Tracelog:QueryRepository Elapsed Time for GetQueriesByTitle(String Title) - {_stopWatch.ElapsedMilliseconds}ms");
                }
            }
        }

        //Fetches the queries which has been solved by IsSolved.
        public IEnumerable<Query> GetQueriesByIsSolved(bool IsSolved)
        {
            if (IsTracingEnabled) _stopWatch.Start();
            try
            {
                return _context.Queries.Where(item => item.IsSolved == IsSolved && item.IsActive).Include(e => e.User);
            }
            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage("QueryService", "GetQueries(bool IsSolved)", exception, IsSolved));
                throw;
            }
            finally
            {
                if (IsTracingEnabled)
                {
                    _stopWatch.Stop();
                    _logger.LogInformation($"Tracelog:QueryRepository Elapsed Time for GetQueries(bool IsSolved) - {_stopWatch.ElapsedMilliseconds}ms");
                }
            }
        }

        //to get the list of query comments.
        public IEnumerable<QueryComment> GetComments()
        {
            if (IsTracingEnabled) _stopWatch.Start();
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
            finally
            {
                if (IsTracingEnabled)
                {
                    _stopWatch.Stop();
                    _logger.LogInformation($"Tracelog:QueryRepository Elapsed Time for GetComments() - {_stopWatch.ElapsedMilliseconds}ms");
                }
            }
        }


        //to add a query as spam using spam object.
        public bool AddSpam(Spam spam)
        {
            if (IsTracingEnabled) _stopWatch.Start();
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
            finally
            {
                if (IsTracingEnabled)
                {
                    _stopWatch.Stop();
                    _logger.LogInformation($"Tracelog:QueryRepository Elapsed Time for AddSpam(Spam spam) - {_stopWatch.ElapsedMilliseconds}ms");
                }
            }
        }


        //to get the list of spam queries.
        public IEnumerable<Spam> GetSpams()
        {
            if (IsTracingEnabled) _stopWatch.Start();
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
            finally
            {
                if (IsTracingEnabled)
                {
                    _stopWatch.Stop();
                    _logger.LogInformation($"Tracelog:QueryRepository Elapsed Time for GetSpams() - {_stopWatch.ElapsedMilliseconds}ms");
                }
            }
        }


        //to update the query as spam using QueryId and VerifyStatusId.
        public bool UpdateSpam(int QueryId, int VerifyStatusID, int UpdatedByUserId)
        {
            if (IsTracingEnabled) _stopWatch.Start();
            if (QueryId <= 0) throw new ArgumentException($"QueryId  must be greater than 0 where QueryId:{QueryId}");
            if (VerifyStatusID <= 0 || VerifyStatusID > 3) throw new ArgumentException($"Verify Status Id must be greater than 0 where VerifyStatusId:{VerifyStatusID}");
            try
            {
                var ExistingSpams = _context.Spams.Where(item => item.QueryId == QueryId).ToList();
                if (ExistingSpams == null) throw new ItemNotFoundException($"There is no matching Spam data with QueryId :{QueryId}");
                foreach (var spam in ExistingSpams)
                {
                    spam.VerifyStatusID = VerifyStatusID;
                    spam.UpdatedBy = UpdatedByUserId;
                    spam.UpdatedOn = DateTime.Now;
                }
                _context.Spams.UpdateRange(ExistingSpams);
                _context.SaveChanges();
                return true;
            }
            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage("QueryRepository", "UpdateSpam(int QueryId, int VerifyStatusID)", exception, VerifyStatusID));
                return false;
            }
            finally
            {
                if (IsTracingEnabled)
                {
                    _stopWatch.Stop();
                    _logger.LogInformation($"Tracelog:QueryRepository Elapsed Time for UpdateSpam(int QueryId, int VerifyStatusID) - {_stopWatch.ElapsedMilliseconds}ms");
                }
            }
        }

        //gets the total count of the queries.
        public object GetCountOfQueries()
        {
            if (IsTracingEnabled) _stopWatch.Start();
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
            finally
            {
                if (IsTracingEnabled)
                {
                    _stopWatch.Stop();
                    _logger.LogInformation($"Tracelog:QueryRepository Elapsed Time for GetCountOfQueries() - {_stopWatch.ElapsedMilliseconds}ms");
                }
            }
        }

        //Getting Range from Configuration for Data fetching .
        private int GetRange()
        {
            if (IsTracingEnabled) _stopWatch.Start();
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
            finally
            {
                if (IsTracingEnabled)
                {
                    _stopWatch.Stop();
                    _logger.LogInformation($"Tracelog:QueryRepository Elapsed Time for GetDuration() - {_stopWatch.ElapsedMilliseconds}ms");
                }
            }
        }

        //Get Tracing Enabled or not from Configuration
        public bool GetIsTraceEnabledFromConfiguration()
        {
            try
            {
                var IsTracingEnabledFromConfiguration = _configuration["Tracing:IsEnabled"];
                return IsTracingEnabledFromConfiguration != null ? Convert.ToBoolean(IsTracingEnabledFromConfiguration) : false;
            }
            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage("UserRepository", "GetIsTraceEnabledFromConfiguration()", exception));
                return false;
            }
        }
    }
}