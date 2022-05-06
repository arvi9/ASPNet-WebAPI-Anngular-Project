
using System.Text;

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






    }
}