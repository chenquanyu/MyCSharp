using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MyCSharp.Utility
{
    public static class SqlHelper
    {
        public static string ConstructInsertSql<T>(List<T> toInsert)
        {
            string tableName = GetTableName<T>();
            List<PropertyInfo> props = typeof(T).GetProperties().ToList();
            props.Remove(typeof(T).GetProperty("Id"));
            string colNames = string.Join(",", props.Select(p => string.Format("[{0}]", p.Name)).ToArray());
            StringBuilder result = new StringBuilder();
            foreach (T item in toInsert)
            {
                string values = string.Join(",", props.Select(p => ReflectionHelper.GetValueByPropertyInfo(item, p)).ToArray());
                result.AppendLine(string.Format("insert into {0} ({1}) values ({2})", tableName, colNames, values));
            }

            return result.ToString();
        }

        public static string ConstructUpdateSql<T>(List<T> toInsert)
        {
            Type type = typeof(T);
            string tableName = GetTableName<T>();
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
                    pairs.Add(string.Format("[{0}] = {1}", p.Name, ReflectionHelper.GetValueByPropertyInfo(item, p)));
                }
                result.Append(string.Join(",", pairs.ToArray()));
                result.AppendLine(string.Format(" where Id = {0}", ReflectionHelper.GetValueByPropertyInfo(item, propId)));
            }

            return result.ToString();
        }

        public static string ConstructDeleteSql<T>(List<T> objs)
        {
            List<string> orCondtions = new List<string>();
            var keys = GetKeyColumns<T>();
            Type type = typeof(T);
            foreach (var obj in objs)
            {
                var andConds = new List<string>();
                foreach (var key in keys)
                {
                    andConds.Add($"{key} = {ReflectionHelper.GetValueByPropertyInfo(obj, type.GetProperty(key))}");
                }
                string andCond = string.Join(" AND ", andConds);
                orCondtions.Add($"({andCond})");
            }

            var condition = string.Join(" OR ", orCondtions);
            string sql = $"delete from {GetTableName<T>()} where ({condition})";

            return sql;
        }

        public static string GetTableName<T>()
        {
            return typeof(T).Name;
        }

        public static List<string> GetKeyColumns<T>()
        {
            var result = new List<string>
            {
                "Id"
            };
            return result;
        }




    }
}
