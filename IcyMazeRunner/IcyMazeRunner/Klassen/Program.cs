using IcyMazeRunner.Klassen;
using SFML.Graphics;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IcyMazeRunner
{
    class Program
    {

        //Attribute hier:
        // Map-Klasse erstellen, walkable als Methode

        static Player Runner;

        static void Main(string[] args)
        {
            RenderWindow win = new RenderWindow(new VideoMode(1280, 720), "IcyMazeRunner");

            GameTime time = new GameTime();
            Map map = new Map();
            
            win.Closed += Windowclosed;

            Initialize();
            time.start();

            while (win.IsOpen())
            {
                win.DispatchEvents();
                time.update();
                update(map, time);
                draw(win, time);
            }

            time.stop();
        }

       static void Initialize(){
           Runner = new Player();
           // Map (45-50 Blöcke untereinander + eventuell einige durchsichtige Blöcke, um einen Hintergrund drum herum darzustellen.
           // später: Gegner, Fallen(Geschosse), Anzeige (Timer/Stoppuhr), HP-Balken
       }


       static void update(Map map, GameTime time)
       {
           Runner.move(map, time);
           
           //Sichtkreis, bewegliche Mauern (if-Abfrage), Kollision mit Schalter
           // später: Bewegung der Gegner, Geschosse, Anzeigen, Kollision
       }

       static void Windowclosed(Object sender, EventArgs e)
       {
           ((RenderWindow)sender).Close();
       }

       static void draw(RenderWindow win, GameTime time)
       {
           win.Clear();
           Runner.draw(win);
           win.Display();
       }
    }
}
