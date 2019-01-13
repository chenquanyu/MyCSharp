using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace MyCSharp
{
    public static class Utils
    {
        public static System.DateTime ParseDate(string sDate)
        {
            if (sDate.Length == 8)
                sDate = sDate.Substring(0, 4) + "-" + sDate.Substring(4, 2) + "-" + sDate.Substring(6, 2);
            return Convert.ToDateTime(sDate);
        }

        public static void CopyProperty<T1, T2>(T1 obj1, T2 obj2)
        {
            dynamic proList = typeof(T2).GetProperties();
            foreach (PropertyInfo pro2 in proList)
            {
                PropertyInfo pro1 = typeof(T1).GetProperty(pro2.Name);
                if (pro1 != null)
                {
                    pro2.SetValue(obj2, pro1.GetValue(obj1, null), null);
                }
            }
        }

        public static MemoryStream SerializeToMemory(object obj)
        {
            MemoryStream stream = new MemoryStream();
            BinaryFormatter bf = new BinaryFormatter();
            bf.Serialize(stream, obj);
            return stream;
        }

        public static object DeserializeFromMemory(Stream stream)
        {
            stream.Position = 0;
            BinaryFormatter bf = new BinaryFormatter();
            return bf.Deserialize(stream);
        }

        public static string FormatArray(IEnumerable<string> a, string split = ",", string left = "[", string right = "]")
        {
            return string.Join(split, a.Select(p => $"{left}{p}{right}"));
        }



    }


}
