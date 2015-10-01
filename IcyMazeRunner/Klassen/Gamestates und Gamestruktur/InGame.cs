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
using IcyMazeRunner.Klassen.Gameplay;

namespace IcyMazeRunner.Klassen
{
    class InGame : GameStates
    {

        /* ~~~~~~~~ allgemeine Spielvariablen ~~~~~~~~ */

        /// <summary>
        /// Gibt Distanz zum Ziel an.
        /// </summary>
        float targetdistance;


        /* ~~~~~~~~ static Variablen ~~~~~~~~ */

        /// <summary>
        ///  Zeigt Level des Spiels an.
        /// </summary>
        static int I_level = 0;

        /// <summary>
        ///  Speichert, ob geheime Wege sichtbar sind, oder nicht. Nur auslösender Boolean.
        /// </summary>
        static Boolean B_IsVisible = false;

        /// <summary>
        /// Stringangabe für Kollisionskontrolle mit Loch im Boden.
        /// </summary>
        public static String Sorange = "ffff8800";
        public static String Sblue = "ff0000ff"; //Ziel


        /* ~~~~~~~~ allgemeine Spielobjekte ~~~~~~~~ */

        /// <summary>
        /// Der Spieler.
        /// </summary>
        Player pRunner;

        /// <summary>
        /// Blocks-Array, um die Map durchzugehen.
        /// </summary>
        Blocks[,] map;

        /// <summary>
        ///  Allgemeine Spielzeituhr.
        /// </summary>
        GameTime gtIngame;

        /// <summary>
        /// Karte des aktuellen Levels.
        /// </summary>
        Map mMap;

        /// <summary>
        /// Sprite für den Hintergrund.
        /// </summary>
        Sprite spBackGround;

        /// <summary>
        /// Sprite für den schwarzen Grund außerhalb des Sichtsfeldes des Spielers.
        /// </summary>
        Sprite spFogOfWar;

        /// <summary>
        /// Menü während des laufenden Spiels.
        /// </summary>
        InGameMenu menu;

        /// <summary>
        /// Gibt an, ob Selbstrettung noch möglich ist, oder nicht mehr.
        /// </summary>
        public static Boolean B_LastChanceAvailable { get; set; }

        /// <summary>
        /// Gibt an, ob Todesanimation pausiert wird, um Zeit für die Letzte Chance zu bieten.
        /// </summary>
        public static Boolean B_DeathAnimationIsPaused{get; set;}

        Boolean B_selfprotectionFirstHalf = true;

        Boolean B_selfprotectionSecondHalf = false;

        Boolean B_selfprotectionSecondKeyCanBePressed = false;

        int I_Randomkey;

        int I_EnumKeyIndex;

        Sprite spLastChanceKey;

        Texture txLastChanceKey;

        /// <summary>
        /// <para>Gibt an, auf welche Art Spieler zu Tode kommt.</para>
        /// <para>0 = alive</para>
        /// <para>1 = Death by falling</para>
        /// <para>2 = instant Death by trap/Death by damage</para>
        /// </summary>
        private int I_typeOfDeath { get; set; }

        /// <summary>
        /// Gibt an, ob Todesanimation beendet ist.
        /// </summary>
        public bool B_isDeathAnimationOver;
        // ToDo: Bool ersetzt watch-Prüfung? Noch nicht implementiert in Update. Eventuell auch als Playerattribut, 
        // sodass es in DeathAnimation direkt private geändert werden kann.

        /// <summary>
        /// Konzentriert Sicht auf Spieler.
        /// </summary>
        View vIngame;

        /// <summary>
        /// Bitmap der aktuellen Levelmap.
        /// </summary>
        Bitmap bmMap;


        /* ~~~~~~~~ Spielobjekte für GUI ~~~~~~~~ */

        /// <summary>
        /// 
        /// </summary>
        GUI Health;

        /// <summary>
        /// 
        /// </summary>
        GUI Kompass;

        /// <summary>
        /// 
        /// </summary>
        GUI Skills;

        /// <summary>
        /// 
        /// </summary>
        GUI Collected;

        /// <summary>
        /// 
        /// </summary>
        GUI SoftPopUp;

        /// <summary>
        /// 
        /// </summary>
        Kompass compass;
        // ToDo: Summaries beschreiben.



        /* ~~~~~~~~ Spielobjekte für bewegliche Mauern ~~~~~~~~ */

        /// <summary>
        ///  Stoppuhr, um zu regeln, wie lange Geheime Wege sichtbar sind.
        /// </summary>
        GameTime gtWallTimer;


        /* ~~~~~~~~ Spielobjekte für geheime Mauern ~~~~~~~~ */

        /// <summary>
        ///  Liste, um zu speichern, an welcher Stelle sich geheime Wege befinden.
        /// </summary>
        List<Coordinates> SecretWayList;


        /* ~~~~~~~~ Spielobjekte für Gegner ~~~~~~~~ */



        /* ~~~~~~~~ Spielobjekte für Fallen ~~~~~~~~ */



        /*~~~~~~~~~~~~~~~~~~~ Handler-Objekte ~~~~~~~~~~~~~~~~~~~~~~~~~*/

        /// <summary>
        /// Allgemeine Gameobject-Handler.
        /// </summary>
        //  GameObjectHandler GOH;
        //ToDo: Handler implementieren


        /* ~~~~~~~~ Spielobjekte zum Testen ~~~~~~~~ */

        /// <summary>
        /// Testgegner
        /// </summary>
        Enemy eTest;


        /// <summary>
        /// Initialisierung der Variablen und Objekte.
        /// </summary>
        public void initialize()
        {
            /* ~~~~ allgemeine Spielobjekte ~~~~ */

            gtIngame = new GameTime();
            gtIngame.start();

            vIngame = new View(new FloatRect(0, 0, Game.windowSizeX - 1, Game.windowSizeY - 1));


            spBackGround = new Sprite(new Texture("Texturen/Map/background.png"));
            spBackGround.Position = new Vector2f(0, 0);

            spFogOfWar = new Sprite(new Texture("Texturen/Map/Fog_of_War.png"));
            spFogOfWar.Position = new Vector2f(-1, -1);

            I_typeOfDeath = 0;
            B_isDeathAnimationOver = false;



            /* ~~~~ GUI-Objekte ~~~~ */

            Kompass = new GUI(vIngame);


            /* ~~~~ Handler-Objekte ~~~~ */

            //GOH = new GameObjectHandler();
            //EH = new EntityHandler();
            //MWH = new MoveableWallHandler();
        }


        /// <summary>
        /// Texturen, Karten, Listen und GUI werden geladen.
        /// </summary>
        public void loadContent()
        {

            if (!Game.B_isLoadedGame)
            {
                I_level = Game.I_level;
            }
            else
            {
                // ToDo: Aus Speicherdatei auslesen.
            }
            /* ~~~~ Level laden ~~~~ */

            bmMap = new Bitmap("Texturen/Map/Map_test.bmp");
            //bmMap = new Bitmap("Texturen/Map/KI-test.bmp");
            // ToDo: Bitmaps in Switch-case laden. Level zuerst erstellen



            switch (I_level)
            {
                
                case 0:
                    mMap = new Map(bmMap);
                    //pRunner = new Player(new Vector2f(281,91), mMap); //(281,91)

                    break;



                default:
                    mMap = new Map(bmMap);
                    //pRunner = new Player(new Vector2f(mMap.vStart.X,mMap.vStart.Y), mMap); //(281,91)

                    break;

            }

            pRunner = new Player(mMap.vStart, mMap, vIngame);
            B_LastChanceAvailable = true;
            B_DeathAnimationIsPaused = false;

            /* ~~~~ Watch für LastChance ~~~~ */

            Game.gameTime.WatchList.Add(new System.Diagnostics.Stopwatch());


            /* ~~~~ Geheime Wege laden ~~~~ */

            loadSecretWays();


            /* Kompass TEST */

            compass = new Kompass(vIngame.Center, vIngame, mMap.vZiel);
            //compass = new Kompass(vIngame.Center, vTarget, bmKompass);

            /* ~~~~ Methoden aufrufen, die die jeweiligen Handler abhängig vom Level füllen ~~~~ */

            /* Enemy TEST */
            eTest = new Enemy(new Vector2f(251, 251), "Texturen/Enemies/downidle.png");
        }
        // ToDo: Methoden für jeden Handler schreiben, die levelabhängig die jeweiligen Objekte laden.

        /// <summary>
        /// Update-Methode
        /// </summary>
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

                if (B_DeathAnimationIsPaused)
                {
                   selfprotection();
                }
                else
                {
                    /* InGame-Menü wird aufgerufen */
                    if (!pRunner.getIsPressed() && Keyboard.IsKeyPressed(Keyboard.Key.Escape))
                    {
                        menu = new InGameMenu(pRunner);
                        pRunner.setIsPressed(true);
                    }


                    /* allgemeine Spiel aktualisieren */
                    gtIngame.update();
                    pRunner.update(gtIngame, GameObjectHandler.moveableWallHandler);

                    /* ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
                       Features aktualisieren (ohne Handler) 
                       ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ */


                    /* Geheime Mauer aktualisieren */
                    // Sichtbarkeit wird ausgelöst
                    if (pRunner.B_WayIsVisible)
                    {
                        makeSecretWaysVisible();
                    }
                    // Wenn Timer vorüber
                    if (gtWallTimer != null && gtWallTimer.Watch.ElapsedMilliseconds >= 7000)
                    {
                        makeSecretWaysInvisible();
                    }

                    /* Handler aktualisieren*/
                    //GOH.update(gametime, pRunner, mMap);

                    //enemy move-update-test
                    eTest.move(new Vector2f(pRunner.getXPosition(), pRunner.getYPosition()), mMap);


                    /* Aktualisierung, ob Spieler stirbt und Sterbeanimation */
                    if (get_Gap_Collision(pRunner.spPlayer, mMap))
                    {
                        pRunner.setPlayerHealth(0);
                        I_typeOfDeath = 1;
                    }

                    if (pRunner.getPlayerHealth() <= 0)
                    {
                        pRunner.DeathAnimation(I_typeOfDeath);

                        if (pRunner.B_IsSaved)
                        {
                            // Spieler hat wieder 5% Leben
                            pRunner.setPlayerHealth((int)(pRunner.getPlayerMaxHealth() * 0.05));
                            pRunner.setDeathWatchIsOn(false);
                        }

                        if (pRunner.gtDeathWatch.Watch.ElapsedMilliseconds > 4000)
                        {
                            // ob Level sinkt, hängt von Todesursache ab, nur bei Loch im Boden
                            if (I_typeOfDeath == 1)
                            {
                                I_level--;
                            }
                            pRunner.setDeathWatchIsOn(false);
                            return EGameStates.gameOver;
                        }
                    }
                    // ToDo: Todesabfragen und Animationen auslagern/kapseln
                    // ToDo: jede Methode, die Schaden zufügt, muss TypeOfDeath entsprechend anpassen
                    // ToDo: bei Abbruch der Todesanimation müssen auch Watches resettet werden


                    /* Kontrolle, ob Spieler Ziel erreicht */
                    targetdistance = Calculator.getDistance(pRunner.spPlayer.Position, mMap.vZiel);
                    if (targetdistance < 200)
                    {
                        I_level++;
                        if (I_level >= 31)
                        {
                            /*Kontrolle, ob Spieler gesamte Spiel gewonnen hat */
                            vIngame = new View(new FloatRect(0, 0, Game.windowSizeX, Game.windowSizeY));
                            return EGameStates.gameWon;
                        }
                        return EGameStates.NextLevel;
                    }



                    /* Hack-Win */
                    if (Keyboard.IsKeyPressed(Keyboard.Key.O) && Keyboard.IsKeyPressed(Keyboard.Key.LControl) && Keyboard.IsKeyPressed(Keyboard.Key.LShift))
                    {
                        vIngame = new View(new FloatRect(0, 0, Game.windowSizeX, Game.windowSizeY));
                        return EGameStates.NextLevel;
                    }



                    /* GUI aktualisieren */

                    vIngame.Move(new Vector2f((pRunner.getXPosition() + (pRunner.getWidth() / 2)), (pRunner.getYPosition() + (pRunner.getHeigth() / 2))) - vIngame.Center);
                    spBackGround.Position = new Vector2f(vIngame.Center.X - (int)(Game.windowSizeX / 2), vIngame.Center.Y - (int)(Game.windowSizeY / 2));
                    spFogOfWar.Position = new Vector2f(vIngame.Center.X - (int)(Game.windowSizeX / 2), vIngame.Center.Y - (int)(Game.windowSizeY / 2));

                    /****************************************
                     ************** KOMPASS *****************
                     ****************************************/
                    compass.update(mMap.vZiel);


                }
            }
            return EGameStates.inGame;
        }


        /// <summary>
        /// Mittelpunkt-Kollision zwischen 2 Objekten A,B (Sprites)
        /// </summary>
        /// <param name="sprite1">ObjektA</param>
        /// <param name="sprite2">ObjektB</param>
        /// <returns></returns>
        /// 
        public static bool point_Collision(Sprite spriteA, Sprite spriteB)
        {
            // nicht ganz sicher ob die vektoren korrekt berechnet wurden
            Vector2f ObjectA = new Vector2f(spriteA.Position.X + spriteA.Texture.Size.X / 2, spriteA.Position.Y + spriteA.Texture.Size.Y / 2);
            Vector2f ObjectB = new Vector2f(spriteB.Position.X + spriteB.Texture.Size.X / 2, spriteB.Position.Y + spriteB.Texture.Size.Y / 2);

            float F_widthMidA = spriteA.Texture.Size.X / 2;
            float F_widthMidB = spriteB.Texture.Size.X / 2;

            float F_heightMidA = spriteA.Texture.Size.Y / 2;
            float F_heightMidB = spriteB.Texture.Size.Y / 2;

            float F_betragX = Math.Abs(ObjectA.X - ObjectB.X);
            float F_betragY = Math.Abs(ObjectA.Y - ObjectB.Y);

            if (F_betragX < F_widthMidA + F_widthMidB && F_betragY < F_heightMidA + F_heightMidB)
                return true;

            else
                return false;
        }
        // ich erinnere mich, das ist Punktkollision, also wenn die Mittelpunkte sich treffen. hatte ich mal überlegt für Pfeile o.ä.


        /// <summary>
        /// Prüft, ob Spieler mit einem Loch im Boden kollidiert.
        /// </summary>
        public bool get_Gap_Collision(Sprite sprite, Map map)
        {
            // Kachel an Spielerposition mit Farbe der Bitmap und damit Kachelfarbe des Lochblocks vergleichen
            if (bmMap.GetPixel((int)((sprite.Position.X + (sprite.Texture.Size.X / 2)) / map.I_blockSize) + 1, ((int)((sprite.Position.Y + (sprite.Texture.Size.Y / 2)) / map.I_blockSize) + 1)).Name == Sorange)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        // ToDo: Methode in EnemyHandler.uodate() aufrufen


        /// <summary>
        /// <para> Um das gesamte Spiel zu zeichnen, ruft sie die jeweiligen Draw-Methoden von hinten nach vorn auf. </para>
        /// <para>Zuerst den Hintergrund. Dann die Map darüber. Darüber der Spieler und alle Spielelemente (Gegner, Mauern, Fallen,
        /// Schalter). Darüber dann den FogOfWar und schließlich die GUI bzw noch darüber temporär aufgerufene Objekte/Seiten/Fenster
        /// (InGame-Menü, Charakterübersicht).</para>
        /// </summary>
        public void draw(RenderWindow win)
        {
            win.SetView(vIngame);
            win.Draw(spBackGround);
            win.SetMouseCursorVisible(false);
            mMap.draw(win);
            pRunner.draw(win);
            win.Draw(spFogOfWar);


            /*GUI ab hier */
            compass.draw(win);


            if (menu != null)
            {
                menu.draw(win);
            }

            if (B_selfprotectionSecondHalf)
            {
                win.Draw(spLastChanceKey);
            }

            //if (inventory != null)
            //{
            //    inventory.draw(win);
            //}
        }

        // ja diesen Befehl einfach copy pasten in der draw von Hauptmenü und ingameMenü nur halt true. und bevor der est gemalt wird.



        /* Methode für Selbstrettung */
        /// <summary>
        /// Kontrolliert die Selbstrettung und ob sie scheitert oder funktioniert.
        /// </summary>
        void selfprotection()
        {
            if (B_selfprotectionFirstHalf)
            {
                // Zu wenig Zeit vergangen, als Spieler eine Taste gedrückt hat
                if (Game.gameTime.WatchList[0].ElapsedMilliseconds < 4000 && Controls.IsAnyKeyPressed())
                {
                    B_DeathAnimationIsPaused = false;
                }

                // Zu viel Zeit vergangen, bevor Spieler geblockt hat
                if (Game.gameTime.WatchList[0].ElapsedMilliseconds > 6000)
                {
                    B_DeathAnimationIsPaused = false;
                }

                // Richtige Reaktion
                if (Game.gameTime.WatchList[0].ElapsedMilliseconds >= 4000 && Game.gameTime.WatchList[0].ElapsedMilliseconds <= 6000 && Keyboard.IsKeyPressed(Keyboard.Key.E))
                {
                    B_selfprotectionFirstHalf = false;
                    B_selfprotectionSecondHalf = true;
                    I_Randomkey = Game.random.Next(4);

                    switch (I_Randomkey)
                    {
                        case 0:
                            {
                                // E
                                I_EnumKeyIndex = 4;
                                txLastChanceKey = new Texture("");
                                break;
                            }

                        case 1:
                            {
                                // F
                                I_EnumKeyIndex = 5;
                                txLastChanceKey = new Texture("");
                                break;
                            }

                        case 2:
                            {
                                // Q
                                I_EnumKeyIndex = 16;
                                txLastChanceKey = new Texture("");
                                break;
                            }

                        case 3:
                            {
                                // Leertaste
                                I_EnumKeyIndex = 57;
                                txLastChanceKey = new Texture("");
                                break;
                            }
                    }

                    spLastChanceKey.Texture = txLastChanceKey;
                    Game.gameTime.WatchList[0].Restart();

                         
                }

                // Richtige Zeit, falsche Taste
                if (Game.gameTime.WatchList[0].ElapsedMilliseconds >= 4000 && Game.gameTime.WatchList[0].ElapsedMilliseconds <= 6000 && Controls.IsAnyKeyPressed() && !Keyboard.IsKeyPressed(Keyboard.Key.E))
                {
                    B_DeathAnimationIsPaused = false;
                }
            }

            if (B_selfprotectionSecondHalf)
            {
                // Tastatur muss einmal losgelassen werden
                if (!Controls.IsAnyKeyPressed())
                {
                    B_selfprotectionSecondKeyCanBePressed = true;
                }

                if (B_selfprotectionSecondKeyCanBePressed && Game.gameTime.WatchList[0].ElapsedMilliseconds <= 2000 && (int)Controls.WhichKeyIsPressed() == I_EnumKeyIndex)
                {
                    pRunner.B_IsSaved = true;
                    B_DeathAnimationIsPaused = false;
                }

                // Wenn Zeit überschritten wurde oder falsche Taste gedrückt wurde, ist die Selbstrettung gescheitert
                if (Game.gameTime.WatchList[0].ElapsedMilliseconds > 2000 || (B_selfprotectionSecondKeyCanBePressed && Controls.IsAnyKeyPressed() && (int)Controls.WhichKeyIsPressed() != I_EnumKeyIndex))
                {
                    B_DeathAnimationIsPaused = false;
                }
            }        
        }



        /* Methoden für Geheime Wege */
        /// <summary>
        /// Lädt die Positionen, wo sich eine Geheime Mauer befindet in eine Liste.
        /// </summary>
        void loadSecretWays()
        {
            map = mMap.map;
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
                        if ((map[row + 1, col].type() != 7) && (map[row + 1, col].type() != 8) && (map[row, col].type() != 7))
                        {
                            helper = true;
                        }

                        // Füge Koordinaten und Boolean in Liste ein.
                        SecretWayList.Add(new Coordinates(row, col, helper));
                    }
                }
            }
        }

        /// <summary>
        /// Wenn B_isVisible durch Spieleraktion auf true gesetzt wird, wird eine Uhr gestartet und die Texturen der Mauern so geändert,
        /// dass man nun erkennen kann, dass es sich um Wege handelt.
        /// </summary>
        void makeSecretWaysVisible()
        {
                    //Timer für Sichtbarkeit wird gestartet
                    gtWallTimer = new GameTime();
                    gtWallTimer.Watch.Start();

                    // geheime Wege werden sichtbar durch Texturwechsel
                    foreach (Coordinates co in SecretWayList)
                    {
                        map[co.I_xCoord, co.I_yCoord].setTexture(new Texture("Texturen/Map/wall-clean.png"));


                        // Wenn Textur über dem Feld zu vertikaler Mauer verändert werden muss, wird sie nun gewechselt.
                        if (co.B_UpperTexChanger)
                        {
                            map[co.I_xCoord - 1, co.I_yCoord].setTexture(new Texture("Texturen/Map/wall-vert.png"));
                        }
                    }

                    // Auslöser wird resettet
                    pRunner.B_WayIsVisible = false;
        }

        /// <summary>
        /// Stellt den Ursprungszustand der Geheimen Mauern nach Ablauf der Zeit wieder her.
        /// </summary>
        void makeSecretWaysInvisible()
        {
            //wird der Timer gestoppt und wieder auf 0 gesetzt,
            gtWallTimer.Watch.Reset();

            //und die Texturen auf ihre jeweiligen Ursprungszustände zurückgesetzt bzw. das Feld darüber, wenn nötig, wieder zu der
            //Draufansicht einer Mauer.
            foreach (Coordinates coordz in SecretWayList)
            {
                if (map[1, 2].type() == 7)
                {
                    map[coordz.I_xCoord, coordz.I_yCoord].setTexture(new Texture("Texturen/Map/wall-vert.png"));
                }
                else
                {
                    map[coordz.I_xCoord, coordz.I_yCoord].setTexture(new Texture("Texturen/Map/wall-hor.png"));
                }

                if (coordz.B_UpperTexChanger)
                {
                    map[coordz.I_xCoord - 1, coordz.I_yCoord].setTexture(new Texture("Texturen/Map/wall-hor.png"));
                }

            }
            gtWallTimer = null;
        }


    }
}
