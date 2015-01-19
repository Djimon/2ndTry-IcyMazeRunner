using SFML.Graphics;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IcyMazeRunner.Klassen
{
    class gameWon : GameStates
    {

        Texture WonTex;
        Sprite Won;
        //Sprite backGround;
        //Texture backTex;

        public void initialize()
        {
            Won = new Sprite(WonTex);
            Won.Position = new Vector2f(0, 0);

        }

        public void loadContent()
        {
            //backTex = new Texture("Textures/Map/BG.jpg");
            //backGround = new Sprite(backTex);
            //backGround.Position = new Vector2f(0, 0);
            WonTex = new Texture("Textures/Menü+Anzeigen/GameWon.png");
        }
        public EGameStates update(GameTime time)
        {
            if (Keyboard.IsKeyPressed(Keyboard.Key.Space))
            {
                return EGameStates.mainMenu;
            }
            return EGameStates.gameWon;
        }

        public void draw(RenderWindow win)
        {
            //win.Draw(backGround);
            win.Draw(Won);
        }
    }
}
