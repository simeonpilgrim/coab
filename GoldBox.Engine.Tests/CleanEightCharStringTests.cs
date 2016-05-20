using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GoldBox.Engine
{
    [TestClass]
    public class CleanEightCharStringTests
    {
        [TestMethod]
        public void TestMethod_clean_string()
        {
            var _stringCleaner = new CleanEightCharString();
            Assert.AreEqual("", _stringCleaner.clean_string(""));
            Assert.AreEqual("12345678", _stringCleaner.clean_string("123456789"));
            Assert.AreEqual("", _stringCleaner.clean_string(" ,.*,?/\\:;|"));
            Assert.AreEqual("12345678", _stringCleaner.clean_string(" 12345678"));
            Assert.AreEqual("1234567", _stringCleaner.clean_string(" 1234567|"));
            //TODO Check this. Code says only to remove beginning and end instances of unclean characters
            //Assert.AreEqual("12345678", _stringCleaner.clean_string(" 1234567|8")); 
        }
    }
}
