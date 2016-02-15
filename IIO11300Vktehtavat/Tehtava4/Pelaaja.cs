using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tehtava4
{
    class Pelaaja
    {
        //Properties
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

        private float siirtohinta;

        public float Siirtohinta
        {
            get { return siirtohinta; }
            set { siirtohinta = value; }
        }


        //Read-only Properties
        public string KokoNimi { get { return etunimi + sukunimi; } }
        public string EsitysNimi { get { return etunimi + " " + sukunimi + ", " + seura; } }


        #region CONSTRUCTORS
        public Pelaaja()
        {
            this.etunimi = "";
            this.sukunimi = "";
            this.siirtohinta = 0;
            this.seura = "";
        }

        public Pelaaja(string etunimi, string sukunimi, float siirtohinta, string seura)
        {
            this.etunimi = etunimi;
            this.sukunimi = sukunimi;
            this.siirtohinta = siirtohinta;
            this.seura = seura;
        }
        #endregion

        #region METHODS
        public static void SaveDataToFile(List<Pelaaja> dataa, string filu)
        {
            //kirjoitetaan data tiedostoon, jos tiedosto on jo olemassa liitetään se olemassaolevaan
            try
            {
                //tutkitaan onko tiedosto olemassa
                if (!System.IO.File.Exists(filu))
                {
                    //luodaan uusi
                    using (StreamWriter sw = File.CreateText(filu))
                    {
                        //käydään kokoelma läpi ja kirjoitetaan kukin pelaaja omalle riville
                        foreach (var pelaaja in dataa)
                        {
                            sw.WriteLine(pelaaja.Etunimi + ";" + pelaaja.Sukunimi + ";" + pelaaja.Siirtohinta + ";" + pelaaja.Seura);
                        }
                    }
                }
                else
                {
                    //lisätään olemassaolevaan tiedostoon
                    using (StreamWriter sw = File.AppendText(filu))
                    {
                        foreach (var pelaaja in dataa)
                        {
                            sw.WriteLine(pelaaja.Etunimi + ";" + pelaaja.Sukunimi + ";" + pelaaja.Siirtohinta + ";" + pelaaja.Seura);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
    }
}
