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
            this.verboseLog = new System.IO.StreamWriter(System.IO.Directory.GetCurrentDirectory());

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        public void logMessage(string message)
        {

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
