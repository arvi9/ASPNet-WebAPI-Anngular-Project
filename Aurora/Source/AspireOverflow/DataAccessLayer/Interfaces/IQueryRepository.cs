using AspireOverflow.Models;


namespace AspireOverflow.DataAccessLayer.Interfaces
{

    public interface IQueryRepository : IQueryCommentRepository
    {


         bool AddQueryToDatabase(Query query);
         bool UpdateQuery(int QueryId, bool IsSolved = false, bool IsDelete = false);
         Query GetQuery(int QueryId);
         IEnumerable<Query> GetQueriesFromDatabase();



    }
    public interface IQueryCommentRepository
    {

         IEnumerable<QueryComment> GetCommentsFromDatabase();

         bool AddCommentToDatabase(QueryComment comment);

    }
}
