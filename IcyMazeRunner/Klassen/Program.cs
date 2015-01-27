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
        static void Main(string[] args)
        {
            game = new Game();
            game.run();
        }

    }
}
