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


        /* ~~~~ Map-Kopie, da Update() die Map nicht mitgegeben bekommt ~~~~ */
        Map mAbsmap;


        /* ~~~~ Timeobject, um Zeit für den Herausforderungsmodus zu messen ~~~~ */
        GameTime gtMoveTime;

        /* ~~~~ Wichtige Playerattribute ~~~~ */
        Sprite spPlayer {get;set;}
        Vector2f playerPosition;


        // benötigt?
        EGameStates gamestate;

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

        /* ~~~~ Attribute für Animation, wenn Spieler stehen bleibt ~~~~ */
        bool isPressed = false;
        int rememberidle = 0;


        /* ~~~~ Attribute für den Herausfoderungsmodus ~~~~ */
        bool is_controlchange = false;
        int movechangerstate = 0;
        Random random = new Random();


        /* ~~~~ Attribute und Zeitobjekt für die Animation, wenn Spieler stirbt ~~~~ */
        public GameTime gtDeathWatch;
        bool DeathWatchIsOn;


        /* ~~~~ Attribute für Fallanimation ~~~~ */
        Texture txFall1a;
        Texture txFall1b;
        Texture txFall2a;
        Texture txFall2b;
        Texture txFall3a;
        Texture txFall3b;
        Texture txFall4a;
        Texture txFall4b;
        int fallAnimationCounter = 0;

        /************ Player attributes ***********/

        int healthPoints;
        float runningSpeed;
        // int basicDamage
        // int basicDefence
        

        /* ~~~~ Konstruktor ~~~~ */
        public Player(Vector2f Pos, Map map) 
        {
            //Festlegen der Spielerposition, wird mit neuem Level neu festgelegt
            playerPosition = Pos;


            // Initialisierung der HP des Spielers, wird mit neuem Level neu festgelegt
            healthPoints = 100;
            

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
           
           //Standardtextur wird festgelegt
            spPlayer = new Sprite(txDown3);


            //Sprite wird geladen
            spPlayer.Scale = new Vector2f(1f, 1f); 
            spPlayer.Position = playerPosition;
            spPlayer = new Sprite(spPlayer);


            //Kopie der Map für move-Methode weiter unten
            mAbsmap = map;
                   

            // Stoppuhr für Herausforderungsmodus starten
            gtMoveTime = new GameTime();
            gtMoveTime.Watch.Start();

            // Attribute und Objekte für Todesanimation initialisieren
            gtDeathWatch = new GameTime();
            DeathWatchIsOn = false;
        }


        /* ~~~~~~~~~~~~~~~~~~ Getter und Setter ~~~~~~~~~~~~~~*/


        //unnötig?
        public Sprite getplayerSprite()
        {
            return this.spPlayer;
        }


        //unnötig?
        public void setSpritePos(Vector2f pos)
        {
            spPlayer.Position = pos;
        }

        public bool getIsPressed()
        {
            return isPressed;
        }

        public void setIsPressed(bool value)
        {
            isPressed = value;
        }

        public float getXPosition()
        {
            return spPlayer.Position.X;
        }

        public float getYPosition()
        {
            return spPlayer.Position.Y;
        }

        public float getWidth()
        {
            return spPlayer.Texture.Size.X;
        }

        public float getHeigth()
        {
            return spPlayer.Texture.Size.Y;
        }

        public int getPlayerHealth()
        {
            return healthPoints;
        }

        public void setPlayerHealth(int data)
        {
            healthPoints = data;
        }

        public void setDeathWatchIsOn(bool value)
        {
            DeathWatchIsOn = value;
        }


        /*~~~~ Update ~~~~*/

        public void update(GameTime time)
        {

            move(this.mAbsmap, time);
        }


        /*~~~~ Methode für Bewegungsteuerung des Spielers ~~~~*/
        public void move(Map map, GameTime time)
        {
            /*~~~~ Anpassen der Spielergeschwindigkeit an den Computer ~~~~*/
            runningSpeed = 0.5f * time.ElapsedTime.Milliseconds;

            /*~~~~ Bool auf Standard setzen ~~~~*/
            isPressed = false;


            /*~~~~ Aktivieren und Deaktivieren des Herausforderungsmodus ~~~~*/
            if (Keyboard.IsKeyPressed(Keyboard.Key.F2))
            is_controlchange = true;

            if (Keyboard.IsKeyPressed(Keyboard.Key.F1))
            is_controlchange = false;


            /*~~~~ Wenn Herausforderungsmodus aktiviert, wechsel in Methode zur Bewegungssteuerung im Herausforderungsmodus ~~~~*/
            if (is_controlchange)
            changingmove(map, time);


            /*~~~~ Ansonsten normale Bewegungssteuerung ~~~~*/
            else
            {


            if (Keyboard.IsKeyPressed(Keyboard.Key.A) && map.iswalkable((spPlayer), new Vector2f(-runningSpeed, 0)))
            {
                playerPosition = new Vector2f(playerPosition.X - runningSpeed, playerPosition.Y);
                if (time.TotalTime.Milliseconds % 500 < 250) this.spPlayer.Texture = txLeft1;
                else this.spPlayer.Texture = txLeft2;
                isPressed = true;
                rememberidle = 0;
            }

            if (Keyboard.IsKeyPressed(Keyboard.Key.D) && map.iswalkable((spPlayer), new Vector2f(runningSpeed, 0)))
            {
                playerPosition = new Vector2f(playerPosition.X + runningSpeed, playerPosition.Y);
                if (time.TotalTime.Milliseconds % 500 < 250) this.spPlayer.Texture = txRight1;
                else this.spPlayer.Texture = txRight2;
                isPressed = true;
                rememberidle = 1;
            }

            if (Keyboard.IsKeyPressed(Keyboard.Key.W) && map.iswalkable((spPlayer), new Vector2f(0, -runningSpeed)))
            {
                playerPosition = new Vector2f(playerPosition.X, playerPosition.Y - runningSpeed);
                if (time.TotalTime.Milliseconds % 500 < 250) this.spPlayer.Texture = txUp1;
                else this.spPlayer.Texture = txUp2;
                isPressed = true;
                rememberidle = 2;
            }

            if (Keyboard.IsKeyPressed(Keyboard.Key.S) && map.iswalkable((spPlayer), new Vector2f(0, runningSpeed)))
            {
                playerPosition = new Vector2f(playerPosition.X, playerPosition.Y + runningSpeed);
                if (time.TotalTime.Milliseconds % 500 < 250) this.spPlayer.Texture = txDown1;
                else this.spPlayer.Texture = txDown2;
                isPressed = true;
                rememberidle = 3;
            }

            /*~~~~ Wenn Spieler sich nicht bewegt, ist isPressed falsch, Textur anpassen ~~~~*/
            if (isPressed == false)
            {
                switch (rememberidle)
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

        /*~~~~ Spriteposition an Position des Objektes anpassen ~~~~*/
        spPlayer.Position = playerPosition;


        }

        /*~~~~ Animation des Todes ~~~~*/
        public void DeathAnimation(int typeOfDeath)
        {

            /*~~~~ Stoppuhr starten, wenn sie noch nicht gestartet ist ~~~~*/
            if (!DeathWatchIsOn)
            {
                setDeathWatchIsOn(true);
                gtDeathWatch.Watch.Start();
            }

            /*~~~~ Hilfsinteger zum Texturenwechsel ~~~~*/
            fallAnimationCounter++;


            /*~~~~ Auswahl der Animation abhängig von Todesursache ~~~~*/
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
                            if (fallAnimationCounter%2==0)
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
                            if (fallAnimationCounter % 2 == 0)
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
                            if (fallAnimationCounter % 2 == 0)
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
                            if (fallAnimationCounter % 2 == 0)
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


        /*~~~~ Draw ~~~~*/
        public void draw(RenderWindow win)
        {
            win.Draw(spPlayer); // Element anpassen
        }
        

        // LAAAAANNNNNGGGEEEE Methode für die 24 möglichen Zuweisungen der Tastatur im Hardmode bis zum Ende der Klasse
        public void changingmove(Map map, GameTime time) {


            /*~~~~ Neue Zuordnung der Tastatur an die Bewegungsrichtung alle 20 Sekunden ~~~~*/
            if (gtMoveTime.Watch.ElapsedMilliseconds >= 20000)
            {
                movechangerstate = random.Next(23);
                gtMoveTime.Watch.Restart();
            }


            /*~~~~ Alle möglichen Fälle für die Steuerung ~~~~*/
switch (movechangerstate) 
{
    case 0:

            if (Keyboard.IsKeyPressed(Keyboard.Key.A) && map.iswalkable((spPlayer), new Vector2f(-runningSpeed,0))) 
            {
                playerPosition = new Vector2f(playerPosition.X - runningSpeed, playerPosition.Y);
                if (time.TotalTime.Milliseconds % 500 < 250) this.spPlayer.Texture = txLeft1;
                else this.spPlayer.Texture = txLeft2;
                isPressed = true;
                rememberidle = 0;
            }

            if (Keyboard.IsKeyPressed(Keyboard.Key.D) && map.iswalkable((spPlayer), new Vector2f(runningSpeed, 0))) 
            {
                playerPosition = new Vector2f(playerPosition.X + runningSpeed, playerPosition.Y);
                if (time.TotalTime.Milliseconds % 500 < 250) this.spPlayer.Texture = txRight1;
                else this.spPlayer.Texture = txRight2;
                isPressed = true;
                rememberidle = 1;
            }

            if (Keyboard.IsKeyPressed(Keyboard.Key.W) && map.iswalkable((spPlayer), new Vector2f(0, -runningSpeed))) 
            {
                playerPosition = new Vector2f(playerPosition.X, playerPosition.Y - runningSpeed);
                if (time.TotalTime.Milliseconds % 500 < 250) this.spPlayer.Texture = txUp1;
                else this.spPlayer.Texture = txUp2;
                isPressed = true;
                rememberidle = 2;
            }

            if (Keyboard.IsKeyPressed(Keyboard.Key.S) && map.iswalkable((spPlayer), new Vector2f(0, runningSpeed))) 
            {
                playerPosition = new Vector2f(playerPosition.X, playerPosition.Y + runningSpeed);
                if (time.TotalTime.Milliseconds % 500 < 250) this.spPlayer.Texture = txDown1;
                else this.spPlayer.Texture = txDown2;
                isPressed = true;
                rememberidle = 3;
            }

            if (isPressed == false)
            {
                switch (rememberidle)
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

            if (Keyboard.IsKeyPressed(Keyboard.Key.A) && map.iswalkable((spPlayer), new Vector2f(-runningSpeed,0))) 
            {
                playerPosition = new Vector2f(playerPosition.X - runningSpeed, playerPosition.Y);
                if (time.TotalTime.Milliseconds % 500 < 250) this.spPlayer.Texture = txLeft1;
                else this.spPlayer.Texture = txLeft2;
                isPressed = true;
                rememberidle = 0;
            }

            if (Keyboard.IsKeyPressed(Keyboard.Key.D) && map.iswalkable((spPlayer), new Vector2f(runningSpeed, 0))) 
            {
                playerPosition = new Vector2f(playerPosition.X + runningSpeed, playerPosition.Y);
                if (time.TotalTime.Milliseconds % 500 < 250) this.spPlayer.Texture = txRight1;
                else this.spPlayer.Texture = txRight2;
                isPressed = true;
                rememberidle = 1;
            }

            if (Keyboard.IsKeyPressed(Keyboard.Key.S) && map.iswalkable((spPlayer), new Vector2f(0, -runningSpeed))) 
            {
                playerPosition = new Vector2f(playerPosition.X, playerPosition.Y - runningSpeed);
                if (time.TotalTime.Milliseconds % 500 < 250) this.spPlayer.Texture = txUp1;
                else this.spPlayer.Texture = txUp2;
                isPressed = true;
                rememberidle = 2;
            }

            if (Keyboard.IsKeyPressed(Keyboard.Key.W) && map.iswalkable((spPlayer), new Vector2f(0, runningSpeed))) 
            {
                playerPosition = new Vector2f(playerPosition.X, playerPosition.Y + runningSpeed);
                if (time.TotalTime.Milliseconds % 500 < 250) this.spPlayer.Texture = txDown1;
                else this.spPlayer.Texture = txDown2;
                isPressed = true;
                rememberidle = 3;
            }

            if (isPressed == false)
            {
                switch (rememberidle)
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

            if (Keyboard.IsKeyPressed(Keyboard.Key.A) && map.iswalkable((spPlayer), new Vector2f(-runningSpeed,0))) 
            {
                playerPosition = new Vector2f(playerPosition.X - runningSpeed, playerPosition.Y);
                if (time.TotalTime.Milliseconds % 500 < 250) this.spPlayer.Texture = txLeft1;
                else this.spPlayer.Texture = txLeft2;
                isPressed = true;
                rememberidle = 0;
            }

            if (Keyboard.IsKeyPressed(Keyboard.Key.S) && map.iswalkable((spPlayer), new Vector2f(runningSpeed, 0))) 
            {
                playerPosition = new Vector2f(playerPosition.X + runningSpeed, playerPosition.Y);
                if (time.TotalTime.Milliseconds % 500 < 250) this.spPlayer.Texture = txRight1;
                else this.spPlayer.Texture = txRight2;
                isPressed = true;
                rememberidle = 1;
            }

            if (Keyboard.IsKeyPressed(Keyboard.Key.W) && map.iswalkable((spPlayer), new Vector2f(0, -runningSpeed))) 
            {
                playerPosition = new Vector2f(playerPosition.X, playerPosition.Y - runningSpeed);
                if (time.TotalTime.Milliseconds % 500 < 250) this.spPlayer.Texture = txUp1;
                else this.spPlayer.Texture = txUp2;
                isPressed = true;
                rememberidle = 2;
            }

            if (Keyboard.IsKeyPressed(Keyboard.Key.D) && map.iswalkable((spPlayer), new Vector2f(0, runningSpeed))) 
            {
                playerPosition = new Vector2f(playerPosition.X, playerPosition.Y + runningSpeed);
                if (time.TotalTime.Milliseconds % 500 < 250) this.spPlayer.Texture = txDown1;
                else this.spPlayer.Texture = txDown2;
                isPressed = true;
                rememberidle = 3;
            }

            if (isPressed == false)
            {
                switch (rememberidle)
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



            if (Keyboard.IsKeyPressed(Keyboard.Key.A) && map.iswalkable((spPlayer), new Vector2f(-runningSpeed,0))) 
            {
                playerPosition = new Vector2f(playerPosition.X - runningSpeed, playerPosition.Y);
                if (time.TotalTime.Milliseconds % 500 < 250) this.spPlayer.Texture = txLeft1;
                else this.spPlayer.Texture = txLeft2;
                isPressed = true;
                rememberidle = 0;
            }

            if (Keyboard.IsKeyPressed(Keyboard.Key.S) && map.iswalkable((spPlayer), new Vector2f(runningSpeed, 0))) 
            {
                playerPosition = new Vector2f(playerPosition.X + runningSpeed, playerPosition.Y);
                if (time.TotalTime.Milliseconds % 500 < 250) this.spPlayer.Texture = txRight1;
                else this.spPlayer.Texture = txRight2;
                isPressed = true;
                rememberidle = 1;
            }

            if (Keyboard.IsKeyPressed(Keyboard.Key.D) && map.iswalkable((spPlayer), new Vector2f(0, -runningSpeed))) 
            {
                playerPosition = new Vector2f(playerPosition.X, playerPosition.Y - runningSpeed);
                if (time.TotalTime.Milliseconds % 500 < 250) this.spPlayer.Texture = txUp1;
                else this.spPlayer.Texture = txUp2;
                isPressed = true;
                rememberidle = 2;
            }

            if (Keyboard.IsKeyPressed(Keyboard.Key.W) && map.iswalkable((spPlayer), new Vector2f(0, runningSpeed))) 
            {
                playerPosition = new Vector2f(playerPosition.X, playerPosition.Y + runningSpeed);
                if (time.TotalTime.Milliseconds % 500 < 250) this.spPlayer.Texture = txDown1;
                else this.spPlayer.Texture = txDown2;
                isPressed = true;
                rememberidle = 3;
            }

            if (isPressed == false)
            {
                switch (rememberidle)
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


            if (Keyboard.IsKeyPressed(Keyboard.Key.A) && map.iswalkable((spPlayer), new Vector2f(-runningSpeed,0))) 
            {
                playerPosition = new Vector2f(playerPosition.X - runningSpeed, playerPosition.Y);
                if (time.TotalTime.Milliseconds % 500 < 250) this.spPlayer.Texture = txLeft1;
                else this.spPlayer.Texture = txLeft2;
                isPressed = true;
                rememberidle = 0;
            }

            if (Keyboard.IsKeyPressed(Keyboard.Key.W) && map.iswalkable((spPlayer), new Vector2f(runningSpeed, 0))) 
            {
                playerPosition = new Vector2f(playerPosition.X + runningSpeed, playerPosition.Y);
                if (time.TotalTime.Milliseconds % 500 < 250) this.spPlayer.Texture = txRight1;
                else this.spPlayer.Texture = txRight2;
                isPressed = true;
                rememberidle = 1;
            }

            if (Keyboard.IsKeyPressed(Keyboard.Key.S) && map.iswalkable((spPlayer), new Vector2f(0, -runningSpeed))) 
            {
                playerPosition = new Vector2f(playerPosition.X, playerPosition.Y - runningSpeed);
                if (time.TotalTime.Milliseconds % 500 < 250) this.spPlayer.Texture = txUp1;
                else this.spPlayer.Texture = txUp2;
                isPressed = true;
                rememberidle = 2;
            }

            if (Keyboard.IsKeyPressed(Keyboard.Key.D) && map.iswalkable((spPlayer), new Vector2f(0, runningSpeed))) 
            {
                playerPosition = new Vector2f(playerPosition.X, playerPosition.Y + runningSpeed);
                if (time.TotalTime.Milliseconds % 500 < 250) this.spPlayer.Texture = txDown1;
                else this.spPlayer.Texture = txDown2;
                isPressed = true;
                rememberidle = 3;
            }

            if (isPressed == false)
            {
                switch (rememberidle)
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
            if (Keyboard.IsKeyPressed(Keyboard.Key.A) && map.iswalkable((spPlayer), new Vector2f(-runningSpeed,0))) 
            {
                playerPosition = new Vector2f(playerPosition.X - runningSpeed, playerPosition.Y);
                if (time.TotalTime.Milliseconds % 500 < 250) this.spPlayer.Texture = txLeft1;
                else this.spPlayer.Texture = txLeft2;
                isPressed = true;
                rememberidle = 0;
            }

            if (Keyboard.IsKeyPressed(Keyboard.Key.W) && map.iswalkable((spPlayer), new Vector2f(runningSpeed, 0))) 
            {
                playerPosition = new Vector2f(playerPosition.X + runningSpeed, playerPosition.Y);
                if (time.TotalTime.Milliseconds % 500 < 250) this.spPlayer.Texture = txRight1;
                else this.spPlayer.Texture = txRight2;
                isPressed = true;
                rememberidle = 1;
            }

            if (Keyboard.IsKeyPressed(Keyboard.Key.D) && map.iswalkable((spPlayer), new Vector2f(0, -runningSpeed))) 
            {
                playerPosition = new Vector2f(playerPosition.X, playerPosition.Y - runningSpeed);
                if (time.TotalTime.Milliseconds % 500 < 250) this.spPlayer.Texture = txUp1;
                else this.spPlayer.Texture = txUp2;
                isPressed = true;
                rememberidle = 2;
            }

            if (Keyboard.IsKeyPressed(Keyboard.Key.S) && map.iswalkable((spPlayer), new Vector2f(0, runningSpeed))) 
            {
                playerPosition = new Vector2f(playerPosition.X, playerPosition.Y + runningSpeed);
                if (time.TotalTime.Milliseconds % 500 < 250) this.spPlayer.Texture = txDown1;
                else this.spPlayer.Texture = txDown2;
                isPressed = true;
                rememberidle = 3;
            }

            if (isPressed == false)
            {
                switch (rememberidle)
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
            if (Keyboard.IsKeyPressed(Keyboard.Key.D) && map.iswalkable((spPlayer), new Vector2f(-runningSpeed,0))) 
            {
                playerPosition = new Vector2f(playerPosition.X - runningSpeed, playerPosition.Y);
                if (time.TotalTime.Milliseconds % 500 < 250) this.spPlayer.Texture = txLeft1;
                else this.spPlayer.Texture = txLeft2;
                isPressed = true;
                rememberidle = 0;
            }

            if (Keyboard.IsKeyPressed(Keyboard.Key.A) && map.iswalkable((spPlayer), new Vector2f(runningSpeed, 0))) 
            {
                playerPosition = new Vector2f(playerPosition.X + runningSpeed, playerPosition.Y);
                if (time.TotalTime.Milliseconds % 500 < 250) this.spPlayer.Texture = txRight1;
                else this.spPlayer.Texture = txRight2;
                isPressed = true;
                rememberidle = 1;
            }

            if (Keyboard.IsKeyPressed(Keyboard.Key.W) && map.iswalkable((spPlayer), new Vector2f(0, -runningSpeed))) 
            {
                playerPosition = new Vector2f(playerPosition.X, playerPosition.Y - runningSpeed);
                if (time.TotalTime.Milliseconds % 500 < 250) this.spPlayer.Texture = txUp1;
                else this.spPlayer.Texture = txUp2;
                isPressed = true;
                rememberidle = 2;
            }

            if (Keyboard.IsKeyPressed(Keyboard.Key.S) && map.iswalkable((spPlayer), new Vector2f(0, runningSpeed))) 
            {
                playerPosition = new Vector2f(playerPosition.X, playerPosition.Y + runningSpeed);
                if (time.TotalTime.Milliseconds % 500 < 250) this.spPlayer.Texture = txDown1;
                else this.spPlayer.Texture = txDown2;
                isPressed = true;
                rememberidle = 3;
            }

            if (isPressed == false)
            {
                switch (rememberidle)
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
            if (Keyboard.IsKeyPressed(Keyboard.Key.D) && map.iswalkable((spPlayer), new Vector2f(-runningSpeed,0))) 
            {
                playerPosition = new Vector2f(playerPosition.X - runningSpeed, playerPosition.Y);
                if (time.TotalTime.Milliseconds % 500 < 250) this.spPlayer.Texture = txLeft1;
                else this.spPlayer.Texture = txLeft2;
                isPressed = true;
                rememberidle = 0;
            }

            if (Keyboard.IsKeyPressed(Keyboard.Key.A) && map.iswalkable((spPlayer), new Vector2f(runningSpeed, 0))) 
            {
                playerPosition = new Vector2f(playerPosition.X + runningSpeed, playerPosition.Y);
                if (time.TotalTime.Milliseconds % 500 < 250) this.spPlayer.Texture = txRight1;
                else this.spPlayer.Texture = txRight2;
                isPressed = true;
                rememberidle = 1;
            }

            if (Keyboard.IsKeyPressed(Keyboard.Key.S) && map.iswalkable((spPlayer), new Vector2f(0, -runningSpeed))) 
            {
                playerPosition = new Vector2f(playerPosition.X, playerPosition.Y - runningSpeed);
                if (time.TotalTime.Milliseconds % 500 < 250) this.spPlayer.Texture = txUp1;
                else this.spPlayer.Texture = txUp2;
                isPressed = true;
                rememberidle = 2;
            }

            if (Keyboard.IsKeyPressed(Keyboard.Key.W) && map.iswalkable((spPlayer), new Vector2f(0, runningSpeed))) 
            {
                playerPosition = new Vector2f(playerPosition.X, playerPosition.Y + runningSpeed);
                if (time.TotalTime.Milliseconds % 500 < 250) this.spPlayer.Texture = txDown1;
                else this.spPlayer.Texture = txDown2;
                isPressed = true;
                rememberidle = 3;
            }

            if (isPressed == false)
            {
                switch (rememberidle)
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

            if (Keyboard.IsKeyPressed(Keyboard.Key.D) && map.iswalkable((spPlayer), new Vector2f(-runningSpeed,0))) 
            {
                playerPosition = new Vector2f(playerPosition.X - runningSpeed, playerPosition.Y);
                if (time.TotalTime.Milliseconds % 500 < 250) this.spPlayer.Texture = txLeft1;
                else this.spPlayer.Texture = txLeft2;
                isPressed = true;
                rememberidle = 0;
            }

            if (Keyboard.IsKeyPressed(Keyboard.Key.S) && map.iswalkable((spPlayer), new Vector2f(runningSpeed, 0))) 
            {
                playerPosition = new Vector2f(playerPosition.X + runningSpeed, playerPosition.Y);
                if (time.TotalTime.Milliseconds % 500 < 250) this.spPlayer.Texture = txRight1;
                else this.spPlayer.Texture = txRight2;
                isPressed = true;
                rememberidle = 1;
            }

            if (Keyboard.IsKeyPressed(Keyboard.Key.A) && map.iswalkable((spPlayer), new Vector2f(0, -runningSpeed))) 
            {
                playerPosition = new Vector2f(playerPosition.X, playerPosition.Y - runningSpeed);
                if (time.TotalTime.Milliseconds % 500 < 250) this.spPlayer.Texture = txUp1;
                else this.spPlayer.Texture = txUp2;
                isPressed = true;
                rememberidle = 2;
            }

            if (Keyboard.IsKeyPressed(Keyboard.Key.W) && map.iswalkable((spPlayer), new Vector2f(0, runningSpeed))) 
            {
                playerPosition = new Vector2f(playerPosition.X, playerPosition.Y + runningSpeed);
                if (time.TotalTime.Milliseconds % 500 < 250) this.spPlayer.Texture = txDown1;
                else this.spPlayer.Texture = txDown2;
                isPressed = true;
                rememberidle = 3;
            }

            if (isPressed == false)
            {
                switch (rememberidle)
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

            if (Keyboard.IsKeyPressed(Keyboard.Key.D) && map.iswalkable((spPlayer), new Vector2f(-runningSpeed,0))) 
            {
                playerPosition = new Vector2f(playerPosition.X - runningSpeed, playerPosition.Y);
                if (time.TotalTime.Milliseconds % 500 < 250) this.spPlayer.Texture = txLeft1;
                else this.spPlayer.Texture = txLeft2;
                isPressed = true;
                rememberidle = 0;
            }

            if (Keyboard.IsKeyPressed(Keyboard.Key.S) && map.iswalkable((spPlayer), new Vector2f(runningSpeed, 0))) 
            {
                playerPosition = new Vector2f(playerPosition.X + runningSpeed, playerPosition.Y);
                if (time.TotalTime.Milliseconds % 500 < 250) this.spPlayer.Texture = txRight1;
                else this.spPlayer.Texture = txRight2;
                isPressed = true;
                rememberidle = 1;
            }

            if (Keyboard.IsKeyPressed(Keyboard.Key.W) && map.iswalkable((spPlayer), new Vector2f(0, -runningSpeed))) 
            {
                playerPosition = new Vector2f(playerPosition.X, playerPosition.Y - runningSpeed);
                if (time.TotalTime.Milliseconds % 500 < 250) this.spPlayer.Texture = txUp1;
                else this.spPlayer.Texture = txUp2;
                isPressed = true;
                rememberidle = 2;
            }

            if (Keyboard.IsKeyPressed(Keyboard.Key.A) && map.iswalkable((spPlayer), new Vector2f(0, runningSpeed))) 
            {
                playerPosition = new Vector2f(playerPosition.X, playerPosition.Y + runningSpeed);
                if (time.TotalTime.Milliseconds % 500 < 250) this.spPlayer.Texture = txDown1;
                else this.spPlayer.Texture = txDown2;
                isPressed = true;
                rememberidle = 3;
            }

            if (isPressed == false)
            {
                switch (rememberidle)
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

 if (Keyboard.IsKeyPressed(Keyboard.Key.D) && map.iswalkable((spPlayer), new Vector2f(-runningSpeed,0))) 
            {
                playerPosition = new Vector2f(playerPosition.X - runningSpeed, playerPosition.Y);
                if (time.TotalTime.Milliseconds % 500 < 250) this.spPlayer.Texture = txLeft1;
                else this.spPlayer.Texture = txLeft2;
                isPressed = true;
                rememberidle = 0;
            }

            if (Keyboard.IsKeyPressed(Keyboard.Key.W) && map.iswalkable((spPlayer), new Vector2f(runningSpeed, 0))) 
            {
                playerPosition = new Vector2f(playerPosition.X + runningSpeed, playerPosition.Y);
                if (time.TotalTime.Milliseconds % 500 < 250) this.spPlayer.Texture = txRight1;
                else this.spPlayer.Texture = txRight2;
                isPressed = true;
                rememberidle = 1;
            }

            if (Keyboard.IsKeyPressed(Keyboard.Key.S) && map.iswalkable((spPlayer), new Vector2f(0, -runningSpeed))) 
            {
                playerPosition = new Vector2f(playerPosition.X, playerPosition.Y - runningSpeed);
                if (time.TotalTime.Milliseconds % 500 < 250) this.spPlayer.Texture = txUp1;
                else this.spPlayer.Texture = txUp2;
                isPressed = true;
                rememberidle = 2;
            }

            if (Keyboard.IsKeyPressed(Keyboard.Key.A) && map.iswalkable((spPlayer), new Vector2f(0, runningSpeed))) 
            {
                playerPosition = new Vector2f(playerPosition.X, playerPosition.Y + runningSpeed);
                if (time.TotalTime.Milliseconds % 500 < 250) this.spPlayer.Texture = txDown1;
                else this.spPlayer.Texture = txDown2;
                isPressed = true;
                rememberidle = 3;
            }

            if (isPressed == false)
            {
                switch (rememberidle)
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

 if (Keyboard.IsKeyPressed(Keyboard.Key.D) && map.iswalkable((spPlayer), new Vector2f(-runningSpeed,0))) 
            {
                playerPosition = new Vector2f(playerPosition.X - runningSpeed, playerPosition.Y);
                if (time.TotalTime.Milliseconds % 500 < 250) this.spPlayer.Texture = txLeft1;
                else this.spPlayer.Texture = txLeft2;
                isPressed = true;
                rememberidle = 0;
            }

            if (Keyboard.IsKeyPressed(Keyboard.Key.W) && map.iswalkable((spPlayer), new Vector2f(runningSpeed, 0))) 
            {
                playerPosition = new Vector2f(playerPosition.X + runningSpeed, playerPosition.Y);
                if (time.TotalTime.Milliseconds % 500 < 250) this.spPlayer.Texture = txRight1;
                else this.spPlayer.Texture = txRight2;
                isPressed = true;
                rememberidle = 1;
            }

            if (Keyboard.IsKeyPressed(Keyboard.Key.A) && map.iswalkable((spPlayer), new Vector2f(0, -runningSpeed))) 
            {
                playerPosition = new Vector2f(playerPosition.X, playerPosition.Y - runningSpeed);
                if (time.TotalTime.Milliseconds % 500 < 250) this.spPlayer.Texture = txUp1;
                else this.spPlayer.Texture = txUp2;
                isPressed = true;
                rememberidle = 2;
            }

            if (Keyboard.IsKeyPressed(Keyboard.Key.S) && map.iswalkable((spPlayer), new Vector2f(0, runningSpeed))) 
            {
                playerPosition = new Vector2f(playerPosition.X, playerPosition.Y + runningSpeed);
                if (time.TotalTime.Milliseconds % 500 < 250) this.spPlayer.Texture = txDown1;
                else this.spPlayer.Texture = txDown2;
                isPressed = true;
                rememberidle = 3;
            }

            if (isPressed == false)
            {
                switch (rememberidle)
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

 if (Keyboard.IsKeyPressed(Keyboard.Key.S) && map.iswalkable((spPlayer), new Vector2f(-runningSpeed,0))) 
            {
                playerPosition = new Vector2f(playerPosition.X - runningSpeed, playerPosition.Y);
                if (time.TotalTime.Milliseconds % 500 < 250) this.spPlayer.Texture = txLeft1;
                else this.spPlayer.Texture = txLeft2;
                isPressed = true;
                rememberidle = 0;
            }

            if (Keyboard.IsKeyPressed(Keyboard.Key.A) && map.iswalkable((spPlayer), new Vector2f(runningSpeed, 0))) 
            {
                playerPosition = new Vector2f(playerPosition.X + runningSpeed, playerPosition.Y);
                if (time.TotalTime.Milliseconds % 500 < 250) this.spPlayer.Texture = txRight1;
                else this.spPlayer.Texture = txRight2;
                isPressed = true;
                rememberidle = 1;
            }

            if (Keyboard.IsKeyPressed(Keyboard.Key.W) && map.iswalkable((spPlayer), new Vector2f(0, -runningSpeed))) 
            {
                playerPosition = new Vector2f(playerPosition.X, playerPosition.Y - runningSpeed);
                if (time.TotalTime.Milliseconds % 500 < 250) this.spPlayer.Texture = txUp1;
                else this.spPlayer.Texture = txUp2;
                isPressed = true;
                rememberidle = 2;
            }

            if (Keyboard.IsKeyPressed(Keyboard.Key.D) && map.iswalkable((spPlayer), new Vector2f(0, runningSpeed))) 
            {
                playerPosition = new Vector2f(playerPosition.X, playerPosition.Y + runningSpeed);
                if (time.TotalTime.Milliseconds % 500 < 250) this.spPlayer.Texture = txDown1;
                else this.spPlayer.Texture = txDown2;
                isPressed = true;
                rememberidle = 3;
            }

            if (isPressed == false)
            {
                switch (rememberidle)
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

 if (Keyboard.IsKeyPressed(Keyboard.Key.S) && map.iswalkable((spPlayer), new Vector2f(-runningSpeed,0))) 
            {
                playerPosition = new Vector2f(playerPosition.X - runningSpeed, playerPosition.Y);
                if (time.TotalTime.Milliseconds % 500 < 250) this.spPlayer.Texture = txLeft1;
                else this.spPlayer.Texture = txLeft2;
                isPressed = true;
                rememberidle = 0;
            }

            if (Keyboard.IsKeyPressed(Keyboard.Key.A) && map.iswalkable((spPlayer), new Vector2f(runningSpeed, 0))) 
            {
                playerPosition = new Vector2f(playerPosition.X + runningSpeed, playerPosition.Y);
                if (time.TotalTime.Milliseconds % 500 < 250) this.spPlayer.Texture = txRight1;
                else this.spPlayer.Texture = txRight2;
                isPressed = true;
                rememberidle = 1;
            }

            if (Keyboard.IsKeyPressed(Keyboard.Key.D) && map.iswalkable((spPlayer), new Vector2f(0, -runningSpeed))) 
            {
                playerPosition = new Vector2f(playerPosition.X, playerPosition.Y - runningSpeed);
                if (time.TotalTime.Milliseconds % 500 < 250) this.spPlayer.Texture = txUp1;
                else this.spPlayer.Texture = txUp2;
                isPressed = true;
                rememberidle = 2;
            }

            if (Keyboard.IsKeyPressed(Keyboard.Key.W) && map.iswalkable((spPlayer), new Vector2f(0, runningSpeed))) 
            {
                playerPosition = new Vector2f(playerPosition.X, playerPosition.Y + runningSpeed);
                if (time.TotalTime.Milliseconds % 500 < 250) this.spPlayer.Texture = txDown1;
                else this.spPlayer.Texture = txDown2;
                isPressed = true;
                rememberidle = 3;
            }

            if (isPressed == false)
            {
                switch (rememberidle)
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

 if (Keyboard.IsKeyPressed(Keyboard.Key.S) && map.iswalkable((spPlayer), new Vector2f(-runningSpeed,0))) 
            {
                playerPosition = new Vector2f(playerPosition.X - runningSpeed, playerPosition.Y);
                if (time.TotalTime.Milliseconds % 500 < 250) this.spPlayer.Texture = txLeft1;
                else this.spPlayer.Texture = txLeft2;
                isPressed = true;
                rememberidle = 0;
            }

            if (Keyboard.IsKeyPressed(Keyboard.Key.W) && map.iswalkable((spPlayer), new Vector2f(runningSpeed, 0))) 
            {
                playerPosition = new Vector2f(playerPosition.X + runningSpeed, playerPosition.Y);
                if (time.TotalTime.Milliseconds % 500 < 250) this.spPlayer.Texture = txRight1;
                else this.spPlayer.Texture = txRight2;
                isPressed = true;
                rememberidle = 1;
            }

            if (Keyboard.IsKeyPressed(Keyboard.Key.A) && map.iswalkable((spPlayer), new Vector2f(0, -runningSpeed))) 
            {
                playerPosition = new Vector2f(playerPosition.X, playerPosition.Y - runningSpeed);
                if (time.TotalTime.Milliseconds % 500 < 250) this.spPlayer.Texture = txUp1;
                else this.spPlayer.Texture = txUp2;
                isPressed = true;
                rememberidle = 2;
            }

            if (Keyboard.IsKeyPressed(Keyboard.Key.D) && map.iswalkable((spPlayer), new Vector2f(0, runningSpeed))) 
            {
                playerPosition = new Vector2f(playerPosition.X, playerPosition.Y + runningSpeed);
                if (time.TotalTime.Milliseconds % 500 < 250) this.spPlayer.Texture = txDown1;
                else this.spPlayer.Texture = txDown2;
                isPressed = true;
                rememberidle = 3;
            }

            if (isPressed == false)
            {
                switch (rememberidle)
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

 if (Keyboard.IsKeyPressed(Keyboard.Key.S) && map.iswalkable((spPlayer), new Vector2f(-runningSpeed,0))) 
            {
                playerPosition = new Vector2f(playerPosition.X - runningSpeed, playerPosition.Y);
                if (time.TotalTime.Milliseconds % 500 < 250) this.spPlayer.Texture = txLeft1;
                else this.spPlayer.Texture = txLeft2;
                isPressed = true;
                rememberidle = 0;
            }

            if (Keyboard.IsKeyPressed(Keyboard.Key.W) && map.iswalkable((spPlayer), new Vector2f(runningSpeed, 0))) 
            {
                playerPosition = new Vector2f(playerPosition.X + runningSpeed, playerPosition.Y);
                if (time.TotalTime.Milliseconds % 500 < 250) this.spPlayer.Texture = txRight1;
                else this.spPlayer.Texture = txRight2;
                isPressed = true;
                rememberidle = 1;
            }

            if (Keyboard.IsKeyPressed(Keyboard.Key.D) && map.iswalkable((spPlayer), new Vector2f(0, -runningSpeed))) 
            {
                playerPosition = new Vector2f(playerPosition.X, playerPosition.Y - runningSpeed);
                if (time.TotalTime.Milliseconds % 500 < 250) this.spPlayer.Texture = txUp1;
                else this.spPlayer.Texture = txUp2;
                isPressed = true;
                rememberidle = 2;
            }

            if (Keyboard.IsKeyPressed(Keyboard.Key.A) && map.iswalkable((spPlayer), new Vector2f(0, runningSpeed))) 
            {
                playerPosition = new Vector2f(playerPosition.X, playerPosition.Y + runningSpeed);
                if (time.TotalTime.Milliseconds % 500 < 250) this.spPlayer.Texture = txDown1;
                else this.spPlayer.Texture = txDown2;
                isPressed = true;
                rememberidle = 3;
            }

            if (isPressed == false)
            {
                switch (rememberidle)
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

 if (Keyboard.IsKeyPressed(Keyboard.Key.S) && map.iswalkable((spPlayer), new Vector2f(-runningSpeed,0))) 
            {
                playerPosition = new Vector2f(playerPosition.X - runningSpeed, playerPosition.Y);
                if (time.TotalTime.Milliseconds % 500 < 250) this.spPlayer.Texture = txLeft1;
                else this.spPlayer.Texture = txLeft2;
                isPressed = true;
                rememberidle = 0;
            }

            if (Keyboard.IsKeyPressed(Keyboard.Key.D) && map.iswalkable((spPlayer), new Vector2f(runningSpeed, 0))) 
            {
                playerPosition = new Vector2f(playerPosition.X + runningSpeed, playerPosition.Y);
                if (time.TotalTime.Milliseconds % 500 < 250) this.spPlayer.Texture = txRight1;
                else this.spPlayer.Texture = txRight2;
                isPressed = true;
                rememberidle = 1;
            }

            if (Keyboard.IsKeyPressed(Keyboard.Key.W) && map.iswalkable((spPlayer), new Vector2f(0, -runningSpeed))) 
            {
                playerPosition = new Vector2f(playerPosition.X, playerPosition.Y - runningSpeed);
                if (time.TotalTime.Milliseconds % 500 < 250) this.spPlayer.Texture = txUp1;
                else this.spPlayer.Texture = txUp2;
                isPressed = true;
                rememberidle = 2;
            }

            if (Keyboard.IsKeyPressed(Keyboard.Key.A) && map.iswalkable((spPlayer), new Vector2f(0, runningSpeed))) 
            {
                playerPosition = new Vector2f(playerPosition.X, playerPosition.Y + runningSpeed);
                if (time.TotalTime.Milliseconds % 500 < 250) this.spPlayer.Texture = txDown1;
                else this.spPlayer.Texture = txDown2;
                isPressed = true;
                rememberidle = 3;
            }

            if (isPressed == false)
            {
                switch (rememberidle)
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

 if (Keyboard.IsKeyPressed(Keyboard.Key.S) && map.iswalkable((spPlayer), new Vector2f(-runningSpeed,0))) 
            {
                playerPosition = new Vector2f(playerPosition.X - runningSpeed, playerPosition.Y);
                if (time.TotalTime.Milliseconds % 500 < 250) this.spPlayer.Texture = txLeft1;
                else this.spPlayer.Texture = txLeft2;
                isPressed = true;
                rememberidle = 0;
            }

            if (Keyboard.IsKeyPressed(Keyboard.Key.D) && map.iswalkable((spPlayer), new Vector2f(runningSpeed, 0))) 
            {
                playerPosition = new Vector2f(playerPosition.X + runningSpeed, playerPosition.Y);
                if (time.TotalTime.Milliseconds % 500 < 250) this.spPlayer.Texture = txRight1;
                else this.spPlayer.Texture = txRight2;
                isPressed = true;
                rememberidle = 1;
            }

            if (Keyboard.IsKeyPressed(Keyboard.Key.A) && map.iswalkable((spPlayer), new Vector2f(0, -runningSpeed))) 
            {
                playerPosition = new Vector2f(playerPosition.X, playerPosition.Y - runningSpeed);
                if (time.TotalTime.Milliseconds % 500 < 250) this.spPlayer.Texture = txUp1;
                else this.spPlayer.Texture = txUp2;
                isPressed = true;
                rememberidle = 2;
            }

            if (Keyboard.IsKeyPressed(Keyboard.Key.W) && map.iswalkable((spPlayer), new Vector2f(0, runningSpeed))) 
            {
                playerPosition = new Vector2f(playerPosition.X, playerPosition.Y + runningSpeed);
                if (time.TotalTime.Milliseconds % 500 < 250) this.spPlayer.Texture = txDown1;
                else this.spPlayer.Texture = txDown2;
                isPressed = true;
                rememberidle = 3;
            }

            if (isPressed == false)
            {
                switch (rememberidle)
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

 if (Keyboard.IsKeyPressed(Keyboard.Key.W) && map.iswalkable((spPlayer), new Vector2f(-runningSpeed,0))) 
            {
                playerPosition = new Vector2f(playerPosition.X - runningSpeed, playerPosition.Y);
                if (time.TotalTime.Milliseconds % 500 < 250) this.spPlayer.Texture = txLeft1;
                else this.spPlayer.Texture = txLeft2;
                isPressed = true;
                rememberidle = 0;
            }

            if (Keyboard.IsKeyPressed(Keyboard.Key.A) && map.iswalkable((spPlayer), new Vector2f(runningSpeed, 0))) 
            {
                playerPosition = new Vector2f(playerPosition.X + runningSpeed, playerPosition.Y);
                if (time.TotalTime.Milliseconds % 500 < 250) this.spPlayer.Texture = txRight1;
                else this.spPlayer.Texture = txRight2;
                isPressed = true;
                rememberidle = 1;
            }

            if (Keyboard.IsKeyPressed(Keyboard.Key.S) && map.iswalkable((spPlayer), new Vector2f(0, -runningSpeed))) 
            {
                playerPosition = new Vector2f(playerPosition.X, playerPosition.Y - runningSpeed);
                if (time.TotalTime.Milliseconds % 500 < 250) this.spPlayer.Texture = txUp1;
                else this.spPlayer.Texture = txUp2;
                isPressed = true;
                rememberidle = 2;
            }

            if (Keyboard.IsKeyPressed(Keyboard.Key.D) && map.iswalkable((spPlayer), new Vector2f(0, runningSpeed))) 
            {
                playerPosition = new Vector2f(playerPosition.X, playerPosition.Y + runningSpeed);
                if (time.TotalTime.Milliseconds % 500 < 250) this.spPlayer.Texture = txDown1;
                else this.spPlayer.Texture = txDown2;
                isPressed = true;
                rememberidle = 3;
            }

            if (isPressed == false)
            {
                switch (rememberidle)
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

 if (Keyboard.IsKeyPressed(Keyboard.Key.W) && map.iswalkable((spPlayer), new Vector2f(-runningSpeed,0))) 
            {
                playerPosition = new Vector2f(playerPosition.X - runningSpeed, playerPosition.Y);
                if (time.TotalTime.Milliseconds % 500 < 250) this.spPlayer.Texture = txLeft1;
                else this.spPlayer.Texture = txLeft2;
                isPressed = true;
                rememberidle = 0;
            }

            if (Keyboard.IsKeyPressed(Keyboard.Key.A) && map.iswalkable((spPlayer), new Vector2f(runningSpeed, 0))) 
            {
                playerPosition = new Vector2f(playerPosition.X + runningSpeed, playerPosition.Y);
                if (time.TotalTime.Milliseconds % 500 < 250) this.spPlayer.Texture = txRight1;
                else this.spPlayer.Texture = txRight2;
                isPressed = true;
                rememberidle = 1;
            }

            if (Keyboard.IsKeyPressed(Keyboard.Key.D) && map.iswalkable((spPlayer), new Vector2f(0, -runningSpeed))) 
            {
                playerPosition = new Vector2f(playerPosition.X, playerPosition.Y - runningSpeed);
                if (time.TotalTime.Milliseconds % 500 < 250) this.spPlayer.Texture = txUp1;
                else this.spPlayer.Texture = txUp2;
                isPressed = true;
                rememberidle = 2;
            }

            if (Keyboard.IsKeyPressed(Keyboard.Key.S) && map.iswalkable((spPlayer), new Vector2f(0, runningSpeed))) 
            {
                playerPosition = new Vector2f(playerPosition.X, playerPosition.Y + runningSpeed);
                if (time.TotalTime.Milliseconds % 500 < 250) this.spPlayer.Texture = txDown1;
                else this.spPlayer.Texture = txDown2;
                isPressed = true;
                rememberidle = 3;
            }

            if (isPressed == false)
            {
                switch (rememberidle)
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

 if (Keyboard.IsKeyPressed(Keyboard.Key.W) && map.iswalkable((spPlayer), new Vector2f(-runningSpeed,0))) 
            {
                playerPosition = new Vector2f(playerPosition.X - runningSpeed, playerPosition.Y);
                if (time.TotalTime.Milliseconds % 500 < 250) this.spPlayer.Texture = txLeft1;
                else this.spPlayer.Texture = txLeft2;
                isPressed = true;
                rememberidle = 0;
            }

            if (Keyboard.IsKeyPressed(Keyboard.Key.S) && map.iswalkable((spPlayer), new Vector2f(runningSpeed, 0))) 
            {
                playerPosition = new Vector2f(playerPosition.X + runningSpeed, playerPosition.Y);
                if (time.TotalTime.Milliseconds % 500 < 250) this.spPlayer.Texture = txRight1;
                else this.spPlayer.Texture = txRight2;
                isPressed = true;
                rememberidle = 1;
            }

            if (Keyboard.IsKeyPressed(Keyboard.Key.A) && map.iswalkable((spPlayer), new Vector2f(0, -runningSpeed))) 
            {
                playerPosition = new Vector2f(playerPosition.X, playerPosition.Y - runningSpeed);
                if (time.TotalTime.Milliseconds % 500 < 250) this.spPlayer.Texture = txUp1;
                else this.spPlayer.Texture = txUp2;
                isPressed = true;
                rememberidle = 2;
            }

            if (Keyboard.IsKeyPressed(Keyboard.Key.D) && map.iswalkable((spPlayer), new Vector2f(0, runningSpeed))) 
            {
                playerPosition = new Vector2f(playerPosition.X, playerPosition.Y + runningSpeed);
                if (time.TotalTime.Milliseconds % 500 < 250) this.spPlayer.Texture = txDown1;
                else this.spPlayer.Texture = txDown2;
                isPressed = true;
                rememberidle = 3;
            }

            if (isPressed == false)
            {
                switch (rememberidle)
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

 if (Keyboard.IsKeyPressed(Keyboard.Key.W) && map.iswalkable((spPlayer), new Vector2f(-runningSpeed,0))) 
            {
                playerPosition = new Vector2f(playerPosition.X - runningSpeed, playerPosition.Y);
                if (time.TotalTime.Milliseconds % 500 < 250) this.spPlayer.Texture = txLeft1;
                else this.spPlayer.Texture = txLeft2;
                isPressed = true;
                rememberidle = 0;
            }

            if (Keyboard.IsKeyPressed(Keyboard.Key.S) && map.iswalkable((spPlayer), new Vector2f(runningSpeed, 0))) 
            {
                playerPosition = new Vector2f(playerPosition.X + runningSpeed, playerPosition.Y);
                if (time.TotalTime.Milliseconds % 500 < 250) this.spPlayer.Texture = txRight1;
                else this.spPlayer.Texture = txRight2;
                isPressed = true;
                rememberidle = 1;
            }

            if (Keyboard.IsKeyPressed(Keyboard.Key.D) && map.iswalkable((spPlayer), new Vector2f(0, -runningSpeed))) 
            {
                playerPosition = new Vector2f(playerPosition.X, playerPosition.Y - runningSpeed);
                if (time.TotalTime.Milliseconds % 500 < 250) this.spPlayer.Texture = txUp1;
                else this.spPlayer.Texture = txUp2;
                isPressed = true;
                rememberidle = 2;
            }

            if (Keyboard.IsKeyPressed(Keyboard.Key.A) && map.iswalkable((spPlayer), new Vector2f(0, runningSpeed))) 
            {
                playerPosition = new Vector2f(playerPosition.X, playerPosition.Y + runningSpeed);
                if (time.TotalTime.Milliseconds % 500 < 250) this.spPlayer.Texture = txDown1;
                else this.spPlayer.Texture = txDown2;
                isPressed = true;
                rememberidle = 3;
            }

            if (isPressed == false)
            {
                switch (rememberidle)
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


 if (Keyboard.IsKeyPressed(Keyboard.Key.W) && map.iswalkable((spPlayer), new Vector2f(-runningSpeed,0))) 
            {
                playerPosition = new Vector2f(playerPosition.X - runningSpeed, playerPosition.Y);
                if (time.TotalTime.Milliseconds % 500 < 250) this.spPlayer.Texture = txLeft1;
                else this.spPlayer.Texture = txLeft2;
                isPressed = true;
                rememberidle = 0;
            }

            if (Keyboard.IsKeyPressed(Keyboard.Key.D) && map.iswalkable((spPlayer), new Vector2f(runningSpeed, 0))) 
            {
                playerPosition = new Vector2f(playerPosition.X + runningSpeed, playerPosition.Y);
                if (time.TotalTime.Milliseconds % 500 < 250) this.spPlayer.Texture = txRight1;
                else this.spPlayer.Texture = txRight2;
                isPressed = true;
                rememberidle = 1;
            }

            if (Keyboard.IsKeyPressed(Keyboard.Key.A) && map.iswalkable((spPlayer), new Vector2f(0, -runningSpeed))) 
            {
                playerPosition = new Vector2f(playerPosition.X, playerPosition.Y - runningSpeed);
                if (time.TotalTime.Milliseconds % 500 < 250) this.spPlayer.Texture = txUp1;
                else this.spPlayer.Texture = txUp2;
                isPressed = true;
                rememberidle = 2;
            }

            if (Keyboard.IsKeyPressed(Keyboard.Key.S) && map.iswalkable((spPlayer), new Vector2f(0, runningSpeed))) 
            {
                playerPosition = new Vector2f(playerPosition.X, playerPosition.Y + runningSpeed);
                if (time.TotalTime.Milliseconds % 500 < 250) this.spPlayer.Texture = txDown1;
                else this.spPlayer.Texture = txDown2;
                isPressed = true;
                rememberidle = 3;
            }

            if (isPressed == false)
            {
                switch (rememberidle)
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

 if (Keyboard.IsKeyPressed(Keyboard.Key.W) && map.iswalkable((spPlayer), new Vector2f(-runningSpeed,0))) 
            {
                playerPosition = new Vector2f(playerPosition.X - runningSpeed, playerPosition.Y);
                if (time.TotalTime.Milliseconds % 500 < 250) this.spPlayer.Texture = txLeft1;
                else this.spPlayer.Texture = txLeft2;
                isPressed = true;
                rememberidle = 0;
            }

            if (Keyboard.IsKeyPressed(Keyboard.Key.D) && map.iswalkable((spPlayer), new Vector2f(runningSpeed, 0))) 
            {
                playerPosition = new Vector2f(playerPosition.X + runningSpeed, playerPosition.Y);
                if (time.TotalTime.Milliseconds % 500 < 250) this.spPlayer.Texture = txRight1;
                else this.spPlayer.Texture = txRight2;
                isPressed = true;
                rememberidle = 1;
            }

            if (Keyboard.IsKeyPressed(Keyboard.Key.S) && map.iswalkable((spPlayer), new Vector2f(0, -runningSpeed))) 
            {
                playerPosition = new Vector2f(playerPosition.X, playerPosition.Y - runningSpeed);
                if (time.TotalTime.Milliseconds % 500 < 250) this.spPlayer.Texture = txUp1;
                else this.spPlayer.Texture = txUp2;
                isPressed = true;
                rememberidle = 2;
            }

            if (Keyboard.IsKeyPressed(Keyboard.Key.A) && map.iswalkable((spPlayer), new Vector2f(0, runningSpeed))) 
            {
                playerPosition = new Vector2f(playerPosition.X, playerPosition.Y + runningSpeed);
                if (time.TotalTime.Milliseconds % 500 < 250) this.spPlayer.Texture = txDown1;
                else this.spPlayer.Texture = txDown2;
                isPressed = true;
                rememberidle = 3;
            }

            if (isPressed == false)
            {
                switch (rememberidle)
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
