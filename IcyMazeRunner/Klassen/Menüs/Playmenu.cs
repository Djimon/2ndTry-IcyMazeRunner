using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IcyMazeRunner.Klassen.Menüs
{
    class Playmenu
    {
        View MView;
        Sprite spBG;
        Sprite spMain;
        Sprite spSelect;
        Sprite spFG;

        Texture txBG = new Texture("Texturen/Menu+Anzeigen/playBG.png");
        Texture txMainfull = new Texture("Texturen/Menu+Anzeigen/playBG.png");
        Texture txMainsingle = new Texture("Texturen/Menu+Anzeigen/playBG.png");
        Texture txNewGame = new Texture("Texturen/Menu+Anzeigen/start.png");
        Texture txContinue = new Texture("Texturen/Menu+Anzeigen/exit.png");
        Texture txFG = new Texture("Texturen/Menu+Anzeigen/GUI-FG.png");

        int select;
        bool closePlay;
        bool saved;
        bool isPressed;

        public void setClosePlay(bool isclosed)
        {
            closePlay = isclosed;
        }

        public bool getClosePlay()
        {
            return closePlay;
        }


        // ~~~Konstruktor~~~
        public Playmenu()
        {
            isPressed = false;
            closePlay = false;
            saved = false;
            // Überprüfen, ob ein Savegame existiert. (can-find)
            // wenn ja da saved = true;

            spBG = new Sprite(txBG);
            if (!saved) spMain = new Sprite(txMainsingle);
            else spMain = new Sprite(txMainfull);
            spSelect = new Sprite(txNewGame);
            spFG = new Sprite(txFG);


            MView = new View(new FloatRect(0, 0, Game.windowSizeX, Game.windowSizeX));


        }

        public void initialize()
        {
            spBG.Position = new Vector2f(0, 0);
            spBG.Scale = new Vector2f(0.83f, 1f);

            spFG.Position = new Vector2f(0, 0);
            spFG.Scale = new Vector2f(0.83f, 1f);

            spMain.Position = new Vector2f(0, 0);
            spMain.Scale = new Vector2f(0.83f, 1f);

            spSelect.Position = new Vector2f(0, 0);
            spSelect.Scale = new Vector2f(0.83f, 1f);
        }


        public EGameStates update()
        {


            if (!saved)
            {
                if (Keyboard.IsKeyPressed(Keyboard.Key.Return) && !isPressed)
                {
                    isPressed = true;
                    return EGameStates.inGame; //(new Game)
                }
                if (Keyboard.IsKeyPressed(Keyboard.Key.Escape) && !isPressed)
                {
                    setClosePlay(true);
                }

                if (!Keyboard.IsKeyPressed(Keyboard.Key.Return) && !Keyboard.IsKeyPressed(Keyboard.Key.Escape))
                {
                    isPressed = false;
                }
            }
            else
            {
                if (Keyboard.IsKeyPressed(Keyboard.Key.Down) && !isPressed)
                {
                    select = (select - 1) % 2;
                    isPressed = true;
                }
                if (Keyboard.IsKeyPressed(Keyboard.Key.Up) && !isPressed)
                {
                    select = (select + 1) % 2;
                    isPressed = true;
                }

                if (!Keyboard.IsKeyPressed(Keyboard.Key.Down) && !Keyboard.IsKeyPressed(Keyboard.Key.Up))
                    isPressed = false;


                switch (select)
                {
                    case 0: spSelect.Texture = txNewGame;
                        break;
                    case 1: spSelect.Texture = txContinue;
                        break;
                }


                if (Keyboard.IsKeyPressed(Keyboard.Key.Escape))
                {
                    setClosePlay(true);
                }
                if (select == 0 && Keyboard.IsKeyPressed(Keyboard.Key.Return))
                {
                    return EGameStates.inGame;//(New)
                }
                if (select == 1 && Keyboard.IsKeyPressed(Keyboard.Key.Return))
                {
                    return EGameStates.inGame;//(Load)	
                }



            }


            return EGameStates.mainMenu;
        }



        public void loadContent()
        {
           
        }


        public void draw(RenderWindow win)
        {
            //win.SetView(MView);
            
            win.Draw(spBG);
            win.Draw(spMain);
            win.Draw(spSelect);
           // win.Draw(spFG);

        }


      
    }
}
