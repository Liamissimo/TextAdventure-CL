using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace MMOTA_Test
{
    class Program
    {
        static void Main(string[] args)
        {
            #region console anpassen
            Console.Title = "MMOTA - Pre-Alpha";
            Console.WindowWidth = 100;
            #endregion

            #region objekte erstellen
            functions func = new functions();
            loops loops = new loops();
            Player spieler = new Player();
            Story story = new Story();
            #endregion


            #region wohnung
            Level wohnung = func.createWohnung(spieler,story);
            spieler.Raum.printInfo();

            #endregion

            while (1 == 1)
            {
                Console.WriteLine("");
                Console.Write(">>");
                string input = Console.ReadLine();
                if (input == "exit")
                    return;
                if (input == "serial")
                    func.serialize(spieler);
                func.interpretInput(input, spieler);
                story.story_listener(spieler);
            }
 
        }
    }
}
