using Microsoft.VisualStudio.TestTools.UnitTesting;
using Scrabble.Game_Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scrabble.Game_Objects.Tests
{
    [TestClass()]
    public class GameBoardSquareTests
    {
        [TestMethod()]
        public void GameBoardSquareTest()
        {
            bool exceptionWasThrown = false;
            try
            {
                GameBoardSquare gbs = new GameBoardSquare(0, 0, 1, 1);
            }
            catch (Exception)
            {
                exceptionWasThrown = true;
            }

            Assert.IsFalse(exceptionWasThrown);
            exceptionWasThrown = false;


            GameBoardSquare gbsCenter = new GameBoardSquare(7, 7, 1, 1);
            Assert.IsTrue(gbsCenter.IsStartSquare);

            try
            {
                GameBoardSquare gbs = new GameBoardSquare(14, 14, 1, 1);
            }
            catch (Exception)
            {
                exceptionWasThrown = true;
            }

            Assert.IsFalse(exceptionWasThrown);
            exceptionWasThrown = false;

            try
            {
                GameBoardSquare gbs = new GameBoardSquare(-1, 0, 1, 1);
            }
            catch (Exception)
            {
                exceptionWasThrown = true;
            }

            Assert.IsTrue(exceptionWasThrown);
            exceptionWasThrown = false;

            try
            {
                GameBoardSquare gbs = new GameBoardSquare(0, -1, 1, 1);
            }
            catch (Exception)
            {
                exceptionWasThrown = true;
            }

            Assert.IsTrue(exceptionWasThrown);
            exceptionWasThrown = false;

            try
            {
                GameBoardSquare gbs = new GameBoardSquare(15, 0, 1, 1);
            }
            catch (Exception)
            {
                exceptionWasThrown = true;
            }

            Assert.IsTrue(exceptionWasThrown);
            exceptionWasThrown = false;

            try
            {
                GameBoardSquare gbs = new GameBoardSquare(0, 15, 1, 1);
            }
            catch (Exception)
            {
                exceptionWasThrown = true;
            }

            Assert.IsTrue(exceptionWasThrown);
            exceptionWasThrown = false;

            try
            {
                GameBoardSquare gbs = new GameBoardSquare(0, 0, 2, 2);
            }
            catch (Exception)
            {
                exceptionWasThrown = true;
            }

            Assert.IsTrue(exceptionWasThrown);
            exceptionWasThrown = false;

            try
            {
                GameBoardSquare gbs = new GameBoardSquare(0, 0, 4, 1);
            }
            catch (Exception)
            {
                exceptionWasThrown = true;
            }

            Assert.IsTrue(exceptionWasThrown);
            exceptionWasThrown = false;

            try
            {
                GameBoardSquare gbs = new GameBoardSquare(0, 0, 1, 4);
            }
            catch (Exception)
            {
                exceptionWasThrown = true;
            }

            Assert.IsTrue(exceptionWasThrown);
            exceptionWasThrown = false;

            try
            {
                GameBoardSquare gbs = new GameBoardSquare(0, 0, 0, 1);
            }
            catch (Exception)
            {
                exceptionWasThrown = true;
            }

            Assert.IsTrue(exceptionWasThrown);
            exceptionWasThrown = false;

            try
            {
                GameBoardSquare gbs = new GameBoardSquare(0, 0, 1, 0);
            }
            catch (Exception)
            {
                exceptionWasThrown = true;
            }

            Assert.IsTrue(exceptionWasThrown);
            exceptionWasThrown = false;
        }

        [TestMethod()]
        public void IsEmptyTest()
        {
            LetterTile lt = new LetterTile('n', 0);
            GameBoardSquare gbs = new GameBoardSquare(0, 0, 1, 1);
            Assert.IsTrue(gbs.IsEmpty());

            Assert.IsTrue(gbs.ContainedLetterTile.Equals(lt));

            lt = new LetterTile('A', 1);
            gbs.InsertLetterTile(lt);

            Assert.IsFalse(gbs.IsEmpty());
            Assert.IsFalse(gbs.ContainedLetterTile.Equals(new LetterTile('n', 0)));
        }

        [TestMethod()]
        public void InsertLetterTileTest()
        {
            GameBoardSquare gbs = new GameBoardSquare(0, 0, 1, 1);
            LetterTile lt = new LetterTile('A', 1);
            gbs.InsertLetterTile(lt);
            Assert.IsFalse(gbs.IsEmpty());
        }

        [TestMethod()]
        public void RemoveLetterTileTest()
        {
            GameBoardSquare gbs = new GameBoardSquare(0, 0, 1, 1);
            LetterTile lt1 = new LetterTile('A', 1);
            gbs.InsertLetterTile(lt1);
            Assert.IsFalse(gbs.IsEmpty());

            LetterTile lt2 = gbs.RemoveLetterTile();
            Assert.IsTrue(gbs.IsEmpty());
        }

        [TestMethod()]
        public void RemoveScoreMultipliersTest()
        {
            GameBoardSquare gbs = new GameBoardSquare(0, 0, 3, 1);
            gbs.RemoveScoreMultipliers();
            Assert.IsTrue(gbs.LetterMultiplier == 1 && gbs.WordMultiplier == 1);

            gbs = new GameBoardSquare(0, 0, 1, 3);
            gbs.RemoveScoreMultipliers();
            Assert.IsTrue(gbs.LetterMultiplier == 1 && gbs.WordMultiplier == 1);
        }
    }
}
