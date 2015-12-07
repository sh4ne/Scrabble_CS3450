using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scrabble.GameWorld
{
    class GUISquare
    {
        private Game_Objects.GameBoardSquare gameBoardSquare;

        public GUISquare(Game_Objects.GameBoardSquare gameBoardSquare)
        {
            this.gameBoardSquare = gameBoardSquare;
        }

        public void update(Game_Objects.GameBoardSquare gameBoardSquare)
        {
            this.gameBoardSquare = gameBoardSquare;
        }

        public void render()
        {
            
        }
    }
}
