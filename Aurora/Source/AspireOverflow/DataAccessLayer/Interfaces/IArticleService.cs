using System.Net;
using AspireOverflow.Models;
namespace AspireOverflow.DataAccessLayer.Interfaces
{

    public interface IArticleService
    {
        bool CreateArticle(Article article);
        bool DeleteArticleByArticleIdAndArticleStatusID(int ArticleId,int ArticleStatusID);
        bool UpdateArticleStatus(int ArticleID,int ArticleStatusID);
        bool AddLikeToArticle(int UserID, int ArticleID);

        bool CreateComment(ArticleComment comment);
        bool AddCommentToArticle(ArticleComment comment);
        Article GetArticleById(int ArticleId);
        IEnumerable<Article> GetArticlesByTitle(string Title);
        IEnumerable<Article> GetArticlesByAuthor(string AuthorName);
        IEnumerable<Article>  GetArticlesByDateRange(DateTime Startdate ,DateTime EndDate);
       IEnumerable<Article> GetArticlesByUserId (int UserId);
       IEnumerable<Article> GetArticlesByUserIdAndArticleStatusId (int UserId,int ArticleStatusID);
       IEnumerable<ArticleComment> GetComments(int ArticleID);

       int GetLikesCount(int ArticleID);
    }
}