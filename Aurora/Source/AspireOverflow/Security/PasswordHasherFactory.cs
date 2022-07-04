
using AspireOverflow.Models;
namespace AspireOverflow.Security
{
    public static class PasswordHasherFactory
    {
        public static BCryptPasswordHasher<User> GetPasswordHasherFactory()
        {
            return new BCryptPasswordHasher<User>();

        }
    }
}