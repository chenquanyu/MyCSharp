using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyCSharp.ClrPractice;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCSharp.ClrPractice.Tests
{
    [TestClass()]
    public class AwaitSeqTests
    {
        [TestMethod()]
        public void DoWorksTest()
        {
            var obj = new AwaitSeq();
            var result = obj.DoWorks().Result;
            Console.WriteLine(result);
        }
    }
}