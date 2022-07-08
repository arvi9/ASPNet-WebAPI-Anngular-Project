using AspireOverflow.Models;
using AspireOverflow.Services;
using AspireOverflow.DataAccessLayer.Interfaces;
using AspireOverflow.CustomExceptions;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
namespace AspireOverflow.DataAccessLayer
{
    public class ArticleRepository : IArticleRepository
    {
        private readonly AspireOverflowContext _context;
        private readonly ILogger<ArticleRepository> _logger;
        public ArticleRepository(AspireOverflowContext context, ILogger<ArticleRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        //to add an article using article object.
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

        //to create a private article using article object and list of shared user Id.
        public bool AddPrivateArticle(Article article, List<int> SharedUsersId)
        {
            Validation.ValidateArticle(article);
            try
            {
                var entry = _context.Articles.Add(article);
                _context.SaveChanges();
                if (article.IsPrivate && SharedUsersId != null && SharedUsersId.Count > 0)
                {
                    SharedUsersId.ForEach(item => _context.PrivateArticles.AddAsync(new PrivateArticle(entry.Entity.ArtileId, item)));
                    _context.SaveChanges();
                }
                return true;
            }
            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage("ArticleRepository", "AddPrivateArticle(Article article, List<int> SharedUsersId)", exception, article));
                return false;
            }
        }

        //to update an article using article object.
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

        //to update an article using ArticleId , ArticleStatusID and UpdatedByUserId.
        public bool UpdateArticle(int ArticleId, int ArticleStatusID, int UpdatedByUserId)
        {
            if (ArticleId <= 0) throw new ArgumentException($"Article Id must be greater than 0 where ArticleId:{ArticleId}");
            if (ArticleStatusID <= 0 || ArticleStatusID > 4) throw new ArgumentException($"Article Status Id must be between 0 and 4 ArticleStatusID:{ArticleStatusID}");
            try
            {
                var ExistingArticle = GetArticleByID(ArticleId);
                if (ExistingArticle.CreatedBy == UpdatedByUserId && ArticleStatusID > 2) throw new ValidationException("Reviewer cannot update status of thier own articles");
                ExistingArticle.ArticleStatusID = ArticleStatusID;
                ExistingArticle.UpdatedOn = DateTime.Now;
                ExistingArticle.UpdatedBy = UpdatedByUserId;
                _context.Articles.Update(ExistingArticle);
                _context.SaveChanges();
                return true;
            }
            catch (ValidationException exception)
            {
                _logger.LogError(HelperService.LoggerMessage("ArticleRepository", "UpdateArticle(int ArticleId, int ArticleStatusID, int UpdatedByUserId)", exception));
                throw;
            }
            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage("ArticleRepository", "UpdateArticle(int ArticleId, int ArticleStatusID, int UpdatedByUserId)", exception));
                return false;
            }
        }

        //to delete an article using ArticleId.
        public bool DeleteArticle(int ArticleId)
        {
            if (ArticleId <= 0) throw new ArgumentException($"Article Id must be greater than 0 where ArticleId:{ArticleId}");
            try
            {
                var ExistingDraftArticle = _context.Articles.FirstOrDefault(item => item.ArtileId == ArticleId && item.ArticleStatusID == 1);
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

        //to fetch the article using ArticleId.
        public Article GetArticleByID(int ArticleId)
        {
            if (ArticleId <= 0) throw new ArgumentException($"Article Id must be greater than 0 where ArticleId:{ArticleId}");
            Article? ExistingArticle;
            try
            {
                ExistingArticle = _context.Articles.FirstOrDefault(item => item.ArtileId == ArticleId);
                return ExistingArticle != null ? ExistingArticle : throw new ItemNotFoundException($"There is no matching Article data with ArticleID :{ArticleId}");
            }
            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage("ArticleRepository", "GetArticleByID(int ArticleId)", exception, ArticleId));
                throw;
            }
        }

        //to get the list of articles.
        public IEnumerable<Article> GetArticles()
        {
            try
            {
                var ListOfArticle = _context.Articles.Include(e => e.ArticleStatus).Include(e => e.User).ToList();
                return ListOfArticle;
            }
            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage("ArticleRepository", "GetArticles()", exception));
                throw;
            }
        }

        //to get the list of privately shared articles 
        public IEnumerable<PrivateArticle> GetPrivateArticles()
        {
            try
            {
                var ListofPrivateArticles = _context.PrivateArticles.ToList();
                return ListofPrivateArticles;
            }
            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage("ArticleRepository", "GetPrivateArticles()", exception));
                throw;
            }
        }

        //to to add comments for the article.
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

        //to get the added comments for the article.
        public IEnumerable<ArticleComment> GetComments()
        {
            try
            {
                var ListOfComments = _context.ArticleComments.Include(e => e.User).ToList();
                return ListOfComments;
            }
            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage("ArticleRepository", "GetComments()", exception));
                throw;
            }
        }

        //to add like for the article.
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
        
        //to get the likes for the article.
        public IEnumerable<ArticleLike> GetLikes()
        {
            try
            {
                var ListOfArticleLikes = _context.ArticleLikes.Include(e => e.Article).ToList();
                return ListOfArticleLikes;
            }
            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage("ArticleRepository", "GetLikes()", exception));
                throw;
            }
        }
    }
}