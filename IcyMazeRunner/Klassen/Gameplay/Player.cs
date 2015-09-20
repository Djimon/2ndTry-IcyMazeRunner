﻿using IcyMazeRunner.Klassen;
using SFML.Graphics;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IcyMazeRunner
{
    class Player
    {

        // ToDo: ENums für move-verwenden, Hardmode-move-Methode entsprechend abstrahieren, allgemein schreiben und Code kürzen

        /// <summary>
        ///  Variable, um Sterbeanimation zu unterbrechen und Spiel fortzusetzen.
        /// </summary>
        public Boolean B_IsSaved {get; set;}


        /// <summary>
        ///  Statehandler, der Zustände des Spielers verwaltet.
        /// </summary>
        PlayerConditionHandler SH;

        /// <summary>
        /// Eine Kopie der Map, um sie in Update() verwenden zu können, da sie nicht mitgegeben werden kann.
        /// </summary>
        Map mAbsmap;

        /// <summary>
        ///  Timeobjekt, um Zeit für den Herausforderungsmodus zu messen, sollte er aktiviert werden.
        /// </summary>
        GameTime gtMoveTime;

        /* ~~~~ Wichtige Playerattribute ~~~~ */
        /// <summary>
        /// Sprite des Spielers.
        /// </summary>
        Sprite spPlayer { get; set; }

        int I_healthPoints;
        int I_maxhealthPoints;
        float F_runningSpeed;
        // int basicDamage
        // int basicDefence


        /* ~~~~ Texturen für Stehen und Bewegungsanimation ~~~~ */
        Texture txDown1;
        Texture txDown2;
        Texture txDown3;
        Texture txUp1;
        Texture txUp2;
        Texture txUp3;
        Texture txRight1;
        Texture txRight2;
        Texture txRight3;
        Texture txLeft1;
        Texture txLeft2;
        Texture txLeft3;

        /* ~~~~ Attribute für idle-Animation ~~~~ */
        /// <summary>
        /// Boolean, ob Spieler stehen bleibt.
        /// </summary>
        bool B_isPressed = false;

        /// <summary>
        /// Gibt die Textur an, welche genutzt wird, wenn Spieler stehen bleibt.
        /// </summary>
        int I_rememberidle = 0;


        /* ~~~~ Attribute für den Herausfoderungsmodus ~~~~ */
        /// <summary>
        /// Gibt an, ob Herausforderungsmodus aktiviert ist.
        /// </summary>
        bool B_isControlChanged = false;

        /// <summary>
        /// Gibt an, welche der 24 möglichen Fälle der Bewegungssteuerung genutzt wird.
        /// </summary>
        int I_moveChangerState = 0;

        /// <summary>
        /// Randomgenerator für I_moveChangerState.
        /// </summary>
        Random random = new Random();


        /* ~~~~ Spieler stirbt ~~~~ */
        /// <summary>
        /// Gametime, um Animation des Spielers zu steuern, wenn er stirbt.
        /// </summary>
        public GameTime gtDeathWatch;
        /// <summary>
        /// Gibt an, ob Todesanimation ausgelöst wurde.
        /// </summary>
        bool B_isDeathWatchOn;


        /* ~~~~ Attribute für Fallanimation ~~~~ */
        Texture txFall1a;
        Texture txFall1b;
        Texture txFall2a;
        Texture txFall2b;
        Texture txFall3a;
        Texture txFall3b;
        Texture txFall4a;
        Texture txFall4b;
        /// <summary>
        /// Linear steigender Int-Wert für wechselnde Texturen, d.h. Animationen mithilfe von Modulo 2.
        /// </summary>
        int I_fallAnimationCounter = 0;


        /* ~~~~ Konstruktor ~~~~ */
        public Player(Vector2f Pos, Map map)
        {
            //Festlegen der Spielerposition, wird mit neuem Level neu festgelegt
            spPlayer.Position = Pos;

            //Initialisieren
            SH = new PlayerConditionHandler();

            // Initialisierung der HP des Spielers, wird mit neuem Level neu festgelegt
            I_healthPoints = 100;
            I_maxhealthPoints = I_healthPoints;


            //Bewegungstexturen werden geladen
            txDown1 = new Texture("Texturen/Player/down1.png");
            txDown2 = new Texture("Texturen/Player/down2.png");
            txDown3 = new Texture("Texturen/Player/downidle.png");
            txUp1 = new Texture("Texturen/Player/up1.png");
            txUp2 = new Texture("Texturen/Player/up2.png");
            txUp3 = new Texture("Texturen/Player/upidle.png");
            txRight1 = new Texture("Texturen/Player/right1.png");
            txRight2 = new Texture("Texturen/Player/right2.png");
            txRight3 = new Texture("Texturen/Player/rightidle.png");
            txLeft1 = new Texture("Texturen/Player/left1.png");
            txLeft2 = new Texture("Texturen/Player/left2.png");
            txLeft3 = new Texture("Texturen/Player/leftidle.png");


            //Fallanimationstexturen werden geladen
            txFall1a = new Texture("Texturen/Player/Fallanimation/fall1a Platzhalter.png");
            txFall1b = new Texture("Texturen/Player/Fallanimation/fall1b Platzhalter.png");
            txFall2a = new Texture("Texturen/Player/Fallanimation/fall2a Platzhalter.png");
            txFall2b = new Texture("Texturen/Player/Fallanimation/fall2b Platzhalter.png");
            txFall3a = new Texture("Texturen/Player/Fallanimation/fall3a Platzhalter.png");
            txFall3b = new Texture("Texturen/Player/Fallanimation/fall3b Platzhalter.png");
            txFall4a = new Texture("Texturen/Player/Fallanimation/fall4a Platzhalter.png");
            txFall4b = new Texture("Texturen/Player/Fallanimation/fall4b Platzhalter.png");

            //Standardtextur wird festgelegt und Sprite initialisiert
            spPlayer = new Sprite(txDown3);
            spPlayer.Scale = new Vector2f(1f, 1f);
            spPlayer.Position = spPlayer.Position;

            mAbsmap = map;

            // Stoppuhr für Herausforderungsmodus starten
            gtMoveTime = new GameTime();
            gtMoveTime.Watch.Start();

            // Attribute und Objekte für Todesanimation initialisieren
            gtDeathWatch = new GameTime();
            B_isDeathWatchOn = false;
            B_IsSaved = false;
        }


        /* ~~~~~~~~~~~~~~~~~~ Getter und Setter ~~~~~~~~~~~~~~*/


        /// <summary>
        /// Gibt Sprite des Spielers zurück. (z.B. für Zugriff auf Position des Sprites)
        /// </summary>
        public Sprite getplayerSprite()
        {
            return this.spPlayer;
        }


        public void setSpritePos(Vector2f pos)
        {
            spPlayer.Position = pos;
        }

        /// <summary>
        /// Gibt zurück, ob eine Taste gedrückt wird.
        /// </summary>
        public bool getIsPressed()
        {
            return B_isPressed;
        }

        /// <summary>
        /// Legt fest, ob eine Taste gedrückt wird.
        /// </summary>
        public void setIsPressed(bool value)
        {
            B_isPressed = value;
        }

        /// <summary>
        /// Gibt x-Position des Sprites zurück.
        /// </summary>
        public float getXPosition()
        {
            return spPlayer.Position.X;
        }

        /// <summary>
        /// Gibt y-Position des Sprites zurück.
        /// </summary>
        public float getYPosition()
        {
            return spPlayer.Position.Y;
        }

        /// <summary>
        /// Gibt Breite der Textur des Sprites zurück.
        /// </summary>
        public float getWidth()
        {
            return spPlayer.Texture.Size.X;
        }

        /// <summary>
        /// Gibt Höhe der Textur des Sprites zurück.
        /// </summary>
        public float getHeigth()
        {
            return spPlayer.Texture.Size.Y;
        }

        /// <summary>
        /// Gibt HealthPoints des Spielers zurück.
        /// </summary>
        public int getPlayerHealth()
        {
            return I_healthPoints;
        }

        /// <summary>
        /// Setzt HealthPoints auf einen bestimmten Wert.
        /// </summary>
        public void setPlayerHealth(int data)
        {
            I_healthPoints = data;
        }

        /// <summary>
        /// Legt fest, ob Todesanimation ausgelöst wurde, oder nicht.
        /// </summary>
        public void setDeathWatchIsOn(bool value)
        {
            B_isDeathWatchOn = value;
        }

        /// <summary>
        ///  Fügt einen Zustand ein, sodass andere Objekte einen Zustand erstellen können und dieser dann im StateHandler des Spielers verwaltet wird.
        /// </summary>
        public void setState (Playercondition state)
        {
            SH.add(state);
        }

        /// <summary>
        /// Fügt dem Spieler Schaden zu.
        /// </summary>
        /// <param name="_Damage">Gibt an, wieviel Schaden bei jedem Tick verursacht werden soll. </param>
        public void setDamage(int _Damage)
        {
            I_healthPoints = I_healthPoints - _Damage;

            if (I_healthPoints <0)
            {
                I_healthPoints = 0;
            }
        }

        /// <summary>
        /// Heilt den Spieler.
        /// </summary>
        /// <param name="_Heal">Gibt an, wieviel Heilung der Spieler erhält. </param>
        public void setHeal(int _Heal)
        {
            I_healthPoints = I_healthPoints + _Heal;

            if (I_healthPoints > I_maxhealthPoints)
            {
                I_healthPoints = I_maxhealthPoints;
            }
        }

        /// <summary>
        /// Heilt den Spieler komplett.
        /// </summary>
        public void setHeal()
        {
                I_healthPoints = I_maxhealthPoints;
        }
        
        /// <summary>
        /// StateHandler aktualisiert sich und verwaltet die Liste. Anschließend wird die move()-Methode aufgerufen.
        /// </summary>
        public void update(GameTime time, MoveableWallHandler MWH)
        {
            SH.update();
            move(this.mAbsmap, time, MWH);
        }


        /// <summary>
        /// <para>Steuert Bewegung des Spielers.</para>
        /// 
        /// <para>Zunächst wird die Laufgeschwindigkeit des Spielers an den Computer angepasst und der Boolean _IsPressed auf Standard zurückgesetzt. </para>
        /// 
        /// <para>Dann wird kontrolliert, ob der Herausforderungsmodus aktiviert oder deaktiviert werden soll und falls er aktiviert ist, wird zur speziellen, längeren Methode für den Herausforderungsmodus gewechselt. </para>
        ///
        /// Ansonsten wird dir normale WASD-Steuerung genutzt. Falls sich der Spieler nicht bewegt, wird auf die idle-Textur umgestellt und am Ende die Position des Sprites an die Position der Spielerinstanz gesetzt. </para>
        /// </summary>
        public void move(Map map, GameTime time, MoveableWallHandler MWH)
        {
            F_runningSpeed = 0.5f * time.ElapsedTime.Milliseconds;
            B_isPressed = false;


            if (Keyboard.IsKeyPressed(Keyboard.Key.F2))
                B_isControlChanged = true;
            if (Keyboard.IsKeyPressed(Keyboard.Key.F1))
                B_isControlChanged = false;


            if (B_isControlChanged)
                Changingmove(map, time);
            else
            {

                if (Keyboard.IsKeyPressed(Keyboard.Key.A) && map.iswalkable((spPlayer), new Vector2f(-F_runningSpeed, 0)) && MWH.isWalkable(this, new Vector2f(-F_runningSpeed, 0)))
                {
                    spPlayer.Position = new Vector2f(spPlayer.Position.X - F_runningSpeed, spPlayer.Position.Y);
                    if (time.TotalTime.Milliseconds % 500 < 250) this.spPlayer.Texture = txLeft1;
                    else this.spPlayer.Texture = txLeft2;
                    B_isPressed = true;
                    I_rememberidle = 0;
                }

                if (Keyboard.IsKeyPressed(Keyboard.Key.D) && map.iswalkable((spPlayer), new Vector2f(F_runningSpeed, 0)) && MWH.isWalkable(this, new Vector2f(  F_runningSpeed, 0)))
                {
                    spPlayer.Position = new Vector2f(spPlayer.Position.X + F_runningSpeed, spPlayer.Position.Y);
                    if (time.TotalTime.Milliseconds % 500 < 250) this.spPlayer.Texture = txRight1;
                    else this.spPlayer.Texture = txRight2;
                    B_isPressed = true;
                    I_rememberidle = 1;
                }

                if (Keyboard.IsKeyPressed(Keyboard.Key.W) && map.iswalkable((spPlayer), new Vector2f(0, -F_runningSpeed)) && MWH.isWalkable(this, new Vector2f(0, -F_runningSpeed)))
                {
                    spPlayer.Position = new Vector2f(spPlayer.Position.X, spPlayer.Position.Y - F_runningSpeed);
                    if (time.TotalTime.Milliseconds % 500 < 250) this.spPlayer.Texture = txUp1;
                    else this.spPlayer.Texture = txUp2;
                    B_isPressed = true;
                    I_rememberidle = 2;
                }

                if (Keyboard.IsKeyPressed(Keyboard.Key.S) && map.iswalkable((spPlayer), new Vector2f(0, F_runningSpeed)) && MWH.isWalkable(this, new Vector2f(0, F_runningSpeed)))
                {
                    spPlayer.Position = new Vector2f(spPlayer.Position.X, spPlayer.Position.Y + F_runningSpeed);
                    if (time.TotalTime.Milliseconds % 500 < 250) this.spPlayer.Texture = txDown1;
                    else this.spPlayer.Texture = txDown2;
                    B_isPressed = true;
                    I_rememberidle = 3;
                }

                if (B_isPressed == false)
                {
                    switch (I_rememberidle)
                    {
                        case 0: this.spPlayer.Texture = txLeft3;
                            break;
                        case 1: this.spPlayer.Texture = txRight3;
                            break;
                        case 2: this.spPlayer.Texture = txUp3;
                            break;
                        case 3: this.spPlayer.Texture = txDown3;
                            break;
                        default: this.spPlayer.Texture = txDown3;
                            break;
                    }
                }
            }

            spPlayer.Position = spPlayer.Position;

        }


        // ToDo: restliche Animationen, Animationen selbst in eigene Methoden auslagern.
        /// <summary>
        /// <para>Animation des Todes</para>
        /// <para> Bei erstem Aufruf der Methode ist B_isDeathWatchOn false, und somit wird die DeathWatch.Watch zurückgesetzt und gestartet und
        /// der Boolean auf wahr gesetzt. Für Animationen wird der Counter hochgezählt und anschließend abhängig von der Todesursache zur 
        /// jeweiligen Animation gesprungen und diese wird ausgeführt.</para>
        /// </summary>
        public void DeathAnimation(int typeOfDeath)
        {
            if (!B_isDeathWatchOn)
            {
                setDeathWatchIsOn(true);
                gtDeathWatch.Watch.Reset();
                gtDeathWatch.Watch.Start();
            }


            I_fallAnimationCounter++;


            switch (typeOfDeath)
            {
                case 0:
                    {
                        /* Nichts, da am Leben */
                        break;
                    }
                    
                case 1:
                    {
                        /* Fallanimation*/
                        if (gtDeathWatch.Watch.ElapsedMilliseconds > 0 && gtDeathWatch.Watch.ElapsedMilliseconds < 1000)
                        {
                            if (I_fallAnimationCounter % 2 == 0)
                            {
                                spPlayer.Texture = txFall1a;
                            }
                            else
                            {
                                spPlayer.Texture = txFall1b;
                            }
                        }

                        if (gtDeathWatch.Watch.ElapsedMilliseconds > 1000 && gtDeathWatch.Watch.ElapsedMilliseconds < 2000)
                        {
                            if (I_fallAnimationCounter % 2 == 0)
                            {
                                spPlayer.Texture = txFall2a;
                            }
                            else
                            {
                                spPlayer.Texture = txFall2b;
                            }
                        }

                        if (gtDeathWatch.Watch.ElapsedMilliseconds > 2000 && gtDeathWatch.Watch.ElapsedMilliseconds < 3000)
                        {
                            if (I_fallAnimationCounter % 2 == 0)
                            {
                                spPlayer.Texture = txFall3a;
                            }
                            else
                            {
                                spPlayer.Texture = txFall3b;
                            }
                        }

                        if (gtDeathWatch.Watch.ElapsedMilliseconds > 3000 && gtDeathWatch.Watch.ElapsedMilliseconds < 4000)
                        {
                            if (I_fallAnimationCounter % 2 == 0)
                            {
                                spPlayer.Texture = txFall4a;
                            }
                            else
                            {
                                spPlayer.Texture = txFall4b;
                            }
                        }

                        break;
                    }

                case 2:
                    {
                        break;
                    }

                case 3:
                    {
                        break;
                    }


            }
        }


        /// <summary>
        /// Zeichnet den Spieler im Fenster.
        /// </summary>
        public void draw(RenderWindow win)
        {
            win.Draw(spPlayer);
        }


        // ToDo: Kürzen möglich?
        /// <summary>
        /// <para>Bewegungssteuerung für den Herausforderungsmodus. </para>
        /// 
        /// <para>Alle 20 Sekunden (nach Ablauf der 20 Sekunden startet die Stoppuhr von vorn) wird mithilfe eines Random-Wertes die Zuordnung der Tastatur für die Bewegungsrichtungen neu zugeordnet, wobei alle 24 Fälle abgedeckt werden. </para>
        /// <para>Ebenso wird die Textur jeweils angepasst, falls man sich nicht mehr bewegt. </para>
        /// </summary>
        public void Changingmove(Map map, GameTime time)
        {
            if (gtMoveTime.Watch.ElapsedMilliseconds >= 20000)
            {
                I_moveChangerState = random.Next(24);
                gtMoveTime.Watch.Restart();
            }

            switch (I_moveChangerState)
            {
                case 0:

                    if (Keyboard.IsKeyPressed(Keyboard.Key.A) && map.iswalkable((spPlayer), new Vector2f(-F_runningSpeed, 0)))
                    {
                        spPlayer.Position = new Vector2f(spPlayer.Position.X - F_runningSpeed, spPlayer.Position.Y);
                        if (time.TotalTime.Milliseconds % 500 < 250) this.spPlayer.Texture = txLeft1;
                        else this.spPlayer.Texture = txLeft2;
                        B_isPressed = true;
                        I_rememberidle = 0;
                    }

                    if (Keyboard.IsKeyPressed(Keyboard.Key.D) && map.iswalkable((spPlayer), new Vector2f(F_runningSpeed, 0)))
                    {
                        spPlayer.Position = new Vector2f(spPlayer.Position.X + F_runningSpeed, spPlayer.Position.Y);
                        if (time.TotalTime.Milliseconds % 500 < 250) this.spPlayer.Texture = txRight1;
                        else this.spPlayer.Texture = txRight2;
                        B_isPressed = true;
                        I_rememberidle = 1;
                    }

                    if (Keyboard.IsKeyPressed(Keyboard.Key.W) && map.iswalkable((spPlayer), new Vector2f(0, -F_runningSpeed)))
                    {
                        spPlayer.Position = new Vector2f(spPlayer.Position.X, spPlayer.Position.Y - F_runningSpeed);
                        if (time.TotalTime.Milliseconds % 500 < 250) this.spPlayer.Texture = txUp1;
                        else this.spPlayer.Texture = txUp2;
                        B_isPressed = true;
                        I_rememberidle = 2;
                    }

                    if (Keyboard.IsKeyPressed(Keyboard.Key.S) && map.iswalkable((spPlayer), new Vector2f(0, F_runningSpeed)))
                    {
                        spPlayer.Position = new Vector2f(spPlayer.Position.X, spPlayer.Position.Y + F_runningSpeed);
                        if (time.TotalTime.Milliseconds % 500 < 250) this.spPlayer.Texture = txDown1;
                        else this.spPlayer.Texture = txDown2;
                        B_isPressed = true;
                        I_rememberidle = 3;
                    }

                    if (B_isPressed == false)
                    {
                        switch (I_rememberidle)
                        {
                            case 0: this.spPlayer.Texture = txLeft3;
                                break;
                            case 1: this.spPlayer.Texture = txRight3;
                                break;
                            case 2: this.spPlayer.Texture = txUp3;
                                break;
                            case 3: this.spPlayer.Texture = txDown3;
                                break;
                            default: this.spPlayer.Texture = txDown3;
                                break;
                        }
                    }
                    break;





                case 1:

                    if (Keyboard.IsKeyPressed(Keyboard.Key.A) && map.iswalkable((spPlayer), new Vector2f(-F_runningSpeed, 0)))
                    {
                        spPlayer.Position = new Vector2f(spPlayer.Position.X - F_runningSpeed, spPlayer.Position.Y);
                        if (time.TotalTime.Milliseconds % 500 < 250) this.spPlayer.Texture = txLeft1;
                        else this.spPlayer.Texture = txLeft2;
                        B_isPressed = true;
                        I_rememberidle = 0;
                    }

                    if (Keyboard.IsKeyPressed(Keyboard.Key.D) && map.iswalkable((spPlayer), new Vector2f(F_runningSpeed, 0)))
                    {
                        spPlayer.Position = new Vector2f(spPlayer.Position.X + F_runningSpeed, spPlayer.Position.Y);
                        if (time.TotalTime.Milliseconds % 500 < 250) this.spPlayer.Texture = txRight1;
                        else this.spPlayer.Texture = txRight2;
                        B_isPressed = true;
                        I_rememberidle = 1;
                    }

                    if (Keyboard.IsKeyPressed(Keyboard.Key.S) && map.iswalkable((spPlayer), new Vector2f(0, -F_runningSpeed)))
                    {
                        spPlayer.Position = new Vector2f(spPlayer.Position.X, spPlayer.Position.Y - F_runningSpeed);
                        if (time.TotalTime.Milliseconds % 500 < 250) this.spPlayer.Texture = txUp1;
                        else this.spPlayer.Texture = txUp2;
                        B_isPressed = true;
                        I_rememberidle = 2;
                    }

                    if (Keyboard.IsKeyPressed(Keyboard.Key.W) && map.iswalkable((spPlayer), new Vector2f(0, F_runningSpeed)))
                    {
                        spPlayer.Position = new Vector2f(spPlayer.Position.X, spPlayer.Position.Y + F_runningSpeed);
                        if (time.TotalTime.Milliseconds % 500 < 250) this.spPlayer.Texture = txDown1;
                        else this.spPlayer.Texture = txDown2;
                        B_isPressed = true;
                        I_rememberidle = 3;
                    }

                    if (B_isPressed == false)
                    {
                        switch (I_rememberidle)
                        {
                            case 0: this.spPlayer.Texture = txLeft3;
                                break;
                            case 1: this.spPlayer.Texture = txRight3;
                                break;
                            case 2: this.spPlayer.Texture = txUp3;
                                break;
                            case 3: this.spPlayer.Texture = txDown3;
                                break;
                            default: this.spPlayer.Texture = txDown3;
                                break;
                        }
                    }
                    break;


                case 2:

                    if (Keyboard.IsKeyPressed(Keyboard.Key.A) && map.iswalkable((spPlayer), new Vector2f(-F_runningSpeed, 0)))
                    {
                        spPlayer.Position = new Vector2f(spPlayer.Position.X - F_runningSpeed, spPlayer.Position.Y);
                        if (time.TotalTime.Milliseconds % 500 < 250) this.spPlayer.Texture = txLeft1;
                        else this.spPlayer.Texture = txLeft2;
                        B_isPressed = true;
                        I_rememberidle = 0;
                    }

                    if (Keyboard.IsKeyPressed(Keyboard.Key.S) && map.iswalkable((spPlayer), new Vector2f(F_runningSpeed, 0)))
                    {
                        spPlayer.Position = new Vector2f(spPlayer.Position.X + F_runningSpeed, spPlayer.Position.Y);
                        if (time.TotalTime.Milliseconds % 500 < 250) this.spPlayer.Texture = txRight1;
                        else this.spPlayer.Texture = txRight2;
                        B_isPressed = true;
                        I_rememberidle = 1;
                    }

                    if (Keyboard.IsKeyPressed(Keyboard.Key.W) && map.iswalkable((spPlayer), new Vector2f(0, -F_runningSpeed)))
                    {
                        spPlayer.Position = new Vector2f(spPlayer.Position.X, spPlayer.Position.Y - F_runningSpeed);
                        if (time.TotalTime.Milliseconds % 500 < 250) this.spPlayer.Texture = txUp1;
                        else this.spPlayer.Texture = txUp2;
                        B_isPressed = true;
                        I_rememberidle = 2;
                    }

                    if (Keyboard.IsKeyPressed(Keyboard.Key.D) && map.iswalkable((spPlayer), new Vector2f(0, F_runningSpeed)))
                    {
                        spPlayer.Position = new Vector2f(spPlayer.Position.X, spPlayer.Position.Y + F_runningSpeed);
                        if (time.TotalTime.Milliseconds % 500 < 250) this.spPlayer.Texture = txDown1;
                        else this.spPlayer.Texture = txDown2;
                        B_isPressed = true;
                        I_rememberidle = 3;
                    }

                    if (B_isPressed == false)
                    {
                        switch (I_rememberidle)
                        {
                            case 0: this.spPlayer.Texture = txLeft3;
                                break;
                            case 1: this.spPlayer.Texture = txRight3;
                                break;
                            case 2: this.spPlayer.Texture = txUp3;
                                break;
                            case 3: this.spPlayer.Texture = txDown3;
                                break;
                            default: this.spPlayer.Texture = txDown3;
                                break;
                        }
                    }

                    break;


                case 3:



                    if (Keyboard.IsKeyPressed(Keyboard.Key.A) && map.iswalkable((spPlayer), new Vector2f(-F_runningSpeed, 0)))
                    {
                        spPlayer.Position = new Vector2f(spPlayer.Position.X - F_runningSpeed, spPlayer.Position.Y);
                        if (time.TotalTime.Milliseconds % 500 < 250) this.spPlayer.Texture = txLeft1;
                        else this.spPlayer.Texture = txLeft2;
                        B_isPressed = true;
                        I_rememberidle = 0;
                    }

                    if (Keyboard.IsKeyPressed(Keyboard.Key.S) && map.iswalkable((spPlayer), new Vector2f(F_runningSpeed, 0)))
                    {
                        spPlayer.Position = new Vector2f(spPlayer.Position.X + F_runningSpeed, spPlayer.Position.Y);
                        if (time.TotalTime.Milliseconds % 500 < 250) this.spPlayer.Texture = txRight1;
                        else this.spPlayer.Texture = txRight2;
                        B_isPressed = true;
                        I_rememberidle = 1;
                    }

                    if (Keyboard.IsKeyPressed(Keyboard.Key.D) && map.iswalkable((spPlayer), new Vector2f(0, -F_runningSpeed)))
                    {
                        spPlayer.Position = new Vector2f(spPlayer.Position.X, spPlayer.Position.Y - F_runningSpeed);
                        if (time.TotalTime.Milliseconds % 500 < 250) this.spPlayer.Texture = txUp1;
                        else this.spPlayer.Texture = txUp2;
                        B_isPressed = true;
                        I_rememberidle = 2;
                    }

                    if (Keyboard.IsKeyPressed(Keyboard.Key.W) && map.iswalkable((spPlayer), new Vector2f(0, F_runningSpeed)))
                    {
                        spPlayer.Position = new Vector2f(spPlayer.Position.X, spPlayer.Position.Y + F_runningSpeed);
                        if (time.TotalTime.Milliseconds % 500 < 250) this.spPlayer.Texture = txDown1;
                        else this.spPlayer.Texture = txDown2;
                        B_isPressed = true;
                        I_rememberidle = 3;
                    }

                    if (B_isPressed == false)
                    {
                        switch (I_rememberidle)
                        {
                            case 0: this.spPlayer.Texture = txLeft3;
                                break;
                            case 1: this.spPlayer.Texture = txRight3;
                                break;
                            case 2: this.spPlayer.Texture = txUp3;
                                break;
                            case 3: this.spPlayer.Texture = txDown3;
                                break;
                            default: this.spPlayer.Texture = txDown3;
                                break;
                        }
                    }
                    break;



                case 4:


                    if (Keyboard.IsKeyPressed(Keyboard.Key.A) && map.iswalkable((spPlayer), new Vector2f(-F_runningSpeed, 0)))
                    {
                        spPlayer.Position = new Vector2f(spPlayer.Position.X - F_runningSpeed, spPlayer.Position.Y);
                        if (time.TotalTime.Milliseconds % 500 < 250) this.spPlayer.Texture = txLeft1;
                        else this.spPlayer.Texture = txLeft2;
                        B_isPressed = true;
                        I_rememberidle = 0;
                    }

                    if (Keyboard.IsKeyPressed(Keyboard.Key.W) && map.iswalkable((spPlayer), new Vector2f(F_runningSpeed, 0)))
                    {
                        spPlayer.Position = new Vector2f(spPlayer.Position.X + F_runningSpeed, spPlayer.Position.Y);
                        if (time.TotalTime.Milliseconds % 500 < 250) this.spPlayer.Texture = txRight1;
                        else this.spPlayer.Texture = txRight2;
                        B_isPressed = true;
                        I_rememberidle = 1;
                    }

                    if (Keyboard.IsKeyPressed(Keyboard.Key.S) && map.iswalkable((spPlayer), new Vector2f(0, -F_runningSpeed)))
                    {
                        spPlayer.Position = new Vector2f(spPlayer.Position.X, spPlayer.Position.Y - F_runningSpeed);
                        if (time.TotalTime.Milliseconds % 500 < 250) this.spPlayer.Texture = txUp1;
                        else this.spPlayer.Texture = txUp2;
                        B_isPressed = true;
                        I_rememberidle = 2;
                    }

                    if (Keyboard.IsKeyPressed(Keyboard.Key.D) && map.iswalkable((spPlayer), new Vector2f(0, F_runningSpeed)))
                    {
                        spPlayer.Position = new Vector2f(spPlayer.Position.X, spPlayer.Position.Y + F_runningSpeed);
                        if (time.TotalTime.Milliseconds % 500 < 250) this.spPlayer.Texture = txDown1;
                        else this.spPlayer.Texture = txDown2;
                        B_isPressed = true;
                        I_rememberidle = 3;
                    }

                    if (B_isPressed == false)
                    {
                        switch (I_rememberidle)
                        {
                            case 0: this.spPlayer.Texture = txLeft3;
                                break;
                            case 1: this.spPlayer.Texture = txRight3;
                                break;
                            case 2: this.spPlayer.Texture = txUp3;
                                break;
                            case 3: this.spPlayer.Texture = txDown3;
                                break;
                            default: this.spPlayer.Texture = txDown3;
                                break;
                        }
                    }
                    break;


                case 5:
                    if (Keyboard.IsKeyPressed(Keyboard.Key.A) && map.iswalkable((spPlayer), new Vector2f(-F_runningSpeed, 0)))
                    {
                        spPlayer.Position = new Vector2f(spPlayer.Position.X - F_runningSpeed, spPlayer.Position.Y);
                        if (time.TotalTime.Milliseconds % 500 < 250) this.spPlayer.Texture = txLeft1;
                        else this.spPlayer.Texture = txLeft2;
                        B_isPressed = true;
                        I_rememberidle = 0;
                    }

                    if (Keyboard.IsKeyPressed(Keyboard.Key.W) && map.iswalkable((spPlayer), new Vector2f(F_runningSpeed, 0)))
                    {
                        spPlayer.Position = new Vector2f(spPlayer.Position.X + F_runningSpeed, spPlayer.Position.Y);
                        if (time.TotalTime.Milliseconds % 500 < 250) this.spPlayer.Texture = txRight1;
                        else this.spPlayer.Texture = txRight2;
                        B_isPressed = true;
                        I_rememberidle = 1;
                    }

                    if (Keyboard.IsKeyPressed(Keyboard.Key.D) && map.iswalkable((spPlayer), new Vector2f(0, -F_runningSpeed)))
                    {
                        spPlayer.Position = new Vector2f(spPlayer.Position.X, spPlayer.Position.Y - F_runningSpeed);
                        if (time.TotalTime.Milliseconds % 500 < 250) this.spPlayer.Texture = txUp1;
                        else this.spPlayer.Texture = txUp2;
                        B_isPressed = true;
                        I_rememberidle = 2;
                    }

                    if (Keyboard.IsKeyPressed(Keyboard.Key.S) && map.iswalkable((spPlayer), new Vector2f(0, F_runningSpeed)))
                    {
                        spPlayer.Position = new Vector2f(spPlayer.Position.X, spPlayer.Position.Y + F_runningSpeed);
                        if (time.TotalTime.Milliseconds % 500 < 250) this.spPlayer.Texture = txDown1;
                        else this.spPlayer.Texture = txDown2;
                        B_isPressed = true;
                        I_rememberidle = 3;
                    }

                    if (B_isPressed == false)
                    {
                        switch (I_rememberidle)
                        {
                            case 0: this.spPlayer.Texture = txLeft3;
                                break;
                            case 1: this.spPlayer.Texture = txRight3;
                                break;
                            case 2: this.spPlayer.Texture = txUp3;
                                break;
                            case 3: this.spPlayer.Texture = txDown3;
                                break;
                            default: this.spPlayer.Texture = txDown3;
                                break;
                        }
                    }
                    break;

                case 6:
                    if (Keyboard.IsKeyPressed(Keyboard.Key.D) && map.iswalkable((spPlayer), new Vector2f(-F_runningSpeed, 0)))
                    {
                        spPlayer.Position = new Vector2f(spPlayer.Position.X - F_runningSpeed, spPlayer.Position.Y);
                        if (time.TotalTime.Milliseconds % 500 < 250) this.spPlayer.Texture = txLeft1;
                        else this.spPlayer.Texture = txLeft2;
                        B_isPressed = true;
                        I_rememberidle = 0;
                    }

                    if (Keyboard.IsKeyPressed(Keyboard.Key.A) && map.iswalkable((spPlayer), new Vector2f(F_runningSpeed, 0)))
                    {
                        spPlayer.Position = new Vector2f(spPlayer.Position.X + F_runningSpeed, spPlayer.Position.Y);
                        if (time.TotalTime.Milliseconds % 500 < 250) this.spPlayer.Texture = txRight1;
                        else this.spPlayer.Texture = txRight2;
                        B_isPressed = true;
                        I_rememberidle = 1;
                    }

                    if (Keyboard.IsKeyPressed(Keyboard.Key.W) && map.iswalkable((spPlayer), new Vector2f(0, -F_runningSpeed)))
                    {
                        spPlayer.Position = new Vector2f(spPlayer.Position.X, spPlayer.Position.Y - F_runningSpeed);
                        if (time.TotalTime.Milliseconds % 500 < 250) this.spPlayer.Texture = txUp1;
                        else this.spPlayer.Texture = txUp2;
                        B_isPressed = true;
                        I_rememberidle = 2;
                    }

                    if (Keyboard.IsKeyPressed(Keyboard.Key.S) && map.iswalkable((spPlayer), new Vector2f(0, F_runningSpeed)))
                    {
                        spPlayer.Position = new Vector2f(spPlayer.Position.X, spPlayer.Position.Y + F_runningSpeed);
                        if (time.TotalTime.Milliseconds % 500 < 250) this.spPlayer.Texture = txDown1;
                        else this.spPlayer.Texture = txDown2;
                        B_isPressed = true;
                        I_rememberidle = 3;
                    }

                    if (B_isPressed == false)
                    {
                        switch (I_rememberidle)
                        {
                            case 0: this.spPlayer.Texture = txLeft3;
                                break;
                            case 1: this.spPlayer.Texture = txRight3;
                                break;
                            case 2: this.spPlayer.Texture = txUp3;
                                break;
                            case 3: this.spPlayer.Texture = txDown3;
                                break;
                            default: this.spPlayer.Texture = txDown3;
                                break;
                        }
                    }
                    break;


                case 7:
                    if (Keyboard.IsKeyPressed(Keyboard.Key.D) && map.iswalkable((spPlayer), new Vector2f(-F_runningSpeed, 0)))
                    {
                        spPlayer.Position = new Vector2f(spPlayer.Position.X - F_runningSpeed, spPlayer.Position.Y);
                        if (time.TotalTime.Milliseconds % 500 < 250) this.spPlayer.Texture = txLeft1;
                        else this.spPlayer.Texture = txLeft2;
                        B_isPressed = true;
                        I_rememberidle = 0;
                    }

                    if (Keyboard.IsKeyPressed(Keyboard.Key.A) && map.iswalkable((spPlayer), new Vector2f(F_runningSpeed, 0)))
                    {
                        spPlayer.Position = new Vector2f(spPlayer.Position.X + F_runningSpeed, spPlayer.Position.Y);
                        if (time.TotalTime.Milliseconds % 500 < 250) this.spPlayer.Texture = txRight1;
                        else this.spPlayer.Texture = txRight2;
                        B_isPressed = true;
                        I_rememberidle = 1;
                    }

                    if (Keyboard.IsKeyPressed(Keyboard.Key.S) && map.iswalkable((spPlayer), new Vector2f(0, -F_runningSpeed)))
                    {
                        spPlayer.Position = new Vector2f(spPlayer.Position.X, spPlayer.Position.Y - F_runningSpeed);
                        if (time.TotalTime.Milliseconds % 500 < 250) this.spPlayer.Texture = txUp1;
                        else this.spPlayer.Texture = txUp2;
                        B_isPressed = true;
                        I_rememberidle = 2;
                    }

                    if (Keyboard.IsKeyPressed(Keyboard.Key.W) && map.iswalkable((spPlayer), new Vector2f(0, F_runningSpeed)))
                    {
                        spPlayer.Position = new Vector2f(spPlayer.Position.X, spPlayer.Position.Y + F_runningSpeed);
                        if (time.TotalTime.Milliseconds % 500 < 250) this.spPlayer.Texture = txDown1;
                        else this.spPlayer.Texture = txDown2;
                        B_isPressed = true;
                        I_rememberidle = 3;
                    }

                    if (B_isPressed == false)
                    {
                        switch (I_rememberidle)
                        {
                            case 0: this.spPlayer.Texture = txLeft3;
                                break;
                            case 1: this.spPlayer.Texture = txRight3;
                                break;
                            case 2: this.spPlayer.Texture = txUp3;
                                break;
                            case 3: this.spPlayer.Texture = txDown3;
                                break;
                            default: this.spPlayer.Texture = txDown3;
                                break;
                        }
                    }
                    break;



                case 8:

                    if (Keyboard.IsKeyPressed(Keyboard.Key.D) && map.iswalkable((spPlayer), new Vector2f(-F_runningSpeed, 0)))
                    {
                        spPlayer.Position = new Vector2f(spPlayer.Position.X - F_runningSpeed, spPlayer.Position.Y);
                        if (time.TotalTime.Milliseconds % 500 < 250) this.spPlayer.Texture = txLeft1;
                        else this.spPlayer.Texture = txLeft2;
                        B_isPressed = true;
                        I_rememberidle = 0;
                    }

                    if (Keyboard.IsKeyPressed(Keyboard.Key.S) && map.iswalkable((spPlayer), new Vector2f(F_runningSpeed, 0)))
                    {
                        spPlayer.Position = new Vector2f(spPlayer.Position.X + F_runningSpeed, spPlayer.Position.Y);
                        if (time.TotalTime.Milliseconds % 500 < 250) this.spPlayer.Texture = txRight1;
                        else this.spPlayer.Texture = txRight2;
                        B_isPressed = true;
                        I_rememberidle = 1;
                    }

                    if (Keyboard.IsKeyPressed(Keyboard.Key.A) && map.iswalkable((spPlayer), new Vector2f(0, -F_runningSpeed)))
                    {
                        spPlayer.Position = new Vector2f(spPlayer.Position.X, spPlayer.Position.Y - F_runningSpeed);
                        if (time.TotalTime.Milliseconds % 500 < 250) this.spPlayer.Texture = txUp1;
                        else this.spPlayer.Texture = txUp2;
                        B_isPressed = true;
                        I_rememberidle = 2;
                    }

                    if (Keyboard.IsKeyPressed(Keyboard.Key.W) && map.iswalkable((spPlayer), new Vector2f(0, F_runningSpeed)))
                    {
                        spPlayer.Position = new Vector2f(spPlayer.Position.X, spPlayer.Position.Y + F_runningSpeed);
                        if (time.TotalTime.Milliseconds % 500 < 250) this.spPlayer.Texture = txDown1;
                        else this.spPlayer.Texture = txDown2;
                        B_isPressed = true;
                        I_rememberidle = 3;
                    }

                    if (B_isPressed == false)
                    {
                        switch (I_rememberidle)
                        {
                            case 0: this.spPlayer.Texture = txLeft3;
                                break;
                            case 1: this.spPlayer.Texture = txRight3;
                                break;
                            case 2: this.spPlayer.Texture = txUp3;
                                break;
                            case 3: this.spPlayer.Texture = txDown3;
                                break;
                            default: this.spPlayer.Texture = txDown3;
                                break;
                        }
                    }
                    break;


                case 9:

                    if (Keyboard.IsKeyPressed(Keyboard.Key.D) && map.iswalkable((spPlayer), new Vector2f(-F_runningSpeed, 0)))
                    {
                        spPlayer.Position = new Vector2f(spPlayer.Position.X - F_runningSpeed, spPlayer.Position.Y);
                        if (time.TotalTime.Milliseconds % 500 < 250) this.spPlayer.Texture = txLeft1;
                        else this.spPlayer.Texture = txLeft2;
                        B_isPressed = true;
                        I_rememberidle = 0;
                    }

                    if (Keyboard.IsKeyPressed(Keyboard.Key.S) && map.iswalkable((spPlayer), new Vector2f(F_runningSpeed, 0)))
                    {
                        spPlayer.Position = new Vector2f(spPlayer.Position.X + F_runningSpeed, spPlayer.Position.Y);
                        if (time.TotalTime.Milliseconds % 500 < 250) this.spPlayer.Texture = txRight1;
                        else this.spPlayer.Texture = txRight2;
                        B_isPressed = true;
                        I_rememberidle = 1;
                    }

                    if (Keyboard.IsKeyPressed(Keyboard.Key.W) && map.iswalkable((spPlayer), new Vector2f(0, -F_runningSpeed)))
                    {
                        spPlayer.Position = new Vector2f(spPlayer.Position.X, spPlayer.Position.Y - F_runningSpeed);
                        if (time.TotalTime.Milliseconds % 500 < 250) this.spPlayer.Texture = txUp1;
                        else this.spPlayer.Texture = txUp2;
                        B_isPressed = true;
                        I_rememberidle = 2;
                    }

                    if (Keyboard.IsKeyPressed(Keyboard.Key.A) && map.iswalkable((spPlayer), new Vector2f(0, F_runningSpeed)))
                    {
                        spPlayer.Position = new Vector2f(spPlayer.Position.X, spPlayer.Position.Y + F_runningSpeed);
                        if (time.TotalTime.Milliseconds % 500 < 250) this.spPlayer.Texture = txDown1;
                        else this.spPlayer.Texture = txDown2;
                        B_isPressed = true;
                        I_rememberidle = 3;
                    }

                    if (B_isPressed == false)
                    {
                        switch (I_rememberidle)
                        {
                            case 0: this.spPlayer.Texture = txLeft3;
                                break;
                            case 1: this.spPlayer.Texture = txRight3;
                                break;
                            case 2: this.spPlayer.Texture = txUp3;
                                break;
                            case 3: this.spPlayer.Texture = txDown3;
                                break;
                            default: this.spPlayer.Texture = txDown3;
                                break;
                        }
                    }
                    break;



                case 10:

                    if (Keyboard.IsKeyPressed(Keyboard.Key.D) && map.iswalkable((spPlayer), new Vector2f(-F_runningSpeed, 0)))
                    {
                        spPlayer.Position = new Vector2f(spPlayer.Position.X - F_runningSpeed, spPlayer.Position.Y);
                        if (time.TotalTime.Milliseconds % 500 < 250) this.spPlayer.Texture = txLeft1;
                        else this.spPlayer.Texture = txLeft2;
                        B_isPressed = true;
                        I_rememberidle = 0;
                    }

                    if (Keyboard.IsKeyPressed(Keyboard.Key.W) && map.iswalkable((spPlayer), new Vector2f(F_runningSpeed, 0)))
                    {
                        spPlayer.Position = new Vector2f(spPlayer.Position.X + F_runningSpeed, spPlayer.Position.Y);
                        if (time.TotalTime.Milliseconds % 500 < 250) this.spPlayer.Texture = txRight1;
                        else this.spPlayer.Texture = txRight2;
                        B_isPressed = true;
                        I_rememberidle = 1;
                    }

                    if (Keyboard.IsKeyPressed(Keyboard.Key.S) && map.iswalkable((spPlayer), new Vector2f(0, -F_runningSpeed)))
                    {
                        spPlayer.Position = new Vector2f(spPlayer.Position.X, spPlayer.Position.Y - F_runningSpeed);
                        if (time.TotalTime.Milliseconds % 500 < 250) this.spPlayer.Texture = txUp1;
                        else this.spPlayer.Texture = txUp2;
                        B_isPressed = true;
                        I_rememberidle = 2;
                    }

                    if (Keyboard.IsKeyPressed(Keyboard.Key.A) && map.iswalkable((spPlayer), new Vector2f(0, F_runningSpeed)))
                    {
                        spPlayer.Position = new Vector2f(spPlayer.Position.X, spPlayer.Position.Y + F_runningSpeed);
                        if (time.TotalTime.Milliseconds % 500 < 250) this.spPlayer.Texture = txDown1;
                        else this.spPlayer.Texture = txDown2;
                        B_isPressed = true;
                        I_rememberidle = 3;
                    }

                    if (B_isPressed == false)
                    {
                        switch (I_rememberidle)
                        {
                            case 0: this.spPlayer.Texture = txLeft3;
                                break;
                            case 1: this.spPlayer.Texture = txRight3;
                                break;
                            case 2: this.spPlayer.Texture = txUp3;
                                break;
                            case 3: this.spPlayer.Texture = txDown3;
                                break;
                            default: this.spPlayer.Texture = txDown3;
                                break;
                        }
                    }
                    break;

                case 11:

                    if (Keyboard.IsKeyPressed(Keyboard.Key.D) && map.iswalkable((spPlayer), new Vector2f(-F_runningSpeed, 0)))
                    {
                        spPlayer.Position = new Vector2f(spPlayer.Position.X - F_runningSpeed, spPlayer.Position.Y);
                        if (time.TotalTime.Milliseconds % 500 < 250) this.spPlayer.Texture = txLeft1;
                        else this.spPlayer.Texture = txLeft2;
                        B_isPressed = true;
                        I_rememberidle = 0;
                    }

                    if (Keyboard.IsKeyPressed(Keyboard.Key.W) && map.iswalkable((spPlayer), new Vector2f(F_runningSpeed, 0)))
                    {
                        spPlayer.Position = new Vector2f(spPlayer.Position.X + F_runningSpeed, spPlayer.Position.Y);
                        if (time.TotalTime.Milliseconds % 500 < 250) this.spPlayer.Texture = txRight1;
                        else this.spPlayer.Texture = txRight2;
                        B_isPressed = true;
                        I_rememberidle = 1;
                    }

                    if (Keyboard.IsKeyPressed(Keyboard.Key.A) && map.iswalkable((spPlayer), new Vector2f(0, -F_runningSpeed)))
                    {
                        spPlayer.Position = new Vector2f(spPlayer.Position.X, spPlayer.Position.Y - F_runningSpeed);
                        if (time.TotalTime.Milliseconds % 500 < 250) this.spPlayer.Texture = txUp1;
                        else this.spPlayer.Texture = txUp2;
                        B_isPressed = true;
                        I_rememberidle = 2;
                    }

                    if (Keyboard.IsKeyPressed(Keyboard.Key.S) && map.iswalkable((spPlayer), new Vector2f(0, F_runningSpeed)))
                    {
                        spPlayer.Position = new Vector2f(spPlayer.Position.X, spPlayer.Position.Y + F_runningSpeed);
                        if (time.TotalTime.Milliseconds % 500 < 250) this.spPlayer.Texture = txDown1;
                        else this.spPlayer.Texture = txDown2;
                        B_isPressed = true;
                        I_rememberidle = 3;
                    }

                    if (B_isPressed == false)
                    {
                        switch (I_rememberidle)
                        {
                            case 0: this.spPlayer.Texture = txLeft3;
                                break;
                            case 1: this.spPlayer.Texture = txRight3;
                                break;
                            case 2: this.spPlayer.Texture = txUp3;
                                break;
                            case 3: this.spPlayer.Texture = txDown3;
                                break;
                            default: this.spPlayer.Texture = txDown3;
                                break;
                        }
                    }
                    break;


                case 12:

                    if (Keyboard.IsKeyPressed(Keyboard.Key.S) && map.iswalkable((spPlayer), new Vector2f(-F_runningSpeed, 0)))
                    {
                        spPlayer.Position = new Vector2f(spPlayer.Position.X - F_runningSpeed, spPlayer.Position.Y);
                        if (time.TotalTime.Milliseconds % 500 < 250) this.spPlayer.Texture = txLeft1;
                        else this.spPlayer.Texture = txLeft2;
                        B_isPressed = true;
                        I_rememberidle = 0;
                    }

                    if (Keyboard.IsKeyPressed(Keyboard.Key.A) && map.iswalkable((spPlayer), new Vector2f(F_runningSpeed, 0)))
                    {
                        spPlayer.Position = new Vector2f(spPlayer.Position.X + F_runningSpeed, spPlayer.Position.Y);
                        if (time.TotalTime.Milliseconds % 500 < 250) this.spPlayer.Texture = txRight1;
                        else this.spPlayer.Texture = txRight2;
                        B_isPressed = true;
                        I_rememberidle = 1;
                    }

                    if (Keyboard.IsKeyPressed(Keyboard.Key.W) && map.iswalkable((spPlayer), new Vector2f(0, -F_runningSpeed)))
                    {
                        spPlayer.Position = new Vector2f(spPlayer.Position.X, spPlayer.Position.Y - F_runningSpeed);
                        if (time.TotalTime.Milliseconds % 500 < 250) this.spPlayer.Texture = txUp1;
                        else this.spPlayer.Texture = txUp2;
                        B_isPressed = true;
                        I_rememberidle = 2;
                    }

                    if (Keyboard.IsKeyPressed(Keyboard.Key.D) && map.iswalkable((spPlayer), new Vector2f(0, F_runningSpeed)))
                    {
                        spPlayer.Position = new Vector2f(spPlayer.Position.X, spPlayer.Position.Y + F_runningSpeed);
                        if (time.TotalTime.Milliseconds % 500 < 250) this.spPlayer.Texture = txDown1;
                        else this.spPlayer.Texture = txDown2;
                        B_isPressed = true;
                        I_rememberidle = 3;
                    }

                    if (B_isPressed == false)
                    {
                        switch (I_rememberidle)
                        {
                            case 0: this.spPlayer.Texture = txLeft3;
                                break;
                            case 1: this.spPlayer.Texture = txRight3;
                                break;
                            case 2: this.spPlayer.Texture = txUp3;
                                break;
                            case 3: this.spPlayer.Texture = txDown3;
                                break;
                            default: this.spPlayer.Texture = txDown3;
                                break;
                        }
                    }
                    break;


                case 13:

                    if (Keyboard.IsKeyPressed(Keyboard.Key.S) && map.iswalkable((spPlayer), new Vector2f(-F_runningSpeed, 0)))
                    {
                        spPlayer.Position = new Vector2f(spPlayer.Position.X - F_runningSpeed, spPlayer.Position.Y);
                        if (time.TotalTime.Milliseconds % 500 < 250) this.spPlayer.Texture = txLeft1;
                        else this.spPlayer.Texture = txLeft2;
                        B_isPressed = true;
                        I_rememberidle = 0;
                    }

                    if (Keyboard.IsKeyPressed(Keyboard.Key.A) && map.iswalkable((spPlayer), new Vector2f(F_runningSpeed, 0)))
                    {
                        spPlayer.Position = new Vector2f(spPlayer.Position.X + F_runningSpeed, spPlayer.Position.Y);
                        if (time.TotalTime.Milliseconds % 500 < 250) this.spPlayer.Texture = txRight1;
                        else this.spPlayer.Texture = txRight2;
                        B_isPressed = true;
                        I_rememberidle = 1;
                    }

                    if (Keyboard.IsKeyPressed(Keyboard.Key.D) && map.iswalkable((spPlayer), new Vector2f(0, -F_runningSpeed)))
                    {
                        spPlayer.Position = new Vector2f(spPlayer.Position.X, spPlayer.Position.Y - F_runningSpeed);
                        if (time.TotalTime.Milliseconds % 500 < 250) this.spPlayer.Texture = txUp1;
                        else this.spPlayer.Texture = txUp2;
                        B_isPressed = true;
                        I_rememberidle = 2;
                    }

                    if (Keyboard.IsKeyPressed(Keyboard.Key.W) && map.iswalkable((spPlayer), new Vector2f(0, F_runningSpeed)))
                    {
                        spPlayer.Position = new Vector2f(spPlayer.Position.X, spPlayer.Position.Y + F_runningSpeed);
                        if (time.TotalTime.Milliseconds % 500 < 250) this.spPlayer.Texture = txDown1;
                        else this.spPlayer.Texture = txDown2;
                        B_isPressed = true;
                        I_rememberidle = 3;
                    }

                    if (B_isPressed == false)
                    {
                        switch (I_rememberidle)
                        {
                            case 0: this.spPlayer.Texture = txLeft3;
                                break;
                            case 1: this.spPlayer.Texture = txRight3;
                                break;
                            case 2: this.spPlayer.Texture = txUp3;
                                break;
                            case 3: this.spPlayer.Texture = txDown3;
                                break;
                            default: this.spPlayer.Texture = txDown3;
                                break;
                        }
                    }
                    break;


                case 14:

                    if (Keyboard.IsKeyPressed(Keyboard.Key.S) && map.iswalkable((spPlayer), new Vector2f(-F_runningSpeed, 0)))
                    {
                        spPlayer.Position = new Vector2f(spPlayer.Position.X - F_runningSpeed, spPlayer.Position.Y);
                        if (time.TotalTime.Milliseconds % 500 < 250) this.spPlayer.Texture = txLeft1;
                        else this.spPlayer.Texture = txLeft2;
                        B_isPressed = true;
                        I_rememberidle = 0;
                    }

                    if (Keyboard.IsKeyPressed(Keyboard.Key.W) && map.iswalkable((spPlayer), new Vector2f(F_runningSpeed, 0)))
                    {
                        spPlayer.Position = new Vector2f(spPlayer.Position.X + F_runningSpeed, spPlayer.Position.Y);
                        if (time.TotalTime.Milliseconds % 500 < 250) this.spPlayer.Texture = txRight1;
                        else this.spPlayer.Texture = txRight2;
                        B_isPressed = true;
                        I_rememberidle = 1;
                    }

                    if (Keyboard.IsKeyPressed(Keyboard.Key.A) && map.iswalkable((spPlayer), new Vector2f(0, -F_runningSpeed)))
                    {
                        spPlayer.Position = new Vector2f(spPlayer.Position.X, spPlayer.Position.Y - F_runningSpeed);
                        if (time.TotalTime.Milliseconds % 500 < 250) this.spPlayer.Texture = txUp1;
                        else this.spPlayer.Texture = txUp2;
                        B_isPressed = true;
                        I_rememberidle = 2;
                    }

                    if (Keyboard.IsKeyPressed(Keyboard.Key.D) && map.iswalkable((spPlayer), new Vector2f(0, F_runningSpeed)))
                    {
                        spPlayer.Position = new Vector2f(spPlayer.Position.X, spPlayer.Position.Y + F_runningSpeed);
                        if (time.TotalTime.Milliseconds % 500 < 250) this.spPlayer.Texture = txDown1;
                        else this.spPlayer.Texture = txDown2;
                        B_isPressed = true;
                        I_rememberidle = 3;
                    }

                    if (B_isPressed == false)
                    {
                        switch (I_rememberidle)
                        {
                            case 0: this.spPlayer.Texture = txLeft3;
                                break;
                            case 1: this.spPlayer.Texture = txRight3;
                                break;
                            case 2: this.spPlayer.Texture = txUp3;
                                break;
                            case 3: this.spPlayer.Texture = txDown3;
                                break;
                            default: this.spPlayer.Texture = txDown3;
                                break;
                        }
                    }
                    break;



                case 15:

                    if (Keyboard.IsKeyPressed(Keyboard.Key.S) && map.iswalkable((spPlayer), new Vector2f(-F_runningSpeed, 0)))
                    {
                        spPlayer.Position = new Vector2f(spPlayer.Position.X - F_runningSpeed, spPlayer.Position.Y);
                        if (time.TotalTime.Milliseconds % 500 < 250) this.spPlayer.Texture = txLeft1;
                        else this.spPlayer.Texture = txLeft2;
                        B_isPressed = true;
                        I_rememberidle = 0;
                    }

                    if (Keyboard.IsKeyPressed(Keyboard.Key.W) && map.iswalkable((spPlayer), new Vector2f(F_runningSpeed, 0)))
                    {
                        spPlayer.Position = new Vector2f(spPlayer.Position.X + F_runningSpeed, spPlayer.Position.Y);
                        if (time.TotalTime.Milliseconds % 500 < 250) this.spPlayer.Texture = txRight1;
                        else this.spPlayer.Texture = txRight2;
                        B_isPressed = true;
                        I_rememberidle = 1;
                    }

                    if (Keyboard.IsKeyPressed(Keyboard.Key.D) && map.iswalkable((spPlayer), new Vector2f(0, -F_runningSpeed)))
                    {
                        spPlayer.Position = new Vector2f(spPlayer.Position.X, spPlayer.Position.Y - F_runningSpeed);
                        if (time.TotalTime.Milliseconds % 500 < 250) this.spPlayer.Texture = txUp1;
                        else this.spPlayer.Texture = txUp2;
                        B_isPressed = true;
                        I_rememberidle = 2;
                    }

                    if (Keyboard.IsKeyPressed(Keyboard.Key.A) && map.iswalkable((spPlayer), new Vector2f(0, F_runningSpeed)))
                    {
                        spPlayer.Position = new Vector2f(spPlayer.Position.X, spPlayer.Position.Y + F_runningSpeed);
                        if (time.TotalTime.Milliseconds % 500 < 250) this.spPlayer.Texture = txDown1;
                        else this.spPlayer.Texture = txDown2;
                        B_isPressed = true;
                        I_rememberidle = 3;
                    }

                    if (B_isPressed == false)
                    {
                        switch (I_rememberidle)
                        {
                            case 0: this.spPlayer.Texture = txLeft3;
                                break;
                            case 1: this.spPlayer.Texture = txRight3;
                                break;
                            case 2: this.spPlayer.Texture = txUp3;
                                break;
                            case 3: this.spPlayer.Texture = txDown3;
                                break;
                            default: this.spPlayer.Texture = txDown3;
                                break;
                        }
                    }
                    break;



                case 16:

                    if (Keyboard.IsKeyPressed(Keyboard.Key.S) && map.iswalkable((spPlayer), new Vector2f(-F_runningSpeed, 0)))
                    {
                        spPlayer.Position = new Vector2f(spPlayer.Position.X - F_runningSpeed, spPlayer.Position.Y);
                        if (time.TotalTime.Milliseconds % 500 < 250) this.spPlayer.Texture = txLeft1;
                        else this.spPlayer.Texture = txLeft2;
                        B_isPressed = true;
                        I_rememberidle = 0;
                    }

                    if (Keyboard.IsKeyPressed(Keyboard.Key.D) && map.iswalkable((spPlayer), new Vector2f(F_runningSpeed, 0)))
                    {
                        spPlayer.Position = new Vector2f(spPlayer.Position.X + F_runningSpeed, spPlayer.Position.Y);
                        if (time.TotalTime.Milliseconds % 500 < 250) this.spPlayer.Texture = txRight1;
                        else this.spPlayer.Texture = txRight2;
                        B_isPressed = true;
                        I_rememberidle = 1;
                    }

                    if (Keyboard.IsKeyPressed(Keyboard.Key.W) && map.iswalkable((spPlayer), new Vector2f(0, -F_runningSpeed)))
                    {
                        spPlayer.Position = new Vector2f(spPlayer.Position.X, spPlayer.Position.Y - F_runningSpeed);
                        if (time.TotalTime.Milliseconds % 500 < 250) this.spPlayer.Texture = txUp1;
                        else this.spPlayer.Texture = txUp2;
                        B_isPressed = true;
                        I_rememberidle = 2;
                    }

                    if (Keyboard.IsKeyPressed(Keyboard.Key.A) && map.iswalkable((spPlayer), new Vector2f(0, F_runningSpeed)))
                    {
                        spPlayer.Position = new Vector2f(spPlayer.Position.X, spPlayer.Position.Y + F_runningSpeed);
                        if (time.TotalTime.Milliseconds % 500 < 250) this.spPlayer.Texture = txDown1;
                        else this.spPlayer.Texture = txDown2;
                        B_isPressed = true;
                        I_rememberidle = 3;
                    }

                    if (B_isPressed == false)
                    {
                        switch (I_rememberidle)
                        {
                            case 0: this.spPlayer.Texture = txLeft3;
                                break;
                            case 1: this.spPlayer.Texture = txRight3;
                                break;
                            case 2: this.spPlayer.Texture = txUp3;
                                break;
                            case 3: this.spPlayer.Texture = txDown3;
                                break;
                            default: this.spPlayer.Texture = txDown3;
                                break;
                        }
                    }
                    break;



                case 17:

                    if (Keyboard.IsKeyPressed(Keyboard.Key.S) && map.iswalkable((spPlayer), new Vector2f(-F_runningSpeed, 0)))
                    {
                        spPlayer.Position = new Vector2f(spPlayer.Position.X - F_runningSpeed, spPlayer.Position.Y);
                        if (time.TotalTime.Milliseconds % 500 < 250) this.spPlayer.Texture = txLeft1;
                        else this.spPlayer.Texture = txLeft2;
                        B_isPressed = true;
                        I_rememberidle = 0;
                    }

                    if (Keyboard.IsKeyPressed(Keyboard.Key.D) && map.iswalkable((spPlayer), new Vector2f(F_runningSpeed, 0)))
                    {
                        spPlayer.Position = new Vector2f(spPlayer.Position.X + F_runningSpeed, spPlayer.Position.Y);
                        if (time.TotalTime.Milliseconds % 500 < 250) this.spPlayer.Texture = txRight1;
                        else this.spPlayer.Texture = txRight2;
                        B_isPressed = true;
                        I_rememberidle = 1;
                    }

                    if (Keyboard.IsKeyPressed(Keyboard.Key.A) && map.iswalkable((spPlayer), new Vector2f(0, -F_runningSpeed)))
                    {
                        spPlayer.Position = new Vector2f(spPlayer.Position.X, spPlayer.Position.Y - F_runningSpeed);
                        if (time.TotalTime.Milliseconds % 500 < 250) this.spPlayer.Texture = txUp1;
                        else this.spPlayer.Texture = txUp2;
                        B_isPressed = true;
                        I_rememberidle = 2;
                    }

                    if (Keyboard.IsKeyPressed(Keyboard.Key.W) && map.iswalkable((spPlayer), new Vector2f(0, F_runningSpeed)))
                    {
                        spPlayer.Position = new Vector2f(spPlayer.Position.X, spPlayer.Position.Y + F_runningSpeed);
                        if (time.TotalTime.Milliseconds % 500 < 250) this.spPlayer.Texture = txDown1;
                        else this.spPlayer.Texture = txDown2;
                        B_isPressed = true;
                        I_rememberidle = 3;
                    }

                    if (B_isPressed == false)
                    {
                        switch (I_rememberidle)
                        {
                            case 0: this.spPlayer.Texture = txLeft3;
                                break;
                            case 1: this.spPlayer.Texture = txRight3;
                                break;
                            case 2: this.spPlayer.Texture = txUp3;
                                break;
                            case 3: this.spPlayer.Texture = txDown3;
                                break;
                            default: this.spPlayer.Texture = txDown3;
                                break;
                        }
                    }
                    break;



                case 18:

                    if (Keyboard.IsKeyPressed(Keyboard.Key.W) && map.iswalkable((spPlayer), new Vector2f(-F_runningSpeed, 0)))
                    {
                        spPlayer.Position = new Vector2f(spPlayer.Position.X - F_runningSpeed, spPlayer.Position.Y);
                        if (time.TotalTime.Milliseconds % 500 < 250) this.spPlayer.Texture = txLeft1;
                        else this.spPlayer.Texture = txLeft2;
                        B_isPressed = true;
                        I_rememberidle = 0;
                    }

                    if (Keyboard.IsKeyPressed(Keyboard.Key.A) && map.iswalkable((spPlayer), new Vector2f(F_runningSpeed, 0)))
                    {
                        spPlayer.Position = new Vector2f(spPlayer.Position.X + F_runningSpeed, spPlayer.Position.Y);
                        if (time.TotalTime.Milliseconds % 500 < 250) this.spPlayer.Texture = txRight1;
                        else this.spPlayer.Texture = txRight2;
                        B_isPressed = true;
                        I_rememberidle = 1;
                    }

                    if (Keyboard.IsKeyPressed(Keyboard.Key.S) && map.iswalkable((spPlayer), new Vector2f(0, -F_runningSpeed)))
                    {
                        spPlayer.Position = new Vector2f(spPlayer.Position.X, spPlayer.Position.Y - F_runningSpeed);
                        if (time.TotalTime.Milliseconds % 500 < 250) this.spPlayer.Texture = txUp1;
                        else this.spPlayer.Texture = txUp2;
                        B_isPressed = true;
                        I_rememberidle = 2;
                    }

                    if (Keyboard.IsKeyPressed(Keyboard.Key.D) && map.iswalkable((spPlayer), new Vector2f(0, F_runningSpeed)))
                    {
                        spPlayer.Position = new Vector2f(spPlayer.Position.X, spPlayer.Position.Y + F_runningSpeed);
                        if (time.TotalTime.Milliseconds % 500 < 250) this.spPlayer.Texture = txDown1;
                        else this.spPlayer.Texture = txDown2;
                        B_isPressed = true;
                        I_rememberidle = 3;
                    }

                    if (B_isPressed == false)
                    {
                        switch (I_rememberidle)
                        {
                            case 0: this.spPlayer.Texture = txLeft3;
                                break;
                            case 1: this.spPlayer.Texture = txRight3;
                                break;
                            case 2: this.spPlayer.Texture = txUp3;
                                break;
                            case 3: this.spPlayer.Texture = txDown3;
                                break;
                            default: this.spPlayer.Texture = txDown3;
                                break;
                        }
                    }
                    break;




                case 19:

                    if (Keyboard.IsKeyPressed(Keyboard.Key.W) && map.iswalkable((spPlayer), new Vector2f(-F_runningSpeed, 0)))
                    {
                        spPlayer.Position = new Vector2f(spPlayer.Position.X - F_runningSpeed, spPlayer.Position.Y);
                        if (time.TotalTime.Milliseconds % 500 < 250) this.spPlayer.Texture = txLeft1;
                        else this.spPlayer.Texture = txLeft2;
                        B_isPressed = true;
                        I_rememberidle = 0;
                    }

                    if (Keyboard.IsKeyPressed(Keyboard.Key.A) && map.iswalkable((spPlayer), new Vector2f(F_runningSpeed, 0)))
                    {
                        spPlayer.Position = new Vector2f(spPlayer.Position.X + F_runningSpeed, spPlayer.Position.Y);
                        if (time.TotalTime.Milliseconds % 500 < 250) this.spPlayer.Texture = txRight1;
                        else this.spPlayer.Texture = txRight2;
                        B_isPressed = true;
                        I_rememberidle = 1;
                    }

                    if (Keyboard.IsKeyPressed(Keyboard.Key.D) && map.iswalkable((spPlayer), new Vector2f(0, -F_runningSpeed)))
                    {
                        spPlayer.Position = new Vector2f(spPlayer.Position.X, spPlayer.Position.Y - F_runningSpeed);
                        if (time.TotalTime.Milliseconds % 500 < 250) this.spPlayer.Texture = txUp1;
                        else this.spPlayer.Texture = txUp2;
                        B_isPressed = true;
                        I_rememberidle = 2;
                    }

                    if (Keyboard.IsKeyPressed(Keyboard.Key.S) && map.iswalkable((spPlayer), new Vector2f(0, F_runningSpeed)))
                    {
                        spPlayer.Position = new Vector2f(spPlayer.Position.X, spPlayer.Position.Y + F_runningSpeed);
                        if (time.TotalTime.Milliseconds % 500 < 250) this.spPlayer.Texture = txDown1;
                        else this.spPlayer.Texture = txDown2;
                        B_isPressed = true;
                        I_rememberidle = 3;
                    }

                    if (B_isPressed == false)
                    {
                        switch (I_rememberidle)
                        {
                            case 0: this.spPlayer.Texture = txLeft3;
                                break;
                            case 1: this.spPlayer.Texture = txRight3;
                                break;
                            case 2: this.spPlayer.Texture = txUp3;
                                break;
                            case 3: this.spPlayer.Texture = txDown3;
                                break;
                            default: this.spPlayer.Texture = txDown3;
                                break;
                        }
                    }
                    break;




                case 20:

                    if (Keyboard.IsKeyPressed(Keyboard.Key.W) && map.iswalkable((spPlayer), new Vector2f(-F_runningSpeed, 0)))
                    {
                        spPlayer.Position = new Vector2f(spPlayer.Position.X - F_runningSpeed, spPlayer.Position.Y);
                        if (time.TotalTime.Milliseconds % 500 < 250) this.spPlayer.Texture = txLeft1;
                        else this.spPlayer.Texture = txLeft2;
                        B_isPressed = true;
                        I_rememberidle = 0;
                    }

                    if (Keyboard.IsKeyPressed(Keyboard.Key.S) && map.iswalkable((spPlayer), new Vector2f(F_runningSpeed, 0)))
                    {
                        spPlayer.Position = new Vector2f(spPlayer.Position.X + F_runningSpeed, spPlayer.Position.Y);
                        if (time.TotalTime.Milliseconds % 500 < 250) this.spPlayer.Texture = txRight1;
                        else this.spPlayer.Texture = txRight2;
                        B_isPressed = true;
                        I_rememberidle = 1;
                    }

                    if (Keyboard.IsKeyPressed(Keyboard.Key.A) && map.iswalkable((spPlayer), new Vector2f(0, -F_runningSpeed)))
                    {
                        spPlayer.Position = new Vector2f(spPlayer.Position.X, spPlayer.Position.Y - F_runningSpeed);
                        if (time.TotalTime.Milliseconds % 500 < 250) this.spPlayer.Texture = txUp1;
                        else this.spPlayer.Texture = txUp2;
                        B_isPressed = true;
                        I_rememberidle = 2;
                    }

                    if (Keyboard.IsKeyPressed(Keyboard.Key.D) && map.iswalkable((spPlayer), new Vector2f(0, F_runningSpeed)))
                    {
                        spPlayer.Position = new Vector2f(spPlayer.Position.X, spPlayer.Position.Y + F_runningSpeed);
                        if (time.TotalTime.Milliseconds % 500 < 250) this.spPlayer.Texture = txDown1;
                        else this.spPlayer.Texture = txDown2;
                        B_isPressed = true;
                        I_rememberidle = 3;
                    }

                    if (B_isPressed == false)
                    {
                        switch (I_rememberidle)
                        {
                            case 0: this.spPlayer.Texture = txLeft3;
                                break;
                            case 1: this.spPlayer.Texture = txRight3;
                                break;
                            case 2: this.spPlayer.Texture = txUp3;
                                break;
                            case 3: this.spPlayer.Texture = txDown3;
                                break;
                            default: this.spPlayer.Texture = txDown3;
                                break;
                        }
                    }
                    break;




                case 21:

                    if (Keyboard.IsKeyPressed(Keyboard.Key.W) && map.iswalkable((spPlayer), new Vector2f(-F_runningSpeed, 0)))
                    {
                        spPlayer.Position = new Vector2f(spPlayer.Position.X - F_runningSpeed, spPlayer.Position.Y);
                        if (time.TotalTime.Milliseconds % 500 < 250) this.spPlayer.Texture = txLeft1;
                        else this.spPlayer.Texture = txLeft2;
                        B_isPressed = true;
                        I_rememberidle = 0;
                    }

                    if (Keyboard.IsKeyPressed(Keyboard.Key.S) && map.iswalkable((spPlayer), new Vector2f(F_runningSpeed, 0)))
                    {
                        spPlayer.Position = new Vector2f(spPlayer.Position.X + F_runningSpeed, spPlayer.Position.Y);
                        if (time.TotalTime.Milliseconds % 500 < 250) this.spPlayer.Texture = txRight1;
                        else this.spPlayer.Texture = txRight2;
                        B_isPressed = true;
                        I_rememberidle = 1;
                    }

                    if (Keyboard.IsKeyPressed(Keyboard.Key.D) && map.iswalkable((spPlayer), new Vector2f(0, -F_runningSpeed)))
                    {
                        spPlayer.Position = new Vector2f(spPlayer.Position.X, spPlayer.Position.Y - F_runningSpeed);
                        if (time.TotalTime.Milliseconds % 500 < 250) this.spPlayer.Texture = txUp1;
                        else this.spPlayer.Texture = txUp2;
                        B_isPressed = true;
                        I_rememberidle = 2;
                    }

                    if (Keyboard.IsKeyPressed(Keyboard.Key.A) && map.iswalkable((spPlayer), new Vector2f(0, F_runningSpeed)))
                    {
                        spPlayer.Position = new Vector2f(spPlayer.Position.X, spPlayer.Position.Y + F_runningSpeed);
                        if (time.TotalTime.Milliseconds % 500 < 250) this.spPlayer.Texture = txDown1;
                        else this.spPlayer.Texture = txDown2;
                        B_isPressed = true;
                        I_rememberidle = 3;
                    }

                    if (B_isPressed == false)
                    {
                        switch (I_rememberidle)
                        {
                            case 0: this.spPlayer.Texture = txLeft3;
                                break;
                            case 1: this.spPlayer.Texture = txRight3;
                                break;
                            case 2: this.spPlayer.Texture = txUp3;
                                break;
                            case 3: this.spPlayer.Texture = txDown3;
                                break;
                            default: this.spPlayer.Texture = txDown3;
                                break;
                        }
                    }
                    break;





                case 22:


                    if (Keyboard.IsKeyPressed(Keyboard.Key.W) && map.iswalkable((spPlayer), new Vector2f(-F_runningSpeed, 0)))
                    {
                        spPlayer.Position = new Vector2f(spPlayer.Position.X - F_runningSpeed, spPlayer.Position.Y);
                        if (time.TotalTime.Milliseconds % 500 < 250) this.spPlayer.Texture = txLeft1;
                        else this.spPlayer.Texture = txLeft2;
                        B_isPressed = true;
                        I_rememberidle = 0;
                    }

                    if (Keyboard.IsKeyPressed(Keyboard.Key.D) && map.iswalkable((spPlayer), new Vector2f(F_runningSpeed, 0)))
                    {
                        spPlayer.Position = new Vector2f(spPlayer.Position.X + F_runningSpeed, spPlayer.Position.Y);
                        if (time.TotalTime.Milliseconds % 500 < 250) this.spPlayer.Texture = txRight1;
                        else this.spPlayer.Texture = txRight2;
                        B_isPressed = true;
                        I_rememberidle = 1;
                    }

                    if (Keyboard.IsKeyPressed(Keyboard.Key.A) && map.iswalkable((spPlayer), new Vector2f(0, -F_runningSpeed)))
                    {
                        spPlayer.Position = new Vector2f(spPlayer.Position.X, spPlayer.Position.Y - F_runningSpeed);
                        if (time.TotalTime.Milliseconds % 500 < 250) this.spPlayer.Texture = txUp1;
                        else this.spPlayer.Texture = txUp2;
                        B_isPressed = true;
                        I_rememberidle = 2;
                    }

                    if (Keyboard.IsKeyPressed(Keyboard.Key.S) && map.iswalkable((spPlayer), new Vector2f(0, F_runningSpeed)))
                    {
                        spPlayer.Position = new Vector2f(spPlayer.Position.X, spPlayer.Position.Y + F_runningSpeed);
                        if (time.TotalTime.Milliseconds % 500 < 250) this.spPlayer.Texture = txDown1;
                        else this.spPlayer.Texture = txDown2;
                        B_isPressed = true;
                        I_rememberidle = 3;
                    }

                    if (B_isPressed == false)
                    {
                        switch (I_rememberidle)
                        {
                            case 0: this.spPlayer.Texture = txLeft3;
                                break;
                            case 1: this.spPlayer.Texture = txRight3;
                                break;
                            case 2: this.spPlayer.Texture = txUp3;
                                break;
                            case 3: this.spPlayer.Texture = txDown3;
                                break;
                            default: this.spPlayer.Texture = txDown3;
                                break;
                        }
                    }
                    break;




                case 23:

                    if (Keyboard.IsKeyPressed(Keyboard.Key.W) && map.iswalkable((spPlayer), new Vector2f(-F_runningSpeed, 0)))
                    {
                        spPlayer.Position = new Vector2f(spPlayer.Position.X - F_runningSpeed, spPlayer.Position.Y);
                        if (time.TotalTime.Milliseconds % 500 < 250) this.spPlayer.Texture = txLeft1;
                        else this.spPlayer.Texture = txLeft2;
                        B_isPressed = true;
                        I_rememberidle = 0;
                    }

                    if (Keyboard.IsKeyPressed(Keyboard.Key.D) && map.iswalkable((spPlayer), new Vector2f(F_runningSpeed, 0)))
                    {
                        spPlayer.Position = new Vector2f(spPlayer.Position.X + F_runningSpeed, spPlayer.Position.Y);
                        if (time.TotalTime.Milliseconds % 500 < 250) this.spPlayer.Texture = txRight1;
                        else this.spPlayer.Texture = txRight2;
                        B_isPressed = true;
                        I_rememberidle = 1;
                    }

                    if (Keyboard.IsKeyPressed(Keyboard.Key.S) && map.iswalkable((spPlayer), new Vector2f(0, -F_runningSpeed)))
                    {
                        spPlayer.Position = new Vector2f(spPlayer.Position.X, spPlayer.Position.Y - F_runningSpeed);
                        if (time.TotalTime.Milliseconds % 500 < 250) this.spPlayer.Texture = txUp1;
                        else this.spPlayer.Texture = txUp2;
                        B_isPressed = true;
                        I_rememberidle = 2;
                    }

                    if (Keyboard.IsKeyPressed(Keyboard.Key.A) && map.iswalkable((spPlayer), new Vector2f(0, F_runningSpeed)))
                    {
                        spPlayer.Position = new Vector2f(spPlayer.Position.X, spPlayer.Position.Y + F_runningSpeed);
                        if (time.TotalTime.Milliseconds % 500 < 250) this.spPlayer.Texture = txDown1;
                        else this.spPlayer.Texture = txDown2;
                        B_isPressed = true;
                        I_rememberidle = 3;
                    }

                    if (B_isPressed == false)
                    {
                        switch (I_rememberidle)
                        {
                            case 0: this.spPlayer.Texture = txLeft3;
                                break;
                            case 1: this.spPlayer.Texture = txRight3;
                                break;
                            case 2: this.spPlayer.Texture = txUp3;
                                break;
                            case 3: this.spPlayer.Texture = txDown3;
                                break;
                            default: this.spPlayer.Texture = txDown3;
                                break;
                        }
                    }
                    break;

            }


        }
    }
}
