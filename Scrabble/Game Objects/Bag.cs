//-----------------------------------------------------------------------
// <copyright file="Bag.cs" company="Scrabble Project Developers">
//     Copyright (c) Scrabble Project Developers. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Scrabble.Game_Objects
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// The bag class contains a set of LetterTiles. LetterTiles can be removed from the bag,
    /// and added to the bag.
    /// </summary>
    public class Bag
    {
        /// <summary>
        /// The set of LetterTiles contained in the bag.
        /// </summary>
        private List<LetterTile> letterTileSet;

        /// <summary>
        /// A dictionary of each possible LetterTile, and their possible point values
        /// (used to make sure no one inserts invalid letters).
        /// </summary>
        private Dictionary<char, int> letterTilePointValues;

        /// <summary>
        /// A dictionary of each possible LetterTile, and their maximum quantities.
        /// </summary>
        private Dictionary<char, int> letterTileMaxQuantities;

        /// <summary>
        /// Initializes a new instance of the <see cref="Bag" /> class.
        /// </summary>
        public Bag()
        {
            this.letterTileSet = new List<LetterTile>();
            this.letterTilePointValues = new Dictionary<char, int>();
            this.letterTileMaxQuantities = new Dictionary<char, int>();
            this.InitializeLetterTiles();
            this.MixLetters();
        }

        /// <summary>
        /// Gets the number of LetterTiles in the bag.
        /// </summary>
        public int LetterTileCount
        {
            get
            {
                return this.letterTileSet.Count;
            }
        }

        /// <summary>
        /// Draw a LetterTile from the Bag. Throw InvalidLetterTileRemovalException if the Bag is empty.
        /// </summary>
        /// <returns>Returns the first LetterTile in this.letterTileSet.</returns>
        public LetterTile DrawLetterTile()
        {
            if (this.LetterTileCount == 0)
            {
                throw new InvalidLetterTileRemovalException(" Invalid attempt to remove a LetterTile from an empty Bag object.");
            }

            LetterTile drawnLetterTile = this.letterTileSet[0];
            this.letterTileSet.Remove(this.letterTileSet[0]);
            return drawnLetterTile;
        }

        /// <summary>
        /// Insert a LetterTile into the bag. Throws InvalidLetterTileInsertionException.
        /// </summary>
        /// <param name="insertedLetterTile">The LetterTile to be inserted into the bag.</param>
        public void InsertLetterTile(LetterTile insertedLetterTile)
        {
            if (this.letterTilePointValues[insertedLetterTile.LetterValue] != insertedLetterTile.PointValue)
            {
                throw new InvalidLetterTileInsertionException("The LetterValue " + insertedLetterTile.LetterValue + " does not match the PointValue " + insertedLetterTile.PointValue.ToString() + ".");
            }

            int countOfInsertedLetterTilesAlreadyPresent = 0;
            foreach (LetterTile lt in this.letterTileSet)
            {
                if (lt.Equals(insertedLetterTile))
                {
                    ++countOfInsertedLetterTilesAlreadyPresent;
                }
            }

            if (countOfInsertedLetterTilesAlreadyPresent >= this.letterTileMaxQuantities[insertedLetterTile.LetterValue])
            {
                throw new InvalidLetterTileInsertionException(insertedLetterTile.LetterValue.ToString());
            }

            this.letterTileSet.Add(insertedLetterTile);
        }

        /// <summary>
        /// Initialize the letterTileSet to the standard set of Scrabble letter tiles.
        /// </summary>
        private void InitializeLetterTiles()
        {
            // Add 2 blank tiles (0 points each)
            LetterTile lt = new LetterTile(' ', 0);
            this.letterTilePointValues.Add(' ', 0);
            this.letterTileMaxQuantities.Add(' ', 2);
            for (int i = 0; i < 2; ++i)
            {
                this.letterTileSet.Add(lt);
            }

            // Add 9 'A' tiles (1 point each).
            lt = new LetterTile('A', 1);
            this.letterTilePointValues.Add('A', 1);
            this.letterTileMaxQuantities.Add('A', 9);
            for (int i = 0; i < 9; ++i)
            {
                this.letterTileSet.Add(lt);
            }

            // Add 2 'B' tiles (3 points each).
            lt = new LetterTile('B', 3);
            this.letterTilePointValues.Add('B', 3);
            this.letterTileMaxQuantities.Add('B', 2);
            for (int i = 0; i < 2; ++i)
            {
                this.letterTileSet.Add(lt);
            }

            // Add 2 'C' tiles (3 points each).
            lt = new LetterTile('C', 3);
            this.letterTilePointValues.Add('C', 3);
            this.letterTileMaxQuantities.Add('C', 2);
            for (int i = 0; i < 2; ++i)
            {
                this.letterTileSet.Add(lt);
            }

            // Add 4 'D' tiles (2 points each).
            lt = new LetterTile('D', 2);
            this.letterTilePointValues.Add('D', 2);
            this.letterTileMaxQuantities.Add('D', 4);
            for (int i = 0; i < 4; ++i)
            {
                this.letterTileSet.Add(lt);
            }

            // Add 12 'E' tiles (1 point each).
            lt = new LetterTile('E', 1);
            this.letterTilePointValues.Add('E', 1);
            this.letterTileMaxQuantities.Add('E', 12);
            for (int i = 0; i < 12; ++i)
            {
                this.letterTileSet.Add(lt);
            }

            // Add 2 'F' tiles (4 points each).
            lt = new LetterTile('F', 4);
            this.letterTilePointValues.Add('F', 4);
            this.letterTileMaxQuantities.Add('F', 2);
            for (int i = 0; i < 2; ++i)
            {
                this.letterTileSet.Add(lt);
            }

            // Add 3 'G' tiles (2 points each).
            lt = new LetterTile('G', 2);
            this.letterTilePointValues.Add('G', 2);
            this.letterTileMaxQuantities.Add('G', 3);
            for (int i = 0; i < 3; ++i)
            {
                this.letterTileSet.Add(lt);
            }

            // Add 2 'H' tiles (4 points each).
            lt = new LetterTile('H', 4);
            this.letterTilePointValues.Add('H', 4);
            this.letterTileMaxQuantities.Add('H', 2);
            for (int i = 0; i < 2; ++i)
            {
                this.letterTileSet.Add(lt);
            }

            // Add 9 'I' tiles (1 point each).
            lt = new LetterTile('I', 1);
            this.letterTilePointValues.Add('I', 1);
            this.letterTileMaxQuantities.Add('I', 9);
            for (int i = 0; i < 9; ++i)
            {
                this.letterTileSet.Add(lt);
            }

            // Add 1 'J' tile (8 points each).
            lt = new LetterTile('J', 8);
            this.letterTilePointValues.Add('J', 8);
            this.letterTileMaxQuantities.Add('J', 1);
            for (int i = 0; i < 1; ++i)
            {
                this.letterTileSet.Add(lt);
            }

            // Add 1 'K' tile (5 points each).
            lt = new LetterTile('K', 5);
            this.letterTilePointValues.Add('K', 5);
            this.letterTileMaxQuantities.Add('K', 1);
            for (int i = 0; i < 1; ++i)
            {
                this.letterTileSet.Add(lt);
            }

            // Add 4 'L' tiles (1 points each).
            lt = new LetterTile('L', 1);
            this.letterTilePointValues.Add('L', 1);
            this.letterTileMaxQuantities.Add('L', 4);
            for (int i = 0; i < 4; ++i)
            {
                this.letterTileSet.Add(lt);
            }

            // Add 2 'M' tiles (3 points each).
            lt = new LetterTile('M', 3);
            this.letterTilePointValues.Add('M', 3);
            this.letterTileMaxQuantities.Add('M', 2);
            for (int i = 0; i < 2; ++i)
            {
                this.letterTileSet.Add(lt);
            }

            // Add 6 'N' tiles (1 points each).
            lt = new LetterTile('N', 1);
            this.letterTilePointValues.Add('N', 1);
            this.letterTileMaxQuantities.Add('N', 6);
            for (int i = 0; i < 6; ++i)
            {
                this.letterTileSet.Add(lt);
            }

            // Add 8 'O' tiles (1 points each).
            lt = new LetterTile('O', 1);
            this.letterTilePointValues.Add('O', 1);
            this.letterTileMaxQuantities.Add('O', 8);
            for (int i = 0; i < 8; ++i)
            {
                this.letterTileSet.Add(lt);
            }

            // Add 2 'P' tiles (3 points each).
            lt = new LetterTile('P', 3);
            this.letterTilePointValues.Add('P', 3);
            this.letterTileMaxQuantities.Add('P', 2);
            for (int i = 0; i < 2; ++i)
            {
                this.letterTileSet.Add(lt);
            }

            // Add 1 'Q' tile (10 points each).
            lt = new LetterTile('Q', 10);
            this.letterTilePointValues.Add('Q', 10);
            this.letterTileMaxQuantities.Add('Q', 1);
            for (int i = 0; i < 1; ++i)
            {
                this.letterTileSet.Add(lt);
            }

            // Add 6 'R' tiles (1 points each).
            lt = new LetterTile('R', 1);
            this.letterTilePointValues.Add('R', 1);
            this.letterTileMaxQuantities.Add('R', 6);
            for (int i = 0; i < 6; ++i)
            {
                this.letterTileSet.Add(lt);
            }

            // Add 4 'S' tiles (1 points each).
            lt = new LetterTile('S', 1);
            this.letterTilePointValues.Add('S', 1);
            this.letterTileMaxQuantities.Add('S', 4);
            for (int i = 0; i < 4; ++i)
            {
                this.letterTileSet.Add(lt);
            }

            // Add 6 'T' tiles (1 points each).
            lt = new LetterTile('T', 1);
            this.letterTilePointValues.Add('T', 1);
            this.letterTileMaxQuantities.Add('T', 6);
            for (int i = 0; i < 6; ++i)
            {
                this.letterTileSet.Add(lt);
            }

            // Add 4 'U' tiles (1 points each).
            lt = new LetterTile('U', 1);
            this.letterTilePointValues.Add('U', 1);
            this.letterTileMaxQuantities.Add('U', 4);
            for (int i = 0; i < 4; ++i)
            {
                this.letterTileSet.Add(lt);
            }

            // Add 2 'V' tiles (4 points each).
            lt = new LetterTile('V', 4);
            this.letterTilePointValues.Add('V', 4);
            this.letterTileMaxQuantities.Add('V', 2);
            for (int i = 0; i < 2; ++i)
            {
                this.letterTileSet.Add(lt);
            }

            // Add 2 'W' tiles (4 points each).
            lt = new LetterTile('W', 4);
            this.letterTilePointValues.Add('W', 4);
            this.letterTileMaxQuantities.Add('W', 2);
            for (int i = 0; i < 2; ++i)
            {
                this.letterTileSet.Add(lt);
            }

            // Add 1 'X' tile (8 points each).
            lt = new LetterTile('X', 8);
            this.letterTilePointValues.Add('X', 8);
            this.letterTileMaxQuantities.Add('X', 1);
            for (int i = 0; i < 1; ++i)
            {
                this.letterTileSet.Add(lt);
            }

            // Add 2 'Y' tiles (4 points each).
            lt = new LetterTile('Y', 4);
            this.letterTilePointValues.Add('Y', 4);
            this.letterTileMaxQuantities.Add('Y', 2);
            for (int i = 0; i < 2; ++i)
            {
                this.letterTileSet.Add(lt);
            }

            // Add 1 'Z' tile (10 points each).
            lt = new LetterTile('Z', 10);
            this.letterTilePointValues.Add('Z', 10);
            this.letterTileMaxQuantities.Add('Z', 1);
            for (int i = 0; i < 1; ++i)
            {
                this.letterTileSet.Add(lt);
            }
        }

        /// <summary>
        /// Randomize the order of this.letterTileSet.
        /// </summary>
        private void MixLetters()
        {
            Random rng = new Random();
            for (int i = 0; i < this.LetterTileCount; ++i)
            {
                int randomIndex = rng.Next() % this.LetterTileCount;
                LetterTile temp = this.letterTileSet[i];
                this.letterTileSet[i] = this.letterTileSet[randomIndex];
                this.letterTileSet[randomIndex] = temp;
            }
        }

        /// <summary>
        /// An exception of this class type means that the there was an error removing a LetterTile from the bag
        /// (e.g. because the bag was empty).
        /// </summary>
        public class InvalidLetterTileRemovalException : Exception
        {
            /// <summary>
            /// A string that contains the details of why an InvalidLetterTileRemovalException exception was thrown.
            /// </summary>
            private string message;

            /// <summary>
            /// Initializes a new instance of the <see cref="InvalidLetterTileRemovalException"/> class.
            /// </summary>
            public InvalidLetterTileRemovalException()
            {
                this.message = "InvalidLetterTileRemoval: A LetterTile was incorrectly removed from a Bag.";
            }

            /// <summary>
            /// Initializes a new instance of the <see cref="InvalidLetterTileRemovalException"/> class.
            /// </summary>
            /// <param name="message">The private member message will be set to message.</param>
            public InvalidLetterTileRemovalException(string message)
            {
                this.message = "InvalidLetterTileRemoval: " + message;
            }

            /// <summary>
            /// Returns a string of the details of why an InvalidLetterTileRemovalException exception was thrown.
            /// </summary>
            /// <returns>A string of exception details.</returns>
            public override string ToString()
            {
                return this.message;
            }
        }

        /// <summary>
        /// An exception of this class type means that an attempt was made to insert an invalid LetterTile into the bag.
        /// </summary>
        public class InvalidLetterTileInsertionException : Exception
        {
            /// <summary>
            /// A string that contains the details of why an InvalidLetterTileInsertionException exception was thrown.
            /// </summary>
            private string message;

            /// <summary>
            /// Initializes a new instance of the <see cref="InvalidLetterTileInsertionException"/> class.
            /// </summary>
            public InvalidLetterTileInsertionException()
            {
                this.message = "InvalidLetterTileInsertion: A LetterTile was incorrectly inserted into a Bag.";
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
            /// Returns a string of the details of why an InvalidLetterTileInsertionException exception was thrown.
            /// </summary>
            /// <returns>A string of exception details.</returns>
            public override string ToString()
            {
                return this.message;
            }
        }
    }
}
