using SFML.Graphics;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IcyMazeRunner.Klassen
{
    class Credits : GameStates

        /* ~~~~ Screen anlegen ~~~~*/
    {
        Texture txCredits;

        /* ~~~~ Screen initialisieren ~~~~*/
        public void initialize()
        {
            Game.spBackGround.Texture = txCredits;
            Game.spBackGround.Position = new Vector2f(0, 0);
        }


        /* ~~~~ Screen laden ~~~~*/
        public void loadContent()
        {
            
                txCredits = new Texture("Texturen/Menu+Anzeigen/creditscreen.png");
            
        }


        /* ~~~~ Screen aktualisieren ~~~~*/
        public EGameStates update(GameTime time)
        {
            // ToDo: Wieso Texture aktualisieren?
            Game.spBackGround.Texture = txCredits;

            // Update der Gamestates

            if (Keyboard.IsKeyPressed(Keyboard.Key.Space))
            {
                return EGameStates.credits;
            }

            if (Keyboard.IsKeyPressed(Keyboard.Key.Escape))
            {
                return EGameStates.mainMenu;
            }
            return EGameStates.credits;
        }

        /* ~~~~ Draw ~~~~ */

        public void draw(RenderWindow win)
        {
            win.Draw(Game.spBackGround);
        }
    }
}
