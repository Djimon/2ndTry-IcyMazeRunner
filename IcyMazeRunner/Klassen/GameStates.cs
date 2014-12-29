using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IcyMazeRunner.Klassen
{
    class GameStates
    {
        enum EGameState
        {
            none,
            mainMenu,
            credits,
            controls,
            inGame,
            gameWon,

        }

        static EGameState gameState;


    }
}
