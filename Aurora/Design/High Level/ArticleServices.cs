
public class Article
{
    
}
public class ArticleComment
{
    
}

public class ArticleLike
{
    
}

public class ArticleServices
{
    

    public bool CreateArticle(Article article);

    public bool UpdateArticle(Article article);

    public   bool DeleteArticle(int ArticleId,int ArticleStatusID);  

    public Article GetArticleById(int ArticleId);

    public List<Article> GetArticlesByTitle(string Title);

    public List<Article> GetArticlesByAuthor(string AuthorName);

    public List<Article>  GetArticlesByDateRange(datetime Startdate ,datetime EndDate);


    public List<Article> GetArticles(string title,string AuthorName,datetime Startdate ,datetime EndDate);


    private List<Article> GetArticlesByUserId (int UserId);

    public List<Article> GetArticlesByStatusId (int UserId,int ArticleStatusID);


    public bool AddCommentToArticle(ArticleComment comment);

    public List<ArticleComment> GetComments(int ArticleID);

    public bool AddLikeToArticle(int UserID, int ArticleID);

    public int GetLikesCount(int ArticleID);

    public bool ChangeStatus(int ArticleID,int ArticleStatusID);
}



