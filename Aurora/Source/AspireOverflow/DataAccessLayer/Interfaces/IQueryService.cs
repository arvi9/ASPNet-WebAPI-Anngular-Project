using System.Net;
using AspireOverflow.Models;
namespace AspireOverflow.DataAccessLayer.Interfaces
{

    public interface IQueryService
    {
         bool CreateQuery(Query query, Enum DevelopmentTeam);
         bool RemoveQueryByQueryID(int QueryID,Enum DevelopmentTeam);
         bool CreateComment(QueryComment comment, Enum DevelopmentTeam);

         bool MarkQueryAsSolved(int QueryId,Enum DevelopmentTeam);

         Query GetQuery(int QueryID, Enum DevelopmentTeam);
         IEnumerable<QueryComment> GetComments(int QueryId, Enum DevelopmentTeam);

         IEnumerable<Query> GetQueries(Enum DevelopmentTeam);
         IEnumerable<Query> GetQueriesByUserID(int UserID, Enum DevelopmentTeam);
         IEnumerable<Query> GetQueriesByTitle(String Title, Enum DevelopmentTeam);
         IEnumerable<Query> GetQueries(bool IsSolved, Enum DevelopmentTeam);



    }

}