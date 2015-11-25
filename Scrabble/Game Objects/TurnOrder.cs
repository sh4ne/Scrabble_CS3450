//-----------------------------------------------------------------------
// <copyright file="TurnOrder.cs" company="Scrabble Project Developers">
//     Copyright (c) Scrabble Project Developers. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Scrabble.Game_Objects
{
    using System;
    using System.Collections.Generic;
    using Scrabble.PlayerClass;

    /// <summary>
    /// A circular queue of <see cref="Player"/> objects. Throws <see cref="InvalidTurnQueueSizeException"/>.
    /// </summary>
    public class TurnOrder
    {
        /// <summary>
        /// The index of the current <see cref="Player"/>.
        /// </summary>
        private int activePlayerIndex;

        /// <summary>
        /// The list of <see cref="Player"/> objects.
        /// </summary>
        private List<Player> turnQueue;

        /// <summary>
        /// Initializes a new instance of the <see cref="TurnOrder" /> class. Throws <see cref="InvalidTurnQueueSizeException"/>.
        /// </summary>
        /// <param name="players">The <see cref="Player"/> objects that are to be added to the queue.</param>
        public TurnOrder(List<Player> players)
        {
            if (players.Count < 2 || players.Count > 4)
            {
                throw new InvalidTurnQueueSizeException(players.Count.ToString() + " players were added to TurnQueue.");
            }

            this.turnQueue = players;
            this.activePlayerIndex = 0;

            Random rng = new Random();
            for (int i = 0; i < this.turnQueue.Count; ++i)
            {
                int randomIndex = rng.Next() % this.turnQueue.Count;
                Player temp = this.turnQueue[i];
                this.turnQueue[i] = this.turnQueue[randomIndex];
                this.turnQueue[randomIndex] = temp;
            }

            this.turnQueue[this.activePlayerIndex].HasTurnPriority = true;
        }

        /// <summary>
        /// Gets <see cref="turnQueue"/>.
        /// </summary>
        public List<Player> Players
        {
            get
            {
                return this.turnQueue;
            }
        }

        /// <summary>
        /// Gets <see cref="activePlayerIndex"/>.
        /// </summary>
        public int ActivePlayerIndex
        {
            get
            {
                return this.activePlayerIndex;
            }
        }

        /// <summary>
        /// Takes turn priority from the current player, and gives it to the next player
        /// (any player with a skipCount greater than 0 will be skipped, and his skipCount decremented).
        /// </summary>
        public void MoveToNextPlayer()
        {
            this.turnQueue[this.activePlayerIndex].HasTurnPriority = false;
            while (true)
            {
                if (this.activePlayerIndex >= this.turnQueue.Count - 1)
                {
                    this.activePlayerIndex = 0;
                }
                else
                {
                    ++this.activePlayerIndex;
                }

                if (this.turnQueue[this.activePlayerIndex].SkipCount > 0)
                {
                    this.turnQueue[this.activePlayerIndex].DecrementSkipCount();
                }
                else
                {
                    this.turnQueue[this.activePlayerIndex].HasTurnPriority = true;
                    break;
                }
            }            
        }

        /// <summary>
        /// Returns the index of a given <see cref="Player"/> in <see cref="turnQueue"/>.
        /// </summary>
        /// <param name="player">The player in question.</param>
        /// <returns>The index of player in <see cref="TurnQueue"/>. Returns -1 if player is not in <see cref="TurnQueue"/>./></returns>
        public int IndexOf(Player player)
        {
            int index = 0;
            foreach (Player p in this.turnQueue)
            {
                if (p.PlayerID == player.PlayerID && p.Username == player.Username)
                {
                    return index;
                }
                else
                {
                    ++index;
                }
            }

            return -1;
        }

        /// <summary>
        /// An exception of this type means that an invalid number of players was added to <see cref="turnQueue"/>.
        /// </summary>
        public class InvalidTurnQueueSizeException : Exception
        {
            /// <summary>
            /// A string that contains the details of why an <see cref="InvalidTurnQueueSizeException"/> was thrown.
            /// </summary>
            private string message;

            /// <summary>
            /// Initializes a new instance of the <see cref="InvalidTurnQueueSizeException"/> class.
            /// </summary>
            public InvalidTurnQueueSizeException()
            {
                this.message = "InvalidQueueSize: An invalid number of players were added to a TurnOrder object.";
            }

            /// <summary>
            /// Initializes a new instance of the <see cref="InvalidTurnQueueSizeException"/> class.
            /// </summary>
            /// <param name="message">The private member message will be set to message.</param>
            public InvalidTurnQueueSizeException(string message)
            {
                this.message = "InvalidQueueSize: " + message;
            }

            /// <summary>
            /// Returns a string of the details of why an <see cref="InvalidTurnQueueSizeException"/> was thrown.
            /// </summary>
            /// <returns>A string of exception details.</returns>
            public override string ToString()
            {
                return this.message;
            }
        }
    }
}
