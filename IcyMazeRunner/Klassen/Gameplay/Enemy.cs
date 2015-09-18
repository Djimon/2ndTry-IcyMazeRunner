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
        /// Gibt die Position des Gegners an.
        /// </summary>
        Vector2f position;
        // ToDo: position benötigt oder über Sprite.Position regeln? siehe player.Position

        /// <summary>
        /// Der Sprite des Gegners.
        /// </summary>
        Sprite sprite;
        // ToDo: Name anpassen?

        /// <summary>
        /// Konstruktor
        /// </summary>
        public Enemy(Vector2f _position, string texturePath)
        {
            position = _position;

            Texture txEnemy = new Texture(texturePath);
            sprite = new Sprite(txEnemy);
        }

        // Pathfinder
        /// <summary>
        /// Findet einen Pfad zum Spieler und lässt Gegner entlang diesen bewegen.
        /// </summary>
        public void move(Vector2f PlayerPosition, Map mapInfo) //andere Bewegungsmuster (e.g. Pathfinder)
        {
            // TODO: your code here ;)
        }

        /// <summary>
        /// Gegner bewegt sich direkt in Richtung des Spielers.
        /// </summary>
        public void move(Vector2f PlayerPosition) 
        {
            Vector2f direction = PlayerPosition - position;
            float length = (float)Math.Sqrt(direction.X * direction.X + direction.Y * direction.Y);
            position += direction / (length * 5);
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
        public void draw(RenderWindow win)

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
