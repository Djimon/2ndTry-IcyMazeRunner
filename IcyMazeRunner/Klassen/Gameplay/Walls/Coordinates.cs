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
        public int I_xCoord { public get; protected set; }

        /// <summary>
        ///  Speichert y-Koordinate.
        /// </summary>
        public int I_yCoord { public get; protected set; }

        /// <summary>
        ///  Speichert, ob Textur in dem Feld darüber verändert werden muss.
        /// </summary>
        public Boolean B_UpperTexChanger { public get; protected set; }

        public Coordinates(int x, int y, Boolean b)
        {
            I_xCoord = x;
            I_yCoord = y;
            B_UpperTexChanger = b;
        }
    }
}
