//-----------------------------------------------------------------------
// <copyright file="GameState.cs" company="Scrabble Project Developers">
//     Copyright (c) Scrabble Project Developers. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Scrabble.Game_Objects
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Scrabble;

    /// <summary>
    /// The object that will be passed to and from the server
    /// Contains essential objects for board creation
    /// </summary>
    public class GameState
    {
        /// <summary>
        /// PlayerList private member variable
        /// </summary>
        private List<PlayerClass.Player> playerList;

        /// <summary>
        /// Initializes a new instance of the <see cref="GameState" /> class.
        /// Default constructor for an empty game state. Use this if 
        /// you want to set everything through the private properties setters manually.
        /// </summary>
        public GameState()
        {
            // Do nothing let them input themselves
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GameState" /> class.
        /// Constructor for game state classes. Allows you to pass in and set
        /// all properties at once.
        /// </summary>
        /// <param name="gameBoard">the value for the GameBoard private member</param>
        /// <param name="playerList">the value for the PlayerList private member</param>
        /// <param name="tileBag">the value for the TileBag private member</param>
        /// <param name="playerOrder">the value for the PlayerOrder private member</param>
        /// <param name="recentPlay">the value for the RecentPlay private member</param>
        public GameState(Board gameBoard, List<PlayerClass.Player> playerList, Bag tileBag, TurnOrder playerOrder, Play recentPlay)
        {
            this.GameBoard = gameBoard;
            this.PlayerList = playerList;
            this.TileBag = tileBag;
            this.PlayerOrder = playerOrder;
            this.RecentPlay = recentPlay;
        }

        /// <summary>
        /// Gets or sets list of all Players in GameState
        /// </summary>
        public List<PlayerClass.Player> PlayerList
        {
            get
            {
                return this.playerList;
            }

            set 
            {
                List<PlayerClass.Player> playerList = value;
                List<PlayerClass.Player> filteredPlayers = new List<PlayerClass.Player>();

                // get rid of some information private to players
                playerList.GetRange(0, Math.Min(4, playerList.Count)).ForEach((player) => 
                {
                    PlayerClass.Player filteredPlayer = new PlayerClass.Player(player.PlayerID, player.Username);
                    filteredPlayer.AddToScore(player.Score);
                    for (int i = 0; i < player.SkipCount; ++i)
                    {
                        filteredPlayer.IncrementSkipCount();
                    }

                    filteredPlayer.HasTurnPriority = player.HasTurnPriority;
                    if (player.HasVotedToEndGame && player.HasVotedToEndGame != filteredPlayer.HasVotedToEndGame)
                    {
                        filteredPlayer.ToggleVote();
                    }

                    filteredPlayers.Add(filteredPlayer);
                });

                this.playerList = filteredPlayers;
            }
        }

        /// <summary>
        /// Gets or sets the GameBoard object, which represents the GUI GameBoard for game
        /// </summary>
        public Board GameBoard
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the Bag object, which contains letter tiles for game
        /// </summary>
        public Bag TileBag
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the TurnOrder object, which contains the order of play for game
        /// </summary>
        public TurnOrder PlayerOrder
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the Play object, which contains the most recent play
        /// </summary>
        public Play RecentPlay
        {
            get; set;
        }
        
        /// <summary>
        /// Adds a single player to the PlayerList private property
        /// </summary>
        /// <param name="player">The player to add</param>
        public void AddPlayer(PlayerClass.Player player)
        {
            if (this.PlayerList == null)
            {
                this.PlayerList = new List<PlayerClass.Player>();
            }

            if (this.PlayerList.Count() < 4 && this.PlayerList.Count() >= 0)
            {
                this.PlayerList.Add(player);
            }
            else
            {
                Console.WriteLine("Players List is Full.");
            }
        }
    }
}
