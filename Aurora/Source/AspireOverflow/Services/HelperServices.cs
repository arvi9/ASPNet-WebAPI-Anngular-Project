
using System.Text;
using AspireOverflow.Models;

namespace AspireOverflow.Services
{
    public static class HelperService
    {



        public static string PropertyList(this object obj)
        {
            if (obj == null) return "";

            try
            {
                var properties = obj.GetType().GetProperties();
                var stringBuilder = new StringBuilder();
                foreach (var data in properties)
                {
                    stringBuilder.AppendLine(data.Name + ": " + data.GetValue(obj, null));
                }
                return stringBuilder.ToString();
            }
            catch
            {
                return "";
            }
        }



        public static string LoggerMessage(string RepositoryName, string MethodName, Exception exception, object Data = null)
        {

            return Data != null ? $"{RepositoryName}:{MethodName}\n  Data passed :{{ \n {PropertyList(Data)}}}\nException :{exception.ToString()}" :

             $"{RepositoryName}:{MethodName}\nException :{exception.ToString()}"; ;
        }


        public static string LoggerMessage(string RepositoryName, string MethodName, Exception exception, int FirstId, int? secondId)
        {

            return secondId != null ? $"{RepositoryName}:{MethodName}\n  Data passed :{{ \n {FirstId}, {secondId} }}\nException :{exception.ToString()}" :

             $"{RepositoryName}:{MethodName}\n Data passed :{{ \n {FirstId} }}\nException :{exception.ToString()}"; ;
        }


        public static string LoggerMessage(string RepositoryName, string MethodName, Exception exception, string data)
        {

            return $"{RepositoryName}:{MethodName}\n  Data passed :{{ \n {data} }}\nException :{exception.ToString()}";
        }



    public static MailRequest ArticleMail(string RecieverEmail,string ArticleTitle,string Subject,int ArticleStatusID)
    {
            var mail=new MailRequest();
            mail.ToEmail=RecieverEmail;
            mail.Subject=Subject;
            switch(ArticleStatusID){

                case 2: mail.Body=$"Hello Aspirian,\n\nGreetings,\n\n Your Article - \"{ArticleTitle}\" has been submitted successfully.\n\n Thanks and Regards,\nAspireOverflow.\n";
                        return mail;
                       

                case 4: mail.Body=$"Hello Aspirian,\n\nGreetings,\n\n Your Article - \"{ArticleTitle}\" have been published successfully.\n\n Thanks and Regards,\nAspireOverflow.\n";
                        return mail;
                      
                
                default: return mail;
                      

            }
             
          
        }



    public static MailRequest SpamMail(string RecieverEmail,string QueryTitle,string Subject,int VerifyStatusID)
        {
            var mail=new MailRequest();
            mail.ToEmail=RecieverEmail;
            mail.Subject=Subject;
            switch(VerifyStatusID){

                case 1: mail.Body=$"Hello Aspirian,\n\nGreetings,\n\n Your Report on - \"{QueryTitle}\" has been accpect as spam.\n\n Thanks and Regards,\nAspireOverflow.\n";
                        return mail;
                     

                case 2: mail.Body=$"Hello Aspirian,\n\nGreetings,\n\n Your Report on - \"{QueryTitle}\" have been rejected from accepting as spam.\n\n Thanks and Regards,\nAspireOverflow.\n";
                        return mail;
                        

               case 3: mail.Body=$"Hello Aspirian,\n\nGreetings,\n\n Your Report on - \"{QueryTitle}\" have been submitted for review.\n\n Thanks and Regards,\nAspireOverflow.\n";
                       return mail;
                     
                
                default: return mail;
                        

            }
          
        }


        public static MailRequest QueryMail(string RecieverEmail,string QueryTitle,string Subject)
        {
            var mail=new MailRequest();
            mail.ToEmail=RecieverEmail;
            mail.Subject=Subject;
            mail.Body=$"Hello Aspirian,\n\nGreetings,\n\n Your Query- \"{QueryTitle}\" have been Created.\n\n Thanks and Regards,\nAspireOverflow.\n";
            return mail;

        }

        public static MailRequest CommentMail(string RecieverEmail,string Title,string Subject)
        {
            var mail=new MailRequest();
            mail.ToEmail=RecieverEmail;
            mail.Subject=Subject;
            mail.Body=$"Hello Aspirian,\n\nGreetings,\n\n Someone has Commented in your- \"{Title}\" \n\n Thanks and Regards,\nAspireOverflow.\n";
            return mail;

        }
    }
}