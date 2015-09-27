using SFML.Graphics;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IcyMazeRunner
{
    class PlayerConditionHandler
    {

        /// <summary>
        ///  Liste an Zustanden, die vom PlayerStateHandler verwaltet werden.
        /// </summary>
        public List<Playercondition> ConditionList;

        /// <summary>
        ///  <para>Konstruktor</para>
        ///  <para>Er initialisiert nur die Liste.</para>
        /// </summary>
        public PlayerConditionHandler()
        {
            ConditionList = new List<Playercondition>();
        }

        /// <summary>
        ///  Fügt der Liste einen Zustand hinzu.
        /// </summary>
        public void add(Playercondition condition)
        {
            ConditionList.Add(condition);
        }

        /// <summary>
        ///  Fügt der Liste eine weitere Liste an Zuständen hinzu.
        /// </summary>
        public void add(List<Playercondition> added_conditionList)
        {
            foreach (Playercondition condition in added_conditionList)
            {
                ConditionList.Add(condition);
            }

        }

        /// <summary>
        ///  Alle Zustände werden beendet und in der nächsten Update() gelöscht.
        /// </summary>
        public void deleteAll()
        {
            foreach (Playercondition condition in ConditionList)
            {
                condition.B_IsFinished = true;
            }
        }

        /// <summary>
        ///  Sortiert Liste und löscht beendete Zustände. Anschließend wird jeder Zustand in der Liste aktualisiert.
        /// </summary>
        public void update()
        {

            ConditionList.Sort();

            for (int i = 0; i < ConditionList.Count; i++)
            {
                if (ConditionList[i].B_IsFinished)
                {
                    ConditionList.RemoveAt(i);
                    i--;
                }
            }

            foreach (Playercondition condition in ConditionList)
            {
                condition.update();
            }

        }

        public void draw (RenderWindow win)
        {
            int I_PositionCounter = 0;
            // ToDo: 1. Position anpassen
            Vector2f Position = new Vector2f(30, 30);

            foreach(Playercondition plc in ConditionList)
            {
                // Ordnet Effekt-Icons nebeneinander an
                // ToDo: Abstand anpassen
                Position.X = Position.X + (I_PositionCounter * (plc.spCondition.Texture.Size.X + 20));

                // Wenn zu weit nach rechts im Bild, neue Reihe anfangen.
                // ToDo: Abstand anpassen
                if (Position.X >= 300)
                {
                    Position.Y = Position.Y + plc.spCondition.Texture.Size.Y + 20;
                    I_PositionCounter = 0;
                    Position.X = 30;
                }

                plc.draw(win, Position);
                I_PositionCounter++;
            }
        }
    }
}
