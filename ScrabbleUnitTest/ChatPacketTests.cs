using Microsoft.VisualStudio.TestTools.UnitTesting;
using Scrabble;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scrabble.Tests
{
    [TestClass()]
    public class ChatPacketTests
    {
        [TestMethod()]
        public void ChatPacketTest()
        {
            ChatPacket cp = new ChatPacket(9001, "Blob", "Hello, world . . . or whatever.","0231");

            Assert.IsTrue(cp.PlayerID == 9001);
            Assert.IsTrue(cp.Username == "Blob");
            Assert.IsTrue(cp.Message == "Hello, world . . . or whatever.");
        }
    }
}