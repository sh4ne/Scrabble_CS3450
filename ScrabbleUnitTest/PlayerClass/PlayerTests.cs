using Scrabble.PlayerClass;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Scrabble.PlayerClass.Tests
{
    [TestClass()]
    public class PlayerTests
    {
        [TestMethod()]
        public void PlayerTest()
        {
            Player player = new Player(1000, "Blob");
            Assert.IsTrue(player.PlayerID == 1000);
            Assert.IsTrue(player.Username == "Blob");
            Assert.IsTrue(player.Score == 0);
            Assert.IsFalse(player.HasTurnPriority);
            Assert.IsTrue(player.Score == 0);

            Player player2 = new Player(9001, "Bill");
            Assert.IsTrue(player2.PlayerID == 9001);
            Assert.IsTrue(player2.Username == "Bill");
            Assert.IsTrue(player2.Score == 0);
            Assert.IsFalse(player2.HasTurnPriority);
            Assert.IsTrue(player2.Score == 0);
        }

        [TestMethod()]
        public void AddToScoreTest()
        {
            Player player = new Player(1000, "Blob");
            Assert.IsTrue(player.Score == 0);

            player.AddToScore(1000);
            Assert.IsTrue(player.Score == 1000);

            player.AddToScore(-250);
            Assert.IsTrue(player.Score == 750);

            player.AddToScore(-9001);
            Assert.IsTrue(player.Score == 0);
        }

        [TestMethod()]
        public void IncrementSkipCountTest()
        {
            Player player = new Player(1000, "Blob");
            Assert.IsTrue(player.SkipCount == 0);

            player.IncrementSkipCount();
            Assert.IsTrue(player.SkipCount == 1);

            player.IncrementSkipCount();
            Assert.IsTrue(player.SkipCount == 2);

            player.IncrementSkipCount();
            Assert.IsTrue(player.SkipCount == 3);
        }

        [TestMethod()]
        public void DecrementSkipCountTest()
        {
            Player player = new Player(1000, "Blob");
            Assert.IsTrue(player.SkipCount == 0);

            player.IncrementSkipCount();
            player.IncrementSkipCount();
            player.IncrementSkipCount();

            Assert.IsTrue(player.SkipCount == 3);

            player.DecrementSkipCount();
            Assert.IsTrue(player.SkipCount == 2);

            player.DecrementSkipCount();
            Assert.IsTrue(player.SkipCount == 1);

            player.DecrementSkipCount();
            Assert.IsTrue(player.SkipCount == 0);

            player.DecrementSkipCount();
            Assert.IsTrue(player.SkipCount == 0);
        }

        [TestMethod()]
        public void DrawLetterTileTest()
        {
            bool exceptionWasThrown = false;

            Player player = new Player(1000, "Blob");
            Assert.IsTrue(player.TileRack.LetterTileCount() == 0);

            try
            {
                player.DrawLetterTile(new Game_Objects.LetterTile('A', 1));
                player.DrawLetterTile(new Game_Objects.LetterTile('A', 1));
                player.DrawLetterTile(new Game_Objects.LetterTile('A', 1));
                player.DrawLetterTile(new Game_Objects.LetterTile('A', 1));
                player.DrawLetterTile(new Game_Objects.LetterTile('A', 1));
                player.DrawLetterTile(new Game_Objects.LetterTile('A', 1));
                player.DrawLetterTile(new Game_Objects.LetterTile('A', 1));
            }
            catch
            {
                exceptionWasThrown = true;
            }
            Assert.IsFalse(exceptionWasThrown);
            exceptionWasThrown = false;
            Assert.IsTrue(player.TileRack.LetterTileCount() == 7);
            try
            {
                player.DrawLetterTile(new Game_Objects.LetterTile('A', 1));
            }
            catch
            {
                exceptionWasThrown = true;
            }
            Assert.IsTrue(exceptionWasThrown);
        }

        [TestMethod()]
        public void PopLetterTileTest()
        {
            bool exceptionWasThrown = false;

            Player player = new Player(1000, "Blob");
            Assert.IsTrue(player.TileRack.LetterTileCount() == 0);

            try
            {
                player.DrawLetterTile(new Game_Objects.LetterTile('A', 1));
                player.DrawLetterTile(new Game_Objects.LetterTile('A', 1));
                player.DrawLetterTile(new Game_Objects.LetterTile('A', 1));
                player.DrawLetterTile(new Game_Objects.LetterTile('A', 1));
                player.DrawLetterTile(new Game_Objects.LetterTile('A', 1));
                player.DrawLetterTile(new Game_Objects.LetterTile('A', 1));
                player.DrawLetterTile(new Game_Objects.LetterTile('A', 1));
            }
            catch
            {
                exceptionWasThrown = true;
            }
            Assert.IsFalse(exceptionWasThrown);
            exceptionWasThrown = false;
            Assert.IsTrue(player.TileRack.LetterTileCount() == 7);
            try
            {
                player.DrawLetterTile(new Game_Objects.LetterTile('A', 1));
            }
            catch
            {
                exceptionWasThrown = true;
            }
            Assert.IsTrue(exceptionWasThrown);

            exceptionWasThrown = false;

            Game_Objects.LetterTile lt = new Game_Objects.LetterTile('E', 1);
            try
            {
                lt = player.PopLetterTile(6);
            }
            catch
            {
                exceptionWasThrown = true;
            }

            Assert.IsFalse(exceptionWasThrown);
            Assert.IsTrue(lt.LetterValue == 'A');
            Assert.IsTrue(player.TileRack.LetterTileCount() == 6);

            try
            {
                player.PopLetterTile(5);
                player.PopLetterTile(4);
                player.PopLetterTile(3);
                player.PopLetterTile(2);
                player.PopLetterTile(1);
                player.PopLetterTile(0);
            }
            catch
            {
                exceptionWasThrown = true;
            }
            Assert.IsFalse(exceptionWasThrown);

            try
            {
                player.PopLetterTile(5);
            }
            catch
            {
                exceptionWasThrown = true;
            }
            Assert.IsTrue(exceptionWasThrown);
        }

        [TestMethod()]
        public void TurnPriorityTest()
        {
            Player p = new Player(1, "Blob");
            p.HasTurnPriority = true;
            Assert.IsTrue(p.HasTurnPriority);
            p.HasTurnPriority = false;
            Assert.IsFalse(p.HasTurnPriority);

        }
    }
}
