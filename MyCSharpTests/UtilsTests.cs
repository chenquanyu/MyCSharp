using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyCSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCSharp.Tests
{
    [TestClass()]
    public class UtilsTests
    {
        [TestMethod()]
        public void SerializeToMemoryTest()
        {
            var obj = new List<string> { "One", "Two", "Three", "Four" };
            var stream = Utils.SerializeToMemory(obj);
            var obj1 = Utils.DeserializeFromMemory(stream) as List<string>;

            Assert.AreEqual(4, obj1.Count);

        }
    }
}