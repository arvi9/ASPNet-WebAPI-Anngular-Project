
using Microsoft.EntityFrameworkCore;


namespace AspireOverflow.DataAccessLayer
{
    public class AspireOverflowContextFactory
    {
        public static AspireOverflowContext GetAspireOverflowContextObject()
        {

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
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }
            var aspireOverflowContext =  new AspireOverflowContext(optionsBuilder.Options);

            return aspireOverflowContext != null ? aspireOverflowContext : throw new Exception("Context is Null");


        }

    }

}