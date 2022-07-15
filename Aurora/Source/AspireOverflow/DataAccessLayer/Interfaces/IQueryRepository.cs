using AspireOverflow.Models;
namespace AspireOverflow.DataAccessLayer.Interfaces
{
    public interface IQueryRepository : IQueryCommentRepository, ISpamReportRepository
    {
        bool AddQuery(Query query);
        bool UpdateQuery(int QueryId, bool IsSolved = false, bool IsDelete = false);
        Query GetQueryByID(int QueryId);
        IEnumerable<Query> GetQueries();
        public object GetCountOfQueries();
        public IEnumerable<Query> GetQueriesByUserId(int UserId);
        public IEnumerable<Query> GetQueriesByTitle(String Title);
        public IEnumerable<Query> GetQueriesByIsSolved(bool IsSolved);
         public bool GetIsTraceEnabledFromConfiguration();

    }

    public interface IQueryCommentRepository
    {
        IEnumerable<QueryComment> GetComments();
        bool AddComment(QueryComment comment);
    }

    public interface ISpamReportRepository
    {
        IEnumerable<Spam> GetSpams();
        bool AddSpam(Spam spam);
        bool UpdateSpam(int QueryId, int VerifyStatusID,int UpdatedByUserId);
    }
}
