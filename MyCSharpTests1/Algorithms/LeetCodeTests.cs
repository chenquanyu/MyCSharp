using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyCSharp.Algorithms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCSharp.Algorithms.Tests
{
    [TestClass()]
    public class LeetCodeTests
    {
        [TestMethod()]
        public void IsMatchTest()
        {
            Assert.IsTrue(LeetCode.IsMatch("aaa", "a.*"));
            Assert.IsTrue(LeetCode.IsMatch("aaa", "aaa"));
            Assert.IsTrue(LeetCode.IsMatch("aaab", "a*b"));
            Assert.IsTrue(LeetCode.IsMatch("aaab", ".*"));
            Assert.IsFalse(LeetCode.IsMatch("aaab", ".*c"));
        }
    }
}