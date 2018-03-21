using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCSharp.Utility
{
    public static class ExcelHelper
    {
        /// <summary>
        /// Read Excel Sheet to DataTable
        /// </summary>
        /// <param name="path"></param>
        /// <param name="sheetName"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public static DataTable ReadExcel(string path, string sheetName = "", int index = 0)
        {
            var result = new DataTable();
            string connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + path + ";" + "Extended Properties='Excel 12.0 Xml;HDR=YES;IMEX=1;MAXSCANROWS=0'";

            using (OleDbConnection conn = new OleDbConnection(connectionString))
            {
                conn.Open();

                OleDbCommand cmd = new OleDbCommand();
                cmd.Connection = conn;
                if (string.IsNullOrEmpty(sheetName))
                {
                    sheetName = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null).Rows[index]["TABLE_NAME"].ToString().TrimEnd('$');
                }

                cmd.CommandText = string.Format("SELECT * FROM [{0}$]", sheetName);
                result.TableName = sheetName;

                OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                da.Fill(result);
                conn.Close();
            }

            return result;
        }

    }
}
