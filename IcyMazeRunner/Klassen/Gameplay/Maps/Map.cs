﻿using SFML.Graphics;
using SFML.System;
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
        /// <summary>
        /// Gibt Position des Ziels an.
        /// </summary>
        public Vector2f vZiel { get; set; }


        /// <summary>
        /// Gibt Startposition des players an.
        /// </summary>
        public Vector2f vStart { get; private set; }

        /// <summary>
        /// Block-Array zum Anlegen der Map.
        /// </summary>
        public Blocks[,] map;

        /// <summary>
        /// Blockgröße, um zentral einzustellen, welchen Wert sie hat.
        /// </summary>
        public int I_blockSize { get; set; }
        // ToDo: eventuell für Höhe und Breite der Blöcke unterscheiden, da der sichtbare Teil der Spielertextur nicht so breit wie hoch ist

        
        /* ~~~~ Strings, um Bitmapfarbe einem Blocktyp zuzuordnen ~~~~ */
        public static String Swhite = "ffffffff"; //Weg
        public static String Sblack = "ff000000"; //Mauer Vertikal
        public static String Sred = "ffff0000"; // Hintergrund
        public static String Sgreen = "ff00ff00"; //Start
        public static String Sblue = "ff0000ff"; //Ziel
        public static String Sgrey = "ff414141"; //Mauer Horizontal
        public static String Sorange = "ffff8800"; // Loch im Boden
        public static String Scyan = "ff008080"; // geheimer Weg Vorderansicht
        public static String Sdarkgreen = "ff004000"; // geheimer Weg Draufsicht


        /// <summary>
        ///  <para>Konstruktor der Map </para>
        /// 
        ///  <para>Übersicht der Namen, Werte, Kacheltypen und walkables:       </para>
        ///  <para>Swhite     = "ffffffff" Weg                        true      </para>
        ///  <para>Sblack     = "ff000000" Mauer Draufsicht           false     </para>
        ///  <para>Sgrey      = "ff414141" Mauer Vorderansicht        false     </para>
        ///  <para>Sred       = "ffff0000" Hintergrund                false     </para>
        ///  <para>Sblue      = "ff0000ff" Ziel                       true      </para>
        ///  <para>Sgreen     = "ff00ff00" Start                      true      </para>
        ///  <para>Sorange    = "ffff8800" Loch im Boden              true      </para>
        ///  <para>Scyan      = "ff008080" geheimer Weg Vorderansicht true      </para>
        ///  <para>Sdarkgreen = "ff004000" geheimer Weg Draufsicht    true      </para>
        ///  
        ///  <para>Sred ist Standardwert.                                       </para>
        /// </summary>
        public Map(Bitmap mask)
        {
            I_blockSize = 90;
            vZiel = new Vector2f(0, 0);

            map = new Blocks[mask.Width, mask.Height];

            for (int row = 0; row < map.GetLength(0); row++)
            {
                for (int col = 0; col < map.GetLength(1); col++)
                {
                   

                    if (mask.GetPixel(row, col).Name == Swhite)
                    {   
                        map[row, col] = new Blocks(0, new Vector2f(row * 90, col * 90));
                        
                    }

                    if (mask.GetPixel(row, col).Name == Sblack)
                    {   
                        map[row, col] = new Blocks(5, new Vector2f(row * 90, col * 90));
                        
                    }

                    if (mask.GetPixel(row, col).Name == Sgrey)
                    {   
                        map[row, col] = new Blocks(1, new Vector2f(row * 90, col * 90));
                       
                    }

                    if (mask.GetPixel(row, col).Name == Sred)
                    {   
                        map[row, col] = new Blocks(2, new Vector2f(row * 90, col * 90));
                        
                    }

                    if (mask.GetPixel(row, col).Name == Sgreen)
                    {  
                        map[row, col] = new Blocks(3, new Vector2f(row * 90, col * 90));
                        vStart = new Vector2f(row * 90+1, col * 90 + 1);
                        
                    }

                    if (mask.GetPixel(row, col).Name == Sblue)
                    {   
                        map[row, col] = new Blocks(4, new Vector2f(row * 90, col * 90));
                        vZiel = new Vector2f(row * 90 + 45, col * 90 + 45);
                        
                    }

                    if (mask.GetPixel(row, col).Name == Sorange)
                    {   
                        map[row, col] = new Blocks(6, new Vector2f(row * 90, col * 90));
                        
                    }

                    if (mask.GetPixel(row, col).Name == Scyan)
                    {   
                        map[row, col] = new Blocks(7, new Vector2f(row * 90, col * 90));
                        
                    }

                    if (mask.GetPixel(row, col).Name == Sdarkgreen)
                    {   
                        map[row, col] = new Blocks(8, new Vector2f(row * 90, col * 90));
                 
                    }

                    else
                    {
                        map[row, col] = new Blocks(2, new Vector2f(row * 90, col * 90));
                        
                    
                    }

                }

            }


        }

       /// <summary>
       /// <para>Kontrolliert, ob Kachel von Spieler betreten werden kann, oder nicht.</para>
       /// <para>B_walkable wird dabei zunächst auf Standardwert festgesetzt und die geplante Position des Objekts ermittelt.
       /// Anschließend wird für diese Position geprüft, ob die Stelle betretbar ist, oder nicht. Falls nicht, wird false
       /// zurückgegeben.</para>
       /// </summary>
        public bool iswalkable(Sprite sprite, Vector2f vector)
        {
            Vector2f newPosition = new Vector2f(sprite.Position.X + vector.X, sprite.Position.Y + vector.Y);

            if (!(map[(int)(newPosition.X / I_blockSize), (int)(newPosition.Y / I_blockSize)].getWalkable()/*links oben*/
              && map[(int)(newPosition.X / I_blockSize), (int)((newPosition.Y + sprite.Texture.Size.Y) / I_blockSize)].getWalkable()/*links unten*/
                && map[(int)newPosition.X / I_blockSize, (int)((newPosition.Y + (sprite.Texture.Size.Y / 2)) / I_blockSize)].getWalkable()/*links mitte*/
                && map[(int)((newPosition.X + sprite.Texture.Size.X) / I_blockSize), (int)(newPosition.Y / I_blockSize)].getWalkable()/*rechts oben*/
                && map[(int)((newPosition.X + sprite.Texture.Size.X) / I_blockSize), (int)((newPosition.Y + sprite.Texture.Size.Y) / I_blockSize)].getWalkable()/*rechts unten*/
                && map[(int)((newPosition.X + sprite.Texture.Size.X) / I_blockSize), (int)((newPosition.Y + (sprite.Texture.Size.Y / 2)) / I_blockSize)].getWalkable()/*rechts mitte*/
                && map[(int)((newPosition.X + (sprite.Texture.Size.X / 2)) / I_blockSize), (int)(newPosition.Y) / I_blockSize].getWalkable()/*oben mitte*/
                && map[(int)((newPosition.X + (sprite.Texture.Size.X / 2)) / I_blockSize), (int)(newPosition.Y + sprite.Texture.Size.Y) / I_blockSize].getWalkable()/*unten mitte*/
                ))
                return false;
            // ToDo: Verschlankung der if-Abfrage?

            // Kontrollangabe
            // Console.WriteLine(B_walkable);

           return true;        
        }


        /// <summary>
        /// Zeichnet die Karte.
        /// </summary>
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
