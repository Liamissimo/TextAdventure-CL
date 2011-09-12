using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace MMOTA_Test
{
    public class Objekt
    {
        #region daten
        string m_name = ""; //name des objektes
        bool m_isvisible = true;
        string m_beschreibung = ""; //Beschreibung des objektes
        string m_beschreibung_took = "";
        bool m_cantake = false; //Werte um zu sehen, was man mit objekt alles machen kann
        bool m_canuse = false; // s.o.
        Dictionary<Objekt, bool> m_usewithobject = new Dictionary<Objekt, bool>(); //liste der objekte, mit denen man es benutzen kann
        bool m_took = false;
        bool m_used = false;
        bool m_looked = false;
        bool m_useableifnotininventar = false;
        #endregion

        #region get/set
        public string Name
        {
            get { return m_name; }
            set { m_name = value; }
        }
        public string Beschreibung
        {
            get {
                if (!m_took)
                    return m_beschreibung;
                else
                    return m_beschreibung_took;

            }
            set { m_beschreibung = value; }
        }
        public string BeschreibungTook
        {
            //Gibt nichts zurück, aus Sauberkeitsgründen bitte Beschreibung nehmen, eingebauter Mechanismus
            set { m_beschreibung_took = value; }
        }
        public bool isVisible
        {
            get { return m_isvisible; }
            set { m_isvisible = value; }
        }
        public bool isUseableifNOTInventar
        {
            get { return m_useableifnotininventar; }
            set { m_useableifnotininventar = value; }
        }
        public bool isTake
        {
            get { return m_cantake; }
            set { m_cantake = value; }
        }
        public bool isUse
        {
            get { return m_canuse; }
            set { m_canuse = value; }
        }
        public bool isTook
        {
            get { return m_took; }
            set { m_took = value; }
        }
        public bool isUsed
        {
            get { return m_used; }
            set { m_used = value; }
        }
        public bool isLooked
        {
            get { return m_looked; }
            set { m_looked = value; }
        }
        public void addUseableObject(Objekt obj)
        {
            m_usewithobject.Add(obj, true);
        }
        public void addUseablePair(Objekt obj1, Objekt obj2)
        {
            obj1.addUseableObject(obj2);
            obj2.addUseableObject(obj1);
        }
        public void useWith(Objekt obj)
        {
            if (m_usewithobject.ContainsKey(obj)) //Damit keine Exception kommt
            {
                if ((m_usewithobject[obj] == true) || (obj.m_useableifnotininventar == true))
                {
                    Console.WriteLine(Name + " mit " + obj.Name + " verwendet!");
                    obj.isUsed = true;
                    this.isUsed = true;
                }
                else
                    Console.WriteLine("Das geht so nicht...");
            }
            else Console.WriteLine("Das geht so nicht...");
        }
        #endregion
    }
}
