using AspireOverflow.DataAccessLayer.Interfaces;
using AspireOverflow.DataAccessLayer;

using AspireOverflow.Models;

namespace AspireOverflow.Services{


public class UserService : IUserService
{
      private static UserRepository database;

        private static ILogger<UserService> _logger;

        public UserService(ILogger<UserService> logger)
        {
            _logger = logger ?? throw new NullReferenceException("logger can't be null");
            database = UserRepositoryFactory.GetUserRepositoryObject(logger);

        }
    public bool CreateUser(User user){
        Validation.ValidateUser(user);
        try
        { 
            user= Validation.SetUserDefaultPropertyValues(user);
            return database.CreateUser(user);
        }
        catch (Exception exception)
        {
              _logger.LogError(HelperService.LoggerMessage(nameof(UserService), nameof(CreateUser), exception, user));
            throw;
        }
    }

    public bool RemoveUser(int UserId){
        Validation.ValidateId(UserId);
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

    public IEnumerable<User> GetUsers(){
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

    public IEnumerable<User> GetUsersByVerifyStatus(int VerifyStatusID){
        try
        {
            return GetUsers().Where(User =>User.VerifyStatusID==VerifyStatusID);  
        }
        catch (Exception exception)
        {
               _logger.LogError(HelperService.LoggerMessage(nameof(UserService), nameof(GetUsersByVerifyStatus), exception, VerifyStatusID));
            throw;
        }
    }

    public User GetUsersByID(int UserId){
        Validation.ValidateId(UserId);
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

    public IEnumerable<User> GetUsersByUserRoleID(int UserRoleID){
        Validation.ValidateId(UserRoleID);
        try
        {
            return GetUsersByVerifyStatus(1).Where(user =>user.UserRoleId ==UserRoleID);
        }
        catch (Exception exception)
        {   _logger.LogError(HelperService.LoggerMessage(nameof(UserService), nameof(GetUsersByUserRoleID), exception, UserRoleID));
            
            throw;
        }

    }

    public  bool ChangeUserVerificationStatus(int UserId,int VerifyStatusID){
    Validation.ValidateId(UserId,VerifyStatusID);
    try
    {
       return  database.UpdateUserByVerifyStatus(UserId,VerifyStatusID);
    }
    catch (Exception exception)
    {     _logger.LogError(HelperService.LoggerMessage(nameof(UserService), nameof(ChangeUserVerificationStatus), exception, $"UserId:{UserId},VerifyStatusID:{VerifyStatusID}"));
        throw exception;
    }}

   

    public IEnumerable<User> GetUsersByIsReviewer(bool IsReviewer){
        try
        {
         return GetUsersByVerifyStatus(1).Where(user =>user.IsReviewer ==IsReviewer);   
        }
        catch (Exception exception)
        {
               _logger.LogError(HelperService.LoggerMessage(nameof(UserService), nameof(GetUsersByIsReviewer), exception, IsReviewer));
            throw;
        }
    }
    
}

}