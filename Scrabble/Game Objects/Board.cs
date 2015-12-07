//-----------------------------------------------------------------------
// <copyright file="Board.cs" company="Scrabble Project Developers">
//     Copyright (c) Scrabble Project Developers. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Scrabble.Game_Objects
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// The board is a 15x15 grid of <see cref="GameBoardSquare"/>s to which plays can be added.
    /// </summary>
    public class Board
    {
        /// <summary>
        /// A list of <see cref="GameBoardSquare"/>s.
        /// </summary>
        private List<GameBoardSquare> squaresList;

        /// <summary>
        /// A 15x15 grid made up of the squares in squaresList.
        /// </summary>
        private GameBoardSquareGrid boardGrid;

        /// <summary>
        /// The last play to be added to the board.
        /// </summary>
        private Play lastPlay;

        /// <summary>
        /// Is true if the last play is not on the board (or if there has been no last play);
        /// </summary>
        private bool lastPlayIsNull;

        /// <summary>
        /// Initializes a new instance of the <see cref="Board" /> class.
        /// </summary>
        public Board()
        {
            this.squaresList = new List<GameBoardSquare>();

            for (int y = 0; y < 15; ++y)
            {
                for (int x = 0; x < 15; ++x)
                {
                    this.squaresList.Add(new GameBoardSquare(x, y, this.GetWordMultiplier(x, y), this.GetLetterMultiplier(x, y)));
                }
            }

            this.boardGrid = new GameBoardSquareGrid(this.squaresList);
            this.lastPlayIsNull = true;
        }

        /// <summary>
        /// Tells whether a <see cref="Play"/> can legally be placed onto the board.
        /// </summary>
        /// <param name="play">The <see cref="Play"/> being checked.</param>
        /// <returns>True if play can be added to the board. Returns false otherwise.</returns>
        public bool PlayIsValid(Play play)
        {
            if (play.GetParallelListLength() == 0)
            {
                // Cuz that's kinda pointless.
                return false;
            }

            if (play.GetParallelListLength() > 7)
            {
                // Yeah . . . no.
                return false;
            }

            if (play.GetParallelListLength() == 1 && this.boardGrid[7, 7].IsEmpty())
            {
                // The first play has to have at least 2 letters in it.
                return false;
            }

            // Make sure the first play puts a LetterTile into the central square.
            if (this.boardGrid[7, 7].IsEmpty())
            {
                bool putsTileInCentralSquare = false;
                for (int i = 0; i < play.GetParallelListLength(); ++i)
                {
                    if (play.GetCoordinateX(i) == 7 && play.GetCoordinateY(i) == 7)
                    {
                        putsTileInCentralSquare = true;
                    }
                }

                if (!putsTileInCentralSquare)
                {
                    return false;
                }
            }

            bool allXCoordinatesAreTheSame = true;
            bool allYCoordinatesAreTheSame = true;

            int coordX = play.GetCoordinateX(0);
            int coordY = play.GetCoordinateY(0);

            // Make sure that all LetterTiles are in either the same row or in the same column.
            for (int i = 0; i < play.GetParallelListLength(); ++i)
            {
                if (coordX != play.GetCoordinateX(i))
                {
                    allXCoordinatesAreTheSame = false;
                }

                if (coordY != play.GetCoordinateY(i))
                {
                    allYCoordinatesAreTheSame = false;
                }
            }

            // Make sure that there are no duplicates in the play.
            if (allXCoordinatesAreTheSame)
            {
                for (int i = 0; i < play.GetParallelListLength(); ++i)
                {
                    int numberOfMatches = 0;
                    for (int j = 0; j < play.GetParallelListLength(); ++j)
                    {
                        if (play.GetCoordinateY(i) == play.GetCoordinateY(j))
                        {
                            ++numberOfMatches;
                        }
                    }

                    if (numberOfMatches != 1)
                    {
                        return false;
                    }
                    else
                    {
                        numberOfMatches = 0;
                    }
                }
            }
            else
            {
                for (int i = 0; i < play.GetParallelListLength(); ++i)
                {
                    int numberOfMatches = 0;
                    for (int j = 0; j < play.GetParallelListLength(); ++j)
                    {
                        if (play.GetCoordinateX(i) == play.GetCoordinateX(j))
                        {
                            ++numberOfMatches;
                        }
                    }

                    if (numberOfMatches != 1)
                    {
                        return false;
                    }
                    else
                    {
                        numberOfMatches = 0;
                    }
                }
            }

            if (!((allXCoordinatesAreTheSame ^ allYCoordinatesAreTheSame) || play.GetParallelListLength() == 1))
            {
                return false;
            }

            // Make sure that no letters are being inserted into where there are already letters.
            if (allXCoordinatesAreTheSame)
            {
                for (int i = 0; i < play.GetParallelListLength(); ++i)
                {
                    if (!this.boardGrid[coordX, play.GetCoordinateY(i)].IsEmpty())
                    {
                        return false;
                    }
                }
            }

            if (allYCoordinatesAreTheSame)
            {
                for (int i = 0; i < play.GetParallelListLength(); ++i)
                {
                    if (!this.boardGrid[play.GetCoordinateX(i), coordY].IsEmpty())
                    {
                        return false;
                    }
                }
            }

            // Make sure that any gaps between the letters in the play are filled by letters that are already on the board.
            if (allXCoordinatesAreTheSame)
            {
                List<int> coordsY = new List<int>();
                for (int i = 0; i < play.GetParallelListLength(); ++i)
                {
                    coordsY.Add(play.GetCoordinateY(i));
                }

                coordsY.Sort();

                int prevY = coordsY[0];
                for (int i = 1; i < play.GetParallelListLength(); ++i)
                {
                    while (coordsY[i] != prevY + 1)
                    {
                        if (this.boardGrid[coordX, prevY + 1].IsEmpty())
                        {
                            return false;
                        }

                        ++prevY;
                    }

                    ++prevY;
                }
            }

            if (allYCoordinatesAreTheSame)
            {
                List<int> coordsX = new List<int>();
                for (int i = 0; i < play.GetParallelListLength(); ++i)
                {
                    coordsX.Add(play.GetCoordinateX(i)); 
                }

                coordsX.Sort();

                int prevX = coordsX[0];
                for (int i = 1; i < play.GetParallelListLength(); ++i)
                {
                    while (coordsX[i] != prevX + 1)
                    {
                        if (this.boardGrid[prevX + 1, coordY].IsEmpty())
                        {
                            return false;
                        }

                        ++prevX;
                    }

                    ++prevX;
                }
            }

            // Make sure that at least one of the letters in the play is adjacent to a letter on the board,
            // if a play has been added to the board.
            if (!this.boardGrid[7, 7].IsEmpty())
            {
                bool playIsAdjacentToSomething = false;
                for (int i = 0; i < play.GetParallelListLength(); ++i)
                {
                    int x = play.GetCoordinateX(i);
                    int y = play.GetCoordinateY(i);

                    // Check down.
                    if (y < 14 && !this.boardGrid[x, y + 1].IsEmpty())
                    {
                        playIsAdjacentToSomething = true;
                        break;
                    }

                    // Check up.
                    if (y > 0 && !this.boardGrid[x, y - 1].IsEmpty())
                    {
                        playIsAdjacentToSomething = true;
                        break;
                    }

                    // Check left.
                    if (x > 0 && !this.boardGrid[x - 1, y].IsEmpty())
                    {
                        playIsAdjacentToSomething = true;
                        break;
                    }

                    // Check right.
                    if (x < 14 && !this.boardGrid[x + 1, y].IsEmpty())
                    {
                        playIsAdjacentToSomething = true;
                        break;
                    }
                }

                return playIsAdjacentToSomething;
            }

            return true;
        }

        /// <summary>
        /// Adds a play to <see cref="squaresList"/>. The play is not scored.
        /// </summary>
        /// <param name="play">The <see cref="Play"/> to be added to the <see cref="Board"/>.</param>
        public void AddPlayToBoard(Play play)
        {
            if (!this.PlayIsValid(play))
            {
                throw new InvalidPlayException();
            }

            for (int i = 0; i < play.GetParallelListLength(); ++i)
            {
                this.boardGrid[play.GetCoordinateX(i), play.GetCoordinateY(i)].InsertLetterTile(play.GetLetterTile(i));
            }

            this.lastPlay = play;
            this.lastPlayIsNull = false;
        }

        /// <summary>
        /// Remove the most recent play from the board, and return its LetterTiles.
        /// </summary>
        /// <returns>The LetterTiles from the most recent play to be added to the board.</returns>
        public List<LetterTile> RemoveLastPlay()
        {
            List<LetterTile> removedLetters = new List<LetterTile>();
            if (this.lastPlayIsNull)
            {
                return removedLetters;
            }
            else
            {
                this.lastPlayIsNull = true;
                for (int i = 0; i < this.lastPlay.GetParallelListLength(); ++i)
                {
                    removedLetters.Add(this.boardGrid[this.lastPlay.GetCoordinateX(i), this.lastPlay.GetCoordinateY(i)].RemoveLetterTile());
                }

                return removedLetters;
            }
        }

        /// <summary>
        /// Scores a <see cref="Play"/> that was already added to the board.
        /// </summary>
        /// <param name="play">The <see cref="Play"/> to be scored.</param>
        /// <param name="shouldRemoveMultipliers">Tells whether the <see cref="GameBoardSquare"/>s which are being filled by
        /// the <see cref="LetterTile"/>s in play should have their score multipliers set to 1.</param>
        /// <returns>The score of a <see cref="Play"/> that was already added to the board.</returns>
        public int ScorePlay(Play play, bool shouldRemoveMultipliers)
        {
            // Make sure the play is already on the board.
            for (int i = 0; i < play.GetParallelListLength(); ++i)
            {
                int x = play.GetCoordinateX(i);
                int y = play.GetCoordinateY(i);
                
                if (!this.boardGrid[x, y].ContainedLetterTile.Equals(play.GetLetterTile(i))) 
                {
                    throw new InvalidPlayException();
                }
            }

            int totalPoints = 0;

            // If the play consists of only one LetterTile, score vertically and horizontally around it.
            if (play.GetParallelListLength() == 1)
            {
                totalPoints += this.ScoreOneHorizontalWord(play.GetCoordinateX(0), play.GetCoordinateY(0));
                totalPoints += this.ScoreOneVerticalWord(play.GetCoordinateX(0), play.GetCoordinateY(0));
            }
            else if (play.GetCoordinateY(0) == play.GetCoordinateY(1))
            {
                // Otherwise, if the play is a horizontal play, score all of the vertical words that branch off from that
                // play, and then score the horizontal word in play.
                int horizontalWordMultiplier = 1;
                int horizontalWordPoints = 0;

                for (int i = 0; i < play.GetParallelListLength(); ++i)
                {
                    horizontalWordMultiplier *= this.boardGrid[play.GetCoordinateX(i), play.GetCoordinateY(i)].WordMultiplier;
                }

                for (int i = 0; i < play.GetParallelListLength(); ++i)
                {
                    totalPoints += this.ScoreOneVerticalWord(play.GetCoordinateX(i), play.GetCoordinateY(i));
                    horizontalWordPoints += this.ScoreOneTile(play.GetCoordinateX(i), play.GetCoordinateY(i));
                }

                // Get the points from the adjacent squares to the left and right of the played letters.
                int minX = play.GetCoordinateX(0);
                int maxX = minX;

                for (int i = 0; i < play.GetParallelListLength(); ++i)
                {
                    if (minX > play.GetCoordinateX(i))
                    {
                        minX = play.GetCoordinateX(i);
                    }

                    if (maxX < play.GetCoordinateX(i))
                    {
                        maxX = play.GetCoordinateX(i);
                    }
                }

                // Go left.
                int count = 1;
                while (true)
                {
                    if (minX - count < 0 || this.boardGrid[minX - count, play.GetCoordinateY(0)].IsEmpty())
                    {
                        break;
                    }
                    else
                    {
                        horizontalWordPoints += this.ScoreOneTile(minX - count, play.GetCoordinateY(0));
                        ++count;
                    }
                }

                // Go right.
                count = 1;
                while (true)
                {
                    if (maxX + count > 14 || this.boardGrid[maxX + count, play.GetCoordinateY(0)].IsEmpty())
                    {
                        break;
                    }
                    else
                    {
                        horizontalWordPoints += this.ScoreOneTile(maxX + count, play.GetCoordinateY(0));
                        ++count;
                    }
                }

                // Check for gaps between letters in the play, and score them.
                List<int> coordsX = new List<int>();
                for (int i = 0; i < play.GetParallelListLength(); ++i)
                {
                    coordsX.Add(play.GetCoordinateX(i));
                }

                coordsX.Sort();
                count = 1;
                for (int i = 0; i < coordsX.Count - 1; ++i)
                {
                    while (coordsX[i] + count < coordsX[i + 1])
                    {
                        horizontalWordPoints += this.ScoreOneTile(coordsX[i] + count, play.GetCoordinateY(0));
                        ++count;
                    }

                    count = 1;
                }

                horizontalWordPoints *= horizontalWordMultiplier;
                totalPoints += horizontalWordPoints;
            }
            else
            {
                // Otherwise, if the play is a vertical play, score all of the horizontal words that branch off from that
                // play, and then score the vertical word in play.
                int verticalWordMultiplier = 1;
                int verticalWordPoints = 0;

                for (int i = 0; i < play.GetParallelListLength(); ++i)
                {
                    verticalWordMultiplier *= this.boardGrid[play.GetCoordinateX(i), play.GetCoordinateY(i)].WordMultiplier;
                }

                for (int i = 0; i < play.GetParallelListLength(); ++i)
                {
                    totalPoints += this.ScoreOneHorizontalWord(play.GetCoordinateX(i), play.GetCoordinateY(i));
                    verticalWordPoints += this.ScoreOneTile(play.GetCoordinateX(i), play.GetCoordinateY(i));
                }

                // Get the points from the adjacent squares above and below the played letters.
                int minY = play.GetCoordinateY(0);
                int maxY = minY;

                for (int i = 0; i < play.GetParallelListLength(); ++i)
                {
                    if (minY > play.GetCoordinateY(i))
                    {
                        minY = play.GetCoordinateY(i);
                    }

                    if (maxY < play.GetCoordinateY(i))
                    {
                        maxY = play.GetCoordinateY(i);
                    }
                }

                // Go up.
                int count = 1;
                while (true)
                {
                    if (minY - count < 0 || this.boardGrid[play.GetCoordinateX(0), minY - count].IsEmpty())
                    {
                        break;
                    }
                    else
                    {
                        verticalWordPoints += this.ScoreOneTile(play.GetCoordinateX(0), minY - count);
                        ++count;
                    }
                }

                // Go down.
                count = 1;
                while (true)
                {
                    if (maxY + count > 14 || this.boardGrid[play.GetCoordinateX(0), maxY + count].IsEmpty())
                    {
                        break;
                    }
                    else
                    {
                        verticalWordPoints += this.ScoreOneTile(play.GetCoordinateY(0), maxY + count);
                        ++count;
                    }
                }

                // Check for gaps between letters in the play, and score them.
                List<int> coordsY = new List<int>();
                for (int i = 0; i < play.GetParallelListLength(); ++i)
                {
                    coordsY.Add(play.GetCoordinateY(i));
                }

                coordsY.Sort();
                count = 1;
                for (int i = 0; i < coordsY.Count - 1; ++i)
                {
                    while (coordsY[i] + count < coordsY[i + 1])
                    {
                        verticalWordPoints += this.ScoreOneTile(play.GetCoordinateX(0), coordsY[i] + count);
                        ++count;
                    }

                    count = 1;
                }

                verticalWordPoints *= verticalWordMultiplier;
                totalPoints += verticalWordPoints;
            }

            if (shouldRemoveMultipliers)
            {
                // Clear the word and letter multipliers of the GameBoardSquares in the play.
                for (int i = 0; i < play.GetParallelListLength(); ++i)
                {
                    this.boardGrid[play.GetCoordinateX(i), play.GetCoordinateY(i)].RemoveScoreMultipliers();
                }
            }

            // Give the 50 point bonus for using 7 LetterTiles.
            if (play.GetParallelListLength() == 7)
            {
                totalPoints += 50;
            }

            return totalPoints;
        }

        public List<string> GetWordsInPlay(Play play)
        {
            List<string> words = new List<string>();
            bool isVerticalPlay;

            // Make sure the play is valid.
            //if (!this.PlayIsValid(play))
            //{
            //    throw new InvalidPlayException();
            //}

            // Make sure that all of the LetterTiles in the play are already on the board.
            for (int i = 0; i < play.GetParallelListLength(); ++i)
            {
                if (this.boardGrid[play.GetCoordinateX(i), play.GetCoordinateY(i)].IsEmpty())
                {
                    throw new InvalidPlayException();
                }

                if (!this.boardGrid[play.GetCoordinateX(i), play.GetCoordinateY(i)].ContainedLetterTile.Equals(play.GetLetterTile(i)))
                {
                    throw new InvalidPlayException();
                }
            }

            if (play.GetCoordinateX(0) == play.GetCoordinateX(1))
            {
                isVerticalPlay = true;
            }
            else
            {
                isVerticalPlay = false;
            }

            int x = play.GetCoordinateX(0);
            int y = play.GetCoordinateY(0);

            if (isVerticalPlay)
            {
                int topOfWord = y;
                int midX = x;
                StringBuilder wordBuilder = new StringBuilder();

                // Get to the top of the vertical word.
                while (topOfWord > 0)
                {
                    if (boardGrid[x, topOfWord - 1].IsEmpty())
                    {
                        break;
                    }
                    else
                    {
                        --topOfWord;
                    }
                }

                while (topOfWord <= 14)
                {
                    if (this.boardGrid[x, topOfWord].IsEmpty())
                    {
                        break;
                    }
                    else
                    {
                        wordBuilder.Append(this.boardGrid[x, topOfWord].ContainedLetterTile.LetterValue);
                        ++topOfWord;
                    }
                }

                if (wordBuilder.ToString().Length > 1)
                {
                    words.Add(wordBuilder.ToString());
                }

                wordBuilder.Clear();

                // Get all the words that branch off from that word.
                for (int i = 0; i < play.GetParallelListLength(); ++i)
                {
                    int leftOfWord = play.GetCoordinateX(i);

                    // Go to the left end of the word
                    while (leftOfWord > 0)
                    {
                        if (boardGrid[leftOfWord - 1, play.GetCoordinateY(i)].IsEmpty())
                        {
                            break;
                        }
                        else
                        {
                            --leftOfWord;
                        }
                    }

                    // Read rightward until the word ends.
                    while (leftOfWord <= 14)
                    {
                        if (this.boardGrid[leftOfWord, play.GetCoordinateY(i)].IsEmpty())
                        {
                            break;
                        }
                        else
                        {
                            wordBuilder.Append(this.boardGrid[leftOfWord, play.GetCoordinateY(i)].ContainedLetterTile.LetterValue);
                            ++leftOfWord;
                        }
                    }

                    if (wordBuilder.ToString().Length > 1)
                    {
                        words.Add(wordBuilder.ToString());  
                    }

                    wordBuilder.Clear();
                }
            }
            else
            {
                int leftOfWord = x;
                int midY = y;
                StringBuilder wordBuilder = new StringBuilder();

                // Get to the top of the vertical word.
                while (leftOfWord > 0)
                {
                    if (boardGrid[leftOfWord - 1, y].IsEmpty())
                    {
                        break;
                    }
                    else
                    {
                        --leftOfWord;
                    }
                }

                while (leftOfWord <= 14)
                {
                    if (this.boardGrid[leftOfWord, y].IsEmpty())
                    {
                        break;
                    }
                    else
                    {
                        wordBuilder.Append(this.boardGrid[leftOfWord, y].ContainedLetterTile.LetterValue);
                        ++leftOfWord;
                    }
                }

                if (wordBuilder.ToString().Length > 1)
                {
                    words.Add(wordBuilder.ToString());
                }

                wordBuilder.Clear();

                // Get all the words that branch off from that word.
                for (int i = 0; i < play.GetParallelListLength(); ++i)
                {
                    int topOfWord = play.GetCoordinateY(i);

                    // Go to the left end of the word
                    while (topOfWord > 0)
                    {
                        if (boardGrid[play.GetCoordinateX(i), topOfWord - 1].IsEmpty())
                        {
                            break;
                        }
                        else
                        {
                            --topOfWord;
                        }
                    }

                    // Read rightward until the word ends.
                    while (topOfWord <= 14)
                    {
                        if (this.boardGrid[play.GetCoordinateX(i), topOfWord].IsEmpty())
                        {
                            break;
                        }
                        else
                        {
                            wordBuilder.Append(this.boardGrid[play.GetCoordinateX(i), topOfWord].ContainedLetterTile.LetterValue);
                            ++topOfWord;
                        }
                    }

                    if (wordBuilder.ToString().Length > 1)
                    {
                        words.Add(wordBuilder.ToString());
                    }

                    wordBuilder.Clear();
                }
            }

            return words;
        }

        /// <summary>
        /// Gets the score for a <see cref="LetterTile"/> at a specific 
        /// location on the board.
        /// </summary>
        /// <param name="x">The x coordinate of the <see cref="GameBoardSquare"/> containing the <see cref="LetterTile"/> to be scored.</param>
        /// <param name="y">The y coordinate of the <see cref="GameBoardSquare"/> containing the <see cref="LetterTile"/> to be scored.</param>
        /// <returns>The point value of the <see cref="LetterTile"/> multiplied by the letter multiplier of the <see cref="GameBoardSquare"/> that contains that <see cref="LetterTile"/>.</returns>
        private int ScoreOneTile(int x, int y)
        {
            return this.boardGrid[x, y].LetterMultiplier * this.boardGrid[x, y].ContainedLetterTile.PointValue;
        }

        /// <summary>
        /// Returns the score of the horizontal word that extends to the left and right of <see cref="GameBoardSquare"/> (x, y).
        /// </summary>
        /// <param name="x">The x coordinate of the <see cref="GameBoardSquare"/> which is in the word.</param>
        /// <param name="y">The y coordinate of the <see cref="GameBoardSquare"/> which is in the word.</param>
        /// <returns>The word multiplier of <see cref="GameBoardSquare"/> at (x, y) multiplied by the point 
        /// values of all of the <see cref="GameBoardSquare"/>s to the left and right of the one at location (x, y.)</returns>
        private int ScoreOneHorizontalWord(int x, int y)
        {
            // Make sure that the word is longer than 1 tile.
            int wordMultiplier = this.boardGrid[x, y].WordMultiplier;
            int totalPoints = 0;
            bool shouldScore = false;

            // Score left
            int count = 1;
            while (true)
            {
                if (x - count < 0 || this.boardGrid[x - count, y].IsEmpty())
                {
                    break;
                }

                totalPoints += this.ScoreOneTile(x - count, y);
                shouldScore = true;
                ++count;
            }

            // Score here
            totalPoints += this.ScoreOneTile(x, y);

            // Score right
            count = 1;
            while (true)
            {
                if (x + count > 14 || this.boardGrid[x + count, y].IsEmpty())
                {
                    break;
                }

                totalPoints += this.ScoreOneTile(x + count, y);
                shouldScore = true;
                ++count;
            }

            if (!shouldScore)
            {
                return 0;
            }

            return totalPoints * wordMultiplier;
        }

        /// <summary>
        /// Returns the score of the vertical word that extends above and below <see cref="GameBoardSquare"/> (x, y).
        /// </summary>
        /// <param name="x">The x coordinate of the <see cref="GameBoardSquare"/> which is in the word.</param>
        /// <param name="y">The y coordinate of the <see cref="GameBoardSquare"/> which is in the word.</param>
        /// <returns>The word multiplier of <see cref="GameBoardSquare"/> at (x, y) multiplied by the point 
        /// values of all of the <see cref="GameBoardSquare"/>s above and below the one at location (x, y.)</returns>
        private int ScoreOneVerticalWord(int x, int y)
        {
            int wordMultiplier = this.boardGrid[x, y].WordMultiplier;
            int totalPoints = 0;
            bool shouldScore = false;

            // Score down
            int count = 1;
            while (true)
            {
                if (x - count < 0 || this.boardGrid[x, y - count].IsEmpty())
                {
                    break;
                }

                totalPoints += this.ScoreOneTile(x, y - count);
                shouldScore = true;
                ++count;
            }

            // Score here
            totalPoints += this.ScoreOneTile(x, y);

            // Score up
            count = 1;
            while (true)
            {
                if (x + count > 14 || this.boardGrid[x, y + count].IsEmpty())
                {
                    break;
                }

                totalPoints += this.ScoreOneTile(x, y + count);
                shouldScore = true;
                ++count;
            }

            if (!shouldScore)
            {
                return 0;
            }

            return totalPoints * wordMultiplier;
        }

        /// <summary>
        /// Gets the proper word multiplier for <see cref="GameBoardSquare"/> (x,y).
        /// </summary>
        /// <param name="x">The x coordinate of the <see cref="GameBoardSquare"/>.</param>
        /// <param name="y">The y coordinate of the <see cref="GameBoardSquare"/>.</param>
        /// <returns>The word multiplier of <see cref="GameBoardSquare"/> (x, y).</returns>
        private int GetWordMultiplier(int x, int y)
        {
            if (y == 0 || y == 14)
            {
                // Triple word scores on rows 0 and 14.
                if (x == 0 || x == 7 || x == 14)
                {
                    return 3;
                }
            }

            if (y == 1 || y == 13)
            {
                // Double word scores on rows 1 and 13.
                if (x == 1 || x == 13)
                {
                    return 2;
                }
            }

            if (y == 2 || y == 12)
            {
                // Double word scores on rows 2 and 12.
                if (x == 2 || x == 12)
                {
                    return 2;
                }
            }

            if (y == 3 || y == 11)
            {
                // Double word scores on rows 3 and 11.
                if (x == 3 || x == 11)
                {
                    return 2;
                }
            }

            if (y == 4 || y == 10)
            {
                // Double word scores on rows 4 and 10.
                if (x == 4 || x == 10)
                {
                    return 2;
                }
            }

            // The center square is a double word score.
            if (y == 7)
            {
                // The triple word scores on row 7.
                if (x == 0 || x == 14)
                {
                    return 3;
                }

                // The double word score in the central square.
                if (x == y)
                {
                    return 2;
                }
            }

            return 1;
        }

        /// <summary>
        /// Gets the proper letter multiplier for <see cref="GameBoardSquare"/> (x,y).
        /// </summary>
        /// <param name="x">The x coordinate of the <see cref="GameBoardSquare"/>.</param>
        /// <param name="y">The y coordinate of the <see cref="GameBoardSquare"/>.</param>
        /// <returns>The letter multiplier of <see cref="GameBoardSquare"/> (x,y).</returns>
        private int GetLetterMultiplier(int x, int y)
        {
            if (y == 0 || y == 14)
            {
                // The double letter scores on rows 0 and 14.
                if (x == 3 || x == 11)
                {
                    return 2;
                }
            }

            if (y == 1 || y == 13)
            {
                // The tripple letter scores on rows 1 and 13.
                if (x == 5 || x == 9)
                {
                    return 3;
                }
            }

            if (y == 2 || y == 12)
            {
                // The double letter scores on rows 2 and 12.
                if (x == 6 || x == 8)
                {
                    return 2;
                }
            }

            if (y == 3 || y == 11)
            {
                // The double letter scores on rows 3 and 11.
                if (x == 0 || x == 7 || x == 14)
                {
                    return 2;
                }
            }

            if (y == 5 || y == 9)
            {
                // The triple letter scores on rows 5 and 9.
                if (x == 1 || x == 5 || x == 9 || x == 13)
                {
                    return 3;
                }
            }

            if (y == 6 || y == 8)
            {
                // The double letter scores on rows 6 and 8.
                if (x == 2 || x == 6 || x == 8 || x == 12)
                {
                    return 2;
                }
            }

            if (y == 7)
            {
                // The double letter scores on row 7.
                if (x == 3 || x == 11)
                {
                    return 2;
                }
            }

            return 1;
        }

        /// <summary>
        /// This struct allows a grid of <see cref="GameBoardSquare"/>s to be accessed using the index operator [x, y].
        /// </summary>
        public struct GameBoardSquareGrid
        {
            /// <summary>
            /// A list of the <see cref="GameBoardSquare"/>s that make up the grid (it is assumed to be a 15x15 grid).
            /// </summary>
            public List<GameBoardSquare> SquaresList;

            /// <summary>
            /// Initializes a new instance of the <see cref="GameBoardSquareGrid" /> struct.
            /// </summary>
            /// <param name="squaresList">The list of <see cref="GameBoardSquare"/>s that will make up the grid.</param>
            public GameBoardSquareGrid(List<GameBoardSquare> squaresList)
            {
                this.SquaresList = squaresList;
            }

            /// <summary>
            /// Gets or sets a particular <see cref="GameBoardSquare"/> in <see cref="SquaresList"/>, by treating it as a 15x15 two-dimensional list.
            /// </summary>
            /// <param name="x">The x coordinate of the desired <see cref="GameBoardSquare"/> in <see cref="SquaresList"/>.</param>
            /// <param name="y">The y coordinate of the desired <see cref="GameBoardSquare"/> in <see cref="SquaresList"/>.</param>
            /// <returns>The <see cref="GameBoardSquare"/> at location (x,y) in <see cref="SquaresList"/>.</returns>
            public GameBoardSquare this[int x, int y]
            {
                get
                {
                    return this.SquaresList[(15 * y) + x];
                }

                set
                {
                    this.SquaresList[(15 * y) + x] = value;
                }
            }
        }

        /// <summary>
        /// An exception of this class type means that an attempt was made to add an invalid <see cref="Play"/> to a <see cref="Board"/>.
        /// </summary>
        public class InvalidPlayException : Exception
        {
            /// <summary>
            /// A string that contains the details of why an <see cref="InvalidPlayException"/> exception was thrown.
            /// </summary>
            private string message;

            /// <summary>
            /// Initializes a new instance of the <see cref="InvalidPlayException"/> class.
            /// </summary>
            public InvalidPlayException()
            {
                this.message = "InvalidPlay: An attempt was made to add, check, or score an invalid Play to a Board.";
            }

            /// <summary>
            /// Initializes a new instance of the <see cref="InvalidPlayException"/> class.
            /// </summary>
            /// <param name="message">The private member message will be set to message.</param>
            public InvalidPlayException(string message)
            {
                this.message = "InvalidPlayInsertion: " + message;
            }

            /// <summary>
            /// Returns a string of the details of why an <see cref="InvalidPlayException"/> exception was thrown.
            /// </summary>
            /// <returns>A string of exception details.</returns>
            public override string ToString()
            {
                return this.message;
            }
        }
    }
}
