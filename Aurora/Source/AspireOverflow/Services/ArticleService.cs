using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Reflection.PortableExecutable;
using System.Net.Mail;
using System.ComponentModel.DataAnnotations;
using AspireOverflow.DataAccessLayer;
using AspireOverflow.Models;
using AspireOverflow.CustomExceptions;
using AspireOverflow.DataAccessLayer.Interfaces;


namespace AspireOverflow.Services
{

    public class ArticleService :IArticleService
    {
        private IArticleRepository database;

        private ILogger<ArticleService> _logger;

        private MailService _mailService;

        public ArticleService(ILogger<ArticleService> logger, MailService mailService,ArticleRepository _articleRepository)
        {
            _logger = logger;
            _mailService = mailService;
            database =_articleRepository;

        }

        public bool CreateArticle(Article article,List<int> SharedUsersId=null)
        {
            if (!Validation.ValidateArticle(article)) throw new ValidationException("Given data is InValid");
            try
            {
                article.Image = System.Convert.FromBase64String(article.ImageString);
                article.CreatedOn = DateTime.Now;
                //for adding articles visible only for shared users.
                if(SharedUsersId != null && article.IsPrivate && SharedUsersId.Count() > 0) return database.AddPrivateArticle(article,SharedUsersId);
               return database.AddArticle(article);
            }

            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage("ArticleService", "CreateArticle(Article article)", exception, article));
                return false;
            }
        }
       



        public bool UpdateArticle(Article article, int CurrentUser)
        {

            if (!Validation.ValidateArticle(article)) throw new ValidationException("Given data is InValid");
            try
            {
                var ExistingArticle = database.GetArticles().ToList().Find(Item => Item.ArtileId == article.ArtileId && Item.ArticleStatusID == 1);
                if (ExistingArticle == null) throw new ItemNotFoundException($"Unable to Find any Article with ArticleId:{article.ArtileId}");
                ExistingArticle.Title=article.Title;
                ExistingArticle.Content=article.Content;
                ExistingArticle.UpdatedOn = DateTime.Now;
                ExistingArticle.UpdatedBy = CurrentUser;
                ExistingArticle.ArticleStatusID=article.ArticleStatusID;
                ExistingArticle.Image = System.Convert.FromBase64String(article.ImageString);
             

                return database.UpdateArticle(ExistingArticle);
            }

            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage("ArticleService", " UpdateArticle(Article article, int CurrentUser)", exception, article));
                return false;
            }
        }


        public bool ChangeArticleStatus(int ArticleId, int ArticleStatusID, int UpdatedByUserId)
        {
            if (ArticleId <= 0) throw new ArgumentException($"Article Id must be greater than 0 where ArticleId:{ArticleId}");
            if (ArticleStatusID <= 0 && ArticleStatusID > 4) throw new ArgumentException($"Article Status Id must be between 0 and 4 ArticleStatusID:{ArticleStatusID}");
            try
            {
                var IsAddedSuccfully = database.UpdateArticle(ArticleId, ArticleStatusID, UpdatedByUserId);
                if (IsAddedSuccfully) _mailService.SendEmailAsync(HelperService.ArticleMail("Venkateshwaranmalai2000@gmail.com", "Title", "Article Created Successfully" , 2));
                return IsAddedSuccfully;
            }
            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage("ArticleService", " ChangeArticleStatus(int ArticleId, int ArticleStatusID, int UpdatedByUserId)", exception), ArticleId, ArticleStatusID);

                throw exception;
            }
        }

        public bool DeleteArticleByArticleId(int ArticleId)
        {
            if (ArticleId <= 0) throw new ArgumentException($"Article Id must be greater than 0 where ArticleId:{ArticleId}");
            try
            {
                if (GetArticles().Where(item => item.ArtileId == ArticleId && item.ArticleStatusID == 1).First() == null) throw new ItemNotFoundException("Only Draft Articles will be deleted");
                return database.DeleteArticle(ArticleId); //only Draft article will be deleted
            }
            catch (Exception exception)
            {

                _logger.LogError(HelperService.LoggerMessage("ArticleService", "DeleteArticleByArticleId(int ArticleId)", exception, ArticleId));

                return false;
            }
        }
        public object GetArticleById(int ArticleId)
        {
            if (ArticleId <= 0) throw new ArgumentException($"Article Id must be greater than 0 where ArticleId:{ArticleId}");
            try
            {
                var article = database.GetArticleByID(ArticleId);
                return new
                {
                    articleId = article.ArtileId,
                    PublishedDate = article.UpdatedBy,
                    title = article.Title,
                    AuthorName = article.User?.FullName,
                    content = article.Content,
                    image = article.Image,
                    Likes = GetLikesCount(article.ArtileId),
                    comments = GetComments(article.ArtileId),
                      status=article.ArticleStatus?.Status,

                };
            }
            catch (Exception exception)
            {

                _logger.LogError(HelperService.LoggerMessage("ArticleService", "GetArticleById(int ArticleId)", exception, ArticleId));

                throw exception;
            }

        }

        public IEnumerable<object> GetLatestArticles()
        {
            try
            {
                var ListOfArticles = GetArticles().OrderByDescending(article => article.CreatedOn);
                return ListOfArticles.Select(Article => new
                {
                    ArticleId = Article.ArtileId,
                    title = Article.Title,
                    AuthorName = Article.User?.FullName,
                    content = Article.Content,
                    image = Article.Image,
                    date = Article.UpdatedOn,
                      status=Article.ArticleStatus?.Status,

                });
            }

            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage("ArticleService", "GetLatestArticles()", exception));
                throw exception;
            }
        }


        public IEnumerable<Object> GetTrendingArticles()
        {
            try
            {
                var data = (database.GetLikes().GroupBy(item => item.ArticleId)).OrderByDescending(item => item.Count());

                var ListOfArticleId = (from item in data select item.First().ArticleId).ToList();
                var ListOfArticles = GetArticles().ToList();
                var TrendingArticles = new List<Article>();
                foreach (var Id in ListOfArticleId)
                {
                    TrendingArticles.Add(ListOfArticles.Find(item => item.ArtileId == Id));
                }
                return TrendingArticles.Select(Article => new
                {
                    ArticleId = Article.ArtileId,
                    title = Article.Title,
                    AuthorName = Article.User?.FullName,
                    content = Article.Content,
                    image = Article.Image,
                    date = Article.UpdatedOn,
                      status=Article.ArticleStatus?.Status,

                });
            }
            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage("ArticleService", "GetTrendingArticles()", exception));
                throw exception;
            }
        }


        public IEnumerable<object> GetArticlesByUserId(int UserId)
        {
            if (UserId <= 0) throw new ArgumentException($"User Id must be greater than 0 where UserId:{UserId}");
            try
            {
                var ListOfArticles = database.GetArticles().Where(item => item.CreatedBy == UserId).ToList();
                return ListOfArticles.Select(Article => new
                {
                    ArticleId = Article.ArtileId,
                    title = Article.Title,
                    AuthorName = Article.User?.FullName,
                    content = Article.Content,
                    image = Article.Image,
                    date = Article.UpdatedOn,
                      status=Article.ArticleStatus?.Status,

                });

            }
            catch (Exception exception)
            {

                _logger.LogError(HelperService.LoggerMessage("ArticleService", "GetArticlesByUserId(int UserId)", exception, UserId));

                throw exception;
            }

        }


        private IEnumerable<Article> GetArticles()
        {

            try
            {
                var ListOfArticles = database.GetArticles().Where(item =>item.ArticleStatusID==4);
                return ListOfArticles;
            }

            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage("ArticleService", "GetArticles()", exception));
                throw exception;
            }


        }
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
                throw exception;
            }


        }


        public IEnumerable<Object> GetListOfArticles()
        {

            try
            {
                var ListOfArticles = GetArticles().Where(Item =>Item.IsPrivate==false);
                return ListOfArticles.Select(Article => new
                {
                    ArticleId = Article.ArtileId,
                    title = Article.Title,
                    AuthorName = Article.User?.FullName,
                    content = Article.Content,
                    image = Article.Image,
                    date = Article.UpdatedOn,
                      status=Article.ArticleStatus?.Status,

                });
            }

            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage("ArticleService", "GetArticles()", exception));
                throw exception;
            }


        }

        public IEnumerable<Object> GetPrivateArticles(int UserId)
        {

            try
            {
                var ListOfPrivateArticles = database.GetPrivateArticles().Where(Item=>Item.UserId==UserId);
                return ListOfPrivateArticles.Select(PrivateArticle => GetArticleById(PrivateArticle.ArticleId));
            }

            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage("ArticleService", "GetPrivateArticles(int UserId)", exception));
                throw exception;
            }


        }



        public IEnumerable<object> GetArticlesByTitle(string Title)
        {
            if (String.IsNullOrEmpty(Title)) throw new ValidationException("Article Title cannot be null or empty");
            try
            {
                var ListOfArticles = GetArticles().Where(article => article.Title.Contains(Title));
                return ListOfArticles.Select(Article => new
                {
                    ArticleId = Article.ArtileId,
                    title = Article.Title,
                    AuthorName = Article.User?.FullName,
                    content = Article.Content,
                    image = Article.Image,
                    date = Article.UpdatedOn,
                      status=Article.ArticleStatus?.Status,

                });
            }
            catch (Exception exception)
            {

                _logger.LogError(HelperService.LoggerMessage("ArticleService", "GetArticlesByTitle(string Title)", exception, Title));

                throw exception;
            }

        }



        public IEnumerable<object> GetArticlesByAuthor(string AuthorName)
        {
            if (String.IsNullOrEmpty(AuthorName)) throw new ArgumentNullException("AuthorName value can't be null");
            try
            {
                var ListOfArticles = GetArticles().Where(article => article.User.FullName.Contains(AuthorName));
                return ListOfArticles.Select(Article => new
                {
                    ArticleId = Article.ArtileId,
                    title = Article.Title,
                    AuthorName = Article.User?.FullName,
                    content = Article.Content,
                    image = Article.Image,
                    date = Article.UpdatedOn,
                      status=Article.ArticleStatus?.Status,

                });
            }
            catch (Exception exception)
            {

                _logger.LogError(HelperService.LoggerMessage("ArticleService", "GetArticlesByAuthor(string AuthorName)", exception, AuthorName));

                throw exception;
            }

        }


        public IEnumerable<object> GetArticlesByReviewerId(int ReviewerId)
        {
            if (ReviewerId <= 0) throw new ArgumentException($"ReviewerId must be greater than 0 While ReviewerId:{ReviewerId}");
            try
            {
                var ListOfArticles = GetArticles().Where(article => article.ReviewerId == ReviewerId);
                return ListOfArticles.Select(Article => new
                {
                    ArticleId = Article.ArtileId,
                    title = Article.Title,
                    AuthorName = Article.User?.FullName,
                    content = Article.Content,
                    image = Article.Image,
                    date = Article.UpdatedOn,
                      status=Article.ArticleStatus?.Status,

                });
            }
            catch (Exception exception)
            {

                _logger.LogError(HelperService.LoggerMessage("ArticleService", " GetArticlesByReviewerId(int ReviewerId)", exception, ReviewerId));

                throw exception;
            }

        }


        public IEnumerable<object> GetArticlesByArticleStatusId(int ArticleStatusID)
        {

            if (ArticleStatusID <= 0 && ArticleStatusID > 4) throw new ArgumentException($"Article Status Id must be between 0 and 4 ArticleStatusID:{ArticleStatusID}");
            try
            {

                var ListOfArticles = GetArticles().Where(item => item.ArticleStatusID == ArticleStatusID).ToList();
                if (ArticleStatusID == 2) ListOfArticles.Where(Item => Item.ArticleStatusID == 3);
                return ListOfArticles.Select(Article => new
                {
                    ArticleId = Article.ArtileId,
                    title = Article.Title,
                    AuthorName = Article.User?.FullName,
                    content = Article.Content,
                    image = Article.Image,
                    date = Article.UpdatedOn,
                    status=Article.ArticleStatus?.Status,

                });
            }
            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage("ArticleService", "GetArticlesByArticleStatusId(int ArticleStatusID)", exception), ArticleStatusID);

                throw exception;
            }

        }


        //Article Comment
        public bool CreateComment(ArticleComment comment)
        {
            Validation.ValidateArticleComment(comment);
            try
            {
                comment.CreatedBy=comment.UserId;
                var IsCommentSuccessfully = database.AddComment(comment);
                if (IsCommentSuccessfully) _mailService.SendEmailAsync(HelperService.CommentMail("Venkateshwaranmalai2000@gmail.com", "Title", "Article Created Successfully" ));
                return IsCommentSuccessfully;
            }
            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage("ArticleService", "CreateComment()", exception), comment);
                return false;
            }
        }


        public IEnumerable<Object> GetComments(int ArticleId)
        {
            if (ArticleId <= 0) throw new ArgumentException($"Article Id must be greater than 0 where ArticleId:{ArticleId}");
            try
            {
                var ListOfComments = database.GetComments().Where(comment => comment.ArticleId == ArticleId);
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
                _logger.LogError(HelperService.LoggerMessage("ArticleService", "GetComments()", exception, ArticleId));
                throw exception;
            }
        }



        public bool AddLikeToArticle(ArticleLike Like)
        {
            if (Like.ArticleId <= 0) throw new ArgumentException($"Article Id must be greater than 0 where ArticleId:{Like.ArticleId}");
            if (Like.UserId <= 0) throw new ArgumentException($"User Id must be greater than 0 where UserId:{Like.UserId}");
            try
            {
                if (database.GetLikes().ToList().Find(item => item.UserId == Like.UserId && item.ArticleId == Like.ArticleId) != null) throw new Exception("Unable to Add like to same article with same UserId");

                return database.AddLike(Like);
            }
            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage("ArticleService", "AddLikeToArticle()", exception, Like));
                return false;
            }
        }


        public int GetLikesCount(int ArticleId)
        {
            if (ArticleId <= 0) throw new ArgumentException($"Article Id must be greater than 0 where ArticleId:{ArticleId}");
            try
            {
                var ArticleLikes = database.GetLikes().Count(item => item.ArticleId == ArticleId);
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