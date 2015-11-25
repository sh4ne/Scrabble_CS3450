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
    using Scrabble.Game_Objects;
    using Scrabble.PlayerClass;

    /// <summary>
    /// A GameWorld object contains all the objects necessary to conduct 1 game of Scrabble (1 game board,
    /// 2-4 players, a bag, etc).
    /// </summary>
    public class GameWorld
    {
        /// <summary>
        /// The <see cref="Board"/> on which the game is to be played.
        /// </summary>
        private Board gameBoard;

        /// <summary>
        /// The <see cref="Bag"/> which contains all of the <see cref="LetterTile"/>s to start off with.
        /// </summary>
        private Bag bag;

        /// <summary>
        /// The players who are playing the game. There should be 2-4 players.
        /// </summary>
        private List<Player> players;

        /// <summary>
        /// The turn order of the <see cref="Player"/>s.
        /// </summary>
        private TurnOrder turnOrder;

        /// <summary>
        /// Initializes a new instance of the <see cref="GameWorld" /> class.
        /// </summary>
        /// <param name="players">A list of the <see cref="Player"/>s who are playing the game.</param>
        public GameWorld(List<Player> players)
        {
            this.players = players;
            this.turnOrder = new TurnOrder(players);
            this.bag = new Bag();
            this.gameBoard = new Board();
        }
    }
}
