using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IcyMazeRunner.Klassen
{
    public class MovableWallHandler : GameObjectHandler
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
                gameObjectList.Add(moveableWall);
            }
        
        }

        public static void deleteAll()
        {
            foreach (Moveable_Wall moveableWall in MoveableWallList)
            {
                moveableWall.kill();
            }
        }

        public static void deleteType(String _type)
        {
            bool B_FoundEntry = false;
            for (int i = 0; i < MoveableWallList.Count; i++)
            {
                if (MoveableWallList[i].S_type.Equals(_type))
                {
                    MoveableWallList.RemoveAt(i);
                    B_FoundEntry = true;
                    i--;
                }
            }

            if (B_FoundEntry)
            {
                GameObjectHandler.deleteType(_type);
            }
        }

        public void update(GameTime gameTime, Player pRunner, Vector2f predictedPosition, Map cMap)
        {

            for (int i = 0; i < MoveableWallList.Count; i++)
            {
                if (!MoveableWallList[i].B_isAlive)
                {
                    MoveableWallList.RemoveAt(i);
                    i--;
                }
            }

            foreach (Moveable_Wall moveableWall in MoveableWallList)
            {
                moveableWall.setB_moveable(moveableWall.wallTrigger.collision(pRunner, predictedPosition));
                moveableWall.update(gameTime, cMap);
            }
        }
    }
}
