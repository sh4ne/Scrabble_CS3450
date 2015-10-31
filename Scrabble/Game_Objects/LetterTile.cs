//-----------------------------------------------------------------------
// <copyright file="LetterTile.cs" company="Scrabble Project Developers">
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
    /// This class allows for the instantiation of letter tile objects,
    /// each of which has a point value and a letter value (note that a letter value of 'n' indicates null).
    /// Throws InvalidLetterValue exception.
    /// </summary>
    public class LetterTile
    {
        /// <summary>
        /// The number of points that this letter tile adds to plays that contain it
        /// (not including multipliers).
        /// </summary>
        private int pointValue;
        
        /// <summary>
        /// The letter value of letter tile. It can be A-Z or a space, or 'n' (which signifies null).
        /// </summary>
        private char letterValue;
        
        /// <summary>
        /// Initializes a new instance of the <see cref="LetterTile"/> class.
        /// </summary>
        /// <param name="letterValue">The letterTile member will be set to this value.</param>
        /// <param name="pointValue">The pointValue member will be set to this value.</param>
        public LetterTile(char letterValue, int pointValue)
        {
            if (letterValue == 'n')
            {
                this.letterValue = letterValue;
                this.pointValue = 0;
            }
            else if (letterValue == ' ' || char.IsUpper(letterValue))
            {
                this.letterValue = letterValue;
                this.pointValue = pointValue;
            }
            else
            {
                InvalidLetterValue invalidLetterValue = new InvalidLetterValue(letterValue + " is not 'n', ' ', or between 'A' and 'Z'.");
                throw invalidLetterValue;
            }
        }

        /// <summary>
        /// Gets the private letterValue member.
        /// </summary>
        public char LetterValue
        {
            get { return this.LetterValue; }
        }

        /// <summary>
        /// Gets the private letterValue member.
        /// </summary>
        public int PointValue
        {
            get { return this.pointValue; }
        }

        /// <summary>
        /// An exception of this class type means that the value of letterValue is invalid.
        /// </summary>
        public class InvalidLetterValue : Exception
        {
            /// <summary>
            /// A string that contains the details of why an InvalidLetterValue exception was thrown.
            /// </summary>
            private string message;

            /// <summary>
            /// Initializes a new instance of the <see cref="InvalidLetterValue"/> class.
            /// </summary>
            public InvalidLetterValue()
            {
                this.message = "InvalidLetterValue: A LetterTile object was assigned an invalid letter value.";
            }

            /// <summary>
            /// Initializes a new instance of the <see cref="InvalidLetterValue"/> class.
            /// </summary>
            /// <param name="message">The private member message will be set to message.</param>
            public InvalidLetterValue(string message)
            {
                this.message = "InvalidLetterValue: " + message;
            }

            /// <summary>
            /// Returns a string of the details of why an InvalidLetterValue exception was thrown.
            /// </summary>
            /// <returns>A string of exception details.</returns>
            public override string ToString()
            {
                return this.message;
            }
        }
    }
}
