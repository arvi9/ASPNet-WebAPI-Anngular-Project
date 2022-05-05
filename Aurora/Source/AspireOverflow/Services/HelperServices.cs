
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
namespace AspireOverflow.Services
{
    public static class HelperService
    {


            private static  String GetJsonResult(object obj){
                if(obj ==null) throw new NullReferenceException();
               var json = JsonSerializer.Serialize(obj, new JsonSerializerOptions()
                {
                    WriteIndented = true,
                    ReferenceHandler = ReferenceHandler.IgnoreCycles
                });
                return json;
            }


        public static string PropertyList(this object obj)
        {
            var properties = obj.GetType().GetProperties();
            var stringBuilder = new StringBuilder();
            foreach (var data in properties)
            {
                stringBuilder.AppendLine(data.Name + ": " + data.GetValue(obj, null));
            }
            return stringBuilder.ToString();
        }


        public static string LoggerMessage(Enum TeamName, string MethodName, Exception exception, object Data =null)
        {

            return LoggerMessage(TeamName.ToString(), MethodName, exception, Data);
        }

        public static string LoggerMessage(Enum TeamName, string MethodName, Exception exception, int FirstId, int? secondId)
        {

            return LoggerMessage(TeamName.ToString(), MethodName, exception, FirstId, secondId);
        }
        public static string LoggerMessage(Enum TeamName, string MethodName, Exception exception, string data)
        {

            return LoggerMessage(TeamName.ToString(), MethodName, exception, data);
        }



        public static string LoggerMessage(string RepositoryName, string MethodName, Exception exception, object Data=null)
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