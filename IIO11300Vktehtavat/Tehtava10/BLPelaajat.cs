using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tehtava10
{
    [Serializable]
    public class Pelaaja
    {
        #region PROPERTIES

        private int id;
        public int Id
        {
            //huom readonly
            get { return id; }
        }

        private string etunimi;
        public string Etunimi
        {
            get { return etunimi; }
            set { etunimi = value; }
        }

        private string sukunimi;
        public string Sukunimi
        {
            get { return sukunimi; }
            set { sukunimi = value; }
        }

        private string seura;
        public string Seura
        {
            get { return seura; }
            set { seura = value; }
        }

        private int arvo;
        public int Arvo
        {
            get { return arvo; }
            set { arvo = value; }
        }

        #endregion
        #region CONSTRUCTORS
        public Pelaaja(int id)
        {
            this.id = id;
        }

        public Pelaaja(int id, string etunimi, string sukunimi, string seura, int arvo)
        {
            this.id = id;
            this.etunimi = etunimi;
            this.sukunimi = sukunimi;
            this.seura = seura;
            this.arvo = arvo;
        }
        #endregion
        #region METHODS
        public override string ToString()
        {
            return etunimi + " " + sukunimi + ", " + seura;
        }
        #endregion
    }
    public class Liiga
    {
        private static string cs = Tehtava10.Properties.Settings.Default.Tietokanta;

        public static List<Pelaaja> GetPelaajat()
        {
            try
            {
                DataTable dt;
                List<Pelaaja> temp = new List<Pelaaja>();
                //haetaan pelaajia, db-kerroksen palauttama datatable mapataan olioiksi eli tehdään ORM
                dt = DBPelaajat.GetPlayers(cs);
                //tehdään ORM eli DataTablen rivit muutetaan olioiksi
                Pelaaja pelaaja;
                foreach (DataRow dr in dt.Rows)
                {
                    pelaaja = new Pelaaja(Int32.Parse(dr["id"].ToString()));
                    pelaaja.Etunimi = dr["etunimi"].ToString();
                    pelaaja.Sukunimi = dr["sukunimi"].ToString();
                    pelaaja.Seura = dr["seura"].ToString();
                    pelaaja.Arvo = Int32.Parse(dr["arvo"].ToString());
                    //olio lisätään kokoelmaan
                    temp.Add(pelaaja);
                }
                return temp;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static void UpdatePelaaja(Pelaaja pelaaja)
        {
            try
            {
                int lkm = DBPelaajat.UpdatePelaaja(cs, pelaaja.Id, pelaaja.Etunimi, pelaaja.Sukunimi, pelaaja.Seura, pelaaja.Arvo);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
