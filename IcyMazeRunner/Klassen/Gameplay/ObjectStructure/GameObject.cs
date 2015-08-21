using SFML.Graphics;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IcyMazeRunner.Klassen
{
    public abstract class GameObject
    {

        /// <summary>
        /// Gibt die Position des GameObjects an.
        /// </summary>
        Vector2f Position;
        // ToDo: benötigt? da in sprite.position schon enthalten, siehe Spieler.

        /// <summary>
        /// Der Sprite des GameObjects.
        /// </summary>
        Sprite spSprite{ get; set; }
        // ToDo: Name anpassen?

        /// <summary>
        /// Aktuelle Textur des Sprites.
        /// </summary>
        Texture txCurrentTexture { get { return spSprite.Texture; } }

        /// <summary>
        /// Skalierender Float.
        /// </summary>
        float F_scale;

        /// <summary>
        /// Gibt Typen des GameObjects an.
        /// </summary>
        public virtual String S_type { get { return "Object"; } }

        /// <summary>
        /// GameObject existiert noch im Spiel.
        /// </summary>
        protected bool B_IsAlive_ = true;

        /// <summary>
        /// GameObject existiert noch im Spiel.
        /// </summary>
        public bool B_isAlive { get { return B_IsAlive_; } set { B_IsAlive_ = value; } }
        // ToDo: Unterschied isAlive und IsAlive...?

        /// <summary>
        /// Gibt Index des GameObjects in der GameObjectList an.
        /// </summary>
        public int I_indexObjectList { get; set; }

        /// <summary>
        /// Gibt an, ob GameObject walkable ist, oder nicht.
        /// </summary>
        public virtual bool B_walkable { get { return false; } }

        /// <summary>
        /// Ist GameObject sichtbar oder unsichtbar? Standard ist sichtbar.
        /// </summary>
        public bool B_isVisible { get { return true; } } //standard is visible

        /// <summary>
        /// Setzt B_isAlive auf falsch, sodass GameObject beim nächsten Update-Durchlauf aus Liste gelöscht wird und damit keine Referenzen auf
        /// dieses GameObject mehr existieren.
        /// </summary>
        public void kill()
        {
            B_isAlive = false;
        }

        /// <summary>
        /// Nicht näher sequenzierte Methode zur Aktualisierung von GameObjects.
        /// </summary>
        public abstract void update(GameTime gameTime);

        /// <summary>
        /// Zeichnet GameObject.
        /// </summary>
        public void draw(RenderWindow win)
        {
            win.Draw(spSprite);
        }

    }
}
