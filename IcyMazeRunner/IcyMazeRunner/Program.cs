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
        static void Main(string[] args)
        {
            RenderWindow Window = new RenderWindow(new VideoMode(800, 600), "IcyMazeRunner");

            Initialize();

            while (Window.IsOpen())
            {
                update();

            }
            
        }

       static void Initialize(){
           // Spieler, Map, 
           // später: Gegner, Fallen(Geschosse), Anzeige (Timer/Stoppuhr), HP-Balken

       }


       static void update()
       {
           // Bewegungsmethode des Spielers, Sichtkreis, bewegliche Mauern (if-Abfrage), Kollision mit Schalter
           // später: Bewegung der Gegner, Geschosse, Anzeigen, Kollision
       }

       static void Windowclosed(Object sender, EventArgs e)
       {
           ((RenderWindow)sender).Close();
       }
    }
}
