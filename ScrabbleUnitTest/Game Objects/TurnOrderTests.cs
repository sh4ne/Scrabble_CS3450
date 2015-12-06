using Microsoft.VisualStudio.TestTools.UnitTesting;
using Scrabble.Game_Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Scrabble.PlayerClass;

namespace Scrabble.Game_Objects.Tests
{
    [TestClass()]
    public class TurnOrderTests
    {
        [TestMethod()]
        public void TurnOrderTest()
        {
            Player p1 = new Player(1, "p1");
            Player p2 = new Player(2, "p2");
            Player p3 = new Player(3, "p3");
            Player p4 = new Player(4, "p4");
            Player p5 = new Player(5, "p5");

            List<Player> players = new List<Player>();
            bool exceptionWasThrown = false;

            // Make sure there must be at least 2 players.
            try
            {
                TurnOrder oops = new TurnOrder(players);
            }
            catch
            {
                exceptionWasThrown = true;
            }
            Assert.IsTrue(exceptionWasThrown);
            exceptionWasThrown = false;

            players.Add(p1);

            try
            {
                TurnOrder oops = new TurnOrder(players);
            }
            catch
            {
                exceptionWasThrown = true;
            }
            Assert.IsTrue(exceptionWasThrown);
            exceptionWasThrown = false;

            players.Add(p2);

            try
            {
                TurnOrder notOops = new TurnOrder(players);
            }
            catch
            {
                Assert.Fail();
            }

            // Make sure that 4 players can be in a turn queue.
            players.Add(p3);
            players.Add(p4);

            try
            {
                TurnOrder notOops = new TurnOrder(players);
            }
            catch
            {
                Assert.Fail();
            }

            // Make sure that 5 players cannot be in a turn queue.
            players.Add(p5);
            try
            {
                TurnOrder oops = new TurnOrder(players);
            }
            catch
            {
                exceptionWasThrown = true;
            }
            Assert.IsTrue(exceptionWasThrown);
        }

        [TestMethod()]
        public void MoveToNextPlayerTest()
        {
            List<Player> players = new List<Player>();
            Player p1 = new Player(1, "p1");
            Player p2 = new Player(2, "p2");
            Player p3 = new Player(3, "p3");
            Player p4 = new Player(4, "p4");

            players.Add(p1);
            players.Add(p2);
            players.Add(p3);
            players.Add(p4);

            TurnOrder turnOrder = new TurnOrder(players);

            Assert.IsTrue(turnOrder.ActivePlayerIndex == 0);
            turnOrder.MoveToNextPlayer();
            Assert.IsTrue(turnOrder.ActivePlayerIndex == 1);
            turnOrder.MoveToNextPlayer();
            Assert.IsTrue(turnOrder.ActivePlayerIndex == 2);
            turnOrder.MoveToNextPlayer();
            Assert.IsTrue(turnOrder.ActivePlayerIndex == 3);
            turnOrder.MoveToNextPlayer();
            Assert.IsTrue(turnOrder.ActivePlayerIndex == 0);

            p1.IncrementSkipCount();
            p2.IncrementSkipCount();
            p3.IncrementSkipCount();

            turnOrder.MoveToNextPlayer();
            Assert.IsTrue(p4.HasTurnPriority);

            turnOrder.MoveToNextPlayer();
            Assert.IsTrue(p1.SkipCount == 0 || p2.SkipCount == 0 || p3.SkipCount == 0);
        }

        [TestMethod()]
        public void IndexOfTest()
        {
            List<Player> players = new List<Player>();
            Player p1 = new Player(1, "p1");
            Player p2 = new Player(2, "p2");
            Player p3 = new Player(3, "p3");
            Player p4 = new Player(4, "p4");

            players.Add(p1);
            players.Add(p2);
            players.Add(p3);
            players.Add(p4);

            TurnOrder turnOrder = new TurnOrder(players);

            Assert.IsTrue(turnOrder.IndexOf(p1) != -1);
            Assert.IsTrue(turnOrder.IndexOf(p2) != -1);
            Assert.IsTrue(turnOrder.IndexOf(p3) != -1);
            Assert.IsTrue(turnOrder.IndexOf(p4) != -1);

            Player badPlayer = new Player(42, "I'm bad, I'm bad, I'm really, really bad!");
            Assert.IsTrue(turnOrder.IndexOf(badPlayer) == -1);
        }

        [TestMethod()]
        public void GetPlayersTest()
        {
            List<Player> players = new List<Player>();
            Player p1 = new Player(1, "p1");
            Player p2 = new Player(2, "p2");
            Player p3 = new Player(3, "p3");
            Player p4 = new Player(4, "p4");

            players.Add(p1);
            players.Add(p2);
            players.Add(p3);
            players.Add(p4);

            TurnOrder turnOrder = new TurnOrder(players);

            Assert.IsTrue(turnOrder.Players == players);
        }

        [TestMethod()]
        public void TestExceptions()
        {
            TurnOrder.InvalidTurnQueueSizeException itqse = new TurnOrder.InvalidTurnQueueSizeException();
            Assert.IsTrue(itqse.ToString() == "InvalidQueueSize: An invalid number of players were added to a TurnOrder object.");
        }
    }
}