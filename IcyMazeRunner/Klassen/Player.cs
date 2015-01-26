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

        Texture down1 = new Texture("Texturen/Player/down1.png");
        Texture down2 = new Texture("Texturen/Player/down2.png");
        Texture down3 = new Texture("Texturen/Player/downidle.png");
        Texture up1 = new Texture("Texturen/Player/up1.png");
        Texture up2 = new Texture("Texturen/Player/up2.png");
        Texture up3 = new Texture("Texturen/Player/upidle.png");
        Texture right1 = new Texture("Texturen/Player/right1.png");
        Texture right2 = new Texture("Texturen/Player/right2.png");
        Texture right3 = new Texture("Texturen/Player/rightidle.png");
        Texture left1 = new Texture("Texturen/Player/left1.png");
        Texture left2 = new Texture("Texturen/Player/left2.png");
        Texture left3 = new Texture("Texturen/Player/leftidle.png");

        bool isPressed = false;
        int rememberidle = 0;

        bool is_controlchange = false;
        int movechangerstate = 0;
        Random random = new Random();

        public Sprite getplayerSprite()
        {
            return this.playerSprite; 
        }

        public void setSpritePos(Vector2f pos)
        {
            playerSprite.Position = pos;
        }

        public Player(Vector2f Pos, Map map) 
        {
        /*wird mit neuem Level eine neue Position festgelegt*/
            playerPosition = Pos; 
           
            playerSprite = new Sprite(down3);

            playerSprite.Scale = new Vector2f(1f, 1f); 
            playerSprite.Position = playerPosition;
            playerSprite = new Sprite(playerSprite);

            absmap = map;
                   
            moveTime = new GameTime();
            moveTime.Watch.Start();
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

        public void update(GameTime time)
        {

            move(this.absmap, time);
        }


        public void move(Map map, GameTime time)
        {

            float runningSpeed = 0.5f * time.ElapsedTime.Milliseconds;
            isPressed = false;

            if (Keyboard.IsKeyPressed(Keyboard.Key.K))
            is_controlchange = true;

            if (Keyboard.IsKeyPressed(Keyboard.Key.L))
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


        public void draw(RenderWindow win)
        {
            win.Draw(playerSprite); // Element anpassen
        }

        public float getH { get; set; }


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
