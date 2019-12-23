using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyUtility.Models;
using System;
using System.Collections.Generic;
using System.Text;
using MyUtility.Utility;

namespace MyUtility.Models.Tests
{
    [TestClass()]
    public class MemberTests
    {
        [TestMethod()]
        public void To3BytesTest()
        {
            var member = new Member { Type = 2, Number = 15, Selected = false };
            string result = member.To3Bytes().ToBinaryString();
            Assert.AreEqual("01000000 00000000 01111000 ", result);
        }
    }
}