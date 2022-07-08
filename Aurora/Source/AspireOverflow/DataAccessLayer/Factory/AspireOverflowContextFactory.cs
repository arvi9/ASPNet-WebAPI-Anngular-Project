using Microsoft.EntityFrameworkCore;
namespace AspireOverflow.DataAccessLayer
{
    public class AspireOverflowContextFactory
    {
        private  AspireOverflowContext? _aspireOverflowContext;
        public  AspireOverflowContext GetAspireOverflowContextObject()
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
                  _aspireOverflowContext =  new AspireOverflowContext(optionsBuilder.Options);
                  return _aspireOverflowContext;
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
                throw;
            }
        }
    }
}