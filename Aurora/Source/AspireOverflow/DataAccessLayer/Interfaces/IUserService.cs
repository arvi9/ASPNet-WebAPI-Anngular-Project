using AspireOverflow.Models;
namespace AspireOverflow.DataAccessLayer.Interfaces{

    public interface IUserService
{
    public bool CreateUser(User user);

    public IEnumerable<User> GetUsers();

    public IEnumerable<User> GetUsersByVerifyStatus(int VerifyStatusID);

    public User GetUsersByID(int UserID);

    public IEnumerable<User> GetUsersByUserRoleID(int UserRoleID);

    public  bool ChangeUserVerificationStatus(int UserID,int VerifyStatusID);

    public IEnumerable<User> GetUsersByIsReviewer(bool IsReviewer);
    
}
}