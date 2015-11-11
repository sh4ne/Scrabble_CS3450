//-----------------------------------------------------------------------
// <copyright file="LetterTile.cs" company="Scrabble Project Developers">
//     Copyright (c) Scrabble Project Developers. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Scrabble.Game_Objects;


namespace Scrabble
{
    /// <summary>
    /// This class is responsible for holding data for a play after it has been selected by the user
    /// throws VaryingParallelListLength exception in initializer if all Lists sent into it are not the same length
    /// </summary>
    public class Play
    {
        /// <summary>
        /// a list of letterTiles objects for the current play
        /// which is a parallel list with xCoordinates and 
        /// yCoordinates
        /// </summary>
        private List<LetterTile> letterTiles;
        /// <summary>
        /// a list of x coordinates for the current play
        /// which is a parallel list with yCoordinates and
        /// letterTiles
        /// </summary>
        private List<int> xCoordinates;
        /// <summary>
        /// a list of y coordinates for the current play
        /// which is a parallel list with xCoordinates and
        /// letterTiles
        /// </summary>    
        private List<int> yCoordinates;
        /// <summary>
        /// the playerID from an instance of Player which is
        /// making the current play
        /// </summary>
        private int playerID;

        /// <summary>
        /// Initializes a new instance of the <see cref="LetterTile"/> class.
        /// </summary>
        /// <param name="letterTiles">The letterTiles member will be set to this value.</param>
        /// <param name="xCoordinates">The xCoordinates member will be set to this value.</param>
        /// <param name="yCoordinates">The yCoordinates member will be set to this value.</param>
        /// <param name="playerID">The playerID member will be set to this value.</param>
        public Play (List<LetterTile> letterTiles, 
            List<int> xCoordinates, 
            List<int> yCoordinates,
            int playerID)
        {
            if(letterTiles.Count == xCoordinates.Count && xCoordinates.Count == yCoordinates.Count)
            {
                this.letterTiles = letterTiles;
                this.xCoordinates = xCoordinates;
                this.yCoordinates = yCoordinates;
                this.playerID = playerID;
            }
            else
            {
                throw new VaryingParallelListLength("arguments for letterTiles, xCoordinates, and yCoordinates have varying lengths");
            }
        }

        /// <summary>
        /// returns playerID member.
        /// </summary>
        public int GetPlayerID()
        {
            return playerID;
        }

        /// <summary>
        /// returns playerID member.
        /// </summary>
        /// <param name="index">The index at this value of xCoordinates member will be returned.</param>
        public int GetXCoordinate(int index)
        {
            return xCoordinates[index];
        }

        /// <summary>
        /// returns playerID member.
        /// </summary>
        /// <param name="index">The index at this value of yCoordinates will be returned.</param>
        public int GetYCoordinate(int index)
        {
            return yCoordinates[index];
        }

        /// <summary>
        /// returns playerID member.
        /// </summary>
        /// <param name="index">The index at this value of letterTiles member will be returned.</param>
        public LetterTile GetLetterTile(int index)
        {
            return letterTiles[index];
        }

        /// <summary>
        /// returns playerID member.
        /// </summary>
        /// <returns>The length of the parallel list with xCoordinates, yCoordinates, and letterTiles.</returns>
        public int GetParallelListLength ()
        {
            return letterTiles.Count;
        }

    }

    /// <summary>
    /// An exception of this class type means that the value of pointValue was invalid.
    /// </summary>
    public class VaryingParallelListLength : Exception
    {
        /// <summary>
        /// A string that contains the details of why an VaryingParallelListLength exception was thrown.
        /// </summary>
        private string message;

        /// <summary>
        /// Initializes a new instance of the <see cref="VaryingParallelListLength"/> class.
        /// </summary>
        public VaryingParallelListLength()
        {
            this.message = "VaryingParallelListLength: Lists which were meant to be parallel were given different lengths.";
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="VaryingParallelListLength"/> class.
        /// </summary>
        /// <param name="message">The private member message will be set to message.</param>
        public VaryingParallelListLength(string message)
        {
            this.message = "VaryingParallelListLength: " + message;
        }

        /// <summary>
        /// Returns a string of the details of why an VaryingParallelListLength exception was thrown.
        /// </summary>
        /// <returns>A string of exception details.</returns>
        public override string ToString()
        {
            return this.message;
        }
    }
}
