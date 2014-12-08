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

        static Player Runner;

        static void Main(string[] args)
        {
            RenderWindow win = new RenderWindow(new VideoMode(1280, 720), "IcyMazeRunner");

            
            win.Closed += Windowclosed;

            Initialize();

            while (win.IsOpen())
            {
                win.DispatchEvents();
                update();
                draw(win);
            }
        }

       static void Initialize(){
           Runner = new Player();
           // Map (45-50 Blöcke untereinander + eventuell einige durchsichtige Blöcke, um einen Hintergrund drum herum darzustellen.
           // später: Gegner, Fallen(Geschosse), Anzeige (Timer/Stoppuhr), HP-Balken
       }


       static void update()
       {
           Runner.move();
           //Sichtkreis, bewegliche Mauern (if-Abfrage), Kollision mit Schalter
           // später: Bewegung der Gegner, Geschosse, Anzeigen, Kollision
       }

       static void Windowclosed(Object sender, EventArgs e)
       {
           ((RenderWindow)sender).Close();
       }

       static void draw(RenderWindow win)
       {
           win.Clear();
           Runner.draw(win);
           win.Display();
       }
    }
}
