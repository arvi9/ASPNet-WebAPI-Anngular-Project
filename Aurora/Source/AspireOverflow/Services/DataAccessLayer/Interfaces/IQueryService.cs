using System.Net;
using AspireOverflow.Models;
namespace Services.DataAccessLayer.Interfaces
{
   
     public interface IQueryService
    {
        public HttpStatusCode AddQuery(Query query);
        public IEnumerable<Query> GetQueries();
        public IEnumerable<Query> GetQueriesByUserID(int UserID);
        public IEnumerable<Query> GetQueriesByTitle(String Title);
        public IEnumerable<Query> GetQueries(bool IsSolved);
        public HttpStatusCode AddCommentToQuery(QueryComment comment);
        public IEnumerable<QueryComment> GetComments(int QueryId);

    }
    
}