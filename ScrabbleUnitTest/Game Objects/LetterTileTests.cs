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
    public class LetterTileTests
    {
        [TestMethod()]
        public void LetterTileTest()
        {
            bool exceptionWasThrown = false;

            // Test that the null LetterTile can be created.
            LetterTile lt = new LetterTile('n', 0);
            Assert.IsTrue(lt.LetterValue == 'n' && lt.PointValue == 0);

            // Test that a valid LetterTile can be created
            lt = new LetterTile('A', 1);
            Assert.IsTrue(lt.LetterValue == 'A' && lt.PointValue == 1);

            // Test that creating a LetterTile with an invalid LetterValue throws an exception.
            try
            {
                lt = new LetterTile('f', 2);
            }
            catch (LetterTile.InvalidLetterValueException)
            {
                exceptionWasThrown = true;
            }

            Assert.IsTrue(exceptionWasThrown);
            exceptionWasThrown = false;

            // Test that creating a LetterTile with a negative PointValue throws an exception.
            try
            {
                lt = new LetterTile('A', -1);
            }
            catch (LetterTile.InvalidPointValueException)
            {
                exceptionWasThrown = true;
            }

            Assert.IsTrue(exceptionWasThrown);
            exceptionWasThrown = false;

            // Test that creating a LetterTile with a PointValue greater than 10 throws an exception.
            try
            {
                lt = new LetterTile('A', 11);
            }
            catch (LetterTile.InvalidPointValueException)
            {
                exceptionWasThrown = true;
            }

            Assert.IsTrue(exceptionWasThrown);
        }

        [TestMethod()]
        public void IsNullLetterTileTest()
        {
            LetterTile lt = new LetterTile('n', 0);
            Assert.IsTrue(lt.IsNullLetterTile());

            lt = new LetterTile('A', 1);
            Assert.IsFalse(lt.IsNullLetterTile());
        }

        [TestMethod()]
        public void EqualsTest()
        {
            LetterTile lt1 = new LetterTile('A', 1);
            LetterTile lt2 = new LetterTile('A', 1);

            Assert.IsTrue(lt1.Equals(lt2));

            lt2 = new LetterTile('E', 1);
            Assert.IsFalse(lt1.Equals(lt2));

            lt2 = new LetterTile('A', 2);
            Assert.IsFalse(lt1.Equals(lt2));
        }
    }
}
