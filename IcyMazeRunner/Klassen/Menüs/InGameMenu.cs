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


        Texture ContinueSelectedTexture = new Texture("Texturen/Menü+Anzeigen/InGame Menü/Spiel fortsetzen Platzhalter.png");
        Texture GoMainMenuSelectedTexture = new Texture("Texturen/Menü+Anzeigen/InGame Menü/Hauptmenü Platzhalter.png");
        Texture ControlsSelectedTexture = new Texture("Texturen/Menü+Anzeigen/InGame Menü/Steuerung Platzhalter.png"); // Controls.cs unterscheiden lassen, von wo Controls aufgerufen wurde
        Texture SaveGameSelectedTexture = new Texture("Texturen/Menü+Anzeigen/InGame Menü/Spiel speichern Platzhalter.png"); // für später
        Texture LoadGameSelectedTexture = new Texture("Texturen/Menü+Anzeigen/InGame Menü/Spiel laden Platzhalter.png"); // für später

        Texture ContinueNotSelectedTexture = new Texture("Texturen/Menü+Anzeigen/InGame Menü/Spiel fortsetzen Platzhalter.png");
        Texture GoMainMenuNotSelectedTexture = new Texture("Texturen/Menü+Anzeigen/InGame Menü/Hauptmenü Platzhalter.png");
        Texture ControlsNotSelectedTexture = new Texture("Texturen/Menü+Anzeigen/InGame Menü/Steuerung Platzhalter.png"); // Controls.cs unterscheiden lassen, von wo Controls aufgerufen wurde
        Texture SaveGameNotSelectedTexture = new Texture("Texturen/Menü+Anzeigen/InGame Menü/Spiel speichern Platzhalter.png"); // für später
        Texture LoadGameNotSelectedTexture = new Texture("Texturen/Menü+Anzeigen/InGame Menü/Spiel laden Platzhalter.png"); // für später



        Sprite MenuBackgroundSprite;
        Sprite MenuHeaderSprite;
        Sprite ContinueSprite;
        Sprite GoMainMenuSprite;
        Sprite ControlsSprite; // Controls.cs unterscheiden lassen, von wo Controls aufgerufen wurde
        Sprite SaveGameSprite; // für später
        Sprite LoadGameSprite; // für später

        int select; // which component of Menu is selected
        bool isPressed;

        public InGameMenu()
        {
            // Initialize menu attributes
            select = 0;
            isPressed = false;

            // Construct sprites and textures

            MenuBackgroundTexture = new Texture("Texturen/Menü+Anzeigen/InGame Menü/Menüfenster Platzhalter.png");
            MenuHeaderTexture = new Texture("Texturen/Menü+Anzeigen/InGame Menü/Menü Platzhalter.png");


            MenuBackgroundSprite = new Sprite(MenuBackgroundTexture);
            MenuHeaderSprite = new Sprite(MenuHeaderTexture);
            ContinueSprite = new Sprite(ContinueSelectedTexture);
            GoMainMenuSprite = new Sprite(GoMainMenuNotSelectedTexture);
            ControlsSprite = new Sprite(ControlsNotSelectedTexture);
            /*
            SaveGameSprite = new Sprite(SaveGameNotSelectedTexture);
            LoadGameSprite = new Sprite(LoadGameNotSelectedTexture);
            */

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

            /* für später
            SaveGameSprite.Position = new Vector2f(380f, 442f); // auf 489 anpassen, wenn Auflösung korrigiert
            SaveGameSprite.Scale = new Vector2f(1f, 1f);

            LoadGameSprite.Position = new Vector2f(380f, 519f); // auf 489 anpassen, wenn Auflösung korrigiert
            LoadGameSprite.Scale = new Vector2f(1f, 1f);
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
                /*
            
            für später
            
                SaveGameSprite.Texture = SaveGameNotSelectedTexture;
                LoadGameSprite.Texture = LoadGameNotSelectedTexture;
                */
            }

            if (select == 1)
            {
                ContinueSprite.Texture = ContinueNotSelectedTexture;
                GoMainMenuSprite.Texture = GoMainMenuSelectedTexture;
                ControlsSprite.Texture = ControlsNotSelectedTexture;
                /*
            
            für später
            
                SaveGameSprite.Texture = SaveGameNotSelectedTexture;
                LoadGameSprite.Texture = LoadGameNotSelectedTexture;
                */
            }
            if (select == 2)
            {
                ContinueSprite.Texture = ContinueNotSelectedTexture;
                GoMainMenuSprite.Texture = GoMainMenuNotSelectedTexture;
                ControlsSprite.Texture = ControlsSelectedTexture;
            /*
            
            für später
            
                SaveGameSprite.Texture = SaveGameNotSelectedTexture;
                LoadGameSprite.Texture = LoadGameNotSelectedTexture;
            */
            }

            /*
            
            für später
            
            if (select == 3)
            {
                ContinueSprite.Texture = ContinueNotSelectedTexture;
                GoMainMenuSprite.Texture = GoMainMenuNotSelectedTexture;
                ControlsSprite.Texture = ControlsNotSelectedTexture;
                SaveGameSprite.Texture = SaveGameSelectedTexture;
                LoadGameSprite.Texture = LoadGameNotSelectedTexture;
            }

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
                return EGameStates.inGame;
            if (select == 1 && Keyboard.IsKeyPressed(Keyboard.Key.Return))
                return EGameStates.mainMenu;
            if (select == 2 && Keyboard.IsKeyPressed(Keyboard.Key.Return))
                return EGameStates.controls;
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
            window.Draw(ContinueSprite);
            window.Draw(GoMainMenuSprite);
            window.Draw(ControlsSprite);

            /*

            für später

            window.Draw(SaveGameSelectedTexture);
            window.Draw(LoadGameSprite
            */
        }
    }
}
