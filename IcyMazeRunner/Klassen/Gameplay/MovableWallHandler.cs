using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IcyMazeRunner.Klassen
{
    public class MoveableWallHandler : GameObjectHandler
    {

        public static List<Moveable_Wall> MoveableWallList;

        public MoveableWallHandler()
        {
            MoveableWallList = new List<Moveable_Wall>();


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


        public Boolean isWalkable(Player pPlayer, Vector2f predictedPosition)
        {
            try
            {
                foreach (Moveable_Wall moveableWall in MoveableWallList)
                {
                    if (moveableWall.Wall_Collision(pPlayer, predictedPosition, calc))
                    {
                        return false;
                    }
                }
            } catch (NullReferenceException)
            {

            }

            return true;
        }


        public void update(GameTime gameTime, Player pRunner, Map cMap, Calculator calc)
        {
            try
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
                /* Auslösen der Bewegung */
                if (!moveableWall.getB_moveable() && moveableWall.wallTrigger.collision(pRunner))
                {
                    moveableWall.setB_moveable(true);
                    moveableWall.set_PrevPosition(moveableWall.get_Position());
                    moveableWall.setI_direction(moveableWall.getI_direction()+1);
                }  

                /* Normalisieren von I_direction und I_orientation in der Moveable_Wall.update(...) */
                moveableWall.update(gameTime);
                
                /* Bewegen der Mauer */
                if (moveableWall.getB_moveable())
                {
                    moveableWall.move(gameTime, cMap);
                }

                /* Wenn an richtiger Position, Bool für erneutes Auslösen auf Standard setzen */
                 if( ((calc.addX(moveableWall.get_PrevPosition(),cMap.getBlocksize()).X.Equals(moveableWall.get_Position().X)) ||
                      (calc.addX(moveableWall.get_PrevPosition(),-cMap.getBlocksize()).X.Equals(moveableWall.get_Position().X))) 
                     &&
                     ((calc.addY(moveableWall.get_PrevPosition(),cMap.getBlocksize()).Y.Equals(moveableWall.get_Position().Y)) ||
                      (calc.addY(moveableWall.get_PrevPosition(),-cMap.getBlocksize()).Y.Equals(moveableWall.get_Position().Y)))
                   )
                 {
                     moveableWall.setB_moveable(false);
                 }
            }
            }
            catch (NullReferenceException)
            {
                Console.WriteLine("moveable wall is null");
            }
        }       
    }
}
