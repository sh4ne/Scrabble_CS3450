//-----------------------------------------------------------------------
// <copyright file="Play.cs" company="Scrabble Project Developers">
//     Copyright (c) Scrabble Project Developers. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Scrabble
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using global::Scrabble.Game_Objects;

    /// <summary>
    /// This class is responsible for holding data for a play after it has been selected by the user
    /// throws VaryingParallelListLength exception in initializer if all Lists sent into it are not the same length
    /// </summary>
    public class Play
    {
        /// <summary>
        /// Defines the axis of tile placements in a play
        /// </summary>
        public enum TileAxis { Horizontal, Vertical, None, Invalid };

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
        private List<int> coordinatesX;
        
        /// <summary>
        /// a list of y coordinates for the current play
        /// which is a parallel list with xCoordinates and
        /// letterTiles
        /// </summary>    
        private List<int> coordinatesY;
        
        /// <summary>
        /// the playerID from an instance of Player which is
        /// making the current play
        /// </summary>
        private int playerID;

        /// <summary>
        /// the current axis of tiles in play
        /// </summary>
        private TileAxis axis;

        /// <summary>
        /// Initializes a new instance of the <see cref="Play"/> class.
        /// </summary>
        /// <param name="letterTiles">The letterTiles member will be set to this value.</param>
        /// <param name="coordinatesX">The xCoordinates member will be set to this value.</param>
        /// <param name="coordinatesY">The yCoordinates member will be set to this value.</param>
        /// <param name="playerID">The playerID member will be set to this value.</param>
        public Play(
            List<LetterTile> letterTiles, 
            List<int> coordinatesX, 
            List<int> coordinatesY,
            int playerID)
        {
            if (!(letterTiles.Count == coordinatesX.Count && coordinatesX.Count == coordinatesY.Count))
            {
                throw new VaryingParallelListLength();
            }
            if (coordinatesX.Count <= 0 && coordinatesY.Count <= 0)
            {
                throw new EmptyParrallelList();
            }
            
            if (coordinatesX.Min() < 0 || coordinatesY.Min() < 0 || coordinatesX.Max() >= 15 || coordinatesY.Max() >= 15)
            {
                throw new OutOfBoardBoundaries();
            }

            // iterate the list and add all coords as a string of coordinates seperated by a space.
            IEnumerable<string> coords = coordinatesX.Select((el, index) => coordinatesX[index] + " " + coordinatesY[index]);

            // check if all the coords are unique/distinct
            if (coords.Distinct().Count() != coords.Count())
            {
                throw new DuplicateCoords();
            }

            this.letterTiles = letterTiles;
            this.playerID = playerID;
            this.coordinatesX = coordinatesX;
            this.coordinatesY = coordinatesY;

            SetAxis();

            if(this.axis == TileAxis.Invalid)
            {
                throw new InvalidAxis();
            }
                
        }

        /// <summary>
        /// sets the TileAxis of play.
        /// </summary>
        private void SetAxis ()
        {
            int lastX = this.coordinatesX[0], lastY = this.coordinatesY[0], changeInX, changeInY;
            bool first = true;
            var axis = TileAxis.None;
            for (int i = 0; i < coordinatesX.Count; ++i)
            {
                int currentX = this.coordinatesX[i],
                    currentY = this.coordinatesY[i];
                if (!first)
                {
                    changeInX = currentX - lastX;
                    changeInY = currentY - lastY;
                    if(changeInX == 0)
                    {
                        if (changeInY != 0)
                        {
                            if (axis == TileAxis.Vertical)
                            {
                                continue;
                            }
                            else if (axis == TileAxis.None)
                            {
                                axis = TileAxis.Vertical;
                            }
                            else
                            {
                                axis = TileAxis.Invalid;
                                break;
                            }
                        }
                        
                    } 
                    else if (changeInY == 0)
                    {
                        if(changeInX != 0)
                        {
                            if (axis == TileAxis.Horizontal)
                            {
                                continue;
                            }
                            else if (axis == TileAxis.None)
                            {
                                axis = TileAxis.Horizontal;
                            }
                            else
                            {
                                axis = TileAxis.Invalid;
                                break;
                            }
                        }
                    }
                    else
                    {
                        axis = TileAxis.Invalid;
                    }
                }
                else
                {
                    first = false;
                }

                lastX = currentX;
                lastY = currentY;
            }
            this.axis = axis;
        }

        /// <summary>
        /// returns playerID member.
        /// </summary>
        /// <returns>The playerID for the player making the move.</returns>
        public int GetPlayerID()
        {
            return this.playerID;
        }

        /// <summary>
        /// returns single value from coordinatesX member.
        /// </summary>
        /// <param name="index">The index at this value of coordinatesX member will be returned.</param>
        /// <returns>The index at the sent in value of coordinatesX member will be returned</returns>
        public int GetCoordinateX(int index)
        {
            return this.coordinatesX[index];
        }

        /// <summary>
        /// returns single value from coordinatesY member.
        /// </summary>
        /// <param name="index">The index at this value of coordinatesY will be returned.</param>
        /// <returns>The index at the sent in value of coordinatesY member will be returned</returns>
        public int GetCoordinateY(int index)
        {
            return this.coordinatesY[index];
        }

        /// <summary>
        /// returns single value from letterTile member.
        /// </summary>
        /// <param name="index">The index at this value of letterTiles member will be returned.</param>
        /// <returns>The index at the sent in value of letterTiles member will be returned</returns>
        public LetterTile GetLetterTile(int index)
        {
            return this.letterTiles[index];
        }

        /// <summary>
        /// returns the length of the parallel list with xCoordinates, yCoordinates, and letterTiles.
        /// </summary>
        /// <returns>The length of the parallel list with xCoordinates, yCoordinates, and letterTiles.</returns>
        public int GetParallelListLength()
        {
            return this.letterTiles.Count;
        }

        /// <summary>
        /// get the current axis of tiles in play
        /// </summary>
        public TileAxis GetTileAxis()
        {
            return this.axis;
        }

        /// <summary>
        /// An exception of this class type means one or more of the parallel lists (letterTiles, xCoordinates, and yCoordinates) does not match the other parallel lists.
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
                this.message = "VaryingParallelListLength: arguments for parallel lists letterTiles, xCoordinates, and yCoordinates have varying lengths.";
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

        /// <summary>
        /// An exception of this class type means one or more of the parallel lists (letterTiles, xCoordinates, and yCoordinates) are empty with length 0.
        /// </summary>
        public class EmptyParrallelList : Exception
        {
            /// <summary>
            /// A string that contains the details of why an EmptyParrallelList exception was thrown.
            /// </summary>
            private string message;

            /// <summary>
            /// Initializes a new instance of the <see cref="EmptyParrallelList"/> class.
            /// </summary>
            public EmptyParrallelList()
            {
                this.message = "EmptyParallelList: parallel lists letterTiles, xCoordinates, and yCoordinates are empty (length 0).";
            }

            /// <summary>
            /// Returns a string of the details of why an EmptyParrallelList exception was thrown.
            /// </summary>
            /// <returns>A string of exception details.</returns>
            public override string ToString()
            {
                return this.message;
            }
        }

        /// <summary>
        /// An exception of this class type means one or more elements of the lists xCoordinates, or yCoordinates are either less than 0 or greater than 14.
        /// </summary>
        public class OutOfBoardBoundaries : Exception
        {
            /// <summary>
            /// A string that contains the details of why an OutOfBoardBoundaries exception was thrown.
            /// </summary>
            private string message;

            /// <summary>
            /// Initializes a new instance of the <see cref="OutOfBoardBoundaries"/> class.
            /// </summary>
            public OutOfBoardBoundaries()
            {
                this.message = "EmptyParallelList: one or more elements of lists for xCoordinates, and/or yCoordinates are either less than 0 or greater than 14";
            }

            /// <summary>
            /// Returns a string of the details of why an OutOfBoardBoundaries exception was thrown.
            /// </summary>
            /// <returns>A string of exception details.</returns>
            public override string ToString()
            {
                return this.message;
            }
        }

        /// <summary>
        /// An exception of this class type means the letterTiles with given x and y coordinates are not aligned horizontally or vertically.
        /// </summary>
        public class InvalidAxis : Exception
        {
            /// <summary>
            /// A string that contains the details of why an InvalidAxis exception was thrown.
            /// </summary>
            private string message;

            /// <summary>
            /// Initializes a new instance of the <see cref="InvalidAxis"/> class.
            /// </summary>
            public InvalidAxis()
            {
                this.message = "InvalidAxis: one or more combinations of same indexed elements of lists for xCoordinates, and yCoordinates are not aligned horizontally or vertically with the rest of the elements.";
            }

            /// <summary>
            /// Returns a string of the details of why an InvalidAxis exception was thrown.
            /// </summary>
            /// <returns>A string of exception details.</returns>
            public override string ToString()
            {
                return this.message;
            }
        }

        /// <summary>
        /// An exception of this class type means one or more same index combinations of lists for xCoordinates, and yCoordinates are the same combination.
        /// </summary>
        public class DuplicateCoords : Exception
        {
            /// <summary>
            /// A string that contains the details of why an DuplicateCoords exception was thrown.
            /// </summary>
            private string message;

            /// <summary>
            /// Initializes a new instance of the <see cref="DuplicateCoords"/> class.
            /// </summary>
            public DuplicateCoords()
            {
                this.message = "DuplicateCoords: one or more same index combinations of lists for xCoordinates, and yCoordinates are the same combination";
            }

            /// <summary>
            /// Returns a string of the details of why an DuplicateCoords exception was thrown.
            /// </summary>
            /// <returns>A string of exception details.</returns>
            public override string ToString()
            {
                return this.message;
            }
        }
    }
}
