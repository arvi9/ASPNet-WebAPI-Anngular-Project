using System.ComponentModel.DataAnnotations;
using AspireOverflow.DataAccessLayer.Interfaces;
using AspireOverflow.DataAccessLayer;
using AspireOverflow.Security;
using Microsoft.AspNetCore.Identity;
using AspireOverflow.Models;


namespace AspireOverflow.Services
{


    public class UserService :IUserService
    {
        private static IUserRepository database;

        private static ILogger<UserService> _logger;

        public UserService(ILogger<UserService> logger,UserRepository _userRepository)
        {
            _logger = logger;
            database =_userRepository;

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

        public bool  UpdateUserByIsReviewer(int UserId,bool IsReviewer){
             if (UserId <= 0) throw new ArgumentException($"User Id must be greater than 0 where UserId:{UserId}");
            try{
                return database.UpdateUserByReviewer(UserId,IsReviewer);

            } catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage("UserService", "UpdateUserByIsReviewer(int UserId,bool IsReviewer)", exception, UserId));
                return false;
            }


        }

        public User GetUser(string Email, string Password)  //Method used in Token Controller
        {
            if(Email == null || Password ==null) throw new ArgumentNullException("Email or Password cannot be null");
            try
            {
                var Hasher = PasswordHasherFactory.GetPasswordHasherFactory();
                var User = GetUsers().Where(user => user.EmailAddress == Email).First();
                if(User ==null) throw new ValidationException("Invalid Email");
                return Hasher.VerifyHashedPassword(User, User.Password, Password) == PasswordVerificationResult.Success ? User : throw new InvalidDataException("Password doesn't match");
            }
            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage("ArticleService()", "GetUser(string Email, string Password)", exception, Email));

                throw exception;
            }
        }

                public object GetUserByID(int UserId)
        {
            if (UserId <= 0) throw new ArgumentException($"User Id must be greater than 0 where UserId:{UserId}");
            try
            {
                var User = database.GetUserByID(UserId);
                return new
                {
                    UserId = User.UserId,
                    fullName = User.FullName,
                    EmployeeId = User.AceNumber,
                    email = User.EmailAddress,
                    DateOfBirth = User.DateOfBirth,
                    Designation = User.Designation?.DesignationName,
                    Department = GetDepartmentByID(User.DesignationId),
                    Gender = User.Gender?.Name,
                    IsReviewer = User.IsReviewer

                };
            }
            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage("UserService", "GetUsersByID(int UserId)", exception, UserId));
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
                _logger.LogError(HelperService.LoggerMessage("UserService", "GetUsers()", exception));
                throw;
            }
        }

        public IEnumerable<Object> GetUsersByVerifyStatus(int VerifyStatusID)
        {
            try
            {
                return GetUsers().Where(User => User.VerifyStatusID == VerifyStatusID).Select(User => new
                {
                    UserId = User.UserId,
                    fullName = User.FullName,
                    EmployeeId = User.AceNumber,
                    Email = User.EmailAddress,
                    DateOfBirth = User.DateOfBirth,
                    Designation = User.Designation?.DesignationName,
                    Department = GetDepartmentByID(User.DesignationId),
                    Gender = User.Gender?.Name,
                    IsReviewer = User.IsReviewer

                });
            }
            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage("UserService", "GetUsersByVerifyStatus(int VerifyStatusID)", exception, VerifyStatusID));
                throw;
            }
        }



        public IEnumerable<Object> GetUsersByUserRoleID(int UserRoleID)
        {
            if (UserRoleID <= 0) throw new ArgumentException($"User Role Id must be greater than 0 where UserRoleId:{UserRoleID}");
            try
            {
                return GetUsers().Where(user => user.UserRoleId == UserRoleID && user.VerifyStatusID == 1).Select(User => new
                {
                    UserId = User.UserId,
                    fullName = User.FullName,
                    EmployeeId = User.AceNumber,
                    Email = User.EmailAddress,
                    DateOfBirth = User.DateOfBirth,
                    Designation = User.Designation?.DesignationName,
                    Department = GetDepartmentByID(User.DesignationId),
                    Gender = User.Gender?.Name,
                    IsReviewer = User.IsReviewer

                });
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



        public IEnumerable<Object> GetUsersByIsReviewer(bool IsReviewer)
        {
            try
            {
                return GetUsers().Where(user => user.IsReviewer == IsReviewer && user.VerifyStatusID == 1).Select(User => new
                {
                    UserId = User.UserId,
                    Name = User.FullName,
                    EmployeeId = User.AceNumber,
                    Email = User.EmailAddress,
                    DateOfBirth = User.DateOfBirth,
                    Designation = User.Designation?.DesignationName,
                    Department = GetDepartmentByID(User.DesignationId),
                    Gender = User.Gender?.Name,
                    IsReviewer = User.IsReviewer

                });
            }
            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage("UserService", "GetUsersByIsReviewer(bool IsReviewer)", exception, IsReviewer));
                throw;
            }
        }


        private string GetDepartmentByID(int DepartmentId)
        {
            if (DepartmentId <= 0) throw new ArgumentException($"User Id must be greater than 0 where DepartmentId:{DepartmentId}");
            try
            {
                var department = database.GetDepartments().Where(item => item.DepartmentId == DepartmentId).First();
                return department.DepartmentName;
               
            }
            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage("UserService", "GetDepartmentByID(int DepartmentId)", exception, DepartmentId));
                throw;
            }
        }

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
                throw exception;
            }
        }

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
                throw exception;
            }
        }

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
                throw exception;
            }
        }

 

    }

}