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
        /// <para>Gibt an, auf welche Art Spieler zu Tode kommt.</para>
        /// <para>0 = alive</para>
        /// <para>1 = Death by falling</para>
        /// <para>2 = instant Death by trap</para>
        /// <para>3 = Death by damage</para>
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

        /// <summary>
        /// Rechner für allgemeine Berechnungen.
        /// </summary>
        Calculator calc;

        Sprite spZiel { get; set; }  // hier map/ blocktype=4 (blue= ziel) ??
        // ToDo: benötigt? 0 Verweise...


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

        /// <summary>
        /// Gibt Vektor an, wo sich Ziel befindet. Orientierungsvektor für den Kompass.
        /// </summary>
        Vector2f vTarget;

        // System.Drawing.Image bmKompass;
        // ToDo: ???

        /// <summary>
        /// Sprite für den Kompass.
        /// </summary>
        Sprite spKompass;


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

        ///// <summary>
        ///// Allgemeine Gameobject-Handler.
        ///// </summary>
        //GameObjectHandler GOH;

        ///// <summary>
        ///// Allgemeine Entity-Handler.
        ///// </summary>
        //EntityHandler EH;

        ///// <summary>
        ///// Allgemeine MovableWall-Handler.
        ///// </summary>
        //MoveableWallHandler MWH;
        // ToDo: EntityHandler und MovableWallHandler benötigt oder Bestandteil als Klassenattribut/Objeht von GameObjectHandler?


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

            vIngame = new View(new FloatRect(0, 0, 1058, 718));
            // ToDo: globale Fenstergrößen-Variable? 1% kleiner, als Original, um Side-glitches zu verhindern --> fixen

            spBackGround = new Sprite(new Texture("Texturen/Map/background.png"));
            spBackGround.Position = new Vector2f(0, 0);

            spFogOfWar = new Sprite(new Texture("Texturen/Map/Fog_of_War.png"));
            spFogOfWar.Position = new Vector2f(-1, -1);

            I_typeOfDeath=0;
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
        public void loadContent(){

            /* ~~~~ Level laden ~~~~ */
          
            bmMap = new Bitmap("Texturen/Map/Map_test.bmp");
            //bmMap = new Bitmap("Texturen/Map/KI-test.bmp");
            // ToDo: Bitmaps in Switch-case laden.
  


            switch (I_level)
            {
                    //ToDo: Spielerstartposition automatisch bestimmen lassen. Eventuell das gleiche für die Map.
                case 0:
                    mMap = new Map(bmMap); 
                    pRunner = new Player(new Vector2f(1,1), mMap); //(281,91)

                    break;



                default:
                    mMap = new Map(bmMap); 
                    pRunner = new Player(new Vector2f(1,1), mMap); //(281,91)

                    break;

            }

            // ToDo: Player erst hier nach dem Switch-case laden: pRunner = new Player(Startvektor, mMap);
            // ToDo: Startvektor vorher initialisieren/bestimmen (in der Map-Klasse Vektor direkt als eigenes Attribut bestimmen und dann hier
            // abrufen.

            /* ~~~~ Geheime Wege laden ~~~~ */
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
                        if ((map[row+1, col].type() !=7 ) && (map[row+1, col].type() != 8) && (map[row, col].type() != 7))
                        {
                            helper = true;
                        }

                        // Füge Koordinaten und Boolean in Liste ein.
                        SecretWayList.Add(new Coordinates(row, col, helper));
                    }

                }
            }

            /* Kompass TEST */

            // bmKompass = new System.Drawing.Bitmap("Texturen/Menü+Anzeige/GUI/Untitled-1.png");
            // ToDo: Kompass laden

            // Festlegung des Zielvekotrs für den Kompass (buggy)
            for (int row = 0; row < map.GetLength(0); row++)
            {
                for (int col = 0; col < map.GetLength(1); col++)
                {
                    if ( bmMap.GetPixel(row, col).Name == Sblue)
                    {
                        vTarget = new Vector2f(row * 90, col * 90); //globale variablen?
                        break;
                    }
                    else vTarget = new Vector2f(0, 0);
                }
            }
            // ToDo: einfach vPos nutzen (vTarget = "Map-Name".vPos)
            // ToDo: vTarget bestimmen

            spKompass = new Sprite(new Texture("Texturen/Menü+Anzeigen/GUI/needle.png"));
            compass = new Kompass(vIngame.Center, vTarget,spKompass.Texture);   
            //compass = new Kompass(vIngame.Center, vTarget, bmKompass);

            /* ~~~~ Methoden aufrufen, die die jeweiligen Handler abhängig vom Level füllen ~~~~ */

            /* Enemy TEST */
            eTest = new Enemy(new Vector2f(251, 251), "Texturen/Enemies/downidle.png");
        }
        // ToDo: Wurde alles in initialize/loadContent geladen und initialisiert???
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
                // ToDo: Ingame-Menu-Update-Methode schreiben (Auslagern/kapseln)
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
                if (B_IsVisible)
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
                    B_IsVisible = false;
                }


                // Wenn Timer vorüber
                if (gtWallTimer != null && gtWallTimer.Watch.ElapsedMilliseconds >= 10000)
                {
                    //wird der Timer gestoppt und wieder auf 0 gesetzt,
                    gtWallTimer.Watch.Reset();

                    //und die Texturen auf ihre jeweiligen Ursprungszustände zurückgesetzt bzw. das Feld darüber, wenn nötig, wieder zu der
                    //Draufansicht einer Mauer.
                    foreach (Coordinates co in SecretWayList)
                    {
                        if (map[1, 2].type() == 7)
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
                // ToDo: in eigene Methode auslagern/kapseln

                /* Handler aktualisieren*/
                //GOH.update(gametime, pRunner, mMap);

                //enemy move-update-test
                eTest.move(new Vector2f(pRunner.getXPosition(), pRunner.getYPosition()), mMap);


                /* Aktualisierung, ob Spieler stirbt und Sterbeanimation */
                if (get_Gap_Collision(pRunner, mMap))
                {
                    pRunner.setPlayerHealth(0);
                    I_typeOfDeath = 1;
                }

                if (pRunner.getPlayerHealth() <= 0)
                {
                    pRunner.DeathAnimation(I_typeOfDeath);

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
                        if (I_typeOfDeath==1)
                        {
                            I_level--;
                        }
                        pRunner.setDeathWatchIsOn(false);
                        return EGameStates.gameOver;
                    }
                }
                // ToDo: Todesabfragen und Animationen auslagern/kapseln
                // ToDo: jede Methode, die Schaden zufügt, muss TypeOfDeath entsprechend anpassen


                /* Kontrolle, ob Spieler Ziel erreicht */
                targetdistance = Calculator.getDistance(pRunner.getplayerSprite().Position, vTarget);
                if (targetdistance <200)
                {
                    I_level++;
                    return EGameStates.NextLevel;
                }

                /*Kontrolle, ob Spieler gesamte Spiel gewonnen hat */
                if (I_level == 31)
                {
                    vIngame = new View(new FloatRect(0, 0, 1062, 720)); // globale fensgtergrößen-vaiable?;
                    return EGameStates.gameWon;
                }
                // ToDo: if-Abfrage zwischen I_level++; und return EGameStates.NextLevel; einfügen?


                /* Hack-Win */
                if (Keyboard.IsKeyPressed(Keyboard.Key.O))
                {
                    vIngame = new View(new FloatRect(0, 0, 1062, 720)); // globale fensgtergrößen-vaiable?;
                    return EGameStates.NextLevel;
                }
                // ToDo: noch benötigt?


                /* GUI aktualisieren */

                vIngame.Move(new Vector2f((pRunner.getXPosition() + (pRunner.getWidth() / 2)), (pRunner.getYPosition() + (pRunner.getHeigth() / 2))) - vIngame.Center);
                spBackGround.Position = new Vector2f(vIngame.Center.X - 531, vIngame.Center.Y - 360);  // globale fensgtergrößen-vaiable?;
                spFogOfWar.Position = new Vector2f(vIngame.Center.X - 531, vIngame.Center.Y - 360);  // globale fensgtergrößen-vaiable?;

                /****************************************
                 ************** KOMPASS *****************
                 ****************************************/
                compass.update();
                   
                /*~~~~~~~Collision mit Ziel, SPrite ziel muss noch übergebenw erden aus (Map/Blocks?)~~~~*/

                //if (point_Collision(Runner.getplayerSprite, (float)Runner.getWidth(), (float)Runner.getHeigth(), ziel.getPosition(), (float)ziel.getWidth(), (float)ziel.getHeight()))
                //{
                //    Console.Write("Collision!!1elf");
                //    view = new View(new FloatRect(0, 0, 1062, 720));
                //    return EGameStates.gameWon;
                //}
                // ToDo: Wofür ist das????

            }
            return EGameStates.inGame;
        }
        // ToDo: Kontrollieren, ob alles, was aktualisiert werden muss, aktualisiert wird.


        /// <summary>
        /// 
        /// </summary>
        /// <param name="sprite1"></param>
        /// <param name="F_highA"></param>
        /// <param name="F_widthA"></param>
        /// <param name="sprite2"></param>
        /// <param name="F_highB"></param>
        /// <param name="F_widthB"></param>
        /// <returns></returns>
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
        // ToDo: benötigt? Soll damit Kollision ersetzt werden???
        // ToDo: Falls Methode behalten wird, Summary schreiben.

        /// <summary>
        /// Prüft, ob Spieler mit einem Loch im Boden kollidiert.
        /// </summary>
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
        // ToDo: Gleiche Methode im Enemy-Handler??
      

        /// <summary>
        /// <para> Um das gesamte Spiel zu zeichnen, ruft sie die jeweiligen Draw-Methoden von hinten nach vorn auf. </para>
        /// <para>Zuerst den Hintergrund. Dann die Map darüber. Darüber der Spieler und alle Spielelemente (Gegner, Mauern, Fallen,
        /// Schalter). Darüber dann den FogOfWar und schließlich die GUI bzw noch darüber temporär aufgerufene Objekte/Seiten/Fenster
        /// (InGame-Menü, Charakterübersicht).</para>
        /// </summary>
        public void draw(RenderWindow win)
        {
            win.Draw(spBackGround);
            win.SetView(vIngame);
            mMap.draw(win);
            pRunner.draw(win);
            //eTest.draw(win);  
            // ToDo: eTestfunktioniert nicht??
            win.Draw(spFogOfWar);
            Kompass.draw(win,spKompass);
            win.SetMouseCursorVisible(false);
            if (menu != null)
            {
                menu.draw(win);
            }
        }
        // ToDo: Können die beiden Sets an der jeweiligen Stelle stehen? Oder kann man sie an den Anfang oder das Ende ziehen?
        // ToDo: Einstellen, wann der Cursor davon abweichend sichtbar ist (im InGameMenü Maussteuerung möglich? Bei Aufruf des Hauptmenüs sollte,
        // falls dort Maussteuerung möglich ist, vor dem Wechsel auch die Maus sichtbar gesetzt werden.
      

    }
}

// ToDo: Handler überarbeiten,GameObjecthandler nur zum Aufruf der anderen Handler...


// ToDo: Kommentarschnipsel wahrscheinlich unbrauchbar

/*
Map-Positionen:
Map_tutorial (190,0)
Map_2 (2263, 3336)
 */

/*
 *                 case 1:
                    bmMap = new Bitmap("Texturen/Map_1.bmp"); // 190, 100 bei Map_1 gespeichert gewesen
                    mMap = new Map(bmMap);
                    pRunner = new Player(new Vector2f(0, 0), mMap);
                    break;
                    
                case 2:
                   // mapOfBits = new Bitmap("Texturen/Map_2.bmp"); alt
                    mMap = new Map(bmMap);
                    pRunner = new Player(new Vector2f(2263,3336), mMap);
                    break;
 * */
