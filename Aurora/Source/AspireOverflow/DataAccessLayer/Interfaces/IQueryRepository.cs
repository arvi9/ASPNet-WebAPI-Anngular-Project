using AspireOverflow.Models;


namespace AspireOverflow.DataAccessLayer.Interfaces
{

    public interface IQueryRepository : IQueryCommentRepository,ISpamReportRepository
    {


         bool AddQuery(Query query);
         bool UpdateQuery(int QueryId, bool IsSolved = false, bool IsDelete = false);
         Query GetQueryByID(int QueryId);
         IEnumerable<Query> GetQueries();



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

         bool UpdateSpam(int SpamId, int VerifyStatusID);

    }
}
