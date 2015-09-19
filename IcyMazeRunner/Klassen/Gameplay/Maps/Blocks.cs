using SFML.Graphics;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IcyMazeRunner.Klassen
{
    class Blocks 
    {
        /// <summary>
        /// Boolean, der angibt, ob Kachel betretbar ist oder nicht/kollidiert.
        /// </summary>
        bool B_walkable= true;


        /// <summary>
        /// Sprite des Blocks.
        /// </summary>
        Sprite spBlock {get;set;}

        EGameStates gameState { get; set; }
        // 
        
        
        
        //ToDo: gameState benötigt?

        /// <summary>
        /// <para>Gibt an, von welcher Art der Block ist.</para>
        /// <para>0 - Weg</para>
        /// <para>1 - Mauer Draufsicht</para>
        /// <para>2 - leer/Hintergrund</para>
        /// <para>3 - Startkachel</para>
        /// <para>4 - Zielkachel</para>
        /// <para>5 - Mauer Vorderansicht</para>
        /// <para>6 - Loch im Boden</para>
        /// <para>7 - Mauer Vorderansicht (geheimer Weg)</para>
        /// <para>8 - Mauer Draufsicht (geheimer Weg)</para>
        /// </summary>
        private int blockType; 
        // ToDo: folgendes Kontrollieren:
        // Kollision ähnlich Loch im Boden --> Kollision mit Farbe --> setzt bool für Level beendet auf true --> player.update()returnt
        // nextLevel an Ingame --> InGame.update(); returnt nextLevel und dann wird Gamestate geändert


        /// <summary>
        /// Methode, die Textur des Blocks auf mitgegebene Textur wechselt.
        /// </summary>
        public void setTexture(Texture tx)
        {
            this.spBlock.Texture = tx;
        }

        /// <summary>
        /// Methode, die zurückgibt, ob der Block walkable ist.
        /// </summary>
        public bool getWalkable()
        {
            return this.B_walkable;
        }

        /// <summary>
        /// Methode, die Typen des jeweiligen Blocks zurückgibt.
        /// </summary>
        public int type()
        {
            return this.blockType;
        }

        /// <summary>
        /// <para>Konstruktor</para>
        /// <para>Zunächst wird Typ des Blocks festgelegt und anhand dessen die restlichen Variablen und die Textur zugeordnet.</para>
        /// </summary>
        public Blocks(int blockType, Vector2f position, Texture txBlock)
            //ToDo: txBlock benötigt als Eingabe-Parameter?
        {
                this.blockType = blockType;

                switch (blockType)
                {
                    case 0:
                        {
                            this.spBlock = new Sprite(new Texture("Texturen/Map/wall-clean.png"));
                            this.spBlock.Position = position;
                            this.B_walkable = true;
                            break;
                        }

                    case 1:
                        {
                            this.spBlock = new Sprite(new Texture("Texturen/Map/wall-vert.png"));
                            this.spBlock.Position = position;
                            this.B_walkable = false;
                            break;
                        }
                    case 2:
                        {
                            this.spBlock = new Sprite();
                            this.spBlock.Position = position;
                            this.B_walkable = false;

                            break;
                        }
                    case 3:
                        {
                            this.spBlock = new Sprite(new Texture("Texturen/Map/wall-clean.png"));
                            this.spBlock.Position = position;
                            this.B_walkable = true;
                            //insert hier spawn für Player
                            // PlayerPosition = blocksprite.position
                            // ToDo: in InGame implementieren
                            break;
                        }
                    case 4:
                        {
                            this.spBlock = new Sprite(new Texture("Texturen/Map/exit.png"));
                            this.spBlock.Position = position;
                            this.B_walkable = false;
                            gameState = EGameStates.inGame;
                            break;
                            // -> gameobject ziel initialisieren??
                            //insert hier Ziel: -> Gamestat/Level = +1
                            //Antwort: siehe oben
                            // ToDo: in InGame implementieren
                        }
                    case 5: 
                        {
                            this.spBlock = new Sprite(new Texture("Texturen/Map/wall-hor.png"));
                            this.spBlock.Position = position;
                            this.B_walkable = false;
                            break;
                        }

                    case 6: 
                        {
                            this.spBlock = new Sprite(new Texture("Texturen/Map/hole.png"));
                            this.spBlock.Position = position;
                            this.B_walkable = true;
                            break;
                        }
                    case 7:
                        {
                            this.spBlock = new Sprite(new Texture("Texturen/Map/wall-vert.png"));
                            this.spBlock.Position = position;
                            this.B_walkable = true;
                            break;
                        }
                    case 8:
                        {
                            this.spBlock = new Sprite(new Texture("Texturen/Map/wall-hor.png"));
                            this.spBlock.Position = position;
                            this.B_walkable = true;
                            break;
                        }

                }
           

               
        }

   

        /// <summary>
        /// Zeichnet den einzelnen Block, nachdem Methode in Map.Draw() aufgerufen wird.
        /// </summary>
        public void draw(RenderWindow win)
        {
           win.Draw(this.spBlock);
        }


    }
}
