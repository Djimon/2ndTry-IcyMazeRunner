using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IcyMazeRunner.Klassen.Gamestates_und_Gamestruktur.GUI
{
    class GUI
    {
        Queue<Sprite> cachedSprites = new Queue<Sprite>();
        RenderWindow win;
        View view;

        public GUI()
        {
    
        }

        public void draw(Sprite sprite, RenderWindow win, View view)
        {
            this.win = win;
            this.view = view;

            // work on a copy, instead of the original, for the original could be reused outside this scope
            Sprite spriteCopy = new Sprite(sprite);  

            // modify sprite, to fit it in the gui
            float viewScale = (float)view.Size.X / win.Size.X;

            spriteCopy.Scale *= viewScale;
            spriteCopy.Position = view.Center - view.Size / 2F + spriteCopy.Position * viewScale;

            // draw the sprite
            win.Draw(spriteCopy);
        }
    }
}