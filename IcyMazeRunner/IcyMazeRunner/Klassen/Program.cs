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
            RenderWindow Window = new RenderWindow(new VideoMode(1280, 720), "IcyMazeRunner");

            Initialize();

            while (Window.IsOpen())
            {
                update();
                draw(Window);

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

       static void draw(RenderWindow Window)
       {
           Window.Clear();
           Runner.draw(Window);
           Window.Display();

       }
    }
}
