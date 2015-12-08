//-----------------------------------------------------------------------
// <copyright file="Client.cs" company="Scrabble Project Developers">
//     Copyright (c) Scrabble Project Developers. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Scrabble.Server
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// The Client class contains all methods and data members to connect
    /// a game to the server and to maintan the game's status.
    /// </summary>
    class Client
    {
        /// <summary>
        /// The user name of the player who created the client.
        /// </summary>
        private string userName;

        /// <summary>
        /// The desired game size as determined by the player of userName.
        /// </summary>
        private int desiredGameSize;

        /// <summary>
        /// The desired maximum time allocated for each turn, determined
        /// by the player of userName.
        /// </summary>
        private int desiredMaxTurnTime;

        /// <summary>
        /// List of all of the necesary game objects needed for client connection.
        /// </summary>
        /// This is commented out due to data type error........
        private  List<GameWorld.GameWorld> gameObjects;

        /// <summary>
        /// GameWorld object instance used for other players to connect to.
        /// </summary>
        /// This is commented out due to data type error.........
        private GameWorld.GameWorld gameWorld;

        /// <summary>
        /// To store the latest game state as a string.
        /// </summary>
        private string lastReceivedGameState;

        /// <summary>
        /// When value is true, a player has issued a challenge, to then initiate
        /// a check on the dictionary to test if word played is legitimate.
        /// </summary>
        private bool challengeWasRequested;
    }
}
