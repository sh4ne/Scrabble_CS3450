using Microsoft.VisualStudio.TestTools.UnitTesting;
using Scrabble.Game_Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scrabble.Game_Objects.Tests
{
    [TestClass()]
    public class GameStateTests
    {
        [TestMethod()]
        public void ToStringTest()
        {
            Logger TempLogger = new Logger();
            try {
                    GameState TempState = new GameState();
                    PlayerClass.Player TempPlayer = new PlayerClass.Player(0001, "username");
                    TempState.AddPlayer(TempPlayer);
                    TempLogger.AddToGameState(TempState.ToString());
                }
            catch(System.Exception e)
            {
                TempLogger.LogError(e.ToString(), "ToStringTest");
            }
            Assert.IsFalse(true);
        }
    }
}