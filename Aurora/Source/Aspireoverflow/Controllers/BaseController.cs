
using AspireOverflow.Models;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;


namespace AspireOverflow.Controllers
{

    public class BaseController : ControllerBase
    {


        protected object Message(string message, object? obj = null)
        {
            if (Message != null && obj == null) return new { Message = message };
            else if (Message != null && obj != null) return new { Message = message, DataPassed = obj };
            else return new { };
        }

        protected CurrentUser GetCurrentUser()
        {

            return new CurrentUser
            {
                UserId = Convert.ToInt32(User.FindFirst("UserId")?.Value),
                Email = User.FindFirst(ClaimTypes.Email)?.Value,
                RoleId = Convert.ToInt32(User.FindFirst(ClaimTypes.Role)?.Value),
                IsReviewer = Convert.ToBoolean(User.FindFirst("IsReviewer")?.Value)
            };

        }
    }
}