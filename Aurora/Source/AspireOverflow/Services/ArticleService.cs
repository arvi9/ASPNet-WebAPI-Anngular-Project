using System.ComponentModel.DataAnnotations;
using AspireOverflow.DataAccessLayer;
using AspireOverflow.Models;

using AspireOverflow.DataAccessLayer.Interfaces;




namespace AspireOverflow.Services
{

    public class ArticleService
    {
        private static IArticleRepository database;

        private static ILogger<ArticleService> _logger;

         public ArticleService(ILogger<ArticleService> logger)
        {
            _logger = logger ?? throw new NullReferenceException("logger can't be null");
            database = ArticleRepositoryFactory.GetArticleRepositoryObject(logger);

        }

         public bool CreateArticle(Article article, Enum DevelopmentTeam)
        {
            if (!Validation.ValidateArticle(article)) throw new ValidationException("Given data is InValid");
            try
            {
                return database.AddArticle(article);

            }

            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage(DevelopmentTeam, nameof(CreateArticle), exception, article));
                return false;
            }
        }

         public bool AddCommentToArticle(ArticleComment comment, Enum DevelopmentTeam)
        {
            Validation.ValidateArticleComment(comment);
            try
            {
                return database.AddComment(comment);
            }
            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage(DevelopmentTeam, nameof(AddCommentToArticle), exception), comment);
                return false;
            }
        }

        public Article GetArticleById(int ArticleId, Enum DevelopmentTeam)
        {
            Validation.ValidateId(ArticleId);
            try
            {
                return database.GetArticleByID(ArticleId);
            }
            catch (Exception exception)
            {

                _logger.LogError(HelperService.LoggerMessage(DevelopmentTeam, nameof(GetArticleById), exception, ArticleId));

                throw exception;
            }

        }

    

        public bool ChangeArticleStatus(int ArticleId,int ArticleStatusID, Enum DevelopmentTeam)
        {
            Validation.ValidateId(ArticleId);
            try
            {
                return database.UpdateArticle(ArticleId,ArticleStatusID );
            }
            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage(DevelopmentTeam, nameof(ChangeArticleStatus), exception),ArticleId, ArticleStatusID );

                throw exception;
            }

        }

          public Article GetArticleByUserId(int UserId, Enum DevelopmentTeam)
        {
            Validation.ValidateId(UserId);
            try
            {
                return database.GetArticleByID(UserId);
            }
            catch (Exception exception)
            {

                _logger.LogError(HelperService.LoggerMessage(DevelopmentTeam, nameof(GetArticleById), exception, UserId));

                throw exception;
            }

        }


          public IEnumerable<Article> GetArticles(Enum DevelopmentTeam)
        {

            try
            {
                var ListOfArticles = database.GetArticles();
                return ListOfArticles;
            }

            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage(DevelopmentTeam, nameof(GetArticles), exception));
                throw exception;
            }


        }


        public IEnumerable<Article> GetArticlesByTitle(string Title, Enum DevelopmentTeam)
        {
            Validation.ValidateTitle(Title);
            try
            {
                return GetArticles(DevelopmentTeam).Where(article => article.Title.Contains(Title));
            }
            catch (Exception exception)
            {

                _logger.LogError(HelperService.LoggerMessage(DevelopmentTeam, nameof(GetArticlesByTitle), exception, Title));

                throw exception;
            }

        }



        public IEnumerable<Article> GetArticlesByAuthor(string AuthorName, Enum DevelopmentTeam)
        {
            if (String.IsNullOrEmpty(AuthorName)) throw new ArgumentNullException("AuthorName value can't be null");
            try
            {
                return GetArticles(DevelopmentTeam).Where(article => article.User.FullName.Contains(AuthorName));
            }
            catch (Exception exception)
            {

                _logger.LogError(HelperService.LoggerMessage(DevelopmentTeam, nameof(GetArticlesByTitle), exception, AuthorName));

                throw exception;
            }

        }


       



 

         public bool CreateComment(ArticleComment comment, Enum DevelopmentTeam)
        {
            Validation.ValidateArticleComment(comment);
            try
            {
                return database.AddComment(comment);
            }
            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage(DevelopmentTeam, nameof(CreateComment), exception), comment);
                return false;
            }
        }


         public IEnumerable<ArticleComment> GetComments(int ArticleId, Enum DevelopmentTeam)
        {
            Validation.ValidateId(ArticleId);
            try
            {
                return database.GetComments().Where(comment => comment.ArticleId==ArticleId);

            }
            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage(DevelopmentTeam, nameof(GetComments), exception, ArticleId));
                throw exception;
            }
        }




    }
}