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
        Sprite spNextLevelScreen;
        static int I_level = 1;
       



        /* ~~~~ Screen initialisieren ~~~~*/
        public void initialize()
        {
            spNextLevelScreen = new Sprite(txNextLevelScreen);
            spNextLevelScreen.Position = new Vector2f(0, 0);

            SaveLevel Levelspeicher = new SaveLevel(I_level);
            I_level = Levelspeicher.decrypt() + 1;
            Levelspeicher.encrypt(I_level);
            Console.WriteLine(Levelspeicher.decrypt()); //debug zwecke...
            Console.WriteLine(I_level); // SaveData.txt wird nicht geschrieben, hat aber in einem externen testrahmen geklappt
            
        }


        /* ~~~~ Screen laden ~~~~*/
        public void loadContent()
        {
            // passende Textur einfügen
            txNextLevelScreen = new Texture("Texturen/Menü+Anzeigen/GameWon.png");

       

        }


        /* ~~~~ Screen aktualisieren ~~~~*/
        public EGameStates update(GameTime time)
        {
            // warten bis Ladevorgang für nächstes level abgeschlossen (B_isready)
           
            if (Keyboard.IsKeyPressed(Keyboard.Key.Return))
            {
                return EGameStates.inGame;
            }
            return EGameStates.NextLevel;
        }


        /* ~~~~ Draw ~~~~ */
        public void draw(RenderWindow win)
        {
            win.Draw(spNextLevelScreen);
        }
    }
}
