using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IcyMazeRunner.Klassen
{
    public class MovableWallHandler : EntityHandler
    {

        public static List<Moveable_Wall> MoveableWallList;

        public MovableWallHandler()
        {
            List<Moveable_Wall> MoveableWallList = new List<Moveable_Wall>();


        }

        public void add(Moveable_Wall moveableWall)
        {
            MoveableWallList.Add(moveableWall);
        }

        public void add(List<Moveable_Wall> added_moveableWallList)
        {
            foreach(Moveable_Wall moveableWall in added_moveableWallList)
            {
                MoveableWallList.Add(moveableWall);
                entityList.Add(moveableWall);
                gameObjectList.Add(moveableWall);
            }
        
        }

        public void update(GameTime gameTime, Player pRunner, Vector2f predictedPosition, Map cMap)
        {
            foreach (Moveable_Wall moveableWall in MoveableWallList)
            {
                moveableWall.wallTrigger.collision(pRunner, predictedPosition);
                moveableWall.move(cMap);
            }
        }
    }
}
