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
        Texture gameOverTex;
        Sprite gameOver;


        public void initialize()
        {
            gameOver = new Sprite(gameOverTex);
            gameOver.Position = new Vector2f(0, 0);
        }

        public void loadContent()
        {
            if (!Game.is_Summer)
                gameOverTex = new Texture("Texturen/Menü+Anzeigen/GameOver-PLATZHALTER.png");
            else
                gameOverTex = new Texture("Texturen/Menü+Anzeigen/GameOver-summer-PLATZHALTER.png");
        }

        public EGameStates update(GameTime time)
        {
            gameOver.Texture = gameOverTex;

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
            win.Draw(gameOver);
        }
    }
}
