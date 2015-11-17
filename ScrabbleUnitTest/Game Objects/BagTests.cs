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
    public class BagTests
    {
        [TestMethod()]
        public void BagTest()
        {
            bool exceptionWasThrown = false;
            try
            {
                Bag bag = new Bag();
            }
            catch
            {
                exceptionWasThrown = true;
            }
            Assert.IsFalse(exceptionWasThrown);
        }

        [TestMethod()]
        public void DrawLetterTileTest()
        {
            // Make a bag, and make sure that you can draw tiles from it
            // equal to its starting LetterTileCount.
            Bag bag = new Bag();
            int letterTileCount = bag.LetterTileCount;
            List<LetterTile> ltl = new List<LetterTile>();
            try
            {
                for (int i = 0; i < letterTileCount; ++i)
                {
                    ltl.Add(bag.DrawLetterTile());
                }
            }
            catch
            {
                Assert.Fail();
            }
            // There should be 100 tiles in the bag to start with.
            Assert.IsTrue(ltl.Count == 100);

            // Make sure the point values of the LetterTile add to 187.
            int totalPoints = 0;
            
            for(int i = 0; i < ltl.Count; ++i)
            {
                totalPoints += ltl[i].PointValue;
            }
            Assert.IsTrue(totalPoints == 187);

            // Make sure that drawing from an empty bag throws an exception.
            bool exceptionWasThrown = false;
            try
            {
                bag.DrawLetterTile();
            }
            catch
            {
                exceptionWasThrown = true;
            }
            Assert.IsTrue(exceptionWasThrown);

            // Make sure it's possible to put all the LetterTiles back into the bag.
            try
            {
                foreach(LetterTile lt in ltl)
                {
                    bag.InsertLetterTile(lt);
                }
            }
            catch
            {
                Assert.Fail();
            }
        }

        [TestMethod()]
        public void InsertLetterTileTest()
        {
            Bag bag = new Bag();

            // Make sure an exception is thrown if any LetterTile is added to a full bag.
            Assert.IsTrue(
                ExceptionGotThrown(' ', 0, bag) &&
                ExceptionGotThrown('A', 1, bag) &&
                ExceptionGotThrown('B', 3, bag) &&
                ExceptionGotThrown('C', 3, bag) &&
                ExceptionGotThrown('D', 2, bag) &&
                ExceptionGotThrown('E', 1, bag) &&
                ExceptionGotThrown('F', 4, bag) &&
                ExceptionGotThrown('G', 2, bag) &&
                ExceptionGotThrown('H', 4, bag) &&
                ExceptionGotThrown('I', 1, bag) &&
                ExceptionGotThrown('J', 8, bag) &&
                ExceptionGotThrown('K', 5, bag) &&
                ExceptionGotThrown('L', 1, bag) &&
                ExceptionGotThrown('M', 3, bag) &&
                ExceptionGotThrown('N', 1, bag) &&
                ExceptionGotThrown('O', 1, bag) &&
                ExceptionGotThrown('P', 3, bag) &&
                ExceptionGotThrown('Q', 10, bag) &&
                ExceptionGotThrown('R', 1, bag) &&
                ExceptionGotThrown('S', 1, bag) &&
                ExceptionGotThrown('T', 1, bag) &&
                ExceptionGotThrown('U', 1, bag) &&
                ExceptionGotThrown('V', 4, bag) &&
                ExceptionGotThrown('W', 4, bag) &&
                ExceptionGotThrown('X', 8, bag) &&
                ExceptionGotThrown('Y', 4, bag) &&
                ExceptionGotThrown('Z', 10, bag)
            );

            // Empty the bag
            for(int i = 0; i < bag.LetterTileCount;)
            {
                bag.DrawLetterTile();
            }

            // Make sure that at least one tile of every type can be added to the bag.
            Assert.IsFalse(
                ExceptionGotThrown(' ', 0, bag) &&
                ExceptionGotThrown('A', 1, bag) &&
                ExceptionGotThrown('B', 3, bag) &&
                ExceptionGotThrown('C', 3, bag) &&
                ExceptionGotThrown('D', 2, bag) &&
                ExceptionGotThrown('E', 1, bag) &&
                ExceptionGotThrown('F', 4, bag) &&
                ExceptionGotThrown('G', 2, bag) &&
                ExceptionGotThrown('H', 4, bag) &&
                ExceptionGotThrown('I', 1, bag) &&
                ExceptionGotThrown('J', 8, bag) &&
                ExceptionGotThrown('K', 5, bag) &&
                ExceptionGotThrown('L', 1, bag) &&
                ExceptionGotThrown('M', 3, bag) &&
                ExceptionGotThrown('N', 1, bag) &&
                ExceptionGotThrown('O', 1, bag) &&
                ExceptionGotThrown('P', 3, bag) &&
                ExceptionGotThrown('Q', 10, bag) &&
                ExceptionGotThrown('R', 1, bag) &&
                ExceptionGotThrown('S', 1, bag) &&
                ExceptionGotThrown('T', 1, bag) &&
                ExceptionGotThrown('U', 1, bag) &&
                ExceptionGotThrown('V', 4, bag) &&
                ExceptionGotThrown('W', 4, bag) &&
                ExceptionGotThrown('X', 8, bag) &&
                ExceptionGotThrown('Y', 4, bag) &&
                ExceptionGotThrown('Z', 10, bag)
            );

            // Empty the bag again.
            for (int i = 0; i < bag.LetterTileCount;)
            {
                bag.DrawLetterTile();
            }

            // Make sure LetterTiles can be added with the correct point values.
            Assert.IsFalse(ExceptionGotThrown(' ', 0, bag));
            Assert.IsFalse(ExceptionGotThrown('A', 1, bag));
            Assert.IsFalse(ExceptionGotThrown('B', 3, bag));
            Assert.IsFalse(ExceptionGotThrown('C', 3, bag));
            Assert.IsFalse(ExceptionGotThrown('D', 2, bag));
            Assert.IsFalse(ExceptionGotThrown('E', 1, bag));
            Assert.IsFalse(ExceptionGotThrown('F', 4, bag));
            Assert.IsFalse(ExceptionGotThrown('G', 2, bag));
            Assert.IsFalse(ExceptionGotThrown('H', 4, bag));
            Assert.IsFalse(ExceptionGotThrown('I', 1, bag));
            Assert.IsFalse(ExceptionGotThrown('J', 8, bag));
            Assert.IsFalse(ExceptionGotThrown('K', 5, bag));
            Assert.IsFalse(ExceptionGotThrown('L', 1, bag));
            Assert.IsFalse(ExceptionGotThrown('M', 3, bag));
            Assert.IsFalse(ExceptionGotThrown('N', 1, bag));
            Assert.IsFalse(ExceptionGotThrown('O', 1, bag));
            Assert.IsFalse(ExceptionGotThrown('P', 3, bag));
            Assert.IsFalse(ExceptionGotThrown('Q', 10, bag));
            Assert.IsFalse(ExceptionGotThrown('R', 1, bag));
            Assert.IsFalse(ExceptionGotThrown('S', 1, bag));
            Assert.IsFalse(ExceptionGotThrown('T', 1, bag));
            Assert.IsFalse(ExceptionGotThrown('U', 1, bag));
            Assert.IsFalse(ExceptionGotThrown('V', 4, bag));
            Assert.IsFalse(ExceptionGotThrown('W', 4, bag));
            Assert.IsFalse(ExceptionGotThrown('X', 8, bag));
            Assert.IsFalse(ExceptionGotThrown('Y', 4, bag));
            Assert.IsFalse(ExceptionGotThrown('Z', 10, bag));

            // Empty the bag again.
            for (int i = 0; i < bag.LetterTileCount;)
            {
                bag.DrawLetterTile();
            }

            // Make sure LetterTiles with incorrect point values cannot be inserted.
            Assert.IsTrue(ExceptionGotThrown(' ', 0 + 1, bag));
            Assert.IsTrue(ExceptionGotThrown('A', 1 + 1, bag));
            Assert.IsTrue(ExceptionGotThrown('B', 3 + 1, bag));
            Assert.IsTrue(ExceptionGotThrown('C', 3 + 1, bag));
            Assert.IsTrue(ExceptionGotThrown('D', 2 + 1, bag));
            Assert.IsTrue(ExceptionGotThrown('E', 1 + 1, bag));
            Assert.IsTrue(ExceptionGotThrown('F', 4 + 1, bag));
            Assert.IsTrue(ExceptionGotThrown('G', 2 + 1, bag));
            Assert.IsTrue(ExceptionGotThrown('H', 4 + 1, bag));
            Assert.IsTrue(ExceptionGotThrown('I', 1 + 1, bag));
            Assert.IsTrue(ExceptionGotThrown('J', 8 + 1, bag));
            Assert.IsTrue(ExceptionGotThrown('K', 5 + 1, bag));
            Assert.IsTrue(ExceptionGotThrown('L', 1 + 1, bag));
            Assert.IsTrue(ExceptionGotThrown('M', 3 + 1, bag));
            Assert.IsTrue(ExceptionGotThrown('N', 1 + 1, bag));
            Assert.IsTrue(ExceptionGotThrown('O', 1 + 1, bag));
            Assert.IsTrue(ExceptionGotThrown('P', 3 + 1, bag));
            Assert.IsTrue(ExceptionGotThrown('Q', 10 + 1, bag));
            Assert.IsTrue(ExceptionGotThrown('R', 1 + 1, bag));
            Assert.IsTrue(ExceptionGotThrown('S', 1 + 1, bag));
            Assert.IsTrue(ExceptionGotThrown('T', 1 + 1, bag));
            Assert.IsTrue(ExceptionGotThrown('U', 1 + 1, bag));
            Assert.IsTrue(ExceptionGotThrown('V', 4 + 1, bag));
            Assert.IsTrue(ExceptionGotThrown('W', 4 + 1, bag));
            Assert.IsTrue(ExceptionGotThrown('X', 8 + 1, bag));
            Assert.IsTrue(ExceptionGotThrown('Y', 4 + 1, bag));
            Assert.IsTrue(ExceptionGotThrown('Z', 10 + 1, bag));

            // Empty the bag yet again . . .
            for (int i = 0; i < bag.LetterTileCount;)
            {
                bag.DrawLetterTile();
            }
            
            // Just in case . . .
            Assert.IsTrue(bag.LetterTileCount == 0);

            // Make sure that the correct maximum amount of each type of LetterTile can be added to the bag. 
            try
            {
                for (int i = 0; i < 2; ++i)
                {
                    bag.InsertLetterTile(new LetterTile(' ', 0));
                }
                for (int i = 0; i < 9; ++i)
                {
                    bag.InsertLetterTile(new LetterTile('A', 1));
                }
                for (int i = 0; i < 2; ++i)
                {
                    bag.InsertLetterTile(new LetterTile('B', 3));
                }
                for (int i = 0; i < 2; ++i)
                {
                    bag.InsertLetterTile(new LetterTile('C', 3));
                }
                for (int i = 0; i < 4; ++i)
                {
                    bag.InsertLetterTile(new LetterTile('D', 2));
                }
                for (int i = 0; i < 12; ++i)
                {
                    bag.InsertLetterTile(new LetterTile('E', 1));
                }
                for (int i = 0; i < 2; ++i)
                {
                    bag.InsertLetterTile(new LetterTile('F', 4));
                }
                for (int i = 0; i < 2; ++i)
                {
                    bag.InsertLetterTile(new LetterTile('G', 2));
                }
                for (int i = 0; i < 2; ++i)
                {
                    bag.InsertLetterTile(new LetterTile('H', 4));
                }
                for (int i = 0; i < 9; ++i)
                {
                    bag.InsertLetterTile(new LetterTile('I', 1));
                }
                for (int i = 0; i < 1; ++i)
                {
                    bag.InsertLetterTile(new LetterTile('J', 8));
                }
                for (int i = 0; i < 1; ++i)
                {
                    bag.InsertLetterTile(new LetterTile('K', 5));
                }
                for (int i = 0; i < 4; ++i)
                {
                    bag.InsertLetterTile(new LetterTile('L', 1));
                }
                for (int i = 0; i < 2; ++i)
                {
                    bag.InsertLetterTile(new LetterTile('M', 3));
                }
                for (int i = 0; i < 6; ++i)
                {
                    bag.InsertLetterTile(new LetterTile('N', 1));
                }
                for (int i = 0; i < 8; ++i)
                {
                    bag.InsertLetterTile(new LetterTile('O', 1));
                }
                for (int i = 0; i < 2; ++i)
                {
                    bag.InsertLetterTile(new LetterTile('P', 3));
                }
                for (int i = 0; i < 1; ++i)
                {
                    bag.InsertLetterTile(new LetterTile('Q', 10));
                }
                for (int i = 0; i < 6; ++i)
                {
                    bag.InsertLetterTile(new LetterTile('R', 1));
                }
                for (int i = 0; i < 4; ++i)
                {
                    bag.InsertLetterTile(new LetterTile('S', 1));
                }
                for (int i = 0; i < 6; ++i)
                {
                    bag.InsertLetterTile(new LetterTile('T', 1));
                }
                for (int i = 0; i < 4; ++i)
                {
                    bag.InsertLetterTile(new LetterTile('U', 1));
                }
                for (int i = 0; i < 2; ++i)
                {
                    bag.InsertLetterTile(new LetterTile('V', 4));
                }
                for (int i = 0; i < 2; ++i)
                {
                    bag.InsertLetterTile(new LetterTile('W', 4));
                }
                for (int i = 0; i < 1; ++i)
                {
                    bag.InsertLetterTile(new LetterTile('X', 8));
                }
                for (int i = 0; i < 2; ++i)
                {
                    bag.InsertLetterTile(new LetterTile('Y', 4));
                }
                for (int i = 0; i < 1; ++i)
                {
                    bag.InsertLetterTile(new LetterTile('Z', 10));
                }
            }
            catch
            {
                Assert.Fail();
            }


            // Now make sure that adding more than that amount throws an exception.
            bool exceptionWasThrown = false;

            try
            { 
                for (int i = 0; i < 2+1; ++i)
                {
                    bag.InsertLetterTile(new LetterTile(' ', 0));
                }
            }
            catch
            {
                exceptionWasThrown = true;
            }
            Assert.IsTrue(exceptionWasThrown);
            exceptionWasThrown = false;

            try {
                for (int i = 0; i < 9 + 1; ++i)
                {
                    bag.InsertLetterTile(new LetterTile('A', 1));
                }
            }
            catch
            {
                exceptionWasThrown = true;
            }
            Assert.IsTrue(exceptionWasThrown);
            exceptionWasThrown = false;

            try
            {
                for (int i = 0; i < 2 + 1; ++i)
                {
                    bag.InsertLetterTile(new LetterTile('B', 3));
                }
            }
            catch
            {
                exceptionWasThrown = true;
            }
            Assert.IsTrue(exceptionWasThrown);
            exceptionWasThrown = false;

            try {
                for (int i = 0; i < 2 + 1; ++i)
                {
                    bag.InsertLetterTile(new LetterTile('C', 3));
                }
            }
            catch
            {
                exceptionWasThrown = true;
            }
            Assert.IsTrue(exceptionWasThrown);
            exceptionWasThrown = false;

            try {
                for (int i = 0; i < 4 + 1; ++i)
                {
                    bag.InsertLetterTile(new LetterTile('D', 2));
                }
            }
            catch
            {
                exceptionWasThrown = true;
            }
            Assert.IsTrue(exceptionWasThrown);
            exceptionWasThrown = false;

            try {
                for (int i = 0; i < 12 + 1; ++i)
                {
                    bag.InsertLetterTile(new LetterTile('E', 1));
                }
            }
            catch
            {
                exceptionWasThrown = true;
            }
            Assert.IsTrue(exceptionWasThrown);
            exceptionWasThrown = false;

            try
            {
                for (int i = 0; i < 2 + 1; ++i)
                {
                    bag.InsertLetterTile(new LetterTile('F', 4));
                }
            }
            catch
            {
                exceptionWasThrown = true;
            }
            Assert.IsTrue(exceptionWasThrown);
            exceptionWasThrown = false;

            try {
                for (int i = 0; i < 2 + 1; ++i)
                {
                    bag.InsertLetterTile(new LetterTile('G', 2));
                }
            }
            catch
            {
                exceptionWasThrown = true;
            }
            Assert.IsTrue(exceptionWasThrown);
            exceptionWasThrown = false;

            try
            {
                for (int i = 0; i < 2 + 1; ++i)
                {
                    bag.InsertLetterTile(new LetterTile('H', 4));
                }
            }
            catch
            {
                exceptionWasThrown = true;
            }
            Assert.IsTrue(exceptionWasThrown);
            exceptionWasThrown = false;

            try
            {
                for (int i = 0; i < 9 + 1; ++i)
                {
                    bag.InsertLetterTile(new LetterTile('I', 1));
                }
            }
            catch
            {
                exceptionWasThrown = true;
            }
            Assert.IsTrue(exceptionWasThrown);
            exceptionWasThrown = false;

            try
            {
                for (int i = 0; i < 1 + 1; ++i)
                {
                    bag.InsertLetterTile(new LetterTile('J', 8));
                }
            }
            catch
            {
                exceptionWasThrown = true;
            }
            Assert.IsTrue(exceptionWasThrown);
            exceptionWasThrown = false;

            try
            {
                for (int i = 0; i < 1 + 1; ++i)
                {
                    bag.InsertLetterTile(new LetterTile('K', 5));
                }
            }
            catch
            {
                exceptionWasThrown = true;
            }
            Assert.IsTrue(exceptionWasThrown);
            exceptionWasThrown = false;

            try
            {
                for (int i = 0; i < 4 + 1; ++i)
                {
                    bag.InsertLetterTile(new LetterTile('L', 1));
                }
            }
            catch
            {
                exceptionWasThrown = true;
            }
            Assert.IsTrue(exceptionWasThrown);
            exceptionWasThrown = false;

            try
            {
                for (int i = 0; i < 2 + 1; ++i)
                {
                    bag.InsertLetterTile(new LetterTile('M', 3));
                }
            }
            catch
            {
                exceptionWasThrown = true;
            }
            Assert.IsTrue(exceptionWasThrown);
            exceptionWasThrown = false;

            try
            {
                for (int i = 0; i < 6 + 1; ++i)
                {
                    bag.InsertLetterTile(new LetterTile('N', 1));
                }
            }
            catch
            {
                exceptionWasThrown = true;
            }
            Assert.IsTrue(exceptionWasThrown);
            exceptionWasThrown = false;



            try
            {
                for (int i = 0; i < 8 + 1; ++i)
                {
                    bag.InsertLetterTile(new LetterTile('O', 1));
                }
            }
            catch
            {
                exceptionWasThrown = true;
            }
            Assert.IsTrue(exceptionWasThrown);
            exceptionWasThrown = false;

            try
            {
                for (int i = 0; i < 2 + 1; ++i)
                {
                    bag.InsertLetterTile(new LetterTile('P', 3));
                }
            }
            catch
            {
                exceptionWasThrown = true;
            }
            Assert.IsTrue(exceptionWasThrown);
            exceptionWasThrown = false;

            try
            {
                for (int i = 0; i < 1 + 1; ++i)
                {
                    bag.InsertLetterTile(new LetterTile('Q', 10));
                }
            }
            catch
            {
                exceptionWasThrown = true;
            }
            Assert.IsTrue(exceptionWasThrown);
            exceptionWasThrown = false;

            try
            {
                for (int i = 0; i < 6 + 1; ++i)
                {
                    bag.InsertLetterTile(new LetterTile('R', 1));
                }
            }
            catch
            {
                exceptionWasThrown = true;
            }
            Assert.IsTrue(exceptionWasThrown);
            exceptionWasThrown = false;

            try
            {
                for (int i = 0; i < 4 + 1; ++i)
                {
                    bag.InsertLetterTile(new LetterTile('S', 1));
                }
            }
            catch
            {
                exceptionWasThrown = true;
            }
            Assert.IsTrue(exceptionWasThrown);
            exceptionWasThrown = false;

            try
            {
                for (int i = 0; i < 6 + 1; ++i)
                {
                    bag.InsertLetterTile(new LetterTile('T', 1));
                }
            }
            catch
            {
                exceptionWasThrown = true;
            }
            Assert.IsTrue(exceptionWasThrown);
            exceptionWasThrown = false;

            try
            {
                for (int i = 0; i < 4 + 1; ++i)
                {
                    bag.InsertLetterTile(new LetterTile('U', 1));
                }
            }
            catch
            {
                exceptionWasThrown = true;
            }
            Assert.IsTrue(exceptionWasThrown);
            exceptionWasThrown = false;

            try
            {
                for (int i = 0; i < 2 + 1; ++i)
                {
                    bag.InsertLetterTile(new LetterTile('V', 4));
                }
            }
            catch
            {
                exceptionWasThrown = true;
            }
            Assert.IsTrue(exceptionWasThrown);
            exceptionWasThrown = false;

            try
            {
                for (int i = 0; i < 2 + 1; ++i)
                {
                    bag.InsertLetterTile(new LetterTile('W', 4));
                }
            }
            catch
            {
                exceptionWasThrown = true;
            }
            Assert.IsTrue(exceptionWasThrown);
            exceptionWasThrown = false;

            try
            {
                for (int i = 0; i < 1 + 1; ++i)
                {
                    bag.InsertLetterTile(new LetterTile('X', 8));
                }
            }
            catch
            {
                exceptionWasThrown = true;
            }
            Assert.IsTrue(exceptionWasThrown);
            exceptionWasThrown = false;

            try
            {
                for (int i = 0; i < 2 + 1; ++i)
                {
                    bag.InsertLetterTile(new LetterTile('Y', 4));
                }
            }
            catch
            {
                exceptionWasThrown = true;
            }
            Assert.IsTrue(exceptionWasThrown);
            exceptionWasThrown = false;

            try
            {
                for (int i = 0; i < 1 + 1; ++i)
                {
                    bag.InsertLetterTile(new LetterTile('Z', 10));
                }
            }
            catch
            {
                exceptionWasThrown = true;
            }
            Assert.IsTrue(exceptionWasThrown);
            exceptionWasThrown = false;

            // That was a disgusting amount of copy/paste . . .
        }

        public bool ExceptionGotThrown(char letterValue, int pointValue, Bag bag)
        {
            bool exceptionWasThrown = false;
            try
            {
                bag.InsertLetterTile(new LetterTile(letterValue, pointValue));
            }
            catch
            {
                exceptionWasThrown = true;
            }
            return exceptionWasThrown;
        }
    }
}