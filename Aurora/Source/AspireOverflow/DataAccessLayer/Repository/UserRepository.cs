using AspireOverflow.Models;
using AspireOverflow.Services;
using AspireOverflow.DataAccessLayer.Interfaces;
using AspireOverflow.CustomExceptions;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

namespace AspireOverflow.DataAccessLayer
{
    public class UserRepository : IUserRepository
    {
        private readonly AspireOverflowContext _context;
        private readonly ILogger<UserRepository> _logger;
        private readonly IConfiguration _configuration;
        private readonly Stopwatch _stopWatch =new Stopwatch();
        private bool IsTracingEnabled;

        public UserRepository(AspireOverflowContext context, ILogger<UserRepository> logger, IConfiguration configuration)
        {
            _context = context;
            _logger = logger;
            _configuration = configuration;
            IsTracingEnabled = GetIsTraceEnabledFromConfiguration();
        }


        //to create an user using user object.
        public bool CreateUser(User User)
        {
            if(IsTracingEnabled) _stopWatch.Start();
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
             finally
            {
                if(IsTracingEnabled)
                {
                _stopWatch.Stop();
                _logger.LogInformation($"Tracelog:UserRepository Elapsed Time for CreateUser(User user) - {_stopWatch.ElapsedMilliseconds}ms");
                }
            }
        }


        //Admin rejected users only be deleted
        public bool RemoveUser(int UserId)
        {
            if(IsTracingEnabled) _stopWatch.Start();
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
            finally
            {
                if(IsTracingEnabled)
                {
                _stopWatch.Stop();
                _logger.LogInformation($"Tracelog:UserRepository Elapsed Time for RemoveUser(int UserId) - {_stopWatch.ElapsedMilliseconds}ms");
                }
            }
        }


        //to fetch the users using UserId.
        public User GetUserByID(int UserId)
        {
            if(IsTracingEnabled) _stopWatch.Start();
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
            finally
            {
                if(IsTracingEnabled)
                {
                _stopWatch.Stop();
                _logger.LogInformation($"Tracelog:UserRepository Elapsed Time for GetUserByID(int UserId) - {_stopWatch.ElapsedMilliseconds}ms");
                }
            }
        }

        //Gets the user by their Email.
        public User GetUserByEmail(string Email)
        {
            if(IsTracingEnabled) _stopWatch.Start();
            Validation.ValidateEmail(Email);
            try
            {
                var user = _context.Users.Include(e=>e.UserRole).Include(e=>e.VerifyStatus).FirstOrDefault(User => User.EmailAddress.ToLower() == Email.ToLower());
                return user != null ? user : throw new ValidationException($"There is no matching User data with Email :{Email}");
            }
            catch (ItemNotFoundException exception)
            {
                _logger.LogError(HelperService.LoggerMessage("UserRepository", "GetUserByID(int Email)", exception, Email));
                throw;
            }
            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage("UserRepository", "GetUserByID(int Email)", exception, Email));
                throw;
            }
            finally
            {
                if(IsTracingEnabled)
                {
                _stopWatch.Stop();
                _logger.LogInformation($"Tracelog:UserRepository Elapsed Time for GetUserByID(int Email) - {_stopWatch.ElapsedMilliseconds}ms");
                }
            }
        }


       
        //to Update the user using UserId and VerifyStatusId.
        public bool UpdateUserByVerifyStatus(int UserId, int VerifyStatusID,int UpdatedByUserId)
        {
            if(IsTracingEnabled) _stopWatch.Start();
            if (UserId <= 0) throw new ArgumentException($"User Id must be greater than 0 where UserId:{UserId}");
            if (VerifyStatusID <= 0 || VerifyStatusID > 3) throw new ArgumentException($"Verify Status Id must be greater than 0 where VerifyStatusId:{VerifyStatusID}");
            try
            {
                var ExistingUser = GetUserByID(UserId);
                ExistingUser.VerifyStatusID = VerifyStatusID;
                ExistingUser.UpdatedOn=DateTime.UtcNow;
                ExistingUser.UpdatedBy=UpdatedByUserId;
                _context.Users.Update(ExistingUser);
                _context.SaveChanges();
                return true;
            }
            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage("UserRepository", "UpdateUserByVerifyStatus(int UserId, int VerifyStatusID)", exception, $"UserId : {UserId},VerifyStatusID :{VerifyStatusID}"));
                throw;
            }
            finally
            {
                if(IsTracingEnabled)
                {
                _stopWatch.Stop();
                _logger.LogInformation($"Tracelog:UserRepository Elapsed Time for GetIsTraceEnabledFromConfiguration() - {_stopWatch.ElapsedMilliseconds}ms");
                }
            }
        }


        //to update the user using userId and IsReviewer.
        public bool UpdateUserByReviewer(int UserId, bool IsReviewer,int UpdatedByUserId)
        {
            if(IsTracingEnabled) _stopWatch.Start();
            if (UserId <= 0) throw new ArgumentException($"User Id must be greater than 0 where UserId:{UserId}");
            try
            {
                var ExistingUser = GetUserByID(UserId);
                ExistingUser.IsReviewer = IsReviewer;
                ExistingUser.UpdatedBy=UpdatedByUserId;
                ExistingUser.UpdatedOn=DateTime.UtcNow;
                _context.Users.Update(ExistingUser);
                _context.SaveChanges();
                return true;
            }
            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage("UserRepository", "UpdateUserByReviewer(int UserId, bool IsReviewer)", exception, $"UserId : {UserId},IsReviewer :{IsReviewer}"));
                throw;
            }
            finally
            {
                if(IsTracingEnabled)
                {
                _stopWatch.Stop();
                _logger.LogInformation($"Tracelog:UserRepository Elapsed Time for GetIsTraceEnabledFromConfiguration() - {_stopWatch.ElapsedMilliseconds}ms");
                }
            }
        }

       //to get the list of users.
        public IEnumerable<User> GetUsers()
        {
            if(IsTracingEnabled) _stopWatch.Start();
            try
            {
                return _context.Users.Include(e => e.Designation).Include(e => e.UserRole).Include(e => e.Gender).Include(e => e.VerifyStatus).ToList();
            }
            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage("UserRepository", "GetUsers()", exception));
                throw;
            }
            finally
            {
                if(IsTracingEnabled)
                {
                _stopWatch.Stop();
                _logger.LogInformation($"Tracelog:UserRepository Elapsed Time for GetIsTraceEnabledFromConfiguration() - {_stopWatch.ElapsedMilliseconds}ms");
                }
            }
        }

        //Get the user list using VerifyStatusID.
         public IEnumerable<User> GetUsersByVerifyStatusId(int VerifyStatusID)
        {
            if(IsTracingEnabled) _stopWatch.Start();
            //throws exception when the VerifyStatusID is not inbetweeen 0 and 3
            if (VerifyStatusID <= 0 || VerifyStatusID > 3) throw new ArgumentException("VerifyStatusId must be greater than 0 and less than 3");
            try
            {
               return  _context.Users.Where(user =>user.VerifyStatusID==VerifyStatusID).Include(e => e.Designation).Include(e => e.Gender).Include(e=>e.Designation!.Department).ToList();
          
            }catch(Exception exception){
                _logger.LogError(HelperService.LoggerMessage("UserRepository", "GetUsersByVerifyStatus(int VerifyStatusID)", exception,VerifyStatusID));
                throw;

            }
            finally
            {
                if(IsTracingEnabled)
                {
                _stopWatch.Stop();
                _logger.LogInformation($"Tracelog:UserRepository Elapsed Time for GetIsTraceEnabledFromConfiguration() - {_stopWatch.ElapsedMilliseconds}ms");
                }
            }
        }

        //gets the Users by their role using UserRoleID.
           public IEnumerable<User> GetUsersByUserRoleID(int UserRoleID)
        {
            if(IsTracingEnabled) _stopWatch.Start();
            if (UserRoleID <= 0 || UserRoleID > 2) throw new ArgumentException($"User Role Id must be greater than 0 where UserRoleId:{UserRoleID}");
            try
            {
                //Gets the users with same UserRoleId with VerifyStatusID as 1 (1->Approved User).
                return _context.Users.Where(user => user.UserRoleId == UserRoleID && user.VerifyStatusID == 1).Include(e => e.Designation).Include(e => e.Gender).Include(e=>e.Designation!.Department);
               
            }
            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage("UserRepository", "GetUsersByUserRoleID(int UserRoleID)", exception, UserRoleID));
                throw;
            }
            finally
            {
                if(IsTracingEnabled)
                {
                _stopWatch.Stop();
                _logger.LogInformation($"Tracelog:UserRepository Elapsed Time for GetIsTraceEnabledFromConfiguration() - {_stopWatch.ElapsedMilliseconds}ms");
                }
            }
        }

        //Gets the User who is a reviewer using IsReviewer.
           public IEnumerable<User> GetUsersByIsReviewer(bool IsReviewer)
        {
            if(IsTracingEnabled) _stopWatch.Start();
            try
            {
                return _context.Users.Where(user => user.IsReviewer == IsReviewer && user.VerifyStatusID == 1).Include(e => e.Designation).Include(e => e.Gender).Include(e=>e.Designation!.Department);
            }
            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage("UserRepository", "GetUsersByIsReviewer(bool IsReviewer)", exception, IsReviewer));
                throw;
            }
            finally
            {
                if(IsTracingEnabled)
                {
                _stopWatch.Stop();
                _logger.LogInformation($"Tracelog:UserRepository Elapsed Time for GetIsTraceEnabledFromConfiguration() - {_stopWatch.ElapsedMilliseconds}ms");
                }
            }
        }

        //Gets the total count of the user who uses the application.
           public object GetCountOfUsers()
        { 
            if(IsTracingEnabled) _stopWatch.Start();
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
            finally
            {
                if(IsTracingEnabled)
                {
                _stopWatch.Stop();
                _logger.LogInformation($"Tracelog:UserRepository Elapsed Time for GetIsTraceEnabledFromConfiguration() - {_stopWatch.ElapsedMilliseconds}ms");
                }
            }
        }

        //to Get the Genders 
        public IEnumerable<Gender> GetGenders()
        {
            if(IsTracingEnabled) _stopWatch.Start();
            try
            {
                return _context.Genders.ToList();
            }
            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage("UserRepository", " GetGenders()", exception));
                throw;
            }
            finally
            {
                if(IsTracingEnabled)
                {
                _stopWatch.Stop();
                _logger.LogInformation($"Tracelog:UserRepository Elapsed Time for GetGenders() - {_stopWatch.ElapsedMilliseconds}ms");
                }
            }
        }


        //to get the designation of the user.
        public IEnumerable<Designation> GetDesignations()
        {
            if(IsTracingEnabled) _stopWatch.Start();
            try
            {
                return _context.Designations.Include(e => e.Department).ToList();
            }
            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage("UserRepository", " GetDesignations()", exception));
                throw;
            }
            finally
            {
                if(IsTracingEnabled)
                {
                _stopWatch.Stop();
                _logger.LogInformation($"Tracelog:UserRepository Elapsed Time for GetIsTraceEnabledFromConfiguration() - {_stopWatch.ElapsedMilliseconds}ms");
                }
            }
        }


        //to get the department of the user.
        public IEnumerable<Department> GetDepartments()
        {
            if(IsTracingEnabled) _stopWatch.Start();
            try
            {
                return _context.Departments.ToList();
            }
            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage("UserRepository", " GetDepartments()", exception));
                throw;
            }
            finally
            {
                if(IsTracingEnabled)
                {
                _stopWatch.Stop();
                _logger.LogInformation($"Tracelog:UserRepository Elapsed Time for GetIsTraceEnabledFromConfiguration() - {_stopWatch.ElapsedMilliseconds}ms");
                }
            }
        }

        //Gets the ols information using the range value.
        private int GetRange()
        {
            if(IsTracingEnabled) _stopWatch.Start();
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
            finally
            {
                if(IsTracingEnabled)
                {
                _stopWatch.Stop();
                _logger.LogInformation($"Tracelog:UserRepository Elapsed Time for GetIsTraceEnabledFromConfiguration() - {_stopWatch.ElapsedMilliseconds}ms");
                }
            }
        }

        //Get Tracing Enabled or not from Configuration
        public bool GetIsTraceEnabledFromConfiguration()
        {
            try
            {
                var IsTracingEnabled = _configuration["Tracing:IsEnabled"];
                return IsTracingEnabled != null ? Convert.ToBoolean(IsTracingEnabled) : false;
            }
            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage("UserRepository", "GetIsTraceEnabledFromConfiguration()", exception));
                return false;
            }
        }
    }
}