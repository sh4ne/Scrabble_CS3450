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
    /// A circular queue of <see cref="Player"/> objects. Throws <see cref="InvalidTurnQueueSizeException"/> and <see cref="InvalidPlayerUpdateException"/>
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
        /// Updates the information of a <see cref="Player"/>. Throws <see cref="InvalidPlayerUpdateException"/>.
        /// </summary>
        /// <param name="index">The index of the <see cref="Player"/> in <see cref="turnQueue"/> to be updated.</param>
        /// <param name="player">The <see cref="Player"/> whose information will be used to update the chosen player.</param>
        public void UpdatePlayer(int index, Player player)
        {
            if (index < 0 || index >= this.turnQueue.Count)
            {
                throw new InvalidPlayerUpdateException("Invalid index of " + index.ToString());
            }

            if (this.turnQueue[index].Username != player.Username)
            {
                throw new InvalidPlayerUpdateException("Given username " + player.Username + " did not match turnQueue[" + index + "].Username " + this.turnQueue[index].Username);
            }
            
            if (this.turnQueue[index].PlayerID != player.PlayerID)
            {
                throw new InvalidPlayerUpdateException("Given PlayerID " + player.PlayerID.ToString() + " did not match turnQueue[" + index + "].PlayerID " + this.turnQueue[index].PlayerID);
            }

            this.turnQueue[index] = player;
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
        /// Tells whether a given <see cref="Player"/> is in <see cref="turnQueue"/>.
        /// </summary>
        /// <param name="player">The player in question.</param>
        /// <returns>True if player is in <see cref="turnQueue"/>. Returns false otherwise./></returns>
        public bool ContainsPlayer(Player player)
        {
            return this.turnQueue.Contains(player);
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

        /// <summary>
        /// An exception of this type means that one of the players in <see cref="turnQueue"/> was update incorrectly.
        /// </summary>
        public class InvalidPlayerUpdateException : Exception
        {
            /// <summary>
            /// A string that contains the details of why an <see cref="InvalidPlayerUpdateException"/> was thrown.
            /// </summary>
            private string message;

            /// <summary>
            /// Initializes a new instance of the <see cref="InvalidPlayerUpdateException"/> class.
            /// </summary>
            public InvalidPlayerUpdateException()
            {
                this.message = "InvalidPlayerUpdate: A player in a TurnQueue object was updated incorrectly.";
            }

            /// <summary>
            /// Initializes a new instance of the <see cref="InvalidPlayerUpdateException" /> class.
            /// </summary>
            /// <param name="message">The private member message will be set to message.</param>
            public InvalidPlayerUpdateException(string message)
            {
                this.message = "InvalidPlayerUpdate: " + message;
            }

            /// <summary>
            /// Returns a string of the details of why an <see cref="InvalidPlayerUpdateException"/> was thrown.
            /// </summary>
            /// <returns>A string of exception details.</returns>
            public override string ToString()
            {
                return this.message;
            }
        }
    }
}
