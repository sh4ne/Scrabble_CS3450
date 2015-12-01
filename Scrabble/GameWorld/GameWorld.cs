//-----------------------------------------------------------------------
// <copyright file="GameWorld.cs" company="Scrabble Project Developers">
//     Copyright (c) Scrabble Project Developers. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Scrabble.GameWorld
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// A GameWorld object contains all the objects necessary to conduct 1 game of Scrabble (1 game board,
    /// 2-4 players, a bag, etc).
    /// </summary>
    public class GameWorld
    {
        private string GameId{ get; set; }

        public override string ToString()
        {
            string toReturn;
            toReturn = "Game ID: " + GameId;
            return toReturn;
        }

        public string getGameId()
        {
            return GameId;
        }
        public void setGameId(string name)
        {
            GameId = name;
        }
    }
}
