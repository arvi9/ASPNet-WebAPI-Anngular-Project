using System.Net;
using Microsoft.EntityFrameworkCore;
using AspireOverflow.Models;
using AspireOverflow.Controllers;
using AspireOverflow.DataAccessLayer.Interfaces;

namespace AspireOverflow.DataAccessLayer
{
  

    public class QueryRepository :IQueryRepository
    {
        private AspireOverflowContext _context;

        // private ILogger<QueryRepository> _logger;
        public QueryRepository(AspireOverflowContext context )
        {
            _context = context;
        

        }

        public IEnumerable<Query> GetQueriesFromDatabase()
        {
            if (_context == null) throw new NullReferenceException();

            try
            {
                var ListOfQueries = _context.Queries;
                return ListOfQueries;

            }
            catch (Exception exception)
            {

                throw exception;
            }


        }


        public IEnumerable<QueryComment> GetCommentsFromDatabase()
        {

            return _context.QueryComments;
        }



        public bool AddQueryToDatabase(Query query)
        {
            if (query == null) return false;
            try
            {

                _context.Queries.Add(query);
                _context.SaveChanges();

                return false;
            }
            catch (OperationCanceledException exception)
            {
                // Exception logged in logger 
                QueryController._logger.LogError($"Exception Occured in AddQueryToDatabase method in QueryDatabase class.\nException : {exception.Message}");
                return false;

            }
            catch (DbUpdateException exception)
            {
                QueryController._logger.LogError($"Exception Occured in AddQueryToDatabase method in QueryDatabase class.\nException : {exception.Message}");
                return false;

            }
            catch (Exception exception)
            {
                return false;

            }

        }



        public HttpStatusCode AddCommentToDatabase(QueryComment comment)
        {
            if (comment == null) return HttpStatusCode.NoContent;
            try
            {

                _context.QueryComments.Add(comment);
                _context.SaveChanges();

                return HttpStatusCode.Created;

            }
            catch (OperationCanceledException exception)
            {
                // Logged exception 
                QueryController._logger.LogError($"Exception Occured in AddCommentToDatabase method in QueryDatabase class.\nException : {exception.Message}");
                return HttpStatusCode.BadRequest;

            }
            catch (DbUpdateException exception)
            {
                QueryController._logger.LogError($"Exception Occured in AddCommentToDatabase method in QueryDatabase class.\nException : {exception.Message}");
                return HttpStatusCode.Conflict;

            }
            catch (Exception exception)
            {
                return HttpStatusCode.BadRequest;

            }

        }
    }

}
