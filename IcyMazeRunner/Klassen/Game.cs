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

            IcyMazeRunner.Klassen.GameStates.EGameStates currentGameState = IcyMazeRunner.Klassen.GameStates.EGameStates.mainMenu;
            IcyMazeRunner.Klassen.GameStates.EGameStates prevGameState;

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
                    case IcyMazeRunner.Klassen.GameStates.EGameStates.none:
                       // Windowclosed(); //Argumentendifferenz
                        break;
                    case IcyMazeRunner.Klassen.GameStates.EGameStates.mainMenu:
                        gameState = new MainMenu();
                        break;
                    case IcyMazeRunner.Klassen.GameStates.EGameStates.inGame:
                        gameState = new InGame();
                        break;
                    case IcyMazeRunner.Klassen.GameStates.EGameStates.credits:
                        gameState = new Credits();
                        break;
                    case IcyMazeRunner.Klassen.GameStates.EGameStates.gameWon:
                        gameState = new gameWon();
                        break;
                    case IcyMazeRunner.Klassen.GameStates.EGameStates.controls:
                        gameState = new Controls();
                        break;
                }
        }
    }
}
