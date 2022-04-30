using System.Net;
using AspireOverflow.Models;
namespace AspireOverflow.DataAccessLayer.Interfaces
{
   
     public interface IQueryService
    {
        public bool AddQuery(Query query,Enum DevelopmentTeam);
        public IEnumerable<Query> GetQueries(Enum TeamName);
        public IEnumerable<Query> GetQueriesByUserID(int UserID,Enum DevelopmentTeam);
        public IEnumerable<Query> GetQueriesByTitle(String Title,Enum DevelopmentTeam);
        public IEnumerable<Query> GetQueries(bool IsSolved,Enum DevelopmentTeam);
        public bool AddCommentToQuery(QueryComment comment,Enum DevelopmentTeam);
        public IEnumerable<QueryComment> GetComments(int QueryId,Enum DevelopmentTeam);

    }
    
}