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
        /// The ID of the game world.
        /// </summary>
        private string gameId;

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

        /// <summary>
        /// Tells whether the game has ended.
        /// </summary>
        private bool gameHasEnded;

        /// <summary>
        /// The dictionary that the game will look up words with.
        /// </summary>
        private Server.Dictionary dictionary;

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
            this.dictionary = new Server.Dictionary(Properties.Resources.dictionary);

            foreach (Player player in this.players)
            {
                for (int i = 0; i < 7; ++i)
                {
                    player.DrawLetterTile(this.bag.DrawLetterTile());
                }
            }
        }

        /// <summary>
        /// Gets or sets the gameId of the <see cref="GameWorld"/>.
        /// </summary>
        public string GameId
        {
            get
            {
                return this.gameId;
            }

            set
            {
                this.gameId = value;
            }
        }

        /// <summary>
        /// Gets a value indicating whether the game is over.
        /// </summary>
        public bool GameIsOver
        {
            get
            {
                return this.gameHasEnded;
            }
        }

        /// <summary>
        /// Makes the <see cref="Player"/> with turn priority take his turn, and score the most recent 
        /// <see cref="Play"/> that was made before it (as well as give its <see cref="Player"/> new <see cref="LetterTiles"/>.
        /// If the PlayerID of play does not match that of the player with turn priority, an exception will be thrown.
        /// </summary>
        /// <param name="play">The <see cref="Play"/> that the player is making.</param>
        public void MakePlayerTakeTurn(Play play)
        {
            if (this.gameHasEnded)
            {
                return;
            }

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
                    int score = this.gameBoard.ScorePlay(this.lastPlay, true);
                    this.turnOrder.AddScoreToPlayer(this.indexOfPlayerWhoLastMadeAPlay, score);
                    while (this.turnOrder.Players[this.indexOfPlayerWhoLastMadeAPlay].TileRack.LetterTileCount() < 7 && !(this.bag.LetterTileCount == 0))
                    {
                        this.turnOrder.Players[this.indexOfPlayerWhoLastMadeAPlay].DrawLetterTile(this.bag.DrawLetterTile());
                    }

                    this.lastPlay = play;
                    this.indexOfPlayerWhoLastMadeAPlay = this.turnOrder.ActivePlayerIndex;
                    this.turnOrder.MoveToNextPlayer();
                }
                else
                {
                    this.lastPlay = play;
                    this.indexOfPlayerWhoLastMadeAPlay = this.turnOrder.ActivePlayerIndex;
                    this.turnOrder.MoveToNextPlayer();
                    this.lastPlayIsInitialized = true;
                }

                // Put the new play onto the board.
                this.gameBoard.AddPlayToBoard(play);

                if (this.GameEndConditionsAreMet())
                {
                    if (!this.CheckWords(this.gameBoard.GetWordsInPlay(play)))
                    {
                        List<LetterTile> tiles = this.gameBoard.RemoveLastPlay();
                        foreach (LetterTile tile in tiles)
                        {
                            this.players[this.indexOfPlayerWhoLastMadeAPlay].DrawLetterTile(tile);
                        }

                        // Technically a lie, but whatever.
                        this.lastPlayIsInitialized = false;
                    }
                    else
                    {
                        this.turnOrder.AddScoreToPlayer(this.indexOfPlayerWhoLastMadeAPlay, this.gameBoard.ScorePlay(play, true));
                        this.gameHasEnded = true;
                    }
                }
            }
        }

        /// <summary>
        /// The <see cref="Player"/> who made the most recent <see cref="Play"/> will have all the words
        /// in that <see cref="Play"/> checked against the dictionary. At least one of them is not in the dictionary,
        /// he will have the <see cref="LetterTile"/>s used in that player returned to him. Otherwise, the challenging
        /// <see cref="Player"/> will lose his next turn.
        /// </summary>
        /// <param name="challengingPlayerID">The ID of the <see cref="Player"/> who is challenging a <see cref="Play"/>.</param>
        public void ChallengeLastPlay(int challengingPlayerID)
        {
            // If there is nothing to challenge, just don't bother.
            if (!this.lastPlayIsInitialized)
            {
                return;
            }

            List<string> words = this.gameBoard.GetWordsInPlay(this.lastPlay);

            // This will either get initialized later, or an exception will get thrown because
            // of bad input.
            Player challengingPlayer = null;
            bool playerWasInitialized = false;

            for (int i = 0; i < this.players.Count; ++i)
            {
                if (this.players[i].PlayerID == challengingPlayerID)
                {
                    challengingPlayer = this.players[i];
                    playerWasInitialized = true;
                }
            }

            // Who challenges his own play? I mean really, who does that even?
            if (!playerWasInitialized || this.players[this.indexOfPlayerWhoLastMadeAPlay] == challengingPlayer)
            {
                throw new InvalidChallengeException("Player " + challengingPlayerID.ToString() + " is not a valid challenger of player " + this.indexOfPlayerWhoLastMadeAPlay.ToString());
            }

            if (this.CheckWords(words))
            {
                // The challenger loses challenge.
                if (this.turnOrder.Players[this.turnOrder.ActivePlayerIndex].PlayerID == challengingPlayerID)
                {
                    this.turnOrder.MoveToNextPlayer();
                }
                else
                {
                    challengingPlayer.IncrementSkipCount();
                }
            }
            else
            {
                List<LetterTile> tiles = this.gameBoard.RemoveLastPlay();
                foreach (LetterTile tile in tiles)
                {
                    this.players[this.indexOfPlayerWhoLastMadeAPlay].DrawLetterTile(tile);
                }
            }
            
            // Technically a lie, but whatever.
            this.lastPlayIsInitialized = false;
        }

        /// <summary>
        /// Gets the PlayerID of the <see cref="Player"/> who currently has turn priority.
        /// </summary>
        /// <returns>The PlayerID of the player who currently has turn priority.</returns>
        public int GetActivePlayerID()
        {
            return this.turnOrder.Players[this.turnOrder.ActivePlayerIndex].PlayerID;
        }

        /// <summary>
        /// Returns a string containing the <see cref="GameWorld"/> game id.
        /// </summary>
        /// <returns>See above.</returns>
        public override string ToString()
        {
            string toReturn;
            toReturn = "Game World- " + this.GameId;
            return toReturn;
        }

        /// <summary>
        /// Checks whether the <see cref="Bag"/> is empty and a <see cref="Player"/>'s <see cref="LetterTileRack"/> is empty.
        /// </summary>
        /// <returns>True if the <see cref="Bag"/> is empty, and a <see cref="Player"/>'s <see cref="LetterTileRack"/> is empty, false otherwise.</returns>
        private bool GameEndConditionsAreMet()
        {
            if (!(this.bag.LetterTileCount == 0))
            {
                return false;
            }

            int emptyRackCount = 0;
            foreach (Player player in this.players)
            {
                if (player.TileRack.LetterTileCount() == 0)
                {
                    ++emptyRackCount;
                }
            }

            if (emptyRackCount == 0)
            {
                return false;
            }

            return true;
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
            foreach (string word in words)
            {
                if (!this.dictionary.ContainsWordBinSearch(word))
                {
                    return false;
                }                
            }

            return true;
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
            /// Returns a string of the details of why an <see cref="InvalidChallengeException"/> was thrown.
            /// </summary>
            /// <returns>A string of exception details.</returns>
            public override string ToString()
            {
                return this.message;
            }
        }

        /// <summary>
        /// An exception of this type means that someone tried to make an invalid challenge.
        /// </summary>
        public class InvalidChallengeException : Exception
        {
            /// <summary>
            /// A string that contains the details of why an <see cref="InvalidChallengeException"/> was thrown.
            /// </summary>
            private string message;

            /// <summary>
            /// Initializes a new instance of the <see cref="InvalidChallengeException" /> class.
            /// </summary>
            public InvalidChallengeException()
            {
                this.message = "InvalidChallenge: A challenge was made incorrectly.";
            }

            /// <summary>
            /// Initializes a new instance of the <see cref="InvalidChallengeException"/> class.
            /// </summary>
            /// <param name="message">The private member message will be set to message.</param>
            public InvalidChallengeException(string message)
            {
                this.message = "InvalidChallenge: " + message;
            }

            /// <summary>
            /// Returns a string of the details of why an <see cref="InvalidChallengeException"/> was thrown.
            /// </summary>
            /// <returns>A string of exception details.</returns>
            public override string ToString()
            {
                return this.message;
            }
        }
    }
}
