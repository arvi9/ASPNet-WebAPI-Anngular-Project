using System.Net;
using AspireOverflow.Models;
namespace AspireOverflow.DataAccessLayer.Interfaces
{

    public interface IQueryService : IQueryCommentService, IQuerySpamService
    {
        bool CreateQuery(Query query);
        bool RemoveQueryByQueryId(int QueryId);
        bool MarkQueryAsSolved(int QueryId);
        Object GetQuery(int QueryID);

        IEnumerable<Object> GetListOfQueries();
        IEnumerable<Object> GetTrendingQueries();
        IEnumerable<Object> GetLatestQueries();
     
        IEnumerable<Object> GetQueriesByUserId(int UserId);
        IEnumerable<Object> GetQueriesByTitle(String Title);
        IEnumerable<Object> GetQueries(bool IsSolved);



    }

    public interface IQueryCommentService
    {
        bool CreateComment(QueryComment comment);
        IEnumerable<Object> GetComments(int QueryId);

    }

    public interface IQuerySpamService
    {
        bool AddSpam(Spam spam);
        IEnumerable<object> GetSpams(int VerifyStatusID);
        bool ChangeSpamStatus(int SpamId, int VerifyStatusID);

    }

}