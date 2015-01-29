using SFML.Graphics;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IcyMazeRunner.Klassen
{
    class Controls : GameStates
    {
        Texture controlsTex;
        Sprite controls;
        bool prevSeason=false;

        public void initialize()
        {
            controls = new Sprite(controlsTex);
            controls.Position = new Vector2f(0, 0);
        }

        public void loadContent()
        {
            if (!Game.is_Summer)
                controlsTex = new Texture("Texturen/Menü+Anzeigen/controllscreen.png");
            else
                controlsTex = new Texture("Texturen/Menü+Anzeigen/controllscreen-summer-PLATZHALTER.png");
        }

        public EGameStates update(GameTime time)
        {

            // Steuerung der Themes

            if (Keyboard.IsKeyPressed(Keyboard.Key.Numpad5))
            {
                Game.is_Summer = true;
            }

            if (Keyboard.IsKeyPressed(Keyboard.Key.Numpad3))
            {
                Game.is_Summer = false;
            }

            // Textur wird nur zugewiesen, wenn sich der Bool-Wert ändert

            if (prevSeason != Game.is_Summer)
            {
                if (Game.is_Summer)
                {
                    controlsTex = new Texture("Texturen/Menü+Anzeigen/controllscreen-summer-PLATZHALTER.png");
                }
                else controlsTex = new Texture("Texturen/Menü+Anzeigen/controllscreen.png");
            }

            controls.Texture = controlsTex;
            
            // Bool-Werte werden abgeglichen
            prevSeason = Game.is_Summer;

            // Steuerungselement

            if (Keyboard.IsKeyPressed(Keyboard.Key.Space)) 
            {
                return EGameStates.controls;
            }

            if (Keyboard.IsKeyPressed(Keyboard.Key.Escape) ) 
            {
                return EGameStates.mainMenu;
            }
            return EGameStates.controls;
            
            
        }

        public void draw(RenderWindow win)
        {
            win.Draw(controls);
        }
    }
}
