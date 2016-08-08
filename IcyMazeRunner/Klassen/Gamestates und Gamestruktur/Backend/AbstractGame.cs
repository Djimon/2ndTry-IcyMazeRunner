
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

        // ToDo: Reihenfolge der Aufrufe beachten
        /// <summary>
        /// GameTime der Spielprozesse.
        /// <para>0 - LastChance</para>
        /// <para></para>
        /// <para></para>
        /// <para></para>
        /// <para></para>
        /// <para></para>
        /// </summary>
        public static GameTime gameTime;

        Music atmo;
        // ToDo: MusicController.cs erstellen

        /// <summary>
        /// Fenster wird geöffnet und Spielzeit initialisiert.
        /// </summary>
        public AbstractGame(uint width, uint height, String title)
        {
            // Fensterkontrolle

            win = new RenderWindow(new VideoMode(width, height), title); //,Styles.Fullscreen

            win.Closed += Windowclosed;

            gameTime = new GameTime();
        }
        /* ~~~~~~~~~~~~~~~~ LAUFENDES SPIEL ~~~~~~~~~~~~~~~~*/

        /// <summary>
        /// Spiel wird gestartet. Die allgemeine Spielzeit beginnt zu laufen, die Musik beginnt und die Gameloop ausgeführt.
        /// </summary>
        public void run()
        {
            gameTime.start();
            atmo = new Music("Texturen/sound/atmo_music.ogg");
            atmo.Play();
            atmo.Volume = 100;
            atmo.Loop = true;

            while (win.IsOpen)
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

        /// <summary>
        /// Kontrolle, ob Fenster geschlossen werden soll.
        /// </summary>
        public void Windowclosed(Object sender, EventArgs e)
        {
            ((RenderWindow)sender).Close();
        }
    }
}
