using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Scrabble;

namespace Scrabble.Game_Objects
{
    /// <summary>
    /// The object that will be passed to and from the server
    /// Contains essential objects for board creation
    /// </summary>
    public class GameState
    {
        /// <summary>
        /// List of all Players in game
        /// </summary>
        private List<PlayerClass.Player> PlayerList
        {
            get; set;
        }

        /// <summary>
        /// GameBoard object, represents the GUI GameBoard for game
        /// </summary>
        private Board GameBoard
        {
            get; set;
        }

        /// <summary>
        /// Bag object, contains letter tiles for game
        /// </summary>
        private Bag TileBag
        {
            get; set;
        }

        /// <summary>
        /// TurnOrder object, contains the order of play for game
        /// </summary>
        private TurnOrder PlayerOrder
        {
            get; set;
        }

        /// <summary>
        /// Play object, contains most recent play
        /// </summary>
        private Play recentPlay
        {
            get; set;
        }

        /// <summary>
        /// Overridden ToString, returns the total game state
        /// </summary>
        /// <returns>returns a game state string </returns>
        public override string ToString()
        {
            System.Text.StringBuilder state = new StringBuilder();
            string TimeStamp = string.Format("{0:MM/dd/yyyy hh:mm:ss}", DateTime.Now);
            state.AppendLine(TimeStamp);
            int i = 0;
            foreach(PlayerClass.Player player in this.PlayerList)
            {
                ++i;
                state.AppendLine("Player " + i + ": \nID - " + player.PlayerID.ToString());
                state.AppendLine("Name: " + player.Username);
                state.AppendLine("Score: " + player.Score);
                state.AppendLine("Turn Priority: " + player.HasTurnPriority.ToString());
            }
            return state.ToString();
        }

        public void AddPlayer(PlayerClass.Player player)
        {
            if (PlayerList == null) PlayerList = new List<PlayerClass.Player>();
            if (PlayerList.Count() < 5 && PlayerList.Count() >= 0)
            {
                PlayerList.Add(player);
            }
            else
            {
                Console.WriteLine("Players List is Full.");
            }
        }
    }
}
