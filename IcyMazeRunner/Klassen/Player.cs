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
        Sprite playerSprite;
        Texture down1 = new Texture("Texturen/Player/down1.png");
        Texture down2 = new Texture("Texturen/Player/down2.png");
        Texture down3 = new Texture("Texturen/Player/downidle.png");
        //Texture up1 = new Texture("Texturen/Player/up1.png");
        //Texture up2 = new Texture("Texturen/Player/up2.png");
        //Texture up3 = new Texture("Texturen/Player/upidle.png");
        Texture right1 = new Texture("Texturen/Player/right1.png");
        Texture right2 = new Texture("Texturen/Player/right2.png");
        Texture right3 = new Texture("Texturen/Player/rightidle.png");
        Texture left1 = new Texture("Texturen/Player/left1.png");
        Texture left2 = new Texture("Texturen/Player/left2.png");
        Texture left3 = new Texture("Texturen/Player/leftidle.png");

        public Player()
        {
           
            playerPosition = new Vector2f(0, 0); //Wert anpassen
            playerSprite = new Sprite(down3);
            


           
            
                playerSprite = new Sprite(playerSprite); 
                playerSprite.Scale = new Vector2f(1f, 1f); //Skalierung anpassen
                playerSprite.Position = playerPosition; 
            
            
                   
        }

        public void move(Map map, GameTime time)
        {
            float runningSpeed = 0.1f * time.ElapsedTime.Milliseconds;

            if (/*map.walkable() &&*/ Keyboard.IsKeyPressed(Keyboard.Key.A))
            {
                playerPosition = new Vector2f(playerPosition.X - runningSpeed, playerPosition.Y);
                
            }
            if (/*map.walkable() &&*/ Keyboard.IsKeyPressed(Keyboard.Key.D))
            {
                playerPosition = new Vector2f(playerPosition.X + runningSpeed, playerPosition.Y);
            }
            if (/*map.walkable() &&*/ Keyboard.IsKeyPressed(Keyboard.Key.W))
            {
                playerPosition = new Vector2f(playerPosition.X, playerPosition.Y - runningSpeed);
            }
            if (/*map.walkable() &&*/ Keyboard.IsKeyPressed(Keyboard.Key.S))
            {
                playerPosition = new Vector2f(playerPosition.X, playerPosition.Y - runningSpeed);
            }

        }
        public void draw(RenderWindow win)
        {
            win.Draw(playerSprite); // Element anpassen
        }
    }
}
