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
        Texture ContinueTexture;
        Texture GoMainMenuTexture;
        Texture ControlsTexture; // Controls.cs unterscheiden lassen, von wo Controls aufgerufen wurde
        Texture SaveGameTexture; // für später
        Texture LoadGameTexture; // für später

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
            ContinueTexture = new Texture("Texturen/Menü+Anzeigen/InGame Menü/Spiel fortsetzen Platzhalter.png");
            GoMainMenuTexture = new Texture("Texturen/Menü+Anzeigen/InGame Menü/Hauptmenü Platzhalter.png");
            ControlsTexture = new Texture("Texturen/Menü+Anzeigen/InGame Menü/Steuerung Platzhalter.png");
            SaveGameTexture = new Texture("Texturen/Menü+Anzeigen/InGame Menü/Spiel speichern Platzhalter.png");
            LoadGameTexture = new Texture("Texturen/Menü+Anzeigen/InGame Menü/Spiel laden Platzhalter.png");

            MenuBackgroundSprite = new Sprite(MenuBackgroundTexture);
            MenuHeaderSprite = new Sprite(MenuHeaderTexture);
            ContinueSprite  = new Sprite(ContinueTexture);
            GoMainMenuSprite = new Sprite(GoMainMenuTexture);
            ControlsSprite = new Sprite(ControlsTexture);
            /*
            SaveGameSprite = new Sprite(SaveGameTexture);
            LoadGameSprite = new Sprite(LoadGameTexture);
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

        public void update(){

        }

    }
}
