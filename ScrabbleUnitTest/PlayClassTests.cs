//-----------------------------------------------------------------------
// <copyright file="FirstThingsFirstUnitTests.cs" company="Scrabble Project Developers">
//     Copyright (c) Scrabble Project Developers. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace ScrabbleUnitTest
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// Unit tests for the Play Class
    /// </summary>
    [TestClass]
    public class PlayClassTests
    {
        /// <summary>
        /// Tests the Play class
        /// </summary>
        [TestMethod]
        public void PlayClassTest()
        {
            // setup
            var letterTileA = new Scrabble.Game_Objects.LetterTile('A', 1);
            var letterTileN = new Scrabble.Game_Objects.LetterTile('N', 1);
            var letterTileT = new Scrabble.Game_Objects.LetterTile('T', 1);
            List<Scrabble.Game_Objects.LetterTile> word = new List<Scrabble.Game_Objects.LetterTile> { letterTileA, letterTileN, letterTileT };
            var coordsX = new List<int> { 0, 1, 2 };
            var coordsX4 = new List<int> { 3, 2, 1, 0 };
            var coordsY = new List<int> { 0, 0, 0 };

            var play = new Scrabble.Play(word, coordsX, coordsY, 1);

            // assertions
            Assert.AreEqual(3, play.GetParallelListLength());

            Assert.AreEqual(0, play.GetCoordinateX(0));
            Assert.AreEqual(1, play.GetCoordinateX(1));
            Assert.AreEqual(2, play.GetCoordinateX(2));

            Assert.AreEqual(0, play.GetCoordinateY(0));
            Assert.AreEqual(0, play.GetCoordinateY(1));
            Assert.AreEqual(0, play.GetCoordinateY(2));

            Assert.AreEqual(letterTileA, play.GetLetterTile(0));
            Assert.AreEqual(letterTileN, play.GetLetterTile(1));
            Assert.AreEqual(letterTileT, play.GetLetterTile(2));

            // should throw VaryingParallelListLength Exception
            try
            {
                var play1 = new Scrabble.Play(word, coordsX4, coordsY, 1);

                // should error before this line.
                Assert.IsTrue(false);
            }
            catch (Exception err)
            {
                Assert.IsTrue(true);
            }
        }
    }
}
