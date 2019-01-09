using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyCSharp.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCSharp.Utility.Tests
{
    [TestClass()]
    public class MyCsvHelperTests
    {
        [TestMethod()]
        public void ReadCsvTest()
        {
            string path = @"C:\Users\11151\Downloads\testError\NT20181220000534\0534lofts.csv";
            //string path = @"C:\Users\11151\Downloads\testError\NT20181226000273\lofts.csv";
            var lofts = MyCsvHelper.ReadCsv<TCS_Lofting>(path);
            Assert.AreEqual(34, lofts.Count);
        }

        public class TCS_Lofting
        {
            public string LoftingID { get; set; }
            public string MapPath2 { get; set; }
            public string PartDesc { get; set; }

        }

    }
}