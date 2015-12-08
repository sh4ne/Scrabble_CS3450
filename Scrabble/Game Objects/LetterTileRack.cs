//-----------------------------------------------------------------------
// <copyright file="LetterTileRack.cs" company="Scrabble Project Developers">
//     Copyright (c) Scrabble Project Developers. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Scrabble.Game_Objects
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// A LetterRack is a container that holds a set of 0-7 LetterTiles. Throws InvalidLetterTileInsertionException.
    /// </summary>
    public class LetterTileRack
    {
        /// <summary>
        /// The set of LetterTile objects that the LetterRack contains.
        /// </summary>
        private List<LetterTile> containedLetterTileSet;

        /// <summary>
        /// Initializes a new instance of the <see cref="LetterTileRack"/> class.
        /// </summary>
        public LetterTileRack()
        {
            this.containedLetterTileSet = new List<LetterTile>();
        }

        /// <summary>
        /// Returns the specified LetterTile in the containedLetterTileSet. Throws InvalidLetterTileAccessException.
        /// </summary>
        /// <param name="index">The index of the LetterTile in containedLetterTileSet to be returned.</param>
        /// <returns>Returns the LetterTile in containedLetterTileSet located at index.</returns>
        public LetterTile this[int index]
        {
            get
            {
                if (index < 0 || index >= this.containedLetterTileSet.Count)
                {
                    throw new InvalidLetterTileAccessException("InvalidLetterTileAccess: Access to the LetterTile at the specified index of " +
                    index.ToString() + " is invalid (the LetterTileRack currently contains " + this.containedLetterTileSet.Count + " LetterTiles).");
                }
                else
                {
                    return this.containedLetterTileSet[index];
                }
            }            
        }

        /// <summary>
        /// Inserts a LetterTile into the LetterTileRack. Throws InvalidLetterTileInsertionException.
        /// </summary>
        /// <param name="letterTileToBeInserted">The LetterTile that is being inserted into the LetterTileRack.</param>
        public void InsertLetterTile(LetterTile letterTileToBeInserted)
        {
            if (this.containedLetterTileSet.Count == 7)
            {
                throw new InvalidLetterTileInsertionException("The insertion of letter tile with LetterValue == " + letterTileToBeInserted.LetterValue +
                    " and PointValue == " + letterTileToBeInserted.PointValue + " is invalid. The LetterTileRack is full.");
            }
            else
            {
                // Inserting a null LetterTile is the equivalent of not inserting anything.
                if (!letterTileToBeInserted.IsNullLetterTile())
                {
                    this.containedLetterTileSet.Add(letterTileToBeInserted);
                }
            }
        }

        /// <summary>
        /// Pops a letter from the LetterTileRack, and returns it. Throws InvalidLetterTileAccessException.
        /// </summary>
        /// <param name="index">The index of the LetterTile to be popped.</param>
        /// <returns>The LetterTile at index.</returns>
        public LetterTile PopLetterTile(int index)
        {
            if (index < 0 || index >= this.containedLetterTileSet.Count)
            {
                throw new InvalidLetterTileAccessException("InvalidLetterTileAccess: Access to the LetterTile at the specified index of " + 
                    index.ToString() + " is invalid (the LetterTileRack currently contains " + this.containedLetterTileSet.Count + " LetterTiles).");
            }
            else
            {
                char letterValue = this.containedLetterTileSet[index].LetterValue;
                int pointValue = this.containedLetterTileSet[index].PointValue;
                this.containedLetterTileSet.Remove(this.containedLetterTileSet[index]);
                return new LetterTile(letterValue, pointValue);
            }
        }

        /// <summary>
        /// Pops a <see cref="LetterTile"/> from the <see cref="LetterTileRack"/>, and returns it. Throws <see cref="InvalidLetterTileAccessException"/>
        /// if the <see cref="LetterTile"/> was not present in the <see cref="LetterTileRack"/>.
        /// </summary>
        /// <param name="tile"></param>
        /// <returns></returns>
        public LetterTile PopLetterTile(LetterTile tile)
        {
            for (int i = 0; i < this.LetterTileCount(); ++i)
            {
                if (this.containedLetterTileSet[i].Equals(tile))
                {
                    this.containedLetterTileSet.Remove(this.containedLetterTileSet[i]);
                    return tile;
                }
            }

            throw new InvalidLetterTileAccessException("InvalidLetterTileAccess: The LetterTile " + tile.LetterValue.ToString() + ", " + tile.PointValue.ToString() + " could not be found.");
        }

        /// <summary>
        /// Returns the number of LetterTiles contained in the LetterTileRack.
        /// </summary>
        /// <returns>The number of LetterTiles contained in the LetterTileRack.</returns>
        public int LetterTileCount()
        {
            return this.containedLetterTileSet.Count;
        }

        /// <summary>
        /// Gets a list of the LetterTiles in the LetterTileRack.
        /// </summary>
        /// <returns>Returns a list of the LetterTiles in the LetterTileRack.</returns>
        public List<LetterTile> GetAllLetterTiles()
        {
            return this.containedLetterTileSet;
        }

        /// <summary>
        /// An exception of this class type means that a LetterTile insertion was attempted into a full LetterTileRack.
        /// </summary>
        public class InvalidLetterTileInsertionException : Exception
        {
            /// <summary>
            /// A string that contains the details of why an InvalidLetterTileInsertionException was thrown.
            /// </summary>
            private string message;

            /// <summary>
            /// Initializes a new instance of the <see cref="InvalidLetterTileInsertionException"/> class.
            /// </summary>
            public InvalidLetterTileInsertionException()
            {
                this.message = "InvalidLetterTileInsertion: An insertion was attempted into a full LetterTileRack.";
            }

            /// <summary>
            /// Initializes a new instance of the <see cref="InvalidLetterTileInsertionException"/> class.
            /// </summary>
            /// <param name="message">The private member message will be set to message.</param>
            public InvalidLetterTileInsertionException(string message)
            {
                this.message = "InvalidLetterTileInsertion: " + message;
            }

            /// <summary>
            /// Returns a string of the details of why an InvalidLetterTileInsertionException was thrown.
            /// </summary>
            /// <returns>A string of exception details.</returns>
            public override string ToString()
            {
                return this.message;
            }
        }

        /// <summary>
        /// An exception of this class type means an invalid attempt was made to access a
        /// LetterTile in a LetterTileRack (e.g. attempting to access a LetterTile in an empty LetterTileRack).
        /// </summary>
        public class InvalidLetterTileAccessException : Exception
        {
            /// <summary>
            /// A string that contains the details of why an InvalidLetterTileAccessException exception was thrown.
            /// </summary>
            private string message;

            /// <summary>
            /// Initializes a new instance of the <see cref="InvalidLetterTileAccessException"/> class.
            /// </summary>
            public InvalidLetterTileAccessException()
            {
                this.message = "InvalidLetterTileAccess: Invalid access was attempted to a LetterTile in a LetterTileRack.";
            }

            /// <summary>
            /// Initializes a new instance of the <see cref="InvalidLetterTileAccessException"/> class.
            /// </summary>
            /// <param name="message">The private member message will be set to message.</param>
            public InvalidLetterTileAccessException(string message)
            {
                this.message = "InvalidLetterTileAccess: " + message;
            }

            /// <summary>
            /// Returns a string of the details of why an InvalidLetterTileAccessException was thrown.
            /// </summary>
            /// <returns>A string of exception details.</returns>
            public override string ToString()
            {
                return this.message;
            }
        }
    }
}
