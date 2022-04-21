


public class User
{
    
}
public class UserRole
{

}
public class UserServices
{
    public bool CreateUser(User user);

    public List<User> GetUsers();

    public User GetUsersByID(int UserID);

    public list<User> GetUsersByUserRoleID(int UserRoleID);

    public bool ApproveUser(int UserID);

    public bool RejectUser(int UserID);   

    public bool DisableUser(int UserID);   

    public bool SetUserRoleAsReviewer(int UserID);

    public bool RemoveUserRoleFromReviewer(int UserID);

    
}