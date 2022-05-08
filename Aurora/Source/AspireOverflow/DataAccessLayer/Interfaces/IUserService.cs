using AspireOverflow.Models;
namespace AspireOverflow.DataAccessLayer.Interfaces{

    public interface IUserService
{
    public bool CreateUser(User user);

    public IEnumerable<Object> GetUsers();

    public IEnumerable<Object> GetUsersByVerifyStatus(int VerifyStatusID);

    public object GetUserByID(int UserID);

    public IEnumerable<Object> GetUsersByUserRoleID(int UserRoleID);

    public  bool ChangeUserVerificationStatus(int UserID,int VerifyStatusID);

    public IEnumerable<Object> GetUsersByIsReviewer(bool IsReviewer);
  
    
}
}