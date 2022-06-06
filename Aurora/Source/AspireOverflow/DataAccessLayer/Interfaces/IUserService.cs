using AspireOverflow.Models;
namespace AspireOverflow.DataAccessLayer.Interfaces
{

    public interface IUserService :IDesignations,IGenders
    {
        public bool CreateUser(User user);
        public bool RemoveUser(int UserId);
 public bool  UpdateUserByIsReviewer(int UserId,bool IsReviewer);
        public IEnumerable<User> GetUsers();
        public IEnumerable<Object> GetUsersByVerifyStatus(int VerifyStatusID);
        public object GetUserByID(int UserID);
        public IEnumerable<Object> GetUsersByUserRoleID(int UserRoleID);
        public bool ChangeUserVerificationStatus(int UserID, int VerifyStatusID);
        public IEnumerable<Object> GetUsersByIsReviewer(bool IsReviewer);
    }
        public interface IDesignations
        {
            public IEnumerable<Object> GetDesignations();
            public IEnumerable<Object> GetDepartments();
        }

        public interface IGenders
        {
            public IEnumerable<Object> GetGenders();
        }

    }
