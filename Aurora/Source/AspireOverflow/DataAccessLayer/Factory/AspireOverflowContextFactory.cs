
using Microsoft.EntityFrameworkCore;



namespace AspireOverflow.DataAccessLayer
{
    public class AspireOverflowContextFactory
    
    {
        private static AspireOverflowContext _aspireOverflowContext;
        public static AspireOverflowContext GetAspireOverflowContextObject()
        {
            // if(_aspireOverflowContext != null) return _aspireOverflowContext;  //SingleTon concept applied here 

            var optionsBuilder = new DbContextOptionsBuilder<AspireOverflowContext>();
            try
            {
                IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json")
                .Build();
                var connectionString = configuration.GetConnectionString("Default");
            
                optionsBuilder.UseSqlServer(connectionString
                                         ?? throw new NullReferenceException(
                                             $"Connection string is passed as null {nameof(connectionString)}"));
                
                  _aspireOverflowContext =  new AspireOverflowContext(optionsBuilder.Options);
                  return _aspireOverflowContext;
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
                throw exception;
            }
         
             

      
        }

    }

}