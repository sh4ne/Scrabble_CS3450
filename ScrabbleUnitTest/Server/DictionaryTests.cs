using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Scrabble.Server.Tests
{
    [TestClass()]
    public class DictionaryTests
    {
        [TestMethod()]
        public void containsWordTest()
        {
            Dictionary dictionary = new Dictionary();
            Assert.IsTrue(dictionary.containsWord("alphabet"));
        }

        [TestMethod()]
        public void notContainsWordTest()
        {
            Dictionary dictionary = new Dictionary();
            Assert.IsFalse(dictionary.containsWord("jsjsjsjsjs"));
        }

    }
}