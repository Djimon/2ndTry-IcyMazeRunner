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
        // werden diese Kommentare noch benötigt? Eventuell ansonsten neu ordnen
        // insert Map-Code here...es folgt eine User-friendly Zuarbeit zum einfachen Copy&Pasten :))
        // Texturpfad: "Texturen/Map/Map_tutorial.png"
        // Rot "ff0000" = leer (Hintergrund/Platzhalter, Landschaft, Atomexplosion) 
        // Texturpfad: "Texturen/Map/BG.jpg"
        // schwarz "000000" = Mauer
        // Texturpfad: "Texturen/Map/wall-clean.png"
        // weiß "ffffff" = weg 
        // Texturpfad: "Texturen/Map/way-clean.png"
        // orange "FF8000" = Loch im Boden (255 Rot, 128 Grün, 0 Blau)

        Blocks[,] map;
        Texture txBlock;

        /* ~~~~ Draw ~~~~ */
        private bool walkable;
        /* ~~~~ Blockgröße, um sie zentral ändern zu können ~~~~ */
        private int blocksize=90; //eventuell für Höhe und Breite der Blöcke unterscheiden, da der sichtbare Teil der Spielertextur nicht so breit wie hoch ist

        
        /* ~~~~ Strings, um Bitmapfarbe einem Blocktyp zuzuordnen ~~~~ */
        public static String white = "ffffffff"; //walkable Hauptweg
        public static String black = "ff000000"; //mauervert
        public static String red = "ffff0000"; // Hintergrund
        public static String green = "ff00ff00"; //später mehr mauerteile
        public static String blue = "ff0000ff"; //später mehr Wegtypen
        public static String grey = "ff414141"; //mauer hor
        public static String orange = "ffff8000"; // Loch im Boden
        //public static String 
        //public static String
        //public static String
        //public static String
        //public static String


        /* ~~~~ Erstellen der Map mithilfe der Bitmap ~~~~ */
        public Map(Bitmap mask)
        {
            map = new Blocks[mask.Width, mask.Height];

            for (int row = 0; row < map.GetLength(0); row++)
            {
                for (int col = 0; col < map.GetLength(1); col++)
                {
                    txBlock = new Texture("Texturen/Map/way-clean.png");

                    if (mask.GetPixel(row, col).Name == white)
                    {
                        map[row, col] = new Blocks(0, new Vector2f(row * 90, col * 90), txBlock);
                        walkable = true;
                    }

                    if (mask.GetPixel(row, col).Name == black)
                    {
                        map[row, col] = new Blocks(5, new Vector2f(row * 90, col * 90), txBlock);
                        walkable = false;
                    }


                    //mauertest
                    if (mask.GetPixel(row, col).Name == grey)
                    {
                        map[row, col] = new Blocks(1, new Vector2f(row * 90, col * 90), txBlock);
                        walkable = false;
                    }



                    if (mask.GetPixel(row, col).Name == red)
                    {
                        map[row, col] = new Blocks(2, new Vector2f(row * 90, col * 90), txBlock);
                        walkable = false;
                    }

                    if (mask.GetPixel(row, col).Name == green)
                    {
                        map[row, col] = new Blocks(3, new Vector2f(row * 90, col * 90), txBlock);
                        walkable = true;
                    }

                    if (mask.GetPixel(row, col).Name == blue)
                    {
                        map[row, col] = new Blocks(4, new Vector2f(row * 90, col * 90), txBlock);
                        walkable = true;
                    }

                    if (mask.GetPixel(row, col).Name == orange)
                    {
                        map[row, col] = new Blocks(1, new Vector2f(row * 90, col * 90), txBlock);
                        walkable = true;
                    }


                }

            }


        }


        /* ~~~~ Getter ~~~~ */
        public int getBlocksize()
        {
            return blocksize;
        }


        // wird Setter benötigt?
        public void setBlocksize(int size)
        {
            blocksize=size;
        }


        /* ~~~~ Kontrolle, wo Spieler laufen kann ~~~~ */
        public bool iswalkable(Sprite sprite, Vector2f vector)
        {
            // walkable wird auf Standardbool gesetzt
            bool walkable = true;
            // zu vergleichende Position, also wo Spieler im nächsten Schritt stehen würde
            Vector2f newPosition = new Vector2f(sprite.Position.X + vector.X, sprite.Position.Y + vector.Y);


            // Verschlankung der if-Abfrage?
            // eigentliche Kollisionsabfrage, wenn Kollision, dann kann man dort nicht laufen, falsch wird zurückgegeben
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

            //Kontrollangabe
            Console.WriteLine(walkable);

           return walkable;
           
        }


        /* ~~~~ Draw ~~~~ */
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
