using SFML.Graphics;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IcyMazeRunner.Klassen;

namespace IcyMazeRunner.Klassen
{
    class Game : AbstractGame
    {

            public static uint windowSizeX = 1280;
            public static uint windowSizeY = 720;

            EGameStates currentGameState = EGameStates.mainMenu;
            EGameStates prevGameState;

            GameStates gameState;


            public Game()
                : base(windowSizeX, windowSizeY, "IcyMazeRunner")
            {
            }


            /* ~~~~~~~~~~~ UPDATE - METHODE ~~~~~~~~~~~~*/

        public override void update(GameTime time)
        {
            if (currentGameState != prevGameState)
                handleGameState();

            currentGameState = gameState.update(time);
        }

            /* ~~~~~~~~~~~ DRAW  ~~~~~~~~~~~~*/

        public override void draw(RenderWindow win)
        {
            gameState.draw(win);
        }
           


            void handleGameState()
            {


                switch (currentGameState)
                {
                    case EGameStates.none:
                         win.Close(); //Argumentendifferenz
                        break;
                    case EGameStates.mainMenu:
                        gameState = new MainMenu();
                        break;
                    case EGameStates.inGame:
                        gameState = new InGame();
                        break;
                    case EGameStates.credits:
                        gameState = new Credits();
                        break;
                    case EGameStates.gameWon:
                        gameState = new gameWon();
                        break;
                    case EGameStates.controls:
                        gameState = new Controls();
                        break;
                }

                gameState.initialize();

                gameState.loadContent();

                prevGameState = currentGameState;
        }

        
    }
}
