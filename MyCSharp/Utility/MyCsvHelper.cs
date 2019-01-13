using CsvHelper;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCSharp.Utility
{
    public class MyCsvHelper
    {

        public static List<T> ReadCsv<T>(string filePath)
        {
            var result = new List<T>();

            using (var stream = File.OpenRead(filePath))
            using (var reader = new StreamReader(stream))
            using (var csv = new CsvReader(reader))
            {
                csv.Configuration.TypeConverterOptionsCache.GetOptions<int?>().NullValues.Add("NULL");
                csv.Configuration.TypeConverterOptionsCache.GetOptions<decimal?>().NullValues.Add("NULL");
                csv.Configuration.TypeConverterOptionsCache.GetOptions<DateTime?>().NullValues.Add("NULL");
                csv.Configuration.TypeConverterOptionsCache.GetOptions<double?>().NullValues.Add("NULL");
                csv.Configuration.TypeConverterOptionsCache.GetOptions<bool?>().NullValues.Add("NULL");

                result = csv.GetRecords<T>().ToList();
                return result;
            }

        }


    }
}
