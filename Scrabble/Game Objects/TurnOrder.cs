using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scrabble.Game_Objects
{
    class TurnOrder
    {
        private int turnOrderIndex;
        private List<Scrabble.PlayerClass.Player> turnQueue = new List<Scrabble.PlayerClass.Player>();

        public TurnOrder()
        {

        }

        public void moveToNextPlayer()
        {
            if (turnOrderIndex >= turnQueue.Count-1) { turnOrderIndex = 0; }
            else { turnOrderIndex++; }
        }

        public int getActivePlayer()
        {
            return turnOrderIndex;
        }

        public bool containsPlayer(PlayerClass.Player player)
        {
            return turnQueue.Contains(player);
        }

        public bool isEmpty()
        {
            return turnQueue.Count == 0;
        }
    }
}
