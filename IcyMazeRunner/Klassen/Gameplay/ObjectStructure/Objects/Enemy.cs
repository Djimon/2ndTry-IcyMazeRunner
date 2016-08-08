using SFML.Graphics;
using SFML.System;
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

        public int I_HealthPoints { get; private set; }

        float evadeChance { get; set; }
        /// <summary>
        /// Konstruktor
        /// </summary>
        public Enemy(Vector2f _position, string texturePath)
        {
            Texture txEnemy = new Texture(texturePath);
            sp_Enemy = new Sprite(txEnemy);
            sp_Enemy.Position = _position;
            I_HealthPoints = Game.I_level * 25; // für die verschiedenen Gegnertypen anpassen, Wert nur genommen, um int zu 
            // initialisieren, um Methode SetDamage zu schreiben.
        }

        /// <summary>
        /// Fügt dem Gegner Schaden zu. Er hat eine 20%-ige Chance, dem Angriff auszuweichen. 
        /// Wenn evadeChance <=0,2f ist, passiert nichts.
        /// </summary>
        public void SetDamage(int _damage)
        {
            evadeChance = (float)Game.random.NextDouble();
            if (evadeChance > 0.2f)
            {
                I_HealthPoints -= _damage;

                if (I_HealthPoints <= 0) B_isAlive = false;
                // ToDo: wenn nicht alive, muss Enemy im Enemyhandler aus Liste entfernt werden.
            }
        }


        // Pathfinder
        /// <summary>
        /// Findet einen Pfad zum Spieler und lässt Gegner entlang diesen bewegen.
        /// </summary>
        public void move(Vector2f Playerposition, Map mapInfo) //andere Bewegungsmuster (e.g. Pathfinder)
        {
            // TODO: your code here ;)
        }

        /// <summary>
        /// Gegner bewegt sich direkt in Richtung des Spielers.
        /// </summary>
        public void move(Vector2f Playerposition) 
        {
            Vector2f direction = Playerposition - sp_Enemy.Position;
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
        /// Zeichnet den Gegner.
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
