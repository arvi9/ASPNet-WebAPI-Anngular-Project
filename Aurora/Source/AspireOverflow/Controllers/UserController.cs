using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using AspireOverflow.Models;
using AspireOverflow.Services;
using AspireOverflow.CustomExceptions;


namespace AspireOverflow.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class UserController : ControllerBase
{

    internal static ILogger<UserController> _logger;
    private static UserService _UserService;

    public UserController(ILogger<UserController> logger, UserService UserService)
    {
        _logger = logger ?? throw new NullReferenceException(nameof(logger));
        _UserService = UserService ?? throw new NullReferenceException(nameof(UserService));

    }

    [HttpPost]
    public async Task<ActionResult> CreateUser(User User)
    {
        if (User == null) return BadRequest("Null value is not supported");
        try
        {   //Development.Web is Enum constants which indicated the request approaching team.
            return _UserService.CreateUser(User) ? await Task.FromResult(Ok("Successfully Created")): BadRequest($"Error Occured while Adding User :{HelperService.PropertyList(User)}");
        }
        catch (ValidationException exception)
        {
            //HelperService.LoggerMessage - returns string for logger with detailed info
            _logger.LogError(HelperService.LoggerMessage(nameof(UserController), nameof(CreateUser), exception, User));
            return BadRequest($"{exception.Message}\n{HelperService.PropertyList(User)}");
        }
        catch (Exception exception)
        {
            _logger.LogError(HelperService.LoggerMessage(nameof(UserController), nameof(CreateUser), exception, User));
            return BadRequest($"Error Occured while Adding User :{HelperService.PropertyList(User)}");
        }
    }

    [HttpPatch]
    public async Task<ActionResult> ChangeUserVerifyStatus(int UserId, bool IsVerified)
    {
        if (UserId <= 0) return BadRequest("User ID must be greater than 0");
        try
        {
            if (IsVerified) return _UserService.ChangeUserVerificationStatus(UserId, 1) ? await Task.FromResult(Ok($"Successfully marked as Verified User in the record with UserId :{UserId}")) : BadRequest($"Error Occurred while marking User as Verified with UserId :{UserId}");
            else return _UserService.ChangeUserVerificationStatus(UserId, 2) ? await Task.FromResult(Ok($"Successfully marked as UnVerified User in the record with UserId :{UserId}")) : BadRequest($"Error Occurred while marking User as UnVerified with UserId :{UserId}");
        }
        catch (ItemNotFoundException exception)
        {
            _logger.LogError(HelperService.LoggerMessage(nameof(UserController), nameof(ChangeUserVerifyStatus), exception, UserId));
            return NotFound($"{exception.Message}");
        }
        catch (Exception exception)
        {
            _logger.LogError(HelperService.LoggerMessage(nameof(UserController), nameof(ChangeUserVerifyStatus), exception, UserId));
            return BadRequest($"Error Occurred while changing UserVerification Status with UserId :{UserId}");
        }
    }


    [HttpDelete]   //Admin rejected users only be deleted
    public async Task<ActionResult> RemoveUser(int UserId)
    {
        if (UserId <= 0) return BadRequest("User ID must be greater than 0");
        try
        {
            return _UserService.RemoveUser(UserId) ? await Task.FromResult(Ok("Not Verified User has been rejected succfully")) : BadRequest($"Unable to remove User with UserId:{UserId}");
        }
        catch (ItemNotFoundException exception)
        {
            _logger.LogError(HelperService.LoggerMessage(nameof(UserController), nameof(RemoveUser), exception, UserId));
            return BadRequest($"{exception.Message}");
        }
        catch (Exception exception)
        {
            _logger.LogError(HelperService.LoggerMessage(nameof(UserController), nameof(RemoveUser), exception, UserId));
            return BadRequest($"Error Occurred while getting User with UserId :{UserId}");
        }
    }

    [HttpGet]
    public async Task<ActionResult<User>> GetUser(int UserId)
    {
        if (UserId <= 0) return BadRequest("User ID must be greater than 0");
        try
        {
            return await Task.FromResult(_UserService.GetUsersByID(UserId));
        }
        catch (ItemNotFoundException exception)
        {
            _logger.LogError(HelperService.LoggerMessage(nameof(UserController), nameof(GetUser), exception, UserId));
            return BadRequest($"{exception.Message}");
        }
        catch (Exception exception)
        {
            _logger.LogError(HelperService.LoggerMessage(nameof(UserController), nameof(GetUser), exception, UserId));
            return BadRequest($"Error Occurred while removing User with UserId :{UserId}");
        }
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerator<User>>> GetVerifiedUsers()
    {
        try
        {
            var ListOfUsers = _UserService.GetUsersByVerifyStatus(1);
            return await Task.FromResult(Ok(ListOfUsers));

        }
        catch (Exception exception)
        {
            _logger.LogError(HelperService.LoggerMessage(nameof(UserController), nameof(GetVerifiedUsers), exception));
            return BadRequest("Error occured while processing your request");
        }
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerator<User>>> GetUsersTobeVerified()
    {
        try
        {
            var ListOfUsers = _UserService.GetUsersByVerifyStatus(3);
            return await Task.FromResult(Ok(ListOfUsers));
        }
        catch (Exception exception)
        {
            _logger.LogError(HelperService.LoggerMessage(nameof(UserController), nameof(GetUsersTobeVerified), exception));
            return BadRequest("Error occured while processing your request");
        }
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerator<User>>> GetRejectedUsers()
    {
        try
        {
            var ListOfUsers = _UserService.GetUsersByVerifyStatus(2);
            return await Task.FromResult(Ok(ListOfUsers));
        }
        catch (Exception exception)
        {
            _logger.LogError(HelperService.LoggerMessage(nameof(UserController), nameof(GetUsersTobeVerified), exception));
            return BadRequest("Error occured while processing your request");
        }
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerator<User>>> GetReviewers()
    {
        try
        {
            var ListOfUsers = _UserService.GetUsersByIsReviewer(true);
            return await Task.FromResult(Ok(ListOfUsers));
        }
        catch (Exception exception)
        {
            _logger.LogError(HelperService.LoggerMessage(nameof(UserController), nameof(GetReviewers), exception));
            return BadRequest("Error occured while processing your request");
        }
    }

}