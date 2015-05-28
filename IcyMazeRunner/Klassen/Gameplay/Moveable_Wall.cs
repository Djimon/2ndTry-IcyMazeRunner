using SFML.Graphics;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IcyMazeRunner.Klassen
{
    public class Moveable_Wall
    {
        
        /* ~~~~ Attribute für Mauer ~~~~ */
        Vector2f Wall_Position;
        Sprite spWall;
        Texture txWall;
        int I_direction;
        int I_orientation;
        /*
         0 - links rechts
         1 - oben unten
        */

        /* ~~~~ Auslöserklasse ~~~~ */
        public class WallTrigger
        {
            /* ~~~~ Attribute für Auslöser ~~~~ */
            Vector2f Wall_Trigger_Position;
            Sprite spWall_Trigger;
            Texture txWall_Trigger;
            

            public WallTrigger(Vector2f _WallTriggerPosition)
            {
                txWall_Trigger = new Texture("");
                spWall_Trigger = new Sprite(txWall_Trigger);
                spWall_Trigger.Position = _WallTriggerPosition;

            }

            public Boolean collision(Player player, Vector2f predictedPosition)
            {
                Vector2f newPosition = new Vector2f((player.getXPosition() + predictedPosition.X + (player.getWidth()/2)), (player.getYPosition() + predictedPosition.Y + (player.getHeigth()/2)));
                // for each wall -Schleife
                if (
                     (Math.Abs(newPosition.X + - spWall_Trigger.Position.X) < 45) 
                     &&
                     (Math.Abs(newPosition.Y + - spWall_Trigger.Position.Y) < 45)
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

            WallTrigger wallTrigger = new WallTrigger(WallTriggerPosition);
            txWall = new Texture("");
            spWall = new Sprite(txWall);
            I_orientation = _orientation;
            I_direction = _direction;
        }


        // wenn Trigger ausgelöst, wird move aufgerufen
        // in 2 Richtungen(oben-unten oder links-rechts)
        // Int%2 gibt an, in welche Richtung
        public void move(int I_direction, Map cMap)
        {
            // Speed an Rechnerzeit anpassen, Blocksize mit hineinnehmen und unten durch Speed ersetzen
            // links-rechts Ausrichtung
            if (I_orientation==0)
            {
                // rechts
                if (I_direction % 2 == 0)
                {
                    Wall_Position.X = Wall_Position.X + cMap.getBlocksize();
                }
                // links
                else
                {
                    Wall_Position.X = Wall_Position.X - cMap.getBlocksize();
                }
                // wenn aktuelle Wallposition sich nur um x Pixel von Endposition(prevPosition.X + bzw. - getBlocksize) unterscheidet, wird es
                // Wallposition auf prevPosition.X + getBlocksize() gesetzt (um genaue Position zu erhalten und Rundungsfehler zu vermeiden)
            }
            // oben-unten Ausrichtung
            else
            {
                // unten
                if (I_direction % 2 == 0)
                {
                    Wall_Position.Y = Wall_Position.Y + cMap.getBlocksize();
                }
                // oben
                else
                {
                    Wall_Position.Y = Wall_Position.Y - cMap.getBlocksize();
                }
            }

            //Kollision einfügen

        } 
        
    }
}