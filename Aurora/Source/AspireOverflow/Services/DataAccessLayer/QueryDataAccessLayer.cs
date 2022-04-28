using System.Net;
using Microsoft.EntityFrameworkCore;
using AspireOverflow.Models;
using AspireOverflow.Controllers;

namespace AspireOverflow.DataAccessLayer
{
    public interface IQueryRepository
    {

        public IEnumerable<Query> GetQueriesFromDatabase();
        public IEnumerable<QueryComment> GetCommentsFromDatabase();
        public HttpStatusCode AddQueryToDatabase(Query query);
        public HttpStatusCode AddCommentToDatabase(QueryComment comment);

    }


    public class QueryRepository : IQueryRepository
    {
        private AspireOverflowContext _context;


        public QueryRepository(AspireOverflowContext context)
        {
            _context = context;

        }

        public IEnumerable<Query> GetQueriesFromDatabase()
        {

            return _context.Queries;
        }


        public IEnumerable<QueryComment> GetCommentsFromDatabase()
        {

            return _context.QueryComments;
        }



        public HttpStatusCode AddQueryToDatabase(Query query)
        {
            if (query == null) return HttpStatusCode.NoContent;
            try
            {

                _context.Queries.Add(query);
                _context.SaveChanges();

                return HttpStatusCode.Created;
            }
            catch (OperationCanceledException exception)
            {
                // Exception logged in logger 
                QueryController._logger.LogError($"Exception Occured in AddQueryToDatabase method in QueryDatabase class.\nException : {exception.Message}");
                return HttpStatusCode.BadRequest;

            }
            catch (DbUpdateException exception)
            {
                QueryController._logger.LogError($"Exception Occured in AddQueryToDatabase method in QueryDatabase class.\nException : {exception.Message}");
                return HttpStatusCode.Conflict;

            }
            catch (Exception exception)
            {
                return HttpStatusCode.BadRequest;

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
