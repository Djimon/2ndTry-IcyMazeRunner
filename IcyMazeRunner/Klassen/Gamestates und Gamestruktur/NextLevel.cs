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
        /* ~~~~ Screen anlegen ~~~~*/
        Texture NextLevelScreenTexture;
        Sprite NextLevelScreen;


        /* ~~~~ Screen initialisieren ~~~~*/
        public void initialize()
        {
            NextLevelScreen = new Sprite(NextLevelScreenTexture);
            NextLevelScreen.Position = new Vector2f(0, 0);
        }


        /* ~~~~ Screen laden ~~~~*/
        public void loadContent()
        {
            // passende Textur einfügen
            NextLevelScreenTexture = new Texture("Texturen/Menü+Anzeigen/GameWon.png");           
        }


        /* ~~~~ Screen aktualisieren ~~~~*/
        public EGameStates update(GameTime time)
        {

            if (Keyboard.IsKeyPressed(Keyboard.Key.Return))
            {
                return EGameStates.inGame;
            }
            return EGameStates.NextLevel;
        }


        /* ~~~~ Draw ~~~~ */
        public void draw(RenderWindow win)
        {
            win.Draw(NextLevelScreen);
        }
    }
}
