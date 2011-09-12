using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;

namespace MMOTA_Test
{
    public class loops
    {
        #region startup und level auswählen

        #region Comment
        /*Hier wird gestartet und man hat die Wahl zwischen 0 und 1 (verlassen und laden eines Levels)
         * Sobald man ein Keyword eingegben hat, wird die while verlassen und danach ausgelesen
         * Filter für Dateien ist .tl
         * Suchtiefe ist nicht implementiert, einfach /level ordner nehmen
         */
        #endregion

        public void startup_loop()
        {
            #region objekte erstellen
            functions func = new functions();
            #endregion

            #region hauptteil
            Console.WriteLine("Wenigstens läuft es...\nWas möchtest du machen?\n 0 = Verlassen | 1 = Level auswählen");
            string key = Console.ReadLine().ToString();

            #region warte auf entscheidung (verlassen|laden)
            while (1 == 1)
            {
                if (key == "0")
                    break;
                if (key == "1")
                    break;
                Console.WriteLine(key + " ist weder 0 noch 1\n\nFalsche Eingabe!\n 0 = Verlassen | 1 = Level auswählen");
                key = Console.ReadLine().ToString();
            }
            #endregion

            #region verlassen oder alle datei einlesen
            if (key == "0")
                return;
            if (key == "1")
            {
                string path = "";
                string exedir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                Console.Clear();
                Console.WriteLine("Programm ausgeführt in:\n" + exedir);
                Console.WriteLine("Wähle dein Level oder schreib return:\n");
                string[] files = Directory.GetFiles(exedir,"*.tl");
                for (int i = 0; i < files.Length; i++)
                {
                    files[i] = Path.GetFileName(files[i]).ToString();
                    Console.WriteLine(i + " - " + files[i]);
                }
                #region select a file
                while (1 == 1) //While no file is selected
                {
                    string sel_file = Console.ReadLine();
                    if (sel_file == "return")
                    {
                        Console.Clear();
                        startup_loop();
                    }
                    try {
                        if (Convert.ToInt16(sel_file) < files.Length)
                        {
                            path = files[Convert.ToInt16(sel_file)].ToString();
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Bitte gültige Zahl eingeben!");
                        }
                    }
                    catch (Exception){Console.WriteLine("Bitte Zahl eingeben!");}
                }
                #endregion


            }
            #endregion

            #endregion
        }
        #endregion
    }
}
