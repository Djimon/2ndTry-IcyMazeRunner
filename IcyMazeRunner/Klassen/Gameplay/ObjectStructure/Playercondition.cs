using SFML.Graphics;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IcyMazeRunner.Klassen;

namespace IcyMazeRunner
{
    class Playercondition
    {
        /// <summary>
        ///  Textur des Zustands.
        /// </summary>
        Texture txCondition;

        /// <summary>
        ///  Sprite des Zustands.
        /// </summary>
        public Sprite spCondition { get; private set; }

        /// <summary>
        ///  <para>Gibt an, wie lange der Zustand insgesamt bereits läuft. </para>
        ///  <para>Die Stoppuhr misst die Zeit bis zum jeweiligen nächsten Schadensaufruf. </para>
        /// </summary>
        GameTime gtTotalTime;
        // ToDo: wird nie etwas zugewiesen, immer Standardwert null

        /// <summary>
        ///  Gibt an, ob Zustand beendet ist.
        /// </summary>
        public Boolean B_IsFinished { get; set; }

        /// <summary>
        ///  Gibt in Sekunden an, wie lange der Zustand insgesamt anhalten soll.
        /// </summary>
        int I_runningTime;

        /// <summary>
        ///  Gibt in Millisekunden an, wie lange ein Intervall des Zustands dauern soll.
        /// </summary>
        int I_intervall;

        /// <summary>
        /// <para>Gibt die Art des Zustands an:</para>
        /// <para>0 - Gift</para>
        /// <para>1 - Gift schwarzer Bildschirm</para>
        /// <para>2 - Wunde</para>
        /// </summary>
        int I_type;

        /// <summary>
        /// Gibt die an, wieviel Schaden ein Tick des Zustands verursacht.
        /// </summary>
        int I_DamagePerTick;

        /// <summary>
        /// Verweis auf den Spieler, zu dem der Zustand gehört.
        /// </summary>
        Player thisPlayer;

        // ToDO: Texturen zuordnen
        /// <summary>
        /// <para>Konstruktor</para>
        /// <para>Variablen werden initialisiert, Texturen zugewiesen und der Schaden direkt zu Beginn wird dem Spieler zugefügt.</para>
        /// </summary>
        /// <param name="_runningtime">Gibt in Sekunden an, wie lange der Zustand anhalten soll.</param>
        /// <param name="_intervall">Gibt in Millisekunden an, wie lange ein Intervall des Zustands dauern soll.</param>
        /// <param name="_type">Gibt die Art des Zustands an. 0-Gift;; 1-Gift schwarzer Bildschirm;; 2-Wunde</param>
        /// <param name="pPlayer">this</param>
        public Playercondition (int _runningtime, int _intervall, int _type, Player pPlayer, int _DamagePerTick)
        {
            B_IsFinished = false;
            I_runningTime = _runningtime;
            I_intervall = _intervall;
            I_type = _type;
            I_DamagePerTick = _DamagePerTick;
            thisPlayer = pPlayer;

            switch (I_type)
            {
                case 0:
                    {
                        txCondition = new Texture("");
                        break;
                    }
                
                case 1:
                    {
                        txCondition = new Texture("");
                        break;
                    }

                case 2:
                    {
                        txCondition = new Texture("");
                        break;
                    }

                default:
                    {
                        txCondition = new Texture("");
                        break;
                    }
            }

            spCondition = new Sprite(txCondition);
            pPlayer.setDamage(I_DamagePerTick);


        }
        
        /// <summary>
        ///  <para>Wenn die TotalTime größer als die runningTime ist, wird der Boolean IsFinished auf true gesetzt, um dann im Händler aussortiert zu werden.</para>
        ///  <para>Wenn die verstrichene Zeit der Stoppuhr größer als das Intervall ist, soll Schaden verursacht werden und der Intervalltimer von vorn beginnen.</para>
        /// </summary>
        public void update()
        {
            if (gtTotalTime.TotalTime.Seconds >= I_runningTime)
            {
                B_IsFinished = true;
            }
            if (gtTotalTime.Watch.ElapsedMilliseconds >= I_intervall)
            {
                thisPlayer.setDamage(I_DamagePerTick);
                gtTotalTime.Watch.Restart();
            }

        }

        /// <summary>
        ///  Setzt Sprite an gegebene Position und zeichnet dort die Textur. Position wird in Liste kontinuierlich angepasst.
        /// </summary>
        public void draw(RenderWindow Win, Vector2f position)
        {
            spCondition.Position = position;
            Win.Draw(spCondition);
        }

    }
}
