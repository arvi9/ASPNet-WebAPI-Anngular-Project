using AspireOverflow.Models;


namespace AspireOverflow.DataAccessLayer.Interfaces
{

    public interface IQueryRepository : IQueryCommentRepository
    {


        public bool AddQueryToDatabase(Query query);
        public bool UpdateQuery(int QueryId, bool IsSolved = false, bool IsDelete = false);
        public Query GetQuery(int QueryId);
        public IEnumerable<Query> GetQueriesFromDatabase();



    }
    public interface IQueryCommentRepository
    {

        public IEnumerable<QueryComment> GetCommentsFromDatabase();

        public bool AddCommentToDatabase(QueryComment comment);

    }
}
