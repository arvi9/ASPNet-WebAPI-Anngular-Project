using System.Net;
using Microsoft.EntityFrameworkCore;
using AspireOverflow.Models;
using AspireOverflow.Controllers;
using AspireOverflow.Services;
using AspireOverflow.DataAccessLayer.Interfaces;

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

        public IEnumerable<Query> GetQueriesFromDatabase()
        {

            try
            {
                var ListOfQueries = _context.Queries;
                return ListOfQueries;

            }
            catch (Exception exception)
            {
                _logger.LogError($"{exception.Message},{exception.StackTrace}");
                throw exception;
            }


        }


        public IEnumerable<QueryComment> GetCommentsFromDatabase()
        {

            try
            {
                var ListOfComments = _context.QueryComments;
                return ListOfComments;

            }
            catch (Exception exception)
            {
                _logger.LogError($"{exception.Message},{exception.StackTrace}");
                throw exception;
            }
        }



        public bool AddQueryToDatabase(Query query)
        {
            if (query == null) throw new ArgumentNullException("Query object cant be null");
            try
            {

                _context.Queries.Add(query);
                _context.SaveChanges();

                return true;
            }
            catch (OperationCanceledException exception)
            {
                _logger.LogError($"{exception.Message},{exception.StackTrace}");

                return false;

            }
            catch (DbUpdateException exception)
            {
                _logger.LogError($"{exception.Message},{exception.StackTrace}");
                return false;

            }
            catch (Exception exception)
            {
                _logger.LogError($"{exception.Message},{exception.StackTrace}");
                return false;

            }

        }



        public bool AddCommentToDatabase(QueryComment comment)
        {
            if (comment == null) throw new ArgumentNullException("comment object cant be null");
            try
            {

                _context.QueryComments.Add(comment);
                _context.SaveChanges();

                return true;

            }
            catch (OperationCanceledException exception)
            {

                _logger.LogError($"{exception.Message},{exception.StackTrace}");
                return false;

            }
            catch (DbUpdateException exception)
            {

                _logger.LogError($"{exception.Message},{exception.StackTrace}");
                return false;

            }
            catch (Exception exception)
            {

                _logger.LogError($"{exception.Message},{exception.StackTrace}");
                return false;

            }

        }
    }

}
