using SFML.Graphics;
using SFML.System;
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

        // GameOver durch Fallen

        Texture txGameOver;

        public void initialize()
        {
            Game.spBackGround.Texture = txGameOver;
            Game.spBackGround.Position = new Vector2f(0, 0);
        }

        public void loadContent()
        {
            
                txGameOver = new Texture("Texturen/Menu+Anzeigen/GameOver-PLATZHALTER.png");
            
        }

        public EGameStates update(GameTime time)
        {
            // ToDo: Wieso Texture aktualisieren?
            Game.spBackGround.Texture = txGameOver;

            // Update der Gamestates

            if (Keyboard.IsKeyPressed(Keyboard.Key.Space))
            {
                return EGameStates.gameOver;
            }

            if (Keyboard.IsKeyPressed(Keyboard.Key.Escape))
            {
                return EGameStates.mainMenu;
            }

            return EGameStates.gameOver;
        }

        public void draw(RenderWindow win)
        {
            win.Draw(Game.spBackGround);
        }
    }
}
