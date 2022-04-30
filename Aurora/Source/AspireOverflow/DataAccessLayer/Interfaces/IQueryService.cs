using System.Net;
using AspireOverflow.Models;
namespace AspireOverflow.DataAccessLayer.Interfaces
{

    public interface IQueryService
    {
        public bool CreateQuery(Query query, Enum DevelopmentTeam);
        public bool RemoveQueryByQueryID(int QueryID,Enum DevelopmentTeam);
        public bool CreateComment(QueryComment comment, Enum DevelopmentTeam);

        public bool MarkQueryAsSolved(int QueryId,Enum DevelopmentTeam);

        public Query GetQuery(int QueryID, Enum DevelopmentTeam);
        public IEnumerable<QueryComment> GetComments(int QueryId, Enum DevelopmentTeam);

        public IEnumerable<Query> GetQueries(Enum DevelopmentTeam);
        public IEnumerable<Query> GetQueriesByUserID(int UserID, Enum DevelopmentTeam);
        public IEnumerable<Query> GetQueriesByTitle(String Title, Enum DevelopmentTeam);
        public IEnumerable<Query> GetQueries(bool IsSolved, Enum DevelopmentTeam);



    }

}