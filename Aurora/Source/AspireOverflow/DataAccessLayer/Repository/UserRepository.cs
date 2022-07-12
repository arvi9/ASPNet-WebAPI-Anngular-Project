using AspireOverflow.Models;
using AspireOverflow.Services;
using AspireOverflow.DataAccessLayer.Interfaces;
using AspireOverflow.CustomExceptions;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
namespace AspireOverflow.DataAccessLayer
{
    public class UserRepository : IUserRepository
    {
        private readonly AspireOverflowContext _context;
        private readonly ILogger<UserRepository> _logger;
        private readonly IConfiguration _configuration;

        public UserRepository(AspireOverflowContext context, ILogger<UserRepository> logger, IConfiguration configuration)
        {
            _context = context;
            _logger = logger;
            _configuration = configuration;
        }


        //to create an user using user object.
        public bool CreateUser(User User)
        {
            Validation.ValidateUser(User);
            try
            {           
                if ( _context.Users.Any(Item => Item.AceNumber == User.AceNumber)) throw new ValidationException("ACE Number Already Exists");
                if ( _context.Users.Any(Item => Item.EmailAddress == User.EmailAddress)) throw new ValidationException("Email Address Already Exists");
                _context.Users.Add(User);
                _context.SaveChanges();
                return true;
            }
            catch (ValidationException exception)
            {
                _logger.LogError(HelperService.LoggerMessage("UserRepository", "CreateUser(User user)", exception, User));
                throw;
            }
            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage("UserRepository", "CreateUser(User user)", exception, User));
                return false;
            }
        }


        //Admin rejected users only be deleted
        public bool RemoveUser(int UserId)
        {
            if (UserId <= 0) throw new ArgumentException($"User Id must be greater than 0 where UserId:{UserId}");
            try
            {
                var User_NotVerified = GetUserByID(UserId);
                if (User_NotVerified.VerifyStatusID == 3)
                {
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


        //to fetch the users using UserId.
        public User GetUserByID(int UserId)
        {
            if (UserId <= 0) throw new ArgumentException($"User Id must be greater than 0 where UserId:{UserId}");
            User? user;
            try
            {
                user = _context.Users.Include(e=>e.Designation).Include(e=>e.Gender).Include(e=>e.Designation!.Department).Include(e=>e.UserRole).FirstOrDefault(User => User.UserId == UserId);
                return user != null ? user : throw new ItemNotFoundException($"There is no matching User data with UserID :{UserId}");
            }
            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage("UserRepository", "GetUserByID(int UserId)", exception, UserId));
                throw;
            }
        }

        public User GetUserByEmail(string Email)
        {
            Validation.ValidateEmail(Email);
            try
            {
                var user = _context.Users.Include(e=>e.UserRole).Include(e=>e.VerifyStatus).FirstOrDefault(User => User.EmailAddress.ToLower() == Email.ToLower());
                return user != null ? user : throw new ValidationException($"There is no matching User data with Email :{Email}");
            }
            catch (ItemNotFoundException exception)
            {
                _logger.LogError(HelperService.LoggerMessage("UserRepository", "GetUserByID(int UserId)", exception, Email));
                throw;
            }
            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage("UserRepository", "GetUserByID(int Email)", exception, Email));
                throw;
            }
        }


       
        //to Update the user using UserId and VerifyStatusId.
        public bool UpdateUserByVerifyStatus(int UserId, int VerifyStatusID)
        {
            if (UserId <= 0) throw new ArgumentException($"User Id must be greater than 0 where UserId:{UserId}");
            if (VerifyStatusID <= 0 || VerifyStatusID > 3) throw new ArgumentException($"Verify Status Id must be greater than 0 where VerifyStatusId:{VerifyStatusID}");
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


        //to update the user using userId and IsReviewer.
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
       //to get the list of users.
        public IEnumerable<User> GetUsers()
        {
            try
            {
                return _context.Users.Include(e => e.Designation).Include(e => e.UserRole).Include(e => e.Gender).Include(e => e.VerifyStatus).ToList();
            }
            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage("UserRepository", "GetUsers()", exception));
                throw;
            }
        }

         public IEnumerable<User> GetUsersByVerifyStatusId(int VerifyStatusID)
        {
            if (VerifyStatusID <= 0 || VerifyStatusID > 3) throw new ArgumentException("VerifyStatusId must be greater than 0 and less than 3");
            try
            {
               return  _context.Users.Where(user =>user.VerifyStatusID==VerifyStatusID).Include(e => e.Designation).Include(e => e.Gender).Include(e=>e.Designation!.Department).ToList();
          
            }catch(Exception exception){
                _logger.LogError(HelperService.LoggerMessage("UserRepository", "GetUsersByVerifyStatus(int VerifyStatusID)", exception,VerifyStatusID));
                throw;

            }

        }
           public IEnumerable<User> GetUsersByUserRoleID(int UserRoleID)
        {
            if (UserRoleID <= 0 || UserRoleID > 2) throw new ArgumentException($"User Role Id must be greater than 0 where UserRoleId:{UserRoleID}");
            try
            {
                return _context.Users.Where(user => user.UserRoleId == UserRoleID && user.VerifyStatusID == 1).Include(e => e.Designation).Include(e => e.Gender).Include(e=>e.Designation!.Department);
               
            }
            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage("UserRepository", "GetUsersByUserRoleID(int UserRoleID)", exception, UserRoleID));
                throw;
            }
        }

           public IEnumerable<User> GetUsersByIsReviewer(bool IsReviewer)
        {
            try
            {
                return _context.Users.Where(user => user.IsReviewer == IsReviewer && user.VerifyStatusID == 1).Include(e => e.Designation).Include(e => e.Gender).Include(e=>e.Designation!.Department);
            }
            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage("UserRepository", "GetUsersByIsReviewer(bool IsReviewer)", exception, IsReviewer));
                throw;
            }
        }
           public object GetCountOfUsers()
        { 
            try
            {
                return new{
                    TotalNumberOfUsers = _context.Users.Count(),
                    UsersToBeVerified = _context.Users.Count(item => item.VerifyStatusID == 3),
                    VerifiedUsers=_context.Users.Count(item => item.VerifyStatusID == 1),
                    NumberOfReviewers = _context.Users.Count(item => item.IsReviewer),            
                };
            }
            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage("UserRepository", "GetCountOfUsers()", exception));
                throw;
            }
        }

        //to Get the Genders 
        public IEnumerable<Gender> GetGenders()
        {
            try
            {
                return _context.Genders.ToList();
            }
            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage("UserRepository", " GetGenders()", exception));
                throw;
            }
        }


        //to get the designation of the user.
        public IEnumerable<Designation> GetDesignations()
        {
            try
            {
                return _context.Designations.Include(e => e.Department).ToList();
            }
            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage("UserRepository", " GetDesignations()", exception));
                throw;
            }
        }


        //to get the department of the user.
        public IEnumerable<Department> GetDepartments()
        {
            try
            {
                return _context.Departments.ToList();
            }
            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage("UserRepository", " GetDepartments()", exception));
                throw;
            }
        }

        private int GetRange()
        {
            try
            {
                var Duration = _configuration["Data_Fetching_Duration:In_months"];
                return Duration != null ? Convert.ToInt32(Duration) : throw new Exception("Data_Fetching_Duration:In_months-> value is Invalif  in AppSettings.json ");
            }
            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage("UserRepository", " GetDuration()", exception));
                throw;

            }
        }
    }
}