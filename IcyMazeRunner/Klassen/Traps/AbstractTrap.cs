using SFML.Graphics;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IcyMazeRunner.Klassen.Traps
{
    abstract class AbstractTrap
    {
        Sprite spTrap;
        Texture txTrap;

        public void collision(Sprite sprite)
        {

            // jeweils die Mitte der Texturen einberechnen. Aktuelle Implementierung sorgt dafür, dass immer mit der oberen linken Ecke gerechnet wird

            if (Math.Abs(sprite.Position.X - spTrap.Position.X) < 45 && Math.Abs(sprite.Position.Y - spTrap.Position.Y) < 45)
            {

            }

        }

        public void update()
        {
            
        }

        public void draw(RenderWindow win)
        {
            win.Draw(spTrap);
        }
    }
}
