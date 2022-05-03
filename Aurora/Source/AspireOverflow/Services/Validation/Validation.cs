using System.ComponentModel.DataAnnotations;
using AspireOverflow.Models;
using AspireOverflow.Security;

namespace AspireOverflow.Services
{
    public class Validation
    {

        public static bool ValidateQuery(Query query)
        {


            if (query == null) throw new NullReferenceException("Query should not be null");
            else if (query.CreatedBy <= 0) throw new ValidationException("CreatedBy Id  must be greater than 0");
            else if (String.IsNullOrEmpty(query.Title)) throw new ValidationException("Title cannot be null or empty");
            else if (String.IsNullOrEmpty(query.Content)) throw new ValidationException("content cannot be null or empty");
            else if (query.Title.Length > 60) throw new ValidationException("Title length must be less than 60 charcter");

            else return true;
        }

        public static bool ValidateComment(QueryComment Comment)
        {


            if (Comment == null) throw new NullReferenceException("Comment should not be null");
            else if (Comment.CreatedBy <= 0) throw new ValidationException("CreatedBy  must be greater than 0");
            else if (Comment.QueryId <= 0) throw new ValidationException("Query Id  must be greater than 0");
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
            if (user == null) throw new NullReferenceException("User should not be null");
            else return true;
        }


        public static User SetUserDefaultPropertyValues(User user)
        {
            user.VerifyStatusID = 3;
            user.CreatedOn = DateTime.Now;
            user.IsReviewer = false;
            user.UserRoleId = 2;
            user.Password=PasswordHasherFactory.GetPasswordHasherFactory().HashPassword(user,user.Password);  //stores password with hashed password
            return user;
        }

            

    }



}
