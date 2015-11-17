//-----------------------------------------------------------------------
// <copyright file="Player.cs" company="Scrabble Project Developers">
//     Copyright (c) Scrabble Project Developers. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Scrabble.PlayerClass
{
    using Game_Objects;

    /// <summary>
    /// The Player class keeps track of 
    /// </summary>
    public class Player
    {
        /// <summary>
        /// The player's username.
        /// </summary>
        private string username;

        /// <summary>
        /// The player's ID.
        /// </summary>
        private int playerID;

        /// <summary>
        /// The number of turns that should skipped for this player.
        /// </summary>
        private int skipCount;

        /// <summary>
        /// The player's score.
        /// </summary>
        private int score;

        /// <summary>
        /// Is true if the player has turn priority.
        /// </summary>
        private bool hasTurnPriority;

        /// <summary>
        /// The player's LetterTileRack.
        /// </summary>
        private LetterTileRack tileRack;

        /// <summary>
        /// Initializes a new instance of the <see cref="Player" /> class.
        /// </summary>
        /// <param name="playerID">this.playerID will be set equal to this parameter.</param>
        /// <param name="username">this.username will be set equal to this parameter.</param>
        public Player(int playerID, string username)
        {
            this.playerID = playerID;
            this.username = username;
            this.skipCount = 0;
            this.score = 0;
            this.hasTurnPriority = false;
            this.tileRack = new LetterTileRack();
        }

        /// <summary>
        /// Gets this.username.
        /// </summary>
        public string Username
        {
            get
            {
                return this.username;
            }
        }

        /// <summary>
        /// Gets this.playerID.
        /// </summary>
        public int PlayerID
        {
            get
            {
                return this.playerID;
            }
        }

        /// <summary>
        /// Gets this.score.
        /// </summary>
        public int Score
        {
            get
            {
                return this.score;
            }
        }

        /// <summary>
        /// Gets this.skipCount.
        /// </summary>
        public int SkipCount
        {
            get
            {
                return this.skipCount;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the player has turn priority.
        /// </summary>
        public bool HasTurnPriority
        {
            get
            {
                return this.hasTurnPriority;
            }

            set
            {
                this.hasTurnPriority = value;
            }
        }

        /// <summary>
        /// Gets this.tileRack.
        /// </summary>
        public LetterTileRack TileRack
        {
            get
            {
                return this.tileRack;
            }
        }

        /// <summary>
        /// Adds an amount of points to the score (a negative amount of points will remove
        /// points, down to 0 points at minimum).
        /// </summary>
        /// <param name="points">The number of points to be added to the player's score.</param>
        public void AddToScore(int points)
        {
            this.score += points;
            if (this.score < 0)
            {
                this.score = 0;
            }
        }

        /// <summary>
        /// Adds 1 to this.skipCount.
        /// </summary>
        public void IncrementSkipCount()
        {
            ++this.skipCount;
        }

        /// <summary>
        /// Subtracts 1 from this.skipCount.
        /// </summary>
        public void DecrementSkipCount()
        {
            --this.skipCount;
            if (this.skipCount < 0)
            {
                this.skipCount = 0;
            }
        }

        /// <summary>
        /// Insert a LetterTile into this.tileRack. This method does not catch any exceptions
        /// thrown by this.tileRack.
        /// </summary>
        /// <param name="drawnLetterTile">The LetterTile that is being inserted into this.tileRack.</param>
        public void DrawLetterTile(LetterTile drawnLetterTile)
        {
            this.tileRack.InsertLetterTile(drawnLetterTile);
        }

        /// <summary>
        /// Remove the LetterTile in the index location from tileRack, and return it. This method
        /// does not catch any exceptions thrown by this.tileRack.
        /// </summary>
        /// <param name="index">The location in this.tileRack of the LetterTile to be removed/returned.</param>
        /// <returns>The LetterTile at the index location in this.tileRack.</returns>
        public LetterTile PopLetterTile(int index)
        {
            return this.tileRack.PopLetterTile(index);
        }
    }
}
