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
        //ToDo: Wenn letztendlich genutzt, Summaries einfügen

       
        /// <summary>
        /// Der Sprite des Gegners.
        /// </summary>
        Sprite sp_Enemy;

        /// <summary>
        /// Konstruktor
        /// </summary>
        public Enemy(Vector2f _position, string texturePath)
        {
            Texture txEnemy = new Texture(texturePath);
            sp_Enemy = new Sprite(txEnemy);
            sp_Enemy.Position = _position;
        }

        // Pathfinder
        /// <summary>
        /// Findet einen Pfad zum Spieler und lässt Gegner entlang diesen bewegen.
        /// </summary>
        public void move(Vector2f spPlayer.Position, Map mapInfo) //andere Bewegungsmuster (e.g. Pathfinder)
        {
            // TODO: your code here ;)
        }

        /// <summary>
        /// Gegner bewegt sich direkt in Richtung des Spielers.
        /// </summary>
        public void move(Vector2f spPlayer.Position) 
        {
            Vector2f direction = spPlayer.Position - position;
            float length = (float)Math.Sqrt(direction.X * direction.X + direction.Y * direction.Y);
            sp_Enemy.Position += direction / (length * 5);
        }

        /// <summary>
        /// Aktualisiert den Gegner.
        /// </summary>
        public override void update(GameTime gametime)

        {

            return;
        }
        // ToDo: Update

        /// <summary>
        /// Zéichnet den Gegner.
        /// </summary>
        public override void draw(RenderWindow win)

        {
            
            return;
        }
        // ToDo: Draw fixen.

        //public override void draw(RenderWindow win)
        //{
        //    sprite.Position = position;
        //    win.Draw(sprite);
        //}

        
    }
}
