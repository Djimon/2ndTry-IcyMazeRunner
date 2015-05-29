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

        Vector2f Position;
        Sprite spSprite{ get; set; }
        Texture txCurrentTexture { get { return spSprite.Texture; } }
        float F_scale;
        public virtual String S_type { get { return "Object"; } }
        protected bool B_IsAlive_ = true;
        public bool B_isAlive { get { return B_IsAlive_; } set { B_IsAlive_ = value; } }
        public int I_indexObjectList { get; set; }
        public virtual bool B_walkable { get { return false; } }
        public bool B_isVisible { get { return true; } } //standard is visible

        public void kill()
        {
            B_isAlive = false;
        }

        public abstract void update(GameTime gameTime);

        public void draw(RenderWindow win)
        {
            win.Draw(spSprite);
        }

    }
}
