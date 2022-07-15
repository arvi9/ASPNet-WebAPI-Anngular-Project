using System;
using System.Data;
using System.Reflection.PortableExecutable;
using System.Net.Mail;
using System.ComponentModel.DataAnnotations;
using AspireOverflow.DataAccessLayer;
using AspireOverflow.Models;
using AspireOverflow.CustomExceptions;
using Microsoft.EntityFrameworkCore;
using AspireOverflow.DataAccessLayer.Interfaces;
using System.Diagnostics;
namespace AspireOverflow.Services
{
    public class QueryService : IQueryService
    {
        private readonly IQueryRepository database;
        private readonly ILogger<QueryService> _logger;
        private readonly MailService _mailService;
        private readonly Stopwatch _stopWatch = new Stopwatch();
        private bool IsTracingEnabled;
        public QueryService(ILogger<QueryService> logger, MailService mailService, IQueryRepository _queryRepository)
        {
            _logger = logger;
            _mailService = mailService;
            database = _queryRepository;
            IsTracingEnabled = database.GetIsTraceEnabledFromConfiguration();
        }


        //to raise an query in the forum.
        public bool CreateQuery(Query query)
        {
            if (IsTracingEnabled) _stopWatch.Start();
            
            Validation.ValidateQuery(query);
            try
            {
                query.CreatedOn = DateTime.UtcNow;
                var IsAddedSuccessfully = database.AddQuery(query);
                if (IsAddedSuccessfully) _mailService?.SendEmailAsync(HelperService.QueryMail("Manimaran.0610@gmail.com", query.Title!, "Query Created Successfully"));
                return IsAddedSuccessfully;
            }
            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage("QueryService", "CreateQuery(Query query)", exception, query));
                return false;
            }
              finally
            {
                if (IsTracingEnabled)
                {
                    _stopWatch.Stop();
                    _logger.LogInformation($"Tracelog:QueryService Elapsed Time for CreateQuery(Query query) - {_stopWatch.ElapsedMilliseconds}ms");
                }
            }
        }


        //To Remove the raised query using QueryId if it is an spam.
        public bool RemoveQueryByQueryId(int QueryId)
        {
            if (IsTracingEnabled) _stopWatch.Start();

            if (QueryId <= 0) throw new ArgumentException($"Query Id must be greater than 0 where QueryId:{QueryId}");
            try
            {
                return database.UpdateQuery(QueryId, IsDelete: true);
            }
            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage("QueryService", "RemoveQueryByQueryId(int QueryId)", exception, QueryId));
                return false;
            }
                finally
            {
                if (IsTracingEnabled)
                {
                    _stopWatch.Stop();
                    _logger.LogInformation($"Tracelog:QueryService Elapsed Time for RemoveQueryByQueryId(int QueryId) - {_stopWatch.ElapsedMilliseconds}ms");
                }
            }
        }


        //To mark the query as solved when the query is solved using QueryId.
        public bool MarkQueryAsSolved(int QueryId)
        {
            if (IsTracingEnabled) _stopWatch.Start();

            if (QueryId <= 0) throw new ArgumentException($"Query Id must be greater than 0 where QueryId:{QueryId}");
            try
            {
                return database.UpdateQuery(QueryId, IsSolved: true);
            }
            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage("QueryService", " MarkQueryAsSolved(int QueryId)", exception, QueryId));
                return false;
            }
              finally
            {
                if (IsTracingEnabled)
                {
                    _stopWatch.Stop();
                    _logger.LogInformation($"Tracelog:QueryService Elapsed Time for MarkQueryAsSolved(int QueryId) - {_stopWatch.ElapsedMilliseconds}ms");
                }
            }
        }


        //to get the query using QueryId.
        public Object GetQuery(int QueryID)
        {
            if (IsTracingEnabled) _stopWatch.Start();

            if (QueryID <= 0) throw new ArgumentException($"Query Id must be greater than 0 where QueryID:{QueryID}");
            try
            {
                var Query = database.GetQueryByID(QueryID);
                return new
                {
                    QueryId = Query.QueryId,
                    Title = Query.Title,
                    Content = Query.Content,
                    code = Query.code,
                    Date = Query.CreatedOn,
                    RaiserName = Query.User?.FullName,
                    IsSolved = Query.IsSolved,
                    Comments = GetComments(QueryID)
                };
            }
            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage("QueryService", "GetQuery(int QueryId)", exception, QueryID));
                throw;
            }
              finally
            {
                if (IsTracingEnabled)
                {
                    _stopWatch.Stop();
                    _logger.LogInformation($"Tracelog:QueryService Elapsed Time for GetQuery(int QueryId)- {_stopWatch.ElapsedMilliseconds}ms");
                }
            }
        }


        ////to fetch the list of queries inthe database.
        public IEnumerable<Object> GetListOfQueries()
        {

            if (IsTracingEnabled) _stopWatch.Start();

            try
            {
                var ListOfQueries = database.GetQueries();
                return ListOfQueries.Select(query => GetAnonymousQueryObject(query));
            }
            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage("QueryService", "GetQueries()", exception));
                throw;
            }
             finally
            {
                if (IsTracingEnabled)
                {
                    _stopWatch.Stop();
                    _logger.LogInformation($"Tracelog:QueryService Elapsed Time for GetListOfQueries() - {_stopWatch.ElapsedMilliseconds}ms");
                }
            }
        }


        //to fetch the list of latest queries using it's creation date.
        public IEnumerable<Object> GetLatestQueries(int Range)
        {
             if (IsTracingEnabled) _stopWatch.Start();

            try
            {
                //get queries from the database using Creation date by descending order.
                var ListOfQueries = database.GetQueries().OrderByDescending(query => query.CreatedOn).ToList();
                if (ListOfQueries.Count >= Range && Range != 0) ListOfQueries = ListOfQueries.GetRange(0, Range);
                return ListOfQueries.Select(Query => GetAnonymousQueryObject(Query));
            }
            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage("QueryService", "GetLatestQueries()", exception));
                throw;
            }
              finally
            {
                if (IsTracingEnabled)
                {
                    _stopWatch.Stop();
                    _logger.LogInformation($"Tracelog:QueryService Elapsed Time for GetLatestQueries(int Range) - {_stopWatch.ElapsedMilliseconds}ms");
                }
            }
        }


        //To Fetch the trending articles using number of comments and usolved query.
        public IEnumerable<Object> GetTrendingQueries(int Range)
        {
            if (IsTracingEnabled) _stopWatch.Start();

            try
            {
                //get the comments and group by QueryId using decending order in count.
                var ListOfComments = (database.GetComments().GroupBy(item => item.QueryId)).OrderByDescending(item => item.Count());
                var ListOfQueryId = (from queryComment in ListOfComments select queryComment.First().QueryId).ToList();
                var ListOfQueries = database.GetQueriesByIsSolved(false).ToList();

                if (ListOfQueryId.Count >= Range && Range != 0) ListOfQueryId = ListOfQueryId.GetRange(0, Range);
                List<Query> TrendingQueries = new List<Query>();
                foreach (var Id in ListOfQueryId)
                {
                    var Query = ListOfQueries.Find(item => item.QueryId == Id);
                    if (Query != null) TrendingQueries.Add(Query);
                }
                return (from Query in TrendingQueries select Query).Select(Query => GetAnonymousQueryObject(Query));
            }
            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage("QueryService", "GetTrendingQueries()", exception));
                throw;
            }
                 finally
            {
                if (IsTracingEnabled)
                {
                    _stopWatch.Stop();
                    _logger.LogInformation($"Tracelog:QueryService Elapsed Time for GetTrendingQueries(int Range) - {_stopWatch.ElapsedMilliseconds}ms");
                }
            }
        }


        //get the query by UserId.
        public IEnumerable<Object> GetQueriesByUserId(int UserId)
        {
            if (IsTracingEnabled) _stopWatch.Start();

            if (UserId <= 0) throw new ArgumentException($"User Id must be greater than 0 where UserId:{UserId}");
            try
            {
                var ListOfQueries = database.GetQueriesByUserId(UserId);
                return ListOfQueries.Select(Query => GetAnonymousQueryObject(Query));
            }
            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage("QueryService", "GetQueriesByUserId(int UserId)", exception, UserId));
                throw;
            }
             finally
            {
                if (IsTracingEnabled)
                {
                    _stopWatch.Stop();
                    _logger.LogInformation($"Tracelog:QueryService Elapsed Time for GetQueriesByUserId(int UserId) - {_stopWatch.ElapsedMilliseconds}ms");
                }
            }
        }


        //Fetch the query by the query title.
        public IEnumerable<Object> GetQueriesByTitle(String Title)
        {
            if (IsTracingEnabled) _stopWatch.Start();

            if (String.IsNullOrEmpty(Title)) throw new ArgumentException(" Title value can't be null");
            try
            {
                var ListOfQueries = database.GetQueriesByTitle(Title);
                return ListOfQueries.Select(Query => GetAnonymousQueryObject(Query));
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
                    _logger.LogInformation($"Tracelog:QueryService Elapsed Time for GetQueriesByTitle(String Title) - {_stopWatch.ElapsedMilliseconds}ms");
                }
            }
        }


        //to fetch the query when the query is solved.
        public IEnumerable<Object> GetQueriesByIsSolved(bool IsSolved)
        {
            if (IsTracingEnabled) _stopWatch.Start();

            try
            {
                var ListOfQueries = database.GetQueriesByIsSolved(IsSolved);
                return ListOfQueries.Select(Query => GetAnonymousQueryObject(Query));
            }
            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage("QueryService", "GetQueriesByIsSolved(bool IsSolved)", exception, IsSolved));
                throw;
            }
                      finally
            {
                if (IsTracingEnabled)
                {
                    _stopWatch.Stop();
                    _logger.LogInformation($"Tracelog:QueryService Elapsed Time for GetQueriesByIsSolved(bool IsSolved) - {_stopWatch.ElapsedMilliseconds}ms");
                }
            }
        }

        //Gets the total cout of queries from the database.
        public object GetCountOfQueries()
        {
            if (IsTracingEnabled) _stopWatch.Start();

            try
            {

                return database.GetCountOfQueries();
            }
            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage("Queryservice", "GetCountOfQueries()", exception));
                throw;
            }
                        finally
            {
                if (IsTracingEnabled)
                {
                    _stopWatch.Stop();
                    _logger.LogInformation($"Tracelog:QueryService Elapsed Time for GetCountOfQueries() - {_stopWatch.ElapsedMilliseconds}ms");
                }
            }

        }

        //Add an comment for the particular query.
        public bool CreateComment(QueryComment comment)
        {
            if (IsTracingEnabled) _stopWatch.Start();

            Validation.ValidateComment(comment);
            try
            {
                comment.CreatedOn = DateTime.UtcNow;
                comment.Datetime = DateTime.UtcNow;
                comment.UpdatedBy=null;
            
                return database.AddComment(comment);
            }
            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage("QueryService", "CreateComment(QueryComment comment)", exception, comment));
                return false;
            }
                       finally
            {
                if (IsTracingEnabled)
                {
                    _stopWatch.Stop();
                    _logger.LogInformation($"Tracelog:QueryService Elapsed Time for CreateComment(QueryComment comment) - {_stopWatch.ElapsedMilliseconds}ms");
                }
            }
        }


        //get the comments for the query using QueryId.
        public IEnumerable<Object> GetComments(int QueryId)
        {
            if (IsTracingEnabled) _stopWatch.Start();
            if (QueryId <= 0) throw new ArgumentException($"Query Id must be greater than 0 where QueryId:{QueryId}");
            try
            {
                var ListOfComments = database.GetComments().Where(comment => comment.QueryId == QueryId);
                return ListOfComments.Select(item => new
                {
                    CommentId = item.QueryCommentId,
                    Message = item.Comment,
                    Name = item.User?.FullName,
                    QueryId = item.QueryId
                });
            }
            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage("QueryService", "GetComments(int QueryId)", exception, QueryId));
                throw;
            }
               finally
            {
                if (IsTracingEnabled)
                {
                    _stopWatch.Stop();
                    _logger.LogInformation($"Tracelog:QueryService Elapsed Time for GetComments(int QueryId) - {_stopWatch.ElapsedMilliseconds}ms");
                }
            }
        }


        //to change the status of the query if it is reported as spam using the QueryId and VerifyStatusID.
        public bool ChangeSpamStatus(int QueryId, int VerifyStatusID,int UpdatedByUserId)
        {
            if (IsTracingEnabled) _stopWatch.Start();

            if (QueryId <= 0) throw new ArgumentException($"QueryId  must be greater than 0  where QueryId:{QueryId}");
            //VerifyStatusID should be inbetween 0 and 3, where 1->Approved, 2->Rejected, 3->To be Reviewed.
            if (VerifyStatusID <= 0 || VerifyStatusID > 3) throw new ArgumentException($"VerifyStatusId must be greater than 0  and less than 3 where VerifyStatusID:{VerifyStatusID}");
            try
            {
                var IsChangeSuccessfully = database.UpdateSpam(QueryId, VerifyStatusID,UpdatedByUserId);
                if (IsChangeSuccessfully) _mailService?.SendEmailAsync(HelperService.SpamMail("Manimaran.0610@gmail.com", "Title", "Hello", 2));
                return IsChangeSuccessfully;
            }
            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage($"QueryService", "ChangeSpamStatus(int QueryId, int VerifyStatusID)", exception, QueryId, VerifyStatusID));
                throw;
            }
               finally
            {
                if (IsTracingEnabled)
                {
                    _stopWatch.Stop();
                    _logger.LogInformation($"Tracelog:QueryService Elapsed Time for ChangeSpamStatus(int QueryId, int VerifyStatusID) - {_stopWatch.ElapsedMilliseconds}ms");
                }
            }
        }


        //to add the query as spam using spam object.
        public bool AddSpam(Spam spam)
        {
            if (IsTracingEnabled) _stopWatch.Start();

            Validation.ValidateSpam(spam);
            try
            {
                spam.CreatedBy=spam.UserId;
                spam.CreatedOn=DateTime.UtcNow;
                spam.UpdatedBy=null;
                return database.AddSpam(spam);
            }
            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage("QueryService", "AddSpam(Spam spam)", exception, spam));
                return false;
            }
               finally
            {
                if (IsTracingEnabled)
                {
                    _stopWatch.Stop();
                    _logger.LogInformation($"Tracelog:QueryService Elapsed Time for AddSpam(Spam spam) - {_stopWatch.ElapsedMilliseconds}ms");
                }
            }
        }


        //to get the spam queries using  VerifyStatusID.
        public IEnumerable<object> GetSpams(int VerifyStatusID)
        {
            if (IsTracingEnabled) _stopWatch.Start();

            if (VerifyStatusID <= 0 || VerifyStatusID > 3) throw new ArgumentException("Verfiystatus Id must be greater than 0 and less than or eeeeeeequal to 4");
            try
            {
                var Spams = database.GetSpams().Where(item => item.VerifyStatusID == VerifyStatusID).GroupBy(item => item.QueryId).OrderByDescending(item => item.Count()).Select(item => new
                {
                    ListOfSpams = item.Select(spam => new
                    {
                        Name = spam.User?.FullName,
                        Reason = spam.Reason
                    }),
                    Query = new
                    {
                        QueryId = item.First().QueryId,
                        Content = item.First().Query?.Content,
                        Title = item.First().Query?.Title
                    }
                });
                return Spams;
            }
            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage("QueryService", "GetSpams(int VerifyStatusID)", exception));
                throw;
            }
              finally
            {
                if (IsTracingEnabled)
                {
                    _stopWatch.Stop();
                    _logger.LogInformation($"Tracelog:QueryService Elapsed Time for GetSpams(int VerifyStatusID) - {_stopWatch.ElapsedMilliseconds}ms");
                }
            }
        }
          //Returns anonymous object for the Query object 
        private object GetAnonymousQueryObject(Query query)
        {
            return new
            {
                QueryId = query.QueryId,
                Title = query.Title,
                content = query.Content,
                IsSolved = query.IsSolved
            };
        }
    }
}
