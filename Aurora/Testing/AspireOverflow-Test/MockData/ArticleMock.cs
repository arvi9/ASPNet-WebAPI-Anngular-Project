using System;
using System.Collections.Generic;
using AspireOverflow.Models;

namespace AspireOverflowTest
{
    static class ArticleMock
    {

        public static Article GetInValidArticle()
        {
            return new Article();
        }
        public static Article GetValidArticle()
        {
            return new Article()
            {
                ArticleId = 1,
                Title = "Sample Title",
                Content = "What is meant by Unit Testing?",
                Image = new byte[] { },
                ArticleStatusID = 1,
                ReviewerId = 1,
                CreatedBy = 1,
                ImageString = "iVBORw0KGgoAAAANSUhEUgAAAAUAAAAFCAYAAACNbyblAAAAHElEQVQI12P4//8/w38GIAXDIBKE0DHxgljNBAAO9TXL0Y4OHwAAAABJRU5ErkJggg==",
            };
        }

        public static List<Article> GetListOfArticle()
        {
            return new List<Article>()
            {
            new Article(){ArticleId=1,Title = "Sample Title", Content = "What is meant by Unit Testing?",Image = new byte[]{}, ArticleStatusID = 1,ReviewerId = 2, CreatedBy = 1},
            new Article(){ArticleId=2,Title = "Sample Title", Content = "What is meant by Unit Testing?",Image = new byte[]{}, ArticleStatusID = 2,ReviewerId = 2, CreatedBy = 2},
            new Article(){ArticleId=3,Title = "Sample Title", Content = "What is meant by Unit Testing?",Image = new byte[]{}, ArticleStatusID = 3,ReviewerId = 2, CreatedBy = 2},
            new Article(){ArticleId=4,Title = "Sample Title", Content = "What is meant by Unit Testing?",Image = new byte[]{}, ArticleStatusID = 4,ReviewerId = 2, CreatedBy = 4},
            new Article(){ArticleId=5,Title = "Sample Title", Content = "What is meant by Unit Testing?",Image = new byte[]{}, ArticleStatusID = 4,ReviewerId = 2, CreatedBy = 2},
            new Article(){ArticleId=6,Title = "Sample Title", Content = "What is meant by Unit Testing?",Image = new byte[]{}, ArticleStatusID = 2,ReviewerId = 2, CreatedBy = 3},
            new Article(){ArticleId=7,Title = "Sample Title", Content = "What is meant by Unit Testing?",Image = new byte[]{}, ArticleStatusID = 1,ReviewerId = 2, CreatedBy = 5},
            };
        }
        public static List<PrivateArticle> GetListofPrivateArticle()
        {
            return new List<PrivateArticle>()
          {
            new PrivateArticle(1,1),
          };

        }

        public static List<ArticleComment> GetValidArticleComment()
        {
            return new List<ArticleComment>()
            {
               new ArticleComment() {
                Comment="what is Query",
                CreatedBy=1,
                ArticleId=2}

            };
        }
        public static ArticleComment GetValidArticleComments()
        {
            return new ArticleComment()
            {
                Comment = "what is Query",
                CreatedBy = 1,
                ArticleId = 2
            };
        }
        public static List<ArticleComment> GetListOfArticleComments()
        {

            return new List<ArticleComment>();
        }

        public static ArticleLike GetValidArticleLike()
        {
            return new ArticleLike()
            {
                LikeId = 1,
                ArticleId = 1,
                UserId = 1
            };
        }

        public static List<ArticleLike> GetListOfArticleLikes()
        {
            return new List<ArticleLike>()
        {
            new ArticleLike(){LikeId=1,ArticleId=1,UserId=1},
            new ArticleLike(){LikeId=3,ArticleId=1,UserId=2},
        };
        }
        
        public static List<Article> GetListOfArticleForSeeding()
        {
            return new List<Article>(){
             new Article(){Title = "Sample Title", Content = "What is meant by Unit Testing?",Image = new byte[]{},ArticleStatusID = 4,ReviewerId = 2,CreatedBy = 1,IsPrivate= false },
              new Article(){Title = "Sample Title", Content = "What is meant by Unit Testing?",Image = new byte[]{},ArticleStatusID = 4,ReviewerId = 2,CreatedBy = 2,IsPrivate= true},
               new Article(){Title = "Sample Title", Content = "What is meant by Unit Testing?",Image = new byte[]{},ArticleStatusID = 4,ReviewerId = 2,CreatedBy = 1,IsPrivate= false},
                new Article(){Title = "Sample Title", Content = "What is meant by Unit Testing?",Image = new byte[]{},ArticleStatusID = 4,ReviewerId = 2,CreatedBy = 2,IsPrivate= true},
                 new Article(){Title = "Sample Title", Content = "What is meant by Unit Testing?",Image = new byte[]{},ArticleStatusID = 4,ReviewerId = 2,CreatedBy = 1,IsPrivate= false},
            };
        }
        public static List<PrivateArticle> GetListOfPrivateArticleForSeeding()
        {
            return new List<PrivateArticle>(){
                new PrivateArticle(1,2),
               };
        }

        public static List<User> GetListOfUsersForSeeding()
        {
            return new List<User>(){
                new User(){UserId=1,FullName="Mani Maran",AceNumber="ACE9898",EmailAddress="Mani.Venkat@aspiresys.com",GenderId=1,Password="88888888" },
                 new User(){UserId=2,FullName="Sriram",AceNumber="ACE9898",EmailAddress="Mani.Venkat@aspiresys.com",GenderId=1 ,Password="898989898"},

        };
        }
        public static List<ArticleComment> GetCommentsForSeeding()
        {
            return new List<ArticleComment>(){
                new ArticleComment() {
                Comment="what is Query",
                CreatedBy=1,
                ArticleId=1}
        };
        }
        public static ArticleComment GetValidArticleCommentforseeding()
        {
            return new ArticleComment()
            {
                Comment = "what is Query",
                CreatedBy = 1,
                ArticleId = 1

            };
        }
        public static Article GetValidArticleforseeding()
        {
            return new Article()
            {
                Title = "Sample Title",
                Content = "What is meant by Unit Testing?",
                Image = new byte[] { },
                ArticleStatusID = 1,
                ReviewerId = 1,
                CreatedBy = 1
            };
        }
        public static List<ArticleLike> GetListOfArticleLikesForSeeding()
        {
            return new List<ArticleLike>()
        {
new ArticleLike(){ArticleId=1,UserId=1},
new ArticleLike(){ArticleId=1,UserId=3},
new ArticleLike(){ArticleId=1,UserId=2},
        };
        }
        public static Article GetValidArticleForSeeding()
        {
            return new Article()
            {
                ArticleId=2,
                Title = "Sample Title",
                Content = "What is meant by Unit Testing?",
                Image = new byte[]{},
                ArticleStatusID = 1,
                ReviewerId = 1,
                CreatedBy = 1,
                  CreatedOn = new System.DateTime(),
                UpdatedOn=new System.DateTime(),
                UpdatedBy=1
                // ImageString = "iVBORw0KGgoAAAANSUhEUgAAAAUAAAAFCAYAAACNbyblAAAAHElEQVQI12P4//8/w38GIAXDIBKE0DHxgljNBAAO9TXL0Y4OHwAAAABJRU5ErkJggg==",
           
            };
        }
    }
}
