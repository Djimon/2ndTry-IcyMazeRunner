﻿using SFML.Graphics;
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
using IcyMazeRunner.Klassen.Gameplay;

namespace IcyMazeRunner.Klassen
{
    class InGame : GameStates
    {
        /* ~~~~~~~~ VARIABLEN UND KONSTANTEN ~~~~~~~~*/

        Blocks[,] map;
        // für Bitmap-Durchlauf der gesamten Map.

        /// <summary>
        ///  Speichert, ob geheime Wege sichtbar sind, oder nicht. Nur auslösender Boolean.
        /// </summary>
        static Boolean B_IsVisible = false;

        /// <summary>
        ///  Zeigt Level des Spiels an.
        /// </summary>
        static int I_level = 0;

        /// <summary>
        ///  Allgemeine Spielzeituhr.
        /// </summary>
        GameTime gtIngame = new GameTime();

        /// <summary>
        ///  Stoppuhr, um zu regeln, wie lange Geheime Wege sichtbar sind.
        /// </summary>
        GameTime gtWallTimer;

        /// <summary>
        ///  Liste, um zu speichern, an welcher Stelle sich geheime Wege befinden.
        /// </summary>
        List<Coordinates> SecretWayList;


        Map mMap;
        View vIngame;
        Bitmap bmMap;
        GUI Health;
        GUI Kompass;
        GUI Skills;
        GUI Collected;
        GUI SoftPopUp;

        Kompass compass;
        Vector2f vTarget;

       // System.Drawing.Image bmKompass;
        
        float targetdistance;

        Player pRunner;
        Enemy eTest;

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

        /*~~~~~~~~~~~~~~~~~~~ Handler ~~~~~~~~~~~~~~~~~~~~~~~~~*/

        GameObjectHandler GOH;
        EntityHandler EH;
        MoveableWallHandler MWH;


        /*~~~~~~~~~~~~~~~~~~~Gap Collision~~~~~~~~~~~~~~~~~~~~~~~~~*/

        public static String Sorange = "ffff8800"; // Loch im Boden



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

            Kompass = new GUI(vIngame);

            setTypeOfDeath(0);
            B_isDeathAnimationOver=false;
            
            GOH = new GameObjectHandler(calc);

            

        }

        /* ~~~~ Inhalte laden ~~~~ */
        public void loadContent(){
          
            /*
            Map-Positionen:
            Map_tutorial (190,0)
            Map_2 (2263, 3336)
             */


            bmMap = new Bitmap("Texturen/Map/Map_test.bmp");
          //  bmMap = new Bitmap("Texturen/Map/KI-test.bmp");

          /****************************************
           ************** KOMPASS *****************
           ****************************************/
          // bmKompass = new System.Drawing.Bitmap("Texturen/Menü+Anzeige/GUI/Untitled-1.png");




            // mapOfBits = new Bitmap("Texturen/Map_1.bmp");

            // Levelzuweisung
            // Objekte in Handler laden in den jeweiligen Level 

            switch (I_level)
            {
                case 0:
                    mMap = new Map(bmMap); 
                    pRunner = new Player(new Vector2f(1,1), mMap); //(281,91)

                    break;

                case 1:
                    bmMap = new Bitmap("Texturen/Map_1.bmp"); // 190, 100 bei Map_1 gespeichert gewesen
                    mMap = new Map(bmMap);
                    pRunner = new Player(new Vector2f(0, 0), mMap);
                    break;
                    
                case 2:
                   // mapOfBits = new Bitmap("Texturen/Map_2.bmp"); alt
                    mMap = new Map(bmMap);
                    pRunner = new Player(new Vector2f(2263,3336), mMap);
                    break;

                default:
                    mMap = new Map(bmMap); 
                    pRunner = new Player(new Vector2f(1,1), mMap); //(281,91)

                    break;

            }

            map = new Blocks[bmMap.Width, bmMap.Height];
            SecretWayList = new List<Coordinates>();


            //komplette Bitmap durchgehen
            for (int row = 0; row < map.GetLength(0); row++)
            {
                for (int col = 0; col < map.GetLength(1); col++)
                {

                    // Wenn Geheimer Weg gefunden, dann füge Daten in Liste
                    if ((map[row, col].type() == 7) || (map[row, col].type() == 8))
                    {
                        Boolean helper = false;

                        // Wenn die Textur über diesem Feld zu einer Vorderansicht einer Mauer gewechselt werden muss, setze helper auf true;
                        if ((map[row+1, col].type() !=7 ) && (map[row+1, col].type() != 8) && (map[row, col].type() != 7))
                        {
                            helper = true;
                        }

                        // Füge Koordinaten und Boolean in Liste ein.
                        SecretWayList.Add(new Coordinates(row, col, helper));
                    }

                }
            }

            // Festlegung des Zielvekotrs für den Kompass (buggy)
            //for (int row = 1; row <= bmMap.Width; row++)
            //{
            //    for (int col = 1; col <= bmMap.Height; col++)
            //    {
            //        if (mMap.getBlockType(row, col) == 4)
            //        {
            //            vTarget = new Vector2f(row * 90, col * 90); //globale variablen?
            //            break;
            //        }
            //        else vTarget = new Vector2f(0, 0);
            //    }
            //}

            //Test-Enemy für KI
            eTest = new Enemy(new Vector2f(251,251), "Texturen/Enemies/downidle.png");

            spKompass = new Sprite(new Texture("Texturen/Menü+Anzeigen/GUI/needle.png"));
            compass = new Kompass(vIngame.Center, vTarget,spKompass.Texture); 

            
          //compass = new Kompass(vIngame.Center, vTarget, bmKompass);

          
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



                GOH.update(gametime, pRunner, mMap);

                gtIngame.update();

                if (pRunner.getPlayerHealth() <= 0)
                {
                    pRunner.DeathAnimation(getTypeOfDeath());

                    // ToDo: Property-Getter einfügen
                    if (pRunner.B_IsSaved)
                    {
                        // Spieler hat wieder Leben
                        // ToDo: variablen Wert eingeben
                        pRunner.setPlayerHealth(20);
                        pRunner.setDeathWatchIsOn(false);
                    }

                    if (pRunner.gtDeathWatch.Watch.ElapsedMilliseconds > 4000)
                    {
                        // ob Level sinkt, hängt von Todesursache ab, nur bei Loch im Boden
                        if (getTypeOfDeath()==1)
                        {
                            I_level--;
                        }
                        pRunner.setDeathWatchIsOn(false);
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

                pRunner.update(gtIngame, GameObjectHandler.moveableWallHandler);

                //enemy move-update-test
                eTest.move(new Vector2f(pRunner.getXPosition(),pRunner.getYPosition()), mMap);

                /****************************************
                ************** KOMPASS *****************
                ****************************************/
                compass.update();
              
                
                // bewegliche Mauern (if-Abfrage), Kollision mit Schalter
                // später: Bewegung der Gegner, Geschosse, Anzeigen, Kollision

                spBackGround.Position = new Vector2f(vIngame.Center.X - 531, vIngame.Center.Y - 360);  // globale fensgtergrößen-vaiable?;
                spFogOfWar.Position = new Vector2f(vIngame.Center.X - 531, vIngame.Center.Y - 360);  // globale fensgtergrößen-vaiable?;
                vIngame.Move(new Vector2f((pRunner.getXPosition() + (pRunner.getWidth() / 2)), (pRunner.getYPosition() + (pRunner.getHeigth() / 2))) - vIngame.Center);


                // Sichtbarkeit wird ausgelöst
                if (B_IsVisible)
                {
                    //Timer für Sichtbarkeit wird gestartet
                    gtWallTimer = new GameTime();
                    gtWallTimer.Watch.Start();

                    // geheime Wege werden sichtbar durch Texturwechsel
                    foreach (Coordinates co in SecretWayList)
                    {
                        map[co.I_xCoord, co.I_yCoord].setTexture(new Texture ("Texturen/Map/wall-clean.png"));


                        // Wenn Textur über dem Feld zu vertikaler Mauer verändert werden muss, wird sie nun gewechselt.
                        if (co.B_UpperTexChanger)
                        {
                            map[co.I_xCoord-1, co.I_yCoord].setTexture(new Texture("Texturen/Map/wall-vert.png"));
                        }
                    }

                    // Auslöser wird resettet
                    B_IsVisible = false;
                }


                // Wenn Timer vorüber
                if (gtWallTimer.Watch.ElapsedMilliseconds >= 10000)
                {
                    //wird der Timer gestoppt und wieder auf 0 gesetzt,
                    gtWallTimer.Watch.Reset();

                    //und die Texturen auf ihre jeweiligen Ursprungszustände zurückgesetzt bzw. das Feld darüber, wenn nötig, wieder zu der
                    //Draufansicht einer Mauer.
                    foreach (Coordinates co in SecretWayList)
                    {
                        if(map[1,2].type() == 7)
                        {
                            map[co.I_xCoord, co.I_yCoord].setTexture(new Texture("Texturen/Map/wall-vert.png"));
                        }
                        else
                        {
                            map[co.I_xCoord, co.I_yCoord].setTexture(new Texture("Texturen/Map/wall-hor.png"));
                        }

                        if (co.B_UpperTexChanger)
                        {
                            map[co.I_xCoord - 1, co.I_yCoord].setTexture(new Texture("Texturen/Map/wall-hor.png"));
                        }

                    }

                }


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
            if (bmMap.GetPixel((int)((player.getXPosition() + (player.getWidth() / 2)) / map.I_blockSize) + 1, ((int)((player.getYPosition() + (player.getHeigth() / 2)) / map.I_blockSize) + 1)).Name == Sorange)
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
           // eTest.draw(win);  funktioniert nicht??
            win.Draw(spFogOfWar);
            Kompass.draw(win,spKompass);
            win.SetMouseCursorVisible(false);
            if (menu != null)
            {
                menu.draw(win);
            }
        }

      

    }
}
