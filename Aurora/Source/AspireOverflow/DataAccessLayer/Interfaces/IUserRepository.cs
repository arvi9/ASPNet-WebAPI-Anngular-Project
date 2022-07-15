using AspireOverflow.Models;
namespace AspireOverflow.DataAccessLayer.Interfaces
{
    public interface IUserRepository
    {
        public bool CreateUser(User User);
        public User GetUserByID(int UserId);
        public User GetUserByEmail(string Email);
        public IEnumerable<User> GetUsers();
        public IEnumerable<User> GetUsersByVerifyStatusId(int VerifyStatusID);
        public IEnumerable<User> GetUsersByUserRoleID(int UserRoleID);
        public IEnumerable<User> GetUsersByIsReviewer(bool IsReviewer);
        public bool UpdateUserByVerifyStatus(int UserId, int VerifyStatusID, int UpdatedByUserId);
        public bool UpdateUserByReviewer(int UserId, bool IsReviewer, int UpdatedByUserId);
        public bool RemoveUser(int UserId);
        public IEnumerable<Department> GetDepartments();
        public IEnumerable<Gender> GetGenders();
        public IEnumerable<Designation> GetDesignations();
        public object GetCountOfUsers();
        public bool GetIsTraceEnabledFromConfiguration();

    }
}