using IcyMazeRunner.Klassen.Gamestates_und_Gamestruktur.GUI;
using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IcyMazeRunner.Klassen
{
    //interface für verschiedene Zustände des Spiels
        public enum EGameStates
        {
            none,
            mainMenu,
            credits,
            controls,
            inGame,
            gameWon,
            gameOver,
            NextLevel,

        }

        interface GameStates
        {
            void initialize();

            void loadContent();

            EGameStates update(GameTime time);

            void draw(RenderWindow win);
        }


    
}
