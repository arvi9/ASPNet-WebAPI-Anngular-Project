using System.Data;
using System.Reflection.PortableExecutable;
using System.Net.Mail;
using System.ComponentModel.DataAnnotations;
using AspireOverflow.DataAccessLayer;
using AspireOverflow.Models;
using AspireOverflow.CustomExceptions;
using Microsoft.EntityFrameworkCore;

using AspireOverflow.DataAccessLayer.Interfaces;




namespace AspireOverflow.Services
{


    public class QueryService :IQueryService
    {
        private static IQueryRepository database;

        private static ILogger<QueryService> _logger;

        private MailService _mailService;

        public QueryService(ILogger<QueryService> logger, MailService mailService,QueryRepository _queryRepository)
        {
            _logger = logger;
            _mailService = mailService;
            database =_queryRepository;

        }


        public bool CreateQuery(Query query)
        {
            Validation.ValidateQuery(query);
            try
            {
                query.CreatedOn = DateTime.Now;
                var IsAddedSuccessfully = database.AddQuery(query);
                if (IsAddedSuccessfully) _mailService?.SendEmailAsync(HelperService.QueryMail("Manimaran.0610@gmail.com", query.Title, "Query Created Successfully"));
                return IsAddedSuccessfully;
            }
            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage("QueryService", "CreateQuery(Query query)", exception, query));
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
                _logger.LogError(HelperService.LoggerMessage("QueryService", "RemoveQueryByQueryId(int QueryId)", exception, QueryId));

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
                _logger.LogError(HelperService.LoggerMessage("QueryService", " MarkQueryAsSolved(int QueryId)", exception, QueryId));

                return false;
            }
        }

        public Object GetQuery(int QueryId)
        {
            if (QueryId <= 0) throw new ArgumentException($"Query Id must be greater than 0 where QueryId:{QueryId}");
            try
            {
                var Query = database.GetQueryByID(QueryId);
                return new
                {
                    QueryId = Query.QueryId,
                    Title = Query.Title,
                    Content = Query.Content,
                    code = Query.code,
                    Date = Query.CreatedOn,
                    RaiserName = Query.User.FullName,
                    Comments = GetComments(QueryId)

                };
            }
            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage("QueryService", "GetQuery(int QueryId)", exception, QueryId));
                throw exception;
            }

        }


        private IEnumerable<Query> GetQueries()
        {
            try
            {
                var ListOfQueries = database.GetQueries();
                return ListOfQueries;
            }
            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage("QueryService", "GetQueries()", exception));
                throw exception;
            }

        }

        public IEnumerable<Object> GetListOfQueries()
        {
            try
            {
                var ListOfQueries = database.GetQueries();
                return ListOfQueries.Select(item => new
                {
                    QueryId = item.QueryId,
                    Title = item.Title,
                    content = item.Content,
                    IsSolved=item.IsSolved
                });
            }
            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage("QueryService", "GetQueries()", exception));
                throw exception;
            }

        }



        public IEnumerable<Object> GetLatestQueries()
        {
            try
            {
                var ListOfQueries = GetQueries().OrderByDescending(query => query.CreatedOn);

                return ListOfQueries.Select(item => new
                {
                    QueryId = item.QueryId,
                    Title = item.Title,
                    content = item.Content,
                      IsSolved=item.IsSolved
                });
            }

            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage("QueryService", "GetLatestQueries()", exception));
                throw exception;
            }
        }



        public IEnumerable<Object> GetTrendingQueries()
        {
            try
            {

                var data = (database.GetComments().GroupBy(item => item.QueryId)).OrderByDescending(item => item.Count());

                var ListOfQueryId = (from item in data select item.First().QueryId).ToList();
                var ListOfQueries = GetQueries().Where(item => item.IsSolved == false).ToList();
                var TrendingQueries = new List<Query>();
                foreach (var id in ListOfQueryId)
                {
                    TrendingQueries.Add(ListOfQueries.Find(item => item.QueryId == id));
                }

                return TrendingQueries.Select(item => new
                {
                    QueryId = item.QueryId,
                    Title = item.Title,
                    content = item.Content,
                      IsSolved=item.IsSolved
                });
            }

            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage("QueryService", "GetTrendingQueries()", exception));
                throw exception;
            }
        }



        public IEnumerable<Object> GetQueriesByUserId(int UserId)
        {
            if (UserId <= 0) throw new ArgumentException($"User Id must be greater than 0 where UserId:{UserId}");
            try
            {
                var ListOfQueries = GetQueries().Where(query => query.CreatedBy == UserId);

                return ListOfQueries.Select(item => new
                {
                    QueryId = item.QueryId,
                    Title = item.Title,
                    content = item.Content,
                      IsSolved=item.IsSolved
                });
            }
            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage("QueryService", "GetQueriesByUserId(int UserId)", exception, UserId));
                throw exception;
            }

        }


        public IEnumerable<Object> GetQueriesByTitle(String Title)
        {
            if (String.IsNullOrEmpty(Title)) throw new ArgumentNullException("Query Title value can't be null");
            try
            {
                var ListOfQueries = GetQueries().Where(query => query.Title.Contains(Title));
                return ListOfQueries.Select(item => new
                {
                    QueryId = item.QueryId,
                    Title = item.Title,
                    content = item.Content,
                      IsSolved=item.IsSolved
                });

            }
            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage("QueryService", "GetQueriesByTitle(String Title)", exception, Title));
                throw exception;
            }
        }


        public IEnumerable<Object> GetQueries(bool IsSolved)
        {
            try
            {
                var ListOfQueries = GetQueries().Where(query => query.IsSolved == IsSolved);
                return ListOfQueries.Select(item => new
                {
                    QueryId = item.QueryId,
                    Title = item.Title,
                    content = item.Content,
                      IsSolved=item.IsSolved
                });
            }
            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage("QueryService", "GetQueries(bool IsSolved)", exception, IsSolved));
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
                _logger.LogError(HelperService.LoggerMessage("QueryService", "CreateComment(QueryComment comment)", exception, comment));
                return false;
            }
        }

        public IEnumerable<Object> GetComments(int QueryId)
        {
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
                throw exception;
            }

        }



 public bool ChangeSpamStatus(int SpamId, int VerifyStatusID)
        {
            if (SpamId <= 0) throw new ArgumentException($"Spam Id must be greater than 0  where SpamId:{SpamId}");
            if(VerifyStatusID <= 0 && VerifyStatusID > 3)throw new ArgumentException($"VerifyStatusId must be greater than 0  and less than 3 where VerifyStatusID:{VerifyStatusID}");
            try
            {
                var IsChangeSuccessfully = database.UpdateSpam(SpamId,  VerifyStatusID);
                if (IsChangeSuccessfully) _mailService?.SendEmailAsync(HelperService.SpamMail("Manimaran.0610@gmail.com","Title", "Hello" , 2));
                return IsChangeSuccessfully;
            }
            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage($"QueryService", "ChangeSpamStatus(int SpamId, int VerifyStatusID)", exception, SpamId,VerifyStatusID));
                throw exception;
            }
        }


        public bool AddSpam(Spam spam)
        {
         Validation.ValidateSpam(spam);
            try
            {
                if (database.GetSpams().ToList().Find(item => item.UserId == spam.UserId && item.QueryId == spam.QueryId) != null) throw new ArgumentException("Unable to Add spam to same Query with same UserID");
             
                return database.AddSpam(spam);
            }
            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage("QueryService", "AddSpam(int QueryId, int UserId)", exception, spam));
                return false;
            }
        }


        public IEnumerable<object> GetSpams(int VerifyStatusID)
        {
            if(VerifyStatusID <=0 && VerifyStatusID > 3) throw new ArgumentException("Verfiystatus Id must be greater than 0 and less than or eeeeeeequal to 4");
            try
            {
                var Spams = database.GetSpams().Where(item =>item.VerifyStatusID==VerifyStatusID).GroupBy(item => item.QueryId).OrderByDescending(item=>item.Count()).Select(item =>new{
                ListOfSpams =item.Select(spam =>new {
                    Name =spam.User?.FullName,
                    Reason=spam.Reason
                }),
                Query=new {
                    QueryId=item.First().QueryId,
                    Content=item.First().Query?.Content,
                    Title=item.First().Query?.Title
                }});
                return Spams;
            }
            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage("QueryService", "GetSpams()", exception));
                throw;
            }
        }
    }
}
