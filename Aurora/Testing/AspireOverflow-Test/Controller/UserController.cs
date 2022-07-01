using System.Linq;

using System.ComponentModel.DataAnnotations;

using AspireOverflow.Controllers;

using AspireOverflow.Models;

using Microsoft.AspNetCore.Mvc;

using Microsoft.Extensions.Logging;

using Moq;

using Xunit;

using AspireOverflow.DataAccessLayer.Interfaces;

using System;

using AspireOverflow.Services;

using AspireOverflow.CustomExceptions;



namespace AspireOverflowTest{

  public class UserControllerTest{

 private readonly UserController _UserController;

        private readonly Mock<ILogger<UserController>> _logger = new Mock<ILogger<UserController>>();

        private readonly Mock<IUserService> _UserService = new Mock<IUserService>();


   public UserControllerTest()
        {
            _UserController = new UserController(_logger.Object, _UserService.Object);
        }
        [Fact]
        public void GetDepartments_ShouldReturnStatusCode200_WhenUserObjectIsPassed()
        {
            var User  = UserMock.GetListOfUser();
             _UserService.Setup(obj => obj.GetDepartments()).Returns(User);
            var Result = _UserController.GetDepartments().Result as ObjectResult;
            Assert.Equal(200, Result?.StatusCode);
         }
         [Fact]
        public void GetDepartments_ShouldReturnStatusCode500_WhenExceptionIsThrown()
        {
            
            _UserService.Setup(obj => obj.GetDepartments()).Throws(new Exception());
            var Result = _UserController.GetDepartments().Result as ObjectResult;
            Assert.Equal(500, Result?.StatusCode);
        }
        [Fact]
        public void GetDesignations_ShouldReturnStatusCode200_WhenUserObjectIsPassed()
        {
            var User  = UserMock.GetListOfUser();
             _UserService.Setup(obj => obj.GetDesignations()).Returns(User);
            var Result = _UserController.GetDesignations().Result as ObjectResult;
            Assert.Equal(200, Result?.StatusCode);
         }
         [Fact]
        public void GetDesignations_ShouldReturnStatusCode500_WhenExceptionIsThrown()
        {
            
            _UserService.Setup(obj => obj.GetDesignations()).Throws(new Exception());
            var Result = _UserController.GetDesignations().Result as ObjectResult;
            Assert.Equal(500, Result?.StatusCode);
        }
        [Fact]
        public void GetGender_ShouldReturnStatusCode200_WhenUserObjectIsPassed()
        {
            var User  = UserMock.GetListOfUser();
             _UserService.Setup(obj => obj.GetGenders()).Returns(User);
            var Result = _UserController.GetGenders().Result as ObjectResult;
            Assert.Equal(200, Result?.StatusCode);
         }
         [Fact]
        public void GetGender_ShouldReturnStatusCode500_WhenExceptionIsThrown()
        {
            
            _UserService.Setup(obj => obj.GetGenders()).Throws(new Exception());
            var Result = _UserController.GetGenders().Result as ObjectResult;
            Assert.Equal(500, Result?.StatusCode);
        }
        [Theory]
        [InlineData(3)]
        public void  GetUsersByUserRoleId_ShouldReturnStatusCode400_WhenUserObjectIsNull(int RoleId)
        {

            var Result = _UserController.GetUsersByUserRoleId(RoleId).Result as ObjectResult;
            Assert.Equal(400, Result?.StatusCode);
        }
        [Theory]
        [InlineData(2)]
         public void GetUsersByUserRoleId_ShouldReturnStatusCode200_WhenUserObjectIsPassed(int RoleId)
         {
           var  Users = UserMock.GetListOfUser();
           _UserService.Setup(obj => obj.GetUsersByUserRoleID(RoleId)).Returns(Users); 
           var Result = _UserController.GetUsersByUserRoleId(RoleId).Result as ObjectResult;
           Assert.Equal(200, Result?.StatusCode);
          }
          [Fact]
        public void GetUsersByUserRoleId_ShouldReturnStatusCode500_WhenExceptionIsThrown()
        {
            int RoleId=1;
            _UserService.Setup(obj => obj.GetUsersByUserRoleID(RoleId)).Throws(new Exception());
           var Result = _UserController.GetUsersByUserRoleId(RoleId).Result as ObjectResult;
            Assert.Equal(500, Result?.StatusCode);
        } 
        [Theory]
        [InlineData(5)]
        public void  GetUsersByVerifyStatusId_ShouldReturnStatusCode400_WhenUserObjectIsNull(int VerifyStatusID)
        {

            var Result = _UserController.GetUsersByVerifyStatusId(VerifyStatusID).Result as ObjectResult;
            Assert.Equal(400, Result?.StatusCode);
        }
        [Theory]
        [InlineData(2)]
         public void GetUsersByVerifyStatusId_ShouldReturnStatusCode200_WhenUserObjectIsPassed(int VerifyStatusID)
         {
           var  Users = UserMock.GetListOfUser();
           _UserService.Setup(obj => obj.GetUsersByVerifyStatus(VerifyStatusID)).Returns(Users); 
           var Result = _UserController.GetUsersByVerifyStatusId(VerifyStatusID).Result as ObjectResult;
           Assert.Equal(200, Result?.StatusCode);
          }
          [Fact]
        public void GetUsersByVerifyStatusId_ShouldReturnStatusCode500_WhenExceptionIsThrown()
        {
            int VerifyStatusID=1;
            _UserService.Setup(obj => obj.GetUsersByVerifyStatus(VerifyStatusID)).Throws(new Exception());
           var Result = _UserController.GetUsersByVerifyStatusId(VerifyStatusID).Result as ObjectResult;
            Assert.Equal(500, Result?.StatusCode);
        } 
        [Theory]
        [InlineData(true)]
         public void GetUsersByIsReviewer_ShouldReturnStatusCode200_WhenUserObjectIsPassed(bool IsReviewer)
         {
           var  Users = UserMock.GetListOfUser();
           _UserService.Setup(obj => obj.GetUsersByIsReviewer(IsReviewer)).Returns(Users); 
           var Result = _UserController.GetUsersByIsReviewer(IsReviewer).Result as ObjectResult;
           Assert.Equal(200, Result?.StatusCode);
          }
           [Fact]
        public void GetUsersByIsReviewer_ShouldReturnStatusCode500_WhenExceptionIsThrown()
        {
            bool IsReviewer=true;
            _UserService.Setup(obj => obj.GetUsersByIsReviewer(IsReviewer)).Throws(new Exception());
           var Result = _UserController.GetUsersByIsReviewer(IsReviewer).Result as ObjectResult;
            Assert.Equal(500, Result?.StatusCode);
        } 
     
        [Fact]
        public void GetUser_ShouldReturnStatusCode200_WhenUserObjectIsPassed()
        {
            int UserId=1;
            var User  = UserMock.GetListOfUser();
             _UserService.Setup(obj => obj.GetUserByID(UserId)).Returns(User);
            var Result = _UserController.GetUser().Result as ObjectResult;
            Assert.Equal(200, Result?.StatusCode);
        }
        [Fact]
       public void GetUser_ShouldReturnStatusCode500_WhenExceptionIsThrown()
        {
            int UserId=0;
            _UserService.Setup(obj => obj.GetUserByID(UserId)).Throws(new Exception());
           var Result = _UserController.GetUser().Result as ObjectResult;
            Assert.Equal(500, Result?.StatusCode);
        } 

        //Create Article

        [Theory]
        [InlineData(null)]
        public void CreateUser_ShouldReturnStatusCode400_WhenUserObjectIsNull(User user)
        {
            var Result = _UserController.CreateUser(user).Result as ObjectResult;
            Assert.Equal(400, Result?.StatusCode);

        }
        [Fact]
        public void CreateUser_ShouldReturnStatusCode200_WhenUserObjectIsPassed()
        {
            User user = UserMock.GetValidUser();
            _UserService.Setup(obj =>obj.CreateUser(user)).Returns(true);
            var Result = _UserController.CreateUser(user).Result as ObjectResult;
            Assert.Equal(200, Result?.StatusCode);
        }
        [Fact]
        public void CreateUser_ShouldReturnStatusCode400_WhenUserServiceReturnsFalse()
        {
            var user = new User();
            _UserService.Setup(obj => obj.CreateUser(user)).Returns(false);
            var Result = _UserController.CreateUser(user).Result as ObjectResult;
            Assert.Equal(400, Result?.StatusCode);

        }
        [Fact]
        public void CreateUser_ShouldReturnStatusCode400_WhenValidationExceptionIsThrown()
        {
            var user = new User();
            _UserService.Setup(obj => obj.CreateUser(user)).Throws(new ValidationException());
            var Result = _UserController.CreateUser(user).Result as ObjectResult;
            Assert.Equal(400, Result?.StatusCode);
        }
        [Fact]
        public void CreateArticle_ShouldReturnStatusCode500_WhenExceptionIsThrown()
        {
            var user = new User();
            _UserService.Setup(obj => obj.CreateUser(user)).Throws(new Exception());
            var Result = _UserController.CreateUser(user).Result as ObjectResult;
            Assert.Equal(500, Result?.StatusCode);
        }

        //Removeuser

        [Theory]
        [InlineData(0)]
        public void RemoveUser_ShouldReturnStatusCode400_WhenUserId_Zero(int UserId)
        {
            var Result = _UserController.RemoveUser(UserId).Result as ObjectResult;
            Assert.Equal(400, Result?.StatusCode);

        }
        [Theory]
        [InlineData(1)]
        public void RemoveUser_ShouldReturnStatusCode200_WhenUserIdIsValid(int UserId)
        {
            _UserService.Setup(obj => obj.RemoveUser(UserId)).Returns(true);

            var Result = _UserController.RemoveUser(UserId).Result as ObjectResult;

            Assert.Equal(200, Result?.StatusCode);

        }
        [Theory]
        [InlineData(1)]
        public void RemoveUser_ShouldReturnStatusCode200_WhenUserIdIsInValid(int UserId)
        {
            _UserService.Setup(obj => obj.RemoveUser(UserId)).Returns(false);

            var Result = _UserController.RemoveUser(UserId).Result as ObjectResult;

            Assert.Equal(400,Result?.StatusCode);

        }
        [Fact]
        public void RemoveUser_ShouldReturnStatusCode400_WhenItemNotFoundExceptionIsThrown()
        {
            int userid=2;
            _UserService.Setup(obj => obj.RemoveUser(userid)).Throws(new ItemNotFoundException());

            var Result = _UserController.RemoveUser(userid).Result as ObjectResult;

            Assert.Equal(400, Result?.StatusCode);
        }
        [Fact]
        public void RemoveUser_ShouldReturnStatusCode500_WhenExceptionIsThrown()
        {
            var UserId = 10;
            _UserService.Setup(obj => obj.RemoveUser(UserId)).Throws(new Exception());

            var Result = _UserController.RemoveUser(UserId).Result as ObjectResult;

            Assert.Equal(500, Result?.StatusCode);
        }

        //ChangeUserVerifyStatus

        [Theory]
        [InlineData(0,false)]
        public void ChangeUserVerifyStatus_ShouldReturn400_WhenValidationErrorOccured(int UserId, bool IsVerified)
        {
              
                var Result = _UserController.ChangeUserVerifyStatus(UserId,IsVerified).Result as ObjectResult;
                Assert.Equal(400,Result?.StatusCode);
        }

        [Theory]
        [InlineData(1,true)]
        public void ChangeUserVerifyStatus_ShouldReturn200_WhenIsVerifiedIsTrueAndServiceReturnsTrue(int UserId, bool IsVerified)
        {
                _UserService.Setup(obj => obj.ChangeUserVerificationStatus(UserId,1)).Returns(true);
                var Result = _UserController.ChangeUserVerifyStatus(UserId,IsVerified).Result as ObjectResult;
                Assert.Equal(200,Result?.StatusCode);
        }

        [Theory]
        [InlineData(1,true)]
        public void ChangeUserVerifyStatus_ShouldReturn400_WhenIsVerifiedIsTrueAndServiceReturnsFalse(int UserId, bool IsVerified)
        {
                _UserService.Setup(obj => obj.ChangeUserVerificationStatus(UserId,1)).Returns(false);
                var Result = _UserController.ChangeUserVerifyStatus(UserId,IsVerified).Result as ObjectResult;
                Assert.Equal(400,Result?.StatusCode);
        }

        [Theory]
        [InlineData(1,false)]
        public void ChangeUserVerifyStatus_ShouldReturn200_WhenIsVerifiedIsFalseAndServiceReturnsTrue(int UserId, bool IsVerified)
        {
                _UserService.Setup(obj => obj.ChangeUserVerificationStatus(UserId,2)).Returns(true);
                var Result = _UserController.ChangeUserVerifyStatus(UserId,IsVerified).Result as ObjectResult;
                Assert.Equal(200,Result?.StatusCode);
        }

        [Theory]
        [InlineData(1,false)]
        public void ChangeUserVerifyStatus_ShouldReturn400_WhenIsVerifiedIsFalseAndServiceReturnsFalse(int UserId, bool IsVerified)
        {
                _UserService.Setup(obj => obj.ChangeUserVerificationStatus(UserId,2)).Returns(false);
                var Result = _UserController.ChangeUserVerifyStatus(UserId,IsVerified).Result as ObjectResult;
                Assert.Equal(400,Result?.StatusCode);
        }
        [Theory]
        [InlineData(1,false)]
        public void ChangeUserVerifyStatus_ShouldReturn500_WhenAnyExceptionOccured(int UserId, bool IsVerified)
        {
                _UserService.Setup(obj => obj.ChangeUserVerificationStatus(UserId,2)).Throws(new Exception());
                var Result = _UserController.ChangeUserVerifyStatus(UserId,IsVerified).Result as ObjectResult;
                Assert.Equal(500,Result?.StatusCode);
        }
        [Theory]
        [InlineData(1,false)]
        public void ChangeUserVerifyStatus_ShouldReturn404_WhenUserIsNotFound(int UserId, bool IsVerified)
        {
                _UserService.Setup(obj => obj.ChangeUserVerificationStatus(UserId,2)).Throws(new ItemNotFoundException());
                var Result = _UserController.ChangeUserVerifyStatus(UserId,IsVerified).Result as ObjectResult;
                Assert.Equal(404,Result?.StatusCode);
        }

        //ChangeUserByIsReviewer

        [Theory]
        [InlineData(0,false)]
        public void ChangeUserByIsReviewer_ShouldReturn400_WhenValidationErrorOccured(int UserId, bool IsReviewer)
        {
                _UserService.Setup(obj => obj.UpdateUserByIsReviewer(UserId,IsReviewer)).Returns(true);
                var Result = _UserController.UpdateUserByIsReviewer(UserId,IsReviewer).Result as ObjectResult;
                Assert.Equal(400,Result?.StatusCode);
        }

        [Theory]
        [InlineData(1,false)]
        public void ChangeUserByIsReviewer_ShouldReturn200_WhenUserisUpdatedAsReviewer(int UserId, bool IsReviewer)
        {
                _UserService.Setup(obj => obj.UpdateUserByIsReviewer(UserId,IsReviewer)).Returns(true);
                var Result = _UserController.UpdateUserByIsReviewer(UserId,IsReviewer).Result as ObjectResult;
                Assert.Equal(200,Result?.StatusCode);
        }

        [Theory]
        [InlineData(1,false)]
        public void ChangeUserByIsReviewer_ShouldReturn400_WhenExceptionOccuredinServiceLayer(int UserId, bool IsReviewer)
        {
                _UserService.Setup(obj => obj.UpdateUserByIsReviewer(UserId,IsReviewer)).Returns(false);
                var Result = _UserController.UpdateUserByIsReviewer(UserId,IsReviewer).Result as ObjectResult;
                Assert.Equal(400,Result?.StatusCode);
        }

        [Theory]
        [InlineData(1,false)]
        public void ChangeUserByIsReviewer_ShouldReturn500_WhenAnyExceptionOccured(int UserId, bool IsReviewer)
        {
                _UserService.Setup(obj => obj.UpdateUserByIsReviewer(UserId,IsReviewer)).Throws(new Exception());
                var Result = _UserController.UpdateUserByIsReviewer(UserId,IsReviewer).Result as ObjectResult;
                Assert.Equal(500,Result?.StatusCode);
        }

        [Theory]
        [InlineData(1,false)]
        public void ChangeUserByIsReviewer_ShouldReturn404_WhenUserIsNotFound(int UserId, bool IsReviewer)
        {
                _UserService.Setup(obj => obj.UpdateUserByIsReviewer(UserId,IsReviewer)).Throws(new ItemNotFoundException());
                var Result = _UserController.UpdateUserByIsReviewer(UserId,IsReviewer).Result as ObjectResult;
                Assert.Equal(404,Result?.StatusCode);
        }
}
}