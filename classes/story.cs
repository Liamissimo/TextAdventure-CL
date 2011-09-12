using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MMOTA_Test
{
    public class Story
    {
        Dictionary<Objekt, bool> m_hashappend = new Dictionary<Objekt, bool>();

        public void story_listener(Player spieler)
        {
            /*Jetzt wirds lustig...
             * Also, die Scheiße hier schaut sich nach jeder abgelaufenen
             * Aktion Schlüssselvariablen an (die ihr hier bitte reinschreibt!!!)
             * und wenn diese sich so wie ihr es hier reingeschrieben habt
             * verändern, wird die Aktion ausgelöst! Soweit klar? Einfach ein Filter
             * damit ihr euch nicht mehr ins Hemd machen müsst und alle Sachen versaut 
             * und irgendwo im QC versteckt!
             */

            #region schlüssel im schrank nach looked @ flur
            if (spieler.Raum.raumname == "Flur")
            {
                if (spieler.Raum.objekte["Garderobe"].isLooked)
                {
                    if (!m_hashappend[spieler.Raum.objekte["Garderobe"]])
                    {
                        spieler.Raum.objekte["Schlüssel"].isVisible = true;
                    }
                }
            }
            #endregion

            #region schlüssel tür @ flur
            if (spieler.Raum.raumname == "Flur")
            {
                if (spieler.Raum.objekte["Tür"].isUsed)
                {
                    if (!m_hashappend[spieler.Raum.objekte["Tür"]])
                    {
                        Console.WriteLine("");
                        Console.WriteLine("Ich versuche meine Tür aufzuschließen. Der Schlüssel klemmt...Ich drücke härter und er bricht durch.\nJetzt hängt der Rest des Schlüssels drin...Mist!");
                        spieler.deleteObjektfromInventar(spieler.inventar["Schlüssel"]);
                        m_hashappend[spieler.Raum.objekte["Tür"]] = true;
                        spieler.Raum.objekte["Tür"].Beschreibung = "Meine Tür mitsamt abgebrochenem Schlüssel.";
                        return;
                    }
                }
            }
            #endregion
        }

        public void addListenerObjekt(Objekt obj, bool hashappend)
        {
            m_hashappend.Add(obj, hashappend);
        }
    }
}
