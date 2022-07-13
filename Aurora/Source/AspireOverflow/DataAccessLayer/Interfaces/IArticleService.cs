using System.Net;
using AspireOverflow.Models;
namespace AspireOverflow.DataAccessLayer.Interfaces
{
    public interface IArticleService : IArticleCommentService, IArticleLikeService
    {
        bool CreateArticle(Article article, List<int> SharedUsersId);
        bool CreateArticle(Article article);
        bool DeleteArticleByArticleId(int ArticleId);
        bool ChangeArticleStatus(int ArticleID, int ArticleStatusID, int UserId);
        bool UpdateArticle(Article article, int _currentUserId,bool IsReviewer=false,List<int> SharedUsersId=default!);
        Object GetArticleById(int ArticleId);
        IEnumerable<Object> GetTrendingArticles(int Range=0);
        IEnumerable<Object> GetLatestArticles(int Range=0);
        IEnumerable<Object> GetListOfArticles();
        IEnumerable<Object> GetPrivateArticles(int UserId);
        IEnumerable<Object> GetArticlesByTitle(string Title);
        IEnumerable<Object> GetArticlesByAuthor(string AuthorName);
        public object GetCountOfArticles();
        IEnumerable<Object> GetArticlesByUserId(int UserId);
        IEnumerable<Article> GetAll();
        IEnumerable<Object> GetArticlesByReviewerId(int ReviewerId);
        IEnumerable<object> GetArticlesByArticleStatusId(int ArticleStatusID,bool IsReviewer);
    }

    public interface IArticleCommentService
    {
        bool CreateComment(ArticleComment comment);
        IEnumerable<object> GetComments(int ArticleID);
    }

    public interface IArticleLikeService
    {
        bool AddLikeToArticle(ArticleLike Like);
        int GetLikesCount(int ArticleID);
    }
}