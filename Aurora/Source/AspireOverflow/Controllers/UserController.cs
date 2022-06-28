
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using AspireOverflow.Models;
using AspireOverflow.Services;
using AspireOverflow.CustomExceptions;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using AspireOverflow.DataAccessLayer.Interfaces;

namespace AspireOverflow.Controllers;

[ApiController][Authorize]
[Route("[controller]/[action]")]
public class UserController : BaseController
{

    internal ILogger<UserController> _logger;
    private IUserService _UserService;

    public UserController(ILogger<UserController> logger, IUserService UserService)
    {
        _logger = logger;
        _UserService = UserService;

    }
        /// <summary>
        /// Create User
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     url : https://localhost:7197/User/CreateUser
        ///
        ///     * fields are required
        ///      body
        ///     {
        ///        userId*: int,
        ///        fullName*: string,
        ///        genderId*: int,
        ///        aceNumber*: string,
        ///        emailAddress*: string,
        ///        password*: string,
        ///        dateOfBirth*: 2022-06-19T16:02:44.207Z,
        ///     }
        ///
        /// </remarks>
        /// <response code="200">If the User was created.</response>
        /// <response code="400">The server will not process the request due to something that is perceived to be a client error.</response>
        /// <response code="500">If there is problem in server.</response>
        /// <param name="User"></param>

    [HttpPost][AllowAnonymous]
    public async Task<ActionResult> CreateUser(User User)
    {

        if (User == null) return BadRequest(Message("Null value is not supported"));
        try
        {
            //Development.Web is Enum constants which indicated the request approaching team.
            return _UserService.CreateUser(User) ? await Task.FromResult(Ok("Successfully Created")) : BadRequest(Message($"Error Occured while Adding User :{HelperService.PropertyList(User)}"));
        }
        catch (ValidationException exception)
        {
            //HelperService.LoggerMessage - returns string for logger with detailed info
            _logger.LogError(HelperService.LoggerMessage("UserController", " CreateUser(User User)", exception, User));
            return BadRequest(Message($"{exception.Message}",User));
        }
        catch (Exception exception)
        {
            _logger.LogError(HelperService.LoggerMessage("UserController", " CreateUser(User User)", exception, User));
            return Problem($"Error Occured while Adding User :{HelperService.PropertyList(User)}");
        }
    }
        /// <summary>
        /// Change user verify status.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     url : https://localhost:7197/User/ChangeUserVerifyStatus
        ///
        ///     body
        ///             {
        ///                 UserId* : int,
        ///                 IsVerified* : bool,
        ///                
        ///             }
        /// 
        /// </remarks>
        /// <response code="200">Returns that user status is verified.</response>
        /// <response code="400">The server will not process the request due to something that is perceived to be a client error. </response>
        /// <response code="500">If there is problem in server. </response>
        /// <param name="UserId"></param>
        /// <param name="IsVerified"></param>

    [HttpPatch]
    public async Task<ActionResult> ChangeUserVerifyStatus(int UserId, bool IsVerified)
    {
        if (UserId <= 0) return BadRequest(Message("User ID must be greater than 0"));
        try
        {
            if (IsVerified) return _UserService.ChangeUserVerificationStatus(UserId, 1) ? await Task.FromResult(Ok($"Successfully marked as Verified User in the record with UserId :{UserId}")) : BadRequest(Message($"Error Occurred while marking User as Verified with UserId :{UserId}"));
            else return _UserService.ChangeUserVerificationStatus(UserId, 2) ? await Task.FromResult(Ok($"Successfully marked as UnVerified User in the record with UserId :{UserId}")) : BadRequest(Message($"Error Occurred while marking User as UnVerified with UserId :{UserId}"));
        }
        catch (ItemNotFoundException exception)
        {
            _logger.LogError(HelperService.LoggerMessage("UserController", " ChangeUserVerifyStatus(int UserId, bool IsVerified)", exception, UserId));
            return NotFound($"{exception.Message}");
        }
        catch (Exception exception)
        {
            _logger.LogError(HelperService.LoggerMessage("UserController", " ChangeUserVerifyStatus(int UserId, bool IsVerified)", exception, UserId));
            return Problem($"Error Occurred while changing UserVerification Status with UserId :{UserId}");
        }
    }
        /// <summary>
        /// Here the user is updated as reviewer.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     url : https://localhost:7197/User/UpdateUserByIsReviewer
        ///
        ///     body
        ///             {
        ///                 UserId* : int,
        ///                 IsReviewer* : bool,
        ///                
        ///             }
        /// 
        /// </remarks>
        /// <response code="200">Returns that the User is marked as reviewer..</response>
        /// <response code="400">The server will not process the request due to something that is perceived to be a client error. </response>
        /// <response code="500">If there is problem in server. </response>
        /// <param name="UserId"></param>
        /// <param name="IsReviewer"></param>

      [HttpPatch]
    public async Task<ActionResult> UpdateUserByIsReviewer(int UserId, bool IsReviewer)
    {
        if (UserId <= 0) return BadRequest(Message("User ID must be greater than 0"));
        try
        {
           return _UserService.UpdateUserByIsReviewer(UserId, IsReviewer) ? await Task.FromResult( Ok($"Successfully Updated the Reviewer status with UserId :{UserId}")) : BadRequest(Message($"Error Occurred while updating the reviewer status with UserId :{UserId}"));
          
        }
        catch (ItemNotFoundException exception)
        {
            _logger.LogError(HelperService.LoggerMessage("UserController", "  UpdateUserByIsReviewer(int UserId, bool IsReviewer)", exception, UserId));
            return NotFound($"{exception.Message}");
        }
        catch (Exception exception)
        {
            _logger.LogError(HelperService.LoggerMessage("UserController", "UpdateUserByIsReviewer(int UserId, bool IsReviewer)", exception, UserId));
            return Problem($"Error Occurred while updating user reviewer Status with UserId :{UserId}");
        }
    }

        
        /// <summary>
        /// To remove user by User id.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     url : https://localhost:7197/User/RemoveUser
        ///     
        ///     * fields are required
        /// 
        ///     body
        ///             {
        ///                 UserId* : int,
        ///                
        ///             }
        /// 
        /// </remarks>
        /// <response code="200">Returns as removed user successfully. </response>
        /// <response code="400">The server will not process the request due to something that is perceived to be a client error. </response>
        /// <response code="500">If there is problem in server. </response>
        /// <param name="UserId"></param>



    [HttpDelete]   //Admin rejected users only be deleted
    public async Task<ActionResult> RemoveUser(int UserId)
    {
        if (UserId <= 0) return BadRequest(Message("User ID must be greater than 0"));
        try
        {
            return _UserService.RemoveUser(UserId) ? await Task.FromResult(Ok("Not Verified User has been rejected succfully")) : BadRequest(Message($"Not Allowed to remove User with UserId:{UserId}"));
        }
        catch (ItemNotFoundException exception)
        {
            _logger.LogError(HelperService.LoggerMessage("UserController", "RemoveUser(int UserId)", exception, UserId));
            return BadRequest(Message($"{exception.Message}"));
        }
        catch (Exception exception)
        {
            _logger.LogError(HelperService.LoggerMessage("UserController", "RemoveUser(int UserId)", exception, UserId));
            return Problem($"Error Occurred while removing User with UserId :{UserId}");
        }
    }

        /// <summary>
        /// Gets all users
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     url : https://localhost:7197/User/GetUser
        ///
        /// </remarks>
        /// <response code="200">Returns a list of user. </response>
        /// <response code="400">The server will not process the request due to something that is perceived to be a client error. </response>
        /// <response code="500">If there is problem in server. </response>

    [HttpGet]
    public async Task<ActionResult> GetUser()
    {
        int UserId=GetCurrentUser().UserId;
        try
        {
            return await Task.FromResult(Ok( _UserService.GetUserByID(UserId)));

        }
        catch (Exception exception)
        {
            _logger.LogError(HelperService.LoggerMessage("UserController", "GetUser()", exception, UserId));
            return Problem($"Error Occurred while Getting User with UserId :{UserId}");
        }
    }

        /// <summary>
        /// Gets a list of verified users by verify status id..
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     url : https://localhost:7197/User/GetUsersByVerifyStatusId
        ///
        /// 
        ///     * fields are required
        /// 
        ///     body
        ///             {
        ///                 VerifyStatusID* : int,
        ///                
        ///             }
        /// </remarks>
        /// <response code="200">Returns a list of verified user. </response>
        /// <response code="400">The server will not process the request due to something that is perceived to be a client error. </response>
        /// <response code="500">If there is problem in server. </response>
        /// <param name="VerifyStatusID"></param>

    [HttpGet]
    public async Task<ActionResult> GetUsersByVerifyStatusId(int VerifyStatusID)
    {
        if (VerifyStatusID <= 0 || VerifyStatusID > 3) return BadRequest(Message($"VerifyStatusID must be greater than 0 and less than 3 - VerifyStatusId:{VerifyStatusID}"));
        try
        {
            var ListOfUsers = _UserService.GetUsersByVerifyStatus(VerifyStatusID);
            return await Task.FromResult(Ok(ListOfUsers));
        }
        catch (Exception exception)
        {
            _logger.LogError(HelperService.LoggerMessage("UserController", "GetUsersByVerifyStatusId(int VerifyStatusID)", exception));
            return Problem($"Error occured while processing your request with VerifyStatusId:{VerifyStatusID}");
        }
    }

        /// <summary>
        /// Gets a list of users role by its id.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     url : https://localhost:7197/User/GetUsersByUserRoleId
        ///
        /// 
        ///     * fields are required
        /// 
        ///     body
        ///             {
        ///                 RoleId* : int,
        ///                
        ///             }
        /// </remarks>
        /// <response code="200">Returns a list of users assigned to the user role. </response>
        /// <response code="400">The server will not process the request due to something that is perceived to be a client error. </response>
        /// <response code="500">If there is problem in server. </response>
        /// <param name="RoleId"></param>
    [HttpGet]
    public async Task<ActionResult> GetUsersByUserRoleId(int RoleId)
    {
        if (RoleId <= 0 || RoleId > 2) return BadRequest(Message($"RoleId must be greater than 0 and less than 2 - RoleId:{RoleId}"));
        try
        {
            var ListOfUsers = _UserService.GetUsersByUserRoleID(RoleId);
            return await Task.FromResult(Ok(ListOfUsers));
        }
        catch (Exception exception)
        {
            _logger.LogError(HelperService.LoggerMessage("UserController", "GetUsersByUserRoleId(int RoleId)", exception));
            return Problem($"Error occured while processing your request with RoleId:{RoleId}");
        }
    }
  
        /// <summary>
        /// Gets a list of user assigned as reviewer.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     url : https://localhost:7197/User/GetUsersByIsReviewer
        ///
        /// 
        ///     * fields are required
        /// 
        ///     body
        ///             {
        ///                 IsReviewer* : bool,
        ///                
        ///             }
        /// </remarks>
        /// <response code="200">Returns a list of user  assigned as reviewer. </response>
        /// <response code="400">The server will not process the request due to something that is perceived to be a client error. </response>
        /// <response code="500">If there is problem in server. </response>
        /// <param name="IsReviewer"></param>
    [HttpGet]
    public async Task<ActionResult> GetUsersByIsReviewer(bool IsReviewer)
    {

        try
        {
            var ListOfUsers = _UserService.GetUsersByIsReviewer(IsReviewer);
            return await Task.FromResult(Ok(ListOfUsers));
        }
        catch (Exception exception)
        {
            _logger.LogError(HelperService.LoggerMessage("UserController", " GetUsersByIsReviewer(bool IsReviewer)", exception));
            return Problem($"Error occured while processing your request with IsReviewer:{IsReviewer}");
        }
    }


    [HttpGet][AllowAnonymous]
    public async Task<IActionResult> GetGenders()
    {
        try
        {
            return await Task.FromResult(Ok(_UserService.GetGenders()));
        }
        catch (Exception exception)
        {
            _logger.LogError(HelperService.LoggerMessage("UserRepository", " GetGenders()", exception));
            return  Problem("Some Internal Server Error Occured");
        }
    }



    [HttpGet][AllowAnonymous]
    public async Task<IActionResult> GetDesignations()
    {
        try
        {
            return await Task.FromResult(Ok(_UserService.GetDesignations()));


        }
        catch (Exception exception)
        {
            _logger.LogError(HelperService.LoggerMessage("UserController", " GetDesignations()", exception));
           return Problem("Some Internal Server Error Occured");
        }
    }
         /// <summary>
        /// Gets all departments.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     url : https://localhost:7197/User/GetDepartments
        ///
        /// </remarks>
        /// <response code="200">Returns a list of departments.
        /// </response>
        /// <response code="500">If there is problem in server. </response>

    [HttpGet][AllowAnonymous]
    public async Task<IActionResult> GetDepartments()
    {
        try
        {
            return await Task.FromResult(Ok(_UserService.GetDepartments()));
        }
        catch (Exception exception)
        {
          _logger.LogError(HelperService.LoggerMessage("UserRepository", " GetDepartments()", exception));
          return  Problem("Some Internal Server Error Occured");
        }
    }
   


}