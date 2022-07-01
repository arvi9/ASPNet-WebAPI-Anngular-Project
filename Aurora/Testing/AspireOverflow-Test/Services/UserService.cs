using System.Linq;
using System.ComponentModel.DataAnnotations;
using AspireOverflow.DataAccessLayer.Interfaces;
using AspireOverflow.Models;
using AspireOverflow.Services;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;
using Microsoft.EntityFrameworkCore;
using System;


namespace AspireOverflowTest
{

    public class UserServiceTest
    {
        private readonly Mock<IUserRepository> _userRepository = new Mock<IUserRepository>();

        private readonly Mock<ILogger<UserService>> _logger = new Mock<ILogger<UserService>>();




        private readonly UserService _userService;
        public UserServiceTest()
        {
            _userService = new UserService(_logger.Object, _userRepository.Object);
        }

        //GetDepartments

        [Fact]
        public void GetDepartments_ShouldReturnTrue_WhenuserObjectIsValid()

        {
            var Departments = UserMock.GetListOfDepartments();
            _userRepository.Setup(obj => obj.GetDepartments()).Returns(Departments);
            Assert.Equal(Departments.Count(), _userService.GetDepartments().Count());
        }
        [Fact]
        public void GetDepartments_ShoultThrowException_WhenAnyExceptionIsRaised()
        {
            _userRepository.Setup(obj => obj.GetDepartments()).Throws(new Exception());
            Assert.Throws<Exception>(() => _userService.GetDepartments());
        }

        //GetDesignations

        [Fact]
        public void GetDesignations_ShouldReturnTrue_WhenuserObjectIsValid()

        {
            var Designations = UserMock.GetListOfDesignation();
            _userRepository.Setup(obj => obj.GetDesignations()).Returns(Designations);
            Assert.Equal(Designations.Count(), _userService.GetDesignations().Count());
        }
        [Fact]
        public void GetDesignations_ShoultThrowException_WhenAnyExceptionIsRaised()
        {
            _userRepository.Setup(obj => obj.GetDesignations()).Throws(new Exception());
            Assert.Throws<Exception>(() => _userService.GetDesignations());
        }

        //GetGender

        [Fact]
        public void GetGender_ShouldReturnTrue_WhenuserObjectIsValid()

        {
            var Gender = UserMock.GetListOfGender();
            _userRepository.Setup(obj => obj.GetGenders()).Returns(Gender);
            Assert.Equal(Gender.Count(), _userService.GetGenders().Count());
        }
        [Fact]
        public void GetGender_ShoultThrowException_WhenAnyExceptionIsRaised()
        {
            _userRepository.Setup(obj => obj.GetGenders()).Throws(new Exception());
            Assert.Throws<Exception>(() => _userService.GetGenders());
        }

        //GetUsersByUserRoleID

        [Theory]
        [InlineData(0)]
        public void GetUsersByUserRoleID_ShouldReturnArgumentException_WhenUserRoleIDIsInValid(int UserRoleID)
        {
            Assert.Throws<ArgumentException>(() => _userService.GetUsersByUserRoleID(UserRoleID));
        }
        [Theory]
        [InlineData(1)]
        public void GetUsersByUserRoleID_ShouldReturnListofUsers_WhenMethodIsCalled(int UserRoleID)
        {
            var Users = UserMock.GetListOfUser();
            _userRepository.Setup(obj => obj.GetUsers()).Returns(Users);
            var ExpectedUser = Users.Where(item => item.UserRoleId == UserRoleID).Count();
            Assert.Equal(ExpectedUser, _userService.GetUsersByUserRoleID(UserRoleID).Count());
        }
        [Fact]
        public void GetUsersByUserRoleID_ShoultThrowException_WhenAnyExceptionIsRaised()
        {
            int UserRoleID = 1;
            _userRepository.Setup(obj => obj.GetUsers()).Throws(new Exception());
            Assert.Throws<Exception>(() => _userService.GetUsersByUserRoleID(UserRoleID));
        }

        //GetUsersByVerifyStatus

        [Theory]
        [InlineData(0)]
        public void GetUsersByVerifyStatus_ShouldReturnArgumentException_WhenUserRoleIDIsInValid(int VerifyStatusID)
        {
            Assert.Throws<ArgumentException>(() => _userService.GetUsersByVerifyStatus(VerifyStatusID));
        }
        [Theory]
        [InlineData(1)]
        public void GetUsersByVerifyStatus_ShouldReturnListofUsers_WhenMethodIsCalled(int VerifyStatusID)
        {
            var Users = UserMock.GetListOfUser();
            _userRepository.Setup(obj => obj.GetUsers()).Returns(Users);
            var ExpectedUser = Users.Where(item => item.VerifyStatusID == VerifyStatusID).Count();
            Assert.Equal(ExpectedUser, _userService.GetUsersByVerifyStatus(VerifyStatusID).Count());
        }
        [Fact]
        public void GetUsersByVerifyStatus_ShoultThrowException_WhenAnyExceptionIsRaised()
        {
            int VerifyStatusID = 1;
            _userRepository.Setup(obj => obj.GetUsers()).Throws(new Exception());
            Assert.Throws<Exception>(() => _userService.GetUsersByVerifyStatus(VerifyStatusID));
        }

        //GetUsersByIsReviewer

        [Theory]
        [InlineData(true)]
        public void GetUsersByIsReviewer_ShouldReturnListofUsers_WhenMethodIsCalled(bool IsReviewer)
        {
            var Users = UserMock.GetListOfUser();
            _userRepository.Setup(obj => obj.GetUsers()).Returns(Users);
            var ExpectedUser = Users.Where(item => item.IsReviewer == IsReviewer).Count();
            Assert.Equal(ExpectedUser, _userService.GetUsersByIsReviewer(IsReviewer).Count());
        }
        [Fact]
        public void GetUsersByIsReviewer_ShoultThrowException_WhenAnyExceptionIsRaised()
        {
            bool IsReviewver = true;
            _userRepository.Setup(obj => obj.GetUsers()).Throws(new Exception());
            Assert.Throws<Exception>(() => _userService.GetUsersByIsReviewer(IsReviewver));
        }

        //Getusers

        [Fact]
        public void GetUsers_ShouldReturnTrue_WhenuserObjectIsValid()

        {
            var Users = UserMock.GetListOfUser();
            _userRepository.Setup(obj => obj.GetUsers()).Returns(Users);
            Assert.Equal(Users.Count(), _userService.GetUsers().Count());
        }
        [Fact]
        public void Getusers_ShoultThrowException_WhenAnyExceptionIsRaised()
        {
            _userRepository.Setup(obj => obj.GetUsers()).Throws(new Exception());
            Assert.Throws<Exception>(() => _userService.GetUsers());
        }

        //RemoveUser

        [Theory]
        [InlineData(0)]
        public void RemoveUser_ShouldThrowException_WhenArticleId_Zero(int UserId)
        {
            Assert.Throws<ArgumentException>(() => _userService.RemoveUser(UserId));
        }
        [Fact]
        public void RemoveUser_ShouldRemoveListOfUser_WhenMethodIsCalled()
        {
            int UserId = 1;
            _userRepository.Setup(obj => obj.RemoveUser(UserId)).Returns(true);
            Assert.Equal(true, _userService.RemoveUser(UserId));
        }
        [Fact]
        public void RemoveUser_ShouldthroughException_WhenMethodIsCalled()
        {
            int UserId = 1;
            _userRepository.Setup(obj => obj.RemoveUser(UserId)).Returns(false);
            Assert.Equal(false, _userService.RemoveUser(UserId));
        }

        //Create User

        [Theory]
        [InlineData(null)]
        public void CreateUser_ShouldThrowValidationException_WhenArticleObjectIsNull(User user)
        {
            Assert.Throws<ValidationException>(() => _userService.CreateUser(user));
        }
        [Fact]
        public void CreateUser_ShouldThrowArgumentNullException_WhenArticleObjectIsInvalid()
        {
            User user = UserMock.GetInValidUser();
            Assert.Throws<ArgumentNullException>(() => _userService.CreateUser(user));
        }
        [Fact]
        public void CreateUser_ShouldReturnTrue_WhenArticleObjectIsValid()
        {
            User user = UserMock.GetValidUser();
            _userRepository.Setup(obj => obj.CreateUser(user)).Returns(true);
            var Result = _userService.CreateUser(user);
            Assert.True(Result);
        }
        [Fact]
        public void CreateUser_ShouldReturnFalse_WhenExceptionThrownInArticleRepository()
        {
            User user = UserMock.GetValidUser();
            _userRepository.Setup(obj => obj.CreateUser(user)).Throws(new DbUpdateException());
            var Result = _userService.CreateUser(user);
            Assert.False(Result);
            _userRepository.Setup(obj => obj.CreateUser(user)).Throws(new Exception());
            Assert.False(_userService.CreateUser(user));
            _userRepository.Setup(obj => obj.CreateUser(user)).Throws(new OperationCanceledException());
            Assert.False(_userService.CreateUser(user));
        }

        //ChangeUserVerificationStatus

        [Theory]
        [InlineData(0,2)]
        [InlineData(-1,2)]
        public void ChangeUserVerificationStatus_ReturnsArgumentException_WhenValidationErrorOccured(int UserId,int VerifyStatusID)
        {
                 Assert.Throws<ArgumentException>(() => _userService.ChangeUserVerificationStatus(UserId,VerifyStatusID));
        }

        [Theory]
        [InlineData(1,4)]
        [InlineData(1,0)]
        public void ChangeUserVerificationStatus_ReturnsArgumentException_WhenVerifyStatusValidationErrorOccured(int UserId,int VerifyStatusID)
        {
                 Assert.Throws<ArgumentException>(() => _userService.ChangeUserVerificationStatus(UserId,VerifyStatusID));
        }
        [Theory]
        [InlineData(1,2)]
        public void ChangeUserVerificationStatus_ReturnsTrue_WhenUserUpdatedAsReviewer(int UserId,int VerifyStatusID)
        {
                _userRepository.Setup(obj => obj.UpdateUserByVerifyStatus(UserId,VerifyStatusID)).Returns(true);
                var Result= _userService.ChangeUserVerificationStatus(UserId,VerifyStatusID);
                Assert.True(Result);
        }
        [Theory]
        [InlineData(1,2)]
        public void ChangeUserVerificationStatus_ReturnsFalse_WhenDALReturnsFalse(int UserId,int VerifyStatusID)
        {
                _userRepository.Setup(obj => obj.UpdateUserByVerifyStatus(UserId,VerifyStatusID)).Returns(false);
                var Result= _userService.ChangeUserVerificationStatus(UserId,VerifyStatusID);
                Assert.False(Result);
        }
        [Theory]
        [InlineData(1,2)]
        public void ChangeUserVerificationStatus_ThrowsException_WhenExceptionOccured(int UserId,int VerifyStatusID)
        {
                _userRepository.Setup(obj => obj.UpdateUserByVerifyStatus(UserId,VerifyStatusID)).Throws(new Exception());
                Assert.Throws<Exception>(() => _userService.ChangeUserVerificationStatus(UserId,VerifyStatusID));
        }

        //UpdateUserByIsReviewer
        
        [Theory]
        [InlineData(0,false)]
        public void UpdateUserByIsReviewer_ReturnsArgumentException_WhenValidationErrorOccured(int UserId,bool IsReviewer)
        {
                 Assert.Throws<ArgumentException>(() => _userService.UpdateUserByIsReviewer(UserId,IsReviewer));
        }
        
        [Theory]
        [InlineData(1,false)]
        public void UpdateUserByIsReviewer_ReturnsTrue_WhenUserUpdatedAsReviewer(int UserId,bool IsReviewer)
        {
                _userRepository.Setup(obj => obj.UpdateUserByReviewer(UserId,IsReviewer)).Returns(true);
                var Result= _userService.UpdateUserByIsReviewer(UserId,IsReviewer);
                Assert.True(Result);
        }

        [Theory]
        [InlineData(1,false)]
        public void UpdateUserByIsReviewer_ReturnsFalse_WhenDALReturnsFalse(int UserId,bool IsReviewer)
        {
                _userRepository.Setup(obj => obj.UpdateUserByReviewer(UserId,IsReviewer)).Returns(false);
                var Result= _userService.UpdateUserByIsReviewer(UserId,IsReviewer);
                Assert.False(Result);
        }
        [Theory]
        [InlineData(1,false)]
        public void UpdateUserByIsReviewer_ReturnsFalse_WhenExceptionOccured(int UserId,bool IsReviewer)
        {
                _userRepository.Setup(obj => obj.UpdateUserByReviewer(UserId,IsReviewer)).Throws(new Exception());
                var Result= _userService.UpdateUserByIsReviewer(UserId,IsReviewer);
                Assert.False(Result);
        }
    }
}