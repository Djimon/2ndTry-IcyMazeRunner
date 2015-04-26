using SFML.Graphics;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IcyMazeRunner.Klassen
{
    class GameOver : GameStates
    {

        //GameOver durch Fallen

        Texture txGameOver;
        Sprite spGameOver;


        public void initialize()
        {
            spGameOver = new Sprite(txGameOver);
            spGameOver.Position = new Vector2f(0, 0);
        }

        public void loadContent()
        {
            
                txGameOver = new Texture("Texturen/Menü+Anzeigen/GameOver-PLATZHALTER.png");
            
        }

        public EGameStates update(GameTime time)
        {
            spGameOver.Texture = txGameOver;

            // Update der Gamestates

            if (Keyboard.IsKeyPressed(Keyboard.Key.Space))
            {
                return EGameStates.gameOver;
            }

            if (Keyboard.IsKeyPressed(Keyboard.Key.Escape))
            {
                return EGameStates.mainMenu;
            }

      /*    grafik updaten mit "Next-Level"
            if (Keyboard.IsKeyPressed(Keyboard.Key.Return))
            {
                return EGameStates.NextLevel;
            }
       */

            return EGameStates.gameOver;
        }

        public void draw(RenderWindow win)
        {
            win.Draw(spGameOver);
        }
    }
}
