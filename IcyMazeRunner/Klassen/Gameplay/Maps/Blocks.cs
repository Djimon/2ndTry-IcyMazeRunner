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
        bool walkable= true;


        /* ~~~~ Blocksprite anlegen ~~~~ */
        Sprite blockSprite {get;set;}
        EGameStates gameState; 
        // Kollision ähnlich Loch im Boden --> Kollision mit Farbe --> setzt bool für Level beendet auf true --> player.update()returnt
        // nextLevel an Ingame --> InGame.update(); returnt nextLevel und dann wird Gamestate geändert


        /* ~~~~ Getter ~~~~ */
        public Texture getTexture()
        {
            return this.blockSprite.Texture;
        }

        public bool getWalkable()
        {
            return this.walkable;
        }


        /* ~~~~ Konstruktor ~~~~ */
        public Blocks(int blockType, Vector2f position, Texture blockTex)
        {


            /* ~~~~ Auswahl der Textur, je nachdem, welches int blockType die Map von der Bitmap mitgibt ~~~~ */
                switch (blockType)
                {
                    case 0: //alphaweg
                        {
                            this.blockSprite = new Sprite(new Texture("Texturen/Map/wall-clean.png"));
                            this.blockSprite.Position = position;
                            this.walkable = true;
                            break;
                        }

                    case 1: //alphamauer
                        {
                            this.blockSprite = new Sprite(new Texture("Texturen/Map/wall-vert.png"));
                            this.blockSprite.Position = position;
                            this.walkable = false;
                            break;
                        }
                    case 2: //leer
                        {
                            this.blockSprite = new Sprite();
                            this.blockSprite.Position = position;
                            this.walkable = false;

                            break;
                        }
                    case 3: //start
                        {
                            this.blockSprite = new Sprite(new Texture("Texturen/Map/wall-clean.png"));
                            this.blockSprite.Position = position;
                            this.walkable = true;
                            //insert hier spawn für Player
                            // playerposition = blocksprite.position
                            break;
                        }
                    case 4: //Ziel
                        {
                            this.blockSprite = new Sprite(new Texture("Texturen/Map/exit.png"));
                            this.blockSprite.Position = position;
                            this.walkable = false;
                            gameState = EGameStates.inGame;
                            break;
                            // -> gameobject ziel initialisieren??
                            //insert hier Ziel: -> Gamestat/Level = +1
                            //Antwort: siehe oben
                        }
                    case 5: //horimauer
                        {
                            this.blockSprite = new Sprite(new Texture("Texturen/Map/wall-hor.png"));
                            this.blockSprite.Position = position;
                            this.walkable = false;
                            break;
                        }

                    case 6: //Loch im Boden
                        {
                            this.blockSprite = new Sprite(new Texture("Texturen/Map/way-hole Platzhalter.png"));
                            this.blockSprite.Position = position;
                            this.walkable = true;
                            break;
                        }

                }
           

               
        }


        /* ~~~~ Draw ~~~~ */
        public void draw(RenderWindow win)
        {
           win.Draw(this.blockSprite);
        }


    }
}
