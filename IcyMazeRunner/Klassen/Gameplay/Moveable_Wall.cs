using SFML.Graphics;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IcyMazeRunner.Klassen
{
    public class Moveable_Wall : GameObject //Attribute anpassen
    {
        
        /* ~~~~ Attribute für Mauer ~~~~ */
        Vector2f Position;
        Vector2f prevPosition;
        Sprite spSprite;
        Texture txCurrentTexture;
        int I_direction;
        int I_orientation;
        float F_runningSpeed;
        /*
         0 - links rechts
         1 - oben unten
        */
        public WallTrigger wallTrigger;
        Boolean B_moveable=false;


        /* ~~~~ Auslöserklasse ~~~~ */
        public class WallTrigger : GameObject
        {
            /* ~~~~ Attribute für Auslöser ~~~~ */
            Vector2f Position;
            Sprite spSprite;
            Texture txCurrentTexture;
            

            public WallTrigger(Vector2f _WallTriggerPosition)
            {
                Position = _WallTriggerPosition;
                txCurrentTexture = new Texture("");
                spSprite = new Sprite(txCurrentTexture);
                spSprite.Position = Position;

            }

            public override void update(GameTime gameTime)
            {

            }

            public Boolean collision(Player player, Vector2f predictedPosition)
            {
                Vector2f newPosition = new Vector2f((player.getXPosition() + predictedPosition.X + (player.getWidth()/2)), (player.getYPosition() + predictedPosition.Y + (player.getHeigth()/2)));
                
                if (
                     (Math.Abs(newPosition.X + -spSprite.Position.X) < 45) 
                     &&
                     (Math.Abs(newPosition.Y + -spSprite.Position.Y) < 45)
                   )
                {

                    return true;
                }

                return false;              
            }

        }

        /* ~~~~ Konstruktur für Mauer ~~~~ */
        public Moveable_Wall(Vector2f WallPosition, Vector2f WallTriggerPosition, int _orientation, int _direction, Map cMap)
        {
            wallTrigger = new WallTrigger(WallTriggerPosition);
            Position = WallPosition;
            prevPosition = Position;
            txCurrentTexture = new Texture("");
            spSprite = new Sprite(txCurrentTexture);
            spSprite.Position = Position;
            I_orientation = _orientation;
            I_direction = _direction;
            F_runningSpeed = 1;
        }

        public Boolean getB_moveable()
        {
            return B_moveable;
        }

        public void setB_moveable(Boolean value)
        {
            B_moveable = value;
        }

        // wenn Trigger ausgelöst, wird move aufgerufen
        public void update(GameTime gameTime, Map cMap)
        {
            if (B_moveable) move(gameTime, cMap);
        }

        public override void update(GameTime gameTime)
        {

        }


 
        // in 2 Richtungen(oben-unten oder links-rechts)
        // Int%2 gibt an, in welche Richtung
        public void move(GameTime gameTime, Map cMap)
        {

            F_runningSpeed = 0.5f * gameTime.ElapsedTime.Milliseconds;

            if (I_orientation==0)
            {
                // rechts
                if (I_direction % 2 == 0)
                {
                    Position.X = Position.X + F_runningSpeed;

                    if (Math.Abs(Position.X - (prevPosition.X + cMap.getBlocksize())) < 5)
                    {
                        Position.X = prevPosition.X + cMap.getBlocksize();
                    }
                }
                // links
                else
                {
                    Position.X = Position.X - F_runningSpeed;

                    if (Math.Abs(Position.X - (prevPosition.X - cMap.getBlocksize())) < 5)
                    {
                        Position.X = prevPosition.X - cMap.getBlocksize();
                    }
                }

            }
            // oben-unten Ausrichtung
            else
            {
                // unten
                if (I_direction % 2 == 0)
                {
                    Position.Y = Position.Y + F_runningSpeed;

                    if (Math.Abs(Position.Y - (prevPosition.Y - cMap.getBlocksize())) < 5)
                    {
                        Position.Y = prevPosition.Y - cMap.getBlocksize();
                    }
                }
                // oben
                else
                {
                    Position.Y = Position.Y - F_runningSpeed;

                    if (Math.Abs(Position.Y - (prevPosition.Y - cMap.getBlocksize())) < 5)
                    {
                        Position.Y = prevPosition.Y - cMap.getBlocksize();
                    }
                }
            }

            spSprite.Position = Position;
        }

        public Boolean Wall_Collision(Player player, Vector2f predictedPosition, Calculator calc)
        {
            if (calc.getDistance(new Vector2f(predictedPosition.X + player.getWidth(), predictedPosition.Y + player.getHeigth()), new Vector2f(spSprite.Position.X + 45, spSprite.Position.Y + 45)) < 50)
            {
                return true;
            }

            return false;
        }
    }
}