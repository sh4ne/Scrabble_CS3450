//-----------------------------------------------------------------------
// <copyright file="ChatPacket.cs" company="Scrabble Project Developers">
//     Copyright (c) Scrabble Project Developers. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Scrabble
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// A ChatPacket is an object that will be sent between a client and a server. It
    /// contains the ID and name of the player who sent the ChatPacket, as well as the
    /// message that was sent.
    /// </summary>
    public class ChatPacket
    {
        /// <summary>
        /// The ID of the player who sent the ChatPacket.
        /// </summary>
        private int playerID;

        /// <summary>
        /// The username of the player who sent the ChatPacket.
        /// </summary>
        private string username;

        /// <summary>
        /// The message that ChatPacket contains.
        /// </summary>
        private string message;

        /// <summary>
        /// Initializes a new instance of the <see cref="ChatPacket" /> class.
        /// </summary>
        /// <param name="playerID">this.playerID will be set equal to this parameter.</param>
        /// <param name="username">this.username will be set equal to this parameter.</param>
        /// <param name="message">this.message will be set equal to this parameter.</param>
        public ChatPacket(int playerID, string username, string message)
        {
            this.playerID = playerID;
            this.username = username;
            this.message = message;
        }

        /// <summary>
        /// Gets this.playerID.
        /// </summary>
        public int PlayerID
        {
            get
            {
                return this.playerID;
            }
        }

        /// <summary>
        /// Gets this.username.
        /// </summary>
        public string Username
        {
            get
            {
                return this.username;
            }
        }

        /// <summary>
        /// Gets this.message.
        /// </summary>
        public string Message
        {
            get
            {
                return this.message;
            }
        }
    }
}
