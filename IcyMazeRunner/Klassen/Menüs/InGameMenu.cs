using SFML.Graphics;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IcyMazeRunner.Klassen.Menüs
{
    class InGameMenu
    {

        Texture MenuBackgroundTexture;
        Texture MenuHeaderTexture;


        Texture ContinueSelectedTexture = new Texture("Texturen/Menü+Anzeigen/InGame Menü/Continue.png");
        Texture GoMainMenuSelectedTexture = new Texture("Texturen/Menü+Anzeigen/InGame Menü/Mainmenu.png");
        Texture ControlsSelectedTexture = new Texture("Texturen/Menü+Anzeigen/InGame Menü/controls.png"); // Controls.cs unterscheiden lassen, von wo Controls aufgerufen wurde

        // Ich nutze die Loadtexture vor erst mal als Map-Texture: via Map sind vergangene Level auswählbar/ erneut spielbar? 
        //-> GameState: Map(Übersichtskarte) (auch aus dem MainMenü anwählbar?)
        // Ansonsten gäbs halt später noch nen extra MapSprite
        Texture LoadGameSelectedTexture = new Texture("Texturen/Menü+Anzeigen/InGame Menü/worldmap.png"); 


        Texture SaveGameSelectedTexture = new Texture("Texturen/Menü+Anzeigen/InGame Menü/Spiel speichern Platzhalter.png"); // für später
        

        Texture ContinueNotSelectedTexture = new Texture("Texturen/Menü+Anzeigen/InGame Menü/Gamemenu.png");
        Texture GoMainMenuNotSelectedTexture = new Texture("Texturen/Menü+Anzeigen/InGame Menü/Gamemenu.png.");
        Texture ControlsNotSelectedTexture = new Texture("Texturen/Menü+Anzeigen/InGame Menü/Gamemenu.png."); // Controls.cs unterscheiden lassen, von wo Controls aufgerufen wurde
        Texture SaveGameNotSelectedTexture = new Texture("Texturen/Menü+Anzeigen/InGame Menü/Gamemenu.png."); // für später
        Texture LoadGameNotSelectedTexture = new Texture("Texturen/Menü+Anzeigen/InGame Menü/Gamemenu.png."); // für später



        Sprite MenuBackgroundSprite;
        Sprite MenuHeaderSprite;
        Sprite ContinueSprite;
        Sprite GoMainMenuSprite;
        Sprite ControlsSprite; // Controls.cs unterscheiden lassen, von wo Controls aufgerufen wurde
        Sprite SaveGameSprite; // für später
        Sprite LoadGameSprite; // für später

        int select; // which component of Menu is selected
        bool isPressed;
        bool closeMenu;

        public void setCloseMenu(bool value)
        {
            closeMenu = value;
        }

        public bool getCloseMenu()
        {
            return closeMenu;
        }

        public InGameMenu()
        {
            // Initialize menu attributes
            select = 0;
            isPressed = false;
            closeMenu = false;

            // Construct sprites and textures

            MenuBackgroundTexture = new Texture("Texturen/Menü+Anzeigen/InGame Menü/MenuBG.png");
            MenuHeaderTexture = new Texture("Texturen/Menü+Anzeigen/InGame Menü/Gamemenu.png");


            MenuBackgroundSprite = new Sprite(MenuBackgroundTexture);
            MenuHeaderSprite = new Sprite(MenuHeaderTexture);
            ContinueSprite = new Sprite(ContinueSelectedTexture);
            GoMainMenuSprite = new Sprite(GoMainMenuNotSelectedTexture);
            ControlsSprite = new Sprite(ControlsNotSelectedTexture);
            LoadGameSprite = new Sprite(LoadGameNotSelectedTexture);
            /*
            SaveGameSprite = new Sprite(SaveGameNotSelectedTexture);
            
            */
            float scaleX = 118/147;
            float scaleY = 4/5;

            MenuBackgroundSprite.Position = new Vector2f(0f, 0f); // auf 460 anpassen, wenn Auflösung korrigiert
            MenuBackgroundSprite.Scale = new Vector2f(scaleX, scaleY);

            MenuHeaderSprite.Position = new Vector2f(0f, 0f); // auf 489 anpassen, wenn Auflösung korrigiert
            MenuHeaderSprite.Scale = new Vector2f(scaleX, scaleY);

            ContinueSprite.Position = new Vector2f(0f, 0f); // auf 489 anpassen, wenn Auflösung korrigiert
            ContinueSprite.Scale = new Vector2f(scaleX, scaleY);

            GoMainMenuSprite.Position = new Vector2f(0f, 0f); // auf 489 anpassen, wenn Auflösung korrigiert
            GoMainMenuSprite.Scale = new Vector2f(scaleX, scaleY);

            ControlsSprite.Position = new Vector2f(0f, 0f); // auf 489 anpassen, wenn Auflösung korrigiert
            ControlsSprite.Scale = new Vector2f(scaleX, scaleY);

            //Beinhaltet momentan "worldmap" was load-funktion beinhalten könnte
            LoadGameSprite.Position = new Vector2f(0f, 0f); // auf 489 anpassen, wenn Auflösung korrigiert
            LoadGameSprite.Scale = new Vector2f(scaleX, scaleY);

            /*
            
            alte Positionen
            
            MenuBackgroundSprite.Position = new Vector2f(351f, 120f); // auf 460 anpassen, wenn Auflösung korrigiert
            MenuBackgroundSprite.Scale = new Vector2f(1f, 1f);

            MenuHeaderSprite.Position = new Vector2f(380f, 134f); // auf 489 anpassen, wenn Auflösung korrigiert
            MenuHeaderSprite.Scale = new Vector2f(1f, 1f);

            ContinueSprite.Position = new Vector2f(380f, 211f); // auf 489 anpassen, wenn Auflösung korrigiert
            ContinueSprite.Scale = new Vector2f(1f, 1f);

            GoMainMenuSprite.Position = new Vector2f(380f, 288f); // auf 489 anpassen, wenn Auflösung korrigiert
            GoMainMenuSprite.Scale = new Vector2f(1f, 1f);

            ControlsSprite.Position = new Vector2f(380f, 357f); // auf 489 anpassen, wenn Auflösung korrigiert
            ControlsSprite.Scale = new Vector2f(1f, 1f);

            //Beinhaltet momentan "worldmap" was load-funktion beinhalten könnte
            LoadGameSprite.Position = new Vector2f(380f, 519f); // auf 489 anpassen, wenn Auflösung korrigiert
            LoadGameSprite.Scale = new Vector2f(1f, 1f);
            
            */
            /* für später
            SaveGameSprite.Position = new Vector2f(380f, 442f); // auf 489 anpassen, wenn Auflösung korrigiert
            SaveGameSprite.Scale = new Vector2f(1f, 1f);

           
            */
        }

        public EGameStates update(GameTime time)
        {
            // Menüsteuerung

            if (Keyboard.IsKeyPressed(Keyboard.Key.Up) && !isPressed)
            {
                select = (select + 1) % 3; // mit Laden und Speichern auf 5 erhöhen
                isPressed = true;
            }
            if (Keyboard.IsKeyPressed(Keyboard.Key.Down) && !isPressed)
            {
                select = (select - 1) % 3; // mit Laden und Speichern auf 5 erhöhen
                isPressed = true;
            }

            if (!Keyboard.IsKeyPressed(Keyboard.Key.Down) && !Keyboard.IsKeyPressed(Keyboard.Key.Up))
                isPressed = false;

            // Menüzustände

            if (select == 0)
            {
                ContinueSprite.Texture = ContinueSelectedTexture;
                GoMainMenuSprite.Texture = GoMainMenuNotSelectedTexture;
                ControlsSprite.Texture = ControlsNotSelectedTexture;
                LoadGameSprite.Texture = LoadGameNotSelectedTexture;
                /*
            
            für später
            
                SaveGameSprite.Texture = SaveGameNotSelectedTexture;
                
                */
            }

            if (select == 1)
            {
                ContinueSprite.Texture = ContinueNotSelectedTexture;
                GoMainMenuSprite.Texture = GoMainMenuSelectedTexture;
                ControlsSprite.Texture = ControlsNotSelectedTexture;
                LoadGameSprite.Texture = LoadGameNotSelectedTexture;
                /*
            
            für später
            
                SaveGameSprite.Texture = SaveGameNotSelectedTexture;
                
                */
            }
            if (select == 2)
            {
                ContinueSprite.Texture = ContinueNotSelectedTexture;
                GoMainMenuSprite.Texture = GoMainMenuNotSelectedTexture;
                ControlsSprite.Texture = ControlsSelectedTexture;
                LoadGameSprite.Texture = LoadGameNotSelectedTexture;
                /*
            
            für später
            
                SaveGameSprite.Texture = SaveGameNotSelectedTexture;
                
            */
            }

            
            
           
            
            if (select == 3)
            {
                ContinueSprite.Texture = ContinueNotSelectedTexture;
                GoMainMenuSprite.Texture = GoMainMenuNotSelectedTexture;
                ControlsSprite.Texture = ControlsNotSelectedTexture;
                LoadGameSprite.Texture = LoadGameNotSelectedTexture;
             //   SaveGameSprite.Texture = SaveGameSelectedTexture;
              
            }
            
            /* für später
             
            if (select == 4)
            {
                ContinueSprite.Texture = ContinueNotSelectedTexture;
                GoMainMenuSprite.Texture = GoMainMenuNotSelectedTexture;
                ControlsSprite.Texture = ControlsNotSelectedTexture;
                SaveGameSprite.Texture = SaveGameNotSelectedTexture;
                LoadGameSprite.Texture = LoadGameSelectedTexture;
            }
            */

            MenuBackgroundSprite.Texture = MenuBackgroundTexture;
            MenuHeaderSprite.Texture = MenuHeaderTexture;

            // Update der Gamestates

            if (select == 0 && Keyboard.IsKeyPressed(Keyboard.Key.Return))
                setCloseMenu(true);
            if (select == 1 && Keyboard.IsKeyPressed(Keyboard.Key.Return))
                return EGameStates.mainMenu;
            if (select == 2 && Keyboard.IsKeyPressed(Keyboard.Key.Return))
                return EGameStates.controls;
            if (select == 3 && Keyboard.IsKeyPressed(Keyboard.Key.Return))  //Platzhalter Map auswahl
                return EGameStates.mainMenu;

            /*
            
            Außerdem je 1 neues Fenster zum Laden/Speichern öffnen 
            
            if (select == 3 && Keyboard.IsKeyPressed(Keyboard.Key.Return))
                return EGameStates.inGame;
            if (select == 4 && Keyboard.IsKeyPressed(Keyboard.Key.Return))
                return EGameStates.inGame;
            */

            return EGameStates.inGame;
        }

        public void draw(RenderWindow window)
        {
            window.Draw(MenuBackgroundSprite);
            window.Draw(MenuHeaderSprite);
            /*
            window.Draw(ContinueSprite);
            window.Draw(GoMainMenuSprite);
            window.Draw(ControlsSprite);
            window.Draw(LoadGameSprite);

            für später

            window.Draw(SaveGameSelectedTexture);
            
            */
        }
    }
}
