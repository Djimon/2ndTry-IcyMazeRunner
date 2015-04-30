using SFML.Window;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IcyMazeRunner.Klassen.Gamestates_und_Gamestruktur.GUI
{
    class Kompass //Kompass
    {

        System.Drawing.Image needle = new Bitmap("Texturen/Menü+Anzeige/GUI/Untitled-1.png");
        Vector2f vCompass = new Vector2f(1, 1); //Kompassmittelpunkt
        Vector2f vTarget = new Vector2f(100, 100); // Zielobjekt

        public Vector2f getVector(Vector2f a, Vector2f b)
        {
            return (b - a);
        }

        public float getWinkel(Vector2f position)
        {
            float x = position.X;
            float y = position.Y;
            return (float)(Math.Atan2(y, x) * 180 / Math.PI);  // drehugnswinkel in (radian) zum zielvector(y-wert,x-wert)
        }


        // grafik rotieren via Winkel      
        // name="oldBitmap"  >Bitmap grafik
        // name="angle"  >winkel in Gradmaß

        private static Bitmap RotateImageByAngle(System.Drawing.Image oldBitmap, float angle)
        {
            var newBitmap = new Bitmap(oldBitmap.Width, oldBitmap.Height);
            newBitmap.SetResolution(oldBitmap.HorizontalResolution, oldBitmap.VerticalResolution);
            var graphics = Graphics.FromImage(newBitmap);
            graphics.TranslateTransform((float)oldBitmap.Width / 2, (float)oldBitmap.Height / 2);
            graphics.RotateTransform(angle);
            graphics.TranslateTransform(-(float)oldBitmap.Width / 2, -(float)oldBitmap.Height / 2);
            graphics.DrawImage(oldBitmap, new Point(0, 0));
            return newBitmap;
        }

        public void update()
        {
            if (getWinkel(getVector(vCompass, vTarget)) != 0)
            {
                RotateImageByAngle(needle, getWinkel(getVector(vCompass, vTarget)));
            }
        }
         //  !!!~~~ DRAW ~~~!!!
 

    }
}
