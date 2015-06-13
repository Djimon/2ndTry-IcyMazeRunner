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
        /* ~~~~ Kollisionsboolean ~~~~ */
        bool B_walkable= true;


        /* ~~~~ Blocksprite anlegen ~~~~ */
        Sprite spBlock {get;set;}
        EGameStates gameState;
        private int blockType; 
        // Kollision ähnlich Loch im Boden --> Kollision mit Farbe --> setzt bool für Level beendet auf true --> player.update()returnt
        // nextLevel an Ingame --> InGame.update(); returnt nextLevel und dann wird Gamestate geändert


        /* ~~~~ Getter ~~~~ */
        public Texture getTexture()
        {
            return this.spBlock.Texture;
        }

        public bool getWalkable()
        {
            return this.B_walkable;
        }

        public int type()
        {
            return this.blockType;
        }

        /* ~~~~ Konstruktor ~~~~ */
        public Blocks(int blockType, Vector2f position, Texture txBlock)
        {
            this.blockType = blockType;

            /* ~~~~ Auswahl der Textur, je nachdem, welches int blockType die Map von der Bitmap mitgibt ~~~~ */
                switch (blockType)
                {
                    case 0: //alphaweg
                        {
                            this.spBlock = new Sprite(new Texture("Texturen/Map/wall-clean.png"));
                            this.spBlock.Position = position;
                            this.B_walkable = true;
                            break;
                        }

                    case 1: //alphamauer
                        {
                            this.spBlock = new Sprite(new Texture("Texturen/Map/wall-vert.png"));
                            this.spBlock.Position = position;
                            this.B_walkable = false;
                            break;
                        }
                    case 2: //leer
                        {
                            this.spBlock = new Sprite();
                            this.spBlock.Position = position;
                            this.B_walkable = false;

                            break;
                        }
                    case 3: //start
                        {
                            this.spBlock = new Sprite(new Texture("Texturen/Map/wall-clean.png"));
                            this.spBlock.Position = position;
                            this.B_walkable = true;
                            //insert hier spawn für Player
                            // PlayerPosition = blocksprite.position
                            break;
                        }
                    case 4: //Ziel
                        {
                            this.spBlock = new Sprite(new Texture("Texturen/Map/exit.png"));
                            this.spBlock.Position = position;
                            this.B_walkable = false;
                            gameState = EGameStates.inGame;
                            break;
                            // -> gameobject ziel initialisieren??
                            //insert hier Ziel: -> Gamestat/Level = +1
                            //Antwort: siehe oben
                        }
                    case 5: //horimauer
                        {
                            this.spBlock = new Sprite(new Texture("Texturen/Map/wall-hor.png"));
                            this.spBlock.Position = position;
                            this.B_walkable = false;
                            break;
                        }

                    case 6: //Loch im Boden
                        {
                            this.spBlock = new Sprite(new Texture("Texturen/Map/hole.png"));
                            this.spBlock.Position = position;
                            this.B_walkable = true;
                            break;
                        }

                }
           

               
        }

   

        /* ~~~~ Draw ~~~~ */
        public void draw(RenderWindow win)
        {
           win.Draw(this.spBlock);
        }


    }
}
