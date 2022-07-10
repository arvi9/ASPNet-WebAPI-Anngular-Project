using AspireOverflow.Models;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
namespace AspireOverflow.Controllers
{
    public class BaseController : ControllerBase
    {
        //Message function takes a string as Input Sends back as Object Result 
        //obj parameter is Optional  
        protected object Message(string message, object? obj = null)
        {
            if (Message != null && obj == null) return new { Message = message };
            else if (Message != null && obj != null) return new { Message = message, DataPassed = obj };
            else return new { };
        }

        
        //Returns the Current Application User's Data By using the Claims.
        protected CurrentUser GetCurrentUser()
        {
            var CurrentUser = new CurrentUser();
            if (User != null)
            try{
                CurrentUser.UserId = Convert.ToInt32(User.FindFirst("UserId")?.Value);
                CurrentUser.Email = User.FindFirst(ClaimTypes.Email)?.Value;
                CurrentUser.RoleId = Convert.ToInt32(User.FindFirst("RoleId")?.Value);
                CurrentUser.IsReviewer = Convert.ToBoolean(User.FindFirst("IsReviewer")?.Value);
                return CurrentUser;
            }catch (Exception){
                throw;
            }
            return CurrentUser;
        }
    }
}