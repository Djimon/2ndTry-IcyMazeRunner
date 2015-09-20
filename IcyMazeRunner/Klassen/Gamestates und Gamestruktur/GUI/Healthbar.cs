using SFML.Graphics;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IcyMazeRunner.Klassen.Gamestates_und_Gamestruktur.GUI
{
    class Healthbar
    {

        Sprite spBG;
        Sprite spHealth;
        Sprite spFG;
        Player tobi;
        View view;
        Texture txHP = new Texture("Texturen/Menü+Anzeigen/InGame Menü/Steuerung Platzhalter.png");

        public Healthbar(Player player, View view)
        {
            this.view = view;
            this.tobi = player;
            spBG = new Sprite(new Texture("Texturen/Menü+Anzeigen/InGame Menü/null.png"));
            spHealth = new Sprite(txHP);
            spFG = new Sprite(new Texture("Texturen/Menü+Anzeigen/InGame Menü/null.png"));

            //// ??
            //spHealth.Position = new Vector2f((view.Center.X - (Game.windowSizeX / 2) + 5), (view.Center.Y - (Game.windowSizeY / 2) + txHP.Size.Y + 10));
            spHealth.Position = new Vector2f(5, 5);
            spBG.Position = new Vector2f(spHealth.Position.X + spHealth.Texture.Size.X, spHealth.Position.Y);
            spFG.Position = spBG.Position;

        }

        public Sprite scale(Sprite sprite)
        {
            // die "-1" ist mir noch unklar, wird durch tests verstanden (hfftl.)
            sprite.Scale = new Vector2f(-1 + (tobi.getPlayerHealth() / tobi.getPlayerMaxHealth()), 1);

            return sprite;
        }


        public void update()
        {
            scale(spHealth);
        }

        public void draw(RenderWindow win)
        {
            // work on a copy, instead of the original, for the original could be reused outside this scope


            // modify sprite, to fit it in the gui
            float viewScale = (float)view.Size.X / win.Size.X;

            spBG.Scale *= viewScale;
            spFG.Scale *= viewScale;
            spHealth.Scale *= viewScale;
            spBG.Position = view.Center - view.Size / 2F + spBG.Position * viewScale;
            spHealth.Position = view.Center - view.Size / 2F + spBG.Position * viewScale;
            spFG.Position = view.Center - view.Size / 2F + spBG.Position * viewScale;

            // draw the sprite
            win.Draw(spBG);
            win.Draw(spHealth);
            win.Draw(spFG);
        }


    } // end cl
}
