public class Query
{
    
}
public class QueryComment
{

}
public class QueryServices
{
    public bool CreateQuery(Query query);

    public bool RemoveQuery(int QueryID);

    public List<Query> GetQueries();

    public Query GetQuery(int QueryID);

    public List<Query> GetQueries(int UserID);

    public List<Query> GetQueries(string Title);

    public List<Query> GetQueriesByCode(string Code);

    public List<Query> GetQueriesByContent(string Contents);

    public List<Query> GetQueries(bool IsSolved);



    public bool AddCommentToQuery(int UserID,int QueryID);

    public List<QueryComment> GetComments(int QueryId);

    public bool RaiseSpam(int QueryID,int RaiserUserId);

    public bool MarkQueryAsSolved(int QueryId);



}