//-----------------------------------------------------------------------
// <copyright file="GameStateTests.cs" company="Scrabble Project Developers">
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
    using Scrabble;
    using Scrabble.Game_Objects;

    /// <summary>
    /// Unit tests for the game state
    /// </summary>
    [TestClass]
    public class GameStateTests
    {
        /// <summary>
        /// All test logic for the game state unit test
        /// </summary>
        [TestMethod]
        public void GameStateTest()
        {
            // should be able to create empty game state
            GameState emptyGameState = new GameState();

            var user1 = new Scrabble.PlayerClass.Player(1, "user1");
            emptyGameState.AddPlayer(user1);
            
            this.AssertPlayerEqual(emptyGameState.PlayerList[0], user1);

            // set up
            Board board = new Board();
            List<Scrabble.PlayerClass.Player> playerList = new List<Scrabble.PlayerClass.Player>();
            Bag tileBag = new Bag();
            
            var letterTileA = new LetterTile('A', 1);
            var letterTileN = new LetterTile('N', 1);
            var letterTileT = new LetterTile('T', 1);
            var letterTileS = new LetterTile('S', 1);
            List<LetterTile> word = new List<LetterTile> { letterTileA, letterTileN, letterTileT, letterTileS };
            var coordsX = new List<int>() { 6, 7, 8, 9 };
            var coordsY = new List<int>() { 7, 7, 7, 7 };
            Play recentPlay = new Play(word, coordsX, coordsY, 42);

            try
            {
                board.AddPlayToBoard(recentPlay);
            }
            catch (Exception err)
            {
                Assert.Fail();
            }

            for (int i = 1; i <= 3; ++i)
            {
                playerList.Add(new Scrabble.PlayerClass.Player(i, "user" + i));
                playerList[i - 1].AddToScore(i * 10);
            }

            playerList[2].IncrementSkipCount();
            playerList[1].ToggleVote();
            
            TurnOrder playerOrder = new TurnOrder(playerList);

            GameState gameState = new GameState(board, playerList, tileBag, playerOrder, recentPlay);

            playerList.Add(new Scrabble.PlayerClass.Player(4, "user" + 4));
            playerList[3].AddToScore(4 * 10);

            gameState.AddPlayer(playerList[3]);

            var player5 = new Scrabble.PlayerClass.Player(5, "user" + 5);
            player5.AddToScore(5 * 10);

            gameState.AddPlayer(player5);

            Assert.IsTrue(gameState.PlayerList.Count == 4);

            for (int i = 0; i < playerList.Count; ++i)
            {
                this.AssertPlayerEqual(gameState.PlayerList[i], playerList[i]);
            }

            Assert.IsTrue(board.Equals(gameState.GameBoard));
            Assert.IsTrue(tileBag.Equals(gameState.TileBag));
            Assert.IsTrue(playerOrder.Equals(gameState.PlayerOrder));
            Assert.IsTrue(recentPlay.Equals(gameState.RecentPlay));
        }

        /// <summary>
        /// Asserts to players are equal after filtration for GameState.
        /// </summary>
        /// <param name="player1">Any player object</param>
        /// <param name="player2">Any other player object</param>
        private void AssertPlayerEqual(Scrabble.PlayerClass.Player player1, Scrabble.PlayerClass.Player player2)
        {
            Assert.IsTrue(player1.HasTurnPriority == player2.HasTurnPriority);
            Assert.IsTrue(player1.HasVotedToEndGame == player2.HasVotedToEndGame);
            Assert.IsTrue(player1.PlayerID == player2.PlayerID);
            Assert.IsTrue(player1.Score == player2.Score);
            Assert.IsTrue(player1.SkipCount == player2.SkipCount);
            Assert.IsTrue(player1.Username == player2.Username);
        }
    }
}
