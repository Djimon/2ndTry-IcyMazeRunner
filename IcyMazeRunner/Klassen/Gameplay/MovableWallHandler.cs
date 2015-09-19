using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IcyMazeRunner.Klassen
{
    class MoveableWallHandler
    {
        /// <summary>
        /// Liste, die alle Objekte der beweglichen Mauern speichert und verwaltet.
        /// </summary>
        public static List<Moveable_Wall> MoveableWallList;

        /// <summary>
        /// einfacher Konstruktor
        /// </summary>
        public MoveableWallHandler()
        {
            MoveableWallList = new List<Moveable_Wall>();


        }

        /// <summary>
        /// Fügt eine Bewegliche Mauer der Liste hinzu.
        /// </summary>
        public void add(Moveable_Wall moveableWall)
        {
            MoveableWallList.Add(moveableWall);
        }

        /// <summary>
        /// Fügt eine weitere Liste Beweglicher Mauern der Liste hinzu.
        /// </summary>
        public void add(List<Moveable_Wall> added_moveableWallList)
        {
            foreach(Moveable_Wall moveableWall in added_moveableWallList)
            {
                MoveableWallList.Add(moveableWall);
                //gameObjectList.Add(moveableWall);
            }
        
        }

        /// <summary>
        /// Löscht alle beweglichen Mauern.
        /// </summary>
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
        // 
        
        //ToDo: nicht benötigt, da nur ein Typ vorhanden, oder?

        /// <summary>
        /// Kontrolliert, ob Spieler mit irgendeiner beweglichen Mauer kollidieren wird.
        /// </summary>
        public Boolean isWalkable(Player pPlayer, Vector2f predictedPosition)
        {
            try
            {
                foreach (Moveable_Wall moveableWall in MoveableWallList)
                {
                    if (moveableWall.Wall_Collision(pPlayer, predictedPosition))
                    {
                        return false;
                    }
                }
            } catch (NullReferenceException)
            {

            }

            return true;
        }
        // ToDo: für Enemy auch Kollision vorbereiten

        /// <summary>
        /// <para>Löscht zunächst alle alten, nicht mehr benötigten Einträge.<para>
        /// 
        /// <para>Anschließend kontrolliert er, ob der Spieler mit einer der Auslöser kollidiert, und wenn ja, wird die Bewegung ausgelöst, und 
        /// solange bewegt, bis er die Endposition erreicht hat. Die orientation und direction der movableWall werden normalisiert.<para>
        /// </summary>
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
                if (((Calculator.addX(moveableWall.get_PrevPosition(), cMap.I_blockSize).X.Equals(moveableWall.get_Position().X)) ||
                      (Calculator.addX(moveableWall.get_PrevPosition(), -cMap.I_blockSize).X.Equals(moveableWall.get_Position().X))) 
                     &&
                     ((Calculator.addY(moveableWall.get_PrevPosition(), cMap.I_blockSize).Y.Equals(moveableWall.get_Position().Y)) ||
                      (Calculator.addY(moveableWall.get_PrevPosition(), -cMap.I_blockSize).Y.Equals(moveableWall.get_Position().Y)))
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
