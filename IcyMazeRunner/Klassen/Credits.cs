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
    {
        Texture creditsTex;
        Sprite credits;

        
        public void initialize()
        {
            credits = new Sprite(creditsTex);
            credits.Position = new Vector2f(0, 0);
        }

        public void loadContent()
        {
            
                creditsTex = new Texture("Texturen/Menü+Anzeigen/creditscreen.png");
            
        }

        public EGameStates update(GameTime time)
        {
            credits.Texture = creditsTex;

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

        public void draw(RenderWindow win)
        {
            win.Draw(credits);
        }
    }
}
