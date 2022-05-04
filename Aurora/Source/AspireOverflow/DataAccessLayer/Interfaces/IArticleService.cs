using System.Net;
using AspireOverflow.Models;
namespace AspireOverflow.DataAccessLayer.Interfaces
{

    public interface IArticleService
    {
        bool CreateArticle(Article article, Enum DevelopmentTeam);
        bool DeleteArticleByArticleIdAndArticleStatusID(int ArticleId,int ArticleStatusID, Enum DevelopmentTeam);
        bool UpdateArticleStatus(int ArticleID,int ArticleStatusID,Enum DevelopmentTeam);
        //bool AddLikeToArticle(int UserID, int ArticleID,Enum DevelopmentTeam);

        bool CreateComment(ArticleComment comment, Enum DevelopmentTeam);
        bool AddCommentToArticle(ArticleComment comment, Enum DevelopmentTeam);
        Article GetArticleById(int ArticleId,Enum DevelopmentTeam);
        IEnumerable<Article> GetArticlesByTitle(string Title, Enum DevelopmentTeam);
        IEnumerable<Article> GetArticlesByAuthor(string AuthorName, Enum DevelopmentTeam);
        IEnumerable<Article>  GetArticlesByDateRange(DateTime Startdate ,DateTime EndDate, Enum DevelopmetTeam);
       IEnumerable<Article> GetArticlesByUserId (int UserId, Enum DevelopmentTeam);
       IEnumerable<Article> GetArticlesByUserIdAndArticleStatusId (int UserId,int ArticleStatusID,Enum DevelopmentTeam);
       IEnumerable<ArticleComment> GetComments(int ArticleID, Enum DevelopmentTeam);

       //int GetLikesCount(int ArticleID,Enum DevelopmentTeam);
    }
}