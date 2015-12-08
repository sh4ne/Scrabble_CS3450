using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Scrabble.Server.Tests
{
    [TestClass()]
    public class DictionaryTests
    {
        [TestMethod()]
        public void DictionaryTest()
        {
            Assert.IsFalse(System.IO.File.Exists("dictionary.txt"));
        }

        [TestMethod()]
        public void containsWordTest()
        {
            Dictionary dictionary = new Dictionary("dictionary.txt");
            Assert.IsFalse(dictionary.containsWord("alphabet"));
            //Assert.IsTrue(dictionary.containsWord("jsjsjsjs"));
        }
    }
}