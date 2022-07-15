using AspireOverflow.Models;

namespace AspireOverflow.DataAccessLayer.Interfaces
{
    public interface IArticleRepository : IArticleComment, IArticleLike, IPrivateArticle
    {
        bool AddArticle(Article article);
        bool UpdateArticle(int ArticleId, int ArticleStatusID, int UpdatedByUserId);
        bool UpdateArticle(Article article);

        Article GetArticleByID(int ArticleId);
        bool DeleteArticleByArticleId(int ArticleId);
        IEnumerable<Article> GetArticles();
        public IEnumerable<Article> GetArticlesByTitle(string Title);
        public IEnumerable<Article> GetArticlesByReviewerId(int ReviewerId);
        public IEnumerable<Article> GetArticlesByAuthor(string AuthorName);
        public IEnumerable<Article> GetArticlesByUserId(int UserId);

        public IEnumerable<Article> GetArticlesByArticleStatusId(int ArticleStatusID, bool IsReviewer = false);
        public object GetCountOfArticles();
         public bool GetIsTraceEnabledFromConfiguration();
    }

    public interface IArticleComment
    {
        IEnumerable<ArticleComment> GetCommentsByArticleId(int ArticleId);
        bool AddComment(ArticleComment comment);
    }

    public interface IArticleLike
    {
        public bool AddLike(ArticleLike like);
        public IEnumerable<ArticleLike> GetLikes();
        public int GetCountOfLikes(int ArticleId);
    }
    public interface IPrivateArticle
    {
        public IEnumerable<PrivateArticleUsers> GetPrivateArticleUsersByUserId(int UserId);
        public IEnumerable<PrivateArticleUsers> GetPrivateArticleUsersByArticleId(int ArticleId);
        public bool AddPrivateArticleWithSharedUsers(Article article, List<int> SharedUsersId);
        public bool UpdatePrivateArticleWithSharedUsers(Article article, List<int> SharedUsersId);
    }

}