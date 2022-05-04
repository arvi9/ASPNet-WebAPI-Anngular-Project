using AspireOverflow.Models;


namespace AspireOverflow.DataAccessLayer.Interfaces
{
    public interface IArticleRepository : IArticleComment ,IArticleLike
    {
        bool AddArticle(Article article);
        bool UpdateArticle(int ArticleId, int ArticleStatusID );
        Article GetArticleByID (int ArticleId);

    

        IEnumerable<Article> GetArticles();
    }

    public interface IArticleComment
    {
        IEnumerable<ArticleComment> GetComments();

         bool AddComment(ArticleComment comment);
    }

    public interface IArticleLike
    {
        //IEnumerable<ArticleLike> GetLikes();

        // bool AddLike(ArticleLike like);

    }
}