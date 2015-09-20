using SFML.Graphics;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IcyMazeRunner.Klassen
{
    class Moveable_Wall : GameObject //Attribute anpassen
    {
        
        /* ~~~~ Attribute für Mauer ~~~~ */

        /// <summary>
        ///  Aktuelle Position der Mauer.
        /// </summary>
        Vector2f Position;

        /// <summary>
        ///  Position, bevor die Move() ausgelöst wurde
        /// </summary>
        Vector2f prevPosition;

        /// <summary>
        ///  Sprite der Mauer
        /// </summary>
        Sprite spSprite;

        /// <summary>
        ///  Textur der Mauer
        /// </summary>
        Texture txCurrentTexture;

        /// <summary>
        ///  <para> 0 - linke oder obere Standposition </para>
        ///  <para> 1 - rechte oder untere Standposition </para>
        /// </summary>
        int I_direction;

        /// <summary>
        ///  <para> 0 - links rechts </para>
        ///  <para> 1 - oben unten</para>
        /// </summary>
        int I_orientation;

        /// <summary>
        ///  Variable, um die Bewegungsgeschwindigkeit an die Rechenzeit anzupassen
        /// </summary>
        float F_runningSpeed;

        /// <summary>
        ///  Variable, um die letzte Skalierung des Sprites zu speichern
        /// </summary>
        float F_LatestScale;

        /// <summary>
        /// Mitgegebene Map für Angabe der Blockgröße.
        /// </summary>
        Map cMap;

        /// <summary>
        ///  Trigger, der automatisch mit erstellt wird, wenn eine neue Mauer erstellt wird. Löst Bewegung der Mauer aus.
        /// </summary>
        public WallTrigger wallTrigger;

        /// <summary>
        ///  Boolean, der angibt, ob Mauer bewegt wird oder nicht, d.h. wenn Boolean wahr ist, wird move() aufgerufen, ansonsten nicht.
        /// </summary>
        Boolean B_moveable=false;


        

        /* ~~~~ Auslöserklasse ~~~~ */

        public class WallTrigger : GameObject
        {
            /* ~~~~ Attribute für Auslöser ~~~~ */

            /// <summary>
            ///  Position des Auslösers
            /// </summary>
            Vector2f Position;

            /// <summary>
            ///  Sprite des Auslösers
            /// </summary>
            Sprite spSprite;

            /// <summary>
            ///  Textur des Auslösers
            /// </summary>
            Texture txCurrentTexture;

            /// <summary>
            ///  Auslöserklasse zur Steuerung der Beweglichen Mauern
            /// </summary>
            public WallTrigger(Vector2f _WallTriggerPosition)
            {
                Position = _WallTriggerPosition;
                txCurrentTexture = new Texture("");
                spSprite = new Sprite(txCurrentTexture);
                spSprite.Position = Position;

            }

            /// <summary>
            /// Methode wird benötigt, da Klasse von GameObject erbt. Ansonsten nicht benötigt.
            /// </summary>
            public override void update(GameTime gameTime)
            {

            }

            /// <summary>
            ///  <para>Methode, um zu überprüfen, ob die aktuell nächste Position des Spielers mit der Mauer kollidieren würde, oder nicht.</para>
            ///  <para>Gibt einen Boolean zurück. Bekommt Spieler und Vektor der zukünftigen Position als Parameter mit.</para>
            /// </summary>
            public Boolean collision(Player player)
            {
                /* Mittlere Position des Spielers im nächsten Schritt */
                Vector2f newPosition = new Vector2f((player.getXPosition() + (player.getWidth()/2)), (player.getYPosition() + (player.getHeigth()/2)));
                
                /* Kollisionstest: Wenn wahr, wird wahr zurückgegeben, ansonsten Standardwert falsch. */
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

        /// <summary>
        /// Konstruktor für Mauer
        /// </summary>
        public Moveable_Wall(Vector2f WallPosition, Vector2f WallTriggerPosition, int _orientation, int _direction, Map _cMap)
        {
            wallTrigger = new WallTrigger(WallTriggerPosition);
            Position = WallPosition;
            prevPosition = Position;
            I_orientation = _orientation;
            if (I_orientation == 0)
            {
                txCurrentTexture = new Texture("");
            }
            else
            {
                txCurrentTexture = new Texture("");
            }
            spSprite = new Sprite(txCurrentTexture);
            spSprite.Position = Position;
            I_direction = _direction;
            F_runningSpeed = 1;
            F_LatestScale = 1;
            cMap = _cMap;
        }


        /* ~~~~ Getter und Setter für Mauer ~~~~ */

        /// <summary>
        ///  Gibt Wert des Boolean B_moveable zurück.
        /// </summary>
        public Boolean getB_moveable()
        {
            return B_moveable;
        }
        
        /// <summary>
        ///  Setzt Wert für Boolean B_moveable fest.
        /// </summary>
        public void setB_moveable(Boolean value)
        {
            B_moveable = value;
        }

       


        /// <summary>
        ///  Gibt Wert des Vektors der aktuellen Position zurück.
        /// </summary>
        public Vector2f get_Position()
        {
            return Position;
        }

        /// <summary>
        ///  Setzt Wert für Vektor der aktuellen Position fest.
        /// </summary>
        public void set_Position(Vector2f vector)
        {
            Position = vector;
        }

        /// <summary>
        ///  Gibt Wert des Vektors PrevPosition zurück, der angibt, wo sich der Vektor vor Beginn der Bewegung befand.
        /// </summary>
        public Vector2f get_PrevPosition()
        {
            return prevPosition;
        }

        /// <summary>
        ///  Setzt Wert für Vektor der vorherigen Position fest.
        /// </summary>
        public void set_PrevPosition(Vector2f vector)
        {
            prevPosition = vector;
        }

        /// <summary>
        ///  Gibt Wert des Int I_direction zurück.
        /// </summary>
        public int getI_direction()
        {
            return I_direction;
        }

        /// <summary>
        ///  Setzt Wert für Int I_direction fest.
        /// </summary>
        public void setI_direction(int value)
        {
            I_direction = value;
        }

        /// <summary>
        ///  Gibt Wert des Int I_orientation zurück.
        /// </summary>
        public int getI_orientation()
        {
            return I_orientation;
        }

        /// <summary>
        ///  Setzt Wert für Int I_orientation fest.
        /// </summary>
        public void setI_orientation(int value)
        {
            I_orientation = value;
        }


        /// <summary>
        ///  Normalisiert I_orientation und I_direction auf einen Wert von 0 oder 1.
        /// </summary>
        public override void update(GameTime gameTime)
        {
            I_orientation = I_orientation % 2;
            I_direction = I_direction % 2;
        }

        /// <summary>
        ///  <para> Move ist für die Bewegung der Mauern verantwortlich. </para>
        ///  <para> Als erstes wird die Bewegungsgeschwindigkeit an die Rechnerzeit angepasst.</para>
        ///  <para> Mit I_orientation wird dann zunächst erfasst, ob es sich um eine Bewegung nach links oder rechts oder um eine Bewegung
        ///         von oben nach unten handelt. </para>
        ///  <para> Anschließend wird erfasst, ob es sich nach links oder nach rechts bzw nach oben oder nach unten bewegt.</para>
        ///  <para> Dann wird die Bewegung ausgeführt. </para>
        ///  <para> Wenn die Position der Mauer nur geringfügig von der Endposition abweicht, wird sie auf exakt diesen Wert gesetzt,
        ///         um Rundungsfehler zu vermeiden. </para>
        ///  <para> Zuletzt wird die Position des Sprites angepasst.</para>
        /// </summary>
        public void move(GameTime gameTime, Map cMap)
        {

            F_runningSpeed = 0.5f * gameTime.ElapsedTime.Milliseconds;

            if (I_orientation==0)
            {
                // nach rechts (in den Gang)
                if (I_direction % 2 == 0)
                {
                    Position.X = Position.X + F_runningSpeed;

                    if (Math.Abs(Position.X - (prevPosition.X + cMap.I_blockSize)) < 5)
                    {
                        Position.X = prevPosition.X + cMap.I_blockSize;
                    }
                }
                // nach links (aus den Gang)
                else
                {
                    Position.X = Position.X - F_runningSpeed;

                    if (Math.Abs(Position.X - (prevPosition.X - cMap.I_blockSize)) < 5)
                    {
                        Position.X = prevPosition.X - cMap.I_blockSize;
                    }
                }

            }
            // oben-unten Ausrichtung
            else
            {
                // nach unten (in den Gang)
                if (I_direction % 2 == 0)
                {
                    Position.Y = Position.Y + F_runningSpeed;

                    if (Math.Abs(Position.Y - (prevPosition.Y - cMap.I_blockSize)) < 5)
                    {
                        Position.Y = prevPosition.Y - cMap.I_blockSize;
                    }
                    // skaliert Sprite schrittweise auf 180 Pixel Höhe hinauf
                    spSprite.Scale = new Vector2f(1, 1 / F_LatestScale);
                    spSprite.Scale = new Vector2f(1, ((90 + Math.Abs(prevPosition.Y - Position.Y)) / 90));
                }
                // nach oben (aus den Gang)
                else 
                {
                    Position.Y = Position.Y - F_runningSpeed;

                    if (Math.Abs(Position.Y - (prevPosition.Y - cMap.I_blockSize)) < 5)
                    {
                        Position.Y = prevPosition.Y - cMap.I_blockSize;
                    }
                    // skaliert Sprite schrittweise auf 90 Pixel Höhe herunter
                    spSprite.Scale = new Vector2f(1, 1 / F_LatestScale);
                    spSprite.Scale = new Vector2f(1, ((180 - Math.Abs(prevPosition.Y - Position.Y)) / 180));
                }

                F_LatestScale = (180 - Math.Abs(prevPosition.Y - Position.Y)) / 180;
            }

            spSprite.Position = Position;
        }

        /// <summary>
        ///  <para>Testet die Kollision des Spielers im nächsten Schritt mit einer Beweglichen Mauer. Gibt einen Boolean zurück, der einen static Boolean aktualisiert.</para>
        ///  <para>Wenn keine Kollision stattfindet, wird wahr zurückgegeben, d.h. Bewegung ist möglich.</para>
        /// </summary>
        public Boolean Wall_Collision(Player player, Vector2f predictedPosition)
        {
            Vector2f newPosition = new Vector2f((player.getXPosition() + predictedPosition.X + (player.getWidth() / 2)), (player.getYPosition() + predictedPosition.Y + (player.getHeigth() / 2)));
            if (I_orientation == 0)
            {
                // 2 Blöcke stehen im Weg, deswegen einmal Kontrolle von oben und einmal von unten (Y+45 obere Mitte, Y+135 untere Mitte)
                if (Calculator.getDistance(new Vector2f(newPosition.X + player.getWidth(), newPosition.Y + player.getHeigth()), new Vector2f(spSprite.Position.X + 45, spSprite.Position.Y + 45)) < 50
                    ||
                    Calculator.getDistance(new Vector2f(newPosition.X + player.getWidth(), newPosition.Y + player.getHeigth()), new Vector2f(spSprite.Position.X + 45, spSprite.Position.Y + 135)) < 50)
                {
                    return false;
                }
            }
            else 
            {
                // 135, da nur mit dem "unteren" Block verglichen wird und nicht mit dem Block in der Mauer
                if (Calculator.getDistance(new Vector2f(newPosition.X + player.getWidth(), newPosition.Y + player.getHeigth()), new Vector2f(spSprite.Position.X + 45, spSprite.Position.Y + 135)) < 50)
                {
                    return false;
                }
            }

            
            return true;
        }
    }
}