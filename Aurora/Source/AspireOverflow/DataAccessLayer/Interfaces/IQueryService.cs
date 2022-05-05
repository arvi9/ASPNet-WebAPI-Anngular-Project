using System.Net;
using AspireOverflow.Models;
namespace AspireOverflow.DataAccessLayer.Interfaces
{

    public interface IQueryService
    {
         bool CreateQuery(Query query);
         bool RemoveQueryByQueryID(int QueryID);
         bool CreateComment(QueryComment comment);

         bool MarkQueryAsSolved(int QueryId);

         Query GetQuery(int QueryID);
         IEnumerable<QueryComment> GetComments(int QueryId);

         IEnumerable<Query> GetQueries();
         IEnumerable<Query> GetQueriesByUserID(int UserID);
         IEnumerable<Query> GetQueriesByTitle(String Title);
         IEnumerable<Query> GetQueries(bool IsSolved);



    }

}