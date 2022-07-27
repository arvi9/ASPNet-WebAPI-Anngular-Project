using AspireOverflow.Models;
using AspireOverflow.Services;
using AspireOverflow.DataAccessLayer.Interfaces;
using AspireOverflow.CustomExceptions;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;


namespace AspireOverflow.DataAccessLayer
{
    public class ArticleRepository : IArticleRepository
    {
        private readonly AspireOverflowContext _context;
        private readonly ILogger<ArticleRepository> _logger;
        private readonly IConfiguration _configuration;
        private readonly Stopwatch _stopWatch = new Stopwatch();
        private bool _isTracingEnabled;
        public ArticleRepository(AspireOverflowContext context, ILogger<ArticleRepository> logger, IConfiguration configuration)
        {
            _context = context;
            _logger = logger;
            _configuration = configuration;
            _isTracingEnabled = GetIsTraceEnabledFromConfiguration();
        }


        //to add an article using article object.
        public bool AddArticle(Article article)
        {
            if (_isTracingEnabled) _stopWatch.Start();
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
            finally
            {
                if (_isTracingEnabled)
                {
                    _stopWatch.Stop();
                    _logger.LogInformation($"Tracelog:ArticleRepository Elapsed Time for AddArticle(Article article) - {_stopWatch.ElapsedMilliseconds}ms");
                }
            }
        }

        //to create a private article using article object and list of shared user Id.
        public bool AddPrivateArticleWithSharedUsers(Article article, List<int> SharedUsersId)
        {
            if (_isTracingEnabled) _stopWatch.Start();
            Validation.ValidateArticle(article);
            try
            {
                var entry = _context.Articles.Add(article);
                _context.SaveChanges();
                if (article.IsPrivate && SharedUsersId.Count > 0)
                {
                    SharedUsersId.ForEach(UserId => _context.PrivateArticleUsers.AddAsync(new PrivateArticleUsers(entry.Entity.ArticleId, UserId)));
                    _context.SaveChanges();
                }
                return true;
            }
            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage("ArticleRepository", "AddPrivateArticle(Article article, List<int> SharedUsersId)", exception, article));
                return false;
            }
            finally
            {
                if (_isTracingEnabled)
                {
                    _stopWatch.Stop();
                    _logger.LogInformation($"Tracelog:ArticleRepository Elapsed Time for AddPrivateArticle(Article article, List<int> SharedUsersId) - {_stopWatch.ElapsedMilliseconds}ms");
                }
            }
        }

        //to update an article using article object.
        public bool UpdateArticle(Article article)
        {
            if (_isTracingEnabled) _stopWatch.Start();
            Validation.ValidateArticle(article);
            try
            {
                if (article.ArticleStatusID > 2) throw new ValidationException("you can update only the Draft Articles");
                _context.Articles.Update(article);
                _context.SaveChanges();
                return true;
            }
            catch (ValidationException exception)
            {
                _logger.LogError(HelperService.LoggerMessage("ArticleRepository", "UpdateArticle(Article article)", exception, article));
                throw;
            }
            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage("ArticleRepository", "UpdateArticle(Article article)", exception, article));
                return false;
            }
            finally
            {
                if (_isTracingEnabled)
                {
                    _stopWatch.Stop();
                    _logger.LogInformation($"Tracelog:ArticleRepository Elapsed Time for UpdateArticle(Article article) - {_stopWatch.ElapsedMilliseconds}ms");
                }
            }
        }

        //to update an article using ArticleId , ArticleStatusID and UpdatedByUserId.
        public bool UpdateArticle(int ArticleId, int ArticleStatusID, int UpdatedByUserId)
        {
            if (_isTracingEnabled) _stopWatch.Start();
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
            finally
            {
                if (_isTracingEnabled)
                {
                    _stopWatch.Stop();
                    _logger.LogInformation($"Tracelog:ArticleRepository Elapsed Time for UpdateArticle(int ArticleId, int ArticleStatusID, int UpdatedByUserId) - {_stopWatch.ElapsedMilliseconds}ms");
                }
            }
        }

        //To Update the existing private article  with shared users
        public bool UpdatePrivateArticleWithSharedUsers(Article article, List<int> SharedUsersId)
        {
            if (_isTracingEnabled) _stopWatch.Start();
            Validation.ValidateArticle(article);
            try
            {
                UpdateArticle(article);
                //Removing existing shared users in the PrivateArticleUsers table.
                var ExistingSharedUsers = _context.PrivateArticleUsers.Where(item => item.ArticleId == article.ArticleId);
                _context.PrivateArticleUsers.RemoveRange(ExistingSharedUsers);
                //Addings Sharedusers Id to the PrivateArticleUsers table
                if (SharedUsersId.Count > 0) SharedUsersId.ForEach(UserId => _context.PrivateArticleUsers.Update(new PrivateArticleUsers(article.ArticleId, UserId)));
                _context.SaveChanges();
                return true;
            }
            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage("ArticleRepository", "UpdatePrivateArticle(Article article, List<int> SharedUsersId)", exception, article));
                return false;
            }
            finally
            {
                if (_isTracingEnabled)
                {
                    _stopWatch.Stop();
                    _logger.LogInformation($"Tracelog:ArticleRepository Elapsed Time for UpdatePrivateArticle(Article article, List<int> SharedUsersId) - {_stopWatch.ElapsedMilliseconds}ms");
                }
            }
        }

        //to delete an article using ArticleId.
        public bool DeleteArticleByArticleId(int ArticleId)
        {
            if (_isTracingEnabled) _stopWatch.Start();
            if (ArticleId <= 0) throw new ArgumentException($"Article Id must be greater than 0 where ArticleId:{ArticleId}");
            try
            {
                var ExistingDraftArticle = _context.Articles.FirstOrDefault(item => item.ArticleId == ArticleId && item.ArticleStatusID == 1);
                if (ExistingDraftArticle == null) throw new ItemNotFoundException($"No Matching data found with ArticleId:{ArticleId}");
                _context.Articles.Remove(ExistingDraftArticle);
                _context.SaveChanges();
                return true;
            }
            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage("ArticleRepository", "DeleteArticleByArticleId(int ArticleId)", exception, ArticleId));
                return false;
            }
            finally
            {
                if (_isTracingEnabled)
                {
                    _stopWatch.Stop();
                    _logger.LogInformation($"Tracelog:ArticleRepository Elapsed Time for DeleteArticleByArticleId(int ArticleId) - {_stopWatch.ElapsedMilliseconds}ms");
                }
            }
        }

        //to fetch the article using ArticleId.
        public Article GetArticleByID(int ArticleId)
        {
            if (_isTracingEnabled) _stopWatch.Start();
            if (ArticleId <= 0) throw new ArgumentException($"Article Id must be greater than 0 where ArticleId:{ArticleId}");
            Article? ExistingArticle;
            try
            {
                ExistingArticle = _context.Articles.Include(e => e.ArticleStatus).Include(e => e.User).FirstOrDefault(item => item.ArticleId == ArticleId);
                return ExistingArticle != null ? ExistingArticle : throw new ItemNotFoundException($"There is no matching Article data with ArticleID :{ArticleId}");
            }
            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage("ArticleRepository", "GetArticleByID(int ArticleId)", exception, ArticleId));
                throw;
            }
            finally
            {
                if (_isTracingEnabled)
                {
                    _stopWatch.Stop();
                    _logger.LogInformation($"Tracelog:ArticleRepository Elapsed Time for GetArticleByID(int ArticleId) - {_stopWatch.ElapsedMilliseconds}ms");
                }
            }
        }

        //to get the list of articles.
        public IEnumerable<Article> GetArticles()
        {
            if (_isTracingEnabled) _stopWatch.Start();
            try
            {
                var ListOfArticle = _context.Articles.Where(item => item.CreatedOn > DateTime.Now.AddMonths(-GetRange())).Include(e => e.ArticleStatus).Include(e => e.User).ToList();
                return ListOfArticle;
            }
            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage("ArticleRepository", "GetArticles()", exception));
                throw;
            }
            finally
            {
                if (_isTracingEnabled)
                {
                    _stopWatch.Stop();
                    _logger.LogInformation($"Tracelog:ArticleRepository Elapsed Time for GetArticles() - {_stopWatch.ElapsedMilliseconds}ms");
                }
            }
        }

        //get the article with the Article StatusID.
        public IEnumerable<Article> GetArticlesByArticleStatusId(int ArticleStatusID, bool IsReviewer = false)

        {
            if (_isTracingEnabled) _stopWatch.Start();
            if (ArticleStatusID <= 0 || ArticleStatusID > 4) throw new ArgumentException($"Article Status Id must be between 0 and 4 ArticleStatusID:{ArticleStatusID}");
            try
            {
                //Articles with status (to be reviewed) and (Under Review) can only be accessible only to reviewer with PrivateArticles.
                var ListOfArticle = (ArticleStatusID > 1) && IsReviewer ? _context.Articles.Where(item => item.CreatedOn > DateTime.Now.AddMonths(-GetRange()) && item.ArticleStatusID == ArticleStatusID).Include(e => e.ArticleStatus).Include(e => e.User).ToList() :
                _context.Articles.Where(item => item.CreatedOn > DateTime.Now.AddMonths(-GetRange()) && !item.IsPrivate && item.ArticleStatusID == ArticleStatusID).Include(e => e.ArticleStatus).Include(e => e.User).ToList();
                return ListOfArticle;
            }
            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage("ArticleRepository", "GetArticlesByArticleStatusId(int ArticleStatusID, bool IsReviewer)", exception));
                throw;
            }
            finally
            {
                if (_isTracingEnabled)
                {
                    _stopWatch.Stop();
                    _logger.LogInformation($"Tracelog:ArticleRepository Elapsed Time for GetArticlesByArticleStatusId(int ArticleStatusID, bool IsReviewer) - {_stopWatch.ElapsedMilliseconds}ms");
                }
            }
        }

        //Get the article by it's title.
        public IEnumerable<Article> GetArticlesByTitle(string Title)
        {
            if (_isTracingEnabled) _stopWatch.Start();
            if (String.IsNullOrEmpty(Title)) throw new ValidationException("Article Title cannot be null or empty");
            try
            {
                //rutrns the article which has the same title with article StatusId as 4 (4->Published article).
                return _context.Articles.Where(article => article.Title!.Contains(Title) && article.ArticleStatusID == 4).Include(e => e.ArticleStatus).Include(e => e.User);
            }
            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage("ArticleRepository", "GetArticlesByTitle(string Title)", exception, Title));
                throw;
            }
            finally
            {
                if (_isTracingEnabled)
                {
                    _stopWatch.Stop();
                    _logger.LogInformation($"Tracelog:ArticleRepository Elapsed Time for GetArticlesByTitle(string Title) - {_stopWatch.ElapsedMilliseconds}ms");
                }
            }
        }

        //Gets the article by it's Author Name.
        public IEnumerable<Article> GetArticlesByAuthor(string AuthorName)
        {
            if (_isTracingEnabled) _stopWatch.Start();
            if (String.IsNullOrEmpty(AuthorName)) throw new ArgumentException("AuthorName value can't be null");
            try
            {
                //returns the article which has the same author and StatusID as 4 (4->Published article).
                return _context.Articles.Where(article => article.User!.FullName.Contains(AuthorName) && article.ArticleStatusID == 4).Include(e => e.ArticleStatus).Include(e => e.User);
            }
            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage("ArticleService", "GetArticlesByAuthor(string AuthorName)", exception, AuthorName));
                throw;
            }
            finally
            {
                if (_isTracingEnabled)
                {
                    _stopWatch.Stop();
                    _logger.LogInformation($"Tracelog:ArticleRepository Elapsed Time for GetArticlesByAuthor(string AuthorName) - {_stopWatch.ElapsedMilliseconds}ms");
                }
            }
        }


        //Get the articles by the Reviewer's Id. Reviewer who reviews the article before publishing.
        public IEnumerable<Article> GetArticlesByReviewerId(int ReviewerId)
        {
            if (_isTracingEnabled) _stopWatch.Start();
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
            finally
            {
                if (_isTracingEnabled)
                {
                    _stopWatch.Stop();
                    _logger.LogInformation($"Tracelog:ArticleRepository Elapsed Time for GetArticlesByReviewerId(int ReviewerId) - {_stopWatch.ElapsedMilliseconds}ms");
                }
            }
        }

        //Get the article with it's UserId (User denotes the author who created the article).
        public IEnumerable<Article> GetArticlesByUserId(int UserId)
        {
            if (_isTracingEnabled) _stopWatch.Start();
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
            finally
            {
                if (_isTracingEnabled)
                {
                    _stopWatch.Stop();
                    _logger.LogInformation($"Tracelog:ArticleRepository Elapsed Time for GetArticlesByUserId(int UserId) - {_stopWatch.ElapsedMilliseconds}ms");
                }
            }
        }

        //to get the list of privately shared articles by UserId
        public IEnumerable<PrivateArticleUsers> GetPrivateArticleUsersByUserId(int UserId)
        {
            if (_isTracingEnabled) _stopWatch.Start();
            if (UserId <= 0) throw new ArgumentException($"User Id must be greater than 0 where UserId:{UserId}");
            try
            {
                var ListofPrivateArticles = _context.PrivateArticleUsers.Include(e => e.article).Where(item => item.UserId == UserId  && item.article!.ArticleStatusID==4);
                return ListofPrivateArticles;
            }
            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage("ArticleRepository", "GetPrivateArticlesByUserId(int UserId)", exception));
                throw;
            }
            finally
            {
                if (_isTracingEnabled)
                {
                    _stopWatch.Stop();
                    _logger.LogInformation($"Tracelog:ArticleRepository Elapsed Time for GetPrivateArticlesByUserId(int UserId) - {_stopWatch.ElapsedMilliseconds}ms");
                }
            }
        }

        //Gets the private article by the articleId (Private article = article which is shared only with certain people).
        public IEnumerable<PrivateArticleUsers> GetPrivateArticleUsersByArticleId(int ArticleId)
        {
            if (_isTracingEnabled) _stopWatch.Start();
            if (ArticleId <= 0) throw new ArgumentException($"Article Id must be greater than 0 where ArticleId:{ArticleId}");
            try
            {
                var ListofPrivateArticles = _context.PrivateArticleUsers.Where(item => item.ArticleId == ArticleId).Include(e => e.user);
                return ListofPrivateArticles;
            }
            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage("ArticleRepository", "GetPrivateArticlesByArticleId", exception));
                throw;
            }
            finally
            {
                if (_isTracingEnabled)
                {
                    _stopWatch.Stop();
                    _logger.LogInformation($"Tracelog:ArticleRepository Elapsed Time for GetPrivateArticlesByArticleId(int ArticleId) - {_stopWatch.ElapsedMilliseconds}ms");
                }
            }
        }

        //get the total count of the articles.
        public object GetCountOfArticles()
        {
            if (_isTracingEnabled) _stopWatch.Start();
            try
            {
                var ListOfCounts = new
                {
                    TotalNumberOfArticles = _context.Articles.Count(),
                    DraftArticles = _context.Articles.Count(item => item.ArticleStatusID == 1),
                    ToBeReviewedArticles = _context.Articles.Count(item => item.ArticleStatusID == 2),
                    UnderReviewArticles = _context.Articles.Count(item => item.ArticleStatusID == 3),
                    ArticlesPublished = _context.Articles.Count(item => item.ArticleStatusID == 4)
                };
                return ListOfCounts;
            }
            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage("ArticleRepository", "GetCountOfArticles()", exception));
                throw;
            }
            finally
            {
                if (_isTracingEnabled)
                {
                    _stopWatch.Stop();
                    _logger.LogInformation($"TraceLog:ArticleRepository Elapsed Time for GetCountOfArticles() - {_stopWatch.ElapsedMilliseconds}ms");
                }
            }
        }

        //to to add comments for the article.
        public bool AddComment(ArticleComment comment)
        {
            if (_isTracingEnabled) _stopWatch.Start();
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
            finally
            {
                if (_isTracingEnabled)
                {
                    _stopWatch.Stop();
                    _logger.LogInformation($"Tracelog:ArticleRepository Elapsed Time for AddComment(ArticleComment comment) - {_stopWatch.ElapsedMilliseconds}ms");
                }
            }
        }

        //to get the added comments for the article.
        public IEnumerable<ArticleComment> GetCommentsByArticleId(int ArticleId)
        {
            if (_isTracingEnabled) _stopWatch.Start();
            if (ArticleId <= 0) throw new ArgumentException($"Article Id must be greater than 0 where ArticleID:{ArticleId}");
            try
            {
                var ListOfComments = _context.ArticleComments.Where(item => item.ArticleId == ArticleId && item.CreatedOn > DateTime.Now.AddMonths(-GetRange())).Include(e => e.User).ToList();
                return ListOfComments;
            }
            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage("ArticleRepository", "GetCommentsByArticleId(int ArticleId)", exception));
                throw;
            }
            finally
            {
                if (_isTracingEnabled)
                {
                    _stopWatch.Stop();
                    _logger.LogInformation($"Tracelog:ArticleRepository Elapsed Time for GetCommentsByArticleId(int ArticleId) - {_stopWatch.ElapsedMilliseconds}ms");
                }
            }
        }

        //to add like for the article.
        public bool AddLike(ArticleLike like)
        {
            if (_isTracingEnabled) _stopWatch.Start();
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
            finally
            {
                if (_isTracingEnabled)
                {
                    _stopWatch.Stop();
                    _logger.LogInformation($"Tracelog:ArticleRepository Elapsed Time for AddLike(ArticleLike like) - {_stopWatch.ElapsedMilliseconds}ms");
                }
            }
        }

        //to get the likes for the article.
        public IEnumerable<ArticleLike> GetLikes()
        {
            if (_isTracingEnabled) _stopWatch.Start();
            try
            {
                var ListOfArticleLikes = _context.ArticleLikes.Include(e => e.Article).Where(item => item.Article!.CreatedOn > DateTime.Now.AddMonths(-GetRange()) && item.Article!.ArticleStatusID==4).ToList();
                return ListOfArticleLikes;
            }
            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage("ArticleRepository", "GetLikes()", exception));
                throw;
            }
            finally
            {
                if (_isTracingEnabled)
                {
                    _stopWatch.Stop();
                    _logger.LogInformation($"Tracelog:ArticleRepository Elapsed Time for GetLikes() - {_stopWatch.ElapsedMilliseconds}ms");
                }
            }
        }

        //get the total count of likes using ArticleId.
        public int GetCountOfLikes(int ArticleId)
        {
            if (_isTracingEnabled) _stopWatch.Start();
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
            finally
            {
                if (_isTracingEnabled)
                {
                    _stopWatch.Stop();
                    _logger.LogInformation($"TraceLog:ArticleRepository Elapsed Time for GetCountOfLikes(int ArticleId) - {_stopWatch.ElapsedMilliseconds}ms");
                }
            }
        }

        //Getting Range from Configuration for Data fetching duration
        private int GetRange()
        {
            if (_isTracingEnabled) _stopWatch.Start();
            try
            {
                var Range = _configuration["Data_Fetching_Duration:In_months"];
                return Range != null ? Convert.ToInt32(Range) : throw new Exception("Data_Fetching_Duration:In_months-> value is Invalif  in AppSettings.json ");
            }
            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage("UserRepository", "GetRange()", exception));
                throw;
            }
            finally
            {
                if (_isTracingEnabled)
                {
                    _stopWatch.Stop();
                    _logger.LogInformation($"Tracelog:ArticleRepository Elapsed Time for GetRange() - {_stopWatch.ElapsedMilliseconds}ms");
                }
            }
        }

        //Get Tracing Enabled or not from Configuration
        public bool GetIsTraceEnabledFromConfiguration()
        {
            try
            {
                var _isTracingEnabledFromConfiguration = _configuration["Tracing:IsEnabled"];
                return _isTracingEnabledFromConfiguration != null ? Convert.ToBoolean(_isTracingEnabledFromConfiguration) : false;
            }
            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage("UserRepository", "GetIsTraceEnabledFromConfiguration()", exception));
                return false;
            }
        }
    }
}

