namespace AspireOverflow.Models
{
    public class Validation
    {

        public static bool ValidateQuery(Query query)
        {

            if (query == null || query.CreatedBy  <=  0 || query.Title==null || query.Content == null)
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