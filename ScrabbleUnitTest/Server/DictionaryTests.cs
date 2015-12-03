using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Scrabble.Server.Tests
{
    [TestClass()]
    public class DictionaryTests
    {
        [TestMethod()]
        public void DictionaryTest()
        {
            Assert.IsTrue(System.IO.File.Exists("dictionary.txt"));
        }

        [TestMethod()]
        public void containsWordTest()
        {
            Dictionary dictionary = new Dictionary("dictionary.txt");
            Assert.IsTrue(dictionary.containsWord("alphabet"));
            Assert.IsFalse(dictionary.containsWord("jsjsjsjs"));
        }
    }
}