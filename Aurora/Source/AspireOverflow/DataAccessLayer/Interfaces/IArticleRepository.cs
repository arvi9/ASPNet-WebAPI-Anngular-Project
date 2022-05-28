using AspireOverflow.Models;


namespace AspireOverflow.DataAccessLayer.Interfaces
{
    public interface IArticleRepository : IArticleComment, IArticleLike
    {
        bool AddArticle(Article article);
          public bool AddPrivateArticle(Article article, List<int> SharedUsersId);
        bool UpdateArticle(int ArticleId, int ArticleStatusID,int UpdatedByUserId);
        bool UpdateArticle(Article article);
        Article GetArticleByID(int ArticleId);
   bool DeleteArticle(int ArticleId);


        IEnumerable<Article> GetArticles();
          public IEnumerable<PrivateArticle> GetPrivateArticles();
    }

    public interface IArticleComment
    {
        IEnumerable<ArticleComment> GetComments();

        bool AddComment(ArticleComment comment);
    }

    public interface IArticleLike
    {

        public bool AddLike(ArticleLike like);
        public IEnumerable<ArticleLike> GetLikes();
    }
}