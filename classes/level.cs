using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace MMOTA_Test
{
    public class Level
    {

        #region addroom
        public void AddRoom(Raum raum, string raumname)
        {
            raum.raumname = raumname;
            m_l_raueme.Add(raumname, raum);
        }
        #endregion

        #region cleanup
        public void Cleanup()
        {
            m_s_raw_level = "";
            //m_raum_dict.Clear();
            m_l_raueme.Clear();
        }
        #endregion

        #region Daten
        string m_s_raw_level; //s_raw_level ist das Grundlevel, welches interpretiert wird
        //Dictionary<Raum, int> m_raum_dict = new Dictionary<Raum, int>(100);
        Dictionary<string,Raum> m_l_raueme = new Dictionary<string,Raum>(100); //100 räume sollten reichen
        #endregion

        #region get/set
        public string raw_level
        {
            get { return m_s_raw_level; }
            set { m_s_raw_level = value; }
        }
        public Dictionary<string,Raum> rooms
        {
            get { return m_l_raueme; }
            set { m_l_raueme = value; }
        } 
        //public Dictionary<Raum, int> roombyint
        //{
        //    get { return m_raum_dict; }
        //    set { m_raum_dict = value; }
        //}
        #endregion
    }
}
