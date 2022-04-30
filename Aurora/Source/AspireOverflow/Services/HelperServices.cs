
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

          public static string LoggerMessage(Enum TeamName,string MethodName,Exception exception,object Data){

              return $"{TeamName}:{MethodName}\n Object passed :{{ \n {PropertyList(Data)}}}\nException :{PropertyList(exception)} ";
          }

           public static string LoggerMessage(Enum TeamName,string MethodName,Exception exception){

              return $"{TeamName}:{MethodName}\nException :{PropertyList(exception)}";
          }
    }
}