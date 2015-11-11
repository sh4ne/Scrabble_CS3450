using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scrabble
{
    /// <summary>
    /// Logger - creates 2 files which you can write what is happening in the game.
    /// used for errors, warnings, normal messages, and the change of gamestate.
    /// </summary>
    class Logger
    {
        /// <summary>
        /// default constructor - each game world will have a logger to keep track of what is happening in game
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
        public void logMessage(string message)
        {
            using (this.verboseLog =
                   new System.IO.StreamWriter(System.IO.Directory.GetCurrentDirectory() + "\\Logs\\" + this.now + "_VERBOSE_LOG.txt", true))
            {
                verboseLog.WriteLineAsync(message);
            }
            verboseLog.Close();
        }

        /// <summary>
        /// logWarning - Logs a warning message to the verbose log file.
        /// </summary>
        /// <param name="warning">The warning message that will be written.</param>
        /// <param name="where">Where the warning is coming from.</param>
        public void logWarning(string warning, string where)
        {
            using (this.verboseLog =
                   new System.IO.StreamWriter(System.IO.Directory.GetCurrentDirectory() + "\\Logs\\" + this.now + "_VERBOSE_LOG.txt", true))
            {
                verboseLog.WriteLineAsync("*** WARNING - "+where+": "+warning +" ***");
            }
            verboseLog.Close();
        }

        /// <summary>
        /// logError - Logs an error message to the verbose log file.
        /// </summary>
        /// <param name="error">The error message that will be written.</param>
        /// <param name="where">Where the error occured.</param>
        public void logError(string error, string where)
        {
            using (this.verboseLog =
                   new System.IO.StreamWriter(System.IO.Directory.GetCurrentDirectory() + "\\Logs\\" + this.now + "_VERBOSE_LOG.txt", true))
            {
                verboseLog.WriteLineAsync("!!!!!!! ERROR - "+where+": "+error+" !!!!!!!");
            }
            verboseLog.Close();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="toGameState"></param>
        public void addToGameState(string toGameState)
        {
            using (this.gameStateLog =
                   new System.IO.StreamWriter(System.IO.Directory.GetCurrentDirectory() + "\\Logs\\" + this.now + "_GAMESTATE_LOG.txt", true))
            {
                gameStateLog.WriteLineAsync(toGameState);
            }
            gameStateLog.Close();
        }

        /// <summary>
        /// 
        /// </summary>
        private System.IO.TextWriter verboseLog;

        /// <summary>
        /// 
        /// </summary>
        private System.IO.TextWriter gameStateLog;

        private string now;

    }
}
