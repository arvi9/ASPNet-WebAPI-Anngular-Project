using AspireOverflow.Models; 
using System.Net;

namespace AspireOverflow.DataAccessLayer.Interfaces
{
  
    public interface IQueryRepository
{

    public IEnumerable<Query> GetQueriesFromDatabase();
    public IEnumerable<QueryComment> GetCommentsFromDatabase();
    public bool AddQueryToDatabase(Query query);
    public HttpStatusCode AddCommentToDatabase(QueryComment comment);

}
    }
