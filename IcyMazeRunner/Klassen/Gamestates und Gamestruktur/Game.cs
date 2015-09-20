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
            // ToDo: Möglichkeit probieren, ob Gamestate-übergreifendes Sprite für die verschiedenen Menüs geeignet ist statt der einzelnen Sprites...
      

            public static uint windowSizeX = 1280; // auf 1280 zurückanpassen (ebenso die Screens des MainMenus, ControlScreen, Creditscreen
            public static uint windowSizeY = 720;

       

            EGameStates currentGameState = EGameStates.mainMenu;
            EGameStates prevGameState;

            GameStates gameState;



            /// <summary>
            /// Übernimmt Konstruktor von AbstractGame.cs
            /// </summary>
            public Game()
                : base(windowSizeX, windowSizeY, "MazeRunner")
            {
            }


            /* ~~~~~~~~~~~ UPDATE - METHODE ~~~~~~~~~~~~*/


        /// <summary>
        /// Gamestates werden aktualisiert.
        /// </summary>
        /// <param name="time"></param>
        public override void update(GameTime time)
        {
            if (currentGameState != prevGameState)
                handleGameState();

            currentGameState = gameState.update(time);
        }

            /* ~~~~~~~~~~~ DRAW  ~~~~~~~~~~~~*/


        /// <summary>
        /// Die dem Gamestate entsprechende draw-Methode wird aufgerufen.
        /// </summary>
        /// <param name="win"></param>
        public override void draw(RenderWindow win)
        {
            gameState.draw(win);
        }

        /// <summary>
        /// Bei Wechsel des Gamestates wird der aktuelle Gamestate angepasst.
        /// </summary>
        public void handleGameState()
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

            gameState.initialize(); //startwerte

            gameState.loadContent(); //grafiken/sounds laden

            prevGameState = currentGameState;
        }

        
    }
}
