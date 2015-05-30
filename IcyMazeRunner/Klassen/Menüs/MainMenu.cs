using SFML.Graphics;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IcyMazeRunner.Klassen
{
    class MainMenu : GameStates
    {

        /* ~~~~ Variablen für Menüsteuerung ~~~~*/

        int I_select;
        bool B_isPressed;
        View vMenu;

        /* ~~~~ Texturen anlegen ~~~~*/
        Texture txCreditsNotSelected;
        Texture txCreditsSelected;

        Texture txExitNotSelected;
        Texture txExitSelected;

        Texture txStartNotSelected;
        Texture txStartSelected;

        Texture txControlsNotSelected;
        Texture txControlsSelected;

        /* ~~~~ Sprites anlegen ~~~~*/

        Sprite spStart;
        Sprite spCredits;
        Sprite spExit;
        Sprite spControls;

        /* ~~~~ Hintergrund anlegen ~~~~*/

        Texture txBackGround;
        Sprite spBackGround;


        /* ~~~~ Initialisierung und Positionsfestlegung ~~~~ */
        public void initialize()
        {
            I_select = 0;
            B_isPressed = false;
            vMenu = new View(new FloatRect(0, 0, 1062, 720));

            spStart = new Sprite(txStartNotSelected);
            spStart.Scale = new Vector2f(1f, 1f);
            spStart.Position = new Vector2f(0, 0);

            spCredits = new Sprite(txCreditsNotSelected);
            spCredits.Position = new Vector2f(0, 0);
            spCredits.Scale = new Vector2f(1f, 1f);

            spExit = new Sprite(txExitNotSelected);
            spExit.Position = new Vector2f(0, 0);
            spExit.Scale = new Vector2f(1f, 1f);

            spControls = new Sprite(txControlsNotSelected);
            spControls.Position = new Vector2f(0, 0);
            spControls.Scale = new Vector2f(1f, 1f);

            spBackGround = new Sprite(txBackGround);
            spBackGround.Position = new Vector2f(0, 0);
            spBackGround.Scale = new Vector2f(1f, 1f);

            
        }


        /* ~~~~ Laden des Inhalts ~~~~ */
        public void loadContent()
        {                        
                txCreditsNotSelected = new Texture("Texturen/Menü+Anzeigen/credits.png");
                txCreditsSelected = new Texture("Texturen/Menü+Anzeigen/credits_s.png");
                txExitNotSelected = new Texture("Texturen/Menü+Anzeigen/quit.png");
                txExitSelected = new Texture("Texturen/Menü+Anzeigen/quit_s.png");
                txStartNotSelected = new Texture("Texturen/Menü+Anzeigen/start.png");
                txStartSelected = new Texture("Texturen/Menü+Anzeigen/start_s.png");
                txControlsNotSelected = new Texture("Texturen/Menü+Anzeigen/controls.png");
                txControlsSelected = new Texture("Texturen/Menü+Anzeigen/controls_s.png");

                txBackGround = new Texture("Texturen/Menü+Anzeigen/Titel.png");
                
        }


        /* ~~~~ Update der GameStates bei Änderung der Auswahl ~~~~ */
        public EGameStates update(GameTime time)
        {


            vMenu.Reset(new FloatRect(0, 0, 1062, 720));
            // Menüsteuerung

            if (Keyboard.IsKeyPressed(Keyboard.Key.Up) && !B_isPressed)
            {
                I_select = (I_select + 1) % 4;
                B_isPressed = true;
            }

            //if (Keyboard.IsKeyPressed(Keyboard.Key.Left) && !B_isPressed)
            //{
            //    I_select = (I_select + 1) % 4;
            //    B_isPressed = true;   
            //}


            if (Keyboard.IsKeyPressed(Keyboard.Key.Down) && !B_isPressed)
            {
                I_select = (I_select - 1) % 4;
                B_isPressed = true;
            }

            //if (Keyboard.IsKeyPressed(Keyboard.Key.Right) && !B_isPressed)
            //{
            //    I_select = (I_select - 1) % 4;
            //    B_isPressed = true;
            //}

            if (!Keyboard.IsKeyPressed(Keyboard.Key.Down) && !Keyboard.IsKeyPressed(Keyboard.Key.Up))
                B_isPressed = false;
            //if (!Keyboard.IsKeyPressed(Keyboard.Key.Left) && !Keyboard.IsKeyPressed(Keyboard.Key.Right))
            //    B_isPressed = false;

            // Menüzustände


            switch (I_select)
            {
                case 0:  //Start
                    {
                        spStart.Texture = txStartSelected;
                        spControls.Texture = txControlsNotSelected;
                        spCredits.Texture = txCreditsNotSelected;
                        spExit.Texture = txExitNotSelected;
                        break;
                    }

                case 1:  //Controls
                    {
                        spStart.Texture = txStartNotSelected;
                        spControls.Texture = txControlsSelected;
                        spCredits.Texture = txCreditsNotSelected;
                        spExit.Texture = txExitNotSelected;
                        break;
                    }

                case -3:
                    {
                        spStart.Texture = txStartNotSelected;
                        spControls.Texture = txControlsSelected;
                        spCredits.Texture = txCreditsNotSelected;
                        spExit.Texture = txExitNotSelected;
                        break;
                    }

                case 2:  // Credits
                    {
                        spStart.Texture = txStartNotSelected;
                        spControls.Texture = txControlsNotSelected;
                        spCredits.Texture = txCreditsSelected;
                        spExit.Texture = txExitNotSelected;
                        break;
                    }

                case -2:
                    {
                        spStart.Texture = txStartNotSelected;
                        spControls.Texture = txControlsNotSelected;
                        spCredits.Texture = txCreditsSelected;
                        spExit.Texture = txExitNotSelected;
                        break;
                    }

                case 3:
                    {
                        spStart.Texture = txStartNotSelected;
                        spControls.Texture = txControlsNotSelected;
                        spCredits.Texture = txCreditsNotSelected;
                        spExit.Texture = txExitSelected;
                        break;
                    }
                case -1:
                    {
                        spStart.Texture = txStartNotSelected;
                        spControls.Texture = txControlsNotSelected;
                        spCredits.Texture = txCreditsNotSelected;
                        spExit.Texture = txExitSelected;
                        break;
                    }
            }

            spBackGround.Texture = txBackGround;
            
            // Update der Gamestates

            if (I_select == 0 && Keyboard.IsKeyPressed(Keyboard.Key.Return))
                return EGameStates.inGame;
            if (I_select == 1 && Keyboard.IsKeyPressed(Keyboard.Key.Return))
                return EGameStates.controls;
            if (I_select == 2 && Keyboard.IsKeyPressed(Keyboard.Key.Return))
                return EGameStates.credits;
            if (I_select == 3 && Keyboard.IsKeyPressed(Keyboard.Key.Return))
                return EGameStates.none;

            return EGameStates.mainMenu;
        }


        /* ~~~~ Draw ~~~~ */
        public void draw(RenderWindow window)
        {
            window.SetView(vMenu);
            window.Draw(spBackGround);
            window.Draw(spStart);
            window.Draw(spCredits);
            window.Draw(spExit);
            window.Draw(spControls);
            window.SetMouseCursorVisible(true);
        }
        
    }
}
