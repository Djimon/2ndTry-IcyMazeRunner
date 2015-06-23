using SFML.Graphics;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IcyMazeRunner.Klassen.Gameplay
{
    class Enemy : GameObject
    {
        Vector2f position;
        Sprite sprite;

        public Enemy(Vector2f _position, string texturePath)
        {
            position = _position;

            Texture txEnemy = new Texture(texturePath);
            sprite = new Sprite(txEnemy);
        }

        // Pathfinder
        public void move(Vector2f PlayerPosition, Map mapInfo) //andere Bewegungsmuster (e.g. Pathfinder)
        {
            // TODO: your code here ;)
        }


        public void move(Vector2f PlayerPosition)  //direkter weg zum player
        {
            Vector2f direction = PlayerPosition - position;
            float length = (float)Math.Sqrt(direction.X * direction.X + direction.Y * direction.Y);
            position += direction / (length * 5);
        }

        //public override void update(GameTime gametime)
        //{
            
        //    return;
        //}


        public override void draw(RenderWindow win)
        {
            win.Draw(sprite);
        }

        
    }
}
