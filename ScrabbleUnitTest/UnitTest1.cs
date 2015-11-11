using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Scrabble;
using System.Collections.Generic;


namespace ScrabbleUnitTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            Assert.IsTrue(true);
        }
    }

    [TestClass]
    public class ClassTests
    {
        [TestMethod]
        public void PlayClassTest()
        {
            // setup
            var aLetterTile = new Scrabble.Game_Objects.LetterTile('A', 1);
            var nLetterTile = new Scrabble.Game_Objects.LetterTile('N', 1);
            var tLetterTile = new Scrabble.Game_Objects.LetterTile('T', 1);
            List<Scrabble.Game_Objects.LetterTile> word = new List<Scrabble.Game_Objects.LetterTile>{aLetterTile, nLetterTile, tLetterTile};
            var xCoords = new List<int>{0,1,2};
            var xCoords4 = new List<int>{ 3, 2, 1, 0 };
            var yCoords = new List<int>{0,0,0};

            var play = new Play(word, xCoords, yCoords, 1);
            // assertions
            Assert.AreEqual(3, play.GetParallelListLength());

            Assert.AreEqual(0, play.GetXCoordinate(0));
            Assert.AreEqual(1, play.GetXCoordinate(1));
            Assert.AreEqual(2, play.GetXCoordinate(2));

            Assert.AreEqual(0, play.GetYCoordinate(0));
            Assert.AreEqual(0, play.GetYCoordinate(1));
            Assert.AreEqual(0, play.GetYCoordinate(2));

            Assert.AreEqual(aLetterTile, play.GetLetterTile(0));
            Assert.AreEqual(nLetterTile, play.GetLetterTile(1));
            Assert.AreEqual(tLetterTile, play.GetLetterTile(2));

            // should throw VaryingParallelListLength Exception
            try
            {
                var play1 = new Play(word, xCoords4, yCoords, 1);
                // should error before this line.
                Assert.IsTrue(false);
            }
            catch (VaryingParallelListLength err)
            {
                Assert.IsTrue(true);
            }


        }
    }
}
