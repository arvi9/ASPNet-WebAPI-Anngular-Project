using System;
using System.Data;
using System.ComponentModel.DataAnnotations;
using AspireOverflow.Models;
using AspireOverflow.CustomExceptions;
using AspireOverflow.DataAccessLayer.Interfaces;
using System.Diagnostics;
namespace AspireOverflow.Services
{
    public class ArticleService : IArticleService
    {
        private readonly IArticleRepository database;
        private readonly ILogger<ArticleService> _logger;
        private readonly MailService _mailService;
        private readonly Stopwatch _stopWatch = new Stopwatch();
        private bool IsTracingEnabled;
        public ArticleService(ILogger<ArticleService> logger, MailService mailService, IArticleRepository _articleRepository)
        {
            _logger = logger;
            _mailService = mailService;
            database = _articleRepository;
            IsTracingEnabled = database.GetIsTraceEnabledFromConfiguration();
        }

        public bool CreateArticle(Article article)
        {
            if (IsTracingEnabled) _stopWatch.Start();
            //throws Validation Exception if any validation fails.
            Validation.ValidateArticle(article);
            if (String.IsNullOrEmpty(article.ImageString)) throw new ValidationException("ImageString should not be null or Empty");
            if (article.IsPrivate) throw new ValidationException("IsPrivate should not be true");
            try
            {
                article.Image = Convert.FromBase64String(article.ImageString!);
                article.Reason = null;
                article.CreatedOn = DateTime.Now;
                return database.AddArticle(article);
            }
            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage("ArticleService", "CreateArticle(Article article)", exception, article));
                return false;
            }
            finally
            {
                if (IsTracingEnabled)
                {
                    _stopWatch.Stop();
                    _logger.LogInformation($"Tracelog:ArticleService Elapsed Time for CreateArticle(Article article) - {_stopWatch.ElapsedMilliseconds}ms");
                }
            }
        }
        //CreateArticle with SharedUsersId used to create private articles.
        public bool CreateArticle(Article article, List<int> SharedUsersId)
        {
            if (IsTracingEnabled) _stopWatch.Start();
            //throws Validation Exception if any validation fails.
            Validation.ValidateArticle(article);
            try
            {
                article.Image = Convert.FromBase64String(article.ImageString!);
                article.CreatedOn = DateTime.Now;
                //for adding articles visible only for shared users.
                return database.AddPrivateArticleWithSharedUsers(article, SharedUsersId);
            }
            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage("ArticleService", "CreateArticle(Article article, List<int> SharedUsersId)", exception, article));
                return false;
            }
            finally
            {
                if (IsTracingEnabled)
                {
                    _stopWatch.Stop();
                    _logger.LogInformation($"Tracelog:ArticleService Elapsed Time for CreateArticle(Article article, List<int> SharedUsersId) - {_stopWatch.ElapsedMilliseconds}ms");
                }
            }
        }


        //Article is Updating using article object and _currentUserId.
        //sharedUsersId is Required only for updating Private Articles.
        public bool UpdateArticle(Article article, int _currentUserId, bool IsReviewer = false, List<int>? SharedUsersId = default!)
        {
            if (IsTracingEnabled) _stopWatch.Start();
            //throws Validation Exception if any validation fails.
            Validation.ValidateArticle(article);
            try
            {
                var ExistingArticle = database.GetArticleByID(article.ArticleId);
                //throws Exception when ExistingArticle is null.
                if (ExistingArticle == null) throw new ItemNotFoundException($"Unable to Find any Article with ArticleId:{article.ArticleId}");

                //if any other user trying to update the article which is not created by them,throws exception
                if (ExistingArticle.CreatedBy != _currentUserId && !IsReviewer) throw new ValidationException("you do not have access to update this article");
                ExistingArticle.Title = article.Title;
                ExistingArticle.Content = article.Content;
                ExistingArticle.UpdatedOn = DateTime.Now;
                ExistingArticle.UpdatedBy = _currentUserId;
                ExistingArticle.ArticleStatusID = article.ArticleStatusID;
                ExistingArticle.IsPrivate = article.IsPrivate;
                ExistingArticle.Image = String.IsNullOrEmpty(article.ImageString) ? ExistingArticle.Image : System.Convert.FromBase64String(article.ImageString!);

                //Reviewer once rejected the article,Reason and Reviewer ID is updated .
                if (_currentUserId != ExistingArticle.CreatedBy && IsReviewer)
                {
                    ExistingArticle.Reason = article.Reason;
                    ExistingArticle.ReviewerId = _currentUserId;
                }
                //Returns true once successfully updated.
                return SharedUsersId == null ? database.UpdateArticle(ExistingArticle) : database.UpdatePrivateArticleWithSharedUsers(ExistingArticle, SharedUsersId);
            }
            catch (ValidationException exception)
            {
                _logger.LogError(HelperService.LoggerMessage("ArticleService", " UpdateArticle(Article article, int CurrentUser)", exception, article));
                throw;
            }
            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage("ArticleService", " UpdateArticle(Article article, int CurrentUser)", exception, article));
                return false;
            }
            finally
            {
                if (IsTracingEnabled)
                {
                    _stopWatch.Stop();
                    _logger.LogInformation($"Tracelog:ArticleService Elapsed Time for UpdateArticle(Article article, int CurrentUser) - {_stopWatch.ElapsedMilliseconds}ms");
                }
            }
        }



        //Changes the Status of the article 1->In draft 2->To be Reviewed 3->Under Review 4->Published.
        public bool ChangeArticleStatus(int ArticleID, int ArticleStatusID, int UserId)
        {
            if (IsTracingEnabled) _stopWatch.Start();
            if (ArticleID <= 0) throw new ArgumentException($"Article Id must be greater than 0 where ArticleID:{ArticleID}");
            if (ArticleStatusID <= 0 || ArticleStatusID > 4) throw new ArgumentException($"Article Status Id must be between 0 and 4 ArticleStatusID:{ArticleStatusID}");
            try
            {
                var IsAddedSuccfully = database.UpdateArticle(ArticleID, ArticleStatusID, UserId);
                //If the article status is changed successfully , mail will be sent for the required status 2 and 4.
                if (IsAddedSuccfully) _mailService?.SendEmailAsync(HelperService.ArticleMail("manimaran.0610@gmail.com", "Title", "Article Created Successfully", 2));
                return IsAddedSuccfully;
            }
            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage("ArticleService", " ChangeArticleStatus(int ArticleId, int ArticleStatusID, int UpdatedByUserId)", exception), ArticleID, ArticleStatusID);
                throw;
            }
            finally
            {
                if (IsTracingEnabled)
                {
                    _stopWatch.Stop();
                    _logger.LogInformation($"Tracelog:ArticleService Elapsed Time for ChangeArticleStatus(int ArticleID, int ArticleStatusID, int UserId) - {_stopWatch.ElapsedMilliseconds}ms");
                }
            }
        }


        //The article will be deleted using ArticleId and Draft article only will be deleted.
        public bool DeleteArticleByArticleId(int ArticleId)
        {
            if (IsTracingEnabled) _stopWatch.Start();
            if (ArticleId <= 0) throw new ArgumentException($"Article Id must be greater than 0 where ArticleId:{ArticleId}");
            try
            {
                return database.DeleteArticleByArticleId(ArticleId);
            }
            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage("ArticleService", "DeleteArticleByArticleId(int ArticleId)", exception, ArticleId));
                return false;
            }
            finally
            {
                if (IsTracingEnabled)
                {
                    _stopWatch.Stop();
                    _logger.LogInformation($"Tracelog:ArticleService Elapsed Time for DeleteArticleByArticleId(int ArticleId) - {_stopWatch.ElapsedMilliseconds}ms");
                }
            }
        }



        //To Fetch the articles using ArticleId.
        public object GetArticleById(int ArticleId, CurrentUser currentUser)
        {
            if (IsTracingEnabled) _stopWatch.Start();
            if (ArticleId <= 0) throw new ArgumentException($"Article Id must be greater than 0 where ArticleId:{ArticleId}");
            try
            {
                var article = database.GetArticleByID(ArticleId);
                //Validating the User access to get the article
                if (article.CreatedBy != currentUser.UserId && article.ArticleStatusID == 1) throw new ValidationException("You dont have access to retrieve this article");
                if (article.CreatedBy != currentUser.UserId && (article.ArticleStatusID == 2 || article.ArticleStatusID == 3) && !currentUser.IsReviewer) throw new ValidationException("You dont have access to retrieve this article");
                //it retrives sharedusers only for the private articles
                var SharedUsers = article.IsPrivate ? database.GetPrivateArticleUsersByArticleId(article.ArticleId).Select(Item => new
                {
                    UserId = Item.user?.UserId,
                    FullName = Item.user?.FullName,
                    Email = Item.user?.EmailAddress
                }) : null;
                return new
                {
                    articleId = article.ArticleId,
                    PublishedDate = article.UpdatedOn != null ? article.UpdatedOn : article.CreatedOn,
                    title = article.Title,
                    AuthorName = article.User?.FullName,
                    content = article.Content,
                    image = article.Image,
                    Likes = GetLikesCount(article.ArticleId),
                    comments = GetComments(article.ArticleId),
                    status = article.ArticleStatus?.Status,
                    ReviewerId = article.ReviewerId,
                    Reason = article.Reason,
                    IsPrivate = article.IsPrivate,
                    SharedUsers = SharedUsers
                };
            }
            catch (ValidationException exception)
            {
                _logger.LogError(HelperService.LoggerMessage("ArticleService", "GetArticleById(int ArticleId)", exception, ArticleId));
                throw;
            }
            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage("ArticleService", "GetArticleById(int ArticleId)", exception, ArticleId));
                throw;
            }
            finally
            {
                if (IsTracingEnabled)
                {
                    _stopWatch.Stop();
                    _logger.LogInformation($"Tracelog:ArticleService Elapsed Time for GetArticleById(int ArticleId) - {_stopWatch.ElapsedMilliseconds}ms");
                }
            }
        }


        //To get the Latest Articles by using published date.
        public IEnumerable<object> GetLatestArticles(int Range = 0)
        {
            if (IsTracingEnabled) _stopWatch.Start();
            try
            {
                var ListOfArticles = GetArticles().OrderByDescending(article => article.UpdatedOn).ToList();
                if (ListOfArticles.Count >= Range && Range != 0) ListOfArticles = ListOfArticles.GetRange(0, Range);
                return ListOfArticles.Select(Article => GetAnonymousArticleObject(Article));
            }
            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage("ArticleService", "GetLatestArticles()", exception));
                throw;
            }
            finally
            {
                if (IsTracingEnabled)
                {
                    _stopWatch.Stop();
                    _logger.LogInformation($"Tracelog:ArticleService Elapsed Time for GetLatestArticles(int Range) - {_stopWatch.ElapsedMilliseconds}ms");
                }
            }
        }


        //To Get the trending article based on the number of likes.
        public IEnumerable<Object> GetTrendingArticles(int Range = 0)
        {
            if (IsTracingEnabled) _stopWatch.Start();
            try
            {
                //Get number of likes and grouped based on ArticleId and sorted by Descending oreder.
                var data = (database.GetLikes().GroupBy(item => item.ArticleId)).OrderByDescending(item => item.Count());
                List<int> ListOfArticleId = (from item in data select item.First().ArticleId).ToList();
                var ListOfArticles = database.GetArticlesByArticleStatusId(4).ToList();
                if (ListOfArticleId.Count >= Range && Range != 0) ListOfArticleId = ListOfArticleId.GetRange(0, Range);
                var TrendingArticles = new List<Article>();
                foreach (var Id in ListOfArticleId)
                {
                    var Article = ListOfArticles.Find(item => item.ArticleId == Id);
                    if (Article != null) TrendingArticles.Add(Article);
                }
                return TrendingArticles.Select(Article => GetAnonymousArticleObject(Article));
            }
            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage("ArticleService", "GetTrendingArticles()", exception));
                throw;
            }
            finally
            {
                if (IsTracingEnabled)
                {
                    _stopWatch.Stop();
                    _logger.LogInformation($"Tracelog:ArticleService Elapsed Time for GetTrendingArticles(int Range) - {_stopWatch.ElapsedMilliseconds}ms");
                }
            }
        }


        //Fetching the articles using UserId.
        public IEnumerable<object> GetArticlesByUserId(int UserId)
        {
            if (IsTracingEnabled) _stopWatch.Start();
            if (UserId <= 0) throw new ArgumentException($"User Id must be greater than 0 where UserId:{UserId}");
            try
            {
                var ListOfArticles = database.GetArticlesByUserId(UserId);
                return ListOfArticles.Select(Article => GetAnonymousArticleObject(Article));
            }
            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage("ArticleService", "GetArticlesByUserId(int UserId)", exception, UserId));
                throw;
            }
            finally
            {
                if (IsTracingEnabled)
                {
                    _stopWatch.Stop();
                    _logger.LogInformation($"Tracelog:ArticleService Elapsed Time for GetArticlesByUserId(int UserId) - {_stopWatch.ElapsedMilliseconds}ms");
                }
            }
        }


        //The articles will be fetched only when the status is 4->published.
        private IEnumerable<Article> GetArticles()
        {
            if (IsTracingEnabled) _stopWatch.Start();
            try
            {
                //get the aricles only when the ArticleStatusID is 4 -> published.
                var ListOfArticles = database.GetArticlesByArticleStatusId(4);
                return ListOfArticles;
            }
            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage("ArticleService", "GetArticles()", exception));
                throw;
            }
            finally
            {
                if (IsTracingEnabled)
                {
                    _stopWatch.Stop();
                    _logger.LogInformation($"Tracelog:ArticleService Elapsed Time for GetArticles() - {_stopWatch.ElapsedMilliseconds}ms");
                }
            }
        }


        //All the article list will be fetched.
        public IEnumerable<Article> GetAll()
        {
            if (IsTracingEnabled) _stopWatch.Start();
            try
            {
                var ListOfArticles = database.GetArticles();
                return ListOfArticles;
            }
            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage("ArticleService", "GetAll()", exception));
                throw;
            }
            finally
            {
                if (IsTracingEnabled)
                {
                    _stopWatch.Stop();
                    _logger.LogInformation($"Tracelog:ArticleService Elapsed Time for  GetAll() - {_stopWatch.ElapsedMilliseconds}ms");
                }
            }
        }


        //to get a list of articles and the article should not be a private article.
        public IEnumerable<Object> GetListOfArticles()
        {
            if (IsTracingEnabled) _stopWatch.Start();
            try
            {
                //to get the articles where private is false.
                var ListOfArticles = GetArticles().Where(Item => !Item.IsPrivate);
                return ListOfArticles.Select(Article => GetAnonymousArticleObject(Article));
            }
            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage("ArticleService", "GetArticles()", exception));
                throw;
            }
            finally
            {
                if (IsTracingEnabled)
                {
                    _stopWatch.Stop();
                    _logger.LogInformation($"Tracelog:ArticleService Elapsed Time for GetListOfArticles() - {_stopWatch.ElapsedMilliseconds}ms");
                }
            }
        }


        //to fetch the article which is private using UserId.
        public IEnumerable<Object> GetPrivateArticles(int UserId)
        {
            if (IsTracingEnabled) _stopWatch.Start();
            try
            {
                var ListOfPrivateArticles = database.GetPrivateArticleUsersByUserId(UserId);
                return ListOfPrivateArticles.Select(PrivateArticle => PrivateArticle.article!);
            }
            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage("ArticleService", "GetPrivateArticles(int UserId)", exception));
                throw;
            }
            finally
            {
                if (IsTracingEnabled)
                {
                    _stopWatch.Stop();
                    _logger.LogInformation($"Tracelog:ArticleService Elapsed Time for GetPrivateArticles(int UserId) - {_stopWatch.ElapsedMilliseconds}ms");
                }
            }
        }


        //to get article by its title.
        public IEnumerable<object> GetArticlesByTitle(string Title)
        {
            if (IsTracingEnabled) _stopWatch.Start();
            if (String.IsNullOrEmpty(Title)) throw new ValidationException("Article Title cannot be null or empty");
            try
            {
                var ListOfArticles = database.GetArticlesByTitle(Title);
                return ListOfArticles.Select(Article => GetAnonymousArticleObject(Article));
            }
            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage("ArticleService", "GetArticlesByTitle(string Title)", exception, Title));
                throw;
            }
            finally
            {
                if (IsTracingEnabled)
                {
                    _stopWatch.Stop();
                    _logger.LogInformation($"Tracelog:ArticleService Elapsed Time for GetArticlesByTitle(string Title) - {_stopWatch.ElapsedMilliseconds}ms");
                }
            }
        }


        //Get the article with its user name.
        public IEnumerable<object> GetArticlesByAuthor(string AuthorName)
        {
            if (IsTracingEnabled) _stopWatch.Start();
            if (String.IsNullOrEmpty(AuthorName)) throw new ArgumentException("AuthorName value can't be null");
            try
            {
                var ListOfArticles = database.GetArticlesByAuthor(AuthorName);
                return ListOfArticles.Select(Article => GetAnonymousArticleObject(Article));
            }
            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage("ArticleService", "GetArticlesByAuthor(string AuthorName)", exception, AuthorName));
                throw;
            }
            finally
            {
                if (IsTracingEnabled)
                {
                    _stopWatch.Stop();
                    _logger.LogInformation($"Tracelog:ArticleService Elapsed Time for GetArticlesByAuthor(string AuthorName) - {_stopWatch.ElapsedMilliseconds}ms");
                }
            }
        }


        //Get the articles by the Reviewer's Id. Reviewer who reviews the article before publishing.
        public IEnumerable<object> GetArticlesByReviewerId(int ReviewerId)
        {
            if (IsTracingEnabled) _stopWatch.Start();
            if (ReviewerId <= 0) throw new ArgumentException($"ReviewerId must be greater than 0 While ReviewerId:{ReviewerId}");
            try
            {
                var ListOfArticles = database.GetArticlesByReviewerId(ReviewerId);
                return ListOfArticles.Select(Article => GetAnonymousArticleObject(Article));
            }
            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage("ArticleService", " GetArticlesByReviewerId(int ReviewerId)", exception, ReviewerId));
                throw;
            }
            finally
            {
                if (IsTracingEnabled)
                {
                    _stopWatch.Stop();
                    _logger.LogInformation($"Tracelog:ArticleService Elapsed Time for GetArticlesByReviewerId(int ReviewerId) - {_stopWatch.ElapsedMilliseconds}ms");
                }
            }
        }


        //To get the article by it's ArticleStatusId.
        public IEnumerable<object> GetArticlesByArticleStatusId(int ArticleStatusID, bool IsReviewer)
        {
            if (IsTracingEnabled) _stopWatch.Start();
            //throws exception when article status is not inbetween 0 to 4.  
            //1->In draft 2->To be Reviewed 3->Under Review 4->Published.
            if (ArticleStatusID <= 0 || ArticleStatusID > 4) throw new ArgumentException($"Article Status Id must be between 0 and 4 ArticleStatusID:{ArticleStatusID}");
            try
            {
                var ListOfArticles = database.GetArticlesByArticleStatusId(ArticleStatusID, IsReviewer);
                return ListOfArticles.Select(Article => GetAnonymousArticleObject(Article));
            }
            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage("ArticleService", "GetArticlesByArticleStatusId(int ArticleStatusID, bool IsReviewer)", exception), ArticleStatusID);
                throw;
            }
            finally
            {
                if (IsTracingEnabled)
                {
                    _stopWatch.Stop();
                    _logger.LogInformation($"Tracelog:ArticleService Elapsed Time for GetArticlesByArticleStatusId(int ArticleStatusID, bool IsReviewer) - {_stopWatch.ElapsedMilliseconds}ms");
                }
            }
        }

        //Gets the count of the article from the database.
        public object GetCountOfArticles()
        {
            if (IsTracingEnabled) _stopWatch.Start();
            try
            {
                return database.GetCountOfArticles();
            }
            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage("ArticleRepository", "GetCountOfArticles()", exception));
                throw;
            }
            finally
            {
                if (IsTracingEnabled)
                {
                    _stopWatch.Stop();
                    _logger.LogInformation($"Tracelog:ArticleService Elapsed Time for GetCountOfArticles() - {_stopWatch.ElapsedMilliseconds}ms");
                }
            }
        }

        //to add an comment under an article.
        public bool CreateComment(ArticleComment comment)
        {
            if (IsTracingEnabled) _stopWatch.Start();
            Validation.ValidateArticleComment(comment);
            try
            {
                comment.CreatedBy = comment.UserId;
                comment.CreatedOn = DateTime.Now;
                var IsCommentSuccessfully = database.AddComment(comment);
                if (IsCommentSuccessfully) _mailService?.SendEmailAsync(HelperService.CommentMail("Venkateshwaranmalai2000@gmail.com", "Title", "Article Created Successfully"));
                return IsCommentSuccessfully;
            }
            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage("ArticleService", "CreateComment(ArticleComment comment)", exception), comment);
                return false;
            }
            finally
            {
                if (IsTracingEnabled)
                {
                    _stopWatch.Stop();
                    _logger.LogInformation($"Tracelog:ArticleService Elapsed Time for CreateComment(ArticleComment comment) - {_stopWatch.ElapsedMilliseconds}ms");
                }
            }
        }


        //get the comments of a n particular article using the ArticleId.
        public IEnumerable<Object> GetComments(int ArticleID)
        {
            if (IsTracingEnabled) _stopWatch.Start();
            if (ArticleID <= 0) throw new ArgumentException($"Article Id must be greater than 0 where ArticleID:{ArticleID}");
            try
            {
                var ListOfComments = database.GetCommentsByArticleId(ArticleID);
                return ListOfComments.Select(Item => new
                {
                    CommentId = Item.ArticleCommentId,
                    Message = Item.Comment,
                    Name = Item.User?.FullName,
                    ArticleId = Item.ArticleId,
                    DateTime = Item.CreatedOn
                }).OrderByDescending(item => item.DateTime);
            }
            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage("ArticleService", "GetComments(int ArticleID)", exception, ArticleID));
                throw;
            }
            finally
            {
                if (IsTracingEnabled)
                {
                    _stopWatch.Stop();
                    _logger.LogInformation($"Tracelog:ArticleService Elapsed Time for GetComments(int ArticleID) - {_stopWatch.ElapsedMilliseconds}ms");
                }
            }
        }


        //to add a like to an article using ArticleId and UserId.
        public bool AddLikeToArticle(ArticleLike Like)
        {
            if (IsTracingEnabled) _stopWatch.Start();
            Validation.ValidateArticleLike(Like);
            try
            {
                return database.AddLike(Like);
            }
            catch (ValidationException exception)
            {
                _logger.LogError(HelperService.LoggerMessage("ArticleService", "AddLikeToArticle()", exception, Like));
                throw;
            }
            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage("ArticleService", "AddLikeToArticle(ArticleLike Like)", exception, Like));
                return false;
            }
            finally
            {
                if (IsTracingEnabled)
                {
                    _stopWatch.Stop();
                    _logger.LogInformation($"Tracelog:ArticleService Elapsed Time for AddLikeToArticle(ArticleLike Like) - {_stopWatch.ElapsedMilliseconds}ms");
                }
            }
        }


        //to fetch the number of likes for the particular article using ArticleId.
        public int GetLikesCount(int ArticleID)
        {
            if (IsTracingEnabled) _stopWatch.Start();
            if (ArticleID <= 0) throw new ArgumentException($"Article Id must be greater than 0 where ArticleID:{ArticleID}");
            try
            {
                var ArticleLikes = database.GetCountOfLikes(ArticleID);
                return ArticleLikes;
            }
            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage("ArticleService", "GetLikesCount(int ArticleID)", exception));
                throw;
            }
            finally
            {
                if (IsTracingEnabled)
                {
                    _stopWatch.Stop();
                    _logger.LogInformation($"Tracelog:ArticleService Elapsed Time for GetLikesCount(int ArticleID) - {_stopWatch.ElapsedMilliseconds}ms");
                }
            }
        }

        //Returns anonymous object for the Articles object 
        private object GetAnonymousArticleObject(Article article)
        {
            return new
            {
                ArticleId = article.ArticleId,
                title = article.Title,
                AuthorName = article.User?.FullName,
                content = article.Content,
                image = article.Image,
                date = article.UpdatedOn,
                status = article.ArticleStatus?.Status,
                ReviewerId = article.ReviewerId,
                Reason = article.Reason,
                IsPrivate = article.IsPrivate
            };
        }
    }
}