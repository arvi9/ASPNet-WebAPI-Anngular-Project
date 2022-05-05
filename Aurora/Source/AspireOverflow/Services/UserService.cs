using AspireOverflow.DataAccessLayer.Interfaces;
using AspireOverflow.DataAccessLayer;
using AspireOverflow.Security;

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
                _logger.LogError(HelperService.LoggerMessage(nameof(UserService), nameof(CreateUser), exception, user));
                throw exception;
            }
        }

        public bool RemoveUser(int UserId)
        {
           if (UserId <= 0) throw new ArgumentOutOfRangeException($"User Id must be greater than 0 where UserId:{UserId}");
            try
            {
                return database.RemoveUser(UserId);
            }
            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage(nameof(UserService), nameof(RemoveUser), exception, UserId));
                throw;
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
                _logger.LogError(HelperService.LoggerMessage(nameof(UserService), nameof(GetUsers), exception));
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
                _logger.LogError(HelperService.LoggerMessage(nameof(UserService), nameof(GetUsersByVerifyStatus), exception, VerifyStatusID));
                throw;
            }
        }

        public User GetUsersByID(int UserId)
        {
            if (UserId <= 0) throw new ArgumentOutOfRangeException($"User Id must be greater than 0 where UserId:{UserId}");
            try
            {
                return database.GetUserByID(UserId);
            }
            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage(nameof(UserService), nameof(GetUsersByID), exception, UserId));
                throw;
            }
        }

        public IEnumerable<User> GetUsersByUserRoleID(int UserRoleID)
        {
            if (UserRoleID <= 0) throw new ArgumentOutOfRangeException($"User Role Id must be greater than 0 where UserRoleId:{UserRoleID}");
            try
            {
                return GetUsersByVerifyStatus(1).Where(user => user.UserRoleId == UserRoleID);
            }
            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage(nameof(UserService), nameof(GetUsersByUserRoleID), exception, UserRoleID));

                throw;
            }

        }

        public bool ChangeUserVerificationStatus(int UserId, int VerifyStatusID)
        {
            if (UserId <= 0) throw new ArgumentOutOfRangeException($"User Id must be greater than 0  where UserId:{UserId}");
            if (VerifyStatusID <= 0 && VerifyStatusID > 3) throw new ArgumentOutOfRangeException($"Verify Status Id must be between 0 and 3 where VerifyStatusID:{VerifyStatusID}");
            try
            {
                return database.UpdateUserByVerifyStatus(UserId, VerifyStatusID);
            }
            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage(nameof(UserService), nameof(ChangeUserVerificationStatus), exception, $"UserId:{UserId},VerifyStatusID:{VerifyStatusID}"));
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
                _logger.LogError(HelperService.LoggerMessage(nameof(UserService), nameof(GetUsersByIsReviewer), exception, IsReviewer));
                throw;
            }
        }

    }

}