using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MyTests
{
    [TestClass]
    public class UnitTestGuy
    {
        [TestMethod]
        public void TestMethod1()
        {
            Assert.AreEqual(2, 1 + 1);
        }
    }
}
