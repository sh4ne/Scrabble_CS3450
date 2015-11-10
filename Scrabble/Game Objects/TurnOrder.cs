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
        private List<Player> turnQueue = new List<Player>();

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

        public bool containsPlayer(Player player)
        {
            return turnQueue.Contains(player);
        }

        public bool isEmpty()
        {
            return turnQueue.Count == 0;
        }
    }
}
