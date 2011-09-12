using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace MMOTA_Test
{
    public class Player
    {
        #region Daten
        Raum m_position = new Raum();
        Dictionary<string, Objekt> m_inventar = new Dictionary<string, Objekt>();
        string m_name = "Spieler";
        #endregion

        #region get/set
        public string Name
        {
            get { return m_name; }
            set { m_name = value; }
        }
        public Raum Raum
        {
            get { return m_position; }
            set { m_position = value; }
        }
        public Dictionary<string, Objekt> inventar
        {
            get { return m_inventar; }
            set { m_inventar = value; }
        }
        #endregion

        #region functions
        public void deleteObjektfromInventar(Objekt obj)
        {
            m_inventar.Remove(obj.Name);
        }
        public void addObjekttoInventar(Objekt obj)
        {
            if (m_inventar.ContainsKey(obj.Name))
                Console.WriteLine("Das habe Ich schon im Inventar!");
            else
                m_inventar.Add(obj.Name, obj); 
       
        }

        public void printInventar(Player spieler)
        {
            Console.WriteLine("Objekte im Inventar:\n");
            for (int i = 0; i < this.m_inventar.Count; i++)
                Console.WriteLine("-"+m_inventar.ElementAt(i).Key.ToString());
        }
        
        #endregion
    }
}
