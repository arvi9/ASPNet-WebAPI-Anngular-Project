
using AspireOverflow.Models;

using AspireOverflow.Services;
using AspireOverflow.DataAccessLayer.Interfaces;
using AspireOverflow.CustomExceptions;

namespace AspireOverflow.DataAccessLayer
{

    public class UserRepository 
    {
        private AspireOverflowContext _context;

        private ILogger<UserService> _logger;
        public UserRepository(AspireOverflowContext context, ILogger<UserService> logger)
        {
            _context = context ?? throw new NullReferenceException("Context can't be Null");
            _logger = logger ?? throw new NullReferenceException("logger can't be Null"); ;


        }




        public bool CreateUser(User user)
        {
            Validation.ValidateUser(user);
            try
            {
                _context.Users.Add(user);
                _context.SaveChanges();
                return true;
            }
            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage(nameof(UserRepository), nameof(CreateUser), exception, user));
                throw;

            }

        }

        //Admin rejected users only be deleted
        public bool RemoveUser(int UserId)
        {
            Validation.ValidateId(UserId);
            try
            {   var User_NotVerified =GetUserByID(UserId);
                if(User_NotVerified.VerifyStatusID==3)_context.Users.Remove(User_NotVerified);
                _context.SaveChanges();
                return true;
            }
            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage(nameof(UserRepository), nameof(RemoveUser), exception, UserId));
                throw;

            }

        }
        public User GetUserByID(int UserId)
        {
            Validation.ValidateId(UserId);
            User user;
            try
            {
                user = _context.Users.Find(UserId);
                return user != null ? user : throw new ItemNotFoundException($"There is no matching User data with UserID :{UserId}");
            }
            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage(nameof(UserRepository), nameof(GetUserByID), exception, UserId));
                throw exception;
            }
        }


        public IEnumerable<User> GetUsers()
        {
            try
            {

                return _context.Users;
            }
            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage(nameof(UserRepository), nameof(GetUsers), exception));
                throw exception;
            }
        }

        public bool UpdateUserByVerifyStatus(int UserId, int VerifyStatusID)
        {
            Validation.ValidateId(UserId, VerifyStatusID);
            try
            {
                var ExistingUser = GetUserByID(UserId);
                ExistingUser.VerifyStatusID = VerifyStatusID;
                _context.Users.Update(ExistingUser);
                _context.SaveChanges();
                return true;
            }
            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage(nameof(UserRepository), nameof(UpdateUserByVerifyStatus), exception, $"UserId : {UserId},VerifyStatusID :{VerifyStatusID}"));
                throw;

            }

        }

        public bool UpdateUserByReviewer(int UserId, bool IsReviewer)
        {
            Validation.ValidateId(UserId);
            try
            {
                var ExistingUser = GetUserByID(UserId);
                ExistingUser.IsReviewer = IsReviewer;
                _context.Users.Update(ExistingUser);
                _context.SaveChanges();
                return true;
            }
            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage(nameof(UserRepository), nameof(UpdateUserByReviewer), exception, $"UserId : {UserId},IsReviewer :{IsReviewer}"));
                throw;

            }

        }


    }
}