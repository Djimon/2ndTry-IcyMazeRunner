using SFML.Graphics;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IcyMazeRunner.Klassen
{
    static class GameObjectHandler
    {
        /// <summary>
        /// Liste an GameObjects.
        /// </summary>
        public static List<GameObject> gameObjectList { get; set; }

        /// <summary>
        /// Unterhandler für Entities.
        /// </summary>
        public static EntityHandler entityHandler { get; set; }

        /// <summary>
        /// Unterhandler für movable Walls.
        /// </summary>
        public static MoveableWallHandler moveableWallHandler { get; set; }



        /// <summary>
        /// Konstruktor. Unterhandler werden erstellt.
        /// </summary>
        //public GameObjectHandler()
        //{
        //    gameObjectList = new List<GameObject>();

        //    entityHandler = new EntityHandler();
        //    moveableWallHandler = new MoveableWallHandler();
        //}

        // ToDo: Abstract-Methoden verwenden?

        /// <summary>
        /// Fügt ein einzelnes GameObject hinzu.
        /// </summary>
        public static void add(GameObject obj)
        {
            //gameObjectList.Add(obj);
        }

        /// <summary>
        /// Fügt eine weitere Liste von Gameobjekten der Liste hinzu.
        /// </summary>
        public static void add(List<GameObject> objs)
        {
            //foreach (GameObject obj in objs)
            //{
            //    gameObjectList.Add(obj);
            //}
        }


        /// <summary>
        /// Löscht ein GameObejct.
        /// </summary>
        public static void deleteOne(int index)
        {
            //if (index < gameObjectList.Count)
            //{
            //    gameObjectList.RemoveAt(index);
            //}
            //for (int i = 0; i < gameObjectList.Count; i++)
            //{
            //    gameObjectList[i].I_indexObjectList = i;
            //}
        }

        /// <summary>
        /// Löscht alle GameObjects eines bestimmten Typs.
        /// </summary>
        public static void deleteType(String S_type_)
        {
            //for (int i = 0; i < gameObjectList.Count; i++)
            //{
            //    if (gameObjectList[i].S_type.Equals(S_type_))
            //    {
            //        gameObjectList.RemoveAt(i);
            //        i--;
            //    }
            //}
        }

        /// <summary>
        /// Löscht alle Gameobjects.
        /// </summary>
        public static void deleteAll()
        {
            //foreach (GameObject gObj in gameObjectList)
            //{
            //    gObj.kill();
            //}
            EntityHandler.deleteAll();
            MoveableWallHandler.deleteAll();
        }

        /// <summary>
        /// <para>Liste wird zunächst sortiert und dann alle nicht mehr vorhandenen Objekte gelöscht. </para>
        /// 
        /// <para>Dann wird der Index aktualisiert und die updates() der Unterhandler aufgerufen.</para>
        /// </summary>
        public static void update(GameTime gameTime, Player pRunner, Map cMap)
        {
            //gameObjectList.Sort();

            //for (int i = 0; i < gameObjectList.Count; i++)
            //{
            //    if (!gameObjectList[i].B_isAlive)
            //    {
            //        gameObjectList.RemoveAt(i);
            //        i--;
            //    }
            //}

            //for (int i = 0; i < gameObjectList.Count; i++)
            //{
            //    gameObjectList[i].I_indexObjectList = i;
            //    gameObjectList[i].update(gameTime);
            //}

            //entityHandler.update(gameTime);
            //moveableWallHandler.update(gameTime, pRunner, cMap);
        }


       /// <summary>
       /// Zeichnet das GameObject.
       /// </summary>
        public static void draw(RenderWindow window)
        {
            foreach (GameObject gObj in gameObjectList)
            {
                if (gObj.B_isVisible)
                {
                    gObj.draw(window);
                }
            }
        }



    }
}
