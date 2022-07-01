
using System.Linq;
using System.ComponentModel.DataAnnotations;
using AspireOverflow.DataAccessLayer.Interfaces;
using AspireOverflow.Models;
using AspireOverflow.Services;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace AspireOverflowTest
{
    public class ArticleServiceTest
    {
        private readonly Mock<IArticleRepository> _articleRepository = new Mock<IArticleRepository>();
        private readonly Mock<ILogger<ArticleService>> _logger = new Mock<ILogger<ArticleService>>();
        private readonly ArticleService _articleService;
        public ArticleServiceTest()
        {
            _articleService = new ArticleService(_logger.Object, null, _articleRepository.Object);
        }
        //GetArticlesByUserId

        [Theory]
        [InlineData(0)]
        public void GetArticlesByUserId_ShouldReturnArgumentException_WhenArticlesByUserIdIsInValid(int UserId)
        {

            Assert.Throws<ArgumentException>(() => _articleService.GetArticlesByUserId(UserId));
        }
        [Theory]
        [InlineData(1)]
        public void GetArticlesByUserId_ShouldReturnListOfArticle_WhenMethodIsCalled(int UserId)
        {
            var Articles = ArticleMock.GetListOfArticle();
            _articleRepository.Setup(obj => obj.GetArticles()).Returns(Articles);

            var ExpectedArticle = Articles.Where(item => item.CreatedBy == UserId).Count();
            Assert.Equal(ExpectedArticle, _articleService.GetArticlesByUserId(UserId).Count());
        }
        [Fact]
        public void GetArticlesByUserId_ShoultThrowException_WhenAnyExceptionIsRaised()
        {
            int UserId = 1;
            _articleRepository.Setup(obj => obj.GetArticles()).Throws(new Exception());
            Assert.Throws<Exception>(() => _articleService.GetArticlesByUserId(UserId));
        }
        //GetPrivateArticles

        [Theory]
        [InlineData(2)]
        public void GetPrivateArticles_ShouldReturnListOfArticle_WhenMethodIsCalled(int UserId)
        {
            IEnumerable<PrivateArticle> Articles = ArticleMock.GetListofPrivateArticle();
            _articleRepository.Setup(obj => obj.GetPrivateArticles()).Returns(Articles);

            var ExpectedArticle = Articles.Where(item => item.UserId == UserId);
            Assert.Equal(ExpectedArticle, _articleService.GetPrivateArticles(UserId));
        }
        [Fact]
        public void GetPrivateArticles_ShoultThrowException_WhenAnyExceptionIsRaised()
        {
            int UserId = 1;
            _articleRepository.Setup(obj => obj.GetPrivateArticles()).Throws(new Exception());
            Assert.Throws<Exception>(() => _articleService.GetPrivateArticles(UserId));
        }
        //GetArticlesByArticleStatusId

        [Theory]
        [InlineData(0)]
        public void GetArticlesByArticleStatusId_ShouldReturnArgumentException_WhenArticleStatusIdIsInValid(int ArticleStatusID)
        {
            Assert.Throws<ArgumentException>(() => _articleService.GetArticlesByArticleStatusId(ArticleStatusID));
        }
        [Theory]
        [InlineData(1)]
        public void GetArticlesByArticleStatusId_ShouldReturnListofArticle_WhenMethodIsCalled(int ArticleStatusID)
        {
            var Articles = ArticleMock.GetListOfArticle();
            _articleRepository.Setup(obj => obj.GetArticles()).Returns(Articles);
            var ExpectedArticle = Articles.Where(item => item.ArticleStatusID == ArticleStatusID).Count();
            Assert.Equal(ExpectedArticle, _articleService.GetArticlesByArticleStatusId(ArticleStatusID).Count());
        }
        [Fact]
        public void GetArticlesByArticleStatusId_ShoultThrowException_WhenAnyExceptionIsRaised()
        {
            int ArticleStatusId = 1;
            _articleRepository.Setup(obj => obj.GetArticles()).Throws(new Exception());
            Assert.Throws<Exception>(() => _articleService.GetArticlesByArticleStatusId(ArticleStatusId));
        }
        //GetComments

        [Theory]
        [InlineData(0)]
        public void GetComments_ShouldReturnArgumentException_WhenCommentsIsInValid(int ArticleId)
        {
            Assert.Throws<ArgumentException>(() => _articleService.GetComments(ArticleId));
        }
        [Theory]
        [InlineData(1)]
        public void GetComments_ShouldReturnValidArticleComment_WhenMethodIsCalled(int ArticleId)
        {
            var Comments = ArticleMock.GetValidArticleComment();
            _articleRepository.Setup(obj => obj.GetComments()).Returns(Comments);

            var ExpectedArticle = Comments.Where(comment => comment.ArticleId == ArticleId).Count();
            Assert.Equal(ExpectedArticle, _articleService.GetArticlesByUserId(ArticleId).Count());
        }
        [Fact]
        public void GetComments_ShoultThrowException_WhenAnyExceptionIsRaised()
        {
            int ArticleId = 1;
            _articleRepository.Setup(obj => obj.GetComments()).Throws(new Exception());
            Assert.Throws<Exception>(() => _articleService.GetComments(ArticleId));
        }
        //GetAll

        [Fact]
        public void GetAll_ShouldReturnTrue_WhenArticleObjectIsValid()

        {
            var Articles = ArticleMock.GetListOfArticle();
            _articleRepository.Setup(obj => obj.GetArticles()).Returns(Articles);
            var ExpectedArticle = Articles.Where(item => item.ArticleStatusID == 4).Count();
            Assert.Equal(ExpectedArticle, _articleService.GetListOfArticles().Count());
        }
        [Fact]
        public void GetAll_ShoultThrowException_WhenAnyExceptionIsRaised()
        {
            _articleRepository.Setup(obj => obj.GetArticles()).Throws(new Exception());
            Assert.Throws<Exception>(() => _articleService.GetListOfArticles());
        }
        //CreateArticle

        [Theory]
        [InlineData(null)]
        public void CreateArticle_ShouldThrowValidationException_WhenArticleObjectIsNull(Article article)
        {
            Assert.Throws<ValidationException>(() => _articleService.CreateArticle(article));
        }
        [Fact]
        public void CreateArticle_ShouldThrowValidationException_WhenArticleObjectIsInvalid()
        {
            Article article = ArticleMock.GetInValidArticle();
            Assert.Throws<ValidationException>(() => _articleService.CreateArticle(article));
        }
        [Fact]
        public void CreateArticle_ShouldReturnTrue_WhenArticleObjectIsValid()
        {
            Article article = ArticleMock.GetValidArticle();
            _articleRepository.Setup(obj => obj.AddArticle(article)).Returns(true);
            var Result = _articleService.CreateArticle(article);
            Assert.True(Result);
        }
        [Fact]
        public void CreateArticle_ShouldReturnTrue_WhenSharedUsersIsNotNull()
        {
            var SharedUsers = new List<int>() { 1 };
            Article article = ArticleMock.GetValidArticle();
            article.IsPrivate = true;
            _articleRepository.Setup(obj => obj.AddPrivateArticle(article, SharedUsers)).Returns(true);
            var Result = _articleService.CreateArticle(article, SharedUsers);
            Assert.True(Result);
        }
        [Fact]
        public void CreateArticle_ShouldReturnFalse_WhenExceptionThrownInArticleRepository()
        {
            Article article = ArticleMock.GetValidArticle();
            _articleRepository.Setup(obj => obj.AddArticle(article)).Throws(new DbUpdateException());
            var Result = _articleService.CreateArticle(article);
            Assert.False(Result);
            _articleRepository.Setup(obj => obj.AddArticle(article)).Throws(new Exception());
            Assert.False(_articleService.CreateArticle(article));
            _articleRepository.Setup(obj => obj.AddArticle(article)).Throws(new OperationCanceledException());
            Assert.False(_articleService.CreateArticle(article));
        }
        //Create Article Comment
        [Theory]
        [InlineData(null)]
        public void CreateComment_ShouldThrowValidationException_WhenArticleObjectIsNull(ArticleComment Comment)
        {
            Assert.Throws<ValidationException>(() => _articleService.CreateComment(Comment));
        }
        [Fact]
        public void CreateComment_ShouldThrowValidationException_WhenArticleObjectIsInvalid()
        {
            ArticleComment Comment = new ArticleComment();
            Assert.Throws<ValidationException>(() => _articleService.CreateComment(Comment));
        }
        [Fact]
        public void CreateComment_ShouldReturnTrue_WhenArticleObjectIsValid()

        {
            ArticleComment Comment = ArticleMock.GetValidArticleComments();
            _articleRepository.Setup(obj => obj.AddComment(Comment)).Returns(true);
            var Result = _articleService.CreateComment(Comment);
            Assert.True(Result);
        }
        [Fact]
        public void CreateComment_ShouldReturnFalse_WhenExceptionThrownInArticleRepository()
        {
            ArticleComment Comment = ArticleMock.GetValidArticleComments();
            Comment.CreatedBy = 1;
            _articleRepository.Setup(obj => obj.AddComment(Comment)).Throws(new Exception());
            Assert.False(_articleService.CreateComment(Comment));
        }
        //AddLikeToArticle
        [Theory]
        [InlineData(null)]
        public void AddLikeToArticle_ShouldThrowArgumentException_WhenArticleLikeObjectIsNull(ArticleLike Like)
        {
            Assert.Throws<ArgumentException>(() => _articleService.AddLikeToArticle(Like));
        }
        [Fact]
        public void AddLikeToArticle_ShouldThrowArgumentException_WhenArticleLikeObjectIsInvalid()
        {
            ArticleLike like = new ArticleLike();
            Assert.Throws<ArgumentException>(() => _articleService.AddLikeToArticle(like));
        }
        [Fact]
        public void AddLikeToArticle_ShouldReturnTrue_WhenArticleObjectIsValid()
        {
            ArticleLike Like = new ArticleLike();
            Like.ArticleId = 1;
            Like.UserId = 1;
            _articleRepository.Setup(obj => obj.AddLike(Like)).Returns(true);
            var Result = _articleService.AddLikeToArticle(Like);
            Assert.True(Result);
        }
        [Fact]
        public void AddLikeToArticle_ShouldReturnFalse_WhenExceptionThrownInArticleRepository()
        {
            ArticleLike Like = new ArticleLike();
            Like.ArticleId = 1;
            Like.UserId = 1;
            _articleRepository.Setup(obj => obj.AddLike(Like)).Throws(new Exception());
            Assert.False(_articleService.AddLikeToArticle(Like));
        }
        //GetArticleById
        [Theory]
        [InlineData(0)]
        public void GetArticleById_ShouldReturnArgumentException_WhenArticleIdIsInValid(int articleId)
        {
            Assert.Throws<ArgumentException>(() => _articleService.GetArticleById(articleId));
        }
        [Fact]
        public void GetArticleById_ShoultThrowException_WhenAnyExceptionIsRaised()
        {
            var ArticleId = 1;
            _articleRepository.Setup(obj => obj.GetArticleByID(ArticleId)).Throws(new Exception());
            Assert.Throws<Exception>(() => _articleService.GetArticleById(ArticleId));
        }
        [Theory]
        [InlineData(1)]
        public void GetArticlebyId_ShouldReturnArticle_WhenMethodIsCalled(int articleId)
        {
            var article = ArticleMock.GetValidArticle();
            _articleRepository.Setup(obj => obj.GetArticleByID(articleId)).Returns(article);
            Assert.NotEqual(null, _articleService.GetArticleById(articleId));
        }
        //GetLatestArticles  
        [Fact]
        public void GetLatestArticles_ShouldReturnListOfQueries_WhenMethodIsCalled()
        {
            var Articles = ArticleMock.GetListOfArticle();
            _articleRepository.Setup(obj => obj.GetArticles()).Returns(Articles);
            Assert.Equal(2, _articleService.GetLatestArticles().Count());
        }
        [Fact]
        public void GetLatestArticles_ShoultThrowException_WhenAnyExceptionIsRaised()
        {
            _articleRepository.Setup(obj => obj.GetArticles()).Throws(new Exception());
            Assert.Throws<Exception>(() => _articleService.GetLatestArticles());
        }
        //GetTrendingArticles  
        //  [Fact]    
        //  public void GetTrendingArticles_ShouldReturnListOfArticle_WhenMethodIsCalled(){
        //    var ArticleLikes = ArticleMock.GetListOfArticleLikes(); 
        //     var Articles = ArticleMock.GetListOfArticle().ToList();        
        //    _articleRepository.Setup(obj => obj.GetLikes()).Returns(ArticleLikes);
        //    _articleRepository.Setup(obj=>obj.GetArticles()).Returns(Articles);
        //     var ExpectedArticles =ArticleLikes.GroupBy(item => item.ArticleId).OrderByDescending(item => item.Count()).Count();
        //     Assert.Equal(ExpectedArticles,_articleService.GetTrendingArticles().Count());
        //  }
        [Fact]
        public void GetTrendingArticles_ShouldThrowException_WhenAnyExceptionIsRaised()
        {
            _articleRepository.Setup(obj => obj.GetLikes()).Throws(new Exception());
            Assert.Throws<Exception>(() => _articleService.GetTrendingArticles());
        }
        //DeleteArticleByArticleId
        [Theory]
        [InlineData(0)]
        public void DeleteArticleByArticleId_ShouldThrowException_WhenArgumentExceptionIsRaised(int articleId)
        {
            Assert.Throws<ArgumentException>(() => _articleService.DeleteArticleByArticleId(articleId));
        }
        [Theory]
        [InlineData(2)]
        public void DeleteArticleByArticleId_ShouldDeleteArticle_WhenArticleisRaised(int articleId)
        {
            _articleRepository.Setup(obj => obj.DeleteArticle(articleId)).Returns(true);
            Assert.Equal(true, _articleService.DeleteArticleByArticleId(articleId));
        }
        [Theory]
        [InlineData(1)]
        public void DeleteArticleByArticleId_ShouldThrowException_WhenAnyExceptionIsRaised(int articleId)
        {
            _articleRepository.Setup(obj => obj.GetArticles()).Throws(new Exception());
            Assert.Equal(false, _articleService.DeleteArticleByArticleId(articleId));
        }
        //changearticlestatus
        [Theory]
        [InlineData(0, 1, 0)]
        public void ChangeArticleStatus_ShouldThrowException_WhenArticleId_Zero(int articleId, int articleStatusId, int UserId)
        {
            Assert.Throws<ArgumentException>(() => _articleService.ChangeArticleStatus(articleId, articleStatusId, UserId));
        }

        [Theory]
        [InlineData(3, 7, 0)]
        public void ChangeArticleStatus_ShouldThrowException_WhenArticleIdAndArticleStatusIdLessthanZeroGreterthanFour(int articleId, int articleStatusId, int UserId)
        {
            Assert.Throws<ArgumentException>(() => _articleService.ChangeArticleStatus(articleId, articleStatusId, UserId));
        }

        [Theory]
        [InlineData(1, 1, 0)]
        public void ChangeArticleStatus_ShouldThrowException_WhenUpdateArticleStatus(int articleId, int articleStatusId, int UserId)
        {
            _articleRepository.Setup(obj => obj.UpdateArticle(articleId, articleStatusId, UserId)).Returns(true);
            Assert.Equal(true, _articleService.ChangeArticleStatus(articleId, articleStatusId, UserId));
        }
        [Theory]
        [InlineData(1, 1, 0)]
        public void ChangeArticleStatus_ShouldThrowException_WhenAnyExceptionIsRaised(int articleId, int articleStatusId, int UserId)
        {
            _articleRepository.Setup(obj => obj.GetArticles()).Throws(new Exception());

            Assert.Equal(false, _articleService.ChangeArticleStatus(articleId, articleStatusId, UserId));
        }
        //updatearticles
        [Theory]
        [InlineData(null, 0)]
        public void UpdateArticle_ShouldThrowValidationException_WhenArticleIsNull(Article article, int CurrentUser)
        {

            Assert.Throws<ValidationException>(() => _articleService.UpdateArticle(article, CurrentUser));
        }
        [Theory]
        [InlineData(1)]
        public void UpdateArticle_ShouldThrowException_WhenAnyExceptionIsRaised(int CurrentUser)
        {
            var article = ArticleMock.GetValidArticle();
            _articleRepository.Setup(obj => obj.GetArticles()).Throws(new Exception());
            Assert.Equal(false, _articleService.UpdateArticle(article, CurrentUser));
        }
    }
}