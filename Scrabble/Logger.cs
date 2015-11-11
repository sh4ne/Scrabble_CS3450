using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scrabble
{
    class Logger
    {
        /// <summary>
        /// 
        /// </summary>
        public Logger()
        {

            if (System.IO.Directory.Exists(System.IO.Directory.GetCurrentDirectory() + "\\Logs\\"))
            {
                this.verboseLog = new System.IO.StreamWriter(System.IO.Directory.GetCurrentDirectory() + "\\Log\\" + string.Format("{0:yyyy-MM-dd_hh-mm-ss-tt}", DateTime.Now) + "_VERBOSE_LOG.txt");
                this.gameStateLog = new System.IO.StreamWriter(System.IO.Directory.GetCurrentDirectory() + string.Format("{0:yyyy-MM-dd_hh-mm-ss-tt}", DateTime.Now) + "_GAME_STATE.txt");
            }
            else
            {
                System.IO.Directory.CreateDirectory(System.IO.Directory.GetCurrentDirectory() + "\\Logs\\");
                this.verboseLog = new System.IO.StreamWriter(System.IO.Directory.GetCurrentDirectory() + "\\Logs\\" + "VERBOSE_LOG.txt");
                this.gameStateLog = new System.IO.StreamWriter(System.IO.Directory.GetCurrentDirectory() + "\\Logs\\" + "GAME_STATE.txt");
            }
            
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        public void logMessage(string message)
        {
            this.verboseLog.WriteLineAsync(message);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="warning"></param>
        public void logWarning(string warning)
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="error"></param>
        public void logError(string error)
        {

        }

        /// <summary>
        /// 
        /// </summary>
        public void createGameState()
        {

        }

        /// <summary>
        /// 
        /// </summary>
        private System.IO.StreamWriter verboseLog;

        /// <summary>
        /// 
        /// </summary>
        private System.IO.StreamWriter gameStateLog;

    }
}
