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
        bool walkable;
        int blocksize=90;
        


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
                    {
                        map[row, col] = new Blocks(0, new Vector2f(row * 90, col * 90), blockTex);
                        walkable = true;
                    }

                    if (mask.GetPixel(row, col).Name == black)
                    {
                        map[row, col] = new Blocks(1, new Vector2f(row * 90, col * 90), blockTex);
                        walkable = false;
                    }

                    if (mask.GetPixel(row, col).Name == red)
                    {
                        map[row, col] = new Blocks(2, new Vector2f(row * 90, col * 90), blockTex);
                        walkable = false;
                    }

                    if (mask.GetPixel(row, col).Name == green)
                    {
                        map[row, col] = new Blocks(3, new Vector2f(row * 90, col * 90), blockTex);
                        walkable = true;
                    }

                    if (mask.GetPixel(row, col).Name == blue)
                    {
                        map[row, col] = new Blocks(4, new Vector2f(row * 90, col * 90), blockTex);
                        walkable = true;
                    }


                }

            }


        }


        public bool iswalkable(Sprite sprite, Vector2f vector)
        {
            bool walkable = true;
            Vector2f newPosition = new Vector2f(sprite.Position.X + vector.X, sprite.Position.Y + vector.Y);


            // Verschlankung der if-Abfrage

            if (!(map[(int)(newPosition.X / blocksize), (int)(newPosition.Y / blocksize)].getWalkable()/*links oben*/
              && map[(int)(newPosition.X / blocksize), (int)((newPosition.Y + sprite.Texture.Size.Y) / blocksize)].getWalkable()/*links unten*/
                && map[(int)newPosition.X / blocksize, (int)((newPosition.Y + (sprite.Texture.Size.Y / 2)) / blocksize)].getWalkable()/*links mitte*/
                && map[(int)((newPosition.X + sprite.Texture.Size.X) / blocksize), (int)(newPosition.Y / blocksize)].getWalkable()/*rechts oben*/
                && map[(int)((newPosition.X + sprite.Texture.Size.X) / blocksize), (int)((newPosition.Y + sprite.Texture.Size.Y) / blocksize)].getWalkable()/*rechts unten*/
                && map[(int)((newPosition.X + sprite.Texture.Size.X) / blocksize), (int)((newPosition.Y + (sprite.Texture.Size.Y / 2)) / blocksize)].getWalkable()/*rechts mitte*/
                && map[(int)((newPosition.X + (sprite.Texture.Size.X / 2)) / blocksize), (int)(newPosition.Y) / blocksize].getWalkable()/*oben mitte*/
                && map[(int)((newPosition.X + (sprite.Texture.Size.X / 2)) / blocksize), (int)(newPosition.Y + sprite.Texture.Size.Y) / blocksize].getWalkable()/*unten mitte*/
                ))
                walkable = false;

            Console.WriteLine(walkable);

           return walkable;
           
        }


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
