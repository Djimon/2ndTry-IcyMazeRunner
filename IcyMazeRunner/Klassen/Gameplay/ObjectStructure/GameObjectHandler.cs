using SFML.Graphics;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IcyMazeRunner.Klassen
{
    public class GameObjectHandler
    {
        public static List<GameObject> gameObjectList { get; set; }
        public static EntityHandler entityHandler { get; set; }
        public static MoveableWallHandler moveableWallHandler { get; set; }

        public Calculator calc = new Calculator();

        public GameObjectHandler()
        {

        }

        public GameObjectHandler(Calculator calc)
        {
            gameObjectList = new List<GameObject>();

            entityHandler = new EntityHandler();
            moveableWallHandler = new MoveableWallHandler();
        }

        /* ~~~~ add ~~~~ */
        public static void add(GameObject obj)
        {
            gameObjectList.Add(obj);
        }

        public static void add(List<GameObject> objs)
        {
            foreach (GameObject obj in objs)
            {
                gameObjectList.Add(obj);
            }
        }


        /* ~~~~ delete one item, all items of a type, all items  ~~~~ */
        public static void deleteOne(int index)
        {
            if (index < gameObjectList.Count)
            {
                gameObjectList.RemoveAt(index);
            }
            for (int i = 0; i < gameObjectList.Count; i++)
            {
                gameObjectList[i].I_indexObjectList = i;
            }
        }

        public static void deleteType(String S_type_)
        {
            for (int i = 0; i < gameObjectList.Count; i++)
            {
                if (gameObjectList[i].S_type.Equals(S_type_))
                {
                    gameObjectList.RemoveAt(i);
                    i--;
                }
            }
        }

        public void deleteAll()
        {
            foreach (GameObject gObj in gameObjectList)
            {
                gObj.kill();
            }
            EntityHandler.deleteAll();
            MoveableWallHandler.deleteAll();
        }

        /* ~~~~ Update ~~~~ */
        public void update(GameTime gameTime, Player pRunner, Map cMap)
        {
            gameObjectList.Sort();

            for (int i = 0; i < gameObjectList.Count; i++)
            {
                if (!gameObjectList[i].B_isAlive)
                {
                    gameObjectList.RemoveAt(i);
                    i--;
                }
            }

            for (int i = 0; i < gameObjectList.Count; i++)
            {
                gameObjectList[i].I_indexObjectList = i;
                gameObjectList[i].update(gameTime);
            }

            entityHandler.update(gameTime);
            moveableWallHandler.update(gameTime, pRunner, cMap, calc);
        }


        /* ~~~~ Draw ~~~~ */
        public void draw(RenderWindow window)
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
