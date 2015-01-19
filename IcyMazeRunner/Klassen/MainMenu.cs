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

        int x;
        bool isPressed;


        /* ~~~~ Texturen anlegen ~~~~*/
        Texture CreditsNotSelected;
        Texture CreditsSelected;
        Texture ExitNotSelected;
        Texture ExitSelected;
        Texture StartNotSelected;
        Texture StartSelected;
        Texture ControlsNotSelected;
        Texture ControlsSelected;

        Sprite Start;
        Sprite Credits;
        Sprite Exit;
        Sprite Controls;

        Texture backGroundTex;
        Sprite backGround;


        /* ~~~~ Initialisierung und Positionsfestlegung ~~~~ */
        public void initialize()
        {
            x = 0;
            isPressed = false;
            Start = new Sprite(StartNotSelected);
            Start.Scale = new Vector2f(1f, 1f);
            Start.Position = new Vector2f(0, 0);
            Credits = new Sprite(CreditsNotSelected);
            Credits.Position = new Vector2f(0, 0);
            Credits.Scale = new Vector2f(1f, 1f);
            Exit = new Sprite(ExitNotSelected);
            Exit.Position = new Vector2f(0, 0);
            Exit.Scale = new Vector2f(1f, 1f);
            Controls = new Sprite(ControlsNotSelected);
            Controls.Position = new Vector2f(0, 0);
            Controls.Scale = new Vector2f(1f, 1f);

            backGround = new Sprite(backGroundTex);
            backGround.Position = new Vector2f(0, 0);
        }


        /* ~~~~ Laden des Inhalts ~~~~ */
        public void loadContent()
        {
            CreditsNotSelected = new Texture("Texturen/Menü+Anzeigen/credits.png");
            CreditsSelected = new Texture("Texturen/Menü+Anzeigen/credits_s.png");
            ExitNotSelected = new Texture("Texturen/Menü+Anzeigen/quit.png");
            ExitSelected = new Texture("Texturen/Menü+Anzeigen/quit_s.png");
            StartNotSelected = new Texture("Texturen/Menü+Anzeigen/start.png");
            StartSelected = new Texture("Texturen/Menü+Anzeigen/start_s.png");
            ControlsNotSelected = new Texture("Texturen/Menü+Anzeigen/controls.png");
            ControlsSelected = new Texture("Texturen/Menü+Anzeigen/controls_s.png");

            backGroundTex = new Texture("Texturen/Menü+Anzeigen/Titel.png");
        }


        /* ~~~~ Update der GameStates bei Änderung der Auswahl ~~~~ */
        public EGameStates update(GameTime time)
        {

            if (Keyboard.IsKeyPressed(Keyboard.Key.Up) && !isPressed)
            {
                x = (x + 1) % 4;
                isPressed = true;
            }
            if (Keyboard.IsKeyPressed(Keyboard.Key.Down) && !isPressed)
            {
                x = (x - 1) % 4;
                isPressed = true;
            }

            if (!Keyboard.IsKeyPressed(Keyboard.Key.Down) && !Keyboard.IsKeyPressed(Keyboard.Key.Up))
                isPressed = false;

            if (x == 0)
            {
                Start.Texture = StartSelected;
                Controls.Texture = ControlsNotSelected;
                Credits.Texture = CreditsNotSelected;
                Exit.Texture = ExitNotSelected;
            }

            if (x == 1)
            {
                Start.Texture = StartNotSelected;
                Controls.Texture = ControlsSelected;
                Credits.Texture = CreditsNotSelected;
                Exit.Texture = ExitNotSelected;
            }
            if (x == 2)
            {
                Start.Texture = StartNotSelected;
                Controls.Texture = ControlsNotSelected;
                Credits.Texture = CreditsSelected;
                Exit.Texture = ExitNotSelected;
            }                
            if (x == 3)
            {
                Start.Texture = StartNotSelected;
                Controls.Texture = ControlsNotSelected;
                Credits.Texture = CreditsNotSelected;
                Exit.Texture = ExitSelected;
            }

            backGround.Texture = backGroundTex;
            

            if (x == 0 && Keyboard.IsKeyPressed(Keyboard.Key.Space))
                return EGameStates.inGame;
            if (x == 1 && Keyboard.IsKeyPressed(Keyboard.Key.Space))
                return EGameStates.controls;
            if (x == 2 && Keyboard.IsKeyPressed(Keyboard.Key.Space))
                return EGameStates.credits;
            if (x == 3 && Keyboard.IsKeyPressed(Keyboard.Key.Space))
                return EGameStates.none;

            return EGameStates.mainMenu;
        }


        /* ~~~~ Draw ~~~~ */
        public void draw(RenderWindow window)
        {
            window.Draw(backGround);
            window.Draw(Start);
            window.Draw(Credits);
            window.Draw(Exit);
            window.Draw(Controls);
        }
        
    }
}
