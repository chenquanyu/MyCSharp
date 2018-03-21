using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyCSharp.Models;
using System.Configuration;

namespace MyCSharp.DAL
{
    public class DALBase
    {
        public TestKrainContext context = new TestKrainContext();

        public int BatchInsert<T>(List<T> objs)
        {
            //var sql = dbHelper.ConstructInsertSql(objs, out paras);
            //var sql = Utility.ConstructInsertSql(objs);
            //using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["TestKrain1"].ConnectionString))
            //{
            //    var cmd = new SqlCommand(sql, conn);
            //    conn.Open();
            //    return cmd.ExecuteNonQuery();
            //}

            var dt = Utils.ConstructDataTable(objs);
            using (var destinationConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["TestKrain1"].ConnectionString))
            using (SqlBulkCopy bulkCopy = new SqlBulkCopy(destinationConnection))
            {
                destinationConnection.Open();
                bulkCopy.DestinationTableName = dt.TableName;
                // Write from the source to the destination.
                bulkCopy.WriteToServer(dt);
            }

            return objs.Count;
            // return context.Database.ExecuteSqlCommand(sql);
        }

        public int BatchUpdate<T>(List<T> objs)
        {
            //var sql = dbHelper.ConstructUpdateSql(objs, out paras);
            var sql = Utils.ConstructUpdateSql(objs);
            return context.Database.ExecuteSqlCommand(sql);
        }

        public int BatchDelete<T>(List<T> objs)
        {
            var sql = Utils.ConstructDeleteSql(objs);
            return context.Database.ExecuteSqlCommand(sql);
        }


    }
}
