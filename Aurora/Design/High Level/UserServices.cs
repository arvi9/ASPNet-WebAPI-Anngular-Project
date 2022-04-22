 


public class User
{
    
}

public class UserServices
{
    public bool CreateUser(User user);

    public List<User> GetUsers();

    public User GetUsersByID(int UserID);

    public list<User> GetUsersByUserRoleID(int UserRoleID);

    public  bool ChangeUserVerificationStatus(int UserID,int VerifyStatusID);

    public bool RemoveUser(int UserID);

    public bool SetUserRoleAsReviewer(int UserID);

    public bool RemoveUserRoleFromReviewer(int UserID);

    
}