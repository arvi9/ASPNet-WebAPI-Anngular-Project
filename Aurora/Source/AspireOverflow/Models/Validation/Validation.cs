namespace AspireOverflow.Models
{
    public class Validation
    {

        public static bool ValidateQuery(Query query)
        {

            if (query == null || query.CreatedBy  <=  0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }




    }
}