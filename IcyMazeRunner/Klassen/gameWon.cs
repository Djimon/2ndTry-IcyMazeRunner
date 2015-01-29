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

        Sprite Won;


        public void initialize()
        {

            // Auswahl der Textur nach Thema

            if (!Game.is_Summer)
                Won = new Sprite(new Texture("Texturen/Menü+Anzeigen/GameWon1.png"));
            else
                Won = new Sprite(new Texture("Texturen/Menü+Anzeigen/GameWon1-summer-PLATZHALTER.png"));
            Won.Position = new Vector2f(0,0);
            // Bildposition fixen
            

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
            win.Draw(Won);
        }
    }
}
