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
        public void ShouldCreateValidPlay()
        {
            // setup
            var letterTileA = new Scrabble.Game_Objects.LetterTile('A', 1);
            var letterTileN = new Scrabble.Game_Objects.LetterTile('N', 1);
            var letterTileT = new Scrabble.Game_Objects.LetterTile('T', 1);
            List<Scrabble.Game_Objects.LetterTile> word = new List<Scrabble.Game_Objects.LetterTile> { letterTileA, letterTileN, letterTileT };
            var coordsX = new List<int> { 0, 1, 2 };
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

            Assert.AreEqual(1, play.GetPlayerID());

            Assert.AreEqual(Scrabble.Play.TileAxis.Horizontal, play.GetTileAxis());

            // switching the coords to get a vertical play
            var play1 = new Scrabble.Play(word, coordsY, coordsX, 1);

            Assert.AreEqual(Scrabble.Play.TileAxis.Vertical, play1.GetTileAxis());
        }

        [TestMethod]
        public void ShouldThrowVaryingParallelListLength()
        {
            // setup
            var letterTileA = new Scrabble.Game_Objects.LetterTile('A', 1);
            var letterTileN = new Scrabble.Game_Objects.LetterTile('N', 1);
            var letterTileT = new Scrabble.Game_Objects.LetterTile('T', 1);
            List<Scrabble.Game_Objects.LetterTile> word = new List<Scrabble.Game_Objects.LetterTile> { letterTileA, letterTileN, letterTileT };
            var coordsX4 = new List<int> { 3, 2, 1, 0 };
            var coordsY = new List<int> { 0, 0, 0 };

            try
            {
                var play1 = new Scrabble.Play(word, coordsX4, coordsY, 1);

                // should error before this line.
                Assert.IsTrue(false);
            }
            catch (Scrabble.Play.VaryingParallelListLength err)
            {
                Console.WriteLine(err);
                Assert.IsTrue(true);
            }
            catch (Exception someOtherErr)
            {
                System.Diagnostics.Debug.WriteLine(someOtherErr);
                // shouldn't throw another type of error
                Assert.IsTrue(false);
            }
        }

        [TestMethod]
        public void ShouldThrowEmptyParallelList()
        {
            // setup
            List<Scrabble.Game_Objects.LetterTile> word = new List<Scrabble.Game_Objects.LetterTile> { };
            var coordsX = new List<int> { };
            var coordsY = new List<int> { };

            try
            {
                var play1 = new Scrabble.Play(word, coordsX, coordsY, 1);

                // should error before this line.
                Assert.IsTrue(false);
            }
            catch (Scrabble.Play.EmptyParrallelList err)
            {
                Console.WriteLine(err);
                Assert.IsTrue(true);
            }
            catch (Exception someOtherErr)
            {
                System.Diagnostics.Debug.WriteLine(someOtherErr);
                // shouldn't throw another type of error
                Assert.IsTrue(false);
            }
        }

        [TestMethod]
        public void ShouldThrowOutOfBoardBoundaries()
        {
            // setup
            var letterTileA = new Scrabble.Game_Objects.LetterTile('A', 1);
            var letterTileN = new Scrabble.Game_Objects.LetterTile('N', 1);
            var letterTileT = new Scrabble.Game_Objects.LetterTile('T', 1);
            List<Scrabble.Game_Objects.LetterTile> word = new List<Scrabble.Game_Objects.LetterTile> { letterTileA, letterTileN, letterTileT };
            var coordsX = new List<int> { -1, 0, 1 };
            var coordsY = new List<int> { 0, 0, 0 };

            try
            {
                var play1 = new Scrabble.Play(word, coordsX, coordsY, 1);

                // should error before this line.
                Assert.IsTrue(false);
            }
            catch (Scrabble.Play.OutOfBoardBoundaries err)
            {
                Console.WriteLine(err);
                Assert.IsTrue(true);
            }
            catch (Exception someOtherErr)
            {
                System.Diagnostics.Debug.WriteLine(someOtherErr);
                // shouldn't throw another type of error
                Assert.IsTrue(false);
            }
        }

        [TestMethod]
        public void ShouldThrowInvalidAxis()
        {
            // setup
            var letterTileA = new Scrabble.Game_Objects.LetterTile('A', 1);
            var letterTileN = new Scrabble.Game_Objects.LetterTile('N', 1);
            var letterTileT = new Scrabble.Game_Objects.LetterTile('T', 1);
            List<Scrabble.Game_Objects.LetterTile> word = new List<Scrabble.Game_Objects.LetterTile> { letterTileA, letterTileN, letterTileT };
            var incrementingCoordsX = new List<int> { 0, 1, 2 };
            var invalidCoordsY = new List<int> { 0, 0, 1 };
            var invalidCoordsX = new List<int> { 0, 0, 1 };
            var incrementingCoordsY = new List<int> { 0, 1, 2 };
            var halfIncrementingCoords = new List<int> { 0, 1, 1 };
            var halfStayingStillCoords = new List<int> { 0, 0, 1 };

            try
            {
                var play = new Scrabble.Play(word, incrementingCoordsX, invalidCoordsY, 1);

                // should error before this line.
                Assert.IsTrue(false);
            }
            catch (Scrabble.Play.InvalidAxis err)
            {
                Console.WriteLine(err);
                Assert.IsTrue(true);
            }
            catch (Exception someOtherErr)
            {
                // shouldn't throw another type of error
                System.Diagnostics.Debug.WriteLine(someOtherErr);
                Assert.IsTrue(false);
            }

            try
            {
                var play = new Scrabble.Play(word, invalidCoordsX, incrementingCoordsY, 1);

                // should error before this line.
                Assert.IsTrue(false);
            }
            catch (Scrabble.Play.InvalidAxis err)
            {
                Console.WriteLine(err);
                Assert.IsTrue(true);
            }
            catch (Exception someOtherErr)
            {
                System.Diagnostics.Debug.WriteLine(someOtherErr);
                // shouldn't throw another type of error
                Assert.IsTrue(false);
            }

            try
            {
                var play = new Scrabble.Play(word, incrementingCoordsX, incrementingCoordsY, 1);

                // should error before this line.
                Assert.IsTrue(false);
            }
            catch (Scrabble.Play.InvalidAxis err)
            {
                Console.WriteLine(err);
                Assert.IsTrue(true);
            }
            catch (Exception someOtherErr)
            {
                System.Diagnostics.Debug.WriteLine(someOtherErr);
                // shouldn't throw another type of error
                Assert.IsTrue(false);
            }

            try
            {
                var play = new Scrabble.Play(word, halfIncrementingCoords, halfStayingStillCoords, 1);

                // should error before this line.
                Assert.IsTrue(false);
            }
            catch (Scrabble.Play.InvalidAxis err)
            {
                Assert.IsTrue(true);
            }
            catch (Exception someOtherErr)
            {
                System.Diagnostics.Debug.WriteLine(someOtherErr);
                // shouldn't throw another type of error
                Assert.IsTrue(false);
            }

            try
            {
                var play = new Scrabble.Play(word, halfStayingStillCoords, halfIncrementingCoords, 1);

                // should error before this line.
                Assert.IsTrue(false);
            }
            catch (Scrabble.Play.InvalidAxis err)
            {
                Console.WriteLine(err);
                Assert.IsTrue(true);
            }
            catch (Exception someOtherErr)
            {
                System.Diagnostics.Debug.WriteLine(someOtherErr);
                // shouldn't throw another type of error
                Assert.IsTrue(false);
            }
        }

        [TestMethod]
        public void ShouldThrowDuplicateCoords ()
        {
            // setup
            var letterTileA = new Scrabble.Game_Objects.LetterTile('A', 1);
            var letterTileN = new Scrabble.Game_Objects.LetterTile('N', 1);
            var letterTileT = new Scrabble.Game_Objects.LetterTile('T', 1);

            List<Scrabble.Game_Objects.LetterTile> word = new List<Scrabble.Game_Objects.LetterTile> { letterTileA, letterTileN, letterTileT };
            try
            {
                var play = new Scrabble.Play(word, new List<int>() { 0, 1, 0 }, new List<int>() { 0, 0, 0 }, 1);

                // should error before this line.
                Assert.IsTrue(false);
            }
            catch (Scrabble.Play.DuplicateCoords err)
            {
                Console.WriteLine(err);
                Assert.IsTrue(true);
            }
            catch (Exception someOtherErr)
            {
                System.Diagnostics.Debug.WriteLine(someOtherErr);
                // shouldn't throw another type of error
                Assert.IsTrue(false);
            }
        }


    }
}
