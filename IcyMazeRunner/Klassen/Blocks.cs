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
        bool walkable= true;
        Sprite blockSprite;
        EGameStates gameState;

        public Texture getTexture()
        {
            return this.blockSprite.Texture;
        }

        public bool getWalkable()
        {
            return this.walkable;
        }

        public Blocks(int blockType, Vector2f position, Texture blockTex)
        {
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
                        this.blockSprite = new Sprite(new Texture("Texturen/Map/way-clean.png"));
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
                        this.blockSprite = new Sprite(new Texture("Texturen/Map/way-clean.png"));
                        this.blockSprite.Position = position;
                        this.walkable = false;
                        gameState = EGameStates.inGame;
                        break;
                        //insert hier Ziel: -> Gamestat/Level = +1
                    }



               }
        }

        public void draw(RenderWindow win)
        {
           win.Draw(this.blockSprite);
        }


    }
}
