using System.ComponentModel.DataAnnotations;
using AspireOverflow.Models;

namespace AspireOverflow.Services
{
    public class Validation
    {

        public static bool ValidateQuery(Query query)
        {


            if (query == null) throw new NullReferenceException("Query should not be null");
            else if (query.CreatedBy <= 0) throw new ValidationException("CreatedBy Id  must be greater than 0");
            else if (String.IsNullOrEmpty(query.Title)) throw new ValidationException("Title cannot be null or empty");
            else if (String.IsNullOrEmpty(query.Content)) throw new ValidationException("content cannot be null or empty");
            else if (query.Title.Length > 60) throw new ValidationException("Title length must be less than 60 charcter");

            else return true;
        }




    }
}