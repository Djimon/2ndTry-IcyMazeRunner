﻿using SFML.Graphics;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IcyMazeRunner.Klassen;
using SFML.System;

namespace IcyMazeRunner.Klassen.Menüs
{
    class InGameMenu
    {


        // ToDo: unnötige Variablen?? 
        View vMenuView;

        /* ~~~~ Menühintergrundtexturen anlegen ~~~~ */
        Texture txMenuBackground = new Texture("Texturen/Menu+Anzeigen/InGame Menü/MenuBG.png");
        Texture txMenuHeader = new Texture("Texturen/Menu+Anzeigen/InGame Menü/Gamemenu.png");

        /* ~~~~ Auswahltexturen anlegen ~~~~ */

        Texture txContinueSelected = new Texture("Texturen/Menu+Anzeigen/InGame Menü/Continue.png");
        Texture txGoMainMenuSelected = new Texture("Texturen/Menu+Anzeigen/InGame Menü/Mainmenu.png");
        Texture txControlsSelected = new Texture("Texturen/Menu+Anzeigen/InGame Menü/controls.png"); // Controls.cs unterscheiden lassen, von wo Controls aufgerufen wurde

        // Ich nutze die Loadtexture vor erst mal als Map-Texture: via Map sind vergangene Level auswählbar/ erneut spielbar? 
        //-> GameState: Map(Übersichtskarte) (auch aus dem MainMenü anwählbar?)
        // Ansonsten gäbs halt später noch nen extra MapSprite --> extra machen; weitere Variable benötigt, die speichert, zu welchem Level 
        // man maximal springen kann, sonst wird int level einfach überschrieben und man kommt nicht mehr einfach zu dem Level, wo man war

        Texture txLoadGameSelected = new Texture("Texturen/Menu+Anzeigen/InGame Menü/worldmap.png");
        Texture txSaveGameSelected = new Texture("Texturen/Menu+Anzeigen/InGame Menü/Spiel speichern Platzhalter.png"); // für später

        /* ~~~~ Deselect-Texturen anlegen ~~~~ */


        //Texture txContinueNotSelected = new Texture("Texturen/Menu+Anzeigen/InGame Menü/null.png");
        //Texture txGoMainMenuNotSelected = new Texture("Texturen/Menu+Anzeigen/InGame Menü/null.png.");
        //Texture txControlsNotSelected = new Texture("Texturen/Menu+Anzeigen/InGame Menü/null.png."); // Controls.cs unterscheiden lassen, von wo Controls aufgerufen wurde
        //Texture txSaveGameNotSelected = new Texture("Texturen/Menu+Anzeigen/InGame Menü/null.png."); // für später
        //Texture txLoadGameNotSelected = new Texture("Texturen/Menu+Anzeigen/InGame Menü/null.png."); // für später

        /* ~~~~ Sprites anlegen ~~~~ */
        Sprite spMenuBackground;
        Sprite spMenuHeader;
        Sprite spSelected;

        // ToDo: werden nie verwendet:
        //Sprite spGoMainMenu;
        //Sprite spControls; // Controls.cs unterscheiden lassen, von wo Controls aufgerufen wurde
        //Sprite SaveGameSprite; // für später
        //Sprite spLoadGame; // für später

        /* ~~~~ Variablen für Menüsteuerung ~~~~*/
        int select;
        bool isPressed;
        bool closeMenu;

        /* ~~~~ Steuerung, um Menü zu schließen oder nicht zu schließen ~~~~*/

        public void setCloseMenu(bool value)
        {
            closeMenu = value;
        }

        public bool getCloseMenu()
        {
            return closeMenu;
        }



        /* ~~~~ Konstruktor ~~~~*/
        public InGameMenu(Player Runner)
        {
            // Initialisieren der Steuerungsvariablen
            select = 0;
            isPressed = false;
            closeMenu = false;
            vMenuView = new View(new FloatRect(0,0,1062,72));

            spMenuBackground = new Sprite(txMenuBackground);
            spMenuHeader = new Sprite(txMenuHeader);
            spSelected = new Sprite(txContinueSelected);
    

            float scaleX = 0.9f;
            float scaleY = 0.9f;
            float xCoord = Runner.getXPosition() - 595;
            float yCoord = Runner.getYPosition() - 360;

            // Position und Skalierung der einzelnen Texturen und Sprites
            spMenuBackground.Position = new Vector2f(xCoord, yCoord);
            spMenuBackground.Scale = new Vector2f(scaleX, scaleY);

            spMenuHeader.Position = new Vector2f(xCoord, yCoord);
            spMenuHeader.Scale = new Vector2f(scaleX, scaleY);

            spSelected.Position = new Vector2f(xCoord, yCoord);
            spSelected.Scale = new Vector2f(scaleX, scaleY);


        }



        public EGameStates update()
        {
            /*~~Menüsteuerung~~*/

            if (Keyboard.IsKeyPressed(Keyboard.Key.Up) && !isPressed)
            {
                select = (select + 1) % 4; // mit Laden und Speichern auf 5 erhöhen
                isPressed = true;
            }
            if (Keyboard.IsKeyPressed(Keyboard.Key.Down) && !isPressed)
            {
                select = (select - 1) % 4; // mit Laden und Speichern auf 5 erhöhen
                isPressed = true;
            }

            if (!Keyboard.IsKeyPressed(Keyboard.Key.Down) && !Keyboard.IsKeyPressed(Keyboard.Key.Up))
                isPressed = false;

            // Menüzustände
            // negative cases anpassen, wenn Menü erweitert wird
            switch (select)
            {
                case 0: //continue
                    {
                        spSelected.Texture = txContinueSelected;
                        break;
                    }
                case 1: //main menü
                    {
                        spSelected.Texture = txGoMainMenuSelected;
                        break;
                    }
                case -3:
                    {
                        spSelected.Texture = txGoMainMenuSelected;
                        break;
                    }
                case 2: //controls
                    {
                        spSelected.Texture = txControlsSelected;
                        break;
                    }
                case -2:
                    {
                        spSelected.Texture = txControlsSelected;
                        break;
                    }
                case 3: //Map (load)
                    {
                        spSelected.Texture = txLoadGameSelected;
                        break;
                    }
                case -1:
                    {
                        spSelected.Texture = txLoadGameSelected;
                        break;
                    }
            }






            
            //Console.WriteLine(select); //Debug-Info -> kein output bei tastendruck?

            /* für später
            if (select == 4)
            {
                SaveGameSprite.Texture = SaveGameSelectedTexture;
            }
            */



            // Update der Gamestates

            if (select == 0 && Keyboard.IsKeyPressed(Keyboard.Key.Return))
            {
                setCloseMenu(true);
                Console.WriteLine("enter");
            }
            if ((select == 1 || select == -3) && Keyboard.IsKeyPressed(Keyboard.Key.Return))
            {
                vMenuView.Reset(new FloatRect(0, 0, Game.windowSizeX,Game.windowSizeY));  
                return EGameStates.mainMenu;
            }
            if ((select == 2 || select == -2) && Keyboard.IsKeyPressed(Keyboard.Key.Return))
            {
                vMenuView.Reset(new FloatRect(0, 0, Game.windowSizeX, Game.windowSizeY));  
                return EGameStates.controls;
            }
            if ((select == 3 || select == -1) && Keyboard.IsKeyPressed(Keyboard.Key.Return))  //Platzhalter Map auswahl
            {
                vMenuView.Reset(new FloatRect(0, 0, Game.windowSizeX, Game.windowSizeY));  
                return EGameStates.mainMenu;
            }

            /* 
            
            Außerdem je 1 neues Fenster zum Laden/Speichern öffnen 
            
            if (select == 4 && Keyboard.IsKeyPressed(Keyboard.Key.Return))
                return EGameStates.inGame;
            */

            return EGameStates.inGame;
        }

        /* ~~~~ Draw ~~~~ */

        public void draw(RenderWindow window)
        {
            window.Draw(spMenuBackground);
            window.Draw(spMenuHeader);
            window.Draw(spSelected);
            window.SetMouseCursorVisible(true);
            //window.Draw(spLoadGame);
            //window.Draw(spControls);
            //window.Draw(spGoMainMenu);





            /*
            für später

            window.Draw(SaveGameSelectedTexture);
            
            */
        }
    }
}
