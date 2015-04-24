using SFML.Graphics;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IcyMazeRunner.Klassen.Gamestates_und_Gamestruktur
{
    class NextLevel : GameStates
    {
        Sprite NextLevelScreen;


        public void initialize()
        {

            // passende Textur einfügen

            NextLevelScreen = new Sprite(new Texture("Texturen/Menü+Anzeigen/GameWon.png"));

            NextLevelScreen.Position = new Vector2f(0, 0);
            // Bildposition fixen


        }

        public void loadContent()
        {

        }
        public EGameStates update(GameTime time)
        {

            if (Keyboard.IsKeyPressed(Keyboard.Key.Return))
            {
                return EGameStates.inGame;
            }
            return EGameStates.NextLevel;
        }

        public void draw(RenderWindow win)
        {
            //win.Draw(backGround);
            win.Draw(NextLevelScreen);
        }
    }
}
