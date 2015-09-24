using System;
using IcyMazeRunner.Klassen.Gamestates_und_Gamestruktur.GUI;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Window;
using SFML.Graphics;

namespace IcyMazeRunner.Klassen
{
    class SkillController
    {

        // ToDo: Jump mit SkillController oder direkt im Player in der Move?
        // ToDo: Skills aktivierbar, wenn sich Spieler bewegt? (Könnte schwer zu animieren sein)

        GUI HealSkillGUI;
        GUI InvincibleGUI;
        GUI SpeedSkillGUI;
        GUI AoESkillGUI;
        GUI VisibleGUI;

        Boolean B_isPressed;
        public Boolean B_isBlocking { get; private set; }
        Boolean B_FirstBlock;
        Boolean B_FirstHeal;
        Boolean B_FirstInvincible;
        Boolean B_FirstSpeed;
        Boolean B_FirstAoE;
        Boolean B_FirstVisible;

        /// <summary>
        /// <para>GameTime für die Cooldowns der verschiedenen Fähigkeiten.</para>
        /// <para>Watchlist-Indizes:</para>
        /// <para>0 - Blocking</para>
        /// <para>1 - Heal</para>
        /// <para>2 - Speed</para>
        /// <para>3 - Invincible</para>
        /// <para>4 - Visible</para>
        /// <para>5 - AoE</para>
        /// </summary>
        GameTime CD;

        /// <summary>
        /// <para>GameTime für die Dauer der verschiedenen Fähigkeiten.</para>
        /// <para>Watchlist-Indizes:</para>
        /// <para>0 - Blocking</para>
        /// <para>1 - Heal(HoT)</para>
        /// <para>2 - Speed</para>
        /// <para>3 - Invincible</para>
        /// </summary>
        GameTime Duration;

        public float Healpercentage { get; protected set; }
        public float Speedpercentage { get; protected set; }


        public SkillController(View view)
        {
            CD = new GameTime();
            CD.WatchList.Add(new System.Diagnostics.Stopwatch());
            CD.WatchList.Add(new System.Diagnostics.Stopwatch());
            CD.WatchList.Add(new System.Diagnostics.Stopwatch());
            CD.WatchList.Add(new System.Diagnostics.Stopwatch());
            CD.WatchList.Add(new System.Diagnostics.Stopwatch());
            CD.WatchList.Add(new System.Diagnostics.Stopwatch());

            Duration = new GameTime();
            Duration.WatchList.Add(new System.Diagnostics.Stopwatch());
            Duration.WatchList.Add(new System.Diagnostics.Stopwatch());
            Duration.WatchList.Add(new System.Diagnostics.Stopwatch());
            Duration.WatchList.Add(new System.Diagnostics.Stopwatch());

            HealSkillGUI = new GUI(view);
            InvincibleGUI = new GUI(view);
            SpeedSkillGUI = new GUI(view);
            AoESkillGUI = new GUI(view);
            VisibleGUI = new GUI(view);

            B_isBlocking = false;
            B_isPressed = false;
            B_FirstBlock = true;
            B_FirstHeal = true;
            B_FirstInvincible = true;
            B_FirstSpeed = true;
            B_FirstAoE = true;
            B_FirstVisible = true;

            Healpercentage = 0.5f;
            Speedpercentage = 1.25f;
        }

        public void update(Player player)
        {
            /* ~~~~ Zurücksetzen des Booleans und aktualisieren der Skillzustände, wenn Cooldown abgelaufen ~~~~ */

            B_isPressed = false;

            /* Blocking */
            if (Duration.WatchList[0].ElapsedMilliseconds > 500)
            {
                B_isBlocking = false;
                Duration.WatchList[0].Reset();
                CD.WatchList[0].Start();
            }

            /* Speed */
            if (Duration.WatchList[2].ElapsedMilliseconds > 10000)
            {
                player.Speedbonus = 1f;
                Duration.WatchList[2].Reset();
            }

            /* Invincible */
            if (Duration.WatchList[3].ElapsedMilliseconds > 8000)
            {
                player.Speedbonus = 1f;
                Duration.WatchList[3].Reset();
            }

            /* ~~~~ Auslösen der Skills ~~~~ */

            /* Simple Attack */
            if (!B_isBlocking && Keyboard.IsKeyPressed(Keyboard.Key.Q))
            {
                B_isPressed = true;
                ////Angriffsmethode aufrufen
            }

            /* Blocking */
            if (!B_isPressed && !B_isBlocking && Keyboard.IsKeyPressed(Keyboard.Key.E) && (CD.WatchList[0].ElapsedMilliseconds>250 || B_FirstBlock ))
            {
                B_isBlocking = true;
                B_isPressed = true;
                B_FirstBlock = false;
                Duration.WatchList[0].Start();
                CD.WatchList[0].Reset();
            }

            // ToDo: mit if-Abfragen (Game.I-Level) die Skills sperren
            // ToDo: Booleans einfügen, die anzeigen ob Skill verfügbar ist oder nicht --> Verzicht auf FirstBooleans, GameTime-Resets werden ausgelagert, if-Abfrage übersichtlicher
            // ToDo: Cooldown-Reset, Cooldown-Start einfügen
            // ToDo: Skillspezialisierung implementieren


            /* Heal */
            if (!B_isPressed && !B_isBlocking && Keyboard.IsKeyPressed(Keyboard.Key.Num1) && (CD.WatchList[1].ElapsedMilliseconds > 30000 || B_FirstHeal ))
            {
                B_isPressed = true;
                B_FirstHeal = false;
                player.setHeal(Healpercentage);
                
            }

            /* Speed */
            if (!B_isPressed && !B_isBlocking && Keyboard.IsKeyPressed(Keyboard.Key.Num2) && (CD.WatchList[2].ElapsedMilliseconds > 60000 || B_FirstSpeed))
            {
                B_isPressed = true;
                B_FirstSpeed = false;
                player.Speedbonus = Speedpercentage;
            }

            /* Visible */
            if (!B_isPressed && !B_isBlocking && Keyboard.IsKeyPressed(Keyboard.Key.Num3) && (CD.WatchList[4].ElapsedMilliseconds > 120000 || B_FirstVisible))
            {
                B_isPressed = true;
                B_FirstVisible = false;
                player.B_WayIsVisible = true;
            }

            /* AoE */
            if (!B_isPressed && !B_isBlocking && Keyboard.IsKeyPressed(Keyboard.Key.Num4) && (CD.WatchList[5].ElapsedMilliseconds > 15000 || B_FirstAoE))
            {
                B_isPressed = true;
                B_FirstAoE = false;
                ////Angriffsmethode aufrufen
            }

            /* Invincible */
            if (!B_isPressed && !B_isBlocking && Keyboard.IsKeyPressed(Keyboard.Key.Num5) && (CD.WatchList[3].ElapsedMilliseconds > 300000 || B_FirstInvincible))
            {
                B_isPressed = true;
                B_FirstInvincible = false;
                player.B_IsInvincible = true;
            }

        }
    }
}
