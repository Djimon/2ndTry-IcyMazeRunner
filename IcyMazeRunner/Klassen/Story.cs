using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IcyMazeRunner.Klassen
{
    class Story
    {
        String plot;
        String climax;
        String twist;
        String tragedy;
        String revelation;
        String Johnny ="Kevin" ;
        String Caroline ="Chantalle";
        String Villian = "Inkasso-Eintreiber";
        String Mentor = "Rudi";
        // neue Personen oder Konstruke, die einen Namen haben als eigenen String anlegen
        public void gesch() 
        {
            plot =
                "Das Dorf wurde heimgesucht von einem "+Villian+", der Angst und Misstrauen über die Bewohner brachte."
                + Johnny+" und seine kleine Schwester" +Caroline+" lebten allein in einer Alten Hütte, die ihrem Eltern gehört hatte."
                + "Diese sind bei einem Waldbrand ums Leben gekommen."
                + Johnny+" wacht eines Tages im Turm auf und weiß nicht wo er ist. Er kämpft sich durch die Labyrinthe und findet Hinweise von "+Mentor
                + "Alle Monstren auf seinem Weg reden wirres Zeug. Auf seiner Reise erlebt er viel: " 
                + " " //add stuff here
                ;
            climax =
                Johnny+ "sieht seine Schwester in der Ecke liegen, wird zornig und stürmt auf "
                + Villian + " zu, der unverständliches kauderwelsch von sich gibt und wildem Ausdruck seine arme schwingt. "
                +Johnny +" erldeigt den" +Villian+" und rennt zu seiner Schwester und weckt sie. "
                ;
            twist =
                Caroline+ "guckt verwirrt, sieht was geschehen ist und beginnt histerisch zu schreien. "
                +Johnny "fragt sie was los ist? "
                +Caroline+": 'Das war mein Freund. Hast du denn meinen Brief nicht gelesen?' und die ganzen Botschafter, die wir dir gesandt haben?"
                +" hast du sie alle getötet?'"
                +Johnny+"  wird einiges klar."
                // sinnvoller Grund, warum Johnny die Sprache der Monster(Botschafter) nicht verstand
                // sinnvoller Grund, warum Johnny den Brief nicht gelesen hat und im Turm aufwachrt.

                ;
                
            revelation = Johnny+" blinzelt, sieht sich um, ist leicht benommen. Er bemerkt langsam, dass das alles nur ein Traum war. "
            
            //Troll-Ende:   eine level, in dem alle auftauchenden Monster nochmal spawnen - endlos.
            //              hier kann sinnlos abgeschlachtet werden, uendlich HP, kein Cooldown
            //              Nebenbei laufen die Credits durch
        } 

        

        


    }
}
