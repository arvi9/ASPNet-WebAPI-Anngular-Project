using System.Linq;

using System.ComponentModel.DataAnnotations;

using AspireOverflow.Controllers;

using AspireOverflow.Models;

using Microsoft.AspNetCore.Mvc;

using Microsoft.Extensions.Logging;

using Moq;

using Xunit;

using AspireOverflow.DataAccessLayer.Interfaces;

using System;

using AspireOverflow.Services;

using AspireOverflow.CustomExceptions;
using AspireOverflowTest.DataAccessLayer;

namespace AspireOverflowTest
{

    public class ArticleControllerTest
    {

        private readonly ArticleController _ArticleController;

        private readonly Mock<ILogger<ArticleController>> _logger = new Mock<ILogger<ArticleController>>();

        private readonly Mock<IArticleService> _ArticleService = new Mock<IArticleService>();


        public ArticleControllerTest()
        {
            _ArticleController = new ArticleController(_logger.Object, _ArticleService.Object);
        }
        // GetArticlesByUserId

        [Fact]
        public void GetArticlesByUserId_ShouldReturnListOfArticles_WhenMethodIsCalled()
        {
            var Articles = ArticleMock.GetListOfArticle();
            int createdby = 1;
            _ArticleService.Setup(obj => obj.GetArticlesByUserId(createdby)).Returns(Articles);
            var Result = _ArticleController.GetArticlesByUserId().Result as ObjectResult;
            Assert.Equal(200, Result?.StatusCode);
        }
        [Fact]
        public void GetArticlesByUserId_ShouldThrowStatusCode500_WhenExceptionIsThrown()
        {

            _ArticleService.Setup(obj => obj.GetArticlesByUserId(0)).Throws<Exception>();
            var Result = _ArticleController.GetArticlesByUserId().Result as ObjectResult;
            Assert.Equal(500, Result?.StatusCode);
        }
        //GetArticlesByTitle

        [Theory]
        [InlineData(null)]
        public void GetArticlesByTitle_ShouldReturnStatusCode400_WhenArticleObjectIsNull(string Title)
        {

            var Result = _ArticleController.GetArticlesByTitle(Title).Result as ObjectResult;
            Assert.Equal(400, Result?.StatusCode);

        }
        [Theory]
        [InlineData("Sample Data")]
        public void GetArticlesByTitle_ShouldReturnStatusCode200_WhenArticleObjectIsPassed(string Title)
        {
            var Articles = ArticleMock.GetListOfArticle();
            _ArticleService.Setup(obj => obj.GetArticlesByTitle(Title)).Returns(Articles);
            var Result = _ArticleController.GetArticlesByTitle(Title).Result as ObjectResult;
            Assert.Equal(200, Result?.StatusCode);
        }
        [Fact]
        public void GetArticlesByTitle_ShouldReturnStatusCode500_WhenExceptionIsThrown()
        {
            string Title = "Sample data";
            _ArticleService.Setup(obj => obj.GetArticlesByTitle(Title)).Throws(new Exception());
            var Result = _ArticleController.GetArticlesByTitle(Title).Result as ObjectResult;
            Assert.Equal(500, Result?.StatusCode);
        }
        //GetArticlesByAuthor

        [Theory]
        [InlineData(null)]
        public void GetArticlesByAuthor_ShouldReturnStatusCode400_WhenArticleObjectIsNull(string AuthorName)
        {

            var Result = _ArticleController.GetArticlesByAuthor(AuthorName).Result as ObjectResult;
            Assert.Equal(400, Result?.StatusCode);

        }
        [Theory]
        [InlineData("Raghu")]
        public void GetArticlesByAuthor_ShouldReturnStatusCode200_WhenArticleObjectIsPassed(string AuthorName)
        {
            var Articles = ArticleMock.GetListOfArticle();
            _ArticleService.Setup(obj => obj.GetArticlesByAuthor(AuthorName)).Returns(Articles);
            var Result = _ArticleController.GetArticlesByAuthor(AuthorName).Result as ObjectResult;
            Assert.Equal(200, Result?.StatusCode);
        }
        //GetPrivateArticles

        [Theory]
        [InlineData(-1)]
        public void GetPrivateArticles_ShouldReturnStatusCode400_WhenArticleObjectIsNull(int UserId)
        {

            var Result = _ArticleController.GetPrivateArticles(UserId).Result as ObjectResult;
            Assert.Equal(400, Result?.StatusCode);

        }
        [Theory]
        [InlineData(2)]
        public void GetPrivateArticles_ShouldReturnStatusCode200_WhenArticleObjectIsPassed(int UserId)
        {
            var Articles = ArticleMock.GetListOfArticle();
            _ArticleService.Setup(obj => obj.GetPrivateArticles(UserId)).Returns(Articles);
            var Result = _ArticleController.GetPrivateArticles(UserId).Result as ObjectResult;
            Assert.Equal(200, Result?.StatusCode);
        }
        [Fact]
        public void GetPrivateArticles_ShouldReturnStatusCode500_WhenExceptionIsThrown()
        {
            int UserId = 1;
            _ArticleService.Setup(obj => obj.GetPrivateArticles(UserId)).Throws(new Exception());
            var Result = _ArticleController.GetPrivateArticles(UserId).Result as ObjectResult;
            Assert.Equal(500, Result?.StatusCode);
        }
        [Fact]
        //GetALL

        public void GetALL_ShouldReturnStatusCode200_WhenArticleObjectIsPassed()
        {
            var Article = ArticleMock.GetListOfArticle();
            _ArticleService.Setup(obj => obj.GetListOfArticles()).Returns(Article);
            var Result = _ArticleController.GetAll().Result as ObjectResult;
            Assert.Equal(200, Result?.StatusCode);
        }
        [Fact]
        public void GetALL_ShouldReturnStatusCode500_WhenExceptionIsThrown()
        {

            _ArticleService.Setup(obj => obj.GetListOfArticles()).Throws(new Exception());
            var Result = _ArticleController.GetAll().Result as ObjectResult;
            Assert.Equal(500, Result?.StatusCode);
        }
        //GetArticlesByArticleStatusId

        [Theory]
        [InlineData(5)]
        public void GetArticlesByArticleStatusId_ShouldReturnStatusCode400_WhenArticleObjectIsNull(int ArticleStatusID)
        {

            var Result = _ArticleController.GetArticlesByArticleStatusId(ArticleStatusID).Result as ObjectResult;
            Assert.Equal(400, Result?.StatusCode);

        }
        [Theory]
        [InlineData(2)]
        public void GetArticlesByArticleStatusId_ShouldReturnStatusCode200_WhenArticleObjectIsPassed(int ArticleStatusID)
        {
            var Articles = ArticleMock.GetListOfArticle();
            _ArticleService.Setup(obj => obj.GetArticlesByArticleStatusId(ArticleStatusID)).Returns(Articles);
            var Result = _ArticleController.GetArticlesByArticleStatusId(ArticleStatusID).Result as ObjectResult;
            Assert.Equal(200, Result?.StatusCode);
        }
        [Fact]
        public void GetArticlesByArticleStatusId_ShouldReturnStatusCode500_WhenExceptionIsThrown()
        {
            int ArticleStatusId = 2;
            _ArticleService.Setup(obj => obj.GetArticlesByArticleStatusId(ArticleStatusId)).Throws(new Exception());
            var Result = _ArticleController.GetArticlesByArticleStatusId(ArticleStatusId).Result as ObjectResult;
            Assert.Equal(500, Result?.StatusCode);
        }
        //GetComments

        [Theory]
        [InlineData(0)]
        public void GetComments_ShouldReturnStatusCode400_WhenQueryObjectIsNull(int ArticleId)
        {
            var Result = _ArticleController.GetComments(ArticleId).Result as ObjectResult;
            Assert.Equal(400, Result?.StatusCode);
        }
        [Theory]
        [InlineData(1)]
        public void GetComments_ShouldReturnStatusCode200_WhenArticleObjectIsPassed(int ArticleId)
        {
            var Articles = ArticleMock.GetListOfArticleComments();
            _ArticleService.Setup(obj => obj.GetComments(ArticleId)).Returns(Articles);
            var Result = _ArticleController.GetComments(ArticleId).Result as ObjectResult;
            Assert.Equal(200, Result?.StatusCode);
        }
        [Fact]
        public void GetComments_ShouldReturnStatusCode500_WhenExceptionIsThrown()
        {
            int ArticleId = 1;
            _ArticleService.Setup(obj => obj.GetComments(ArticleId)).Throws(new Exception());
            var Result = _ArticleController.GetComments(ArticleId).Result as ObjectResult;
            Assert.Equal(500, Result?.StatusCode);
        }
        //CreateArticle

        [Theory]
        [InlineData(null)]
        public void CreateArticle_ShouldReturnStatusCode400_WhenArticleObjectIsNull(Article article)
        {
            var Result = _ArticleController.CreateArticle(article).Result as ObjectResult;
            Assert.Equal(400, Result?.StatusCode);
        }
        [Fact]
        public void CreateArticle_ShouldReturnStatusCode200_WhenArticleObjectIsPassed()
        {
            Article article = ArticleMock.GetValidArticle();
            _ArticleService.Setup(obj => obj.CreateArticle(article, null)).Returns(true);

            var Result = _ArticleController.CreateArticle(article).Result as ObjectResult;
            Assert.Equal(200, Result?.StatusCode);

        }
        [Fact]
        public void CreateArticle_ShouldReturnStatusCode400_WhenArticleServiceReturnsFalse()
        {
            var article = new Article();
            _ArticleService.Setup(obj => obj.CreateArticle(article, null)).Returns(false);
            var Result = _ArticleController.CreateArticle(article).Result as ObjectResult;
            Assert.Equal(400, Result?.StatusCode);

        }
        [Fact]
        public void CreateArticle_ShouldReturnStatusCode400_WhenValidationExceptionIsThrown()
        {
            var article = new Article();
            _ArticleService.Setup(obj => obj.CreateArticle(article, null)).Throws(new ValidationException());
            var Result = _ArticleController.CreateArticle(article).Result as ObjectResult;
            Assert.Equal(400, Result?.StatusCode);
        }
        [Fact]
        public void CreateArticle_ShouldReturnStatusCode500_WhenExceptionIsThrown()
        {
            var article = new Article();
            _ArticleService.Setup(obj => obj.CreateArticle(article, null)).Throws(new Exception());

            var Result = _ArticleController.CreateArticle(article).Result as ObjectResult;

            Assert.Equal(500, Result?.StatusCode);
        }
        //CreatePrivateArticle

        [Theory]
        [InlineData(null)]
        public void CreatePrivateArticle_ShouldReturnStatusCode400_WhenArticleObjectIsNull(PrivateArticleDto PrivateArticleDto)
        {

            var Result = _ArticleController.CreatePrivateArticle(PrivateArticleDto).Result as ObjectResult;
            Assert.Equal(400, Result?.StatusCode);

        }
        [Fact]
        public void CreatePrivateArticle_ShouldReturnStatusCode200_WhenArticleObjectIsPassed()
        {
            Article PrivateArticleDto = ArticleMock.GetValidArticle();
            _ArticleService.Setup(obj => obj.CreateArticle(PrivateArticleDto, null)).Returns(true);

            var Result = _ArticleController.CreateArticle(PrivateArticleDto).Result as ObjectResult;
            Assert.Equal(200, Result?.StatusCode);

        }
        [Fact]
        public void CreatePrivateArticle_ShouldReturnStatusCode400_WhenArticleServiceReturnsFalse()
        {
            Article PrivateArticleDto = ArticleMock.GetValidArticle();
            _ArticleService.Setup(obj => obj.CreateArticle(PrivateArticleDto, null)).Returns(false);
            var Result = _ArticleController.CreateArticle(PrivateArticleDto).Result as ObjectResult;
            Assert.Equal(400, Result?.StatusCode);
        }
        [Fact]
        public void CreatePrivateArticle_ShouldReturnStatusCode500_WhenValidationExceptionIsThrown()
        {
            Article PrivateArticle = ArticleMock.GetValidArticle();
            var PrivateArticleDto = new PrivateArticleDto();
            _ArticleService.Setup(obj => obj.CreateArticle(PrivateArticle, null)).Throws(new ValidationException());
            var Result = _ArticleController.CreatePrivateArticle(PrivateArticleDto).Result as ObjectResult;
            Assert.Equal(500, Result?.StatusCode);
        }
        [Fact]
        public void CreatePrivateArticle_ShouldReturnStatusCode500_WhenExceptionIsThrown()
        {
            Article PrivateArticle = ArticleMock.GetValidArticle();
            var PrivateArticleDto = new PrivateArticleDto();
            _ArticleService.Setup(obj => obj.CreateArticle(PrivateArticle, null)).Throws(new Exception());
            var Result = _ArticleController.CreatePrivateArticle(PrivateArticleDto).Result as ObjectResult;

            Assert.Equal(500, Result?.StatusCode);
        }
        //CreateComment

        [Theory]
        [InlineData(null)]
        public void CreateComment_ShouldReturnStatusCode400_WhenArticleObjectIsNull(ArticleComment comment)
        {
            var Result = _ArticleController.CreateComment(comment).Result as ObjectResult;
            Assert.Equal(400, Result?.StatusCode);
        }
        [Fact]
        public void CreateComment_ShouldReturnStatusCode200_WhenArticleCommentIsValid()
        {
            ArticleComment comment = new ArticleComment();
            _ArticleService.Setup(obj => obj.CreateComment(comment)).Returns(true);

            var Result = _ArticleController.CreateComment(comment).Result as ObjectResult;
            Assert.Equal(200, Result?.StatusCode);
        }
        [Fact]
        public void CreateComment_ShouldReturnStatusCode400_WhenArticleServiceReturnsFalse()
        {
            ArticleComment comment = new ArticleComment();
            _ArticleService.Setup(obj => obj.CreateComment(comment)).Returns(false);
            var Result = _ArticleController.CreateComment(comment).Result as ObjectResult;
            Assert.Equal(400, Result?.StatusCode);
        }
        [Fact]
        public void CreateComment_ShouldReturnStatusCode400_WhenValidationExceptionIsThrown()
        {
            ArticleComment comment = new ArticleComment();
            _ArticleService.Setup(obj => obj.CreateComment(comment)).Throws(new ValidationException());
            var Result = _ArticleController.CreateComment(comment).Result as ObjectResult;
            Assert.Equal(400, Result?.StatusCode);
        }
        [Fact]
        public void CreateComment_ShouldReturnStatusCode500_WhenExceptionIsThrown()
        {
            ArticleComment comment = new ArticleComment();
            _ArticleService.Setup(obj => obj.CreateComment(comment)).Throws(new Exception());
            var Result = _ArticleController.CreateComment(comment).Result as ObjectResult;
            Assert.Equal(500, Result?.StatusCode);
        }
        //GetArticleById

        [Theory]
        [InlineData(0)]
        public void GetArticleById_ShouldReturnStatusCode400_WhenArticleIdIsZero(int articleId)
        {
            var Result = _ArticleController.GetArticleById(articleId).Result as ObjectResult;
            Assert.Equal(400, Result?.StatusCode);
        }
        [Theory]
        [InlineData(1)]
        public void GetArticleById_ShouldReturnQueries_WhenArticleIdIsValid(int articleId)
        {
            _ArticleService.Setup(obj => obj.GetArticleById(articleId)).Returns(true);
            var Result = _ArticleController.GetArticleById(articleId).Result as ObjectResult;
            Assert.Equal(200, Result?.StatusCode);
        }
        [Fact]
        public void GetArticleById_ShouldReturnStatusCode500_WhenExceptionIsThrown()
        {
            var articleId = 4;
            _ArticleService.Setup(obj => obj.GetArticleById(articleId)).Throws(new Exception());
            var Result = _ArticleController.GetArticleById(articleId).Result as ObjectResult;
            Assert.Equal(500, Result?.StatusCode);
        }
        [Fact]
        public void GetArticleById_ShouldReturnStatusCode500_WhenItemNotFoundExceptionIsThrown()
        {
            int articleId = 2;
            _ArticleService.Setup(obj => obj.GetArticleById(articleId)).Throws(new Exception());
            var Result = _ArticleController.GetArticleById(articleId).Result as ObjectResult;
            Assert.Equal(500, Result?.StatusCode);
        }
        //AddLikeToArticle

        [Theory]
        [InlineData(null)]
        public void AddLikeToArticle_ShouldReturnStatusCode400_WhenArticleLikeObjectIsNull(ArticleLike Like)
        {
            var Result = _ArticleController.AddLikeToArticle(Like).Result as ObjectResult;
            Assert.Equal(400, Result?.StatusCode);
        }
        //Passed
        [Fact]
        public void AddLikeToArticle_ShouldReturnStatusCode400_WhenArticleIdIsZero()
        {
            ArticleLike Like = new ArticleLike();
            Like.ArticleId = 0;
            var Result = _ArticleController.AddLikeToArticle(Like).Result as ObjectResult;
            Assert.Equal(400, Result?.StatusCode);
        }
        [Fact]
        public void AddLikeToArticle_ShouldReturnStatusCode200_WhenArticleCommentIsValid()
        {
            ArticleLike Like = new ArticleLike();
            Like.ArticleId = 1;
            _ArticleService.Setup(obj => obj.AddLikeToArticle(Like)).Returns(true);
            var Result = _ArticleController.AddLikeToArticle(Like).Result as ObjectResult;
            Assert.Equal(200, Result?.StatusCode);
        }
        [Fact]
        public void AddLikeToArticle_ShouldReturnStatusCode400_WhenArgumentExceptionIsThrown()
        {
            ArticleLike Like = new ArticleLike();
            Like.ArticleId = 1;
            _ArticleService.Setup(obj => obj.AddLikeToArticle(Like)).Throws(new ArgumentException());
            var Result = _ArticleController.AddLikeToArticle(Like).Result as ObjectResult;
            Assert.Equal(500, Result?.StatusCode);
        }
        [Fact]
        public void AddLikeToArticle_ShouldReturnStatusCode500_WhenExceptionIsThrown()
        {
            ArticleLike Like = new ArticleLike();
            Like.ArticleId = 1;
            _ArticleService.Setup(obj => obj.AddLikeToArticle(Like)).Throws(new Exception());
            var Result = _ArticleController.AddLikeToArticle(Like).Result as ObjectResult;
            Assert.Equal(500, Result?.StatusCode);
        }
        //GetLatestArticles

        [Theory]
        [InlineData(0)]
        public void GetLatestArticles_ShouldReturnListOfQueries_WhenMethodIsCalled(int Range)
        {

            var Articles = ArticleMock.GetListOfArticle();

            _ArticleService.Setup(obj => obj.GetLatestArticles()).Returns(Articles);

            var Result = _ArticleController.GetLatestArticles().Result as ObjectResult;

            Assert.Equal(Articles, Result?.Value);
        }
        [Fact]
        public void GetLatestArticles_ShouldReturnStatusCode400_WhenRangelimitexceeded()
        {
            int range = 10;

            var Result = _ArticleController.GetLatestArticles(range).Result as ObjectResult;
            Assert.Equal(400, Result?.StatusCode);
        }
        [Fact]
        public void GetLatestArticles_ShouldReturnStatusCode500_WhenExceptionIsThrown()
        {

            _ArticleService.Setup(obj => obj.GetLatestArticles()).Throws(new Exception());

            var Result = _ArticleController.GetLatestArticles().Result as ObjectResult;

            Assert.Equal(500, Result?.StatusCode);
        }
        //GetTrendingArticles

        [Theory]
        [InlineData(0)]
        public void GetTrendingArticles_ShouldReturnListOfQueries_WhenMethodIsCalled(int Range)

        {
            int count = ArticleMock.GetListOfArticle().Count();
            var Articles = ArticleMock.GetListOfArticle();

            _ArticleService.Setup(obj => obj.GetTrendingArticles()).Returns(Articles);

            var Result = _ArticleController.GetTrendingArticles().Result as ObjectResult;

            Assert.Equal(Articles, Result?.Value);
        }
        [Fact]
        public void GetTrendingArticles_ShouldReturnStatusCode400_WhenRangelimitexceeded()
        {
            int range = 10;

            var Result = _ArticleController.GetTrendingArticles(range).Result as ObjectResult;
            Assert.Equal(400, Result?.StatusCode);
        }
        [Fact]
        public void GetTrendingArticles_ShouldReturnStatusCode500_WhenExceptionIsThrown()
        {

            _ArticleService.Setup(obj => obj.GetTrendingArticles()).Throws(new Exception());

            var Result = _ArticleController.GetTrendingArticles().Result as ObjectResult;

            Assert.Equal(500, Result?.StatusCode);
        }
    }
}
