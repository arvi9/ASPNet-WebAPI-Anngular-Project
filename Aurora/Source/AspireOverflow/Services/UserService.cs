using System.ComponentModel.DataAnnotations;
using AspireOverflow.DataAccessLayer.Interfaces;
using AspireOverflow.DataAccessLayer;
using AspireOverflow.Security;
using Microsoft.AspNetCore.Identity;
using AspireOverflow.Models;
using System.Linq;
using AspireOverflow.CustomExceptions;
using System.Diagnostics;

namespace AspireOverflow.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository database;
        private readonly ILogger<UserService> _logger;
        private readonly Stopwatch _stopWatch = new Stopwatch();
        private bool IsTracingEnabled;
        public UserService(ILogger<UserService> logger, IUserRepository _userRepository)
        {
            _logger = logger;
            database = _userRepository;
             IsTracingEnabled = database.GetIsTraceEnabledFromConfiguration();
        }


        //to create an user using user object.
        public bool CreateUser(User user)
        {
             if (IsTracingEnabled) _stopWatch.Start();

            Validation.ValidateUser(user);
            try
            {
                user.Password = PasswordHasherFactory.GetPasswordHasherFactory().HashPassword(user, user.Password);
                user.CreatedOn=DateTime.UtcNow;
                user.UpdatedBy=null;
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
             finally
            {
                if (IsTracingEnabled)
                {
                    _stopWatch.Stop();
                    _logger.LogInformation($"Tracelog:UserService Elapsed Time for CreateUser(User user) - {_stopWatch.ElapsedMilliseconds}ms");
                }
            }
        }


        //to remove an existing user using UserId.
        public bool RemoveUser(int UserId)
        {
            if (IsTracingEnabled) _stopWatch.Start();

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
              finally
            {
                if (IsTracingEnabled)
                {
                    _stopWatch.Stop();
                    _logger.LogInformation($"Tracelog:UserService Elapsed Time for RemoveUser(int UserId) - {_stopWatch.ElapsedMilliseconds}ms");
                }
            }
        }


        //to update the user as reviewer using UserId and IsReviewer.
        public bool UpdateUserByIsReviewer(int UserId, bool IsReviewer,int UpdatedByUserId)
        {
            if (IsTracingEnabled) _stopWatch.Start();

            if (UserId <= 0) throw new ArgumentException($"User Id must be greater than 0 where UserId:{UserId}");
            try
            {
                return database.UpdateUserByReviewer(UserId, IsReviewer,UpdatedByUserId);
            }
            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage("UserService", "UpdateUserByIsReviewer(int UserId,bool IsReviewer,int UpdatedByUserId)", exception, UserId));
                return false;
            }
               finally
            {
                if (IsTracingEnabled)
                {
                    _stopWatch.Stop();
                    _logger.LogInformation($"Tracelog:UserService Elapsed Time for UpdateUserByIsReviewer(int UserId, bool IsReviewer,int UpdatedByUserId) - {_stopWatch.ElapsedMilliseconds}ms");
                }
            }
        }


        //to get the user using their email and password.
        public User GetUser(string Email, string Password)  //Method used in Token Controller
        {
            if (IsTracingEnabled) _stopWatch.Start();

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
               finally
            {
                if (IsTracingEnabled)
                {
                    _stopWatch.Stop();
                    _logger.LogInformation($"Tracelog:UserService Elapsed Time for GetUser(string Email, string Password) - {_stopWatch.ElapsedMilliseconds}ms");
                }
            }
        }


        //to get the user by UserID from database.
        public object GetUserByID(int UserID)
        {
             if (IsTracingEnabled) _stopWatch.Start();

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
             finally
            {
                if (IsTracingEnabled)
                {
                    _stopWatch.Stop();
                    _logger.LogInformation($"Tracelog:UserService Elapsed Time for GetUserByID(int UserID) - {_stopWatch.ElapsedMilliseconds}ms");
                }
            }
        }


        //to get all the user from the database.
        public IEnumerable<User> GetUsers()
        {
            if (IsTracingEnabled) _stopWatch.Start();

            try
            {
                return database.GetUsers();
            }
            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage("UserService", "GetUsers()", exception));
                throw;
            }
               finally
            {
                if (IsTracingEnabled)
                {
                    _stopWatch.Stop();
                    _logger.LogInformation($"Tracelog:UserService Elapsed Time for GetUsers() - {_stopWatch.ElapsedMilliseconds}ms");
                }
            }
        }


        //to get the users using VerifyStatusID.
        public IEnumerable<Object> GetUsersByVerifyStatus(int VerifyStatusID)
        {
            if (IsTracingEnabled) _stopWatch.Start();

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
             finally
            {
                if (IsTracingEnabled)
                {
                    _stopWatch.Stop();
                    _logger.LogInformation($"Tracelog:UserService Elapsed Time for GetUsersByVerifyStatus(int VerifyStatusID) - {_stopWatch.ElapsedMilliseconds}ms");
                }
            }
        }


        //to fet the users using UserRoleID.
        public IEnumerable<Object> GetUsersByUserRoleID(int UserRoleID)
        {
            if (IsTracingEnabled) _stopWatch.Start();

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
               finally
            {
                if (IsTracingEnabled)
                {
                    _stopWatch.Stop();
                    _logger.LogInformation($"Tracelog:UserService Elapsed Time for GetUsersByUserRoleID(int UserRoleID) - {_stopWatch.ElapsedMilliseconds}ms");
                }
            }
        }


        //to change the status of the user uding UserID and VerifyStatusID.
        public bool ChangeUserVerificationStatus(int UserID, int VerifyStatusID,int UpdatedByUserId)
        {
            if (IsTracingEnabled) _stopWatch.Start();

            if (UserID <= 0) throw new ArgumentException($"User Id must be greater than 0  where UserID:{UserID}");
            if (VerifyStatusID <= 0 || VerifyStatusID > 3) throw new ArgumentException($"Verify Status Id must be between 0 and 3 where VerifyStatusID:{VerifyStatusID}");
            try
            {
                return database.UpdateUserByVerifyStatus(UserID, VerifyStatusID,UpdatedByUserId);
            }
            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage("UserService", "ChangeUserVerificationStatus(int UserID, int VerifyStatusID,int UpdatedByUserId)", exception, $"UserID:{UserID},VerifyStatusID:{VerifyStatusID}"));
                throw;
            }
              finally
            {
                if (IsTracingEnabled)
                {
                    _stopWatch.Stop();
                    _logger.LogInformation($"Tracelog:UserService Elapsed Time for ChangeUserVerificationStatus(int UserID, int VerifyStatusID,int UpdatedByUserId) - {_stopWatch.ElapsedMilliseconds}ms");
                }
            }
        }


        //to get the user by IsReviewer.
        public IEnumerable<Object> GetUsersByIsReviewer(bool IsReviewer)
        {
            if (IsTracingEnabled) _stopWatch.Start();

            try
            {
                return database.GetUsersByIsReviewer(IsReviewer).Select(User => GetAnonymousUserObject(User));
            }
            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage("UserService", "GetUsersByIsReviewer(bool IsReviewer)", exception, IsReviewer));
                throw;
            }
             finally
            {
                if (IsTracingEnabled)
                {
                    _stopWatch.Stop();
                    _logger.LogInformation($"Tracelog:UserService Elapsed Time for GetUsersByIsReviewer(bool IsReviewer) - {_stopWatch.ElapsedMilliseconds}ms");
                }
            }
        }

        //Gets the total count of the Users.
        public object GetCountOfUsers()
        {
            if (IsTracingEnabled) _stopWatch.Start();

            try
            {
                return database.GetCountOfUsers();
            }
            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage("UserService", "GetCountOfUsers()", exception));
                throw;
            }
            finally
            {
                if (IsTracingEnabled)
                {
                    _stopWatch.Stop();
                    _logger.LogInformation($"Tracelog:UserService Elapsed Time for GetCountOfUsers() - {_stopWatch.ElapsedMilliseconds}ms");
                }
            }

        }
        //to get the department using DepartmentId.
        private string GetDepartmentByID(int DepartmentId)
        {
            if (IsTracingEnabled) _stopWatch.Start();

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
             finally
            {
                if (IsTracingEnabled)
                {
                    _stopWatch.Stop();
                    _logger.LogInformation($"Tracelog:UserService Elapsed Time for GetDepartmentByID(int DepartmentId) - {_stopWatch.ElapsedMilliseconds}ms");
                }
            }
            
        }


        //to get the gender from the database.
        public IEnumerable<Object> GetGenders()
        {
            if (IsTracingEnabled) _stopWatch.Start();

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
             finally
            {
                if (IsTracingEnabled)
                {
                    _stopWatch.Stop();
                    _logger.LogInformation($"Tracelog:UserService Elapsed Time for  GetGenders() - {_stopWatch.ElapsedMilliseconds}ms");
                }
            }
        }


        //to get the designation from the database.
        public IEnumerable<Object> GetDesignations()
        {
            if (IsTracingEnabled) _stopWatch.Start();

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
            finally
            {
                if (IsTracingEnabled)
                {
                    _stopWatch.Stop();
                    _logger.LogInformation($"Tracelog:UserService Elapsed Time for GetDesignations() - {_stopWatch.ElapsedMilliseconds}ms");
                }
            }
        }


        //to get the departments from the database.
        public IEnumerable<object> GetDepartments()
        {
            if (IsTracingEnabled) _stopWatch.Start();
            
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
            finally
            {
                if (IsTracingEnabled)
                {
                    _stopWatch.Stop();
                    _logger.LogInformation($"Tracelog:UserService Elapsed Time for GetDepartments() - {_stopWatch.ElapsedMilliseconds}ms");
                }
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