//-----------------------------------------------------------------------
// <copyright file="GameBoardSquare.cs" company="Scrabble Project Developers">
//     Copyright (c) Scrabble Project Developers. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Scrabble.Game_Objects
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// A GameBoardSquare is a container that can hold either 0 or 1 letter tiles.
    /// It also contains a word and letter multiplier (only one of which can be greater than 1).
    /// </summary>
    public class GameBoardSquare
    {
        /// <summary>
        /// The letter tile contained in the square. If the containedTile is null, it means that the 
        /// GameBoardSquare is empty.
        /// </summary>
        private LetterTile containedTile;
        
        /// <summary>
        /// The square's letter multiplier (must be from 1 - 3, and 1 if wordMultiplier is not 1).
        /// </summary>
        private int letterMultiplier;
        
        /// <summary>
        /// The square's word multiplier (must be from 1 - 3, and 1 if letter multiplier is not 1).
        /// </summary>
        private int wordMultiplier;

        /// <summary>
        /// The x coordinate of the square's location in the board in which it is contained.
        /// Can range from 0 - 14.
        /// </summary>
        private int coordinateX;

        /// <summary>
        /// The y coordinate of the square's location in the board in which it is contained.
        /// Can range from 0 - 14.
        /// </summary>
        private int coordinateY;

        /// <summary>
        /// The start square is the square that is in the center of the board.
        /// </summary>
        private bool isStartSquare;

        /// <summary>
        /// Initializes a new instance of the <see cref="GameBoardSquare"/> class.
        /// </summary>
        /// <param name="coordinateX">The private member coordinateX will be set to this value (must be from 0 - 14).</param>
        /// <param name="coordinateY">The private member coordinateY will be set to this value (must be from 0 - 14).</param>
        /// <param name="wordMultiplier">The private member wordMultiplier will be set to this value (must be from 1 - 3,
        /// and must be 1 if letterMultiplier is not 1).</param>
        /// <param name="letterMultiplier">The private member letterMultiplier will be set to this value (must be from 1 - 3,
        /// and must be 1 if wordMultiplier is not 1).</param>
        public GameBoardSquare(int coordinateX, int coordinateY, int wordMultiplier, int letterMultiplier)
        {
            if (coordinateX >= 0 && coordinateX < 15)
            {
                this.coordinateX = coordinateX;
            }
            else
            {
                InvalidGameBoardSquareConfigurationException invalidSquare = new InvalidGameBoardSquareConfigurationException("coordinateX set to " + coordinateX.ToString());
                throw invalidSquare;
            }

            if (coordinateY >= 0 && coordinateY < 15)
            {
                this.coordinateY = coordinateY;
            }
            else
            {
                InvalidGameBoardSquareConfigurationException invalidSquare = new InvalidGameBoardSquareConfigurationException("coordinateY set to " + coordinateY.ToString());
                throw invalidSquare;
            }
            
            this.isStartSquare = this.coordinateX == 7 && this.coordinateY == 7;

            if (wordMultiplier != 1 && letterMultiplier != 1)
            {
                InvalidGameBoardSquareConfigurationException invalidSquare = new InvalidGameBoardSquareConfigurationException("wordMultiplier = " + wordMultiplier.ToString() + ", and letterMultiplier = " + letterMultiplier.ToString());
                throw invalidSquare;
            }

            if (wordMultiplier < 1 || wordMultiplier > 3)
            {
                InvalidGameBoardSquareConfigurationException invalidSquare = new InvalidGameBoardSquareConfigurationException("wordMultiplier out of bounds (wordMultiplier = " + wordMultiplier.ToString() + ")");
                throw invalidSquare;
            }

            if (letterMultiplier < 1 || letterMultiplier > 3)
            {
                InvalidGameBoardSquareConfigurationException invalidSquare = new InvalidGameBoardSquareConfigurationException("letterMultiplier out of bounds (wordMultiplier = " + letterMultiplier.ToString() + ")");
                throw invalidSquare;
            }

            this.wordMultiplier = wordMultiplier;
            this.letterMultiplier = letterMultiplier;

            this.containedTile = new LetterTile('n', 0);
        }

        /// <summary>
        /// Gets a value indicating whether this is the start square (i.e. the central square in the board).
        /// </summary>
        public bool IsStartSquare
        {
            get { return this.isStartSquare; }
        }

        /// <summary>
        /// Gets letterMultiplier.
        /// </summary>
        public int LetterMultiplier
        {
            get { return this.letterMultiplier; }
        }

        /// <summary>
        /// Gets wordMultiplier.
        /// </summary>
        public int WordMultiplier
        {
            get { return this.wordMultiplier; }
        }

        /// <summary>
        /// Gets coordinateY.
        /// </summary>
        public int CoordinateX
        {
            get { return this.coordinateX; }
        }

        /// <summary>
        /// Gets coordinateY.
        /// </summary>
        public int CoordinateY
        {
            get { return this.coordinateY; }
        }

        /// <summary>
        /// An exception of type InvalidGameBoardSquareConfigurationException is thrown if a GameBoardSquare is configured incorrectly.
        /// </summary>
        public class InvalidGameBoardSquareConfigurationException : Exception
        {
            /// <summary>
            /// A string that contains the details of why an InvalidGameBoardSquareConfigurationException exception was thrown.
            /// </summary>
            private string message;

            /// <summary>
            /// Initializes a new instance of the <see cref="InvalidGameBoardSquareConfigurationException"/> class.
            /// </summary>
            public InvalidGameBoardSquareConfigurationException()
            {
                this.message = "InvalidGameBoardSquareConfigurationException: A GameBoardSquare object was incorrectly configured.";
            }

            /// <summary>
            /// Initializes a new instance of the <see cref="InvalidGameBoardSquareConfigurationException"/> class.
            /// </summary>
            /// <param name="message">The private member message will be set to message.</param>
            public InvalidGameBoardSquareConfigurationException(string message)
            {
                this.message = "InvalidGameBoardSquareConfigurationException: " + message;
            }

            /// <summary>
            /// Returns a string of the details of why an InvalidGameBoardSquareConfigurationException exception was thrown.
            /// </summary>
            /// <returns>A string of exception details.</returns>
            public override string ToString()
            {
                return this.message;
            }
        }
    }
}
