using SFML.Graphics;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IcyMazeRunner.Klassen;

namespace IcyMazeRunner.Klassen.Menüs
{
    class InGameMenu
    {
        /* unnötige Variablen?? */
        View vMenuView;
        int test;


        /* ~~~~ Menühintergrundtexturen anlegen ~~~~ */
        Texture txMenuBackground = new Texture("Texturen/Menü+Anzeigen/InGame Menü/MenuBG.png");
        Texture txMenuHeader = new Texture("Texturen/Menü+Anzeigen/InGame Menü/Gamemenu.png");

        /* ~~~~ Auswahltexturen anlegen ~~~~ */

        Texture txContinueSelected = new Texture("Texturen/Menü+Anzeigen/InGame Menü/Continue.png");
        Texture txGoMainMenuSelected = new Texture("Texturen/Menü+Anzeigen/InGame Menü/Mainmenu.png");
        Texture txControlsSelected = new Texture("Texturen/Menü+Anzeigen/InGame Menü/controls.png"); // Controls.cs unterscheiden lassen, von wo Controls aufgerufen wurde

        // Ich nutze die Loadtexture vor erst mal als Map-Texture: via Map sind vergangene Level auswählbar/ erneut spielbar? 
        //-> GameState: Map(Übersichtskarte) (auch aus dem MainMenü anwählbar?)
        // Ansonsten gäbs halt später noch nen extra MapSprite --> extra machen; weitere Variable benötigt, die speichert, zu welchem Level 
        // man maximal springen kann, sonst wird int level einfach überschrieben und man kommt nicht mehr einfach zu dem Level, wo man war

        Texture txLoadGameSelected = new Texture("Texturen/Menü+Anzeigen/InGame Menü/worldmap.png"); 
        Texture txSaveGameSelected = new Texture("Texturen/Menü+Anzeigen/InGame Menü/Spiel speichern Platzhalter.png"); // für später

        /* ~~~~ Deselect-Texturen anlegen ~~~~ */
        

        Texture txContinueNotSelected = new Texture("Texturen/Menü+Anzeigen/InGame Menü/null.png");
        Texture txGoMainMenuNotSelected = new Texture("Texturen/Menü+Anzeigen/InGame Menü/null.png.");
        Texture txControlsNotSelected = new Texture("Texturen/Menü+Anzeigen/InGame Menü/null.png."); // Controls.cs unterscheiden lassen, von wo Controls aufgerufen wurde
        Texture txSaveGameNotSelected = new Texture("Texturen/Menü+Anzeigen/InGame Menü/null.png."); // für später
        Texture txLoadGameNotSelected = new Texture("Texturen/Menü+Anzeigen/InGame Menü/null.png."); // für später

        /* ~~~~ Sprites anlegen ~~~~ */     
        Sprite spMenuBackground;
        Sprite spMenuHeader;
        Sprite spContinue;
        Sprite spGoMainMenu;
        Sprite spControls; // Controls.cs unterscheiden lassen, von wo Controls aufgerufen wurde
        Sprite SaveGameSprite; // für später
        Sprite spLoadGame; // für später

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

        public void loadContent()
        {
            // Hintergrund laden


        }

        /* ~~~~ Konstruktor ~~~~*/
        public InGameMenu(Player Runner)
        {
            // Initialisieren der Steuerungsvariablen
            select = 2;
            isPressed = false;
            closeMenu = false;

            spMenuBackground = new Sprite(txMenuBackground);
            spMenuHeader = new Sprite(txMenuHeader);
            spContinue = new Sprite(txContinueSelected);
            spGoMainMenu = new Sprite(txGoMainMenuNotSelected);
            spControls = new Sprite(txControlsNotSelected);
            spLoadGame = new Sprite(txLoadGameNotSelected);
            SaveGameSprite = new Sprite(txSaveGameNotSelected);


            float scaleX = 0.9f;
            float scaleY = 0.9f;
            float xCoord = Runner.getXPosition()-595;  
            float yCoord = Runner.getYPosition() - 360; 

           // Position und Skalierung der einzelnen Texturen und Sprites
            spMenuBackground.Position = new Vector2f(xCoord, yCoord); 
            spMenuBackground.Scale = new Vector2f(scaleX, scaleY);

            spMenuHeader.Position = new Vector2f(xCoord, yCoord); 
            spMenuHeader.Scale = new Vector2f(scaleX, scaleY);

            spContinue.Position = new Vector2f(xCoord, yCoord); 
            spContinue.Scale = new Vector2f(scaleX, scaleY);

            spGoMainMenu.Position = new Vector2f(xCoord, yCoord); 
            spGoMainMenu.Scale = new Vector2f(scaleX, scaleY);

            spControls.Position = new Vector2f(xCoord, yCoord);
            spControls.Scale = new Vector2f(scaleX, scaleY);

            //Beinhaltet momentan "worldmap" was load-funktion beinhalten könnte
            spLoadGame.Position = new Vector2f(xCoord, yCoord); 
            spLoadGame.Scale = new Vector2f(scaleX, scaleY);

            /* für später
            SaveGameSprite.Position = new Vector2f(xCoord, yCoord); 
            SaveGameSprite.Scale = new Vector2f(scaleX, scaleY);           
            */
        }

        public EGameStates update()
        {
            // Menüsteuerung
            

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
                        spContinue.Texture = txContinueSelected;
                        spGoMainMenu.Texture = txGoMainMenuNotSelected;
                        spControls.Texture = txControlsNotSelected;
                        spLoadGame.Texture = txLoadGameNotSelected;
                        // SaveGameSprite.Texture = SaveGameNotSelectedTexture;
                        break;
                    }
                case 1: //main menü
                    {
                        spContinue.Texture = txContinueNotSelected;
                        spGoMainMenu.Texture = txGoMainMenuSelected;
                        spControls.Texture = txControlsNotSelected;
                        spLoadGame.Texture = txLoadGameNotSelected;
                        // SaveGameSprite.Texture = SaveGameNotSelectedTexture;
                        break;
                    }
                case -3: 
                    {
                        spContinue.Texture = txContinueNotSelected;
                        spGoMainMenu.Texture = txGoMainMenuSelected;
                        spControls.Texture = txControlsNotSelected;
                        spLoadGame.Texture = txLoadGameNotSelected;
                        // SaveGameSprite.Texture = SaveGameNotSelectedTexture;
                        break;
                    }
                case 2: //controls
                    {
                        spContinue.Texture = txContinueNotSelected;
                        spGoMainMenu.Texture = txGoMainMenuNotSelected;
                        spControls.Texture = txControlsSelected;
                        spLoadGame.Texture = txLoadGameNotSelected;
                        // SaveGameSprite.Texture = SaveGameNotSelectedTexture;
                        break;
                    }
                case -2:
                    {
                        spContinue.Texture = txContinueNotSelected;
                        spGoMainMenu.Texture = txGoMainMenuNotSelected;
                        spControls.Texture = txControlsSelected;
                        spLoadGame.Texture = txLoadGameNotSelected;
                        // SaveGameSprite.Texture = SaveGameNotSelectedTexture;
                        break;
                    }
                case 3: //Map (load)
                    {
                        spContinue.Texture = txContinueNotSelected;
                        spGoMainMenu.Texture = txGoMainMenuNotSelected;
                        spControls.Texture = txControlsNotSelected;
                        spLoadGame.Texture = txLoadGameSelected;
                        // SaveGameSprite.Texture = SaveGameNotSelectedTexture;
                        break;
                    }
                case -1:
                    {
                        spContinue.Texture = txContinueNotSelected;
                        spGoMainMenu.Texture = txGoMainMenuNotSelected;
                        spControls.Texture = txControlsNotSelected;
                        spLoadGame.Texture = txLoadGameSelected;
                        // SaveGameSprite.Texture = SaveGameNotSelectedTexture;
                        break;
                    }
            }

           




            //noch benötigt??
            Console.WriteLine(select); //Debug-Info -> kein output bei tastendruck?
            
            /* für später
            if (select == 4)
            {
                ContinueSprite.Texture = ContinueNotSelectedTexture;
                GoMainMenuSprite.Texture = GoMainMenuNotSelectedTexture;
                ControlsSprite.Texture = ControlsNotSelectedTexture;
                SaveGameSprite.Texture = SaveGameSelectedTexture;
                LoadGameSprite.Texture = LoadGameNotSelectedTexture;
            }
            */

          

            // Update der Gamestates

            if (select == 0 && Keyboard.IsKeyPressed(Keyboard.Key.Return))
                setCloseMenu(true);
            if ((select == 1 || select == -3) && Keyboard.IsKeyPressed(Keyboard.Key.Return))
                return EGameStates.mainMenu;
            if ((select == 2 || select == -2) && Keyboard.IsKeyPressed(Keyboard.Key.Return))
                return EGameStates.controls;
            if ((select == 3 || select == -1) && Keyboard.IsKeyPressed(Keyboard.Key.Return))  //Platzhalter Map auswahl
                return EGameStates.mainMenu;

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
            window.Draw(spContinue);
            window.Draw(spLoadGame);
            window.Draw(spControls);
            window.Draw(spGoMainMenu);

            //switch (select)
            //{
            //    case 0:
            //        {
            //            window.Draw(spContinue);
            //            break;
            //        }

            //    case 1:
            //        {
            //            window.Draw(spGoMainMenu);
            //            break;
            //        }
            //    case -3:
            //        {
            //            window.Draw(spGoMainMenu);
            //            break;
            //        }
            //    case 2:
            //        {
            //            window.Draw(spControls); 
            //            break;
            //        }
            //    case -2:
            //        {
            //            window.Draw(spControls);
            //            break;
            //        }
            //    case 3:
            //        {
            //            window.Draw(spLoadGame);
            //            break;
            //        }
            //    case -1:
            //        {
            //            window.Draw(spLoadGame);
            //            break;
            //        }
            //}
            
            
            
            /*
            für später

            window.Draw(SaveGameSelectedTexture);
            
            */
        }
    }
}
