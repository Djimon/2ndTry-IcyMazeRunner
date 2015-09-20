using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IcyMazeRunner.Klassen
{
    public class EntityHandler
    {
        /// <summary>
        /// Liste aller Entities.
        /// </summary>
        //public static List<Entity> entityList { get; set; }
        // ToDo: Wird Entitylist und alle Methoden hierin benötigt?

        public EntityHandler()
        {
            //entityList = new List<Entity>();
        }

        /// <summary>
        /// Hinzufügen einer einzelnen Entität.
        /// </summary>
        //public static void add(Entity entity)
        //{
        //    entityList.Add(entity);
        //    GameObjectHandler.add(entity);
        //}


        /// <summary>
        /// Hinzufügen einer Liste von Entitäten.
        /// </summary>
        //public static void add(List<Entity> entities)
        //{
        //    foreach (Entity entity in entities)
        //    {
        //        entityList.Add(entity);
        //        GameObjectHandler.add(entity);
        //    }
        //}


        /// <summary>
        /// Löscht alle Entitäten.
        /// </summary>
        //public static void deleteAll()
        //{
        //    foreach (Entity entity in entityList)
        //    {
        //        entity.kill();
        //    }
        //}
        

        ///// <summary>
        ///// Löscht alle Entitäten eines bestimmten Typs.
        ///// </summary>
        //public static void deleteType(String _type)
        //{
        //    bool B_FoundEntry = false;
        //    for (int i = 0; i < entityList.Count; i++)
        //    {
        //        if (entityList[i].S_type.Equals(_type))
        //        {
        //            entityList.RemoveAt(i);
        //            B_FoundEntry = true;
        //            i--;
        //        }
        //    }

        //    if (B_FoundEntry)
        //    {
        //        GameObjectHandler.deleteType(_type);
        //    }
        //}


        ///// <summary>
        ///// Aktualisiert die Liste der Entitäten.
        ///// </summary>
        //public void update(GameTime gameTime)
        //{
        //    for (int i = 0; i < entityList.Count; i++)
        //    {
        //        if (!entityList[i].B_isAlive)
        //        {
        //            entityList.RemoveAt(i);
        //            i--;
        //        }
        //    }
        //}

    }
}
