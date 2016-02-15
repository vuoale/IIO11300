using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace Tehtava5
{
    public class Pelaaja
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
        public static bool SaveDataToFile(List<Pelaaja> pelaajat)
        {
            try
            {
                //dialogin avaus
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.FileName = "Pelaajat";
                sfd.InitialDirectory = "d:\\";
                sfd.Filter = "Tekstitiedostot .txt|*.txt|Xml-tiedostot .xml|*.xml|All files|*.*";
                Nullable<bool> result = sfd.ShowDialog();

                if (result == true)
                {
                    string fileName = sfd.FileName;

                    //selvitetään tiedostomuoto
                    string fileFormat = Path.GetExtension(fileName);

                    //teksitiedostoon tallennus
                    if (fileFormat == ".txt")
                    {                      
                        using (StreamWriter sw = new StreamWriter(fileName))
                        {
                            //käydään kokoelma läpi ja kirjoitetaan kukin pelaaja omalle riville tekstitiedostoon
                            foreach (var pelaaja in pelaajat)
                            {
                                sw.WriteLine(pelaaja.Etunimi + ";" + pelaaja.Sukunimi + ";" + pelaaja.Siirtohinta + ";" + pelaaja.Seura);
                            }
                        }
                        return true;
                    }

                    //xml-filuun tallennus
                    if (fileFormat == ".xml")
                    {                
                        using (XmlWriter xw = XmlWriter.Create(fileName))
                        {
                            //serialisoidaan kokoelma xml:äksi ja kirjoitetaan xml-filuun
                            XmlSerializer serializer = new XmlSerializer(pelaajat.GetType());
                            serializer.Serialize(xw, pelaajat);
                        }
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return false;    
        }

        public static List<Pelaaja> ReadDataFromFile()
        {
            try
            {
                //dialogin avaus
                OpenFileDialog ofd = new OpenFileDialog();
                ofd.InitialDirectory = "d:\\";
                ofd.Filter = "Tekstitiedostot .txt|*.txt|Xml-tiedostot .xml|*.xml|All files|*.*";
                Nullable<bool> result = ofd.ShowDialog();

                if (result == true)
                {
                    string fileName = ofd.FileName;

                    //selvitetään tiedostomuoto
                    string fileFormat = Path.GetExtension(fileName);

                    //tekstitiedostosta lukeminen
                    if (fileFormat == ".txt")
                    {
                        if (File.Exists(fileName))
                        {
                            using (StreamReader sr = File.OpenText(fileName))
                            {
                                //luetaan rivi kerrallaan tekstitiedostoa
                                Pelaaja p;
                                List<Pelaaja> pelaajat = new List<Pelaaja>();
                                string rivi = "";
                                while ((rivi = sr.ReadLine()) != null)
                                {
                                    //pilkotaan rivi puolipisteiden kohdalta ja parsitaan hinta kolmannesta sarakkeesta (float)
                                    string[] split = rivi.Split(new char[] { ';' });
                                    float hinta = float.Parse(split[2]);

                                    //luodaan tekstinpätkistä olio
                                    p = new Pelaaja(split[0], split[1], hinta, split[3]);
                                    pelaajat.Add(p);
                                }
                                return pelaajat;
                            }
                        }
                        else
                        {
                            throw new FileNotFoundException();
                        }
                    }
                    //xml-filusta lukeminen
                    if (fileFormat == ".xml")
                    {
                        if (File.Exists(fileName))
                        {                      
                            using (XmlReader xw = XmlReader.Create(fileName))
                            {
                                //luetaan xml-filu ja deserialisoidaan se Pelaaja-olioita sisältäväksi oliokokoelmaksi
                                List<Pelaaja> pelaajat = new List<Pelaaja>();
                                XmlSerializer serializer = new XmlSerializer(pelaajat.GetType());
                                pelaajat = serializer.Deserialize(xw) as List<Pelaaja>;
                                return pelaajat;
                            }                  
                        }
                        else
                        {
                            throw new FileNotFoundException();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return null;
        }
        #endregion
    }
}
