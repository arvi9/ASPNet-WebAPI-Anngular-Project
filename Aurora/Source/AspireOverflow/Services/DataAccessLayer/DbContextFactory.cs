namespace AspireOverflow.DataAccessLayer
{
    public class DbContextFactory
    {
        public static AspireOverflowContext GetAspireOverflowContextObject()
        {

            var _AspireOverflowContext = new AspireOverflowContext();

            return _AspireOverflowContext;

        }
    }
}