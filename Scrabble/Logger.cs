//-----------------------------------------------------------------------
// <copyright file="Logger.cs" company="Scrabble Project Developers">
//     Copyright (c) Scrabble Project Developers. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Scrabble
{
    using System;

    /// <summary>
    /// Logger - creates 2 files which you can write what is happening in the game.
    /// used for errors, warnings, normal messages, and the change of game state.
    /// </summary>
    public partial class Logger
    {
        /// <summary>
        /// The verbose log stream file
        /// </summary>
        public System.IO.TextWriter verboseLog;

        /// <summary>
        /// The game state log stream file
        /// </summary>
        public System.IO.TextWriter gameStateLog;

        /// <summary>
        /// Record of when the logger class was created.
        /// </summary>
        public string now;

        /// <summary>
        /// Initializes a new instance of the Logger class.
        /// each game world will have a logger to keep track of what is happening in game
        /// </summary>
        public Logger()
        {
            this.now = string.Format("{0:yyyy-MM-dd_hh-mm-ss}", DateTime.Now);
            System.IO.Directory.CreateDirectory(System.IO.Directory.GetCurrentDirectory() + "\\Logs\\");
        }

        /// <summary>
        /// logMessage - Logs a normal message to the verbose log file.
        /// </summary>
        /// <param name="message">This is the message that will be written to file</param>
        public void LogMessage(string message)
        {
            using (this.verboseLog =
                   new System.IO.StreamWriter(System.IO.Directory.GetCurrentDirectory() + "\\Logs\\" + this.now + "_VERBOSE_LOG.txt", true))
            {
                this.verboseLog.WriteLineAsync(message);
            }

            this.verboseLog.Close();
        }

        /// <summary>
        /// logWarning - Logs a warning message to the verbose log file.
        /// </summary>
        /// <param name="warning">The warning message that will be written.</param>
        /// <param name="where">Where the warning is coming from.</param>
        public void LogWarning(string warning, string where)
        {
            using (this.verboseLog =
                   new System.IO.StreamWriter(System.IO.Directory.GetCurrentDirectory() + "\\Logs\\" + this.now + "_VERBOSE_LOG.txt", true))
            {
                this.verboseLog.WriteLineAsync("*** WARNING - " + where + ": " + warning + " ***");
            }

            this.verboseLog.Close();
        }

        /// <summary>
        /// logError - Logs an error message to the verbose log file.
        /// </summary>
        /// <param name="error">The error message that will be written.</param>
        /// <param name="where">Where the error happened.</param>
        public void LogError(string error, string where)
        {
            using (this.verboseLog =
                   new System.IO.StreamWriter(System.IO.Directory.GetCurrentDirectory() + "\\Logs\\" + this.now + "_VERBOSE_LOG.txt", true))
            {
                this.verboseLog.WriteLineAsync("!!!!!!! ERROR - " + where + ": " + error + " !!!!!!!");
            }

            this.verboseLog.Close();
        }

        /// <summary>
        /// Adds a line to game state log.
        /// </summary>
        /// <param name="toGameState">String to add to game state log.</param>
        public void AddToGameState(string toGameState)
        {
            using (this.gameStateLog =
                   new System.IO.StreamWriter(System.IO.Directory.GetCurrentDirectory() + "\\Logs\\" + this.now + "_GAMESTATE_LOG.txt", true))
            {
                this.gameStateLog.WriteLineAsync(toGameState);
            }

            this.gameStateLog.Close();
        }
    }
}
