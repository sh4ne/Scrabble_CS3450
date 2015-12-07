//-----------------------------------------------------------------------
// <copyright file="GameWorld.cs" company="Scrabble Project Developers">
//     Copyright (c) Scrabble Project Developers. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Scrabble.GameWorld
{
    using System;
    using System.Collections.Generic;
    using Scrabble.Game_Objects;
    using Scrabble.PlayerClass;

    /// <summary>
    /// A GameWorld object contains all the objects necessary to conduct 1 game of Scrabble (1 game board,
    /// 2-4 players, a bag, etc).
    /// </summary>
    public class GameWorld
    {
        /// <summary>
        /// The <see cref="Board"/> on which the game is to be played.
        /// </summary>
        private Board gameBoard;

        /// <summary>
        /// The <see cref="Bag"/> which contains all of the <see cref="LetterTile"/>s to start off with.
        /// </summary>
        private Bag bag;

        /// <summary>
        /// The players who are playing the game. There should be 2-4 players.
        /// </summary>
        private List<Player> players;

        /// <summary>
        /// The turn order of the <see cref="Player"/>s.
        /// </summary>
        private TurnOrder turnOrder;

        /// <summary>
        ///  The <see cref="Play"/> that was added to the <see cref="Board"/> most recently.
        /// </summary>
        private Play lastPlay;

        /// <summary>
        /// Keeps track of whether there exists a lastPlay.
        /// </summary>
        private bool lastPlayIsInitialized;

        /// <summary>
        /// The index of the player who made the most recent play.
        /// </summary>
        private int indexOfPlayerWhoLastMadeAPlay;

        // Need a dictionary

        /// <summary>
        /// Initializes a new instance of the <see cref="GameWorld" /> class.
        /// </summary>
        /// <param name="players">A list of the <see cref="Player"/>s who are playing the game.</param>
        public GameWorld(List<Player> players)
        {
            this.players = players;
            this.turnOrder = new TurnOrder(players);
            this.bag = new Bag();
            this.gameBoard = new Board();
            this.lastPlayIsInitialized = false;

            foreach (Player player in this.players)
            {
                for (int i = 0; i < 7; ++i)
                {
                    player.DrawLetterTile(this.bag.DrawLetterTile());
                }
            }
        }

        /// <summary>
        /// Makes the <see cref="Player"/> with turn priority take his turn. If the PlayerID of play
        /// does not match that of the player with turn priority, an exception will be thrown.
        /// </summary>
        /// <param name="play">The <see cref="Play"/> that the player is making.</param>
        public void MakePlayerTakeTurn(Play play)
        {
            if (this.turnOrder.Players[this.turnOrder.ActivePlayerIndex].PlayerID != play.GetPlayerID())
            {
                throw new InvalidPlayOrderException();
            }
            else
            {
                // Remove the letter tiles from the player's rack. If a letter tile is not in his rack, 
                // an exception will get thrown.
                for (int i = 0; i < play.GetParallelListLength(); ++i)
                {
                    this.turnOrder.Players[this.turnOrder.ActivePlayerIndex].PopLetterTile(play.GetLetterTile(i));
                }

                // Score the play of the player who made the most recent play (not this play).
                if (this.lastPlayIsInitialized)
                {
                    this.turnOrder.Players[this.indexOfPlayerWhoLastMadeAPlay].AddToScore(this.gameBoard.ScorePlay(this.lastPlay, true));
                    this.lastPlay = play;
                    this.indexOfPlayerWhoLastMadeAPlay = this.turnOrder.ActivePlayerIndex;
                    this.turnOrder.MoveToNextPlayer();
                    
                }
                else
                {
                    this.lastPlay = play;
                    this.indexOfPlayerWhoLastMadeAPlay = this.turnOrder.ActivePlayerIndex;
                    this.turnOrder.MoveToNextPlayer();
                }

                // Put the new play on the board.
                this.gameBoard.AddPlayToBoard(play);
            }
        }

        public void ChallengeLastPlay(int challengedPlayerID, int challengingPlayerID)
        {
            List<string> words = this.gameBoard.GetWordsInPlay(this.lastPlay);
            if (this.CheckWords(words))
            {
                // The challenger loses challenge.

            }
            else
            {
                // The challenger wins the challenge.
            }
        }

        /// <summary>
        /// Checks the dictionary to make sure that the given list of words are all
        /// real words. NOTE: At the moment, there is not a dictionary, so it returns words.Count % 2 == 0.
        /// </summary>
        /// <param name="words">The list of words to be checked.</param>
        /// <returns>True if all of the words are in the dictionary. Returns false otherwise.</returns>
        private bool CheckWords(List<string> words)
        {
            // There isn't an actual dictionary at the moment. This'll have to change later.
            return words.Count % 2 == 0;
        }

        /// <summary>
        /// An exception of this type means that someone tried to make a play out of order.
        /// </summary>
        public class InvalidPlayOrderException : Exception
        {
            /// <summary>
            /// A string that contains the details of why an <see cref="InvalidPlayOrderException"/> was thrown.
            /// </summary>
            private string message;

            /// <summary>
            /// Initializes a new instance of the <see cref="InvalidPlayOrderException" /> class.
            /// </summary>
            public InvalidPlayOrderException()
            {
                this.message = "InvalidPlayOrder: A player other than the active player attempted to make a play.";
            }

            /// <summary>
            /// Initializes a new instance of the <see cref="InvalidPlayOrderException"/> class.
            /// </summary>
            /// <param name="message">The private member message will be set to message.</param>
            public InvalidPlayOrderException(string message)
            {
                this.message = "InvalidPlayOrder: " + message;
            }

            /// <summary>
            /// Returns a string of the details of why an <see cref="InvalidTurnQueueSizeException"/> was thrown.
            /// </summary>
            /// <returns>A string of exception details.</returns>
            public override string ToString()
            {
                return this.message;
            }
        }
    }
}
