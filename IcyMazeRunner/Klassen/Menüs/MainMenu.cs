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

        Texture txOptionsSelected;
        Texture txExitSelected;
        Texture txStartSelected;
        Texture txControlsSelected;
        Texture txBG;
        Texture txFG;
        Texture txMain;

        /* ~~~~ Sprites anlegen ~~~~*/

        Sprite spSelect;
        Sprite spBackGround;
        Sprite spForeGround;
        Sprite spMain;



        /* ~~~~ Initialisierung und Positionsfestlegung ~~~~ */
        public void initialize()
        {
           

            I_select = 0;
            B_isPressed = false;
            vMenu = new View(new FloatRect(0, 0, 1280, 720));

            txBG = new Texture("Texturen/Map/background.png");

            txMain = new Texture("Texturen/Menü+Anzeigen/MainMenu.png");

            txOptionsSelected = new Texture("Texturen/Menü+Anzeigen/options.png");

            txExitSelected = new Texture("Texturen/Menü+Anzeigen/exit.png");

            txStartSelected = new Texture("Texturen/Menü+Anzeigen/start.png");

            txControlsSelected = new Texture("Texturen/Menü+Anzeigen/controls.png");

            txFG = new Texture("Texturen/Menü+Anzeigen/GUI-FG.png");
            
            
            Vector2f viewScale = new Vector2f((float)vMenu.Size.X / (float)Game.windowSizeX, (float)vMenu.Size.Y / (float)Game.windowSizeY);


            spBackGround = new Sprite(txBG);
            spBackGround.Position = new Vector2f(0, 0);
            
            

            spSelect = new Sprite(txStartSelected);
            spSelect.Position = new Vector2f(0, 0);
            spSelect.Scale = new Vector2f(0.83f, 1f);
           // spSelect.Scale = viewScale;
            

            spMain = new Sprite(txMain);
            spMain.Position = new Vector2f(0, 0);
            spMain.Scale = new Vector2f(0.83f, 1f);
          //  spMain.Scale = viewScale;


            spForeGround = new Sprite(txFG);
            spForeGround.Position = new Vector2f(0, 0);
            spForeGround.Scale = new Vector2f(0.83f, 1f);


            // modify sprite, to fit it in the gui
          
            

            
        }


        /* ~~~~ Laden des Inhalts ~~~~ */
        public void loadContent()
        {

                
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
                        spSelect.Texture = txStartSelected;
                        break;
                    }

                case 1:  //Controls
                    {
                        spSelect.Texture = txControlsSelected;
                        break;
                    }

                case -3:
                    {
                        spSelect.Texture = txControlsSelected;
                        break;
                    }

                case 2:  // Credits
                    {
                        spSelect.Texture = txOptionsSelected;
                        break;
                    }

                case -2:
                    {
                        spSelect.Texture = txOptionsSelected;
                        break;
                    }
                        //Quit
                case 3:
                    {
                        spSelect.Texture = txExitSelected;
                        break;
                    }
                case -1:
                    {
                        spSelect.Texture = txExitSelected;
                        break;
                    }
            }

            spBackGround.Texture = txBG;
            
            // Update der Gamestates

            if (I_select == 0 && Keyboard.IsKeyPressed(Keyboard.Key.Return))
            {
                Game.B_isLoadedGame = false;
                return EGameStates.inGame;
            }
            if (I_select == 1 && Keyboard.IsKeyPressed(Keyboard.Key.Return))
                return EGameStates.controls;
            if (I_select == 2 && Keyboard.IsKeyPressed(Keyboard.Key.Return))
                return EGameStates.credits;
            if (I_select == 3 && Keyboard.IsKeyPressed(Keyboard.Key.Return))
            {
                return EGameStates.none;
                //Programm schließen!?
            }

            return EGameStates.mainMenu;
        }


        /* ~~~~ Draw ~~~~ */
        public void draw(RenderWindow window)
        {
            window.SetView(vMenu);


            // draw the sprite
            window.SetView(vMenu);
            window.Draw(spBackGround);
            window.Draw(spMain);
            window.Draw(spForeGround);
            window.Draw(spSelect);
            
            window.SetMouseCursorVisible(true);
        }
        
    }
}
