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
    public class Playerstate
    {
        /// <summary>
        ///  Textur des Zustands.
        /// </summary>
        Texture txState;

        /// <summary>
        ///  Sprite des Zustands.
        /// </summary>
        Sprite spState;

        /// <summary>
        ///  Gibt an, wie lange der Zustand insgesamt bereits läuft. 
        ///  Die Stoppuhr misst die Zeit bis zum jeweiligen nächsten Schadensaufruf.
        /// </summary>
        GameTime gtTotalTime;

        // ToDo: Getter/Setter als Property einfügen, im State Handler anschließend anpassen und public entfernen.
        /// <summary>
        ///  Gibt an, ob Zustand beendet ist.
        /// </summary>
        public Boolean B_IsFinished;

        /// <summary>
        ///  Gibt in Sekunden an, wie lange der Zustand insgesamt anhalten soll.
        /// </summary>
        int I_runningTime;

        /// <summary>
        ///  Gibt in Millisekunden an, wie lange ein Intervall des Zustands dauern soll.
        /// </summary>
        int I_intervall;

        /// <summary>
        /// Gibt die Art des Zustands an:
        /// 0 - Gift
        /// 1 - Gift schwarzer Bildschirm
        /// 2 - Wunde
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
        /// 
        /// </summary>
        /// <param name="_runningtime">Gibt in Sekunden an, wie lange der Zustand anhalten soll.</param>
        /// <param name="_intervall">Gibt in Millisekunden an, wie lange ein Intervall des Zustands dauern soll.</param>
        /// <param name="_type">Gibt die Art des Zustands an. 0-Gift;; 1-Gift schwarzer Bildschirm;; 2-Wunde</param>
        /// <param name="pPlayer">this</param>
        public Playerstate (int _runningtime, int _intervall, int _type, Player pPlayer, int _DamagePerTick)
        {
            // Standardzuweisung
            B_IsFinished = false;

            // Initialisierung
            I_runningTime = _runningtime;
            I_intervall = _intervall;
            I_type = _type;
            I_DamagePerTick = _DamagePerTick;
            thisPlayer = pPlayer;

            // Texturzuweisung
            switch (I_type)
            {
                case 0:
                    {
                        txState = new Texture("");
                        break;
                    }
                
                case 1:
                    {
                        txState = new Texture("");
                        break;
                    }

                case 2:
                    {
                        txState = new Texture("");
                        break;
                    }

                default:
                    {
                        txState = new Texture("");
                        break;
                    }
            }

            spState = new Sprite(txState);
            // Schaden direkt zu Beginn
            pPlayer.setDamage(I_DamagePerTick);


        }
        
        /// <summary>
        ///  Wenn die TotalTime größer als die runningTime ist, wird der Boolean IsFinished auf true gesetzt, um dann im Händler aussortiert zu werden.
        ///  Wenn die verstrichene Zeit der Stoppuhr größer als das Intervall ist, soll Schaden verursacht werden und der Intervalltimer von vorn beginnen. 
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
            spState.Position = position;
            Win.Draw(spState);
        }

    }
}
