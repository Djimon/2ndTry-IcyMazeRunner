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
        Texture playerTexture;

        public Player()
        {
            playerTexture = new Texture("/Texturen/Player/Platzhalter.png"); //richtige Bild einfügen in Ordner und Pfad
            playerSprite = new Sprite(playerTexture);
            playerSprite.Scale = new Vector2f (1f, 1f); //Skalierung anpassen

            playerPosition = new Vector2f(0, 0); //Wert anpassen
            playerSprite.Position = playerPosition;        
        }

        public void move(){
            // moveUp, moveDown, moveRight, moveLeft
        }

        public void draw(RenderWindow Window)
        {
            Window.Draw(playerSprite);
        }
    }
}
