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

        /* ~~~~ Screen anlegen ~~~~*/
        Texture controlsTex;
        Sprite controls;


        /* ~~~~ Screen initialisieren ~~~~*/
        public void initialize()
        {
            controls = new Sprite(controlsTex);
            controls.Position = new Vector2f(0, 0);
        }

        /* ~~~~ Screen laden ~~~~*/
        public void loadContent()
        {
            
                controlsTex = new Texture("Texturen/Menü+Anzeigen/controllscreen.png");
            
        }


        /* ~~~~ Screen aktualisieren ~~~~*/
        public EGameStates update(GameTime time)
        {

          
            controls.Texture = controlsTex;


            // Update der Gamestates

            if (Keyboard.IsKeyPressed(Keyboard.Key.Escape) ) 
            {
                return EGameStates.mainMenu;
            }
            return EGameStates.controls;
            
            
        }

        /* ~~~~ Draw ~~~~ */
        public void draw(RenderWindow win)
        {
            win.Draw(controls);
        }
    }
}
