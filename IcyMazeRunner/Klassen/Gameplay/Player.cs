using IcyMazeRunner.Klassen;
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
        public Sprite spPlayer { get; private set; }

        int I_healthPoints;
        int I_maxHealth;
        float F_runningSpeed;
        // int I_basicDamage
        // int I_basicDefense


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

        /* ~~~~ Attribute für Bewegungsanimation und -steuerung ~~~~ */
        /// <summary>
        /// Boolean, ob Spieler stehen bleibt.
        /// </summary>
        bool B_isPressed = false;

        /// <summary>
        /// Boolean für Aktivierung des Hardmodes, sodass Tastendruck HM nicht sofort wieder deaktiviert/aktiviert.
        /// </summary>
        Boolean B_HMisPressed;

        /// <summary>
        /// Gibt die Textur an, welche genutzt wird, wenn Spieler stehen bleibt.
        /// </summary>
        int I_rememberidle = 0;

        /// <summary>
        /// Normalisierter Richtungsvektor nach oben.
        /// </summary>
        Vector2f up = new Vector2f(0, -1);

        /// <summary>
        /// Normalisierter Richtungsvektor nach unten.
        /// </summary>
        Vector2f down = new Vector2f(0, 1);

        /// <summary>
        /// Normalisierter Richtungsvektor nach links.
        /// </summary>
        Vector2f left = new Vector2f(-1, 0);

        /// <summary>
        /// Normalisierter Richtungsvektor nach rechts.
        /// </summary>
        Vector2f right = new Vector2f(1, 0);

        /// <summary>
        /// Vektorarray für die 4 Richtungen, in die sich der Spieler bewegen kann.
        /// </summary>
        Vector2f[] direction = new Vector2f[4];

        /// <summary>
        /// Speichert den letzten Richtungsvektor der Spielerposition ab.
        /// </summary>
        Vector2f latest_Movement;

        Boolean B_isJumping { get; set; }

        int I_Jumpdirection { get; set; }

        Vector2f Jumpstartposition { get; set; }


        /* ~~~~ Attribute für den Herausfoderungsmodus ~~~~ */
        /// <summary>
        /// Gibt an, ob Herausforderungsmodus aktiviert ist.
        /// </summary>
        public bool B_isControlChanged { get; private set; }

        /// <summary>
        /// Gibt an, ob Standardzuordnung der Tasten bereits wiederhergestellt wurde.
        /// </summary>
        public bool B_isControlNormalized { get; private set; }

        public Boolean B_HardmodeActivated { get; private set; }


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

        /* ~~~~ Attribute für den SkillController ~~~~ */

        SkillController SkCtrl;
        public float Speedbonus { get; set; }

        public Boolean B_WayIsVisible { get; set; }

        public Boolean B_IsInvincible { get; set; }


        /* ~~~~ Konstruktor ~~~~ */
        public Player(Vector2f Pos, Map map, View view)
        {
            //Festlegen der Spielerposition, wird mit neuem Level neu festgelegt
            

            //Initialisieren
            SH = new PlayerConditionHandler();

            // Initialisierung der HP des Spielers, wird mit neuem Level neu festgelegt
            I_healthPoints = 100;
            I_maxHealth = I_healthPoints;

            if (!Game.B_isLoadedGame)
            {
                // Verteidigung = levelabhängige Verteidigung + Game.I_BonusDefense; (I_basicDamage)
                // Angriff = levelabhängiger Angriff + Game.I_BonusAttack;  (I_basicDefense)
            }
            else
            {
                // ToDo: Aus Speicherdatei auslesen.
            }


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
            //spPlayer.Position = playerPosition;

            spPlayer.Position = Pos;

            mAbsmap = map;

            normalizeMovement();
            latest_Movement = new Vector2f(0, 0);

            // Booleans für Herausforderungsmodus initialisieren
            B_HMisPressed = false;
            B_isControlChanged = false;
            B_isControlNormalized = true;
            B_HardmodeActivated = false;

            // Attribute und Objekte für Todesanimation initialisieren
            gtDeathWatch = new GameTime();
            B_isDeathWatchOn = false;
            B_IsSaved = false;

            // Skillcontroller initialisieren
            SkCtrl = new SkillController(view);
            Speedbonus = 1f;
            B_WayIsVisible = false;
            B_IsInvincible = false;
        }


        /* ~~~~~~~~~~~~~~~~~~ Getter und Setter ~~~~~~~~~~~~~~*/

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

        public int getPlayerMaxHealth()
        {
            return I_maxHealth;
        }

        public void setPlayerMaxHealth(int mhp)
        {
            I_maxHealth = mhp;
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
        /// <param name="enemydamage">Wenn true, stammt der Schaden von einem Feind. Wenn false, stammt der Schaden von einer Falle
        ///  oder einer Condition.</param>
        public void setDamage(int _Damage, Boolean enemydamage)
        {

            if (!B_IsInvincible)
            {
                if (!enemydamage)
                {
                    I_healthPoints = I_healthPoints - _Damage;
                    InGame.I_typeOfDeath = 2;
                }
                else
                {
                    if (!SkCtrl.B_isBlocking)
                    {
                        I_healthPoints = I_healthPoints - _Damage;
                        InGame.I_typeOfDeath = 2;
                    }
                }
            }

            if (I_healthPoints < 0)
            {
                I_healthPoints = 0;
            }
        }

        /// <summary>
        /// Heilt den Spieler.
        /// </summary>
        /// <param name="_Heal">Gibt an, wieviel Heilung in absoluten Werten der Spieler erhält. </param>
        public void setHeal(int _Heal)
        {
            I_healthPoints = I_healthPoints + _Heal;

            if (I_healthPoints > I_maxHealth)
            {
                I_healthPoints = I_maxHealth;
            }
        }

        /// <summary>
        /// Heilt den Spieler.
        /// </summary>
        /// <param name="_Heal">Gibt an, wieviel Heilung in Prozent der Spieler erhält. </param>
        public void setHeal(float _Heal)
        {
            I_healthPoints = I_healthPoints + (int)(_Heal*(float)I_maxHealth);

            if (I_healthPoints > I_maxHealth)
            {
                I_healthPoints = I_maxHealth;
            }
        }

        /// <summary>
        /// Heilt den Spieler komplett.
        /// </summary>
        public void setHeal()
        {
                I_healthPoints = I_maxHealth;
        }
        
        /// <summary>
        ///  Stellt Standardzuordnung zwischen Tasten und Richtung wieder her.
        /// </summary>
        void normalizeMovement()
        {
            direction[(int)Emovestates.up] = up;
            direction[(int)Emovestates.down] = down;
            direction[(int)Emovestates.left] = left;
            direction[(int)Emovestates.right] = right;
            B_isControlNormalized = true;
        }

        /// <summary>
        /// StateHandler aktualisiert sich und verwaltet die Liste. Anschließend wird die move()-Methode aufgerufen.
        /// </summary>
        public void update(GameTime time, MoveableWallHandler MWH)
        {
            SH.update();
            move(this.mAbsmap, time, MWH);
            SkCtrl.update(this);
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
            F_runningSpeed = 0.5f * time.ElapsedTime.Milliseconds*Speedbonus;
            B_isPressed = false;

            HardmodeUpdate(map, time);

            jumptrigger(map, MWH);

            if (B_isJumping)
            {
                jump();
            }

            if (!B_isJumping && !SkCtrl.B_isUsingSkill)
            {
                move_(map, MWH, time);
            }
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
            if (typeOfDeath != 1 && InGame.B_LastChanceAvailable)
            {
                InGame.B_DeathAnimationIsPaused = true;
                InGame.B_LastChanceAvailable = false;
                Game.gameTime.WatchList[0].Start();
            }

            else
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
                            /* Fallanimation */
                            fallAnimation();


                            break;
                        }

                    case 2:
                        {
                            /* Tod durch Feind, Falle oder Zustände */
                            normalDeathAnimation();
                            break;
                        }

                    default:
                        {
                            /* Nichts */
                            break;
                        }
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

        /// <summary>
        /// Kontrolliert, ob Hardmode aktiviert oder deaktiviert (wurde), und löst aus, welche weiteren Schritte jeweils nötig sind.
        /// </summary>
        void HardmodeUpdate(Map map, GameTime time)
        {
            if (!Keyboard.IsKeyPressed(Keyboard.Key.F1))
                B_HMisPressed = false;


            if (!B_HardmodeActivated && !B_HMisPressed && Keyboard.IsKeyPressed(Keyboard.Key.F1))
            {
                Changingmove(map, time);
                B_isControlChanged = true;
                B_HMisPressed = true;
                gtMoveTime = new GameTime();
                gtMoveTime.Watch.Start();
            }
            if (B_HardmodeActivated && !B_HMisPressed && Keyboard.IsKeyPressed(Keyboard.Key.F1))
            {
                B_isControlChanged = false;
                B_HMisPressed = true;
                gtMoveTime = null;
            }

            if (B_isControlChanged)
            {
                if (gtMoveTime.Watch.ElapsedMilliseconds >= 20000)
                {
                    gtMoveTime.Watch.Restart();
                    Changingmove(map, time);
                }
            }
            else
            {
                if (!B_isControlNormalized)
                {
                    normalizeMovement();
                }
            }
        }

        /// <summary>
        /// <para>Bewegungssteuerung für den Herausforderungsmodus. </para>
        /// 
        /// <para>Alle 20 Sekunden (nach Ablauf der 20 Sekunden startet die Stoppuhr von vorn) wird mithilfe eines Random-Wertes die Zuordnung der Tastatur für die Bewegungsrichtungen neu zugeordnet, wobei alle 24 Fälle abgedeckt werden. </para>
        /// </summary>
        void Changingmove(Map map, GameTime time)
        {
            List<Vector2f> directionlist = new List<Vector2f> { up, down, left, right };

            for (int i = 0; i < direction.Length; ++i)
            {
                int index = Game.random.Next(directionlist.Count);
                direction[i] = directionlist[index];
                directionlist.RemoveAt(index);
            }

            B_isControlNormalized = false;
        }


        // ToDo: Texturen einfügen
        /// <summary>
        /// Testet, ob benötigte Tasten gedrückt wurden und am Sprungziel keine Mauer ist und kein Skill in diesem Moment aktiv ist.
        /// Wenn alles in Ordnung ist, löst er den Sprung aus und speichert die Richtung ab.
        /// </summary>
        void jumptrigger(Map map, MoveableWallHandler MWH)
        {
            if (Keyboard.IsKeyPressed(Keyboard.Key.A) && Keyboard.IsKeyPressed(Keyboard.Key.Space) && map.iswalkable((spPlayer), direction[(int)Emovestates.left] * 200) && MWH.isWalkable(this.spPlayer, direction[(int)Emovestates.left] * 200) && !SkCtrl.B_isUsingSkill)
            {
                I_Jumpdirection = (int) jumpdirection.left;
                B_isJumping = true;
                Jumpstartposition = spPlayer.Position;
                spPlayer.Texture = new Texture("");
            }

            if (Keyboard.IsKeyPressed(Keyboard.Key.A) && Keyboard.IsKeyPressed(Keyboard.Key.Space) && map.iswalkable((spPlayer), direction[(int)Emovestates.left] * 200) && MWH.isWalkable(this.spPlayer, direction[(int)Emovestates.left] * 200) && !SkCtrl.B_isUsingSkill)
            {
                I_Jumpdirection = (int)jumpdirection.right;
                B_isJumping = true;
                Jumpstartposition = spPlayer.Position;
                spPlayer.Texture = new Texture("");
            }

            if (Keyboard.IsKeyPressed(Keyboard.Key.A) && Keyboard.IsKeyPressed(Keyboard.Key.Space) && map.iswalkable((spPlayer), direction[(int)Emovestates.left] * 200) && MWH.isWalkable(this.spPlayer, direction[(int)Emovestates.left] * 200) && !SkCtrl.B_isUsingSkill)
            {
                I_Jumpdirection = (int)jumpdirection.up;
                B_isJumping = true;
                Jumpstartposition = spPlayer.Position;
                spPlayer.Texture = new Texture("");
            }

            if (Keyboard.IsKeyPressed(Keyboard.Key.A) && Keyboard.IsKeyPressed(Keyboard.Key.Space) && map.iswalkable((spPlayer), direction[(int)Emovestates.left] * 200) && MWH.isWalkable(this.spPlayer, direction[(int)Emovestates.left] * 200) && !SkCtrl.B_isUsingSkill)
            {
                I_Jumpdirection = (int)jumpdirection.down;
                B_isJumping = true;
                Jumpstartposition = spPlayer.Position;
                spPlayer.Texture = new Texture("");
            }
        }

        void jump()
        {
            // Aktualisierung der Texturposition
            // 8 Pixel nach links/rechts, 3 Pixel nach oben/unten im Durchschnitt, eventuell später Flugkurve genauer anpassen (Formel)
            // Sprung nach unten: erste Hälfte Y ändert sich nur um 5, 2. Hälfte ändert sich Y um 11, Sprung nach oben umgekehrt
            if (Calculator.getDistance(spPlayer.Position, Jumpstartposition) < 107)
            {
                switch (I_Jumpdirection)
                {
                    case (int)jumpdirection.left:
                        {
                            
                            break;
                        }

                    case (int)jumpdirection.right:
                        {
                            break;
                        }

                    case (int)jumpdirection.up:
                        {
                            break;
                        }

                    case (int)jumpdirection.down:
                        {
                            break;
                        }

                    default: { break; }
                }
            }


            else
            {

                switch (I_Jumpdirection)
                {
                    case (int)jumpdirection.left:
                        {
                            break;
                        }

                    case (int)jumpdirection.right:
                        {
                            break;
                        }

                    case (int)jumpdirection.up:
                        {
                            break;
                        }

                    case (int)jumpdirection.down:
                        {
                            break;
                        }

                    default: { break; }
                }

            }

            // ToDo: nur Sprite.Position nutzen
            // ToDo: GapCollision-Abfrage um !B_isJumping erweitern
            // Aktualisierung der Sprite-Position
            switch (I_Jumpdirection)
            {
                case (int) jumpdirection.left:
                    {
                        spPlayer.Position += direction[(int)Emovestates.left] * F_runningSpeed;
                        break;
                    }

                case (int)jumpdirection.right:
                    {
                        spPlayer.Position += direction[(int)Emovestates.right] * F_runningSpeed;
                        break;
                    }

                case (int)jumpdirection.up:
                    {
                        spPlayer.Position += direction[(int)Emovestates.up] * F_runningSpeed;
                        break;
                    }

                case (int)jumpdirection.down:
                    {
                        spPlayer.Position += direction[(int)Emovestates.down] * F_runningSpeed;
                        break;
                    }

                default: { break; }
            }

            // ToDo: Beenden des Sprungs --> B_isJumping = false; wenn getDistance >=200 ist und Positionen so anpassen, dass sich nur X
            // oder nur Y um 200 geändert hat
        }

        /// <summary>
        /// Die eigentliche move-Methode
        /// </summary>
        void move_(Map map, MoveableWallHandler MWH, GameTime time)
        {
            if (Keyboard.IsKeyPressed(Keyboard.Key.A) && map.iswalkable((spPlayer), direction[(int)Emovestates.left] * F_runningSpeed) && MWH.isWalkable(this.spPlayer, direction[(int)Emovestates.left] * F_runningSpeed))
                {
                    spPlayer.Position += direction[(int)Emovestates.left] * F_runningSpeed;
                    latest_Movement = direction[(int)Emovestates.left];
                    B_isPressed = true;
                }

                if (Keyboard.IsKeyPressed(Keyboard.Key.D) && map.iswalkable((spPlayer), direction[(int)Emovestates.right] * F_runningSpeed) && MWH.isWalkable(this.spPlayer, direction[(int)Emovestates.right] * F_runningSpeed))
                {
                    spPlayer.Position += direction[(int)Emovestates.right] * F_runningSpeed;
                    latest_Movement = direction[(int)Emovestates.right];
                    B_isPressed = true;
                }

                if (Keyboard.IsKeyPressed(Keyboard.Key.W) && map.iswalkable((spPlayer), direction[(int)Emovestates.up] * F_runningSpeed) && MWH.isWalkable(this.spPlayer, direction[(int)Emovestates.up] * F_runningSpeed))
                {
                    spPlayer.Position += direction[(int)Emovestates.up] * F_runningSpeed;
                    latest_Movement = direction[(int)Emovestates.up];
                    B_isPressed = true;
                }

                if (Keyboard.IsKeyPressed(Keyboard.Key.S) && map.iswalkable((spPlayer), direction[(int)Emovestates.down] * F_runningSpeed) && MWH.isWalkable(this.spPlayer, direction[(int)Emovestates.down] * F_runningSpeed))
                {
                    spPlayer.Position += direction[(int)Emovestates.down] * F_runningSpeed;
                    latest_Movement = direction[(int)Emovestates.down];
                    B_isPressed = true;
                }


                move_Animation(latest_Movement, time);
        }

        /// <summary>
        /// Steuert die Animation des Spielers in der Bewegung mithilfe des mitgegebenen Vektors.
        /// </summary>
        /// <param name="movedirection">Vektor, der eine Kopie des aktuellen Richtungsvektors der Spielerposition ist.</param>
        void move_Animation(Vector2f movedirection, GameTime time)
        {
            if (movedirection.X > 0)
            {
                if (time.TotalTime.Milliseconds % 500 < 250) this.spPlayer.Texture = txRight1;
                else this.spPlayer.Texture = txRight2;
                I_rememberidle = 1;
            }
            if (movedirection.X < 0)
            {
                if (time.TotalTime.Milliseconds % 500 < 250) this.spPlayer.Texture = txLeft1;
                else this.spPlayer.Texture = txLeft2;
                I_rememberidle = 0;
            }
            if (movedirection.Y > 0)
            {
                if (time.TotalTime.Milliseconds % 500 < 250) this.spPlayer.Texture = txDown1;
                else this.spPlayer.Texture = txDown2;
                I_rememberidle = 3;
            }
            if (movedirection.Y < 0)
            {
                if (time.TotalTime.Milliseconds % 500 < 250) this.spPlayer.Texture = txUp1;
                else this.spPlayer.Texture = txUp2;
                I_rememberidle = 2;
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

        /// <summary>
        /// Animiert das Fallen, wenn Spieler durch Loch im Boden stirbt.
        /// </summary>
        void fallAnimation()
        {
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
        }

        /// <summary>
        /// Animiert den normalen Tod des Spielers durch Schaden.
        /// </summary>
        void normalDeathAnimation()
        {
            // ToDo: Animation
        }

        /// <summary>
        /// Enum zum Speichern der Sprungrichtung, um die Aktualisierung der Sprite.Position zu ermöglichen.
        /// </summary>
        enum jumpdirection
        {
            none = -1,
            left,
            right,
            up,
            down,
            jumpdirectionCount
        }
    }
}
