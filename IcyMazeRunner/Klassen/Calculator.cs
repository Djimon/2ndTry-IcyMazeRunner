using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IcyMazeRunner.Klassen
{
    public class Calculator
    {

        public Calculator()
        {

        }

        public float getDistance(Vector2f VectorOne, Vector2f VectorTwo)
        {
            float distance;
            return distance = (float)Math.Sqrt(Math.Pow(Math.Abs(VectorOne.X-VectorTwo.X),2) + Math.Pow(Math.Abs(VectorOne.Y-VectorTwo.Y),2));
        }

        public float getWinkel(Vector2f position) //zur x-Achse
        {
            float x = position.X;
            float y = position.Y;

            //////////// MATH.ATAN2////////////////
            /* math.atan2(y,x) gibt Winkel zwischen x-Achse und Vektor(x,y) in Bogenmaß aus.
             * Beachte: beim benutzen von Atan, erst die Y, dann die X koordinate  
             *  (*180/ PI) = Umrechung von Bogenmaß in Grad */
            return (float)(Math.Atan2(y, x) * 180 / Math.PI);  // drehugnswinkel (in grad *180/PI) zum zielvector(y-wert,x-wert)
        }

        public Vector2f addX (Vector2f vector, float value)
        {
            Vector2f resultVector = new Vector2f(vector.X + value, vector.Y);
            return resultVector;
        }
        public Vector2f addY(Vector2f vector, float value)
        {
            Vector2f resultVector = new Vector2f(vector.Y + value, vector.Y);
            return resultVector;
        }

    }
}
