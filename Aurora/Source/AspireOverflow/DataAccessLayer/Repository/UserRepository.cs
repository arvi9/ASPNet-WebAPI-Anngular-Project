
using AspireOverflow.Models;

using AspireOverflow.Services;
using AspireOverflow.DataAccessLayer.Interfaces;
using AspireOverflow.CustomExceptions;
using Microsoft.EntityFrameworkCore;

namespace AspireOverflow.DataAccessLayer
{

    public class UserRepository :IUserRepository
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
                _logger.LogError(HelperService.LoggerMessage("UserRepository", "CreateUser(User user)", exception, user));
                return false;

            }

        }

        //Admin rejected users only be deleted
        public bool RemoveUser(int UserId)
        {
           if (UserId <= 0) throw new ArgumentException($"User Id must be greater than 0 where UserId:{UserId}");
            try
            {   var User_NotVerified =GetUserByID(UserId);
                if(User_NotVerified.VerifyStatusID==3){
                _context.Users.Remove(User_NotVerified);
                _context.SaveChanges();
                }
                return true;
            }
            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage("UserRepository", "RemoveUser(int UserId)", exception, UserId));
                return false;

            }

        }
        public User GetUserByID(int UserId)
        {
            if (UserId <= 0) throw new ArgumentException($"User Id must be greater than 0 where UserId:{UserId}");
            User user;
            try
            {
                user = GetUsers().Where(User =>User.UserId==UserId).First();
                return user != null ? user : throw new ItemNotFoundException($"There is no matching User data with UserID :{UserId}");
            }
            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage("UserRepository", "GetUserByID(int UserId)", exception, UserId));
                throw exception;
            }
        }


        public IEnumerable<User> GetUsers()
        {
            try
            {

                return _context.Users.Include("Designation").Include("UserRole").Include("Gender").Include("VerifyStatus").ToList();
            }
            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage("UserRepository", "GetUsers()", exception));
                throw exception;
            }
        }

        public bool UpdateUserByVerifyStatus(int UserId, int VerifyStatusID)
        {
           if (UserId <= 0) throw new ArgumentException($"User Id must be greater than 0 where UserId:{UserId}");
           if (VerifyStatusID <= 0 && VerifyStatusID > 3) throw new ArgumentException($"Verify Status Id must be greater than 0 where VerifyStatusId:{VerifyStatusID}");
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
                _logger.LogError(HelperService.LoggerMessage("UserRepository", "UpdateUserByVerifyStatus(int UserId, int VerifyStatusID)", exception, $"UserId : {UserId},VerifyStatusID :{VerifyStatusID}"));
                throw;

            }

        }

        public bool UpdateUserByReviewer(int UserId, bool IsReviewer)
        {
           if (UserId <= 0) throw new ArgumentException($"User Id must be greater than 0 where UserId:{UserId}");
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
                _logger.LogError(HelperService.LoggerMessage("UserRepository", "UpdateUserByReviewer(int UserId, bool IsReviewer)", exception, $"UserId : {UserId},IsReviewer :{IsReviewer}"));
                throw;

            }

        }

         public IEnumerable<Gender> GetGenders()
        {
            try
            {

                return _context.Genders.ToList();
            }
            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage("UserRepository", " GetGenders()", exception));
                throw exception;
            }
        }

         public IEnumerable<Designation> GetDesignations()
        {
            try
            {

                return _context.Designations.Include("Department").ToList();
            }
            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage("UserRepository", " GetDesignations()", exception));
                throw exception;
            }
        }

         public IEnumerable<Department> GetDepartments()
        {
            try
            {
                return _context.Departments.ToList();
            }
            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage("UserRepository", " GetDepartments()", exception));
                throw exception;
            }
        }


    }
}