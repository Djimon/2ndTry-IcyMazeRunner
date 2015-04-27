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

        Player pRunner;

        Sprite spBackGround;
        Sprite spFogOfWar;

        InGameMenu menu;
        bool B_isMenuOpen = false;

        private int I_typeOfDeath;
        /*
         * 0 = alive
         * 1 = Death by falling
         * 2 = instant Death by trap
         * 3 = Death by damage
        */
        public bool B_isDeathAnimationOver;


        /*~~~~~~~~~~~~~~~~~~~Gap Collision~~~~~~~~~~~~~~~~~~~~~~~~~*/

        public static String orange = "ffff8000"; // Loch im Boden



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
           

            gtIngame = new GameTime();
            gtIngame.start();
            // globale fensgtergrößen-vaiable?;
            vIngame = new View(new FloatRect(0, 0, 1058, 718));  // 1% kleiner, als Original, um Side-glitches zu verhindern 

            spBackGround = new Sprite(new Texture("Texturen/Map/background.png"));
            spBackGround.Position = new Vector2f(0, 0);

            spFogOfWar = new Sprite(new Texture("Texturen/Map/Fog_of_War.png"));
            spFogOfWar.Position = new Vector2f(-1,-1);
           

            setTypeOfDeath(0);
            B_isDeathAnimationOver=false;
            
          
        }

        /* ~~~~ Inhalte laden ~~~~ */
        public void loadContent(){
          
            /*
            Map-Positionen:
            Map_tutorial (190,0)
            Map_2 (2263, 3336)
             */

            bmMap = new Bitmap("Texturen/Map/Map_tutorial.bmp");
            // mapOfBits = new Bitmap("Texturen/Map_1.bmp");

            // Levelzuweisung

            switch (I_level)
            {
                case 0:
                    /* Redundant
                     mapOfBits = new Bitmap("Texturen/Map/Map_tutorial.bmp");
                     mapOfBits = new Bitmap("Texturen/Map_1.bmp");
                     */
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

          
            //        hier Fallen und Hindernisse laden???
            //         ziel?
           

            
        }


        /* ~~~~ Update ~~~~ */
        public EGameStates update(GameTime gametime)
        {


            if (B_isMenuOpen)
                return menu.update();
            else
            {

                if (!pRunner.getIsPressed() && Keyboard.IsKeyPressed(Keyboard.Key.Escape))
                {
                    menu = new InGameMenu(pRunner);
                    B_isMenuOpen = true;
                    pRunner.setIsPressed(true);

                }

                //if (menu != null)
                //{

                //    return menu.update();
                //}

                if (menu != null && menu.getCloseMenu())
                {
                    B_isMenuOpen = false;
                    menu = null;
                }

                if (get_Gap_Collision(pRunner, mMap))
                {
                    pRunner.setPlayerHealth(0);
                    setTypeOfDeath(1);
                }

                gtIngame.update();

                if (pRunner.getPlayerHealth() == 0)
                {

                    pRunner.DeathAnimation(getTypeOfDeath());


                    if (pRunner.gtDeathWatch.Watch.ElapsedMilliseconds > 4000)
                    {
                        I_level--;
                        return EGameStates.gameOver;
                    }
                }

                if (I_level != I_level) // Kollision mit Treppe= true
                {
                    I_level++;
                    Program.game.handleGameState(); //wieso?
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
                    return EGameStates.gameWon;
                }

                pRunner.update(gtIngame);
                // bewegliche Mauern (if-Abfrage), Kollision mit Schalter
                // später: Bewegung der Gegner, Geschosse, Anzeigen, Kollision

                spBackGround.Position = new Vector2f(vIngame.Center.X - 531, vIngame.Center.Y - 360);  // globale fensgtergrößen-vaiable?;
                spFogOfWar.Position = new Vector2f(vIngame.Center.X - 531, vIngame.Center.Y - 360);  // globale fensgtergrößen-vaiable?;
                vIngame.Move(new Vector2f((pRunner.getXPosition() + (pRunner.getWidth() / 2)), (pRunner.getYPosition() + (pRunner.getHeigth() / 2))) - vIngame.Center);




                /*~~~~~~~Collision mit Ziel, SPrite ziel muss noch übergebenw erden aus (Map/Blocks?)~~~~*/

                //if (collision(Runner.getplayerSprite, (float)Runner.getWidth(), (float)Runner.getHeigth(), ziel.getPosition(), (float)ziel.getWidth(), (float)ziel.getHeight()))
                //{
                //    Console.Write("Collision!!1elf");
                //    view = new View(new FloatRect(0, 0, 1062, 720));
                //    return EGameStates.gameWon;
                //}

            }
            return EGameStates.inGame;
        }

        public static bool collision(Vector2f sprite1, float F_highA, float F_widthA, Vector2f sprite2, float F_highB, float F_widthB)
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
            if (menu != null)
            {
                menu.draw(win);
            }
        }
        
    }
}
