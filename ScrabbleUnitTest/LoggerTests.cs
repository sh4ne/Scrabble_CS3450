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
    public class LoggerTests
    {
        [TestMethod()]
        public void LoggerTest()
        {
            Scrabble.Logger testLog = new Logger();
            Scrabble.Logger test2 = new Logger();
            Assert.IsFalse(testLog.Equals(test2));
        }

        [TestMethod()]
        public void LogMessageTest()
        {
            Scrabble.Logger tempLog = new Logger();
            tempLog.LogMessage("Testing...");
            string inFile = System.IO.File.ReadAllText(System.IO.Directory.GetCurrentDirectory() + "\\Logs\\" + tempLog.now + "_VERBOSE_LOG.txt");
            Assert.IsFalse(inFile.Equals("Testing..."));     
        }

        [TestMethod()]
        public void LogWarningTest()
        {
            Scrabble.Logger tempLog = new Logger();
            tempLog.LogWarning("Warning...","Unit Test");
            string inFile = System.IO.File.ReadAllText(System.IO.Directory.GetCurrentDirectory() + "\\Logs\\" + tempLog.now + "_VERBOSE_LOG.txt");
            Console.Write(inFile);
            Assert.IsFalse(inFile.Equals("Testing...*** WARNING - Unit Test: Warning... ***"));
        }

        [TestMethod()]
        public void LogErrorTest()
        {
            Scrabble.Logger tempLog = new Logger();
            tempLog.LogError("Error...", "Unit Test");
            string inFile = System.IO.File.ReadAllText(System.IO.Directory.GetCurrentDirectory() + "\\Logs\\" + tempLog.now + "_VERBOSE_LOG.txt");
            Console.Write(inFile);
            Assert.IsFalse(inFile.Equals("!!!!!!!ERROR - " + "Unit Test" + ": " + "Error..." + " !!!!!!!"));
        }

        [TestMethod()]
        public void AddToGameStateTest()
        {
            Scrabble.Logger tempLog = new Logger();
            tempLog.AddToGameState("Testing...");
            string inFile = System.IO.File.ReadAllText(System.IO.Directory.GetCurrentDirectory() + "\\Logs\\" + tempLog.now + "_GAMESTATE_LOG.txt");
            Assert.IsFalse(inFile.Equals("Testing..."));
        }
    }
}