using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IcyMazeRunner
{
    class PlayerStateHandler
    {

        /// <summary>
        ///  Liste an Zustanden, die vom PlayerStateHandler verwaltet werden.
        /// </summary>
        public List<Playerstate> StateList;

        /// <summary>
        ///  <para>Konstruktor</para>
        ///  <para>Er initialisiert nur die Liste.</para>
        /// </summary>
        public PlayerStateHandler()
        {
             StateList = new List<Playerstate>();
        }

        /// <summary>
        ///  Fügt der Liste einen Zustand hinzu.
        /// </summary>
        public void add(Playerstate state)
        {
            StateList.Add(state);
        }

        /// <summary>
        ///  Fügt der Liste eine weitere Liste an Zuständen hinzu.
        /// </summary>
        public void add(List<Playerstate> added_stateList)
        {
            foreach (Playerstate state in added_stateList)
            {
                StateList.Add(state);
            }

        }

        /// <summary>
        ///  Alle Zustände werden beendet und in der nächsten Update() gelöscht.
        /// </summary>
        public void deleteAll()
        {
            foreach (Playerstate state in StateList)
            {
                state.B_IsFinished = true;
            }
        }

        /// <summary>
        ///  Sortiert Liste und löscht beendete Zustände. Anschließend wird jeder Zustand in der Liste aktualisiert.
        /// </summary>
        public void update()
        {

            StateList.Sort();

            for (int i = 0; i < StateList.Count; i++)
            {
                if (StateList[i].B_IsFinished)
                {
                    StateList.RemoveAt(i);
                    i--;
                }
            }

            foreach (Playerstate state in StateList)
            {
                state.update();
            }

        }
    }
}
