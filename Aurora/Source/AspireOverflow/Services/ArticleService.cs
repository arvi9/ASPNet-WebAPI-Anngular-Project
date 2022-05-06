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
            _logger = logger;
            database = ArticleRepositoryFactory.GetArticleRepositoryObject(logger);

        }

        public bool CreateArticle(Article article)
        {
            if (!Validation.ValidateArticle(article)) throw new ValidationException("Given data is InValid");
            try
            {
                
                Validation.SetUserDefaultPropertyValues(article);

                return database.AddArticle(article);
            }

            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage(nameof(ArticleService), nameof(CreateArticle), exception, article));
                return false;
            }
        }

        public bool AddCommentToArticle(ArticleComment comment)
        {
            Validation.ValidateArticleComment(comment);
            try
            {
                return database.AddComment(comment);
            }
            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage(nameof(ArticleService), nameof(AddCommentToArticle), exception), comment);
                return false;
            }
        }
        public bool ChangeArticleStatus(int ArticleId, int ArticleStatusID)
        {
            Validation.ValidateId(ArticleId);
            try
            {
                return database.UpdateArticle(ArticleId, ArticleStatusID);
            }
            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage(nameof(ArticleService), nameof(ChangeArticleStatus), exception), ArticleId, ArticleStatusID);

                throw exception;
            }

        }

        public Article GetArticleById(int ArticleId)
        {
            Validation.ValidateId(ArticleId);
            try
            {
                return database.GetArticleByID(ArticleId);
            }
            catch (Exception exception)
            {

                _logger.LogError(HelperService.LoggerMessage(nameof(ArticleService), nameof(GetArticleById), exception, ArticleId));

                throw exception;
            }

        }

        public IEnumerable<Article> GetLatestArticles()
        {
            try
            {
                var ListOfArticles = GetArticles().OrderByDescending(article => article.CreatedOn);
                return ListOfArticles;
            }

            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage(nameof(ArticleService), nameof(GetLatestArticles), exception));
                throw exception;
            }
        }

        public IEnumerable<Article> GetTrendingArticles()
        {
            try
            {
                var data = (database.GetLikes().GroupBy(item => item.ArticleId)).OrderByDescending(item => item.Count());

                var ListOfArticleId = (from item in data select item.First().ArticleId).ToList();
                var ListOfArticles = GetArticles().ToList();
                var TrendingArticles = new List<Article>();
                foreach (var id in ListOfArticleId)
                {
                    TrendingArticles.Add(ListOfArticles.Find(item => item.ArtileId == id));
                }
                return TrendingArticles;
            }
            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage(nameof(ArticleService), nameof(GetTrendingArticles), exception));
                throw exception;
            }
        }


        public Article GetArticleByUserId(int UserId)
        {
            Validation.ValidateId(UserId);
            try
            {
                return database.GetArticleByID(UserId);
            }
            catch (Exception exception)
            {

                _logger.LogError(HelperService.LoggerMessage(nameof(ArticleService), nameof(GetArticleById), exception, UserId));

                throw exception;
            }

        }


        public IEnumerable<Article> GetArticles()
        {

            try
            {
                var ListOfArticles = database.GetArticles();
                return ListOfArticles;
            }

            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage(nameof(ArticleService), nameof(GetArticles), exception));
                throw exception;
            }


        }


        public IEnumerable<Article> GetArticlesByTitle(string Title)
        {
            Validation.ValidateTitle(Title);
            try
            {
                return GetArticles().Where(article => article.Title.Contains(Title));
            }
            catch (Exception exception)
            {

                _logger.LogError(HelperService.LoggerMessage(nameof(ArticleService), nameof(GetArticlesByTitle), exception, Title));

                throw exception;
            }

        }



        public IEnumerable<Article> GetArticlesByAuthor(string AuthorName)
        {
            if (String.IsNullOrEmpty(AuthorName)) throw new ArgumentNullException("AuthorName value can't be null");
            try
            {
                return GetArticles().Where(article => article.User.FullName.Contains(AuthorName));
            }
            catch (Exception exception)
            {

                _logger.LogError(HelperService.LoggerMessage(nameof(ArticleService), nameof(GetArticlesByTitle), exception, AuthorName));

                throw exception;
            }

        }







        //Article Comment
        public bool CreateComment(ArticleComment comment)
        {
            Validation.ValidateArticleComment(comment);
            try
            {
                return database.AddComment(comment);
            }
            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage(nameof(ArticleService), nameof(CreateComment), exception), comment);
                return false;
            }
        }


        public IEnumerable<ArticleComment> GetComments(int ArticleId)
        {
            Validation.ValidateId(ArticleId);
            try
            {
                return database.GetComments().Where(comment => comment.ArticleId == ArticleId);

            }
            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage(nameof(ArticleService), nameof(GetComments), exception, ArticleId));
                throw exception;
            }
        }


        public bool AddLikeToArticle(int ArticleId, int UserId)
        {
            Validation.ValidateId(ArticleId, UserId);
            try
            {
                if (database.GetLikes().Where(item => item.UserId == UserId && item.ArticleId == ArticleId) != null) throw new Exception("Unable to Add like to same article with same UserID");
                var ArticleLike = new ArticleLike();
                ArticleLike.ArticleId = ArticleId;
                ArticleLike.UserId = UserId;
                return database.AddLike(ArticleLike);
            }
            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage(nameof(ArticleService), nameof(AddLikeToArticle), exception, ArticleId, UserId));
                throw;
            }
        }


        public int GetLikesCount(int ArticleId)
        {
            Validation.ValidateId(ArticleId);
            try
            {
                var ArticleLikes = database.GetLikes().Count(item => item.ArticleId == ArticleId);
                return ArticleLikes;
            }
            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage(nameof(ArticleService), nameof(GetLikesCount), exception));
                throw;
            }
        }

    }
}