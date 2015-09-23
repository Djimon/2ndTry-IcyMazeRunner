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

        public void initialize()
        {

            Game.spBackGround.Texture = new Texture("Texturen/Menu+Anzeigen/GameWon.png");
            Game.spBackGround.Position = new Vector2f(0, 0);
            // ToDo: testen, ob gefixed
            

        }

        public void loadContent()
        {
           
        }
        public EGameStates update(GameTime time)
        {
            
            if (Keyboard.IsKeyPressed(Keyboard.Key.Return))
            {
                return EGameStates.mainMenu;
            }
            return EGameStates.gameWon;
        }

        public void draw(RenderWindow win)
        {
            //win.Draw(backGround);
            win.Draw(Game.spBackGround);
        }
    }
}
