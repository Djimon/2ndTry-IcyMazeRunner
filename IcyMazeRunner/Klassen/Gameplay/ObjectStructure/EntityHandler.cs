using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IcyMazeRunner.Klassen
{
    public class EntityHandler : GameObjectHandler
    {

        public static List<Entity> entityList { get; set; }
        public static MovableWallHandler movableWallHandler { get; set; }

        public EntityHandler()
        {
            entityList = new List<Entity>();
            MovableWallHandler movableWallHandler = new MovableWallHandler();
        }

        public static void add(Entity entity)
        {
            entityList.Add(entity);
            GameObjectHandler.add(entity);
        }

        public static void add(List<Entity> entities)
        {
            foreach (Entity entity in entities)
            {
                entityList.Add(entity);
                GameObjectHandler.add(entity);
            }
        }



        public static void deleteAll()
        {
            foreach (Entity entity in entityList)
            {
                entity.kill();
            }
        }

        public static void deleteType(String _type)
        {
            bool B_FoundEntry = false;
            for (int i = 0; i < entityList.Count; i++)
            {
                if (entityList[i].S_type.Equals(_type))
                {
                    entityList.RemoveAt(i);
                    B_FoundEntry = true;
                    i--;
                }
            }

            if (B_FoundEntry)
            {
                GameObjectHandler.deleteType(_type);
            }
        }

        public void update(GameTime gameTime)
        {
            for (int i = 0; i < entityList.Count; i++)
            {
                if (!entityList[i].B_isAlive)
                {
                    entityList.RemoveAt(i);
                    i--;
                }
            }

            movableWallHandler.update(gameTime);
        }

    }
}
