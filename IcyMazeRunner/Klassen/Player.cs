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
        Sprite[] playerSprite;
        Texture[] playerTexture;

        public Player()
        {
            playerPosition = new Vector2f(0, 0); //Wert anpassen
            playerSprite = new Sprite[12];
            
            playerTexture = new Texture[12];
            playerTexture[0] = new Texture("Texturen/Player/downidle.png");
            playerTexture[1] = new Texture("Texturen/Player/down1.png");
            playerTexture[2] = new Texture("Texturen/Player/down2.png");
            playerTexture[3] = new Texture("Texturen/Player/upidle.png");
            playerTexture[4] = new Texture("Texturen/Player/up1.png");
            playerTexture[5] = new Texture("Texturen/Player/up2.png");
            playerTexture[6] = new Texture("Texturen/Player/leftidle.png");
            playerTexture[7] = new Texture("Texturen/Player/left1.png");
            playerTexture[8] = new Texture("Texturen/Player/left2.png");
            playerTexture[9] = new Texture("Texturen/Player/rightidle.png");
            playerTexture[10] = new Texture("Texturen/Player/right1.png");
            playerTexture[11] = new Texture("Texturen/Player/right2.png");

            for (int index = 0; index < playerTexture.Length; index++)
            {
                playerSprite[index] = new Sprite(playerTexture[index]); // eventuell Arrays anpassen je nach Richtung
                playerSprite[index].Scale = new Vector2f(1f, 1f); //Skalierung anpassen
                playerSprite[index].Position = playerPosition; 
            }
            
                   
        }

        public void move(Map map, GameTime time)
        {
            float runningSpeed = 0.1f * time.ElapsedTime.Milliseconds;

            if (map.walkable() && Keyboard.IsKeyPressed(Keyboard.Key.A))
            {
                playerPosition = new Vector2f(playerPosition.X - runningSpeed, playerPosition.Y);
            }
            if (map.walkable() && Keyboard.IsKeyPressed(Keyboard.Key.D))
            {
                playerPosition = new Vector2f(playerPosition.X + runningSpeed, playerPosition.Y);
            }
            if (map.walkable() && Keyboard.IsKeyPressed(Keyboard.Key.W))
            {
                playerPosition = new Vector2f(playerPosition.X, playerPosition.Y - runningSpeed);
            }
            if (map.walkable() && Keyboard.IsKeyPressed(Keyboard.Key.S))
            {
                playerPosition = new Vector2f(playerPosition.X, playerPosition.Y - runningSpeed);
            }

        }
        public void draw(RenderWindow win)
        {
            win.Draw(playerSprite[0]); // Element anpassen
        }
    }
}
