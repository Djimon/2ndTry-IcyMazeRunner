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
        Sprite spCredits;



        /* ~~~~ Screen initialisieren ~~~~*/
        public void initialize()
        {
            spCredits = new Sprite(txCredits);
            spCredits.Position = new Vector2f(0, 0);
        }


        /* ~~~~ Screen laden ~~~~*/
        public void loadContent()
        {
            
                txCredits = new Texture("Texturen/Menü+Anzeigen/creditscreen.png");
            
        }


        /* ~~~~ Screen aktualisieren ~~~~*/
        public EGameStates update(GameTime time)
        {
            spCredits.Texture = txCredits;

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
            win.Draw(spCredits);
        }
    }
}
