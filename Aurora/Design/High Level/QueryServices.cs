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

    public List<Query> GetQueries(int UserID);

    public List<Query> GetQueries(string Title);

    public List<Query> GetQueries(bool IsSolved);



    public bool AddCommentToQuery(QueryComment comment));

    public List<QueryComment> GetComments(int QueryId);

    public bool MarkQueryAsSolved(int QueryId);


    public bool RaiseSpam(SpamReport spam);
        
    public bool DeleteSpam(int SpamID);

    public Spam GetSpam(int SpamID);

    public List<Spam> GetSpamsByVerifyStatus(int VerifyStatusID);

    public  bool ApproveSpam(int SpamID);
    
    public bool RemoveQuery(int QueryID);

    public bool RejectSpam(int SpamID);

}