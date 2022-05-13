
using Microsoft.AspNetCore.Mvc;

namespace AspireOverflow.Controllers
{
   
   public class BaseController: ControllerBase
   {

      
      protected static object Message(string message,object? obj=null){
           if(Message !=null && obj ==null ) return new {Message=message};
           else if(Message !=null && obj !=null) return new{Message=message,DataPassed =obj};
           else return new {};   
   }
}
}