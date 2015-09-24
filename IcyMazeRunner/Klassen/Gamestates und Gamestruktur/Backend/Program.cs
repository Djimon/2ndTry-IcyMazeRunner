using IcyMazeRunner.Klassen;
using SFML.Graphics;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IcyMazeRunner
{
    class Program
    {
        public static Game game;
        
        /* ~~~~ Einstiegspunkt ~~~~ */
        /* ~~~~ MAIN - Spielstart ~~~~ */

        /// <summary>
        /// Startet das Spiel.
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            game = new Game();
            game.run();
        }

    }
}
