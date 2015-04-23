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

        View MenuView;
        int test;

        

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
        

        Texture ContinueNotSelectedTexture = new Texture("Texturen/Menü+Anzeigen/InGame Menü/null.png");
        Texture GoMainMenuNotSelectedTexture = new Texture("Texturen/Menü+Anzeigen/InGame Menü/null.png.");
        Texture ControlsNotSelectedTexture = new Texture("Texturen/Menü+Anzeigen/InGame Menü/null.png."); // Controls.cs unterscheiden lassen, von wo Controls aufgerufen wurde
        Texture SaveGameNotSelectedTexture = new Texture("Texturen/Menü+Anzeigen/InGame Menü/null.png."); // für später
        Texture LoadGameNotSelectedTexture = new Texture("Texturen/Menü+Anzeigen/InGame Menü/null.png."); // für später


       
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

        public InGameMenu(Player Runner)
        {
            // Initialize menu attributes
            select = 2;
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

            MenuBackgroundSprite.Texture = MenuBackgroundTexture;
            MenuHeaderSprite.Texture = MenuHeaderTexture;


            float scaleX = 0.9f;
            float scaleY = 0.9f;
            float xCoord = Runner.getXPosition()-595;  
            float yCoord = Runner.getYPosition() - 360; 

           
            MenuBackgroundSprite.Position = new Vector2f(xCoord, yCoord); 
            MenuBackgroundSprite.Scale = new Vector2f(scaleX, scaleY);

            MenuHeaderSprite.Position = new Vector2f(xCoord, yCoord); 
            MenuHeaderSprite.Scale = new Vector2f(scaleX, scaleY);

            ContinueSprite.Position = new Vector2f(xCoord, yCoord); 
            ContinueSprite.Scale = new Vector2f(scaleX, scaleY);

            GoMainMenuSprite.Position = new Vector2f(xCoord, yCoord); 
            GoMainMenuSprite.Scale = new Vector2f(scaleX, scaleY);

            ControlsSprite.Position = new Vector2f(xCoord, yCoord);
            ControlsSprite.Scale = new Vector2f(scaleX, scaleY);

            //Beinhaltet momentan "worldmap" was load-funktion beinhalten könnte
            LoadGameSprite.Position = new Vector2f(xCoord, yCoord); 
            LoadGameSprite.Scale = new Vector2f(scaleX, scaleY);

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

            if (select == 1 && select == -1)
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
            if (select == 2 && select == -2)
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

            
            
           
            
            if (select == 3 && select == -3)
            {
                ContinueSprite.Texture = ContinueNotSelectedTexture;
                GoMainMenuSprite.Texture = GoMainMenuNotSelectedTexture;
                ControlsSprite.Texture = ControlsNotSelectedTexture;
                LoadGameSprite.Texture = LoadGameSelectedTexture;
             //   SaveGameSprite.Texture = SaveGameSelectedTexture;
              
            }


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
            window.Draw(ContinueSprite);
            window.Draw(GoMainMenuSprite);
            window.Draw(ControlsSprite);
            window.Draw(LoadGameSprite);
            /*
            für später

            window.Draw(SaveGameSelectedTexture);
            
            */
        }
    }
}
