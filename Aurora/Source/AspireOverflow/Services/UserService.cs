using AspireOverflow.DataAccessLayer.Interfaces;
using AspireOverflow.DataAccessLayer;
using AspireOverflow.Security;
using Microsoft.AspNetCore.Identity;
using AspireOverflow.Models;

namespace AspireOverflow.Services
{


    public class UserService : IUserService
    {
        private static IUserRepository database;

        private static ILogger<UserService> _logger;

        public UserService(ILogger<UserService> logger)
        {
            _logger = logger;
            database = UserRepositoryFactory.GetUserRepositoryObject(logger);

        }
        public bool CreateUser(User user)
        {
            Validation.ValidateUser(user);
            try
            {
                 user.Password = PasswordHasherFactory.GetPasswordHasherFactory().HashPassword(user, user.Password);
           
                return database.CreateUser(user);
            }
            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage("UserService", "CreateUser(User user)", exception, user));
                return false;
            }
        }

        public bool RemoveUser(int UserId)
        {
           if (UserId <= 0) throw new ArgumentException($"User Id must be greater than 0 where UserId:{UserId}");
            try
            {
                return database.RemoveUser(UserId);
            }
            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage("UserService", "RemoveUser(int UserId)", exception, UserId));
                return false;
            }

        }

        public  User GetUser(string Email, string Password)  //Method used in Token Controller
        {
            try
            {
                var Hasher = PasswordHasherFactory.GetPasswordHasherFactory();
                var User = GetUsers().Where(user => user.EmailAddress == Email).First();
                return Hasher.VerifyHashedPassword(User, User.Password, Password) == PasswordVerificationResult.Success ? User : throw new InvalidDataException("Password doesn't match");
            }
            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage("ArticleService()","GetUser(string Email, string Password)",exception,Email));
              
                throw exception;
            }
        }
        public IEnumerable<User> GetUsers()
        {
            try
            {
                return database.GetUsers();
            }
            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage("UserService", "GetUsers()", exception));
                throw;
            }
        }

        public IEnumerable<User> GetUsersByVerifyStatus(int VerifyStatusID)
        {
            try
            {
                return GetUsers().Where(User => User.VerifyStatusID == VerifyStatusID);
            }
            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage("UserService", "GetUsersByVerifyStatus(int VerifyStatusID)", exception, VerifyStatusID));
                throw;
            }
        }

        public User GetUsersByID(int UserId)
        {
            if (UserId <= 0) throw new ArgumentException($"User Id must be greater than 0 where UserId:{UserId}");
            try
            {
                return database.GetUserByID(UserId);
            }
            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage("UserService", "GetUsersByID(int UserId)", exception, UserId));
                throw;
            }
        }

        public IEnumerable<User> GetUsersByUserRoleID(int UserRoleID)
        {
            if (UserRoleID <= 0) throw new ArgumentException($"User Role Id must be greater than 0 where UserRoleId:{UserRoleID}");
            try
            {
                return GetUsersByVerifyStatus(1).Where(user => user.UserRoleId == UserRoleID);
            }
            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage("UserService", "GetUsersByUserRoleID(int UserRoleID)", exception, UserRoleID));

                throw;
            }

        }

        public bool ChangeUserVerificationStatus(int UserId, int VerifyStatusID)
        {
            if (UserId <= 0) throw new ArgumentException($"User Id must be greater than 0  where UserId:{UserId}");
            if (VerifyStatusID <= 0 && VerifyStatusID > 3) throw new ArgumentException($"Verify Status Id must be between 0 and 3 where VerifyStatusID:{VerifyStatusID}");
            try
            {
                return database.UpdateUserByVerifyStatus(UserId, VerifyStatusID);
            }
            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage("UserService", "ChangeUserVerificationStatus(int UserId, int VerifyStatusID)", exception, $"UserId:{UserId},VerifyStatusID:{VerifyStatusID}"));
                throw exception;
            }
        }



        public IEnumerable<User> GetUsersByIsReviewer(bool IsReviewer)
        {
            try
            {
                return GetUsersByVerifyStatus(1).Where(user => user.IsReviewer == IsReviewer);
            }
            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage("UserService", "GetUsersByIsReviewer(bool IsReviewer)", exception, IsReviewer));
                throw;
            }
        }

    }

}