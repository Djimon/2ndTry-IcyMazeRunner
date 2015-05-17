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
        private bool B_walkable;
        /* ~~~~ Blockgröße, um sie zentral ändern zu können ~~~~ */
        private int I_blockSize=90; //eventuell für Höhe und Breite der Blöcke unterscheiden, da der sichtbare Teil der Spielertextur nicht so breit wie hoch ist

        
        /* ~~~~ Strings, um Bitmapfarbe einem Blocktyp zuzuordnen ~~~~ */
        public static String Swhite = "ffffffff"; //walkable Hauptweg
        public static String Sblack = "ff000000"; //mauervert
        public static String Sred = "ffff0000"; // Hintergrund
        public static String Sgreen = "ff00ff00"; //später mehr mauerteile
        public static String Sblue = "ff0000ff"; //später mehr Wegtypen
        public static String Sgrey = "ff414141"; //mauer hor
        public static String Sorange = "ffff8800"; // Loch im Boden
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

                    if (mask.GetPixel(row, col).Name == Swhite)
                    {
                        map[row, col] = new Blocks(0, new Vector2f(row * 90, col * 90), txBlock);
                        B_walkable = true;
                    }

                    if (mask.GetPixel(row, col).Name == Sblack)
                    {
                        map[row, col] = new Blocks(5, new Vector2f(row * 90, col * 90), txBlock);
                        B_walkable = false;
                    }


                    //mauertest
                    if (mask.GetPixel(row, col).Name == Sgrey)
                    {
                        map[row, col] = new Blocks(1, new Vector2f(row * 90, col * 90), txBlock);
                        B_walkable = false;
                    }



                    if (mask.GetPixel(row, col).Name == Sred)
                    {
                        map[row, col] = new Blocks(2, new Vector2f(row * 90, col * 90), txBlock);
                        B_walkable = false;
                    }

                    if (mask.GetPixel(row, col).Name == Sgreen)
                    {
                        map[row, col] = new Blocks(3, new Vector2f(row * 90, col * 90), txBlock);
                        B_walkable = true;
                    }

                    if (mask.GetPixel(row, col).Name == Sblue)
                    {
                        map[row, col] = new Blocks(4, new Vector2f(row * 90, col * 90), txBlock);
                        B_walkable = true;
                    }

                    if (mask.GetPixel(row, col).Name == Sorange)
                    {
                        map[row, col] = new Blocks(6, new Vector2f(row * 90, col * 90), txBlock);
                        B_walkable = true;
                    }


                }

            }


        }


        /* ~~~~ Getter ~~~~ */
        public int getBlocksize()
        {
            return I_blockSize;
        }


        // wird Setter benötigt?
        public void setBlocksize(int size)
        {
            I_blockSize=size;
        }


        /* ~~~~ Kontrolle, wo Spieler laufen kann ~~~~ */
        public bool iswalkable(Sprite sprite, Vector2f vector)
        {
            // walkable wird auf Standardbool gesetzt
            bool B_walkable = true;
            // zu vergleichende Position, also wo Spieler im nächsten Schritt stehen würde
            Vector2f newPosition = new Vector2f(sprite.Position.X + vector.X, sprite.Position.Y + vector.Y);


            // Verschlankung der if-Abfrage?
            // eigentliche Kollisionsabfrage, wenn Kollision, dann kann man dort nicht laufen, falsch wird zurückgegeben
            if (!(map[(int)(newPosition.X / I_blockSize), (int)(newPosition.Y / I_blockSize)].getWalkable()/*links oben*/
              && map[(int)(newPosition.X / I_blockSize), (int)((newPosition.Y + sprite.Texture.Size.Y) / I_blockSize)].getWalkable()/*links unten*/
                && map[(int)newPosition.X / I_blockSize, (int)((newPosition.Y + (sprite.Texture.Size.Y / 2)) / I_blockSize)].getWalkable()/*links mitte*/
                && map[(int)((newPosition.X + sprite.Texture.Size.X) / I_blockSize), (int)(newPosition.Y / I_blockSize)].getWalkable()/*rechts oben*/
                && map[(int)((newPosition.X + sprite.Texture.Size.X) / I_blockSize), (int)((newPosition.Y + sprite.Texture.Size.Y) / I_blockSize)].getWalkable()/*rechts unten*/
                && map[(int)((newPosition.X + sprite.Texture.Size.X) / I_blockSize), (int)((newPosition.Y + (sprite.Texture.Size.Y / 2)) / I_blockSize)].getWalkable()/*rechts mitte*/
                && map[(int)((newPosition.X + (sprite.Texture.Size.X / 2)) / I_blockSize), (int)(newPosition.Y) / I_blockSize].getWalkable()/*oben mitte*/
                && map[(int)((newPosition.X + (sprite.Texture.Size.X / 2)) / I_blockSize), (int)(newPosition.Y + sprite.Texture.Size.Y) / I_blockSize].getWalkable()/*unten mitte*/
                ))
                B_walkable = false;

            //Kontrollangabe
            Console.WriteLine(B_walkable);

           return B_walkable;
           
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
