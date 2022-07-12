
using System;
using System.Data;
using System.ComponentModel.DataAnnotations;
using AspireOverflow.Models;
using AspireOverflow.CustomExceptions;
using AspireOverflow.DataAccessLayer.Interfaces;
namespace AspireOverflow.Services
{
    public class ArticleService : IArticleService
    {
        private readonly IArticleRepository database;
        private readonly ILogger<ArticleService> _logger;
        private readonly MailService _mailService;
        public ArticleService(ILogger<ArticleService> logger, MailService mailService, IArticleRepository _articleRepository)
        {
            _logger = logger;
            _mailService = mailService;
            database = _articleRepository;
        }

        public bool CreateArticle(Article article)
        {
            //throws Validation Exception if any validation fails.
            Validation.ValidateArticle(article);
            try
            {
                article.Image = Convert.FromBase64String(article.ImageString!);
                article.CreatedOn = DateTime.Now;
                return database.AddArticle(article);
            }
            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage("ArticleService", "CreateArticle(Article article)", exception, article));
                return false;
            }
        }
        //CreateArticle with SharedUsersId used to create private articles.
        public bool CreateArticle(Article article, List<int> SharedUsersId)
        {
            //throws Validation Exception if any validation fails.
            Validation.ValidateArticle(article);
            try
            {
                article.Image = Convert.FromBase64String(article.ImageString!);
                article.CreatedOn = DateTime.Now;
                //for adding articles visible only for shared users.
                return database.AddPrivateArticle(article, SharedUsersId);
            }
            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage("ArticleService", "CreateArticle(Article article, List<int> SharedUsersId)", exception, article));
                return false;
            }
        }


        //Article is Updating using article object and UpdatedByUserId.
        public bool UpdateArticle(Article article, int UpdatedByUserId)
        {
            //throws Validation Exception if any validation fails.
            Validation.ValidateArticle(article);
            try
            {
                var ExistingArticle = database.GetArticleByID(article.ArtileId);
                //throws Exception when ExistingArticle is null.
                if (ExistingArticle == null) throw new ItemNotFoundException($"Unable to Find any Article with ArticleId:{article.ArtileId}");
                if (ExistingArticle.ArticleStatusID != 1) throw new ValidationException("you can update only the Draft Articles");
                ExistingArticle.Title = article.Title;
                ExistingArticle.Content = article.Content;
                ExistingArticle.UpdatedOn = DateTime.Now;
                ExistingArticle.UpdatedBy = UpdatedByUserId;
                ExistingArticle.ArticleStatusID = article.ArticleStatusID;
                ExistingArticle.Image = System.Convert.FromBase64String(article.ImageString!);
                //Returns true once successfully updated.
                return database.UpdateArticle(ExistingArticle);
            }
            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage("ArticleService", " UpdateArticle(Article article, int CurrentUser)", exception, article));
                return false;
            }
        }


        //Changes the Status of the article 1->In draft 2->To be Reviewed 3->Under Review 4->Published.
        public bool ChangeArticleStatus(int ArticleID, int ArticleStatusID, int UserId)
        {
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
        }


        //The article will be deleted using ArticleId and Draft article only will be deleted.
        public bool DeleteArticleByArticleId(int ArticleId)
        {
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
        }



        //To Fetch the articles using ArticleId.
        public object GetArticleById(int ArticleId)
        {
            if (ArticleId <= 0) throw new ArgumentException($"Article Id must be greater than 0 where ArticleId:{ArticleId}");
            try
            {
                var article = database.GetArticleByID(ArticleId);
                return new
                {
                    articleId = article.ArtileId,
                    PublishedDate = article.UpdatedOn,
                    title = article.Title,
                    AuthorName = article.User?.FullName,
                    content = article.Content,
                    image = article.Image,
                    Likes = GetLikesCount(article.ArtileId),
                    comments = GetComments(article.ArtileId),
                    status = article.ArticleStatus?.Status,
                    ReviewerId=article.ReviewerId
                };
            }
            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage("ArticleService", "GetArticleById(int ArticleId)", exception, ArticleId));
                throw;
            }
        }


        //To get the Latest Articles by using published date.
        public IEnumerable<object> GetLatestArticles(int Range)
        {  try
            {
                var ListOfArticles = GetArticles().OrderByDescending(article => article.UpdatedOn).ToList();
             if( ListOfArticles.Count > Range && Range != 0) ListOfArticles= ListOfArticles.GetRange(0,Range);
                return ListOfArticles.Select(Article => new
                {
                    ArticleId = Article.ArtileId,
                    title = Article.Title,
                    AuthorName = Article.User?.FullName,
                    content = Article.Content,
                    image = Article.Image,
                    date = Article.UpdatedOn,
                    status = Article.ArticleStatus?.Status,
                });
            }
            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage("ArticleService", "GetLatestArticles()", exception));
                throw;
            }
        }


        //To Get the trending article based on the number of likes.
        public IEnumerable<Object> GetTrendingArticles(int Range)
        {
            try
            {
                //Get number of likes and grouped based on ArticleId and sorted by Descending oreder.
                var data = (database.GetLikes().GroupBy(item => item.ArticleId)).OrderByDescending(item => item.Count());
                List<int> ListOfArticleId = (from item in data select item.First().ArticleId).ToList();

                if(ListOfArticleId.Count > Range && Range != 0) ListOfArticleId=ListOfArticleId.GetRange(0,Range);
                var TrendingArticles = new List<Article>();
                foreach (var Id in ListOfArticleId)
                {
                    var Article = database.GetArticleByID(Id);
                    if (Article != null) TrendingArticles.Add(Article);
                }
                return TrendingArticles.Select(Article => new
                {
                    ArticleId = Article.ArtileId,
                    title = Article.Title,
                    AuthorName = Article.User?.FullName,
                    content = Article.Content,
                    image = Article.Image,
                    date = Article.UpdatedOn,
                    status = Article.ArticleStatus?.Status,
                });
            }
            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage("ArticleService", "GetTrendingArticles()", exception));
                throw;
            }
        }


        //Fetching the articles using UserId.
        public IEnumerable<object> GetArticlesByUserId(int UserId)
        {
            if (UserId <= 0) throw new ArgumentException($"User Id must be greater than 0 where UserId:{UserId}");
            try
            {
                var ListOfArticles = database.GetArticlesByUserId(UserId);
                return ListOfArticles.Select(Article => new
                {
                    ArticleId = Article.ArtileId,
                    title = Article.Title,
                    AuthorName = Article.User?.FullName,
                    content = Article.Content,
                    image = Article.Image,
                    date = Article.UpdatedOn,
                    status = Article.ArticleStatus?.Status,
                });
            }
            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage("ArticleService", "GetArticlesByUserId(int UserId)", exception, UserId));
                throw;
            }
        }


        //The articles will be fetched only when the status is 4->published.
        private IEnumerable<Article> GetArticles()
        {
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
        }


        //All the article list will be fetched.
        public IEnumerable<Article> GetAll()
        {
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
        }


        //to get a list of articles and the article should not be a private article.
        public IEnumerable<Object> GetListOfArticles()
        {
            try
            {
                //to get the articles where private is false.
                var ListOfArticles = GetArticles().Where(Item => !Item.IsPrivate);
                return ListOfArticles.Select(Article => new
                {
                    ArticleId = Article.ArtileId,
                    title = Article.Title,
                    AuthorName = Article.User?.FullName,
                    content = Article.Content,
                    image = Article.Image,
                    date = Article.UpdatedOn,
                    status = Article.ArticleStatus?.Status,
                });
            }
            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage("ArticleService", "GetArticles()", exception));
                throw;
            }
        }


        //to fetch the article which is private using UserId.
        public IEnumerable<Object> GetPrivateArticles(int UserId)
        {
            try
            {
                var ListOfPrivateArticles = database.GetPrivateArticlesByUserId(UserId);
                return ListOfPrivateArticles.Select(PrivateArticle => PrivateArticle.article!);
            }
            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage("ArticleService", "GetPrivateArticles(int UserId)", exception));
                throw;
            }
        }


        //to get article by its title.
        public IEnumerable<object> GetArticlesByTitle(string Title)
        {
            if (String.IsNullOrEmpty(Title)) throw new ValidationException("Article Title cannot be null or empty");
            try
            {
                var ListOfArticles = database.GetArticlesByTitle(Title);
                return ListOfArticles.Select(Article => new
                {
                    ArticleId = Article.ArtileId,
                    title = Article.Title,
                    AuthorName = Article.User?.FullName,
                    content = Article.Content,
                    image = Article.Image,
                    date = Article.UpdatedOn,
                    status = Article.ArticleStatus?.Status,
                });
            }
            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage("ArticleService", "GetArticlesByTitle(string Title)", exception, Title));
                throw;
            }
        }


        //Get the article with its user name.
        public IEnumerable<object> GetArticlesByAuthor(string AuthorName)
        {
            if (String.IsNullOrEmpty(AuthorName)) throw new ArgumentException("AuthorName value can't be null");
            try
            {
                var ListOfArticles = database.GetArticlesByAuthor(AuthorName);
                return ListOfArticles.Select(Article => new
                {
                    ArticleId = Article.ArtileId,
                    title = Article.Title,
                    AuthorName = Article.User?.FullName,
                    content = Article.Content,
                    image = Article.Image,
                    date = Article.UpdatedOn,
                    status = Article.ArticleStatus?.Status,
                });
            }
            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage("ArticleService", "GetArticlesByAuthor(string AuthorName)", exception, AuthorName));
                throw;
            }
        }


        //Get the articles by the Reviewer's Id. Reviewer who reviews the article before publishing.
        public IEnumerable<object> GetArticlesByReviewerId(int ReviewerId)
        {
            if (ReviewerId <= 0) throw new ArgumentException($"ReviewerId must be greater than 0 While ReviewerId:{ReviewerId}");
            try
            {
                var ListOfArticles = database.GetArticlesByReviewerId(ReviewerId);
                return ListOfArticles.Select(Article => new
                {
                    ArticleId = Article.ArtileId,
                    title = Article.Title,
                    AuthorName = Article.User?.FullName,
                    content = Article.Content,
                    image = Article.Image,
                    date = Article.UpdatedOn,
                    status = Article.ArticleStatus?.Status,
                });
            }
            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage("ArticleService", " GetArticlesByReviewerId(int ReviewerId)", exception, ReviewerId));
                throw;
            }
        }


        //To get the article by it's ArticleStatusId.
        public IEnumerable<object> GetArticlesByArticleStatusId(int ArticleStatusID,bool IsReviewer)
        {
            //throws exception when article status is not inbetween 0 to 4.  
            //1->In draft 2->To be Reviewed 3->Under Review 4->Published.
            if (ArticleStatusID <= 0 || ArticleStatusID > 4) throw new ArgumentException($"Article Status Id must be between 0 and 4 ArticleStatusID:{ArticleStatusID}");
            try
            {
                var ListOfArticles = database.GetArticlesByArticleStatusId(ArticleStatusID,IsReviewer);
                return ListOfArticles.Select(Article => new
                {
                    ArticleId = Article.ArtileId,
                    title = Article.Title,
                    AuthorName = Article.User?.FullName,
                    content = Article.Content,
                    image = Article.Image,
                    date = Article.UpdatedOn,
                    status = Article.ArticleStatus?.Status,
                    ReviewerId = Article.ReviewerId
                });
            }
            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage("ArticleService", "GetArticlesByArticleStatusId(int ArticleStatusID)", exception), ArticleStatusID);
                throw;
            }
        }

        public object GetCountOfArticles()
        {
            try
            {
                return database.GetCountOfArticles();
            }
            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage("ArticleRepository", "GetCountOfArticles()", exception));
                throw;
            }
        }

        //to add an comment under an article.
        public bool CreateComment(ArticleComment comment)
        {
            Validation.ValidateArticleComment(comment);
            try
            {
                comment.CreatedBy = comment.UserId;
                var IsCommentSuccessfully = database.AddComment(comment);
                if (IsCommentSuccessfully) _mailService?.SendEmailAsync(HelperService.CommentMail("Venkateshwaranmalai2000@gmail.com", "Title", "Article Created Successfully"));
                return IsCommentSuccessfully;
            }
            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage("ArticleService", "CreateComment()", exception), comment);
                return false;
            }
        }


        //get the comments of a n particular article using the ArticleId.
        public IEnumerable<Object> GetComments(int ArticleID)
        {
            if (ArticleID <= 0) throw new ArgumentException($"Article Id must be greater than 0 where ArticleID:{ArticleID}");
            try
            {
                var ListOfComments = database.GetCommentsByArticleId(ArticleID);
                return ListOfComments.Select(Item => new
                {
                    CommentId = Item.ArticleCommentId,
                    Message = Item.Comment,
                    Name = Item.User?.FullName,
                    ArticleId = Item.ArticleId
                });
            }
            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage("ArticleService", "GetComments()", exception, ArticleID));
                throw;
            }
        }


        //to add a like to an article using ArticleId and UserId.
        public bool AddLikeToArticle(ArticleLike Like)
        {
            Validation.ValidateArticleLike(Like);
            try
            {
                return database.AddLike(Like);
            }
            catch (ValidationException exception)
            {
                _logger.LogError(HelperService.LoggerMessage("ArticleService", "AddLikeToArticle()", exception, Like));
                return false;
            }
            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage("ArticleService", "AddLikeToArticle()", exception, Like));
                return false;
            }
        }


        //to fetch the number of likes for the particular article using ArticleId.
        public int GetLikesCount(int ArticleID)
        {
            if (ArticleID <= 0) throw new ArgumentException($"Article Id must be greater than 0 where ArticleID:{ArticleID}");
            try
            {
                var ArticleLikes = database.GetCountOfLikes(ArticleID);
                return ArticleLikes;
            }
            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage("ArticleService", "GetLikesCount()", exception));
                throw;
            }
        }
    }
}