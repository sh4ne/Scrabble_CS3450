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
    public class LetterTileRackTests
    {
        [TestMethod()]
        public void LetterTileRackTest()
        {
            bool exceptionWasThrown = false;
            try
            {
                LetterTileRack ltr = new LetterTileRack();
            }
            catch (Exception)
            {
                exceptionWasThrown = true;
            }
            Assert.IsFalse(exceptionWasThrown);
        }

        [TestMethod()]
        public void InsertLetterTileTest()
        {
            bool exceptionWasThrown = false;
            LetterTile lt = new LetterTile('A', 1);
            LetterTileRack ltr = new LetterTileRack();

            try
            {
                for (int i = 0; i < 7; ++i)
                {
                    ltr.InsertLetterTile(lt);
                }
            }
            catch (LetterTileRack.InvalidLetterTileInsertionException)
            {
                exceptionWasThrown = true;
            }
            Assert.IsFalse(exceptionWasThrown);

            try
            {
                ltr.InsertLetterTile(lt);
            }
            catch (Exception)
            {
                exceptionWasThrown = true;
            }
            Assert.IsTrue(exceptionWasThrown);
        }

        [TestMethod()]
        public void PopLetterTileTest()
        {
            LetterTileRack ltr = new LetterTileRack();

            LetterTile lt = new LetterTile('A', 1);

            ltr.InsertLetterTile(lt);
            ltr.InsertLetterTile(lt);
            ltr.InsertLetterTile(lt);

            Assert.IsTrue(ltr.LetterTileCount() == 3);

            LetterTile lt2 = ltr.PopLetterTile(2);
            Assert.IsTrue(ltr.LetterTileCount() == 2);
            Assert.IsTrue(lt2.Equals(lt));

            bool exceptionWasThrown = false;
            try
            {
                ltr.PopLetterTile(-1);
            } catch(LetterTileRack.InvalidLetterTileAccessException)
            {
                exceptionWasThrown = true;
            }

            Assert.IsTrue(exceptionWasThrown);
            exceptionWasThrown = false;

            try
            {
                ltr.PopLetterTile(2);
            }
            catch (LetterTileRack.InvalidLetterTileAccessException)
            {
                exceptionWasThrown = true;
            }

            Assert.IsTrue(exceptionWasThrown);
            exceptionWasThrown = false;
        }

        [TestMethod()]
        public void LetterTileCountTest()
        {
            LetterTileRack ltr = new LetterTileRack();
            Assert.IsTrue(ltr.LetterTileCount() == 0);

            LetterTile lt = new LetterTile('A', 1);

            ltr.InsertLetterTile(lt);
            Assert.IsTrue(ltr.LetterTileCount() == 1);

            ltr.InsertLetterTile(lt);
            ltr.InsertLetterTile(lt);
            ltr.InsertLetterTile(lt);
            ltr.InsertLetterTile(lt);
            ltr.InsertLetterTile(lt);
            ltr.InsertLetterTile(lt);
            Assert.IsTrue(ltr.LetterTileCount() == 7);

            ltr.PopLetterTile(5);
            Assert.IsTrue(ltr.LetterTileCount() == 6);
        }

        [TestMethod()]
        public void GetAllLetterTilesTest()
        {
            LetterTileRack ltr = new LetterTileRack();
            LetterTile lt = new LetterTile('A', 1);

            ltr.InsertLetterTile(lt);
            ltr.InsertLetterTile(lt);
            ltr.InsertLetterTile(lt);

            List<LetterTile> lst = new List<LetterTile>();
            lst = ltr.GetAllLetterTiles();

            Assert.IsTrue(lst.Count == 3);
            Assert.IsTrue(lst[1].Equals(lt));
        }

        [TestMethod()]
        public void SquareBracketOperatorTest()
        {
            LetterTileRack ltr = new LetterTileRack();
            LetterTile lt = new LetterTile('A', 1);

            ltr.InsertLetterTile(lt);
            ltr.InsertLetterTile(lt);
            ltr.InsertLetterTile(lt);
            ltr.InsertLetterTile(lt);

            Assert.IsTrue(ltr[3].Equals(lt));

            bool exceptionWasThrown = false;
            try
            {
                char ch = ltr[4].LetterValue;
            }
            catch(LetterTileRack.InvalidLetterTileAccessException)
            {
                exceptionWasThrown = true;
            }

            Assert.IsTrue(exceptionWasThrown);
            exceptionWasThrown = false;

            try
            {
                char ch = ltr[-1].LetterValue;
            }
            catch (LetterTileRack.InvalidLetterTileAccessException)
            {
                exceptionWasThrown = true;
            }

            Assert.IsTrue(exceptionWasThrown);
            exceptionWasThrown = false;

        }
    }
}
