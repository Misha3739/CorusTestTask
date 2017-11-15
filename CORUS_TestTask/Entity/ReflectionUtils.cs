using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Collections;

namespace CORUS_TestTask.Entity
{
   public static class ReflectionUtils
    {
        public static string ToString(object entity)
        {
            StringBuilder sb = new StringBuilder();
            var type = entity.GetType();
            sb.AppendLine($"{type.Name} :");
            foreach (var prop in type.GetProperties(BindingFlags.Instance | BindingFlags.Public))
            {
                if (prop.PropertyType.IsPrimitive || prop.PropertyType == typeof(string))
                    sb.Append($"{prop.Name} = {prop.GetValue(entity, null)}  ");
                else if(prop.PropertyType.IsGenericType)
                {
                    var list = prop.GetValue(entity) as IList;
                    if (list == null) continue;
                    var nested = prop.PropertyType.GetGenericArguments()[0];
                    sb.AppendLine($"With list of {nested.Name}");
                    
                    foreach(var item in list)
                    {
                        sb.AppendLine(ToString(item));
                    }
                }
            }
            sb.AppendLine(new string('-',100));
            return sb.ToString();
        }
    }
}
