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
        Texture txControls;

        /* ~~~~ Screen initialisieren ~~~~*/
        public void initialize()
        {
            Game.spBackGround.Texture = txControls;
            Game.spBackGround.Position = new Vector2f(0, 0);
            Game.spBackGround.Scale = new Vector2f(0.83f, 1f);
        }

        /* ~~~~ Screen laden ~~~~*/
        public void loadContent()
        {
            
                txControls = new Texture("Texturen/Menu+Anzeigen/ControlsBG.png");
            
        }


        /* ~~~~ Screen aktualisieren ~~~~*/
        public EGameStates update(GameTime time)
        {

            // ToDo: Wieso Texture aktualisieren?
            Game.spBackGround.Texture = txControls;


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
            win.Draw(Game.spBackGround);
        }


        public static Boolean IsAnyKeyPressed()
        {
            for (int i = 0; i < (int)Keyboard.Key.KeyCount; i++ )
            {
                if (Keyboard.IsKeyPressed((Keyboard.Key)i))
                return true; 
            }
                return false;
        }

        public static int WhichKeyIsPressed()
        {
            for (int i = 0; i < (int)Keyboard.Key.KeyCount; i++)
            {
                if (Keyboard.IsKeyPressed((Keyboard.Key)i))
                    return i;
            }
            return -1;
        }
    }
}
