using System.Net;
using AspireOverflow.Models;
namespace AspireOverflow.DataAccessLayer.Interfaces
{

    public interface IArticleService:IArticleCommentService , IArticleLikeService
    {
        bool CreateArticle(Article article);
        bool DeleteArticleByArticleId(int ArticleId);
        bool UpdateArticleStatus(int ArticleID,int ArticleStatusID);
      

        Article GetArticleById(int ArticleId);
        IEnumerable<Article> GetArticlesByTitle(string Title);
        IEnumerable<Article> GetArticlesByAuthor(string AuthorName);
        IEnumerable<Article>  GetArticlesByDateRange(DateTime Startdate ,DateTime EndDate);
       IEnumerable<Article> GetArticlesByUserId (int UserId);
       IEnumerable<Article> GetArticlesByUserIdAndArticleStatusId (int UserId,int ArticleStatusID);
      
   
    }

    public interface IArticleCommentService{
        bool CreateComment(ArticleComment comment);
        bool AddCommentToArticle(ArticleComment comment);
        IEnumerable<ArticleComment> GetComments(int ArticleID);

    }

    public interface IArticleLikeService{
        bool AddLikeToArticle(int UserID, int ArticleID);
        int GetLikesCount(int ArticleID);

    }
}