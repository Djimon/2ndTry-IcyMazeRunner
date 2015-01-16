using SFML.Graphics;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IcyMazeRunner.Klassen;

namespace IcyMazeRunner.Klassen
{
    class InGame : GameStates
    {
        /* ~~~~~~~~ VARIABLEN UND KONSTANTEN ~~~~~~~~*/

        int level;
        GameTime time = new GameTime();

        Map map;
        View view;

        Player Runner;

        Texture backGroundTex;
        Sprite backGround;


        /* ~~~~ Initialisierung des Spiels ~~~~ */
        public void initialize()
        {
            level = 0;

            time = new GameTime();
            time.start();
            view = new View(new FloatRect(0, 0, 1280, 720));

            backGround = new Sprite(backGroundTex);
            backGround.Position = new Vector2f(0, 0);
        }

        /* ~~~~ Inhalte laden ~~~~ */
        public void loadContent(){
            backGroundTex = new Texture("Texturen/Map/BG.jpg");

            //Map_tutorial (190,0)
           // Map_2 (2263, 3336)

            switch (level)
            {
                case 0:
                    map = new Map(new Bitmap("Texturen/Map/Map_tutorial.bmp"));
                    Runner = new Player(new Vector2f(190, 0), map);

                    break;
            }

          
            //        hier Fallen und Hindernisse laden???
            //         ziel?
           

            //        case 1:
            //            map = new Map(new Bitmap("Texturen/Map_tutorial.bmp"));
            //            break;
            //    }


            
        }


        /* ~~~~ Update ~~~~ */
        public EGameStates update(GameTime gametime)
        {
            time.update();
            //if-Abfrage gewonnen
            //if-Abfrage-Level

            Runner.update(time);
            //Sichtkreis, bewegliche Mauern (if-Abfrage), Kollision mit Schalter
            // später: Bewegung der Gegner, Geschosse, Anzeigen, Kollision

            backGround.Position = new Vector2f(view.Center.X - 640, view.Center.Y - 360);
            view.Move(new Vector2f((Runner.getXPosition() + (Runner.getWidth() / 2)), (Runner.getYPosition() + (Runner.getHeigth() / 2))) - view.Center);


            return EGameStates.inGame;
        }

        public void draw(RenderWindow win)
        {
            win.Draw(backGround);
            win.SetView(view);
            map.draw(win);
            Runner.draw(win);
        }
        
    }
}
