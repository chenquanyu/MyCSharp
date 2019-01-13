using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyCSharp.Utility;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCSharp.Utility.Tests
{
    [TestClass()]
    public class MyCsvHelperTests
    {
        public const string testFolder = "TestData";

        [TestMethod()]
        public void ReadCsvTest()
        {
            string path = Path.Combine(testFolder, "lofts.csv");
            var lofts = MyCsvHelper.ReadCsv<TCS_Lofting>(path);
            Assert.AreEqual(34, lofts.Count);
        }

        public class TCS_Lofting
        {
            public string LoftingID { get; set; }
            public string MapPath2 { get; set; }
            public string PartDesc { get; set; }
            public int? Quantity { get; set; }
            public int? VersionModify { get; set; }

        }

    }
}