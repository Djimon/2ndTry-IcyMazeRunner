using SFML.Graphics;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IcyMazeRunner.Klassen.Gameplay
{
    class Moveable_Wall
    {

        /* ~~~~ Attribute für Mauer ~~~~ */
        Vector2f Wall_Position;
        Sprite spWall;
        Texture txWall;
        int I_orientation;
        /*
         0 - links rechts
         1 - oben unten
        */

        /* ~~~~ Auslöserklasse ~~~~ */
        class WallTrigger
        {
            /* ~~~~ Attribute für Auslöser ~~~~ */
            Vector2f Wall_Trigger_Position;
            Sprite spWall_Trigger;
            Texture txWall_Trigger;

            public WallTrigger(Vector2f _WallTriggerPosition)
            {
                //Wall_Trigger_Position = _WallTriggerPosition;
                txWall_Trigger = new Texture("");
                spWall_Trigger = new Sprite(txWall_Trigger);
                spWall_Trigger.Position = _WallTriggerPosition;

            }

        }

        /* ~~~~ Konstruktur für Mauer ~~~~ */
        public Moveable_Wall(Vector2f WallPosition, Vector2f WallTriggerPosition, int _orientation)
        {

            WallTrigger wallTrigger = new WallTrigger(WallTriggerPosition);
            txWall = new Texture("");
            spWall = new Sprite(txWall);
            I_orientation = _orientation;
        }


        // wenn Trigger ausgelöst, wird move aufgerufen
        // in 2 Richtungen(oben-unten oder links-rechts)
        // Int%2 gibt an, in welche Richtung
        public void move(int I_direction)
        {
            // links-rechts Ausrichtung
            if (I_orientation==0)
            {
                // rechts
                if (I_direction % 2 == 0)
                {
                    Wall_Position.X = Wall_Position.X + Map.getBlocksize();
                }
                // links
                else
                {
                    Wall_Position.X = Wall_Position.X - Map.getBlocksize();
                }
            }
            // oben-unten Ausrichtung
            else
            {
                // unten
                if (I_direction % 2 == 0)
                {
                 //   Wall_Position.Y = Wall_Position.Y + Map.getBlocksize();
                }
                // oben
                else
                {
                  //  Wall_Position.Y = Wall_Position.Y - Map.getBlocksize();
                }
            }

        }
    }
}