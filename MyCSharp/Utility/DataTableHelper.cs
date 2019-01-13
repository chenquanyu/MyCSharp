using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCSharp.Utility
{
    public static class DataTableHelper
    {

        /// <summary>
        /// Convert List To DataTable
        /// </summary>
        public static DataTable ConvertToDataTable<T>(IEnumerable<T> toInsert, string tableName = null)
        {
            var result = new DataTable();
            var dataType = typeof(T);
            var props = dataType.GetProperties().ToList();
            result.TableName = string.IsNullOrEmpty(tableName) ? dataType.Name : tableName;
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

        public static Type GetDbType(this Type t)
        {
            Type u = Nullable.GetUnderlyingType(t);
            if (u == null)
            {
                u = t;
            }
            return u;
        }





    }
}
