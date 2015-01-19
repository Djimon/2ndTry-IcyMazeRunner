using SFML.Graphics;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IcyMazeRunner.Klassen;
using SFML.Audio;

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
        Music atmo;
        
        /* ~~~~ Initialisierung des Spiels ~~~~ */
        public void initialize()
        {
           

            //atmo = new Music("Texturen/music+sound/atmo_music.mp3");
            time = new GameTime();
            time.start();
            view = new View(new FloatRect(0, 0, 1280, 720));

            backGround = new Sprite(new Texture("Texturen/Map/background.png"));
            backGround.Position = new Vector2f(0, 0);
          
        }

        /* ~~~~ Inhalte laden ~~~~ */
        public void loadContent(){
            // backGroundTex = new Texture("Texturen/Map/background.png");
            //atmo.Play();
            //atmo.Loop = true;
            //Map_tutorial (190,0)
           // Map_2 (2263, 3336)

            level = 1;
            switch (level)
            {
                case 0:
                    map = new Map(new Bitmap("Texturen/Map/Map_tutorial.bmp"));
                    Runner = new Player(new Vector2f(190, 0), map);

                    break;

                case 1:
                    map = new Map(new Bitmap("Texturen/Map_1.bmp"));
                    Runner = new Player(new Vector2f(0, 0), map);
                    break;
                    
                case 2:
                    map = new Map(new Bitmap("Texturen/Map_2.bmp"));
                    Runner = new Player(new Vector2f(2263,3336), map);
                    break;

            }

          
            //        hier Fallen und Hindernisse laden???
            //         ziel?
           

            //     


            
        }


        /* ~~~~ Update ~~~~ */
        public EGameStates update(GameTime gametime)
        {
            time.update();
            //if-Abfrage gewonnen
            //if-Abfrage-Level

            if (Keyboard.IsKeyPressed(Keyboard.Key.O))
                return EGameStates.gameWon;

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
