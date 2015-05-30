﻿using SFML.Graphics;
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
       
        System.Drawing.Image needle;
        Sprite spNeedle;
        Sprite spnew;
        Vector2f vCompass; //Kompassmittelpunkt
        Vector2f vTarget; // Zielobjekt


        // Konstruktor
        public Kompass(Vector2f midpoint, Vector2f targetPos, Texture texturPfad)
        {
            this.spNeedle = new Sprite(new Texture(texturPfad));
            this.vCompass = midpoint;
            this.vTarget = targetPos;
        }

        public Vector2f getVector(Vector2f a, Vector2f b)
        {
            return (b - a);
        }

        public float getWinkel(Vector2f position)
        {
            float x = position.X;
            float y = position.Y;
            return (float)(Math.Atan2(y, x) * 180 / Math.PI);  // drehugnswinkel (in grad *180/PI) zum zielvector(y-wert,x-wert)
        }


        // grafik rotieren via Winkel      
        // name="oldBitmap"  >Bitmap grafik
        // name="angle"  >winkel in Gradmaß

        //private static Bitmap RotateImageByAngle(System.Drawing.Image oldBitmap, float angle)
        //{
        //    var newBitmap = new Bitmap(oldBitmap.Width, oldBitmap.Height);
        //    newBitmap.SetResolution(oldBitmap.HorizontalResolution, oldBitmap.VerticalResolution);
        //    var graphics = Graphics.FromImage(newBitmap);
        //    graphics.TranslateTransform((float)oldBitmap.Width / 2, (float)oldBitmap.Height / 2);
        //    graphics.RotateTransform(angle);
        //    graphics.TranslateTransform(-(float)oldBitmap.Width / 2, -(float)oldBitmap.Height / 2);
        //    graphics.DrawImage(oldBitmap, new Point(0, 0));
        //    return newBitmap;
        //}

        static Sprite RotateImageByAngle(Sprite spPic, float angle)
        {
            //Sprite spOldPic = spPic;
            Sprite spNewPic = new Sprite(spPic);
            spNewPic.Rotation = angle;
            

            return spNewPic;
        }


        public void update()
        {
            if (getWinkel(getVector(vCompass, vTarget)) != 0)
            {
                Sprite spnew = RotateImageByAngle(spNeedle, getWinkel(getVector(vCompass, vTarget)));
            }
        }

        public void draw(RenderWindow win) 
        {
             win.Draw(spnew);
        }

    }
}