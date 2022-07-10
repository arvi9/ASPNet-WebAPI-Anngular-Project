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
        private readonly IConfiguration _configuration;
        public ArticleRepository(AspireOverflowContext context, ILogger<ArticleRepository> logger, IConfiguration configuration)
        {
            _context = context;
            _logger = logger;
            _configuration = configuration;
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
                if (article.IsPrivate && SharedUsersId.Count > 0)
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
                if (ArticleStatusID > 2) ExistingArticle.ReviewerId = UpdatedByUserId; // Reviewer Id has been assigned once article status Id greater than 2
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
        public bool DeleteArticleByArticleId(int ArticleId)
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
                var ListOfArticle = _context.Articles.Where(item => item.UpdatedOn > DateTime.Now.AddMonths(-GetDuration())).Include(e => e.ArticleStatus).Include(e => e.User).ToList();
                return ListOfArticle;
            }
            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage("ArticleRepository", "GetArticles()", exception));
                throw;
            }
        }

        public IEnumerable<Article> GetArticlesByArticleStatusId(int ArticleStatusID)

        {
            if (ArticleStatusID <= 0 || ArticleStatusID > 4) throw new ArgumentException($"Article Status Id must be between 0 and 4 ArticleStatusID:{ArticleStatusID}");
            try
            {
                var ListOfArticle = _context.Articles.Where(item => item.UpdatedOn > DateTime.Now.AddMonths(-GetDuration()) && !item.IsPrivate && item.ArticleStatusID == ArticleStatusID).Include(e => e.ArticleStatus).Include(e => e.User).ToList();
                return ListOfArticle;
            }
            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage("ArticleRepository", "GetArticles()", exception));
                throw;
            }
        }
        public IEnumerable<Article> GetArticlesByTitle(string Title)
        {
            if (String.IsNullOrEmpty(Title)) throw new ValidationException("Article Title cannot be null or empty");
            try
            {
                return _context.Articles.Where(article => article.Title!.Contains(Title) && article.ArticleStatusID == 1).Include(e => e.ArticleStatus).Include(e => e.User);
            }
            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage("ArticleRepository", "GetArticlesByTitle(string Title)", exception, Title));
                throw;
            }
        }
        public IEnumerable<Article> GetArticlesByAuthor(string AuthorName)
        {
            if (String.IsNullOrEmpty(AuthorName)) throw new ArgumentException("AuthorName value can't be null");
            try
            {
                return _context.Articles.Where(article => article.User!.FullName.Contains(AuthorName) && article.ArticleStatusID == 4).Include(e => e.ArticleStatus).Include(e => e.User);

            }
            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage("ArticleService", "GetArticlesByAuthor(string AuthorName)", exception, AuthorName));
                throw;
            }
        }


        //Get the articles by the Reviewer's Id. Reviewer who reviews the article before publishing.
        public IEnumerable<Article> GetArticlesByReviewerId(int ReviewerId)

        {
            if (ReviewerId <= 0) throw new ArgumentException($"ReviewerId must be greater than 0 While ReviewerId:{ReviewerId}");
            try
            {
                return _context.Articles.Where(article => article.ReviewerId == ReviewerId).Include(e => e.ArticleStatus).Include(e => e.User);

            }
            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage("ArticeRepository", " GetArticlesByReviewerId(int ReviewerId)", exception, ReviewerId));
                throw;
            }
        }

        public IEnumerable<Article> GetArticlesByUserId(int UserId)
        {
            if (UserId <= 0) throw new ArgumentException($"User Id must be greater than 0 where UserId:{UserId}");
            try
            {
                return _context.Articles.Where(item => item.CreatedBy == UserId).Include(e => e.ArticleStatus).Include(e => e.User);

            }
            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage("ArticleRepository", "GetArticlesByUserId(int UserId)", exception, UserId));
                throw;
            }
        }

        //to get the list of privately shared articles 
        public IEnumerable<PrivateArticle> GetPrivateArticlesByUserId(int UserId)
        {
            if (UserId <= 0) throw new ArgumentException($"User Id must be greater than 0 where UserId:{UserId}");
            try
            {
                var ListofPrivateArticles = _context.PrivateArticles.Where(item => item.UserId == UserId).Include(e => e.article);
                return ListofPrivateArticles;
            }
            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage("ArticleRepository", "GetPrivateArticles()", exception));
                throw;
            }
        }

        public object GetCountOfArticles()
        {
            try
            {
                return new{
                    TotalNumberOfArticles = _context.Articles.Count(),
                    DraftArticles = _context.Articles.Count(item => item.ArticleStatusID == 1),
                    ToBeReviewedArticles = _context.Articles.Count(item => item.ArticleStatusID == 1),
                    UnderReviewArticles = _context.Articles.Count(item => item.ArticleStatusID == 1),
                    ArticlesPublished = _context.Articles.Count(item => item.ArticleStatusID == 4)
                };
            }
            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage("ArticleRepository", "GetCountOfArticles()", exception));
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
        public IEnumerable<ArticleComment> GetCommentsByArticleId(int ArticleId)
        {
            if (ArticleId <= 0) throw new ArgumentException($"Article Id must be greater than 0 where ArticleID:{ArticleId}");
            try
            {
                var ListOfComments = _context.ArticleComments.Where(item => item.ArticleId == ArticleId && item.CreatedOn > DateTime.Now.AddMonths(-GetDuration())).Include(e => e.User).ToList();
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
            Validation.ValidateArticleLike(like);
            try
            {
                if (_context.ArticleLikes.Any(item => item.ArticleId == like.ArticleId && item.UserId == like.UserId)) throw new ValidationException("you cannot add more than 1 like to the same article");
                _context.ArticleLikes.Add(like);
                _context.SaveChanges();
                return true;
            }
            catch (ValidationException exception)
            {
                _logger.LogError(HelperService.LoggerMessage("ArticleRepository", "AddLike(ArticleLike like)", exception, like));
                throw;
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
        public int GetCountOfLikes(int ArticleId)
        {
            if (ArticleId <= 0) throw new ArgumentException($"Article Id must be greater than 0 where ArticleID:{ArticleId}");
            try
            {
                var CountOfArticleLikes = _context.ArticleLikes.Count(item => item.ArticleId == ArticleId);
                return CountOfArticleLikes;
            }
            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage("ArticleRepository", "GetCountOfLikes(int ArticleId)", exception));
                throw;
            }
        }
        private int GetDuration()
        {
            try
            {
                var Duration = _configuration["Data_Fetching_Duration:In_months"];
                return Duration != null ? Convert.ToInt32(Duration) : throw new Exception("Data_Fetching_Duration:In_months-> value is Invalif  in AppSettings.json ");
            }
            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage("UserRepository", " GetDuration()", exception));
                throw;

            }
        }
    }
}