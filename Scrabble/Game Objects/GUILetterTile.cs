using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scrabble.GameWorld
{
    class GUILetterTile 
    {
        private Game_Objects.LetterTile letterTile;

        public GUILetterTile(Game_Objects.LetterTile letterTile) {
            this.letterTile = letterTile;
        }

        public void update(Game_Objects.LetterTile letterTile){
            this.letterTile = letterTile;
            //render();?
        }

        public void render(){

        }

    }
}
