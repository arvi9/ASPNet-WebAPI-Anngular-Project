
using System.Text;
namespace AspireOverflow.Services
{
    public static class HelperService
    {
        public  static string PropertyList(this object obj)
        {
            var properties = obj.GetType().GetProperties();
            var stringBuilder = new StringBuilder();
            foreach (var data in properties)
            {
                stringBuilder.AppendLine(data.Name + ": " + data.GetValue(obj, null));
            }
            return stringBuilder.ToString();
        }

          public static string LoggerMessage(Enum TeamName,string MethodName,Exception exception,object Data = null){

              return Data != null? $"{TeamName}:{MethodName}\n  Object passed :{{ \n {PropertyList(Data)}}}\nException :{PropertyList(exception)}":

               $"{TeamName}:{MethodName}\nException :{PropertyList(exception)}";;
          }

          

            public static string LoggerMessage(string RepositoryName,string MethodName,Exception exception,object Data=null){

              return Data != null? $"{RepositoryName}:{MethodName}\n  Object passed :{{ \n {PropertyList(Data)}}}\nException :{PropertyList(exception)}":
              
               $"{RepositoryName}:{MethodName}\nException :{PropertyList(exception)}";;
          }
    }
}