
public class Article
{
    
}
public class ArticleComment
{
    
}

public class ArticleServices
{
    

    public bool CreateArticle(Article article);

    public bool UpdateArticle(Article article);

    public   bool DeleteDraftArticle(int ArticleId);  

    public Article GetArticleById(int ArticleId);

    public List<Article> GetArticlesByTitle(string Title);

    public List<Article> GetArticlesByAuthor(String AuthorName);

    public List<Article>  GetArticlesByDateRange(DateTime Startdate ,DateTime EndDate);


    public List<Articles> GetArticlesByTitleAndAuthor(String title,String AuthorName);

    public List<Article> GetArticles(String title,String AuthorName,DateTime Startdate ,DateTime EndDate);

    public List<Article> GetArticlesInDraft(int  UserId);

    public List<Article> GetArticlesToBeReviewed();

    public List<Article> GetPublishedArticle();

    public List<Article> GetArticlesByUserId (int UserId);

    public List<Article> GetArticlesByStatusId(int statusId);



    public bool AddCommentToArticle(int UserId,int ArticleId);

    public List<ArticleComment> GetComments(int ArticleId);

    public bool AddLikeToArticle(int ArticleId,int UserId);

    public int GetLikesCount(int ArticleId);

    public bool ChangeStatusInDraft(int ArticleId);

    public bool ChangeStatusToBeReviewed(int ArticleId);

    public bool ChangeStatusUnderReview(int ArticleId);

    public bool ChangeStatusPublished(int ArticleId);


  
 
}



