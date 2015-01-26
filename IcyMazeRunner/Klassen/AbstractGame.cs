
using SFML.Graphics;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IcyMazeRunner.Klassen;

namespace IcyMazeRunner.Klassen
{
    


    public abstract class AbstractGame
    {
        public RenderWindow win;
        GameTime gameTime;
        

        public AbstractGame(uint width, uint height, String title)
        {


            win = new RenderWindow(new VideoMode(width, height), title);

            win.Closed += Windowclosed;

            gameTime = new GameTime();
        }
        /* ~~~~~~~~~~~~~~~~ LAUFENDES SPIEL ~~~~~~~~~~~~~~~~*/
        public void run()
        {
            gameTime.start();

            while (win.IsOpen())
            {
                win.Clear();
                win.DispatchEvents();

                gameTime.update();
                update(gameTime);

                draw(win);


                win.Display();
            }

            gameTime.stop();
        }

        /* ~~~~ Update ~~~~ */

        public abstract void update(GameTime gameTime);

        /* ~~~~ Draw ~~~~ */

        public abstract void draw(RenderWindow win);

        /* ~~~~~~~~~~~ FENSTER-CHECK ~~~~~~~~~~~~*/

        public void Windowclosed(Object sender, EventArgs e)
        {
            ((RenderWindow)sender).Close();
        }
    }
}
