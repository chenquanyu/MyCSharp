using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyCSharp.Algorithms;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
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

        [TestMethod()]
        public void MySqrtTest()
        {
            int range = 1000000;

            for (var i = 0; i <= range; i++)
            {
                int expected = (int)Math.Sqrt(i);
                int actual = leetCode.MySqrt(i);
                Assert.AreEqual(expected, actual);
            }

            for (var i = int.MaxValue; i >= int.MaxValue - range; i--)
            {
                int expected = (int)Math.Sqrt(i);
                int actual = leetCode.MySqrt(i);
                Assert.AreEqual(expected, actual);
            }

        }

        [TestMethod()]
        public void MySqrt2Test()
        {
            int range = 1000000;

            for (var i = 0; i <= range; i++)
            {
                int expected = (int)Math.Sqrt(i);
                int actual = leetCode.MySqrt2(i);
                Assert.AreEqual(expected, actual);
            }

            for (var i = int.MaxValue; i >= int.MaxValue - range; i--)
            {
                int expected = (int)Math.Sqrt(i);
                int actual = leetCode.MySqrt2(i);
                Assert.AreEqual(expected, actual);
            }


        }

        [TestMethod()]
        public void IsValidSudokuTest()
        {

            var case1 = JsonConvert.DeserializeObject<char[][]>("[[\"5\", \"3\", \".\", \".\", \"7\", \".\", \".\", \".\", \".\"],  [\"6\", \".\", \".\", \"1\", \"9\", \"5\", \".\", \".\", \".\"],  [\".\", \"9\", \"8\", \".\", \".\", \".\", \".\", \"6\", \".\"],  [\"8\", \".\", \".\", \".\", \"6\", \".\", \".\", \".\", \"3\"],  [\"4\", \".\", \".\", \"8\", \".\", \"3\", \".\", \".\", \"1\"],  [\"7\", \".\", \".\", \".\", \"2\", \".\", \".\", \".\", \"6\"],  [\".\", \"6\", \".\", \".\", \".\", \".\", \"2\", \"8\", \".\"],  [\".\", \".\", \".\", \"4\", \"1\", \"9\", \".\", \".\", \"5\"],  [\".\", \".\", \".\", \".\", \"8\", \".\", \".\", \"7\", \"9\"]]");

            Assert.IsTrue(leetCode.IsValidSudoku(case1));

        }


        [TestMethod()]
        public void ArrayTest()
        {

            var case1 = new char[3][];
            case1[0] = new char[] { '1', '2', '3' };
            case1[1] = new char[] { '4', '5', '6' };
            case1[2] = new char[] { '7', '8', '9' };

            var case2 = new char[3, 3];
            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 3; j++)
                {
                    case2[i, j] = (i * 3 + 1 + j).ToString()[0];
                }

            var str1 = JsonConvert.SerializeObject(case1);
            var str2 = JsonConvert.SerializeObject(case2);

            Assert.AreEqual(str1, str2);
        }

        [TestMethod()]
        public void SetZeroesTest()
        {
            var case1 = new int[3][];
            case1[0] = new int[] { 1, 0, 3 };
            case1[1] = new int[] { 4, 0, 6 };
            case1[2] = new int[] { 7, 8, 9 };

            leetCode.SetZeroes(case1);


            var str1 = JsonConvert.SerializeObject(case1);

        }

        [TestMethod()]
        public void MinWindowTest()
        {
            string s = "ADOBECODEBANC";
            string t = "ABC";

            var result = leetCode.MinWindow(s, t);

        }

        [TestMethod()]
        public void SubsetsTest()
        {
            var nums = new int[] { 1, 2, 3 };
            var result = leetCode.Subsets(nums);
            Console.WriteLine(JsonConvert.SerializeObject(result));

        }

        [TestMethod()]
        public void ExistTest()
        {
            var board = JsonConvert.DeserializeObject<char[][]>("[[\"a\",\"b\"],[\"c\",\"d\"]]");
            string word = "abdc";

            Assert.IsTrue(leetCode.Exist(board, word));

            board = JsonConvert.DeserializeObject<char[][]>("[[\"a\",\"b\"],[\"c\",\"d\"]]");
            word = "abdcd";

            Assert.IsFalse(leetCode.Exist(board, word));

            board = JsonConvert.DeserializeObject<char[][]>("[[\"a\"]]");
            word = "a";

            Assert.IsTrue(leetCode.Exist(board, word));


            board = JsonConvert.DeserializeObject<char[][]>("[[\"C\",\"A\",\"A\"],[\"A\",\"A\",\"A\"],[\"B\",\"C\",\"D\"]]");
            word = "AAB";

            Assert.IsTrue(leetCode.Exist(board, word));


        }

    }
}