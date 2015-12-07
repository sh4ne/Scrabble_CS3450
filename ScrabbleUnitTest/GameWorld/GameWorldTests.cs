using Microsoft.VisualStudio.TestTools.UnitTesting;
using Scrabble.Game_Objects;
using Scrabble.GameWorld;
using Scrabble.PlayerClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scrabble.GameWorld.Tests
{
    [TestClass()]
    public class GameWorldTests
    {
        [TestMethod()]
        public void GameWorldTest()
        {
            List<Player> players = new List<Player>()
            {
                new Player(167, "Shane"),
                new Player(254, "Josh"),
                new Player(689, "Peter"),
                new Player(255, "Mike")
            };
            try
            {
                GameWorld gw = new GameWorld(players);
            }
            catch
            {
                Assert.Fail();
            }
            
        }

        [TestMethod()]
        public void MakePlayerTakeTurnTest()
        {
            GameWorld gw = null;
            Play play;
            Player activePlayer = null;
            List<Player> players = new List<Player>()
            {
                new Player(167, "Shane"),
                new Player(254, "Josh"),
                new Player(689, "Peter"),
                new Player(255, "Mike")
            };
            try
            {
                gw = new GameWorld(players);
            }
            catch
            {
                Assert.Fail();
            }

            try
            {
                int count = 0;
                while(true)
                {
                    if (players[count].PlayerID == gw.GetActivePlayerID())
                    {
                        activePlayer = players[count];
                        break;
                    }
                    ++count;
                }
            }
            catch
            {
                Assert.Fail();
            }

            try
            {
                List<LetterTile> tiles = new List<LetterTile>();
                for (int i = 0; (i < 5 && i < activePlayer.TileRack.LetterTileCount()); ++i)
                {
                    tiles.Add(activePlayer.TileRack[i]);
                }

                List<int> coordsX = new List<int>() { 4, 5, 6, 7, 8 };
                List<int> coordsY = new List<int>() { 7, 7, 7, 7, 7 };
                play = new Play(tiles, coordsX, coordsY, activePlayer.PlayerID);
                gw.MakePlayerTakeTurn(play);
            }
            catch
            {
                Assert.Fail();
            }

            bool exceptionWasThrown = false;
            try
            {
                List<LetterTile> tiles = new List<LetterTile>();
                for (int i = 0; (i < 2 && i < activePlayer.TileRack.LetterTileCount()); ++i)
                {
                    tiles.Add(activePlayer.TileRack[i]);
                }

                List<int> coordsX = new List<int>() { 4, 5 };
                List<int> coordsY = new List<int>() { 8, 8 };
                play = new Play(tiles, coordsX, coordsY, activePlayer.PlayerID);
                gw.MakePlayerTakeTurn(play);
            }
            catch
            {
                exceptionWasThrown = true;
            }
            Assert.IsTrue(exceptionWasThrown);
            exceptionWasThrown = false;

            Player oldActivePlayer = activePlayer;
            int oldActivePlayerScore = oldActivePlayer.Score;
            try
            {
                int count = 0;
                while (true)
                {
                    if (players[count].PlayerID == gw.GetActivePlayerID())
                    {
                        activePlayer = players[count];
                        break;
                    }
                    ++count;
                }
            }
            catch
            {
                Assert.Fail();
            }

            try
            {
                List<LetterTile> tiles = new List<LetterTile>();
                for (int i = 0; (i < 5 && i < activePlayer.TileRack.LetterTileCount()); ++i)
                {
                    tiles.Add(activePlayer.TileRack[i]);
                }

                List<int> coordsX = new List<int>() { 4, 5, 6, 7, 8 };
                List<int> coordsY = new List<int>() { 8, 8, 8, 8, 8 };
                play = new Play(tiles, coordsX, coordsY, activePlayer.PlayerID);
                gw.MakePlayerTakeTurn(play);
            }
            catch
            {
                Assert.Fail();
            }

            Assert.IsTrue(oldActivePlayer.Score > oldActivePlayerScore);
            Assert.IsTrue(oldActivePlayer.TileRack.LetterTileCount() == 7);
        }

        [TestMethod()]
        public void ChallengeLastPlayTest()
        {
            Assert.Fail();
        }
    }
}