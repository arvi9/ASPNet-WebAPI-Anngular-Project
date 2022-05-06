using System.ComponentModel.DataAnnotations;
using AspireOverflow.Models;
using AspireOverflow.Security;

namespace AspireOverflow.Services
{
    public class Validation
    {


        public static bool ValidateQuery(Query query)
        {


            if (query == null) throw new ValidationException("Query should not be null");
            else if (query.CreatedBy <= 0) throw new ValidationException("CreatedBy Id  must be greater than 0");
            else if (String.IsNullOrEmpty(query.Title)) throw new ValidationException("Title cannot be null or empty");
            else if (String.IsNullOrEmpty(query.Content)) throw new ValidationException("content cannot be null or empty");
            else if (query.Title.Length > 60) throw new ValidationException("Title length must be less than 60 charcter");
            else if(query.IsActive != true)throw new ValidationException("");

            else return true;
        }

        public static bool ValidateComment(QueryComment Comment)
        {


            if (Comment == null) throw new ValidationException("Comment should not be null");
            else if (Comment.CreatedBy <= 0) throw new ValidationException("CreatedBy  must be greater than 0");
            else if (Comment.QueryId <= 0) throw new ValidationException("Query Id  must be greater than 0");
            else if (String.IsNullOrEmpty(Comment.Comment)) throw new ValidationException("Comment cannot be null or empty");

            else return true;
        }

        public static bool ValidateArticle(Article article)
        {


            if (article == null) throw new ValidationException("Article should not be null");
            else if (article.CreatedBy <= 0) throw new ValidationException("CreatedBy Id  must be greater than 0");
            else if (String.IsNullOrEmpty(article.Title)) throw new ValidationException("Title cannot be null or empty");
            else if (String.IsNullOrEmpty(article.Content)) throw new ValidationException("content cannot be null or empty");
            else if (article.Title.Length > 100) throw new ValidationException("Title length must be less than 100 charcter");

            else return true;
        }

        public static bool ValidateArticleComment(ArticleComment Comment)
        {


            if (Comment == null) throw new ValidationException("Comment should not be null");
            else if (Comment.CreatedBy <= 0) throw new ValidationException("CreatedBy  must be greater than 0");
            //else if (Comment.ArticleId <= 0) throw new ValidationException("Article Id  must be greater than 0");
            else if (String.IsNullOrEmpty(Comment.Comment)) throw new ValidationException("Comment cannot be null or empty");

            else return true;
        }

        public static bool ValidateId(int Id)
        {
            return Id <= 0 ? throw new ArgumentOutOfRangeException("Id must be greater than 0") : true;
        }

        public static bool ValidateId(int FirstId, int SecondId)
        {
            if (FirstId <= 0) throw new ArgumentOutOfRangeException("Id must be greater than 0");
            if (SecondId <= 0) throw new ArgumentOutOfRangeException("Id must be greater than 0");
            else return true;
        }


        public static bool ValidateUser(User user)
        {
            if (user == null) throw new ValidationException("User should not be null");
            else if(user.VerifyStatusID != 3)throw new ValidationException($"VerifyStatus must be 3  VerifyStatusId:{user.VerifyStatusID}");
            else if( user.IsReviewer != false)throw new ValidationException($"IsReviewer must be false");
            else if(  user.UserRoleId != 2)throw new ValidationException($"IsReviewer must be false");
            else if(  user.UserRoleId != 2) throw new ValidationException($"UserRoleId must be equal to 2 UserRole:{user.UserRoleId}");
            else return true;
        }

        public static bool ValidateTitle(string Title)
        {
            if (String.IsNullOrEmpty(Title)) throw new ValidationException("Title cannot be null or empty");

            else return true;
        }
       

        public static Query SetUserDefaultPropertyValues(Query query)
        {

            query.CreatedOn = DateTime.Now;

           
            query.IsActive = true;
            query.IsSolved = false;
            return query;
        }
        public static Article SetUserDefaultPropertyValues(Article article)
        {

            article.CreatedOn = DateTime.Now;

            article.Title = article.Title.ToLower();
            article.ArticleStatusID = 1;
         
            return article;
        }



    }



}
