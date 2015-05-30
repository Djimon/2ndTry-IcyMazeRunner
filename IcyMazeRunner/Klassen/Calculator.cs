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

        public float Vectordistance(Vector2f VectorOne, Vector2f VectorTwo)
        {
            float distance;
            return distance = (float)Math.Sqrt(Math.Pow(Math.Abs(VectorOne.X-VectorTwo.X),2) + Math.Pow(Math.Abs(VectorOne.Y-VectorTwo.Y),2));
        }
    }
}
