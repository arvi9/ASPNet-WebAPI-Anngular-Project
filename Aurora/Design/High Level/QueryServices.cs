public class Query
{
    
}
public class QueryComment
{

}
public class SpamReport
{

}
public class QueryServices
{
    public bool CreateQuery(Query query);

    public List<Query> GetQueries();

    public Query GetQuery(int QueryID);

    public List<Query> GetQueriesByUserID(int UserID);

    public List<Query> GetQueriesByTitle(string Title);

    public List<Query> GetQueries(bool IsSolved);



    public bool AddCommentToQuery(QueryComment comment));

    public List<QueryComment> GetComments(int QueryId);

    public bool MarkQueryAsSolved(int QueryId);


    public bool RaiseSpam(SpamReport spam);

    public List<query> GetQueriesByVerifyStatus(int VerifyStatusID);

    public int GetSpamCountForQueryID(int QueryID);

    public bool DeleteSpamsByQueryID(int QueryID);
    
    public bool RemoveQueryByQueryID(int QueryID);

    public  bool ApproveSpam(int QueryID);

    public bool RejectSpam(int QueryID);

}