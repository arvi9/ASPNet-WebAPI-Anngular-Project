using System.ComponentModel.DataAnnotations;
using AspireOverflow.DataAccessLayer.Interfaces;
using AspireOverflow.DataAccessLayer;
using AspireOverflow.Security;
using Microsoft.AspNetCore.Identity;
using AspireOverflow.Models;
using System.Linq;
using AspireOverflow.CustomExceptions;

namespace AspireOverflow.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository database;
        private readonly ILogger<UserService> _logger;
        public UserService(ILogger<UserService> logger, IUserRepository _userRepository)
        {
            _logger = logger;
            database = _userRepository;
        }


        //to create an user using user object.
        public bool CreateUser(User user)
        {
            Validation.ValidateUser(user);
            try
            {
                user.Password = PasswordHasherFactory.GetPasswordHasherFactory().HashPassword(user, user.Password);
                return database.CreateUser(user);
            }
            catch (ValidationException exception)
            {
                _logger.LogError(HelperService.LoggerMessage("UserService", "CreateUser(User user)", exception, user));
                throw;
            }
            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage("UserService", "CreateUser(User user)", exception, user));
                return false;
            }
        }


        //to remove an existing user using UserId.
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


        //to update the user as reviewer using UserId and IsReviewer.
        public bool UpdateUserByIsReviewer(int UserId, bool IsReviewer)
        {
            if (UserId <= 0) throw new ArgumentException($"User Id must be greater than 0 where UserId:{UserId}");
            try
            {
                return database.UpdateUserByReviewer(UserId, IsReviewer);
            }
            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage("UserService", "UpdateUserByIsReviewer(int UserId,bool IsReviewer)", exception, UserId));
                return false;
            }
        }


        //to get the user using their email and password.
        public User GetUser(string Email, string Password)  //Method used in Token Controller
        {
            if (Email == null || Password == null) throw new ArgumentException("Email or Password cannot be null");
            try
            {
                var Hasher = PasswordHasherFactory.GetPasswordHasherFactory();
                var User = database.GetUserByEmail(Email);
                return Hasher.VerifyHashedPassword(User, User.Password, Password) == PasswordVerificationResult.Success ? User : throw new ValidationException("Password doesn't match");
            }
            catch (ValidationException exception)
            {
                _logger.LogError(HelperService.LoggerMessage("ArticleService()", "GetUser(string Email, string Password)", exception, Email));
                throw;
            }
            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage("ArticleService()", "GetUser(string Email, string Password)", exception, Email));
                throw;
            }
        }


        //to get the user by UserID from database.
        public object GetUserByID(int UserID)
        {
            if (UserID <= 0) throw new ArgumentException($"User Id must be greater than 0 where UserID:{UserID}");
            try
            {
                var User = database.GetUserByID(UserID);
                return GetAnonymousUserObject(User);
            }
            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage("UserService", "GetUsersByID(int UserId)", exception, UserID));
                throw;
            }
        }


        //to get all the user from the database.
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


        //to get the users using VerifyStatusID.
        public IEnumerable<Object> GetUsersByVerifyStatus(int VerifyStatusID)
        {
            if (VerifyStatusID <= 0 || VerifyStatusID > 3) throw new ArgumentException("VerifyStatusId must be greater than 0 and less than 3");
            try
            {
                return database.GetUsersByVerifyStatusId(VerifyStatusID).Select(User => GetAnonymousUserObject(User));
            }
            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage("UserService", "GetUsersByVerifyStatus(int VerifyStatusID)", exception, VerifyStatusID));
                throw;
            }
        }


        //to fet the users using UserRoleID.
        public IEnumerable<Object> GetUsersByUserRoleID(int UserRoleID)
        {
            if (UserRoleID <= 0 || UserRoleID > 2) throw new ArgumentException($"User Role Id must be greater than 0 where UserRoleId:{UserRoleID}");
            try
            {
                return database.GetUsersByUserRoleID(UserRoleID).Select(User => GetAnonymousUserObject(User));
            }
            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage("UserService", "GetUsersByUserRoleID(int UserRoleID)", exception, UserRoleID));
                throw;
            }
        }


        //to change the status of the user uding UserID and VerifyStatusID.
        public bool ChangeUserVerificationStatus(int UserID, int VerifyStatusID)
        {
            if (UserID <= 0) throw new ArgumentException($"User Id must be greater than 0  where UserID:{UserID}");
            if (VerifyStatusID <= 0 || VerifyStatusID > 3) throw new ArgumentException($"Verify Status Id must be between 0 and 3 where VerifyStatusID:{VerifyStatusID}");
            try
            {
                return database.UpdateUserByVerifyStatus(UserID, VerifyStatusID);
            }
            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage("UserService", "ChangeUserVerificationStatus(int UserID, int VerifyStatusID)", exception, $"UserID:{UserID},VerifyStatusID:{VerifyStatusID}"));
                throw;
            }
        }


        //to get the user by IsReviewer.
        public IEnumerable<Object> GetUsersByIsReviewer(bool IsReviewer)
        {
            try
            {
                return database.GetUsersByIsReviewer(IsReviewer).Select(User => GetAnonymousUserObject(User));
            }
            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage("UserService", "GetUsersByIsReviewer(bool IsReviewer)", exception, IsReviewer));
                throw;
            }
        }

        //Gets the total count of the Users.
        public object GetCountOfUsers()
        {
            try
            {
                return database.GetCountOfUsers();
            }
            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage("UserService", "GetCountOfUsers()", exception));
                throw;
            }

        }
        //to get the department using DepartmentId.
        private string GetDepartmentByID(int DepartmentId)
        {
            if (DepartmentId <= 0) throw new ArgumentException($"User Id must be greater than 0 where DepartmentId:{DepartmentId}");
            try
            {
                var department = database.GetDepartments().FirstOrDefault(item => item.DepartmentId == DepartmentId);
                return department?.DepartmentName!;
            }
            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage("UserService", "GetDepartmentByID(int DepartmentId)", exception, DepartmentId));
                throw;
            }
        }


        //to get the gender from the database.
        public IEnumerable<Object> GetGenders()
        {
            try
            {
                var Genders = database.GetGenders().Select(item => new
                {
                    GenderId = item.GenderId,
                    Name = item.Name
                });
                return Genders;
            }
            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage("UserService", " GetGenders()", exception));
                throw;
            }
        }


        //to get the designation from the database.
        public IEnumerable<Object> GetDesignations()
        {
            try
            {
                var designations = database.GetDesignations().Select(item => new
                {
                    DesignationId = item.DesignationId,
                    Name = item.DesignationName,
                    DepartmentId = item.DepartmentId
                });
                return designations;
            }
            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage("UserService", " GetDesignations()", exception));
                throw;
            }
        }


        //to get the departments from the database.
        public IEnumerable<object> GetDepartments()
        {
            try
            {
                var Departments = database.GetDepartments().Select(item => new
                {
                    DepartmentId = item.DepartmentId,
                    Name = item.DepartmentName
                });
                return Departments;
            }
            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage("UserRepository", " GetDepartments()", exception));
                throw;
            }
        }
     //Returns anonymous object for the User object 
    private object GetAnonymousUserObject(User user)
    {
        return new
        {
            UserId = user.UserId,
            fullName = user.FullName,
            EmployeeId = user.AceNumber,
            Email = user.EmailAddress,
            DateOfBirth = user.DateOfBirth,
            Designation = user.Designation?.DesignationName,
            Department = user.Designation!.Department.DepartmentName,
            Gender = user.Gender?.Name,
            IsReviewer = user.IsReviewer
        };
    }
}
}