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
            get
            {
                if (PlayerList.Count() < 0)
                {
                    List<PlayerClass.Player> tempList = new List<PlayerClass.Player>();
                    return tempList;
                }
                else if (PlayerList.Count() >=4)
                {
                    string error = "PlayerList can't have more than four players";
                    throw new System.Exception(error);
                }
                return this.PlayerList;
            }           
        }

        /// <summary>
        /// GameBoard object, represents the GUI GameBoard for game
        /// </summary>
        //private Scrabble.GameBoard Board
        //{
        //    get { return this.Board; }
        //}

        /// <summary>
        /// Bag object, contains letter tiles for game
        /// </summary>
        //private Bag TileBag
        //{
        //    get { return this.TileBag; }
        //}

        /// <summary>
        /// TurnOrder object, contains the order of play for game
        /// </summary>
        //private TurnOrder PlayerOrder
        //{
        //    get { return this.PlayerOrder; }
        //}

        /// <summary>
        /// Play object, contains most recent play
        /// </summary>
        private Scrabble.Play recentPlay
        {
            get { return this.recentPlay; }
        }

        /// <summary>
        /// Overridden ToString, returns the total game state
        /// </summary>
        /// <returns>returns a game state string </returns>
        public override string ToString()
        {
            System.Text.StringBuilder state = new System.Text.StringBuilder();
            string TimeStamp = string.Format("{0:yyyy-MM-dd_hh-mm-ss}", DateTime.Now);
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
            PlayerList.Add(player);
        }
    }
}
