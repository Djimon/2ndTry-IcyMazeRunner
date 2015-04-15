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
        

        public void initialize()
        {
            controls = new Sprite(controlsTex);
            controls.Position = new Vector2f(0, 0);
        }

        public void loadContent()
        {
            
                controlsTex = new Texture("Texturen/Menü+Anzeigen/controllscreen.png");
            
        }

        public EGameStates update(GameTime time)
        {

          
            controls.Texture = controlsTex;
            

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
