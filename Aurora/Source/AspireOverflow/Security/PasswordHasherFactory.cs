
using AspireOverflow.Models;
namespace AspireOverflow.Security
{
    public class PasswordHasherFactory
    {
        public static BCryptPasswordHasher<User> GetPasswordHasherFactory()
        {
            return new BCryptPasswordHasher<User>();

        }
    }
}