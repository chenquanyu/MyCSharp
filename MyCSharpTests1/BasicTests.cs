using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MyCSharp.Algorithms.Tests
{
    [TestClass()]
    public class BasicTests
    {
        [TestMethod()]
        public void ObjectTest()
        {
            var a = new object[] { 1, 2, true };
            var b = new object[] { 1, 2, true };
            Assert.IsTrue(a[0].Equals(b[0]));
            Assert.IsFalse(a[0] == b[0]);
        }
    }
}