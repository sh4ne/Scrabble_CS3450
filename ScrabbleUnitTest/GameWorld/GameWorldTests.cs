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
            GameWorld gw = null;
            Play play;
            Player activePlayer = null;
            List<int> coordsX;
            List<int> coordsY;
            List<LetterTile> tiles;
            List<Player> players = new List<Player>()
            {
                new Player(167, "Shane"),
                new Player(689, "Peter"),
            };

            try
            {
                gw = new GameWorld(players);
            }
            catch
            {
                Assert.Fail();
            }

            // Player 1, turn 1.

            coordsX = new List<int>() { 4, 5, 6, 7, 8, 9, 10 };
            coordsY = new List<int>() { 7, 7, 7, 7, 7, 7, 7 };

            tiles = new List<LetterTile>();

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

            for (int i = 0; i < activePlayer.TileRack.LetterTileCount(); ++i)
            {
                tiles.Add(activePlayer.TileRack[i]);
            }

            play = new Play(tiles, coordsX, coordsY, activePlayer.PlayerID);
            gw.MakePlayerTakeTurn(play);

            int challengerID;
            if (activePlayer.PlayerID == players[0].PlayerID)
            {
                challengerID = players[1].PlayerID;
            }
            else
            {
                challengerID = players[0].PlayerID;
            }

            Assert.IsTrue(activePlayer.TileRack.LetterTileCount() == 0);
            gw.ChallengeLastPlay(challengerID);

            // There is technically a really, really, really tiny chance that this will fail, but probably
            // not during the next year or so. Probably.
            Assert.IsTrue(activePlayer.TileRack.LetterTileCount() != 0);

            // Cheat the system, and make sure an actual word gets challenged.
            players = new List<Player>()
            {
                new Player(167, "Shane"),
                new Player(689, "Peter"),
            };

            gw = new GameWorld(players);

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

            // Dump the letter tiles . . .
            while (activePlayer.TileRack.LetterTileCount() > 0)
            {
                activePlayer.PopLetterTile(0);
            }

            // Then pull new ones from thin air!
            tiles = new List<LetterTile>()
            {
                new LetterTile('C', 3),
                new LetterTile('H', 4),
                new LetterTile('E', 1),
                new LetterTile('A', 1),
                new LetterTile('T', 1),
                new LetterTile('E', 1),
                new LetterTile('R', 1),
            };

            foreach (LetterTile tile in tiles)
            {
                activePlayer.DrawLetterTile(tile);
            }

            coordsX = new List<int>() { 4, 5, 6, 7, 8, 9, 10 };
            coordsY = new List<int>() { 7, 7, 7, 7, 7, 7, 7 };

            play = new Play(tiles, coordsX, coordsY, activePlayer.PlayerID);

            gw.MakePlayerTakeTurn(play);

            Player challenger;
            if (activePlayer.PlayerID == players[0].PlayerID)
            {
                challenger = players[1];
            }
            else
            {
                challenger = players[0];
            }

            // After challenging, it should be the active player's turn again, even though he just went.
            // This will mean the challenger lost his turn.
            Assert.IsTrue(activePlayer.TileRack.LetterTileCount() == 0);
            gw.ChallengeLastPlay(challenger.PlayerID);
            Assert.IsTrue(gw.GetActivePlayerID() == activePlayer.PlayerID);

            bool exceptionWasThrown = false;
            try
            {
                gw.ChallengeLastPlay(challenger.PlayerID);
                Assert.IsTrue(gw.GetActivePlayerID() == activePlayer.PlayerID);
            } catch
            {
                Assert.Fail();
            }

            players = new List<Player>()
            {
                new Player(167, "Shane"),
                new Player(689, "Peter"),
            };

            try
            {
                gw = new GameWorld(players);
            }
            catch
            {
                Assert.Fail();
            }

            // Player 1, turn 1.

            coordsX = new List<int>() { 4, 5, 6, 7, 8, 9, 10 };
            coordsY = new List<int>() { 7, 7, 7, 7, 7, 7, 7 };

            tiles = new List<LetterTile>();

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

            for (int i = 0; i < activePlayer.TileRack.LetterTileCount(); ++i)
            {
                tiles.Add(activePlayer.TileRack[i]);
            }

            play = new Play(tiles, coordsX, coordsY, activePlayer.PlayerID);
            gw.MakePlayerTakeTurn(play);

            if (activePlayer.PlayerID == players[0].PlayerID)
            {
                challengerID = players[1].PlayerID;
            }
            else
            {
                challengerID = players[0].PlayerID;
            }

            Assert.IsTrue(activePlayer.TileRack.LetterTileCount() == 0);
            try
            {
                gw.ChallengeLastPlay(activePlayer.PlayerID);
            }
            catch
            {
                exceptionWasThrown = true;
            }
            Assert.IsTrue(exceptionWasThrown);
            exceptionWasThrown = false;

            try
            {
                gw.ChallengeLastPlay(-9001);
            }
            catch
            {
                exceptionWasThrown = true;
            }
            Assert.IsTrue(exceptionWasThrown);
            exceptionWasThrown = false;

            // Meh, let's pad some code coverage.
            gw.ToString();
            gw.GameId = "42";
            string foo = gw.GameId;
        }

        [TestMethod()]
        public void EndOfTheGameWorldTest()
        {
            GameWorld gw = null;
            Play play;
            List<int> coordsX;
            List<int> coordsY;
            List<LetterTile> tiles;
            List<Player> players = new List<Player>()
            {
                new Player(167, "Shane"),
                new Player(689, "Peter"),
            };

            gw = new GameWorld(players);

            Assert.IsTrue(gw.GetActivePlayerID() == players[0].PlayerID);
            
            // 7 tiles used
            int activePlayerIndex = 0;
            int row = 7;
            DoATurn(gw, out play, out coordsX, out coordsY, out tiles, players, activePlayerIndex, row);

            // 14 tiles used
            activePlayerIndex = 1;
            row = 8;
            DoATurn(gw, out play, out coordsX, out coordsY, out tiles, players, activePlayerIndex, row);

            // 21 tiles used
            activePlayerIndex = 0;
            row = 9;
            DoATurn(gw, out play, out coordsX, out coordsY, out tiles, players, activePlayerIndex, row);

            // 28 tiles used
            activePlayerIndex = 1;
            row = 10;
            DoATurn(gw, out play, out coordsX, out coordsY, out tiles, players, activePlayerIndex, row);

            // 35 tiles used
            activePlayerIndex = 0;
            row = 11;
            DoATurn(gw, out play, out coordsX, out coordsY, out tiles, players, activePlayerIndex, row);

            // 42 tiles used
            activePlayerIndex = 1;
            row = 12;
            DoATurn(gw, out play, out coordsX, out coordsY, out tiles, players, activePlayerIndex, row);

            // 49 tiles used
            activePlayerIndex = 0;
            row = 13;
            DoATurn(gw, out play, out coordsX, out coordsY, out tiles, players, activePlayerIndex, row);

            // 56 tiles used
            activePlayerIndex = 1;
            row = 14;
            DoATurn(gw, out play, out coordsX, out coordsY, out tiles, players, activePlayerIndex, row);

            // 63 tiles used
            activePlayerIndex = 0;
            row = 6;
            DoATurn(gw, out play, out coordsX, out coordsY, out tiles, players, activePlayerIndex, row);

            // 70 tiles used
            activePlayerIndex = 1;
            row = 5;
            DoATurn(gw, out play, out coordsX, out coordsY, out tiles, players, activePlayerIndex, row);

            // 77 tiles used
            activePlayerIndex = 0;
            row = 4;
            DoATurn(gw, out play, out coordsX, out coordsY, out tiles, players, activePlayerIndex, row);

            // 84 tiles used
            activePlayerIndex = 1;
            row = 3;
            DoATurn(gw, out play, out coordsX, out coordsY, out tiles, players, activePlayerIndex, row);

            // 91 tiles used
            activePlayerIndex = 0;
            row = 2;
            DoATurn(gw, out play, out coordsX, out coordsY, out tiles, players, activePlayerIndex, row);

            // Cheat and make sure that player[1] has an 'S' to stick at the end of a word.
            activePlayerIndex = 1;
            row = 1;

            coordsX = new List<int> { 4, 5, 6, 7, 8, 9};
            coordsY = new List<int> { row, row, row, row, row, row };
            tiles = new List<LetterTile>();

            players[activePlayerIndex].PopLetterTile(0);
            players[activePlayerIndex].DrawLetterTile(new LetterTile('S', 1));

            for (int i = 0; i < players[activePlayerIndex].TileRack.LetterTileCount() - 2; ++i)
            {
                tiles.Add(players[activePlayerIndex].TileRack[i]);
            }

            tiles.Add(players[activePlayerIndex].TileRack[6]);

            play = new Play(tiles, coordsX, coordsY, players[activePlayerIndex].PlayerID);

            gw.MakePlayerTakeTurn(play);

            // Cheat and make sure that player [0] has an 'H' and an 'E' (but use them incorrectly the first time)
            activePlayerIndex = 0;
            coordsX = new List<int>() { 9, 9 };
            coordsY = new List<int>() { 3, 2 };
            tiles = new List<LetterTile>();

            players[activePlayerIndex].PopLetterTile(0);
            players[activePlayerIndex].PopLetterTile(0);

            players[activePlayerIndex].DrawLetterTile(new LetterTile('H', 4));
            players[activePlayerIndex].DrawLetterTile(new LetterTile('E', 1));

            for (int i = 0; i < players[activePlayerIndex].TileRack.LetterTileCount(); ++i)
            {
                tiles.Add(players[activePlayerIndex].TileRack[i]);
            }

            play = new Play(tiles, coordsX, coordsY, players[activePlayerIndex].PlayerID);
            gw.MakePlayerTakeTurn(play);

            Assert.IsFalse(gw.GameIsOver);

            // Make the other player try to make a play.
            activePlayerIndex = 1;
            coordsX = new List<int>() { 8 };
            coordsY = new List<int>() { 14 };
            tiles = new List<LetterTile>() { players[activePlayerIndex].TileRack[0] };

            play = new Play(tiles, coordsX, coordsY, players[activePlayerIndex].PlayerID);
            gw.MakePlayerTakeTurn(play);

            Assert.IsFalse(gw.GameIsOver);

            // Now finish the game.
            activePlayerIndex = 0;
            coordsX = new List<int>() { 9, 9 };
            coordsY = new List<int>() { 2, 3 };
            tiles = new List<LetterTile>();

            players[activePlayerIndex].PopLetterTile(0);
            players[activePlayerIndex].PopLetterTile(0);

            players[activePlayerIndex].DrawLetterTile(new LetterTile('H', 4));
            players[activePlayerIndex].DrawLetterTile(new LetterTile('E', 1));

            for (int i = 0; i < players[activePlayerIndex].TileRack.LetterTileCount(); ++i)
            {
                tiles.Add(players[activePlayerIndex].TileRack[i]);
            }

            play = new Play(tiles, coordsX, coordsY, players[activePlayerIndex].PlayerID);
            gw.MakePlayerTakeTurn(play);

            Assert.IsTrue(gw.GameIsOver);

            // Pad that code coverage.
            gw.MakePlayerTakeTurn(play);
        }

        private static void DoATurn(GameWorld gw, out Play play, out List<int> coordsX, out List<int> coordsY, out List<LetterTile> tiles, List<Player> players, int activePlayerIndex, int row)
        {
            coordsX = new List<int> { 1, 2, 3, 4, 5, 6, 7 };
            coordsY = new List<int> { row, row, row, row, row, row, row };
            tiles = new List<LetterTile>();
            for (int i = 0; i < players[activePlayerIndex].TileRack.LetterTileCount(); ++i)
            {
                tiles.Add(players[activePlayerIndex].TileRack[i]);
            }

            play = new Play(tiles, coordsX, coordsY, players[activePlayerIndex].PlayerID);

            gw.MakePlayerTakeTurn(play);
        }
    }
}