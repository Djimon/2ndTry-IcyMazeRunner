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
using IcyMazeRunner.Klassen.Menüs;
using IcyMazeRunner.Klassen.Gamestates_und_Gamestruktur.GUI;

namespace IcyMazeRunner.Klassen
{
    class InGame : GameStates
    {
        /* ~~~~~~~~ VARIABLEN UND KONSTANTEN ~~~~~~~~*/

        static int I_level = 0;
        GameTime gtIngame = new GameTime();

        Map mMap;
        View vIngame;
        Bitmap bmMap;
        GUI Health;
        GUI Kompass;
        GUI Skills;
        GUI Collected;
        GUI SoftPopUp;

        Kompass compass;
        Vector2f vTarget = new Vector2f(-500, 500);
        float targetdistance;

        Player pRunner;

        Sprite spBackGround;
        Sprite spFogOfWar;
        Sprite spKompass;

        InGameMenu menu;

        private int I_typeOfDeath;
        /*
         * 0 = alive
         * 1 = Death by falling
         * 2 = instant Death by trap
         * 3 = Death by damage
        */


        //Bool ersetzt watch-Prüfung?
        public bool B_isDeathAnimationOver;

        Calculator calc;


        /*~~~~~~~~~~~~~~~~~~~Gap Collision~~~~~~~~~~~~~~~~~~~~~~~~~*/

        public static String orange = "ffff8800"; // Loch im Boden



        /*~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~*/

        Sprite spZiel;  // hier map/ blocktype=4 (blue= ziel) ??

        /*~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~*/


        /*~~~~~~~~~~~~~    Getter and Setter    ~~~~~~~~~~~~~~~*/

        public int getTypeOfDeath()
        {
            return I_typeOfDeath;
        }

        public void setTypeOfDeath(int data)
        {
            I_typeOfDeath = data;
        }


        /*~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~*/
        
        
        /* ~~~~ Initialisierung des Spiels ~~~~ */
        public void initialize()
        {

            calc = new Calculator();

            //Moveable_Wall wall = new Moveable_Wall(new Vector2f(0, 0), new Vector2f(0, 0), 1, 1, mMap);
            //wall.wallTrigger.collision(pRunner, new Vector2f(1,1));

            gtIngame = new GameTime();
            gtIngame.start();
           
            // globale fensgtergrößen-vaiable?;
            vIngame = new View(new FloatRect(0, 0, 1058, 718));  // 1% kleiner, als Original, um Side-glitches zu verhindern 

            spBackGround = new Sprite(new Texture("Texturen/Map/background.png"));
            spBackGround.Position = new Vector2f(0, 0);

            spFogOfWar = new Sprite(new Texture("Texturen/Map/Fog_of_War.png"));
            spFogOfWar.Position = new Vector2f(-1,-1);

            Kompass = new GUI();

            setTypeOfDeath(0);
            B_isDeathAnimationOver=false;
            
            // Handler laden
        }

        /* ~~~~ Inhalte laden ~~~~ */
        public void loadContent(){
          
            /*
            Map-Positionen:
            Map_tutorial (190,0)
            Map_2 (2263, 3336)
             */

            bmMap = new Bitmap("Texturen/Map/Map_test.bmp");

          /****************************************
           ************** KOMPASS *****************
           ****************************************/
           



            // mapOfBits = new Bitmap("Texturen/Map_1.bmp");

            // Levelzuweisung
            // Objekte in Handler laden in den jeweiligen Level 

            switch (I_level)
            {
                case 0:
                    mMap = new Map(bmMap); 
                    pRunner = new Player(new Vector2f(281,91), mMap); // 190, 100 bei Map_1 gespeichert gewesen

                    break;

                case 1:
                    bmMap = new Bitmap("Texturen/Map_1.bmp");
                    mMap = new Map(bmMap);
                    pRunner = new Player(new Vector2f(0, 0), mMap);
                    break;
                    
                case 2:
                   // mapOfBits = new Bitmap("Texturen/Map_2.bmp"); alt
                    mMap = new Map(bmMap);
                    pRunner = new Player(new Vector2f(2263,3336), mMap);
                    break;

            }

            for (int row = 0; row < bmMap.Width; row++)
            {
                for (int col = 0; col < bmMap.Height; col++)
                {
                    if (mMap.getBlockType(row, col) == 4)
                        vTarget = new Vector2f(row *90 , col *90); //globale variablen?
                }
            }



            compass = new Kompass(vIngame.Center, vTarget,new Texture("Texturen/Menü+Anzeigen/GUI/needle.png")); //WHY????
          
            //        hier Fallen und Hindernisse laden???
            //         ziel?
           

            
        }


        /* ~~~~ Update ~~~~ */
        public EGameStates update(GameTime gametime)
        {


            if (menu != null)
            {
                
                if (menu != null && menu.getCloseMenu())
                {
                    menu = null;
                }

                // if-Abfrage, falls zwischen obiger if-Abfrage und update menu=null gesetzt wird
                if (menu != null)
                {
                    return menu.update();
                }
            }
            else
            {

                if (!pRunner.getIsPressed() && Keyboard.IsKeyPressed(Keyboard.Key.Escape))
                {
                    menu = new InGameMenu(pRunner);
                    pRunner.setIsPressed(true);
                }


                if (get_Gap_Collision(pRunner, mMap))
                {
                    pRunner.setPlayerHealth(0);
                    setTypeOfDeath(1);
                }
                
                
                /*
                 for(each triggerobject: objecthandler)
                 {
                  if(triggerObjectXY.point_Collision(Runner) && !triggerObjectXY.getB_Movable() )
                    {
                    triggerObjectXY.setB_Movable(true);
                    Vector2f movableWallXY.prevPosition = movableWallXY.position;
                    movableWallXY.I_direction ++;
                    }
                 if(triggerObjectXY.getB_Movable())
                    movableWallXY.move();
                 if (minimum Zeit vergangen) // überflüssig?????
                    {
                        if(((movableWallXY.prevPosition.X+map.getBlocksize == movableWallXY.position.X) oder
                           (movableWallXY.prevPosition.X-map.getBlocksize == movableWallXY.position.X)) und
                           ((movableWallXY.prevPosition.Y+map.getBlocksize == movableWallXY.position.Y) oder
                           (movableWallXY.prevPosition.Y-map.getBlocksize == movableWallXY.position.Y))
                          )
                           triggerObjectXY.setB_Movable(false);
                    }
                }
                 */

                gtIngame.update();

                if (pRunner.getPlayerHealth() == 0)
                {
                    pRunner.DeathAnimation(getTypeOfDeath());


                    if (pRunner.gtDeathWatch.Watch.ElapsedMilliseconds > 4000)
                    {
                        // ob Level sinkt, hängt von Todesursache ab, nur bei Loch im Boden
                        I_level--;
                        return EGameStates.gameOver;
                    }
                }

                targetdistance = calc.getDistance(pRunner.getplayerSprite().Position, vTarget);
                if (targetdistance <200)
                {
                    I_level++;
                    return EGameStates.NextLevel;
                }

                if (I_level == 15)
                {
                    vIngame = new View(new FloatRect(0, 0, 1062, 720)); // globale fensgtergrößen-vaiable?;
                    return EGameStates.gameWon;
                }

                if (Keyboard.IsKeyPressed(Keyboard.Key.O))
                {
                    vIngame = new View(new FloatRect(0, 0, 1062, 720)); // globale fensgtergrößen-vaiable?;
                    return EGameStates.NextLevel;
                }

                pRunner.update(gtIngame);
                /****************************************
                ************** KOMPASS *****************
                ****************************************/
                compass.update();
              
                
                // bewegliche Mauern (if-Abfrage), Kollision mit Schalter
                // später: Bewegung der Gegner, Geschosse, Anzeigen, Kollision

                spBackGround.Position = new Vector2f(vIngame.Center.X - 531, vIngame.Center.Y - 360);  // globale fensgtergrößen-vaiable?;
                spFogOfWar.Position = new Vector2f(vIngame.Center.X - 531, vIngame.Center.Y - 360);  // globale fensgtergrößen-vaiable?;
                vIngame.Move(new Vector2f((pRunner.getXPosition() + (pRunner.getWidth() / 2)), (pRunner.getYPosition() + (pRunner.getHeigth() / 2))) - vIngame.Center);




                /*~~~~~~~Collision mit Ziel, SPrite ziel muss noch übergebenw erden aus (Map/Blocks?)~~~~*/

                //if (point_Collision(Runner.getplayerSprite, (float)Runner.getWidth(), (float)Runner.getHeigth(), ziel.getPosition(), (float)ziel.getWidth(), (float)ziel.getHeight()))
                //{
                //    Console.Write("Collision!!1elf");
                //    view = new View(new FloatRect(0, 0, 1062, 720));
                //    return EGameStates.gameWon;
                //}

            }
            return EGameStates.inGame;
        }

        public static bool point_Collision(Vector2f sprite1, float F_highA, float F_widthA, Vector2f sprite2, float F_highB, float F_widthB)
        {
            Vector2f ObjectA = new Vector2f(sprite1.X + F_widthA / 2, sprite1.Y + F_highA / 2);
            Vector2f ObjectB = new Vector2f(sprite2.X + F_widthB / 2, sprite2.Y + F_highB / 2);

            float F_widthMidA = F_widthA / 2;
            float F_widthMidB = F_widthB / 2;

            float F_heightMidA = F_highA / 2;
            float F_heightMidB = F_highB / 2;

            float F_betragX = Math.Abs(ObjectA.X - ObjectB.X);
            float F_betragY = Math.Abs(ObjectA.Y - ObjectB.Y);

            if (F_betragX < F_widthMidA + F_widthMidB && F_betragY < F_heightMidA + F_heightMidB)
                return true;

            else
                return false;
        }

        public bool get_Gap_Collision(Player player, Map map)
        {
            // Kachel an Spielerposition mit Farbe der Bitmap und damit Kachelfarbe des Lochblocks vergleichen
            if (bmMap.GetPixel((int)((player.getXPosition() + (player.getWidth() / 2)) / map.getBlocksize()) + 1, ((int)((player.getYPosition() + (player.getHeigth()/2))/map.getBlocksize()) + 1)).Name == orange)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

      
        public void draw(RenderWindow win)
        {
            win.Draw(spBackGround);
            win.SetView(vIngame);
            mMap.draw(win);
            pRunner.draw(win);
            win.Draw(spFogOfWar);
            compass.draw(win);
            win.SetMouseCursorVisible(false);
            if (menu != null)
            {
                menu.draw(win);
            }
        }

    }
}
