
using Microsoft.EntityFrameworkCore;
using AspireOverflow.Services;


namespace AspireOverflow.DataAccessLayer
{
    public class EmailServiceFactory
    
    {
        private static MailService _mailService;

        public static MailService GetMailServiceObject()
        {
             if(_mailService != null) return _mailService;  //SingleTon concept applied here 

            var options=new BinderOptions();
        try
            {
                IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json")
                .Build();
                var connectionString = configuration.GetSection("MailSettings");
         
           
                  return null ;
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
                throw exception;
            }
         
             

      
        }

    }

}