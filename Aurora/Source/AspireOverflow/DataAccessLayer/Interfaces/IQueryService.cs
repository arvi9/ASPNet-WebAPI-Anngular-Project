using System.Net;
using AspireOverflow.Models;
namespace AspireOverflow.DataAccessLayer.Interfaces
{

    public interface IQueryService : IQueryCommentService , IQuerySpamService
    {
         bool CreateQuery(Query query);
         bool RemoveQueryByQueryID(int QueryID);
        

         bool MarkQueryAsSolved(int QueryId);

         Query GetQuery(int QueryID);
         

         IEnumerable<Query> GetQueries();
         IEnumerable<Query> GetQueriesByUserID(int UserID);
         IEnumerable<Query> GetQueriesByTitle(String Title);
         IEnumerable<Query> GetQueries(bool IsSolved);



    }

    public interface IQueryCommentService{
        bool CreateComment(QueryComment comment);
        IEnumerable<QueryComment> GetComments(int QueryId);

    }

    public interface IQuerySpamService{
        bool AddSpam(Spam spam);
        IEnumerable<object> GetSpams(int VerifyStatusID);
        bool ChangeSpamStatus(int SpamId, int VerifyStatusID);

    }

}