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
        #region "DB Helper"

        public static string ConstructInsertSql<T>(List<T> toInsert)
        {
            string tableName = TableNameMapping(typeof(T).Name);
            List<PropertyInfo> props = typeof(T).GetProperties().ToList();
            props.Remove(typeof(T).GetProperty("Id"));
            string colNames = string.Join(",", props.Select(p => string.Format("[{0}]", p.Name)).ToArray());
            StringBuilder result = new StringBuilder();
            foreach (T item in toInsert)
            {
                string values = string.Join(",", props.Select(p => GetValueByPropertyInfo(item, p)).ToArray());
                result.AppendLine(string.Format("insert into {0} ({1}) values ({2})", tableName, colNames, values));
            }

            return result.ToString();
        }

        public static DataTable ConstructDataTable<T>(List<T> toInsert)
        {
            var result = new DataTable();
            var dataType = typeof(T);
            var props = dataType.GetProperties().ToList();
            result.TableName = TableNameMapping(dataType.Name);
            foreach (var pro in props)
            {
                result.Columns.Add(pro.Name, GetDbType(pro.PropertyType));
            }

            foreach (var item in toInsert)
            {
                var dataRow = result.Rows.Add();
                foreach (var pro in props)
                {
                    dataRow[pro.Name] = pro.GetValue(item) == null ? DBNull.Value : pro.GetValue(item);
                }
            }

            return result;
        }

        public static string ConstructUpdateSql<T>(List<T> toInsert)
        {
            Type type = typeof(T);
            string tableName = TableNameMapping(type.Name);
            List<PropertyInfo> props = type.GetProperties().ToList();
            dynamic propId = type.GetProperty("Id");
            props.Remove(propId);

            string colNames = string.Join(",", props.Select(p => string.Format("[{0}]", p.Name)).ToArray());
            StringBuilder result = new StringBuilder();
            foreach (T item in toInsert)
            {
                result.Append(string.Format("update [{0}] set ", tableName));

                List<string> pairs = new List<string>();
                foreach (PropertyInfo p in props)
                {
                    pairs.Add(string.Format("[{0}] = {1}", p.Name, GetValueByPropertyInfo(item, p)));
                }
                result.Append(string.Join(",", pairs.ToArray()));
                result.AppendLine(string.Format(" where Id = {0}", GetValueByPropertyInfo(item, propId)));
            }

            return result.ToString();
        }

        public static string ConstructDeleteSql<T>(List<T> objs)
        {
            return string.Format("delete from {0} where Id in ({1})", TableNameMapping(typeof(T).Name), ConbineListKey(objs));
        }

        public static string ConbineListKey<T>(List<T> objs, string key = "Id", bool needQuatationMark = false)
        {
            if (objs.Count == 0)
                return "";
            List<string> listId = new List<string>();
            for (int i = 0; i <= objs.Count - 1; i++)
            {
                string strFormat = "{0}";
                if (needQuatationMark)
                    strFormat = "'{0}'";
                listId.Add(string.Format(strFormat, objs[i].GetType().GetProperty(key).GetValue(objs[i], null)));
            }
            return string.Join(",", listId.ToArray());
        }

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

        public static Type GetDbType(this Type t)
        {
            Type u = Nullable.GetUnderlyingType(t);
            if (u == null)
            {
                u = t;
            }
            return u;
        }


        public static string TableNameMapping(string className)
        {
            if (className == "Broker")
                return "ST_B_Broker";
            if (className == "BrokerCode")
                return "ST_B_BrokerCode";
            if (className == "Product")
                return "ST_B_Product";
            if (className == "BrokerageFX")
                return "ST_B_FX";
            if (className == "BrokerageDerivative")
                return "ST_B_Derivative";
            if (className == "BrokerageFee")
                return "ST_B_Fee";
            if (className == "Discount")
                return "ST_B_Discount";
            if (className == "TaxFormula")
                return "ST_B_TaxFormula";
            if (className == "Tax")
                return "ST_B_Tax";
            if (className == "Reconciliation")
                return "ST_B_Reconciliation";
            return className;
        }

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




        #endregion




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
