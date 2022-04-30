using AspireOverflow.Models; 


namespace AspireOverflow.DataAccessLayer.Interfaces
{
  
    public interface IQueryRepository
{

    public IEnumerable<Query> GetQueriesFromDatabase();
    public IEnumerable<QueryComment> GetCommentsFromDatabase();
    public bool AddQueryToDatabase(Query query);
    public bool AddCommentToDatabase(QueryComment comment);

}
    }
