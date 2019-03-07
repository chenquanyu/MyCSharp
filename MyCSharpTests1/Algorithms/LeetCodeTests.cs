using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyCSharp.Algorithms;
using Newtonsoft.Json;
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
        private LeetCode leetCode = new LeetCode();

        [TestMethod()]
        public void IsMatchTest()
        {
            Assert.IsTrue(leetCode.IsMatch("aaa", "a.*"));
            Assert.IsTrue(leetCode.IsMatch("aaa", "aaa"));
            Assert.IsTrue(leetCode.IsMatch("aaab", "a*b"));
            Assert.IsTrue(leetCode.IsMatch("aaab", ".*"));
            Assert.IsFalse(leetCode.IsMatch("aaab", ".*c"));
        }

        [TestMethod()]
        public void IsMatchDPTest()
        {
            Assert.IsTrue(leetCode.IsMatchDP("aaa", "a.*"));
            Assert.IsTrue(leetCode.IsMatchDP("aaa", "aaa"));
            Assert.IsTrue(leetCode.IsMatchDP("aaab", "a*b"));
            Assert.IsTrue(leetCode.IsMatchDP("aaab", ".*"));
            Assert.IsFalse(leetCode.IsMatchDP("aaab", ".*c"));
        }

        [TestMethod()]
        public void CountHeightTest()
        {
            Assert.AreEqual(0, leetCode.CountHeight(new int[] { }));
            Assert.AreEqual(1, leetCode.CountHeight(new int[] { 100, 100 }));
            Assert.AreEqual(2, leetCode.CountHeight(new int[] { 100, 101 }));
            Assert.AreEqual(2, leetCode.CountHeight(new int[] { 9, 9, 2 }));
            Assert.AreEqual(2, leetCode.CountHeight(new int[] { 9, 9, 10 }));
            Assert.AreEqual(2, leetCode.CountHeight(new int[] { 2, 9, 9 }));
            Assert.AreEqual(2, leetCode.CountHeight(new int[] { 10, 9, 9 }));
            Assert.AreEqual(5, leetCode.CountHeight(new int[] { 10, 9, 8, 9, 8, 9 }));
            Assert.AreEqual(5, leetCode.CountHeight(new int[] { 10, 9, 8, 9, 8, 9, 10 }));

        }


        [TestMethod()]
        public void SubstringTest()
        {
            Assert.AreEqual(string.Empty, "1".Substring(1));
        }

        [TestMethod()]
        public void IsMatchWildTest()
        {
            Assert.AreEqual(true, leetCode.IsMatchWild("aa", "****a****?"));
        }

        [TestMethod()]
        public void RotateTest()
        {
            var matrix = JsonConvert.DeserializeObject<int[,]>("[[1,2,3],[4,5,6],[7,8,9]]");
            leetCode.Rotate(matrix);
            Assert.AreEqual(7, matrix[0, 0]);
        }

        [TestMethod()]
        public void MyPowTest()
        {
            Assert.AreEqual(0, leetCode.MyPow(2, int.MinValue));
            Assert.AreEqual(1024, leetCode.MyPow(2, 10));
            Assert.AreEqual(0.25, leetCode.MyPow(2, -2));
            Assert.AreEqual(16, leetCode.MyPow(2, 4));

            // Assert.Fail();
        }
    }
}