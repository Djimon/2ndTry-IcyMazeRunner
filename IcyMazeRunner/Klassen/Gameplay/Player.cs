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
        Map absmap;


        /* ~~~~ Timeobject, um Zeit für den Herausforderungsmodus zu messen ~~~~ */
        GameTime moveTime;

        /* ~~~~ Wichtige Playerattribute ~~~~ */
        Sprite playerSprite {get;set;}
        Vector2f playerPosition;


        // benötigt?
        EGameStates gamestate;

        /* ~~~~ Texturen für Stehen und Bewegungsanimation ~~~~ */
        Texture down1;
        Texture down2;
        Texture down3;
        Texture up1;
        Texture up2;
        Texture up3;
        Texture right1;
        Texture right2;
        Texture right3;
        Texture left1;
        Texture left2;
        Texture left3;

        /* ~~~~ Attribute für Animation, wenn Spieler stehen bleibt ~~~~ */
        bool isPressed = false;
        int rememberidle = 0;


        /* ~~~~ Attribute für den Herausfoderungsmodus ~~~~ */
        bool is_controlchange = false;
        int movechangerstate = 0;
        Random random = new Random();


        /* ~~~~ Attribute und Zeitobjekt für die Animation, wenn Spieler stirbt ~~~~ */
        public GameTime DeathWatch;
        bool DeathWatchIsOn;


        /* ~~~~ Attribute für Fallanimation ~~~~ */
        Texture Fall1a;
        Texture Fall1b;
        Texture Fall2a;
        Texture Fall2b;
        Texture Fall3a;
        Texture Fall3b;
        Texture Fall4a;
        Texture Fall4b;
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
                down1 = new Texture("Texturen/Player/down1.png");
                down2 = new Texture("Texturen/Player/down2.png");
                down3 = new Texture("Texturen/Player/downidle.png");
                up1 = new Texture("Texturen/Player/up1.png");
                up2 = new Texture("Texturen/Player/up2.png");
                up3 = new Texture("Texturen/Player/upidle.png");
                right1 = new Texture("Texturen/Player/right1.png");
                right2 = new Texture("Texturen/Player/right2.png");
                right3 = new Texture("Texturen/Player/rightidle.png");
                left1 = new Texture("Texturen/Player/left1.png");
                left2 = new Texture("Texturen/Player/left2.png");
                left3 = new Texture("Texturen/Player/leftidle.png");


            //Fallanimationstexturen werden geladen
                Fall1a = new Texture("Texturen/Player/Fallanimation/fall1a Platzhalter.png");
                Fall1b = new Texture("Texturen/Player/Fallanimation/fall1b Platzhalter.png");
                Fall2a = new Texture("Texturen/Player/Fallanimation/fall2a Platzhalter.png");
                Fall2b = new Texture("Texturen/Player/Fallanimation/fall2b Platzhalter.png");
                Fall3a = new Texture("Texturen/Player/Fallanimation/fall3a Platzhalter.png");
                Fall3b = new Texture("Texturen/Player/Fallanimation/fall3b Platzhalter.png");
                Fall4a = new Texture("Texturen/Player/Fallanimation/fall4a Platzhalter.png");
                Fall4b = new Texture("Texturen/Player/Fallanimation/fall4b Platzhalter.png");
           
           //Standardtextur wird festgelegt
            playerSprite = new Sprite(down3);


            //Sprite wird geladen
            playerSprite.Scale = new Vector2f(1f, 1f); 
            playerSprite.Position = playerPosition;
            playerSprite = new Sprite(playerSprite);


            //Kopie der Map für move-Methode weiter unten
            absmap = map;
                   

            // Stoppuhr für Herausforderungsmodus starten
            moveTime = new GameTime();
            moveTime.Watch.Start();

            // Attribute und Objekte für Todesanimation initialisieren
            DeathWatch = new GameTime();
            DeathWatchIsOn = false;
        }


        /* ~~~~~~~~~~~~~~~~~~ Getter und Setter ~~~~~~~~~~~~~~*/


        //unnötig?
        public Sprite getplayerSprite()
        {
            return this.playerSprite;
        }


        //unnötig?
        public void setSpritePos(Vector2f pos)
        {
            playerSprite.Position = pos;
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
            return playerSprite.Position.X;
        }

        public float getYPosition()
        {
            return playerSprite.Position.Y;
        }

        public float getWidth()
        {
            return playerSprite.Texture.Size.X;
        }

        public float getHeigth()
        {
            return playerSprite.Texture.Size.Y;
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

            move(this.absmap, time);
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


            if (Keyboard.IsKeyPressed(Keyboard.Key.A) && map.iswalkable((playerSprite), new Vector2f(-runningSpeed, 0)))
            {
                playerPosition = new Vector2f(playerPosition.X - runningSpeed, playerPosition.Y);
                if (time.TotalTime.Milliseconds % 500 < 250) this.playerSprite.Texture = left1;
                else this.playerSprite.Texture = left2;
                isPressed = true;
                rememberidle = 0;
            }

            if (Keyboard.IsKeyPressed(Keyboard.Key.D) && map.iswalkable((playerSprite), new Vector2f(runningSpeed, 0)))
            {
                playerPosition = new Vector2f(playerPosition.X + runningSpeed, playerPosition.Y);
                if (time.TotalTime.Milliseconds % 500 < 250) this.playerSprite.Texture = right1;
                else this.playerSprite.Texture = right2;
                isPressed = true;
                rememberidle = 1;
            }

            if (Keyboard.IsKeyPressed(Keyboard.Key.W) && map.iswalkable((playerSprite), new Vector2f(0, -runningSpeed)))
            {
                playerPosition = new Vector2f(playerPosition.X, playerPosition.Y - runningSpeed);
                if (time.TotalTime.Milliseconds % 500 < 250) this.playerSprite.Texture = up1;
                else this.playerSprite.Texture = up2;
                isPressed = true;
                rememberidle = 2;
            }

            if (Keyboard.IsKeyPressed(Keyboard.Key.S) && map.iswalkable((playerSprite), new Vector2f(0, runningSpeed)))
            {
                playerPosition = new Vector2f(playerPosition.X, playerPosition.Y + runningSpeed);
                if (time.TotalTime.Milliseconds % 500 < 250) this.playerSprite.Texture = down1;
                else this.playerSprite.Texture = down2;
                isPressed = true;
                rememberidle = 3;
            }

            /*~~~~ Wenn Spieler sich nicht bewegt, ist isPressed falsch, Textur anpassen ~~~~*/
            if (isPressed == false)
            {
                switch (rememberidle)
                {
                    case 0: this.playerSprite.Texture = left3;
                        break;
                    case 1: this.playerSprite.Texture = right3;
                        break;
                    case 2: this.playerSprite.Texture = up3;
                        break;
                    case 3: this.playerSprite.Texture = down3;
                        break;
                    default: this.playerSprite.Texture = down3;
                        break;
                }   
            }
        }

        /*~~~~ Spriteposition an Position des Objektes anpassen ~~~~*/
        playerSprite.Position = playerPosition;


        }

        /*~~~~ Animation des Todes ~~~~*/
        public void DeathAnimation(int typeOfDeath)
        {

            /*~~~~ Stoppuhr starten, wenn sie noch nicht gestartet ist ~~~~*/
            if (!DeathWatchIsOn)
            {
                setDeathWatchIsOn(true);
                DeathWatch.Watch.Start();
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
                        if (DeathWatch.Watch.ElapsedMilliseconds > 0 && DeathWatch.Watch.ElapsedMilliseconds < 1000)
                        {
                            if (fallAnimationCounter%2==0)
                            {
                                playerSprite.Texture = Fall1a;
                            }
                            else
                            {
                                playerSprite.Texture = Fall1b;
                            }
                        }

                        if (DeathWatch.Watch.ElapsedMilliseconds > 1000 && DeathWatch.Watch.ElapsedMilliseconds < 2000)
                        {
                            if (fallAnimationCounter % 2 == 0)
                            {
                                playerSprite.Texture = Fall2a;
                            }
                            else
                            {
                                playerSprite.Texture = Fall2b;
                            }
                        }

                        if (DeathWatch.Watch.ElapsedMilliseconds > 2000 && DeathWatch.Watch.ElapsedMilliseconds < 3000)
                        {
                            if (fallAnimationCounter % 2 == 0)
                            {
                                playerSprite.Texture = Fall3a;
                            }
                            else
                            {
                                playerSprite.Texture = Fall3b;
                            }
                        }

                        if (DeathWatch.Watch.ElapsedMilliseconds > 3000 && DeathWatch.Watch.ElapsedMilliseconds < 4000)
                        {
                            if (fallAnimationCounter % 2 == 0)
                            {
                                playerSprite.Texture = Fall4a;
                            }
                            else
                            {
                                playerSprite.Texture = Fall4b;
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
            win.Draw(playerSprite); // Element anpassen
        }
        

        // LAAAAANNNNNGGGEEEE Methode für die 24 möglichen Zuweisungen der Tastatur im Hardmode bis zum Ende der Klasse
        public void changingmove(Map map, GameTime time) {


            /*~~~~ Neue Zuordnung der Tastatur an die Bewegungsrichtung alle 20 Sekunden ~~~~*/
            if (moveTime.Watch.ElapsedMilliseconds >= 20000)
            {
                movechangerstate = random.Next(23);
                moveTime.Watch.Restart();
            }


            /*~~~~ Alle möglichen Fälle für die Steuerung ~~~~*/
switch (movechangerstate) 
{
    case 0:

            if (Keyboard.IsKeyPressed(Keyboard.Key.A) && map.iswalkable((playerSprite), new Vector2f(-runningSpeed,0))) 
            {
                playerPosition = new Vector2f(playerPosition.X - runningSpeed, playerPosition.Y);
                if (time.TotalTime.Milliseconds % 500 < 250) this.playerSprite.Texture = left1;
                else this.playerSprite.Texture = left2;
                isPressed = true;
                rememberidle = 0;
            }

            if (Keyboard.IsKeyPressed(Keyboard.Key.D) && map.iswalkable((playerSprite), new Vector2f(runningSpeed, 0))) 
            {
                playerPosition = new Vector2f(playerPosition.X + runningSpeed, playerPosition.Y);
                if (time.TotalTime.Milliseconds % 500 < 250) this.playerSprite.Texture = right1;
                else this.playerSprite.Texture = right2;
                isPressed = true;
                rememberidle = 1;
            }

            if (Keyboard.IsKeyPressed(Keyboard.Key.W) && map.iswalkable((playerSprite), new Vector2f(0, -runningSpeed))) 
            {
                playerPosition = new Vector2f(playerPosition.X, playerPosition.Y - runningSpeed);
                if (time.TotalTime.Milliseconds % 500 < 250) this.playerSprite.Texture = up1;
                else this.playerSprite.Texture = up2;
                isPressed = true;
                rememberidle = 2;
            }

            if (Keyboard.IsKeyPressed(Keyboard.Key.S) && map.iswalkable((playerSprite), new Vector2f(0, runningSpeed))) 
            {
                playerPosition = new Vector2f(playerPosition.X, playerPosition.Y + runningSpeed);
                if (time.TotalTime.Milliseconds % 500 < 250) this.playerSprite.Texture = down1;
                else this.playerSprite.Texture = down2;
                isPressed = true;
                rememberidle = 3;
            }

            if (isPressed == false)
            {
                switch (rememberidle)
                {
                    case 0: this.playerSprite.Texture = left3;
                        break;
                    case 1: this.playerSprite.Texture = right3;
                        break;
                    case 2: this.playerSprite.Texture = up3;
                        break;
                    case 3: this.playerSprite.Texture = down3;
                        break;
                    default: this.playerSprite.Texture = down3;
                        break;
                }
            }
	    break;





    case 1:

            if (Keyboard.IsKeyPressed(Keyboard.Key.A) && map.iswalkable((playerSprite), new Vector2f(-runningSpeed,0))) 
            {
                playerPosition = new Vector2f(playerPosition.X - runningSpeed, playerPosition.Y);
                if (time.TotalTime.Milliseconds % 500 < 250) this.playerSprite.Texture = left1;
                else this.playerSprite.Texture = left2;
                isPressed = true;
                rememberidle = 0;
            }

            if (Keyboard.IsKeyPressed(Keyboard.Key.D) && map.iswalkable((playerSprite), new Vector2f(runningSpeed, 0))) 
            {
                playerPosition = new Vector2f(playerPosition.X + runningSpeed, playerPosition.Y);
                if (time.TotalTime.Milliseconds % 500 < 250) this.playerSprite.Texture = right1;
                else this.playerSprite.Texture = right2;
                isPressed = true;
                rememberidle = 1;
            }

            if (Keyboard.IsKeyPressed(Keyboard.Key.S) && map.iswalkable((playerSprite), new Vector2f(0, -runningSpeed))) 
            {
                playerPosition = new Vector2f(playerPosition.X, playerPosition.Y - runningSpeed);
                if (time.TotalTime.Milliseconds % 500 < 250) this.playerSprite.Texture = up1;
                else this.playerSprite.Texture = up2;
                isPressed = true;
                rememberidle = 2;
            }

            if (Keyboard.IsKeyPressed(Keyboard.Key.W) && map.iswalkable((playerSprite), new Vector2f(0, runningSpeed))) 
            {
                playerPosition = new Vector2f(playerPosition.X, playerPosition.Y + runningSpeed);
                if (time.TotalTime.Milliseconds % 500 < 250) this.playerSprite.Texture = down1;
                else this.playerSprite.Texture = down2;
                isPressed = true;
                rememberidle = 3;
            }

            if (isPressed == false)
            {
                switch (rememberidle)
                {
                    case 0: this.playerSprite.Texture = left3;
                        break;
                    case 1: this.playerSprite.Texture = right3;
                        break;
                    case 2: this.playerSprite.Texture = up3;
                        break;
                    case 3: this.playerSprite.Texture = down3;
                        break;
                    default: this.playerSprite.Texture = down3;
                        break;
                }
            }
	    break;


    case 2:

            if (Keyboard.IsKeyPressed(Keyboard.Key.A) && map.iswalkable((playerSprite), new Vector2f(-runningSpeed,0))) 
            {
                playerPosition = new Vector2f(playerPosition.X - runningSpeed, playerPosition.Y);
                if (time.TotalTime.Milliseconds % 500 < 250) this.playerSprite.Texture = left1;
                else this.playerSprite.Texture = left2;
                isPressed = true;
                rememberidle = 0;
            }

            if (Keyboard.IsKeyPressed(Keyboard.Key.S) && map.iswalkable((playerSprite), new Vector2f(runningSpeed, 0))) 
            {
                playerPosition = new Vector2f(playerPosition.X + runningSpeed, playerPosition.Y);
                if (time.TotalTime.Milliseconds % 500 < 250) this.playerSprite.Texture = right1;
                else this.playerSprite.Texture = right2;
                isPressed = true;
                rememberidle = 1;
            }

            if (Keyboard.IsKeyPressed(Keyboard.Key.W) && map.iswalkable((playerSprite), new Vector2f(0, -runningSpeed))) 
            {
                playerPosition = new Vector2f(playerPosition.X, playerPosition.Y - runningSpeed);
                if (time.TotalTime.Milliseconds % 500 < 250) this.playerSprite.Texture = up1;
                else this.playerSprite.Texture = up2;
                isPressed = true;
                rememberidle = 2;
            }

            if (Keyboard.IsKeyPressed(Keyboard.Key.D) && map.iswalkable((playerSprite), new Vector2f(0, runningSpeed))) 
            {
                playerPosition = new Vector2f(playerPosition.X, playerPosition.Y + runningSpeed);
                if (time.TotalTime.Milliseconds % 500 < 250) this.playerSprite.Texture = down1;
                else this.playerSprite.Texture = down2;
                isPressed = true;
                rememberidle = 3;
            }

            if (isPressed == false)
            {
                switch (rememberidle)
                {
                    case 0: this.playerSprite.Texture = left3;
                        break;
                    case 1: this.playerSprite.Texture = right3;
                        break;
                    case 2: this.playerSprite.Texture = up3;
                        break;
                    case 3: this.playerSprite.Texture = down3;
                        break;
                    default: this.playerSprite.Texture = down3;
                        break;
                }
            }

	    break;


    case 3:



            if (Keyboard.IsKeyPressed(Keyboard.Key.A) && map.iswalkable((playerSprite), new Vector2f(-runningSpeed,0))) 
            {
                playerPosition = new Vector2f(playerPosition.X - runningSpeed, playerPosition.Y);
                if (time.TotalTime.Milliseconds % 500 < 250) this.playerSprite.Texture = left1;
                else this.playerSprite.Texture = left2;
                isPressed = true;
                rememberidle = 0;
            }

            if (Keyboard.IsKeyPressed(Keyboard.Key.S) && map.iswalkable((playerSprite), new Vector2f(runningSpeed, 0))) 
            {
                playerPosition = new Vector2f(playerPosition.X + runningSpeed, playerPosition.Y);
                if (time.TotalTime.Milliseconds % 500 < 250) this.playerSprite.Texture = right1;
                else this.playerSprite.Texture = right2;
                isPressed = true;
                rememberidle = 1;
            }

            if (Keyboard.IsKeyPressed(Keyboard.Key.D) && map.iswalkable((playerSprite), new Vector2f(0, -runningSpeed))) 
            {
                playerPosition = new Vector2f(playerPosition.X, playerPosition.Y - runningSpeed);
                if (time.TotalTime.Milliseconds % 500 < 250) this.playerSprite.Texture = up1;
                else this.playerSprite.Texture = up2;
                isPressed = true;
                rememberidle = 2;
            }

            if (Keyboard.IsKeyPressed(Keyboard.Key.W) && map.iswalkable((playerSprite), new Vector2f(0, runningSpeed))) 
            {
                playerPosition = new Vector2f(playerPosition.X, playerPosition.Y + runningSpeed);
                if (time.TotalTime.Milliseconds % 500 < 250) this.playerSprite.Texture = down1;
                else this.playerSprite.Texture = down2;
                isPressed = true;
                rememberidle = 3;
            }

            if (isPressed == false)
            {
                switch (rememberidle)
                {
                    case 0: this.playerSprite.Texture = left3;
                        break;
                    case 1: this.playerSprite.Texture = right3;
                        break;
                    case 2: this.playerSprite.Texture = up3;
                        break;
                    case 3: this.playerSprite.Texture = down3;
                        break;
                    default: this.playerSprite.Texture = down3;
                        break;
                }
            }
	    break;



    case 4:


            if (Keyboard.IsKeyPressed(Keyboard.Key.A) && map.iswalkable((playerSprite), new Vector2f(-runningSpeed,0))) 
            {
                playerPosition = new Vector2f(playerPosition.X - runningSpeed, playerPosition.Y);
                if (time.TotalTime.Milliseconds % 500 < 250) this.playerSprite.Texture = left1;
                else this.playerSprite.Texture = left2;
                isPressed = true;
                rememberidle = 0;
            }

            if (Keyboard.IsKeyPressed(Keyboard.Key.W) && map.iswalkable((playerSprite), new Vector2f(runningSpeed, 0))) 
            {
                playerPosition = new Vector2f(playerPosition.X + runningSpeed, playerPosition.Y);
                if (time.TotalTime.Milliseconds % 500 < 250) this.playerSprite.Texture = right1;
                else this.playerSprite.Texture = right2;
                isPressed = true;
                rememberidle = 1;
            }

            if (Keyboard.IsKeyPressed(Keyboard.Key.S) && map.iswalkable((playerSprite), new Vector2f(0, -runningSpeed))) 
            {
                playerPosition = new Vector2f(playerPosition.X, playerPosition.Y - runningSpeed);
                if (time.TotalTime.Milliseconds % 500 < 250) this.playerSprite.Texture = up1;
                else this.playerSprite.Texture = up2;
                isPressed = true;
                rememberidle = 2;
            }

            if (Keyboard.IsKeyPressed(Keyboard.Key.D) && map.iswalkable((playerSprite), new Vector2f(0, runningSpeed))) 
            {
                playerPosition = new Vector2f(playerPosition.X, playerPosition.Y + runningSpeed);
                if (time.TotalTime.Milliseconds % 500 < 250) this.playerSprite.Texture = down1;
                else this.playerSprite.Texture = down2;
                isPressed = true;
                rememberidle = 3;
            }

            if (isPressed == false)
            {
                switch (rememberidle)
                {
                    case 0: this.playerSprite.Texture = left3;
                        break;
                    case 1: this.playerSprite.Texture = right3;
                        break;
                    case 2: this.playerSprite.Texture = up3;
                        break;
                    case 3: this.playerSprite.Texture = down3;
                        break;
                    default: this.playerSprite.Texture = down3;
                        break;
                }
            }
	    break;


    case 5:
            if (Keyboard.IsKeyPressed(Keyboard.Key.A) && map.iswalkable((playerSprite), new Vector2f(-runningSpeed,0))) 
            {
                playerPosition = new Vector2f(playerPosition.X - runningSpeed, playerPosition.Y);
                if (time.TotalTime.Milliseconds % 500 < 250) this.playerSprite.Texture = left1;
                else this.playerSprite.Texture = left2;
                isPressed = true;
                rememberidle = 0;
            }

            if (Keyboard.IsKeyPressed(Keyboard.Key.W) && map.iswalkable((playerSprite), new Vector2f(runningSpeed, 0))) 
            {
                playerPosition = new Vector2f(playerPosition.X + runningSpeed, playerPosition.Y);
                if (time.TotalTime.Milliseconds % 500 < 250) this.playerSprite.Texture = right1;
                else this.playerSprite.Texture = right2;
                isPressed = true;
                rememberidle = 1;
            }

            if (Keyboard.IsKeyPressed(Keyboard.Key.D) && map.iswalkable((playerSprite), new Vector2f(0, -runningSpeed))) 
            {
                playerPosition = new Vector2f(playerPosition.X, playerPosition.Y - runningSpeed);
                if (time.TotalTime.Milliseconds % 500 < 250) this.playerSprite.Texture = up1;
                else this.playerSprite.Texture = up2;
                isPressed = true;
                rememberidle = 2;
            }

            if (Keyboard.IsKeyPressed(Keyboard.Key.S) && map.iswalkable((playerSprite), new Vector2f(0, runningSpeed))) 
            {
                playerPosition = new Vector2f(playerPosition.X, playerPosition.Y + runningSpeed);
                if (time.TotalTime.Milliseconds % 500 < 250) this.playerSprite.Texture = down1;
                else this.playerSprite.Texture = down2;
                isPressed = true;
                rememberidle = 3;
            }

            if (isPressed == false)
            {
                switch (rememberidle)
                {
                    case 0: this.playerSprite.Texture = left3;
                        break;
                    case 1: this.playerSprite.Texture = right3;
                        break;
                    case 2: this.playerSprite.Texture = up3;
                        break;
                    case 3: this.playerSprite.Texture = down3;
                        break;
                    default: this.playerSprite.Texture = down3;
                        break;
                }
            }
	    break;

    case 6:
            if (Keyboard.IsKeyPressed(Keyboard.Key.D) && map.iswalkable((playerSprite), new Vector2f(-runningSpeed,0))) 
            {
                playerPosition = new Vector2f(playerPosition.X - runningSpeed, playerPosition.Y);
                if (time.TotalTime.Milliseconds % 500 < 250) this.playerSprite.Texture = left1;
                else this.playerSprite.Texture = left2;
                isPressed = true;
                rememberidle = 0;
            }

            if (Keyboard.IsKeyPressed(Keyboard.Key.A) && map.iswalkable((playerSprite), new Vector2f(runningSpeed, 0))) 
            {
                playerPosition = new Vector2f(playerPosition.X + runningSpeed, playerPosition.Y);
                if (time.TotalTime.Milliseconds % 500 < 250) this.playerSprite.Texture = right1;
                else this.playerSprite.Texture = right2;
                isPressed = true;
                rememberidle = 1;
            }

            if (Keyboard.IsKeyPressed(Keyboard.Key.W) && map.iswalkable((playerSprite), new Vector2f(0, -runningSpeed))) 
            {
                playerPosition = new Vector2f(playerPosition.X, playerPosition.Y - runningSpeed);
                if (time.TotalTime.Milliseconds % 500 < 250) this.playerSprite.Texture = up1;
                else this.playerSprite.Texture = up2;
                isPressed = true;
                rememberidle = 2;
            }

            if (Keyboard.IsKeyPressed(Keyboard.Key.S) && map.iswalkable((playerSprite), new Vector2f(0, runningSpeed))) 
            {
                playerPosition = new Vector2f(playerPosition.X, playerPosition.Y + runningSpeed);
                if (time.TotalTime.Milliseconds % 500 < 250) this.playerSprite.Texture = down1;
                else this.playerSprite.Texture = down2;
                isPressed = true;
                rememberidle = 3;
            }

            if (isPressed == false)
            {
                switch (rememberidle)
                {
                    case 0: this.playerSprite.Texture = left3;
                        break;
                    case 1: this.playerSprite.Texture = right3;
                        break;
                    case 2: this.playerSprite.Texture = up3;
                        break;
                    case 3: this.playerSprite.Texture = down3;
                        break;
                    default: this.playerSprite.Texture = down3;
                        break;
                }
            }
	    break;


    case 7:
            if (Keyboard.IsKeyPressed(Keyboard.Key.D) && map.iswalkable((playerSprite), new Vector2f(-runningSpeed,0))) 
            {
                playerPosition = new Vector2f(playerPosition.X - runningSpeed, playerPosition.Y);
                if (time.TotalTime.Milliseconds % 500 < 250) this.playerSprite.Texture = left1;
                else this.playerSprite.Texture = left2;
                isPressed = true;
                rememberidle = 0;
            }

            if (Keyboard.IsKeyPressed(Keyboard.Key.A) && map.iswalkable((playerSprite), new Vector2f(runningSpeed, 0))) 
            {
                playerPosition = new Vector2f(playerPosition.X + runningSpeed, playerPosition.Y);
                if (time.TotalTime.Milliseconds % 500 < 250) this.playerSprite.Texture = right1;
                else this.playerSprite.Texture = right2;
                isPressed = true;
                rememberidle = 1;
            }

            if (Keyboard.IsKeyPressed(Keyboard.Key.S) && map.iswalkable((playerSprite), new Vector2f(0, -runningSpeed))) 
            {
                playerPosition = new Vector2f(playerPosition.X, playerPosition.Y - runningSpeed);
                if (time.TotalTime.Milliseconds % 500 < 250) this.playerSprite.Texture = up1;
                else this.playerSprite.Texture = up2;
                isPressed = true;
                rememberidle = 2;
            }

            if (Keyboard.IsKeyPressed(Keyboard.Key.W) && map.iswalkable((playerSprite), new Vector2f(0, runningSpeed))) 
            {
                playerPosition = new Vector2f(playerPosition.X, playerPosition.Y + runningSpeed);
                if (time.TotalTime.Milliseconds % 500 < 250) this.playerSprite.Texture = down1;
                else this.playerSprite.Texture = down2;
                isPressed = true;
                rememberidle = 3;
            }

            if (isPressed == false)
            {
                switch (rememberidle)
                {
                    case 0: this.playerSprite.Texture = left3;
                        break;
                    case 1: this.playerSprite.Texture = right3;
                        break;
                    case 2: this.playerSprite.Texture = up3;
                        break;
                    case 3: this.playerSprite.Texture = down3;
                        break;
                    default: this.playerSprite.Texture = down3;
                        break;
                }
            }
	    break;



    case 8:

            if (Keyboard.IsKeyPressed(Keyboard.Key.D) && map.iswalkable((playerSprite), new Vector2f(-runningSpeed,0))) 
            {
                playerPosition = new Vector2f(playerPosition.X - runningSpeed, playerPosition.Y);
                if (time.TotalTime.Milliseconds % 500 < 250) this.playerSprite.Texture = left1;
                else this.playerSprite.Texture = left2;
                isPressed = true;
                rememberidle = 0;
            }

            if (Keyboard.IsKeyPressed(Keyboard.Key.S) && map.iswalkable((playerSprite), new Vector2f(runningSpeed, 0))) 
            {
                playerPosition = new Vector2f(playerPosition.X + runningSpeed, playerPosition.Y);
                if (time.TotalTime.Milliseconds % 500 < 250) this.playerSprite.Texture = right1;
                else this.playerSprite.Texture = right2;
                isPressed = true;
                rememberidle = 1;
            }

            if (Keyboard.IsKeyPressed(Keyboard.Key.A) && map.iswalkable((playerSprite), new Vector2f(0, -runningSpeed))) 
            {
                playerPosition = new Vector2f(playerPosition.X, playerPosition.Y - runningSpeed);
                if (time.TotalTime.Milliseconds % 500 < 250) this.playerSprite.Texture = up1;
                else this.playerSprite.Texture = up2;
                isPressed = true;
                rememberidle = 2;
            }

            if (Keyboard.IsKeyPressed(Keyboard.Key.W) && map.iswalkable((playerSprite), new Vector2f(0, runningSpeed))) 
            {
                playerPosition = new Vector2f(playerPosition.X, playerPosition.Y + runningSpeed);
                if (time.TotalTime.Milliseconds % 500 < 250) this.playerSprite.Texture = down1;
                else this.playerSprite.Texture = down2;
                isPressed = true;
                rememberidle = 3;
            }

            if (isPressed == false)
            {
                switch (rememberidle)
                {
                    case 0: this.playerSprite.Texture = left3;
                        break;
                    case 1: this.playerSprite.Texture = right3;
                        break;
                    case 2: this.playerSprite.Texture = up3;
                        break;
                    case 3: this.playerSprite.Texture = down3;
                        break;
                    default: this.playerSprite.Texture = down3;
                        break;
                }
            }
	    break;


    case 9:

            if (Keyboard.IsKeyPressed(Keyboard.Key.D) && map.iswalkable((playerSprite), new Vector2f(-runningSpeed,0))) 
            {
                playerPosition = new Vector2f(playerPosition.X - runningSpeed, playerPosition.Y);
                if (time.TotalTime.Milliseconds % 500 < 250) this.playerSprite.Texture = left1;
                else this.playerSprite.Texture = left2;
                isPressed = true;
                rememberidle = 0;
            }

            if (Keyboard.IsKeyPressed(Keyboard.Key.S) && map.iswalkable((playerSprite), new Vector2f(runningSpeed, 0))) 
            {
                playerPosition = new Vector2f(playerPosition.X + runningSpeed, playerPosition.Y);
                if (time.TotalTime.Milliseconds % 500 < 250) this.playerSprite.Texture = right1;
                else this.playerSprite.Texture = right2;
                isPressed = true;
                rememberidle = 1;
            }

            if (Keyboard.IsKeyPressed(Keyboard.Key.W) && map.iswalkable((playerSprite), new Vector2f(0, -runningSpeed))) 
            {
                playerPosition = new Vector2f(playerPosition.X, playerPosition.Y - runningSpeed);
                if (time.TotalTime.Milliseconds % 500 < 250) this.playerSprite.Texture = up1;
                else this.playerSprite.Texture = up2;
                isPressed = true;
                rememberidle = 2;
            }

            if (Keyboard.IsKeyPressed(Keyboard.Key.A) && map.iswalkable((playerSprite), new Vector2f(0, runningSpeed))) 
            {
                playerPosition = new Vector2f(playerPosition.X, playerPosition.Y + runningSpeed);
                if (time.TotalTime.Milliseconds % 500 < 250) this.playerSprite.Texture = down1;
                else this.playerSprite.Texture = down2;
                isPressed = true;
                rememberidle = 3;
            }

            if (isPressed == false)
            {
                switch (rememberidle)
                {
                    case 0: this.playerSprite.Texture = left3;
                        break;
                    case 1: this.playerSprite.Texture = right3;
                        break;
                    case 2: this.playerSprite.Texture = up3;
                        break;
                    case 3: this.playerSprite.Texture = down3;
                        break;
                    default: this.playerSprite.Texture = down3;
                        break;
                }
            }
	    break;



    case 10:

 if (Keyboard.IsKeyPressed(Keyboard.Key.D) && map.iswalkable((playerSprite), new Vector2f(-runningSpeed,0))) 
            {
                playerPosition = new Vector2f(playerPosition.X - runningSpeed, playerPosition.Y);
                if (time.TotalTime.Milliseconds % 500 < 250) this.playerSprite.Texture = left1;
                else this.playerSprite.Texture = left2;
                isPressed = true;
                rememberidle = 0;
            }

            if (Keyboard.IsKeyPressed(Keyboard.Key.W) && map.iswalkable((playerSprite), new Vector2f(runningSpeed, 0))) 
            {
                playerPosition = new Vector2f(playerPosition.X + runningSpeed, playerPosition.Y);
                if (time.TotalTime.Milliseconds % 500 < 250) this.playerSprite.Texture = right1;
                else this.playerSprite.Texture = right2;
                isPressed = true;
                rememberidle = 1;
            }

            if (Keyboard.IsKeyPressed(Keyboard.Key.S) && map.iswalkable((playerSprite), new Vector2f(0, -runningSpeed))) 
            {
                playerPosition = new Vector2f(playerPosition.X, playerPosition.Y - runningSpeed);
                if (time.TotalTime.Milliseconds % 500 < 250) this.playerSprite.Texture = up1;
                else this.playerSprite.Texture = up2;
                isPressed = true;
                rememberidle = 2;
            }

            if (Keyboard.IsKeyPressed(Keyboard.Key.A) && map.iswalkable((playerSprite), new Vector2f(0, runningSpeed))) 
            {
                playerPosition = new Vector2f(playerPosition.X, playerPosition.Y + runningSpeed);
                if (time.TotalTime.Milliseconds % 500 < 250) this.playerSprite.Texture = down1;
                else this.playerSprite.Texture = down2;
                isPressed = true;
                rememberidle = 3;
            }

            if (isPressed == false)
            {
                switch (rememberidle)
                {
                    case 0: this.playerSprite.Texture = left3;
                        break;
                    case 1: this.playerSprite.Texture = right3;
                        break;
                    case 2: this.playerSprite.Texture = up3;
                        break;
                    case 3: this.playerSprite.Texture = down3;
                        break;
                    default: this.playerSprite.Texture = down3;
                        break;
                }
            }
	    break;

    case 11:

 if (Keyboard.IsKeyPressed(Keyboard.Key.D) && map.iswalkable((playerSprite), new Vector2f(-runningSpeed,0))) 
            {
                playerPosition = new Vector2f(playerPosition.X - runningSpeed, playerPosition.Y);
                if (time.TotalTime.Milliseconds % 500 < 250) this.playerSprite.Texture = left1;
                else this.playerSprite.Texture = left2;
                isPressed = true;
                rememberidle = 0;
            }

            if (Keyboard.IsKeyPressed(Keyboard.Key.W) && map.iswalkable((playerSprite), new Vector2f(runningSpeed, 0))) 
            {
                playerPosition = new Vector2f(playerPosition.X + runningSpeed, playerPosition.Y);
                if (time.TotalTime.Milliseconds % 500 < 250) this.playerSprite.Texture = right1;
                else this.playerSprite.Texture = right2;
                isPressed = true;
                rememberidle = 1;
            }

            if (Keyboard.IsKeyPressed(Keyboard.Key.A) && map.iswalkable((playerSprite), new Vector2f(0, -runningSpeed))) 
            {
                playerPosition = new Vector2f(playerPosition.X, playerPosition.Y - runningSpeed);
                if (time.TotalTime.Milliseconds % 500 < 250) this.playerSprite.Texture = up1;
                else this.playerSprite.Texture = up2;
                isPressed = true;
                rememberidle = 2;
            }

            if (Keyboard.IsKeyPressed(Keyboard.Key.S) && map.iswalkable((playerSprite), new Vector2f(0, runningSpeed))) 
            {
                playerPosition = new Vector2f(playerPosition.X, playerPosition.Y + runningSpeed);
                if (time.TotalTime.Milliseconds % 500 < 250) this.playerSprite.Texture = down1;
                else this.playerSprite.Texture = down2;
                isPressed = true;
                rememberidle = 3;
            }

            if (isPressed == false)
            {
                switch (rememberidle)
                {
                    case 0: this.playerSprite.Texture = left3;
                        break;
                    case 1: this.playerSprite.Texture = right3;
                        break;
                    case 2: this.playerSprite.Texture = up3;
                        break;
                    case 3: this.playerSprite.Texture = down3;
                        break;
                    default: this.playerSprite.Texture = down3;
                        break;
                }
            }
	    break;


    case 12:

 if (Keyboard.IsKeyPressed(Keyboard.Key.S) && map.iswalkable((playerSprite), new Vector2f(-runningSpeed,0))) 
            {
                playerPosition = new Vector2f(playerPosition.X - runningSpeed, playerPosition.Y);
                if (time.TotalTime.Milliseconds % 500 < 250) this.playerSprite.Texture = left1;
                else this.playerSprite.Texture = left2;
                isPressed = true;
                rememberidle = 0;
            }

            if (Keyboard.IsKeyPressed(Keyboard.Key.A) && map.iswalkable((playerSprite), new Vector2f(runningSpeed, 0))) 
            {
                playerPosition = new Vector2f(playerPosition.X + runningSpeed, playerPosition.Y);
                if (time.TotalTime.Milliseconds % 500 < 250) this.playerSprite.Texture = right1;
                else this.playerSprite.Texture = right2;
                isPressed = true;
                rememberidle = 1;
            }

            if (Keyboard.IsKeyPressed(Keyboard.Key.W) && map.iswalkable((playerSprite), new Vector2f(0, -runningSpeed))) 
            {
                playerPosition = new Vector2f(playerPosition.X, playerPosition.Y - runningSpeed);
                if (time.TotalTime.Milliseconds % 500 < 250) this.playerSprite.Texture = up1;
                else this.playerSprite.Texture = up2;
                isPressed = true;
                rememberidle = 2;
            }

            if (Keyboard.IsKeyPressed(Keyboard.Key.D) && map.iswalkable((playerSprite), new Vector2f(0, runningSpeed))) 
            {
                playerPosition = new Vector2f(playerPosition.X, playerPosition.Y + runningSpeed);
                if (time.TotalTime.Milliseconds % 500 < 250) this.playerSprite.Texture = down1;
                else this.playerSprite.Texture = down2;
                isPressed = true;
                rememberidle = 3;
            }

            if (isPressed == false)
            {
                switch (rememberidle)
                {
                    case 0: this.playerSprite.Texture = left3;
                        break;
                    case 1: this.playerSprite.Texture = right3;
                        break;
                    case 2: this.playerSprite.Texture = up3;
                        break;
                    case 3: this.playerSprite.Texture = down3;
                        break;
                    default: this.playerSprite.Texture = down3;
                        break;
                }
            }
	    break;


    case 13:

 if (Keyboard.IsKeyPressed(Keyboard.Key.S) && map.iswalkable((playerSprite), new Vector2f(-runningSpeed,0))) 
            {
                playerPosition = new Vector2f(playerPosition.X - runningSpeed, playerPosition.Y);
                if (time.TotalTime.Milliseconds % 500 < 250) this.playerSprite.Texture = left1;
                else this.playerSprite.Texture = left2;
                isPressed = true;
                rememberidle = 0;
            }

            if (Keyboard.IsKeyPressed(Keyboard.Key.A) && map.iswalkable((playerSprite), new Vector2f(runningSpeed, 0))) 
            {
                playerPosition = new Vector2f(playerPosition.X + runningSpeed, playerPosition.Y);
                if (time.TotalTime.Milliseconds % 500 < 250) this.playerSprite.Texture = right1;
                else this.playerSprite.Texture = right2;
                isPressed = true;
                rememberidle = 1;
            }

            if (Keyboard.IsKeyPressed(Keyboard.Key.D) && map.iswalkable((playerSprite), new Vector2f(0, -runningSpeed))) 
            {
                playerPosition = new Vector2f(playerPosition.X, playerPosition.Y - runningSpeed);
                if (time.TotalTime.Milliseconds % 500 < 250) this.playerSprite.Texture = up1;
                else this.playerSprite.Texture = up2;
                isPressed = true;
                rememberidle = 2;
            }

            if (Keyboard.IsKeyPressed(Keyboard.Key.W) && map.iswalkable((playerSprite), new Vector2f(0, runningSpeed))) 
            {
                playerPosition = new Vector2f(playerPosition.X, playerPosition.Y + runningSpeed);
                if (time.TotalTime.Milliseconds % 500 < 250) this.playerSprite.Texture = down1;
                else this.playerSprite.Texture = down2;
                isPressed = true;
                rememberidle = 3;
            }

            if (isPressed == false)
            {
                switch (rememberidle)
                {
                    case 0: this.playerSprite.Texture = left3;
                        break;
                    case 1: this.playerSprite.Texture = right3;
                        break;
                    case 2: this.playerSprite.Texture = up3;
                        break;
                    case 3: this.playerSprite.Texture = down3;
                        break;
                    default: this.playerSprite.Texture = down3;
                        break;
                }
            }
	    break;


    case 14:

 if (Keyboard.IsKeyPressed(Keyboard.Key.S) && map.iswalkable((playerSprite), new Vector2f(-runningSpeed,0))) 
            {
                playerPosition = new Vector2f(playerPosition.X - runningSpeed, playerPosition.Y);
                if (time.TotalTime.Milliseconds % 500 < 250) this.playerSprite.Texture = left1;
                else this.playerSprite.Texture = left2;
                isPressed = true;
                rememberidle = 0;
            }

            if (Keyboard.IsKeyPressed(Keyboard.Key.W) && map.iswalkable((playerSprite), new Vector2f(runningSpeed, 0))) 
            {
                playerPosition = new Vector2f(playerPosition.X + runningSpeed, playerPosition.Y);
                if (time.TotalTime.Milliseconds % 500 < 250) this.playerSprite.Texture = right1;
                else this.playerSprite.Texture = right2;
                isPressed = true;
                rememberidle = 1;
            }

            if (Keyboard.IsKeyPressed(Keyboard.Key.A) && map.iswalkable((playerSprite), new Vector2f(0, -runningSpeed))) 
            {
                playerPosition = new Vector2f(playerPosition.X, playerPosition.Y - runningSpeed);
                if (time.TotalTime.Milliseconds % 500 < 250) this.playerSprite.Texture = up1;
                else this.playerSprite.Texture = up2;
                isPressed = true;
                rememberidle = 2;
            }

            if (Keyboard.IsKeyPressed(Keyboard.Key.D) && map.iswalkable((playerSprite), new Vector2f(0, runningSpeed))) 
            {
                playerPosition = new Vector2f(playerPosition.X, playerPosition.Y + runningSpeed);
                if (time.TotalTime.Milliseconds % 500 < 250) this.playerSprite.Texture = down1;
                else this.playerSprite.Texture = down2;
                isPressed = true;
                rememberidle = 3;
            }

            if (isPressed == false)
            {
                switch (rememberidle)
                {
                    case 0: this.playerSprite.Texture = left3;
                        break;
                    case 1: this.playerSprite.Texture = right3;
                        break;
                    case 2: this.playerSprite.Texture = up3;
                        break;
                    case 3: this.playerSprite.Texture = down3;
                        break;
                    default: this.playerSprite.Texture = down3;
                        break;
                }
            }
	    break;



    case 15:

 if (Keyboard.IsKeyPressed(Keyboard.Key.S) && map.iswalkable((playerSprite), new Vector2f(-runningSpeed,0))) 
            {
                playerPosition = new Vector2f(playerPosition.X - runningSpeed, playerPosition.Y);
                if (time.TotalTime.Milliseconds % 500 < 250) this.playerSprite.Texture = left1;
                else this.playerSprite.Texture = left2;
                isPressed = true;
                rememberidle = 0;
            }

            if (Keyboard.IsKeyPressed(Keyboard.Key.W) && map.iswalkable((playerSprite), new Vector2f(runningSpeed, 0))) 
            {
                playerPosition = new Vector2f(playerPosition.X + runningSpeed, playerPosition.Y);
                if (time.TotalTime.Milliseconds % 500 < 250) this.playerSprite.Texture = right1;
                else this.playerSprite.Texture = right2;
                isPressed = true;
                rememberidle = 1;
            }

            if (Keyboard.IsKeyPressed(Keyboard.Key.D) && map.iswalkable((playerSprite), new Vector2f(0, -runningSpeed))) 
            {
                playerPosition = new Vector2f(playerPosition.X, playerPosition.Y - runningSpeed);
                if (time.TotalTime.Milliseconds % 500 < 250) this.playerSprite.Texture = up1;
                else this.playerSprite.Texture = up2;
                isPressed = true;
                rememberidle = 2;
            }

            if (Keyboard.IsKeyPressed(Keyboard.Key.A) && map.iswalkable((playerSprite), new Vector2f(0, runningSpeed))) 
            {
                playerPosition = new Vector2f(playerPosition.X, playerPosition.Y + runningSpeed);
                if (time.TotalTime.Milliseconds % 500 < 250) this.playerSprite.Texture = down1;
                else this.playerSprite.Texture = down2;
                isPressed = true;
                rememberidle = 3;
            }

            if (isPressed == false)
            {
                switch (rememberidle)
                {
                    case 0: this.playerSprite.Texture = left3;
                        break;
                    case 1: this.playerSprite.Texture = right3;
                        break;
                    case 2: this.playerSprite.Texture = up3;
                        break;
                    case 3: this.playerSprite.Texture = down3;
                        break;
                    default: this.playerSprite.Texture = down3;
                        break;
                }
            }
	    break;



    case 16:

 if (Keyboard.IsKeyPressed(Keyboard.Key.S) && map.iswalkable((playerSprite), new Vector2f(-runningSpeed,0))) 
            {
                playerPosition = new Vector2f(playerPosition.X - runningSpeed, playerPosition.Y);
                if (time.TotalTime.Milliseconds % 500 < 250) this.playerSprite.Texture = left1;
                else this.playerSprite.Texture = left2;
                isPressed = true;
                rememberidle = 0;
            }

            if (Keyboard.IsKeyPressed(Keyboard.Key.D) && map.iswalkable((playerSprite), new Vector2f(runningSpeed, 0))) 
            {
                playerPosition = new Vector2f(playerPosition.X + runningSpeed, playerPosition.Y);
                if (time.TotalTime.Milliseconds % 500 < 250) this.playerSprite.Texture = right1;
                else this.playerSprite.Texture = right2;
                isPressed = true;
                rememberidle = 1;
            }

            if (Keyboard.IsKeyPressed(Keyboard.Key.W) && map.iswalkable((playerSprite), new Vector2f(0, -runningSpeed))) 
            {
                playerPosition = new Vector2f(playerPosition.X, playerPosition.Y - runningSpeed);
                if (time.TotalTime.Milliseconds % 500 < 250) this.playerSprite.Texture = up1;
                else this.playerSprite.Texture = up2;
                isPressed = true;
                rememberidle = 2;
            }

            if (Keyboard.IsKeyPressed(Keyboard.Key.A) && map.iswalkable((playerSprite), new Vector2f(0, runningSpeed))) 
            {
                playerPosition = new Vector2f(playerPosition.X, playerPosition.Y + runningSpeed);
                if (time.TotalTime.Milliseconds % 500 < 250) this.playerSprite.Texture = down1;
                else this.playerSprite.Texture = down2;
                isPressed = true;
                rememberidle = 3;
            }

            if (isPressed == false)
            {
                switch (rememberidle)
                {
                    case 0: this.playerSprite.Texture = left3;
                        break;
                    case 1: this.playerSprite.Texture = right3;
                        break;
                    case 2: this.playerSprite.Texture = up3;
                        break;
                    case 3: this.playerSprite.Texture = down3;
                        break;
                    default: this.playerSprite.Texture = down3;
                        break;
                }
            }
	    break;



    case 17:

 if (Keyboard.IsKeyPressed(Keyboard.Key.S) && map.iswalkable((playerSprite), new Vector2f(-runningSpeed,0))) 
            {
                playerPosition = new Vector2f(playerPosition.X - runningSpeed, playerPosition.Y);
                if (time.TotalTime.Milliseconds % 500 < 250) this.playerSprite.Texture = left1;
                else this.playerSprite.Texture = left2;
                isPressed = true;
                rememberidle = 0;
            }

            if (Keyboard.IsKeyPressed(Keyboard.Key.D) && map.iswalkable((playerSprite), new Vector2f(runningSpeed, 0))) 
            {
                playerPosition = new Vector2f(playerPosition.X + runningSpeed, playerPosition.Y);
                if (time.TotalTime.Milliseconds % 500 < 250) this.playerSprite.Texture = right1;
                else this.playerSprite.Texture = right2;
                isPressed = true;
                rememberidle = 1;
            }

            if (Keyboard.IsKeyPressed(Keyboard.Key.A) && map.iswalkable((playerSprite), new Vector2f(0, -runningSpeed))) 
            {
                playerPosition = new Vector2f(playerPosition.X, playerPosition.Y - runningSpeed);
                if (time.TotalTime.Milliseconds % 500 < 250) this.playerSprite.Texture = up1;
                else this.playerSprite.Texture = up2;
                isPressed = true;
                rememberidle = 2;
            }

            if (Keyboard.IsKeyPressed(Keyboard.Key.W) && map.iswalkable((playerSprite), new Vector2f(0, runningSpeed))) 
            {
                playerPosition = new Vector2f(playerPosition.X, playerPosition.Y + runningSpeed);
                if (time.TotalTime.Milliseconds % 500 < 250) this.playerSprite.Texture = down1;
                else this.playerSprite.Texture = down2;
                isPressed = true;
                rememberidle = 3;
            }

            if (isPressed == false)
            {
                switch (rememberidle)
                {
                    case 0: this.playerSprite.Texture = left3;
                        break;
                    case 1: this.playerSprite.Texture = right3;
                        break;
                    case 2: this.playerSprite.Texture = up3;
                        break;
                    case 3: this.playerSprite.Texture = down3;
                        break;
                    default: this.playerSprite.Texture = down3;
                        break;
                }
            }
	    break;



    case 18:

 if (Keyboard.IsKeyPressed(Keyboard.Key.W) && map.iswalkable((playerSprite), new Vector2f(-runningSpeed,0))) 
            {
                playerPosition = new Vector2f(playerPosition.X - runningSpeed, playerPosition.Y);
                if (time.TotalTime.Milliseconds % 500 < 250) this.playerSprite.Texture = left1;
                else this.playerSprite.Texture = left2;
                isPressed = true;
                rememberidle = 0;
            }

            if (Keyboard.IsKeyPressed(Keyboard.Key.A) && map.iswalkable((playerSprite), new Vector2f(runningSpeed, 0))) 
            {
                playerPosition = new Vector2f(playerPosition.X + runningSpeed, playerPosition.Y);
                if (time.TotalTime.Milliseconds % 500 < 250) this.playerSprite.Texture = right1;
                else this.playerSprite.Texture = right2;
                isPressed = true;
                rememberidle = 1;
            }

            if (Keyboard.IsKeyPressed(Keyboard.Key.S) && map.iswalkable((playerSprite), new Vector2f(0, -runningSpeed))) 
            {
                playerPosition = new Vector2f(playerPosition.X, playerPosition.Y - runningSpeed);
                if (time.TotalTime.Milliseconds % 500 < 250) this.playerSprite.Texture = up1;
                else this.playerSprite.Texture = up2;
                isPressed = true;
                rememberidle = 2;
            }

            if (Keyboard.IsKeyPressed(Keyboard.Key.D) && map.iswalkable((playerSprite), new Vector2f(0, runningSpeed))) 
            {
                playerPosition = new Vector2f(playerPosition.X, playerPosition.Y + runningSpeed);
                if (time.TotalTime.Milliseconds % 500 < 250) this.playerSprite.Texture = down1;
                else this.playerSprite.Texture = down2;
                isPressed = true;
                rememberidle = 3;
            }

            if (isPressed == false)
            {
                switch (rememberidle)
                {
                    case 0: this.playerSprite.Texture = left3;
                        break;
                    case 1: this.playerSprite.Texture = right3;
                        break;
                    case 2: this.playerSprite.Texture = up3;
                        break;
                    case 3: this.playerSprite.Texture = down3;
                        break;
                    default: this.playerSprite.Texture = down3;
                        break;
                }
            }
	    break;




    case 19:

 if (Keyboard.IsKeyPressed(Keyboard.Key.W) && map.iswalkable((playerSprite), new Vector2f(-runningSpeed,0))) 
            {
                playerPosition = new Vector2f(playerPosition.X - runningSpeed, playerPosition.Y);
                if (time.TotalTime.Milliseconds % 500 < 250) this.playerSprite.Texture = left1;
                else this.playerSprite.Texture = left2;
                isPressed = true;
                rememberidle = 0;
            }

            if (Keyboard.IsKeyPressed(Keyboard.Key.A) && map.iswalkable((playerSprite), new Vector2f(runningSpeed, 0))) 
            {
                playerPosition = new Vector2f(playerPosition.X + runningSpeed, playerPosition.Y);
                if (time.TotalTime.Milliseconds % 500 < 250) this.playerSprite.Texture = right1;
                else this.playerSprite.Texture = right2;
                isPressed = true;
                rememberidle = 1;
            }

            if (Keyboard.IsKeyPressed(Keyboard.Key.D) && map.iswalkable((playerSprite), new Vector2f(0, -runningSpeed))) 
            {
                playerPosition = new Vector2f(playerPosition.X, playerPosition.Y - runningSpeed);
                if (time.TotalTime.Milliseconds % 500 < 250) this.playerSprite.Texture = up1;
                else this.playerSprite.Texture = up2;
                isPressed = true;
                rememberidle = 2;
            }

            if (Keyboard.IsKeyPressed(Keyboard.Key.S) && map.iswalkable((playerSprite), new Vector2f(0, runningSpeed))) 
            {
                playerPosition = new Vector2f(playerPosition.X, playerPosition.Y + runningSpeed);
                if (time.TotalTime.Milliseconds % 500 < 250) this.playerSprite.Texture = down1;
                else this.playerSprite.Texture = down2;
                isPressed = true;
                rememberidle = 3;
            }

            if (isPressed == false)
            {
                switch (rememberidle)
                {
                    case 0: this.playerSprite.Texture = left3;
                        break;
                    case 1: this.playerSprite.Texture = right3;
                        break;
                    case 2: this.playerSprite.Texture = up3;
                        break;
                    case 3: this.playerSprite.Texture = down3;
                        break;
                    default: this.playerSprite.Texture = down3;
                        break;
                }
            }
	    break;




    case 20:

 if (Keyboard.IsKeyPressed(Keyboard.Key.W) && map.iswalkable((playerSprite), new Vector2f(-runningSpeed,0))) 
            {
                playerPosition = new Vector2f(playerPosition.X - runningSpeed, playerPosition.Y);
                if (time.TotalTime.Milliseconds % 500 < 250) this.playerSprite.Texture = left1;
                else this.playerSprite.Texture = left2;
                isPressed = true;
                rememberidle = 0;
            }

            if (Keyboard.IsKeyPressed(Keyboard.Key.S) && map.iswalkable((playerSprite), new Vector2f(runningSpeed, 0))) 
            {
                playerPosition = new Vector2f(playerPosition.X + runningSpeed, playerPosition.Y);
                if (time.TotalTime.Milliseconds % 500 < 250) this.playerSprite.Texture = right1;
                else this.playerSprite.Texture = right2;
                isPressed = true;
                rememberidle = 1;
            }

            if (Keyboard.IsKeyPressed(Keyboard.Key.A) && map.iswalkable((playerSprite), new Vector2f(0, -runningSpeed))) 
            {
                playerPosition = new Vector2f(playerPosition.X, playerPosition.Y - runningSpeed);
                if (time.TotalTime.Milliseconds % 500 < 250) this.playerSprite.Texture = up1;
                else this.playerSprite.Texture = up2;
                isPressed = true;
                rememberidle = 2;
            }

            if (Keyboard.IsKeyPressed(Keyboard.Key.D) && map.iswalkable((playerSprite), new Vector2f(0, runningSpeed))) 
            {
                playerPosition = new Vector2f(playerPosition.X, playerPosition.Y + runningSpeed);
                if (time.TotalTime.Milliseconds % 500 < 250) this.playerSprite.Texture = down1;
                else this.playerSprite.Texture = down2;
                isPressed = true;
                rememberidle = 3;
            }

            if (isPressed == false)
            {
                switch (rememberidle)
                {
                    case 0: this.playerSprite.Texture = left3;
                        break;
                    case 1: this.playerSprite.Texture = right3;
                        break;
                    case 2: this.playerSprite.Texture = up3;
                        break;
                    case 3: this.playerSprite.Texture = down3;
                        break;
                    default: this.playerSprite.Texture = down3;
                        break;
                }
            }
	    break;




    case 21:

 if (Keyboard.IsKeyPressed(Keyboard.Key.W) && map.iswalkable((playerSprite), new Vector2f(-runningSpeed,0))) 
            {
                playerPosition = new Vector2f(playerPosition.X - runningSpeed, playerPosition.Y);
                if (time.TotalTime.Milliseconds % 500 < 250) this.playerSprite.Texture = left1;
                else this.playerSprite.Texture = left2;
                isPressed = true;
                rememberidle = 0;
            }

            if (Keyboard.IsKeyPressed(Keyboard.Key.S) && map.iswalkable((playerSprite), new Vector2f(runningSpeed, 0))) 
            {
                playerPosition = new Vector2f(playerPosition.X + runningSpeed, playerPosition.Y);
                if (time.TotalTime.Milliseconds % 500 < 250) this.playerSprite.Texture = right1;
                else this.playerSprite.Texture = right2;
                isPressed = true;
                rememberidle = 1;
            }

            if (Keyboard.IsKeyPressed(Keyboard.Key.D) && map.iswalkable((playerSprite), new Vector2f(0, -runningSpeed))) 
            {
                playerPosition = new Vector2f(playerPosition.X, playerPosition.Y - runningSpeed);
                if (time.TotalTime.Milliseconds % 500 < 250) this.playerSprite.Texture = up1;
                else this.playerSprite.Texture = up2;
                isPressed = true;
                rememberidle = 2;
            }

            if (Keyboard.IsKeyPressed(Keyboard.Key.A) && map.iswalkable((playerSprite), new Vector2f(0, runningSpeed))) 
            {
                playerPosition = new Vector2f(playerPosition.X, playerPosition.Y + runningSpeed);
                if (time.TotalTime.Milliseconds % 500 < 250) this.playerSprite.Texture = down1;
                else this.playerSprite.Texture = down2;
                isPressed = true;
                rememberidle = 3;
            }

            if (isPressed == false)
            {
                switch (rememberidle)
                {
                    case 0: this.playerSprite.Texture = left3;
                        break;
                    case 1: this.playerSprite.Texture = right3;
                        break;
                    case 2: this.playerSprite.Texture = up3;
                        break;
                    case 3: this.playerSprite.Texture = down3;
                        break;
                    default: this.playerSprite.Texture = down3;
                        break;
                }
            }
	    break;





    case 22:


 if (Keyboard.IsKeyPressed(Keyboard.Key.W) && map.iswalkable((playerSprite), new Vector2f(-runningSpeed,0))) 
            {
                playerPosition = new Vector2f(playerPosition.X - runningSpeed, playerPosition.Y);
                if (time.TotalTime.Milliseconds % 500 < 250) this.playerSprite.Texture = left1;
                else this.playerSprite.Texture = left2;
                isPressed = true;
                rememberidle = 0;
            }

            if (Keyboard.IsKeyPressed(Keyboard.Key.D) && map.iswalkable((playerSprite), new Vector2f(runningSpeed, 0))) 
            {
                playerPosition = new Vector2f(playerPosition.X + runningSpeed, playerPosition.Y);
                if (time.TotalTime.Milliseconds % 500 < 250) this.playerSprite.Texture = right1;
                else this.playerSprite.Texture = right2;
                isPressed = true;
                rememberidle = 1;
            }

            if (Keyboard.IsKeyPressed(Keyboard.Key.A) && map.iswalkable((playerSprite), new Vector2f(0, -runningSpeed))) 
            {
                playerPosition = new Vector2f(playerPosition.X, playerPosition.Y - runningSpeed);
                if (time.TotalTime.Milliseconds % 500 < 250) this.playerSprite.Texture = up1;
                else this.playerSprite.Texture = up2;
                isPressed = true;
                rememberidle = 2;
            }

            if (Keyboard.IsKeyPressed(Keyboard.Key.S) && map.iswalkable((playerSprite), new Vector2f(0, runningSpeed))) 
            {
                playerPosition = new Vector2f(playerPosition.X, playerPosition.Y + runningSpeed);
                if (time.TotalTime.Milliseconds % 500 < 250) this.playerSprite.Texture = down1;
                else this.playerSprite.Texture = down2;
                isPressed = true;
                rememberidle = 3;
            }

            if (isPressed == false)
            {
                switch (rememberidle)
                {
                    case 0: this.playerSprite.Texture = left3;
                        break;
                    case 1: this.playerSprite.Texture = right3;
                        break;
                    case 2: this.playerSprite.Texture = up3;
                        break;
                    case 3: this.playerSprite.Texture = down3;
                        break;
                    default: this.playerSprite.Texture = down3;
                        break;
                }
            }
	    break;




    case 23:

 if (Keyboard.IsKeyPressed(Keyboard.Key.W) && map.iswalkable((playerSprite), new Vector2f(-runningSpeed,0))) 
            {
                playerPosition = new Vector2f(playerPosition.X - runningSpeed, playerPosition.Y);
                if (time.TotalTime.Milliseconds % 500 < 250) this.playerSprite.Texture = left1;
                else this.playerSprite.Texture = left2;
                isPressed = true;
                rememberidle = 0;
            }

            if (Keyboard.IsKeyPressed(Keyboard.Key.D) && map.iswalkable((playerSprite), new Vector2f(runningSpeed, 0))) 
            {
                playerPosition = new Vector2f(playerPosition.X + runningSpeed, playerPosition.Y);
                if (time.TotalTime.Milliseconds % 500 < 250) this.playerSprite.Texture = right1;
                else this.playerSprite.Texture = right2;
                isPressed = true;
                rememberidle = 1;
            }

            if (Keyboard.IsKeyPressed(Keyboard.Key.S) && map.iswalkable((playerSprite), new Vector2f(0, -runningSpeed))) 
            {
                playerPosition = new Vector2f(playerPosition.X, playerPosition.Y - runningSpeed);
                if (time.TotalTime.Milliseconds % 500 < 250) this.playerSprite.Texture = up1;
                else this.playerSprite.Texture = up2;
                isPressed = true;
                rememberidle = 2;
            }

            if (Keyboard.IsKeyPressed(Keyboard.Key.A) && map.iswalkable((playerSprite), new Vector2f(0, runningSpeed))) 
            {
                playerPosition = new Vector2f(playerPosition.X, playerPosition.Y + runningSpeed);
                if (time.TotalTime.Milliseconds % 500 < 250) this.playerSprite.Texture = down1;
                else this.playerSprite.Texture = down2;
                isPressed = true;
                rememberidle = 3;
            }

            if (isPressed == false)
            {
                switch (rememberidle)
                {
                    case 0: this.playerSprite.Texture = left3;
                        break;
                    case 1: this.playerSprite.Texture = right3;
                        break;
                    case 2: this.playerSprite.Texture = up3;
                        break;
                    case 3: this.playerSprite.Texture = down3;
                        break;
                    default: this.playerSprite.Texture = down3;
                        break;
                }
            }
	    break;

}


}
    }
}
