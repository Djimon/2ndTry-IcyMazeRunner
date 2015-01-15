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
/* ~~~~~~~~~~~ VARIABLEN UND KONSTANTEN ~~~~~~~~~~~~*/
        //Attribute hier:
        // Map-Klasse erstellen, walkable als Methode

        static Player Runner;
        static Map map = new Map(new Bitmap("Texturen/Map/Map_tutorial.bmp"));
        static Sprite Bg; //Background
        static Texture BGTex;
        static int level;
        static View view;

/* ~~~~~~~~~~~ MAIN - SPIELSTART ~~~~~~~~~~~~*/
        static void Main(string[] args)
        {
            RenderWindow win = new RenderWindow(new VideoMode(1280, 720), "IcyMazeRunner");

            GameTime time = new GameTime();
           
            
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

 /* ~~~~~~~~~~~ SPIEL - INITIALISIERUNG ~~~~~~~~~~~~*/
       static void Initialize(){
           
           Runner = new Player();
           Bg = new Sprite(BGTex);
           Bg.Position = new Vector2f(0, 0);
           level = 0;
           view = new View(new FloatRect(0, 0, 1280, 720));
<<<<<<< HEAD
           
=======
>>>>>>> 2c1e8813f184e3af04c7b60d390846cc1e5fc84d
           // Map (45-50 Blöcke untereinander + eventuell einige durchsichtige Blöcke, um einen Hintergrund drum herum darzustellen.
           // später: Gegner, Fallen(Geschosse), Anzeige (Timer/Stoppuhr), HP-Balken

       }

 /* ~~~~~~~~~~~ KONTENT LADEN ~~~~~~~~~~~~*/
       static void loadContent()
       {
           BGTex = new Texture("Texturen/Map/BG.jpg");

           switch (level)
           {
               case 0:
                   map = new Map(new Bitmap("Texturen/Map_tutorial.bmp"));
                   //hier Falen und Hindernisse laden???
                   // ziel?
                   break;
           }
                  
       
       }


 /* ~~~~~~~~~~~ UPDATE - METHODE ~~~~~~~~~~~~*/
       static void update(Map map, GameTime time)
       {
           
           Runner.move(map, time);
<<<<<<< HEAD
           view.Move(new Vector2f((Runner.getXPosition()+(Runner.getWidth()/2)), (Runner.getYPosition()+(Runner.getHeigth()/2)))-view.Center);
=======
           updateView(Runner.getplayerSprite().Position);

>>>>>>> 2c1e8813f184e3af04c7b60d390846cc1e5fc84d
           
           //Sichtkreis, bewegliche Mauern (if-Abfrage), Kollision mit Schalter
           // später: Bewegung der Gegner, Geschosse, Anzeigen, Kollision
       }


 /* ~~~~~~~~~~~ FENSTER-CHECK ~~~~~~~~~~~~*/
       static void Windowclosed(Object sender, EventArgs e)
       {
           ((RenderWindow)sender).Close();
       }


 /* ~~~~~~~~~~~ MALEN  ~~~~~~~~~~~~*/


       static void updateView(Vector2f Runner)
       {
            view.Move(new Vector2f(Runner.X , Runner.Y) - view.Center);
           if (view.Center.X < 640)
               return ;
       }


       static void draw(RenderWindow win, GameTime time)
       {
           win.Clear();
<<<<<<< HEAD
           win.Draw(Bg);
=======
           win.SetView(view);
>>>>>>> 2c1e8813f184e3af04c7b60d390846cc1e5fc84d
           map.draw(win);
           Runner.draw(win);
           
           win.Display();
<<<<<<< HEAD

=======
           
>>>>>>> 2c1e8813f184e3af04c7b60d390846cc1e5fc84d

       }
    }
}
