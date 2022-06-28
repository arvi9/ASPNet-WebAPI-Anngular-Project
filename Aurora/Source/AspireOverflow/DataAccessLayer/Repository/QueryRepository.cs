

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

        private ILogger<QueryRepository> _logger;
        public QueryRepository(AspireOverflowContext context, ILogger<QueryRepository> logger)
        {
            _context = context;
            _logger = logger ;


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

               return false;


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
                return false;

            }
        }

        //Updating query Either by marking as Solved 
        //Same method using to disable or soft delete the query
        public bool UpdateQuery(int QueryId, bool IsSolved, bool IsDelete)
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


        public Query GetQueryByID(int QueryId)
        {
             if (QueryId <= 0) throw new ArgumentException($"Query Id must be greater than 0 where QueryId:{QueryId}");
            Query? ExistingQuery;
            try
            {
                ExistingQuery = GetQueries().ToList().Find(query =>query.QueryId==QueryId);
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

        public bool AddSpam(Spam spam)
        {
           Validation.ValidateSpam(spam);
            try
            {
               if (GetSpams().ToList().Any(item => item.UserId == spam.UserId && item.QueryId == spam.QueryId)) throw new ArgumentException("Unable to Create spam to same Query with same UserID");
               
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


        public IEnumerable<Spam> GetSpams()
        {
            try
            {
                var ListOfSpams = _context.Spams.Include("Query").Include("User").ToList();
                return ListOfSpams;

            }
            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage("QueryRepository", "GetSpams()", exception));
                throw exception;
            }
        }



        public bool UpdateSpam(int QueryId, int VerifyStatusID)
        {

            if (QueryId <= 0) throw new ArgumentException($"QueryId  must be greater than 0 where QueryId:{QueryId}");
            if (VerifyStatusID <= 0 || VerifyStatusID > 3) throw new ArgumentException($"Verify Status Id must be greater than 0 where VerifyStatusId:{VerifyStatusID}");
            
            try
            {
              var ExistingSpam = GetSpams().Where(item=>item.QueryId==QueryId).ToList();
               if(ExistingSpam == null) throw new ItemNotFoundException($"There is no matching Spam data with QueryId :{QueryId}");
                 ExistingSpam.ForEach(Item =>Item.VerifyStatusID=VerifyStatusID);
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



    
    }

}