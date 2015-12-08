using System.Resources;

namespace Scrabble.GameWorld
{
    class GUILetterTile 
    {
        private Game_Objects.LetterTile letterTile;

        public GUILetterTile(Game_Objects.LetterTile letterTile) {
            this.letterTile = letterTile;
            ResourceManager letters = new ResourceManager("Strings", typeof(System.Reflection.Assembly).Assembly);
        }

        public void update(Game_Objects.LetterTile letterTile){
            this.letterTile = letterTile;
            //render();?
        }

        public void render(){

        }

    }
}
