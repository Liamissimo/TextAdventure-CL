using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace MMOTA_Test
{
    public class Raum
    {
        #region daten
        Raum[] m_o_nebenraum = {null,null,null,null}; //N-O-S-W
        Dictionary<string,Objekt> m_objekte = new Dictionary<string,Objekt>();
   
        string m_s_raumname = "";
        #endregion

        #region public sachen
        public Raum[] nebenraume
        {
            get
            {
                return m_o_nebenraum;
            }
            set
            {
                m_o_nebenraum = value;
            }
        }
        public string raumname
        {
            get
            {
                return m_s_raumname;
            }
            set
            {
                m_s_raumname = value;
            }
        }
        public Dictionary<string,Objekt> objekte
        {
            get { return m_objekte; }
            set { m_objekte = value; }
        }
        public void deleteObjekt(Objekt obj)
        {
            m_objekte.Remove(obj.Name);
        }
        public void addObjekt(Objekt obj,string objektname, string beschreibung)
        {
            obj.Name = objektname;
            obj.Beschreibung = beschreibung;
            m_objekte.Add(objektname, obj);
        }
        public void printNebenraeume()
        {
            string[] himmel = {"Norden","Osten","Süden","Westen"};
            for (int i = 0; i < nebenraume.Length; i++)
                if(this.nebenraume[i] != null)
                Console.WriteLine(himmel[i] + ":\t " + this.nebenraume[i].raumname.ToString());
        }
        public void printObjekte()
        {
            for (int i = 0; i < m_objekte.Count; i++)
            {
                if(m_objekte.ElementAt(i).Value.isTook == false)
                    if(m_objekte.ElementAt(i).Value.isVisible == true)
                        Console.WriteLine("-" + m_objekte.ElementAt(i).Key.ToString());
            }
        }
        public void printInfo()
        {
            Console.WriteLine("-----------------------------------------------------------");
            Console.WriteLine("Du bist im " + this.raumname);
            Console.WriteLine("\n");
            Console.WriteLine("Räume in die Du gehen kannst");
            this.printNebenraeume();
            Console.WriteLine("\n");
            Console.WriteLine("Folgende Objekte befinden sich hier: ");
            this.printObjekte();
            Console.WriteLine("-----------------------------------------------------------");
        }
        #endregion
    }
}
