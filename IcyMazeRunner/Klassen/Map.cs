using SFML.Graphics;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IcyMazeRunner.Klassen
{
    class Map
    {
        // insert Map-Code here...es folgt eine User-friendly Zuarbeit zum einfachen Copy&Pasten :))
        // Texturpfad: "Texturen/Map/Map_tutorial.png"
        // Rot "ff0000" = leer (Hintergrund/Platzhalter, Landschaft, Atomexplosion) 
        // Texturpfad: "Texturen/Map/BG.jpg"
        // schwarz "000000" = Mauer
        // Texturpfad: "Texturen/Map/wall-clean.png"
        // weiß "ffffff" = weg 
        // Texturpfad: "Texturen/Map/way-clean.png"

        Blocks[,] map;
        Texture blockTex;
        
        


        public static String white = "ffffffff"; //walkable Hauptweg
        public static String black = "ff000000"; //alphamauer
        public static String red = "ffff0000"; // Hintergrund
        public static String green = "ff00ff00"; //später mehr mauerteile
        public static String blue = "ff0000ff"; //später mehr Wegtypen
        //public static String 
        //public static String
        //public static String
        //public static String
        //public static String

        public Map(Bitmap mask)
        {
            map = new Blocks[mask.Width, mask.Height];

            for (int row = 0; row < map.GetLength(0); row++)
            {
                for (int col = 0; col < map.GetLength(1); col++)
                {
                    blockTex = new Texture("Texturen/Map/way-clean.png");

                    if (mask.GetPixel(row, col).Name == white)
                        map[row, col] = new Blocks(0, new Vector2f(row*90, col*90), blockTex);

                    if (mask.GetPixel(row, col).Name == black)
                        map[row, col] = new Blocks(1, new Vector2f(row*90, col*90), blockTex);

                    if (mask.GetPixel(row, col).Name == red)
                        map[row, col] = new Blocks(2, new Vector2f(row*90, col*90), blockTex);
                    //if (mask.GetPixel(row, col).Name == blue)
                    //    map[row, col] = new Blocks(3, new Vector2f(row*90, col*90), blockTex);
                    //if (mask.GetPixel(row, col).Name == red)
                    //    map[row, col] = new Blocks(4, new Vector2f(row*90, col*90), blockTex);


                }

            }


        }

        //public bool walkable
        //{

        //   (fritz.getWalkable) return true;
        //    else  return false;
        //}

        public void draw(RenderWindow win)
        {

            for (int i = 0; i < map.GetLength(0); i++)
            {
                for (int j = 0; j < map.GetLength(1); j++)
                {
                    map[i, j].draw(win);
                }
            }
        }








    }
}
