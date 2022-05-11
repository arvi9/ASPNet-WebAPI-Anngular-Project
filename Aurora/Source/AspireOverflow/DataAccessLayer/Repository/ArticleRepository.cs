 using AspireOverflow.Models;

using AspireOverflow.Services;
using AspireOverflow.DataAccessLayer.Interfaces;
using AspireOverflow.CustomExceptions;
using Microsoft.EntityFrameworkCore;

namespace AspireOverflow.DataAccessLayer
{
    public class ArticleRepository : IArticleRepository
    {
        private AspireOverflowContext _context;

        private ILogger<ArticleService> _logger;
        public ArticleRepository(AspireOverflowContext context, ILogger<ArticleService> logger)
        {
            _context = context;
            _logger = logger;

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
                _logger.LogError(HelperService.LoggerMessage("ArticleRepository", "AddArticle(Article article)", exception, article));

               return false;


            }
        }

        public bool UpdateArticle(Article article)
        {
            Validation.ValidateArticle(article);
            try
            {
                _context.Articles.Update(article);
                _context.SaveChanges();
                return true;
            }
            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage("ArticleRepository", "UpdateArticle(Article article)", exception, article));

                return false;


            }
        }

        public bool UpdateArticle(int ArticleId, int ArticleStatusID, int UpdatedByUserId)
        {

            if (ArticleId <= 0) throw new ArgumentException($"Article Id must be greater than 0 where ArticleId:{ArticleId}");
            if (ArticleStatusID <= 0 && ArticleStatusID > 4) throw new ArgumentException($"Article Status Id must be between 0 and 4 ArticleStatusID:{ArticleStatusID}");
            try
            {
                var ExistingArticle = GetArticleByID(ArticleId);
                ExistingArticle.ArticleStatusID = ArticleStatusID;
                ExistingArticle.UpdatedOn = DateTime.Now;
                ExistingArticle.UpdatedBy = UpdatedByUserId;


                _context.Articles.Update(ExistingArticle);
                _context.SaveChanges();
                return true;
            }
            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage("ArticleRepository", "UpdateArticle(int ArticleId, int ArticleStatusID, int UpdatedByUserId)", exception));
                return false;

            }
        }

        public bool DeleteArticle(int ArticleId)
        {
            if (ArticleId <= 0) throw new ArgumentException($"Article Id must be greater than 0 where ArticleId:{ArticleId}");
            try
            {
                var ExistingDraftArticle = GetArticles().Where(item => item.ArtileId == ArticleId && item.ArticleStatusID == 1).First();
                if (ExistingDraftArticle == null) throw new ItemNotFoundException($"No Matching data found with ArticleId:{ArticleId}");

                _context.Articles.Remove(ExistingDraftArticle);
                _context.SaveChanges();
                return true;

            }
            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage("ArticleRepository", "DeleteArticle(int ArticleId)", exception, ArticleId));
                 return false;

            }
        }
        public Article GetArticleByID(int ArticleId)
        {
            if (ArticleId <= 0) throw new ArgumentException($"Article Id must be greater than 0 where ArticleId:{ArticleId}");
            Article ExistingArticle;
            try
            {
                ExistingArticle = GetArticles().Where(item=>item.ArtileId==ArticleId).First();
                return ExistingArticle != null ? ExistingArticle : throw new ItemNotFoundException($"There is no matching Article data with ArticleID :{ArticleId}");
            }
            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage("ArticleRepository", "GetArticleByID(int ArticleId)", exception, ArticleId));
                throw exception;
            }
        }

        public IEnumerable<Article> GetArticles()
        {
            try
            {
                var ListOfArticle = _context.Articles.Include("ArticleStatus").Include("User").ToList();
                return ListOfArticle;

            }
            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage("ArticleRepository", "GetArticles()", exception));

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
                _logger.LogError(HelperService.LoggerMessage("ArticleRepository", "AddComment(ArticleComment comment)", exception, comment));

               return false;


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
                _logger.LogError(HelperService.LoggerMessage("ArticleRepository", "GetComments()", exception));

                throw exception;
            }
        }


        public bool AddLike(ArticleLike like)
        {
            if (like.ArticleId <= 0) throw new ArgumentException($"Article Id must be greater than 0 where ArticleId:{like.ArticleId}");
            if (like.UserId <= 0) throw new ArgumentException($"User Id must be greater than 0 where UserId:{like.UserId}");
            try
            {
                _context.ArticleLikes.Add(like);
                _context.SaveChanges();
                return true;
            }
            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage("ArticleRepository", "AddLike(ArticleLike like)", exception, like));

               return false;

            }
        }


        public IEnumerable<ArticleLike> GetLikes()
        {

            try
            {
                var ListOfArticleLikes = _context.ArticleLikes.Include("Article").ToList();
                return ListOfArticleLikes;

            }
            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage("ArticleRepository", "GetLikes()", exception));

                throw exception;
            }
        }
    }
}