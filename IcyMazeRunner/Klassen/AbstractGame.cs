
using SFML.Graphics;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IcyMazeRunner.Klassen;
using SFML.Audio;

namespace IcyMazeRunner.Klassen
{
    


    public abstract class AbstractGame
    {
        public RenderWindow win;
        GameTime gameTime;
        Music atmo;
        

        public AbstractGame(uint width, uint height, String title)
        {
            // Fensterkontrolle

            win = new RenderWindow(new VideoMode(width, height), title);

            win.Closed += Windowclosed;

            gameTime = new GameTime();
        }
        /* ~~~~~~~~~~~~~~~~ LAUFENDES SPIEL ~~~~~~~~~~~~~~~~*/
        public void run()
        {
            gameTime.start();
            atmo = new Music("Texturen/sound/atmo_music.ogg");
            atmo.Play();
            atmo.Volume = 100;
            atmo.Loop = true;

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
