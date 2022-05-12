using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using AspireOverflow.Models;


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
            else if (query.IsActive != true) throw new ValidationException("IsActive must be true");
            else if (query.IsSolved != false) throw new ValidationException("IsSolved must be false");

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
            else if (article.ArticleStatusID <= 0 && article.ArticleStatusID > 2) throw new ValidationException("ArticlestatusID must be less than 2");
            else return true;
        }

        public static bool ValidateArticleComment(ArticleComment Comment)
        {


            if (Comment == null) throw new ValidationException("Comment should not be null");
            else if (Comment.CreatedBy <= 0) throw new ValidationException("CreatedBy  must be greater than 0");
            else if (Comment.ArticleId <= 0) throw new ValidationException("Article Id  must be greater than 0");
            else if (String.IsNullOrEmpty(Comment.Comment)) throw new ValidationException("Comment cannot be null or empty");

            else return true;
        }





        public static bool ValidateUser(User user)
        {
            


            if (user == null) throw new ValidationException("User should not be null");
            else if (user.VerifyStatusID != 3) throw new ValidationException($"VerifyStatus must be 3  VerifyStatusId:{user.VerifyStatusID}");
            else if (user.IsReviewer != false) throw new ValidationException($"IsReviewer must be false");
            else if (user.GenderId <= 0 && user.GenderId > 2) throw new ValidationException($"Gender ID must be 1 or 2");
            else if (user.UserRoleId != 2) throw new ValidationException($"UserRoleId must be equal to 2 UserRole:{user.UserRoleId}");
            else if(!ValidateUserCredentials(user.EmailAddress,user.Password)) return false;
            else return true;
        }



        public static bool ValidateSpam(Spam spam)
        {
            if (spam == null) throw new ValidationException("Spam should not be null");
            else if (spam.QueryId <= 0) throw new ValidationException("Query Id must be greater than 0");
            else if (spam.UserId <= 0) throw new ValidationException("UserId must be greater than 0");
            else if (spam.VerifyStatusID <= 0 && spam.VerifyStatusID > 3)
            throw new ValidationException("Verify Status Id must be between 0 and 3");
            else if (String.IsNullOrEmpty(spam.Reason)) throw new ValidationException("Spam Reason cannot be null or empty");
            else return true;
        }
 public static bool ValidateUserCredentials(String Email,String Password )
        {
            var mail = new Regex("^[a-zA-Z0-9+_.-]+@[a-zA-Z0-9.-]+$");
            var password = new Regex("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$");
            if (!mail.IsMatch(Email)) throw new ValidationException($"Email format is incorrect EmailEntered:{ Email }");
            else if(!password.IsMatch(Password))throw new ValidationException($"Password must be at least 4 characters, no more than 8 characters, and must include at least one upper case letter, one lower case letter,  one numeric digit and one special character. Password Entered:{Password}");
            else return true;
        }



    }



}
