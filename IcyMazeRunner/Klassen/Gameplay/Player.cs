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

        Vector2f playerPosition;      
        Map absmap;
        GameTime moveTime;
        Sprite playerSprite {get;set;}
        EGameStates gamestate;

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

        bool isPressed = false;
        int rememberidle = 0;

        bool is_controlchange = false;
        int movechangerstate = 0;
        Random random = new Random();

        public GameTime DeathWatch;
        bool DeathWatchIsOn;

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
        

        public Sprite getplayerSprite()
        {
            return this.playerSprite; 
        }

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


        public Player(Vector2f Pos, Map map) 
        {
        /*wird mit neuem Level eine neue Position festgelegt*/
            playerPosition = Pos;

            healthPoints = 100;
            
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

                Fall1a = new Texture("Texturen/Player/Fallanimation/fall1a Platzhalter.png");
                Fall1b = new Texture("Texturen/Player/Fallanimation/fall1b Platzhalter.png");
                Fall2a = new Texture("Texturen/Player/Fallanimation/fall2a Platzhalter.png");
                Fall2b = new Texture("Texturen/Player/Fallanimation/fall2b Platzhalter.png");
                Fall3a = new Texture("Texturen/Player/Fallanimation/fall3a Platzhalter.png");
                Fall3b = new Texture("Texturen/Player/Fallanimation/fall3b Platzhalter.png");
                Fall4a = new Texture("Texturen/Player/Fallanimation/fall4a Platzhalter.png");
                Fall4b = new Texture("Texturen/Player/Fallanimation/fall4b Platzhalter.png");
           
           
            playerSprite = new Sprite(down3);

            playerSprite.Scale = new Vector2f(1f, 1f); 
            playerSprite.Position = playerPosition;
            playerSprite = new Sprite(playerSprite);

            absmap = map;
                   
            moveTime = new GameTime();
            moveTime.Watch.Start();

            DeathWatch = new GameTime();
            DeathWatchIsOn = false;
        }


        /* ~~~~~~~~~~~~~~~~~~ Getter und Setter ~~~~~~~~~~~~~~*/


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


        /*~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~*/

        public void update(GameTime time)
        {

            move(this.absmap, time);
        }


        public void move(Map map, GameTime time)
        {

            runningSpeed = 0.5f * time.ElapsedTime.Milliseconds;
            isPressed = false;

            if (Keyboard.IsKeyPressed(Keyboard.Key.F2))
            is_controlchange = true;

            if (Keyboard.IsKeyPressed(Keyboard.Key.F1))
            is_controlchange = false;

            if (is_controlchange)
            changingmove(map, time);
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

        playerSprite.Position = playerPosition;


        }

        public void DeathAnimation(int typeOfDeath)
        {
            if (!DeathWatchIsOn)
            {
                DeathWatch.Watch.Start();
            }

            fallAnimationCounter++;

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


        public void draw(RenderWindow win)
        {
            win.Draw(playerSprite); // Element anpassen
        }

        public float getH { get; set; }   





        // LAAAAANNNNNGGGEEEE Methode für die 24 möglichen Zuweisungen der Tastatur im Hardmode bis zum Ende der Klasse

        public void changingmove(Map map, GameTime time) {

            float runningSpeed = 0.5f * time.ElapsedTime.Milliseconds;
            isPressed = false;

            if (moveTime.Watch.ElapsedMilliseconds >= 20000)
            {
                movechangerstate = random.Next(23);
                moveTime.Watch.Restart();
            }

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
