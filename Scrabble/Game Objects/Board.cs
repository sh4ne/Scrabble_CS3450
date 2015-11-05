using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scrabble.Game_Objects
{  
    // Board class handles the list of tiles, and plays made to the board.

    class Board
    {
        //Contains the list of board tiles.
        private List<GameBoardSquare> SquaresList;
        public Board() { }
        /// <summary>
        /// Applies a Play to the tiles in SquaresList.
        /// Play parameter might need to be traced to its location once created.
        /// </summary>
        /// <param name="play">Receives a play and applys that to tiles.</param>
        public void AddPlayToBoard(Play play) { }
    }
}
