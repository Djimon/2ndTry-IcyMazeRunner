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

        Sprite spWon;


        public void initialize()
        {

            spWon = new Sprite(new Texture("Texturen/Menü+Anzeigen/GameWon.png"));   
            spWon.Position = new Vector2f(0,0);
            // ToDo: Bildposition fixen
            

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
            win.Draw(spWon);
        }
    }
}
