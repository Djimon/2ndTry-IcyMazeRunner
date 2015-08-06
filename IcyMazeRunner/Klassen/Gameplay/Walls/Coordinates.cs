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
        private int I_xCoord { public get; public set; }

        /// <summary>
        ///  Speichert y-Koordinate.
        /// </summary>
        private int I_yCoord { public get; public set; }

        /// <summary>
        ///  Speichert, ob Textur in dem Feld darüber verändert werden muss.
        /// </summary>
        private Boolean B_UpperTexChanger { public get; public set; }

        public Coordinates(int x, int y, Boolean b)
        {
            I_xCoord = x;
            I_yCoord = y;
            B_UpperTexChanger = b;
        }
    }
}
