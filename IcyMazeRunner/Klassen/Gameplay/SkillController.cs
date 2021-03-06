﻿using System;
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
        GUI HealSkillGUI;
        GUI InvincibleGUI;
        GUI SpeedSkillGUI;
        GUI AoESkillGUI;
        GUI VisibleGUI;

        Boolean B_isPressed;
        public Boolean B_isBlocking { get; private set; }
        Boolean B_BlockAvailable;
        Boolean B_HealAvailable;
        Boolean B_InvincibleAvailable;
        Boolean B_SpeedAvailable;
        Boolean B_AoEAvailable;
        Boolean B_VisibleAvailable;

        public Boolean B_isUsingSkill { get; private set; }

        /// <summary>
        /// <para>GameTime für die Cooldowns der verschiedenen Fähigkeiten.</para>
        /// <para>Watchlist-Indizes:</para>
        /// <para>0 - Blocking</para>
        /// <para>1 - Heal</para>
        /// <para>2 - Speed</para>
        /// <para>3 - Visible</para>
        /// <para>4 - AoE</para>
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
            B_BlockAvailable = true;
            B_HealAvailable = true;
            B_InvincibleAvailable = true;
            B_SpeedAvailable = true;
            B_AoEAvailable = true;
            B_VisibleAvailable = true;

            B_isUsingSkill = false;

            Healpercentage = 0.5f;
            Speedpercentage = 1.25f;
        }

        public void update(Player player)
        {
            /* ~~~~ Zurücksetzen des Booleans und aktualisieren der Skillzustände, wenn Cooldown abgelaufen ~~~~ */

            B_isPressed = false;
            B_isUsingSkill = false;

            /* kein Sofortskill */
            if (B_isBlocking) B_isUsingSkill = true;

            Cooldownupdate();

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
            if (Duration.WatchList[3].ElapsedMilliseconds > 7000)
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
                B_isUsingSkill = true;
            }

            /* Blocking */
            if (!B_isPressed && !B_isBlocking && Keyboard.IsKeyPressed(Keyboard.Key.E) && B_BlockAvailable)
            {
                B_isBlocking = true;
                B_isPressed = true;
                B_BlockAvailable = false;
                Duration.WatchList[0].Start();
                CD.WatchList[0].Reset();
                B_isUsingSkill = true;
            }

            // ToDo: mit if-Abfragen (Game.I-Level) die Skills sperren
            // ToDo: Skillspezialisierung implementieren


            /* Heal */
            if (!B_isPressed && !B_isBlocking && Keyboard.IsKeyPressed(Keyboard.Key.Num1) && B_HealAvailable)
            {
                B_isPressed = true;
                B_HealAvailable = false;
                player.setHeal(Healpercentage);
                CD.WatchList[1].Start();
                B_isUsingSkill = true;
            }

            /* Speed */
            if (!B_isPressed && !B_isBlocking && Keyboard.IsKeyPressed(Keyboard.Key.Num2) && B_SpeedAvailable)
            {
                B_isPressed = true;
                B_SpeedAvailable = false;
                player.Speedbonus = Speedpercentage;
                CD.WatchList[2].Start();
                B_isUsingSkill = true;
            }

            /* Visible */
            if (!B_isPressed && !B_isBlocking && Keyboard.IsKeyPressed(Keyboard.Key.Num3) && B_VisibleAvailable)
            {
                B_isPressed = true;
                B_VisibleAvailable = false;
                player.B_WayIsVisible = true;
                CD.WatchList[3].Start();
                B_isUsingSkill = true;
            }

            /* AoE */
            if (!B_isPressed && !B_isBlocking && Keyboard.IsKeyPressed(Keyboard.Key.Num4) && B_AoEAvailable)
            {
                B_isPressed = true;
                B_AoEAvailable = false;
                ////Angriffsmethode aufrufen
                CD.WatchList[4].Start();
                B_isUsingSkill = true;
            }

            /* Invincible */
            if (!B_isPressed && !B_isBlocking && Keyboard.IsKeyPressed(Keyboard.Key.Num5) && B_InvincibleAvailable)
            {
                B_isPressed = true;
                B_InvincibleAvailable = false;
                player.B_IsInvincible = true;
                B_isUsingSkill = true;
            }

        }

        /// <summary>
        /// Aktualisiert die Cooldowns, wenn sie abgelaufen sind.
        /// </summary>
        void Cooldownupdate()
        {
            if (CD.WatchList[0].ElapsedMilliseconds > 250)
            {
                B_BlockAvailable = true;
                CD.WatchList[0].Reset();
            }

            if (CD.WatchList[1].ElapsedMilliseconds > 30000)
            {
                B_HealAvailable = true;
                CD.WatchList[1].Reset();
            }

            if (CD.WatchList[2].ElapsedMilliseconds > 60000) 
            {
                B_SpeedAvailable = true;
                CD.WatchList[2].Reset();
            }

            if (CD.WatchList[3].ElapsedMilliseconds > 25000) 
            {
                B_VisibleAvailable = true;
                CD.WatchList[3].Reset();
            }

            if (CD.WatchList[4].ElapsedMilliseconds > 30000) 
            {
                B_AoEAvailable = true;
                CD.WatchList[4].Reset();
            }
        }
    }
}
