using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml.Serialization;

namespace MMOTA_Test
{
    class functions
    {
        #region Wohnung #1
        public Level createWohnung(Player spieler,Story story)
        {
            Level level = new Level();
            Raum wohnzimmer = new Raum(); Raum bad = new Raum(); Raum schlafzimmer = new Raum();
            Raum flur = new Raum(); Raum kuche = new Raum(); Raum arbeitszimmer = new Raum();

            #region Flur
            flur.nebenraume[1] = wohnzimmer;

            #region objekte
            Objekt tuer = new Objekt();
            flur.addObjekt(tuer, "Tür","Meine Haustüre. Sie hat einige Kratzer an den Seiten, Ich sollte sie wohl nicht mehr so zuschlagen.");

            Objekt garderobe = new Objekt();
            flur.addObjekt(garderobe,"Garderobe","Die Garderobe, gut gefüllt mit meinen Mänteln und Schuhen. Ich sollte sie mal wieder aufräumen...Moment, da liegt etwas...ein Schlüssel!");
            garderobe.BeschreibungTook = "Immernoch meine Garderobe, immernoch gefüllt mit Mänteln und Schuhen und immernoch unordentlich...";

            Objekt schluessel = new Objekt();
            flur.addObjekt(schluessel, "Schlüssel", "Ein kleiner, rostiger Schlüssel.");
            schluessel.isTake = true;
            schluessel.isVisible = false;

            Objekt kl_tisch = new Objekt();
            flur.addObjekt(kl_tisch, "kl.Tisch", "Ein kleiner Tisch mitsamt meiner Vase drauf. Die Blume ist vertrocknet...brauch man mehr als nur Dünger?");

            Objekt portrait = new Objekt();
            flur.addObjekt(portrait, "Bild", "Das Bild zeigt eine alte Frau, die gekrümmt auf einem Stuhl sitzt. Sie sieht traurig aus und hat tiefe Augenringe, sie scheint Angst zu haben...");
            #endregion

            #region useable pairs
            schluessel.addUseablePair(schluessel, tuer);
            #endregion

            level.AddRoom(flur, "Flur");

            #region listeners
            story.addListenerObjekt(tuer,false);
            story.addListenerObjekt(garderobe, false);
            #endregion

            #endregion

            #region wohnzimmer
            wohnzimmer.nebenraume[0] = kuche;
            wohnzimmer.nebenraume[1] = schlafzimmer;
            wohnzimmer.nebenraume[2] = arbeitszimmer;
            wohnzimmer.nebenraume[3] = flur;
            level.AddRoom(wohnzimmer, "Wohnzimmer");
            #endregion

            spieler.Raum = flur;

            return level;
        }
        #endregion

        #region interpretiere eingabe
        public void interpretInput(string input,Player spieler)
        {
            #region keywords
            string[] keywords = {"use","look","take","move","roominfo","inventar"};

            #endregion

            if (input == "")
                Console.WriteLine("Bitte gib etwas ein!");

            string[] words = input.Split(' '); // use x with y => use,x,with,y in array
            if (keywords.Contains(words[0].ToString()))
            {
                #region roominfo
                if (words[0] == "roominfo")
                {
                    spieler.Raum.printInfo();
                    return;
                }
                #endregion

                #region inventar
                if (words[0] == "inventar")
                {
                    spieler.printInventar(spieler);
                    return;
                }
                #endregion

                if (words.Length > 1)
                {
                    #region look
                    if (words[0] == "look")
                    {
                        if (spieler.Raum.objekte.Keys.Contains(words[1]))
                        {
                            Console.WriteLine(spieler.Raum.objekte[words[1]].Beschreibung.ToString());
                            spieler.Raum.objekte[words[1]].isLooked = true;
                            return;
                        }
                    }
                    #endregion

                    #region move
                    if (words[0] == "move")
                    {
                        Dictionary<string,int> himmel = new Dictionary<string,int>();
                        himmel.Add("Norden",0); himmel.Add("Osten",1); himmel.Add("Süden",2); himmel.Add("Westen",3);
                        if (himmel.ContainsKey(words[1]))
                        {
                            if (spieler.Raum.nebenraume[himmel[words[1]]] != null)
                            {
                                spieler.Raum = spieler.Raum.nebenraume[himmel[words[1]]];
                                spieler.Raum.printInfo();
                            }
                            else
                                Console.WriteLine("Dieser Raum existiert nicht.");
                        }
                        else
                            Console.WriteLine("Das ist echt keine Himmelsrichtung!");
                        return;
                    }
                    #endregion

                    #region take
                    if (words[0] == "take")
                    {
                        if (spieler.Raum.objekte.ContainsKey(words[1]))
                        {
                            if (spieler.inventar.ContainsKey(words[1]))
                            {
                                Console.WriteLine("Du hast das bereits im Inventar.");
                                return;
                            }
                            else{
                                if (spieler.Raum.objekte[words[1]].isTake)
                                {
                                    if (spieler.Raum.objekte[words[1]].isVisible)
                                    {
                                        spieler.addObjekttoInventar(spieler.Raum.objekte[words[1]]);
                                        spieler.Raum.objekte[words[1]].isTook = true;
                                        spieler.Raum.objekte[words[1]].isVisible = false;
                                        Console.WriteLine(words[1] + " aufgenommen.");
                                        return;
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("Das kann Ich nicht nehmen!");
                                    return;
                                }
                            }
                        }
                        else
                        {
                            Console.WriteLine("Objekt ist im aktuellen Raum nicht vorhanden.");
                            return;
                        }
                    }
                    #endregion

                    #region use
                    if (words[0] == "use")
                    {
                        if (words.Length == 3)
                        {
                            if (spieler.inventar.ContainsKey(words[1]) || (spieler.Raum.objekte[words[1]].isUseableifNOTInventar == true))
                            {
                                if (spieler.Raum.objekte.ContainsKey(words[2]) || spieler.inventar.ContainsKey(words[2]))
                                {
                                    if (spieler.inventar.ContainsKey(words[1]))
                                        spieler.inventar[words[1]].useWith(spieler.Raum.objekte[words[2]]);
                                    else
                                        spieler.Raum.objekte[words[1]].useWith(spieler.Raum.objekte[words[2]]);
                                }
                                else
                                    Console.WriteLine(words[2].ToString() + " befindet sich weder im Inventar noch im Raum!");
                            }
                            else
                                Console.WriteLine(words[1].ToString() + " befindet sich nicht im Inventar!");
                        }
                        else
                            Console.WriteLine("Da fehlt jetzt irgendwie was...");
                    }
                    #endregion
                }
                else { Console.WriteLine("Bitte gib ein zweites Argument ein!"); }
            }
            else
            {
                Console.WriteLine(words[0].ToString() + " ist ein unbekannter Begriff.");
            }
        }
        #endregion

        #region serialisieren
        public void serialize(Player obj)
        {
            XmlSerializer ser = new XmlSerializer(typeof(Player));
            FileStream fs = new FileStream(@"ser.xml", FileMode.Create);
            ser.Serialize(fs, obj);
            fs.Close();
        }
        #endregion
    }
}
