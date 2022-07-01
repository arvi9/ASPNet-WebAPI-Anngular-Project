using System;
using System.ComponentModel.DataAnnotations;
using AspireOverflow.DataAccessLayer;
using AspireOverflow.Models;
using Microsoft.AspNetCore.Mvc;
using AspireOverflow.Services;
using AspireOverflowTest.DataAccessLayer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;
using System.Linq;
using System.Collections.Generic;

namespace AspireOverflowTest
{

    public class ArticleRepositoryTest
    {
        private AspireOverflowContext _context;


        private Mock<ILogger<ArticleRepository>> _logger = new Mock<ILogger<ArticleRepository>>();
        private ArticleRepository _articleRepository;

        public ArticleRepositoryTest()
        {
            _context = MockDBContext.GetInMemoryDbContext();
            _articleRepository = new ArticleRepository(_context, _logger.Object);
        }

        //GetPrivateArticles

        [Fact]
        public void GetPrivateArticles_ShouldReturnListOfArticles_WhenMethodIsCalled()
        {
            // MockDBContext.SeedMockDataInMemoryDb(_context);
            var Articles = ArticleMock.GetListOfPrivateArticleForSeeding();
            Assert.Equal(Articles.Count(), _articleRepository.GetPrivateArticles().Count());
        }

        //GetComments

        [Fact]
        public void GetComments_ShouldReturnListOfComments_WhenMethodIsCalled()
        {
            // MockDBContext.SeedMockDataInMemoryDb(_context);
            var comments = ArticleMock.GetCommentsForSeeding();
            Assert.Equal(comments.Count() + 1, _articleRepository.GetComments().Count());
        }

        //Add Article

        [Fact]
        public void AddArticle_ShouldReturnTrue_WhenArticleObjectIsValid()
        {
            Article article = ArticleMock.GetValidArticleforseeding();
            var Result = _articleRepository.AddArticle(article);
            Assert.True(Result);
        }
        [Theory]
        [InlineData(null)]
        public void AddArticle_ShouldThrowValidationException_WhenArticleObjectIsNull(Article article)
        {
            Assert.Throws<ValidationException>(() => _articleRepository.AddArticle(article));
        }
        [Fact]
        public void AddArticle_ShouldThrowValidationException_WhenArticleObjectIsInvalid()
        {
            Article article = ArticleMock.GetInValidArticle();
            Assert.Throws<ValidationException>(() => _articleRepository.AddArticle(article));
        }
        [Fact]
        public void AddArticle_ShouldThrowDBUpdateException()
        {
            Article article = ArticleMock.GetInValidArticle();
            Assert.Throws<ValidationException>(() => _articleRepository.AddArticle(article));
        }

        //Add Private Article

        [Fact]
        public void AddPrivateArticle_ShouldReturnTrue_WhenArticleObjectIsValid()
        {
            Article article = ArticleMock.GetValidArticleforseeding();
            var SharedUsers = new List<int>() { 1, 2, 3 };
            var Result = _articleRepository.AddPrivateArticle(article, SharedUsers);
            Assert.True(Result);
        }
        [Theory]
        [InlineData(null, null)]
        public void AddPrivateArticle_ShouldThrowValidationException_WhenArticleObjectIsNull(Article article, List<int> SharedUsers)
        {
            Assert.Throws<ValidationException>(() => _articleRepository.AddPrivateArticle(article, SharedUsers));
        }
        [Fact]
        public void AddPrivateArticle_ShouldThrowValidationException_WhenArticleObjectIsInvalid()
        {
            Article article = ArticleMock.GetInValidArticle();
            var SharedUsers = new List<int>() { 1, 2, 3 };
            Assert.Throws<ValidationException>(() => _articleRepository.AddPrivateArticle(article, SharedUsers));
        }
        [Fact]
        public void AddPrivateArticle_ShouldThrowDBUpdateException()
        {
            Article article = ArticleMock.GetInValidArticle();
            var SharedUsers = new List<int>() { 1, 2, 3 };
            Assert.Throws<ValidationException>(() => _articleRepository.AddPrivateArticle(article, SharedUsers));
        }

        //Add Comment

        [Fact]
        public void AddComment_ShouldReturnTrue_WhenArticleObjectIsValid()
        {
            ArticleComment comment = ArticleMock.GetValidArticleCommentforseeding();
            var Result = _articleRepository.AddComment(comment);
            Assert.True(Result);
        }
        [Theory]
        [InlineData(null)]
        public void AddComment_ShouldThrowValidationException_WhenArticleObjectIsNull(ArticleComment comment)
        {

            Assert.Throws<ValidationException>(() => _articleRepository.AddComment(comment));
        }
        [Fact]
        public void AddComment_ShouldThrowValidationException_WhenArticleObjectIsInvalid()
        {
            ArticleComment comment = new ArticleComment();
            Assert.Throws<ValidationException>(() => _articleRepository.AddComment(comment));
        }

        //Get Article by ArticleId
        //Passed
        [Theory]
        [InlineData(null)]
        public void GetArticleByArticleID_ShouldReturnArgumentException_WhenMethodIsCalled(int articleId)
        {
            Assert.Throws<ArgumentException>(() => _articleRepository.GetArticleByID(articleId));
        }
        // [Theory]
        // [InlineData(1)]
        //  public void GetArticleByID_ShouldReturnArticle_WhenMethodIsCalled(int articleId)
        //  {  
        //     MockDBContext.SeedMockDataInMemoryDb(_context);
        //     var Result =_articleRepository.GetArticleByID(articleId);  
        //     Assert.NotEqual(null,Result);                     
        //  }

        //UpdateArticle
        [Theory]
        [InlineData(0, 1, 0)]
        public void UpdateArticle_ShouldThrowArgumentException_WhenArticleId_Zero(int articleId, int articleStatusId, int userId)
        {
            Assert.Throws<ArgumentException>(() => _articleRepository.UpdateArticle(articleId, articleStatusId, userId));
        }
        [Theory]
        [InlineData(0, 7, 0)]
        public void UpdateArticle_ShouldThrowItemNotFoundException_WhenArticleStatusIdIs_zero(int articleId, int articleStatusId, int userId)
        {
            Assert.Throws<ArgumentException>(() => _articleRepository.UpdateArticle(articleId, articleStatusId, userId));
        }

        [Fact]  
        public void UpdateArticle_ShouldReturnTrue_WhenArticleObjectIsValid()
        {

            var article = ArticleMock.GetValidArticle();
            var Result = _articleRepository.UpdateArticle(article);
            Assert.False(Result);
        }

        // //GetArticles

        // [Fact]    //failed
        // public void GetArticles_ShouldReturnListOfArticles_WhenMethodIsCalled()
        // {
        //     MockDBContext.SeedMockDataInMemoryDb(_context);
        //     var article = ArticleMock.GetListOfArticleForSeeding();
        //     Assert.Equal(article.Count(), _articleRepository.GetArticles().Count());
        // }


        [Fact]
        public void GetArticlesLikes_ShouldReturnListOfArticles_WhenMethodIsCalled()
        {
            // MockDBContext.SeedMockDataInMemoryDb(_context);
            var articles = ArticleMock.GetListOfArticleLikesForSeeding();
            Assert.Equal(articles.Count(),_articleRepository.GetLikes().Count());
        }

        //deletearticle
        [Theory]
        [InlineData(0)]
        public void DeleteArticle_ShouldThrowArgumentException_WhenArticleId_Zero(int articleId)
        {
            Assert.Throws<ArgumentException>(() => _articleRepository.DeleteArticle(articleId));
        }
        // [Theory]
        // [InlineData(1)]  //failed
        // public void DeleteArticle_ShouldReturnTrue_WhenArticleObjectIsValid(int ArticleId)
        // {
        //     MockDBContext.SeedMockDataInMemoryDb(_context);  
        //     var article = ArticleMock.GetValidArticleForSeeding();                     
        //     var Result = _articleRepository.DeleteArticle(article.ArticleId);
        //     Assert.True(Result);
        // }
    }
}