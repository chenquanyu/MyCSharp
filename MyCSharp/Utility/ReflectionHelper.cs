using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MyCSharp.Utility
{
    public static class ReflectionHelper
    {
        public static object GetValueByPropertyInfo(object obj, PropertyInfo prop)
        {
            dynamic value = prop.GetValue(obj, null);
            if (value == null)
                return "NULL";
            string result = string.Empty;
            if (prop.PropertyType.IsEnum || prop.PropertyType.IsNullableEnum())
            {
                return Convert.ToInt32(prop.GetValue(obj, null));
            }
            if (prop.PropertyType == typeof(bool) || prop.PropertyType == typeof(bool?)) { return (Convert.ToBoolean(prop.GetValue(obj, null)) ? "'1'" : "'0'"); }
            if (prop.PropertyType == typeof(DateTime?) || prop.PropertyType == typeof(DateTime)) { return string.Format("'{0}'", Convert.ToDateTime(prop.GetValue(obj, null)).ToString("yyyy/MM/dd HH:mm:ss")); }
            if (prop.PropertyType == typeof(string))
            {
                result = prop.GetValue(obj, null).ToString();
                result = result.Replace("'", "''");
                result = string.Format("N'{0}'", result);
                return result;
            }
            return string.Format("'{0}'", prop.GetValue(obj, null).ToString());

        }

        public static bool IsNullableEnum(this Type t)
        {
            Type u = Nullable.GetUnderlyingType(t);
            return (u != null) && u.IsEnum;
        }



    }
}
