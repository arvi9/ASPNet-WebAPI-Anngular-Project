using AspireOverflow.Models;

using AspireOverflow.Services;
using AspireOverflow.DataAccessLayer.Interfaces;
using AspireOverflow.CustomExceptions;

namespace AspireOverflow.DataAccessLayer
{
    public class ArticleRepository : IArticleRepository
    {
        private AspireOverflowContext _context;

        private ILogger<ArticleService> _logger;
        public ArticleRepository(AspireOverflowContext context, ILogger<ArticleService> logger)
        {
            _context = context ;
            _logger = logger ;

        }

        public bool AddArticle(Article article)
        {
            Validation.ValidateArticle(article);
            try
            {

                _context.Articles.Add(article);
                _context.SaveChanges();

                return true;
            }
            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage(nameof(ArticleRepository), nameof(AddArticle), exception, article));

                throw exception;

            }
        }

        public bool UpdateArticle(int ArticleId, int ArticleStatusID)
        {

            Validation.ValidateId(ArticleId, ArticleStatusID);
            try
            {
                var ExistingArticle = GetArticleByID(ArticleId);
                ExistingArticle.ArticleStatusID = ArticleStatusID;


                _context.Articles.Update(ExistingArticle);
                _context.SaveChanges();
                return true;
            }
            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage(nameof(ArticleRepository), nameof(UpdateArticle), exception));
                throw exception;
            }
        }

        public Article GetArticleByID(int ArticleId)
        {
            Validation.ValidateId(ArticleId);
            Article ExistingArticle;
            try
            {
                ExistingArticle = _context.Articles.Find(ArticleId);
                return ExistingArticle != null ? ExistingArticle : throw new ItemNotFoundException($"There is no matching Article data with ArticleID :{ArticleId}");
            }
            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage(nameof(ArticleRepository), nameof(GetArticleByID), exception, ArticleId));
                throw exception;
            }
        }


        public IEnumerable<Article> GetArticles()
        {
            try
            {
                var ListOfArticle = _context.Articles.ToList();
                return ListOfArticle;

            }
            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage(nameof(ArticleRepository), nameof(GetArticles), exception));

                throw exception;
            }


        }

        public bool AddComment(ArticleComment comment)
        {
            Validation.ValidateArticleComment(comment);
            try
            {
                _context.ArticleComments.Add(comment);
                _context.SaveChanges();

                return true;
            }
            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage(nameof(ArticleRepository), nameof(AddComment), exception, comment));

                throw exception;

            }
        }



        public IEnumerable<ArticleComment> GetComments()
        {

            try
            {
                var ListOfComments = _context.ArticleComments.ToList();
                return ListOfComments;

            }
            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage(nameof(ArticleRepository), nameof(GetComments), exception));

                throw exception;
            }
        }


        public bool AddLike(ArticleLike like)
        {
            Validation.ValidateId(like.ArticleId, like.UserId);
            try
            {
                _context.ArticleLikes.Add(like);
                _context.SaveChanges();
                return true;
            }
            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage(nameof(ArticleRepository), nameof(AddLike), exception, like));

                throw exception;
            }
        }

        
        public IEnumerable<ArticleLike> GetLikes()
        {
           
            try
            {
                var ListOfArticleLikes = _context.ArticleLikes.ToList();
                return ListOfArticleLikes;

            }
            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage(nameof(ArticleRepository), nameof(GetLikes), exception));

                throw exception;
            }
        }
    }
}