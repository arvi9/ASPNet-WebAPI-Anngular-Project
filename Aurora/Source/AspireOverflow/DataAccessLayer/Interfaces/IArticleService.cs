using System.Net;
using AspireOverflow.Models;
namespace AspireOverflow.DataAccessLayer.Interfaces
{

    public interface IArticleService : IArticleCommentService, IArticleLikeService
    {
     
        bool CreateArticle(Article article, List<int> SharedUsersId= default!);
        bool DeleteArticleByArticleId(int ArticleId);
        bool ChangeArticleStatus(int ArticleID, int ArticleStatusID, int UserId);

        bool UpdateArticle(Article article, int _currentUserId);


        Object GetArticleById(int ArticleId);

        IEnumerable<Object> GetTrendingArticles();
        IEnumerable<Object> GetLatestArticles();
        IEnumerable<Object> GetListOfArticles();
        IEnumerable<Object> GetPrivateArticles(int UserId);
        IEnumerable<Object> GetArticlesByTitle(string Title);
        IEnumerable<Object> GetArticlesByAuthor(string AuthorName);

        IEnumerable<Object> GetArticlesByUserId(int UserId);
     

        IEnumerable<object> GetArticlesByArticleStatusId(int ArticleStatusID);

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