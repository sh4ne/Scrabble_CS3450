using Microsoft.VisualStudio.TestTools.UnitTesting;
using Scrabble.Game_Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scrabble.Game_Objects.Tests
{
    [TestClass()]
    public class BoardTests
    {
        [TestMethod()]
        public void BoardTest()
        {
            try
            {
                Board board = new Board();
            }
            catch
            {
                Assert.Fail();
            }
        }

        [TestMethod()]
        public void PlayIsValidTest()
        {
            Board board = new Board();

            // Make sure that there plays containing no LetterTiles are invalid.
            try
            {
                Assert.IsFalse(board.PlayIsValid(new Play(new List<LetterTile>(), new List<int>(), new List<int>(), 42)));
            }
            catch (Play.EmptyParrallelList err)
            {
                Console.WriteLine(err);
                Assert.IsTrue(true);
            }
            catch (Exception someOtherErr)
            {
                System.Diagnostics.Debug.WriteLine(someOtherErr);
                // shouldn't throw another type of error
                Assert.IsTrue(false);
            }

            List<int> coordsX = new List<int>() { 7 };
            List<int> coordsY = new List<int>() { 7 };
            List<LetterTile> letterTiles = new List<LetterTile>();
            letterTiles.Add(new LetterTile('A', 1));

            // Make sure that the first play is invalid if there is only one LetterTile in it.
            Assert.IsFalse(board.PlayIsValid(new Play(letterTiles, coordsX, coordsY, 42)));
            Assert.IsFalse(board.PlayIsValid(new Play(letterTiles, coordsY, coordsX, 42)));

            // Make sure that a play consisting of two LetterTiles at GameBoardSquares (7,7) and (8,7) is valid.
            coordsX.Add(8);
            coordsY.Add(7);
            letterTiles.Add(new LetterTile('A', 1));
            Assert.IsTrue(board.PlayIsValid(new Play(letterTiles, coordsX, coordsY, 42)));
            Assert.IsTrue(board.PlayIsValid(new Play(letterTiles, coordsY, coordsX, 42)));

            // Make sure that the first play is invalid if it doesn't go in (7,7).
            List<int> coordsX2 = new List<int>() { 5, 6 };
            List<int> coordsY2 = new List<int>() { 5, 5 };
            List<LetterTile> letterTiles2 = new List<LetterTile>();
            letterTiles2.Add(new LetterTile('A', 1));
            letterTiles2.Add(new LetterTile('A', 1));
            Assert.IsFalse(board.PlayIsValid(new Play(letterTiles2, coordsX2, coordsY2, 42)));
            Assert.IsFalse(board.PlayIsValid(new Play(letterTiles2, coordsY2, coordsX2, 42)));

            // Make sure that a play is invalid if it includes duplicates.
            coordsX2 = new List<int>() { 7, 7, 6 };
            coordsY2 = new List<int>() { 7, 7, 7 };
            letterTiles2 = new List<LetterTile>();
            letterTiles2.Add(new LetterTile('A', 1));
            letterTiles2.Add(new LetterTile('A', 1));
            letterTiles2.Add(new LetterTile('A', 1));
            try
            {
                Assert.IsFalse(board.PlayIsValid(new Play(letterTiles2, coordsX2, coordsY2, 42)));
            }
            catch (Play.DuplicateCoords err)
            {
                Console.WriteLine(err);
                Assert.IsTrue(true);
            }

            try
            {
                Assert.IsFalse(board.PlayIsValid(new Play(letterTiles2, coordsY2, coordsX2, 42)));
            }
            catch (Play.DuplicateCoords err)
            {
                Console.WriteLine(err);
                Assert.IsTrue(true);
            }

            // Make sure that all the tiles in a play have to either be in the same row or the same column.
            coordsX2 = new List<int>() { 5, 6, 7 };
            coordsY2 = new List<int>() { 5, 6, 7 };
            letterTiles2 = new List<LetterTile>();
            letterTiles2.Add(new LetterTile('A', 1));
            letterTiles2.Add(new LetterTile('A', 1));
            letterTiles2.Add(new LetterTile('A', 1));
            try {
                Assert.IsFalse(board.PlayIsValid(new Play(letterTiles2, coordsX2, coordsY2, 42)));
            }
            catch (Play.InvalidAxis err)
            {
                Console.WriteLine(err);
                Assert.IsTrue(true);
            }

            try
            {
                Assert.IsFalse(board.PlayIsValid(new Play(letterTiles2, coordsY2, coordsX2, 42)));
            }
            catch (Play.InvalidAxis err)
            {
                Console.WriteLine(err);
                Assert.IsTrue(true);
            }

            // Make sure that a play consisting of two LetterTiles at GameBoardSquares (7,7) and (8,7) is valid.
            coordsX.Add(9);
            coordsY.Add(7);
            coordsX.Add(6);
            coordsY.Add(7);
            letterTiles.Add(new LetterTile('A', 1));
            letterTiles.Add(new LetterTile('A', 1));
            Assert.IsTrue(board.PlayIsValid(new Play(letterTiles, coordsX, coordsY, 42)));
            Assert.IsTrue(board.PlayIsValid(new Play(letterTiles, coordsY, coordsX, 42)));

            // Make sure that a gap between LetterTiles makes it invalid.
            coordsX.Add(11);
            coordsY.Add(7);
            letterTiles.Add(new LetterTile('A', 1));
            Assert.IsFalse(board.PlayIsValid(new Play(letterTiles, coordsX, coordsY, 42)));
            Assert.IsFalse(board.PlayIsValid(new Play(letterTiles, coordsY, coordsX, 42)));

            // Add the play to the board without removing the invalid bits.
            bool exceptionWasThrown = false;
            try
            {
                board.AddPlayToBoard(new Play(letterTiles, coordsX, coordsY, 42));
            }
            catch
            {
                exceptionWasThrown = true;
            }
            Assert.IsTrue(exceptionWasThrown);

            coordsX.Remove(11);
            coordsY.Remove(7);
            letterTiles.Remove(letterTiles[0]);

            // Add the play to the board after removing the invalid bits.
            try
            {
                board.AddPlayToBoard(new Play(letterTiles, coordsX, coordsY, 42));
            } 
            catch
            {
                Assert.Fail();
            }

            // The last play was horizontal, so make a vertical play, and add it to the board.

            coordsX = new List<int>() { 8, 8, 8, 8 };
            coordsY = new List<int>() { 5, 6, 8, 9 };

            try
            {
                board.AddPlayToBoard(new Play(letterTiles, coordsX, coordsY, 42));
            }
            catch
            {
                Assert.Fail();
            }

            // Add a play that overlaps with another play, and make sure that an exception gets thrown.
            coordsX = new List<int>() { 6, 6, 6, 6 };
            coordsY = new List<int>() { 5, 6, 7, 8 };

            exceptionWasThrown = false;
            try
            {
                board.AddPlayToBoard(new Play(letterTiles, coordsX, coordsY, 42));
            }
            catch
            {
                exceptionWasThrown = true;
            }
            Assert.IsTrue(exceptionWasThrown);

            exceptionWasThrown = false;
            try
            {
                board.AddPlayToBoard(new Play(letterTiles, coordsY, coordsX, 42));
            }
            catch
            {
                exceptionWasThrown = true;
            }
            Assert.IsTrue(exceptionWasThrown);

            // Add a play that is not adjacent to any LetterTiles, and make sure that an exception gets thrown.
            coordsX = new List<int>() { 6, 6, 6, 6 };
            coordsY = new List<int>() { 9, 10, 11, 12 };

            exceptionWasThrown = false;
            try
            {
                board.AddPlayToBoard(new Play(letterTiles, coordsX, coordsY, 42));
            }
            catch
            {
                exceptionWasThrown = true;
            }
            Assert.IsTrue(exceptionWasThrown);

            // Add a play that is adjacent to LetterTiles, but has a gap between its letters, and make sure that an exception gets thrown.
            coordsX = new List<int>() { 6, 6, 6, 6 };
            coordsY = new List<int>() { 8, 9, 11, 12 };

            exceptionWasThrown = false;
            try
            {
                board.AddPlayToBoard(new Play(letterTiles, coordsX, coordsY, 42));
            }
            catch
            {
                exceptionWasThrown = true;
            }
            Assert.IsTrue(exceptionWasThrown);

            // Now correctly add another vertical play.
            coordsX = new List<int>() { 6, 6, 6, 6 };
            coordsY = new List<int>() { 4, 5, 6, 8 };

            exceptionWasThrown = false;
            try
            {
                board.AddPlayToBoard(new Play(letterTiles, coordsX, coordsY, 42));
            }
            catch
            {
                Assert.Fail();
            }

            // Now add a correct horizontal play that has two gaps.
            // Now correctly add another vertical play.
            coordsX = new List<int>() { 5, 7, 9, 10 };
            coordsY = new List<int>() { 5, 5, 5, 5 };

            exceptionWasThrown = false;
            try
            {
                board.AddPlayToBoard(new Play(letterTiles, coordsX, coordsY, 42));
            }
            catch
            {
                Assert.Fail();
            }

            // Add plays to make sure that the edges don't mess up the algorithms.
            coordsX = new List<int>() { 9, 9, 9, 9, 9, 9, 9 };
            coordsY = new List<int>() { 8, 9, 10, 11, 12, 13, 14 };
            letterTiles.Add(new LetterTile('A', 1));
            letterTiles.Add(new LetterTile('A', 1));
            letterTiles.Add(new LetterTile('A', 1));

            exceptionWasThrown = false;

            
            try
            {
                board.AddPlayToBoard(new Play(letterTiles, coordsX, coordsY, 42));
            }
            catch
            {
                Assert.Fail();
            }

            coordsX = new List<int>() { 11, 11, 11, 11, 11, 11, 11 };
            coordsY = new List<int>() { 6, 5, 4, 3, 2, 1, 0 };

            exceptionWasThrown = false;


            try
            {
                board.AddPlayToBoard(new Play(letterTiles, coordsX, coordsY, 42));
            }
            catch
            {
                Assert.Fail();
            }

            coordsX = new List<int>() { 14, 13, 12, 10, 9, 8, 7 };
            coordsY = new List<int>() { 0, 0, 0, 0, 0, 0, 0 };

            exceptionWasThrown = false;


            try
            {
                board.AddPlayToBoard(new Play(letterTiles, coordsX, coordsY, 42));
            }
            catch
            {
                Assert.Fail();
            }

            coordsX = new List<int>() { 0, 1, 2, 3, 4, 5, 6 };
            coordsY = new List<int>() { 0, 0, 0, 0, 0, 0, 0 };

            exceptionWasThrown = false;


            try
            {
                board.AddPlayToBoard(new Play(letterTiles, coordsX, coordsY, 42));
            }
            catch
            {
                Assert.Fail();
            }

            coordsX = new List<int> { 8 };
            coordsY = new List<int> { 4 };
            letterTiles = new List<LetterTile>();
            letterTiles.Add(new LetterTile('A', 1));

            try
            {
                board.AddPlayToBoard(new Play(letterTiles, coordsX, coordsY, 42));
            }
            catch
            {
                Assert.Fail();
            }

            board = new Board();
            coordsX = new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14 };
            coordsY = new List<int> { 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7 };
            letterTiles = new List<LetterTile>();

            for (int i = 0; i < 15; ++i)
            {
                letterTiles.Add(new LetterTile('A', 1));
            }

            try
            {
                board.AddPlayToBoard(new Play(letterTiles, coordsX, coordsY, 42));
            } catch
            {
                exceptionWasThrown = true;
            }
            Assert.IsTrue(exceptionWasThrown);
            exceptionWasThrown = false;
            try
            {
                board.AddPlayToBoard(new Play(letterTiles, coordsY, coordsX, 42));
            }
            catch
            {
                exceptionWasThrown = true;
            }
            Assert.IsTrue(exceptionWasThrown);

        }

        [TestMethod()]
        public void ScorePlayTest()
        {
            List<int> coordsX;
            List<int> coordsY;
            List<LetterTile> letterTiles;
            Board board = new Board();

            coordsX = new List<int>() { 3, 4, 5, 6, 7 };
            coordsY = new List<int>() { 7, 7, 7, 7, 7 };
            letterTiles = new List<LetterTile>()
            { 
                new LetterTile('F', 4),
                new LetterTile('I', 1),
                new LetterTile('L', 1),
                new LetterTile('E', 1),
                new LetterTile('S', 1)
            };

            try
            {
                board.AddPlayToBoard(new Play(letterTiles, coordsX, coordsY, 42));
            }
            catch
            {
                Assert.Fail();
            }

            int score = 0;
            try
            {
                score = board.ScorePlay(new Play(letterTiles, coordsX, coordsY, 42), false);
            } catch
            {
                Assert.Fail();
            }
            Assert.IsTrue(score == 24);

            try
            {
                score = board.ScorePlay(new Play(letterTiles, coordsX, coordsY, 42), true);
            }
            catch
            {
                Assert.Fail();
            }
            Assert.IsTrue(score == 24);

            try
            {
                score = board.ScorePlay(new Play(letterTiles, coordsX, coordsY, 42), true);
            }
            catch
            {
                Assert.Fail();
            }
            // The word/letter multipliers should now be gone after scoring.
            Assert.IsTrue(score == 8);

            bool exceptionWasThrown = false;
            coordsX = new List<int>() { 3, 4, 5, 6, 8 };

            try
            {
                score = board.ScorePlay(new Play(letterTiles, coordsX, coordsY, 42), true);
            }
            catch
            {
                exceptionWasThrown = true;
            }
            Assert.IsTrue(exceptionWasThrown);
            exceptionWasThrown = false;

            coordsX = new List<int>() { 3, 4, 5, 6, 7 };
            coordsY = new List<int>() { 6, 6, 6, 6, 6 };

            try
            {
                board.AddPlayToBoard(new Play(letterTiles, coordsX, coordsY, 42));
            }
            catch
            {
                Assert.Fail();
            }

            try
            {
                score = board.ScorePlay(new Play(letterTiles, coordsX, coordsY, 42), true);
            }
            catch
            {
                Assert.Fail();
            }
            Assert.IsTrue(score == 26);

            coordsX = new List<int>() { 2, 2, 2, 2, 2 };
            coordsY = new List<int>() { 2, 3, 4, 5, 6 };
            try
            {
                board.AddPlayToBoard(new Play(letterTiles, coordsX, coordsY, 42));
            }
            catch
            {
                Assert.Fail();
            }
            try
            {
                score = board.ScorePlay(new Play(letterTiles, coordsX, coordsY, 42), true);
            }
            catch
            {
                Assert.Fail();
            }
            Assert.IsTrue(score == 28);

            coordsX = new List<int>() { 3, 4, 5, 6, 7 };
            coordsY = new List<int>() { 2, 2, 2, 2, 2 };
            try
            {
                board.AddPlayToBoard(new Play(letterTiles, coordsX, coordsY, 42));
            }
            catch
            {
                Assert.Fail();
            }
            try
            {
                score = board.ScorePlay(new Play(letterTiles, coordsX, coordsY, 42), true);
            }
            catch
            {
                Assert.Fail();
            }
            Assert.IsTrue(score == 13);

            coordsX = new List<int>() { 7, 7, 7, 7, 7 };
            coordsY = new List<int>() { 8, 9, 10, 11, 12 };
            try
            {
                board.AddPlayToBoard(new Play(letterTiles, coordsX, coordsY, 42));
            }
            catch
            {
                Assert.Fail();
            }
            try
            {
                score = board.ScorePlay(new Play(letterTiles, coordsX, coordsY, 42), true);
            }
            catch
            {
                Assert.Fail();
            }
            Assert.IsTrue(score == 11);

            coordsX = new List<int>() { 2, 3, 4, 5, 6 };
            coordsY = new List<int>() { 12, 12, 12, 12, 12 };
            try
            {
                board.AddPlayToBoard(new Play(letterTiles, coordsX, coordsY, 42));
            }
            catch
            {
                Assert.Fail();
            }
            try
            {
                score = board.ScorePlay(new Play(letterTiles, coordsX, coordsY, 42), true);
            }
            catch
            {
                Assert.Fail();
            }
            Assert.IsTrue(score == 20);

            coordsX = new List<int>() { 8, 9, 10, 11, 12 };
            coordsY = new List<int>() { 10, 10, 10, 10, 10 };
            try
            {
                board.AddPlayToBoard(new Play(letterTiles, coordsX, coordsY, 42));
            }
            catch
            {
                Assert.Fail();
            }
            try
            {
                score = board.ScorePlay(new Play(letterTiles, coordsX, coordsY, 42), true);
            }
            catch
            {
                Assert.Fail();
            }
            Assert.IsTrue(score == 18);

            coordsX = new List<int>() { 0, 1, 3, 4, 5 };
            coordsY = new List<int>() { 4, 4, 4, 4, 4 };
            try
            {
                board.AddPlayToBoard(new Play(letterTiles, coordsX, coordsY, 42));
            }
            catch
            {
                Assert.Fail();
            }
            try
            {
                score = board.ScorePlay(new Play(letterTiles, coordsX, coordsY, 42), true);
            }
            catch
            {
                Assert.Fail();
            }
            Assert.IsTrue(score == 18);

            coordsX = new List<int>() { 11, 11, 11, 11, 11 };
            coordsY = new List<int>() { 6, 7, 8, 9, 11 };
            try
            {
                board.AddPlayToBoard(new Play(letterTiles, coordsX, coordsY, 42));
            }
            catch
            {
                Assert.Fail();
            }
            try
            {
                score = board.ScorePlay(new Play(letterTiles, coordsX, coordsY, 42), true);
            }
            catch
            {
                Assert.Fail();
            }
            Assert.IsTrue(score == 20);

            coordsX = new List<int>() { 9, 9, 9, 9, 9 };
            coordsY = new List<int>() { 6, 7, 8, 9, 11 };
            try
            {
                board.AddPlayToBoard(new Play(letterTiles, coordsX, coordsY, 42));
            }
            catch
            {
                Assert.Fail();
            }
            try
            {
                score = board.ScorePlay(new Play(letterTiles, coordsX, coordsY, 42), true);
            }
            catch
            {
                Assert.Fail();
            }
            Assert.IsTrue(score == 11);

            coordsX = new List<int>() { 8, 10, 12, 13, 14 };
            coordsY = new List<int>() { 7, 7, 7, 7, 7 };
            try
            {
                board.AddPlayToBoard(new Play(letterTiles, coordsX, coordsY, 42));
            }
            catch
            {
                Assert.Fail();
            }
            try
            {
                score = board.ScorePlay(new Play(letterTiles, coordsX, coordsY, 42), true);
            }
            catch
            {
                Assert.Fail();
            }
            Assert.IsTrue(score == 54);

            coordsX = new List<int>() { 4, 4, 4, 4, 4 };
            coordsY = new List<int>() { 1, 3, 5, 8, 9 };
            try
            {
                board.AddPlayToBoard(new Play(letterTiles, coordsX, coordsY, 42));
            }
            catch
            {
                Assert.Fail();
            }
            try
            {
                score = board.ScorePlay(new Play(letterTiles, coordsX, coordsY, 42), true);
            }
            catch
            {
                Assert.Fail();
            }
            Assert.IsTrue(score == 12);


            letterTiles = new List<LetterTile>() { new LetterTile('A', 1) };
            coordsX = new List<int>() { 3 };
            coordsY = new List<int>() { 5 }; try
            {
                board.AddPlayToBoard(new Play(letterTiles, coordsX, coordsY, 42));
            }
            catch
            {
                Assert.Fail();
            }
            try
            {
                score = board.ScorePlay(new Play(letterTiles, coordsX, coordsY, 42), true);
            }
            catch
            {
                Assert.Fail();
            }
            Assert.IsTrue(score == 13);
        }

        [TestMethod()]
        public void ExceptionTest()
        {
            try
            {
                throw new Board.InvalidPlayException("Blah");
            }
            catch (Exception e)
            {
                Assert.IsTrue(e.ToString() == "InvalidPlayInsertion: Blah");
            }
        }

        [TestMethod()]
        public void GameBoardGridTest()
        {
            List<GameBoardSquare> squaresList = new List<GameBoardSquare>();

            for (int y = 0; y < 15; ++y)
            {
                for (int x = 0; x < 15; ++x)
                {
                    squaresList.Add(new GameBoardSquare(x, y, 1, 1));
                }
            }

            Board.GameBoardSquareGrid boardGrid = new Board.GameBoardSquareGrid(squaresList);

            try
            {
                boardGrid[7, 7] = new GameBoardSquare(8, 8, 3, 1);
            } catch
            {
                Assert.Fail();
            }
            Assert.IsTrue(boardGrid[7, 7].CoordinateX == 8 && boardGrid[7, 7].CoordinateY == 8);
        }

        [TestMethod()]
        public void PlayRemovalTest()
        {
            List<int> coordsX;
            List<int> coordsY;
            List<LetterTile> letterTiles;
            Board board = new Board();

            coordsX = new List<int>() { 3, 4, 5, 6, 7 };
            coordsY = new List<int>() { 7, 7, 7, 7, 7 };
            letterTiles = new List<LetterTile>()
            {
                new LetterTile('F', 4),
                new LetterTile('I', 1),
                new LetterTile('L', 1),
                new LetterTile('E', 1),
                new LetterTile('S', 1)
            };

            Assert.IsTrue(board.RemoveLastPlay().Count == 0);

            try
            {
                board.AddPlayToBoard(new Play(letterTiles, coordsX, coordsY, 42));
            }
            catch
            {
                Assert.Fail();
            }

            List<LetterTile> removedLetterTiles = board.RemoveLastPlay();

            Assert.IsTrue(removedLetterTiles.Count == 5);
            Assert.IsTrue(removedLetterTiles[0].LetterValue == 'F');
            Assert.IsTrue(removedLetterTiles[1].LetterValue == 'I');
            Assert.IsTrue(removedLetterTiles[2].LetterValue == 'L');
            Assert.IsTrue(removedLetterTiles[3].LetterValue == 'E');
            Assert.IsTrue(removedLetterTiles[4].LetterValue == 'S');

            Assert.IsTrue(board.RemoveLastPlay().Count == 0);
        }
    }
}