
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

    public UserController(ILogger<UserController> logger, UserService UserService)
    {
        _logger = logger;
        _UserService = UserService;

    }

    [HttpPost]
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

    [HttpGet]
    public async Task<ActionResult> GetUser()
    {
        int UserId=GetCurrentUser().UserId;
   
        try
        {
         

            return await Task.FromResult(Ok( _UserService.GetUserByID(UserId)));

        }
        catch (ItemNotFoundException exception)
        {
            _logger.LogError(HelperService.LoggerMessage("UserController", "GetUser()", exception, UserId));
            return BadRequest(Message($"{exception.Message} with UserId:{UserId}"));
        }
        catch (Exception exception)
        {
            _logger.LogError(HelperService.LoggerMessage("UserController", "GetUser()", exception, UserId));
            return Problem($"Error Occurred while Getting User with UserId :{UserId}");
        }
    }

    

    [HttpGet]
    public async Task<ActionResult> GetUsersByVerifyStatusId(int VerifyStatusID)
    {
        if (VerifyStatusID <= 0 && VerifyStatusID > 3) return BadRequest(Message($"VerifyStatusID must be greater than 0 and less than 3 - VerifyStatusId:{VerifyStatusID}"));
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

    [HttpGet]
    public async Task<ActionResult> GetUsersByUserRoleId(int RoleId)
    {
        if (RoleId <= 0 && RoleId > 2) return BadRequest(Message($"RoleId must be greater than 0 and less than 2 - RoleId:{RoleId}"));
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
            throw exception;
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
            throw exception;
        }
    }

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
            throw exception;
        }
    }
   


}