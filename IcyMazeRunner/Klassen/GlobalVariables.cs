using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IcyMazeRunner.Klassen
{
    public static class GlobalVariables
    {
        public static int windowSizeX;
        public static int windowSizeY;

        public static void SetX(int X) 
        {
            windowSizeX = X;
        }
        public static void setY(int y)
        {
            windowSizeY = y;
        }
        public static int getX()
        {
            return windowSizeX;
        }
        public static int getY()
        {
            return windowSizeY;
        }

    }
}
