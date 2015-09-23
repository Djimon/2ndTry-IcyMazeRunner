using SFML.Graphics;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IcyMazeRunner.Klassen.Gamestates_und_Gamestruktur
{
    class NextLevel : GameStates 
    {
        /* ~~~~ Screen anlegen ~~~~*/
        Texture txNextLevelScreen;

        // ToDo: Level speichern.
        /* ~~~~ Screen initialisieren ~~~~*/
        public void initialize()
        {
            Game.spBackGround.Texture = txNextLevelScreen;
            Game.spBackGround.Position = new Vector2f(0, 0);


            // ToDo: ohne decrypt aus Game.I_level abrufen und erhöhen, nur fürs speichern encrypten
            // ToDo: außerdem Game.I_BonusDefense und Game.I_BonusAttack abspeichern
            SaveLevel Levelspeicher = new SaveLevel(Game.I_level);
            Game.I_level = Levelspeicher.decrypt() + 1;
            Levelspeicher.encrypt(Game.I_level);
            Console.WriteLine(Levelspeicher.decrypt()); //debug zwecke...
            Console.WriteLine(Game.I_level); // SaveData.txt wird nicht geschrieben, hat aber in einem externen testrahmen geklappt
            
        }


        /* ~~~~ Screen laden ~~~~*/
        public void loadContent()
        {
            // passende Textur einfügen
            txNextLevelScreen = new Texture("Texturen/Menu+Anzeigen/GameWon.png");

       

        }


        /* ~~~~ Screen aktualisieren ~~~~*/
        public EGameStates update(GameTime time)
        {
            // ToDo: warten bis Ladevorgang für nächstes level abgeschlossen (B_isready)
           
            if (Keyboard.IsKeyPressed(Keyboard.Key.Return))
            {
                return EGameStates.inGame;
            }
            return EGameStates.NextLevel;
        }


        /* ~~~~ Draw ~~~~ */
        public void draw(RenderWindow win)
        {
            win.Draw(Game.spBackGround);
        }
    }
}
