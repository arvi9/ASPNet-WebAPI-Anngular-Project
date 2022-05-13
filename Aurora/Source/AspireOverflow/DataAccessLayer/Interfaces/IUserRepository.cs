using AspireOverflow.Models;


namespace AspireOverflow.DataAccessLayer.Interfaces
{

    public interface IUserRepository
    {


        public bool CreateUser(User User);

        public User GetUserByID(int UserId);
        public IEnumerable<User> GetUsers();
        public bool UpdateUserByVerifyStatus(int UserId, int VerifyStatusID);
        public bool UpdateUserByReviewer(int UserId, bool IsReviewer);
        public bool RemoveUser(int UserId);

        public IEnumerable<Department> GetDepartments();
        public IEnumerable<Gender> GetGenders();
         public IEnumerable<Designation> GetDesignations();
    }
}