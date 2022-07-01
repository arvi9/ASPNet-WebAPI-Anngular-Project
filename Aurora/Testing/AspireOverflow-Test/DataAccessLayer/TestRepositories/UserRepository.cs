using System;
using System.ComponentModel.DataAnnotations;
using AspireOverflow.DataAccessLayer;
using AspireOverflow.Models;
using Microsoft.AspNetCore.Mvc;
using AspireOverflow.Services;
using AspireOverflowTest.DataAccessLayer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;
using System.Linq;
using AspireOverflow.CustomExceptions;

namespace AspireOverflowTest
{

    public class UserRepositoryTest
    {
        private AspireOverflowContext _context;
        private Mock<ILogger<UserRepository>> _logger = new Mock<ILogger<UserRepository>>();
        private UserRepository _userRepository;

        public UserRepositoryTest()
        {
            _context = MockDBContext.GetInMemoryDbContext();
            _userRepository = new UserRepository(_context, _logger.Object);

        }
        // [Fact]
        // public void GetDepartments_ShouldReturnListOfDepartmrnts_WhenMethodIsCalled()
        // {
        //     var departments = UserMock.GetListOfDepartmentsForSeeding();
        //     Assert.Equal(departments.Count(), _userRepository.GetDepartments().Count());
        // }
        [Fact]
        public void GetDesignation_ShouldReturnListOfDesignation_WhenMethodIsCalled()
        {
            MockDBContext.SeedMockDataInMemoryDb(_context);
            var Designations = UserMock.GetListOfDesignationForSeeding();
            Assert.Equal(Designations.Count(), _userRepository.GetDesignations().Count());
        }
        [Fact]
        public void GetGenders_ShouldReturnListOfGender_WhenMethodIsCalled()
        {
        //    MockDBContext.SeedMockDataInMemoryDb(_context);
            var genders = UserMock.GetListOfGenderForSeeding();
            Assert.Equal(genders.Count(), _userRepository.GetGenders().Count());
        }
        // [Fact]
        // public void GetUsers_ShouldReturnListOfUsers_WhenMethodIsCalled()
        // {
             
        //     var users = UserMock.GetListOfUsersForSeeding();
        //     Assert.Equal(users.Count(), _userRepository.GetUsers().Count());
        // }

        //Create User
        
        [Fact]
        public void CreateUser_ShouldReturnTrue_WhenArticleObjectIsValid()
        {
            User user = UserMock.GetValidUser();
            var Result = _userRepository.CreateUser(user);
            Assert.True(Result);
        }
        [Theory]
        [InlineData(null)]
        public void CreateUser_ShouldThrowValidationException_WhenArticleObjectIsNull(User user)
        {  
            Assert.Throws<ValidationException>(() => _userRepository.CreateUser(user));
        }
        [Fact]
        public void CreateUser_ShouldThrowArgumentNullException_WhenArticleObjectIsInvalid()
        {
            User user = UserMock.GetInValidUser();
            Assert.Throws<ArgumentNullException>(() => _userRepository.CreateUser(user));
        }

        //RemoveUser

        [Theory]
        [InlineData(0)]
        public void RemoveUser_ShouldThrowArgumentException_WhenArticleId_Zero(int userId)
        {
            Assert.Throws<ArgumentException>(() => _userRepository.RemoveUser(userId));
        }
    //    [Fact]  
    //     public void RemoveUser_ShouldReturnTrue_WhenArticleObjectIsValid()
    //     {
    //         MockDBContext.SeedMockDataInMemoryDb(_context);
    //         var Result = _userRepository.RemoveUser(11);
    //         Assert.True(Result);
    //     }

        [Theory]
        [InlineData(0,2)]
        [InlineData(-1,2)]
        public void ChangeUserVerifyStatus_ThrowsArgumentException_WhenValidationErrorOccured(int UserId, int VerifyStatusID)
        {
            Assert.Throws<ArgumentException>(() => _userRepository.UpdateUserByVerifyStatus(UserId,VerifyStatusID));
                
        }


        [Theory]
        [InlineData(1,4)]
        [InlineData(1,0)]
        public void ChangeUserVerifyStatus_ThrowsArgumentException_WhenVerifyStatusValidationErrorOccured(int UserId, int VerifyStatusID)
        {
            Assert.Throws<ArgumentException>(() => _userRepository.UpdateUserByVerifyStatus(UserId,VerifyStatusID));  
        }
        

        [Theory]
        [InlineData(12,2)]
        public void ChangeUserVerifyStatus_ShouldThrowItemNotFoundException_WhenQueryIdNotExists(int UserId, int VerifyStatusID){
            Assert.Throws<ItemNotFoundException>(()=> _userRepository.UpdateUserByVerifyStatus(UserId,VerifyStatusID));
        }
  
        // [Theory]
        // [InlineData(1,2)]
        // public void ChangeUserVerifyStatus_ShouldReturnTrue_WhenValidDataPassed(int UserId, int VerifyStatusID){
        //     Assert.True(_userRepository.UpdateUserByVerifyStatus(UserId,VerifyStatusID));
        // }

        //UpdateUserByReviewer
        
        [Theory]
        [InlineData(0,true)]
        public void UpdateUserByReviewer_ShouldThrowArgumentException_WhenQueryIdISLessThanZero(int QueryId,bool IsReviewer){
            Assert.Throws<ArgumentException>(()=> _userRepository.UpdateUserByReviewer(QueryId,IsReviewer));
        }          
        [Theory]
        [InlineData(60,true)]
        public void UpdateUserByReviewer_ShouldThrowItemNotFoundException_WhenQueryIdNotExists(int QueryId,bool IsReviewer){
            Assert.Throws<ItemNotFoundException>(()=> _userRepository.UpdateUserByReviewer(QueryId,IsReviewer));
        }
  
        // [Theory]
        // [InlineData(2,true)]
        // public void UpdateUserByReviewer_ShouldReturnTrue_WhenValidDataPassed(int QueryId,bool IsReviewer){
        //     Assert.True(_userRepository.UpdateUserByReviewer(QueryId,IsReviewer));
        // }

    }
}