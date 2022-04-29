using System.Net;
using AspireOverflow.Models;
namespace AspireOverflow.DataAccessLayer.Interfaces
{
   
     public interface IQueryService
    {
        public bool AddQuery(Query query);
        public IEnumerable<Query> GetQueries();
        public IEnumerable<Query> GetQueriesByUserID(int UserID);
        public IEnumerable<Query> GetQueriesByTitle(String Title);
        public IEnumerable<Query> GetQueries(bool IsSolved);
        public bool AddCommentToQuery(QueryComment comment);
        public IEnumerable<QueryComment> GetComments(int QueryId);

    }
    
}