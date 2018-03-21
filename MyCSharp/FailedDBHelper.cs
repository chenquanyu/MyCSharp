using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MyCSharp
{
    public class FailedDBHelper
    {
        public FailedDBHelper(string pKey = "Id")
        {
            _pKey = pKey;
        }

        private string _pKey = "Id";

        public virtual string TableNameMapping(string className)
        {
            return className;
        }
        
        public string ConstructInsertSql<T>(List<T> toInsert, out List<SqlParameter> paraList)
        {
            paraList = new List<SqlParameter>();
            var type = typeof(T);
            string tableName = TableNameMapping(type.Name);
            List<PropertyInfo> props = type.GetProperties().ToList();
            props.Remove(type.GetProperty(_pKey));

            string colNames = string.Join(",", props.Select(p => string.Format("[{0}]", p.Name)).ToArray());

            StringBuilder result = new StringBuilder();
            int seq = 1;

            foreach (T item in toInsert)
            {
                var paramNames = new List<string>();
                foreach (var prop in props)
                {
                    var paraName = $"@{prop.Name}{seq}";
                    var paraValue = GetValue(item, prop);
                    paramNames.Add(paraName);
                    paraList.Add(new SqlParameter(paraName, paraValue));
                }

                var values = string.Join(",", paramNames);
                result.AppendLine(string.Format("insert into {0} ({1}) values ({2})", tableName, colNames, values));
                seq++;
            }

            return result.ToString();
        }

        public string ConstructUpdateSql<T>(List<T> toInsert, out List<SqlParameter> paraList)
        {
            paraList = new List<SqlParameter>();
            Type type = typeof(T);
            string tableName = TableNameMapping(type.Name);

            List<PropertyInfo> props = type.GetProperties().ToList();
            var propId = type.GetProperty(_pKey);
            props.Remove(propId);

            StringBuilder result = new StringBuilder();
            int seq = 1;

            foreach (T item in toInsert)
            {
                var operations = new List<string>();
                foreach (var prop in props)
                {
                    var paraName = $"@{prop.Name}{seq}";
                    var paraValue = GetValue(item, prop);
                    operations.Add($"[{prop.Name}] = {paraName}");
                    paraList.Add(new SqlParameter(paraName, paraValue));
                }

                var values = string.Join(",", operations);
                result.AppendLine($"update [{tableName}] set {values} where [{_pKey}] = @{_pKey}{seq}");
                paraList.Add(new SqlParameter($"@{_pKey}{seq}", propId.GetValue(item, null)));
                seq++;
            }

            return result.ToString();
        }

        public string ConstructDeleteSql<T>(List<T> objs, out List<SqlParameter> paraList)
        {
            paraList = new List<SqlParameter>();
            string tableName = TableNameMapping(typeof(T).Name);
            var prop = typeof(T).GetProperty(_pKey);

            int seq = 1;
            var paramNames = new List<string>();
            foreach (T item in objs)
            {
                var paraName = $"@{prop.Name}{seq}";
                var paraValue = GetValue(item, prop);
                paramNames.Add(paraName);
                paraList.Add(new SqlParameter(paraName, paraValue));
                seq++;
            }

            return string.Format("delete from {0} where {1} in ({2})", TableNameMapping(typeof(T).Name), _pKey, string.Join(",", paramNames));
        }

        private static object GetValue(object obj, PropertyInfo prop)
        {
            var value = prop.GetValue(obj);
            return value == null ? DBNull.Value : value;
        }
    }
}
