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
            public static uint windowSizeX = 1280; // auf 1280 zurückanpassen (ebenso die Screens des MainMenus, ControlScreen, Creditscreen
            public static uint windowSizeY = 720;

       

            EGameStates currentGameState = EGameStates.mainMenu;
            EGameStates prevGameState = EGameStates.none;

            GameStates gameState;

            /// <summary>
            /// Gibt das aktuelle Level an.
            /// </summary>
            public static int I_level = 1;

            public static int I_maxhealth = 100;

            /// <summary>
            /// Gibt den levelunabhängigen Bonus auf Verteidigung an.
            /// </summary>
            public static int I_BonusDefense = 0;

            /// <summary>
            /// Gibt den levelunabhängigen Bonus auf Angriffe an.
            /// </summary>
            public static int I_BonusAttack = 0;

            /// <summary>
            /// Gibt an, ob ein gespeichertes Spiel geladen wird oder ein neues Spiel gestartet wird.
            /// </summary>
            public static Boolean B_isLoadedGame = false;

            public static Sprite spBackGround;

            /// <summary>
            /// Übernimmt Konstruktor von AbstractGame.cs
            /// </summary>
            public Game()
                : base(windowSizeX, windowSizeY, "MazeRunner")
            {
                spBackGround = new Sprite(new Texture("Texturen/Map/background.png"));
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
