using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IcyMazeRunner
{
    class Coordinates
    {
        /// <summary>
        ///  Speichert x-Koordinate.
        /// </summary>
        public int I_xCoord { get; protected set; }

        /// <summary>
        ///  Speichert y-Koordinate.
        /// </summary>
        public int I_yCoord { get; protected set; }

        /// <summary>
        ///  Speichert, ob Textur in dem Feld darüber verändert werden muss.
        /// </summary>
        public Boolean B_UpperTexChanger { get; protected set; }

        /// <summary>
        /// Konstruktor
        /// </summary>
        public Coordinates(int x, int y, Boolean b)
        {
            I_xCoord = x;
            I_yCoord = y;
            B_UpperTexChanger = b;
        }
    }
}
