
using AspireOverflow.Models;

namespace AspireOverflow.DataAccessLayer.Interfaces{
 public interface ITokenService
    {
      public  object GenerateToken(Login Credentials);
    }

}
