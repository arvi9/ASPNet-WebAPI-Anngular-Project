using Microsoft.EntityFrameworkCore;

namespace AspireOverflow.DataAccessLayer
{
    public class DbContextFactory
    {
        public static AspireOverflowContext GetAspireOverflowContextObject()
        {

            var optionsBuilder = new DbContextOptionsBuilder<AspireOverflowContext>();
            var connectionString = "Server=.;database=AspireOverflow;Trusted_Connection=true;";
            optionsBuilder.UseSqlServer(connectionString
                                     ?? throw new NullReferenceException(
                                         $"Connection string is not got from environment {nameof(connectionString)}"));

            
            
            return new AspireOverflowContext(optionsBuilder.Options);



        }

    }

}