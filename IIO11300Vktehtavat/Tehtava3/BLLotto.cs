using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JAMK.IT.IIO11300
{
    public class Lotto
    {
        //Lottorivin tyyppi: Suomi, VikingLotto tai Eurojackpot
        private string tyyppi;

        public string Tyyppi
        {
            get { return tyyppi; }
            set { tyyppi = value; }
        }

        //Montako riviä arvotaan
        private int riviLkm;

        public int RiviLkm
        {
            get { return riviLkm; }
            set { riviLkm = value; }
        }

        //Rivien arvonta metodi
        public List<string> ArvoRivi()
        {
            //Lista kaikista arvotuista riveistä
            List<string> rivit = new List<string>();

            //Lista numeroista yhdessä rivissä
            List<int> riviNumerot = new List<int>();

            Random random = new Random();

            //Numeroiden lukumäärä ja suurin arvo loton tyypin mukaan
            int numLkm = 7; int maxNum = 40; //Suomi
            if (Tyyppi == "VikingLotto") { numLkm = 6; maxNum = 49; } //VikingLotto
            else if (Tyyppi == "Eurojackpot") { numLkm = 5; maxNum = 51; } //Eurojackpot

            //Ulompi loop: arvotaan niin monta riviä kuin käyttäjä haluaa
            for (int i = 0; i < RiviLkm; i++)
            {
                //Sisempi loop: arvotaan yhteen riviin numeroita sen verran kuin loton tyyppi määrää
                for (int j = 0; j < numLkm; j++)
                {
                    //Arvotaan random numero loton tyypin määräämältä numero alueelta
                    int randomNumero = random.Next(1, maxNum);

                    //Tarkistetaan, ettei sama numero tule riviin kahdesti
                    for (int k = 0; k < riviNumerot.Count; k++)
                    {
                        if (randomNumero == riviNumerot[k])
                        {
                            randomNumero = random.Next(1, maxNum);
                            k = 0;
                        }
                    }

                    //Lisätään arvottu random numero riviin
                    riviNumerot.Add(randomNumero);
                }
                //Yhdistetään yhden rivin kaikki numerot stringiksi pilkulla eroteltuna
                string rivi = string.Join(",", riviNumerot.ToArray());

                //Lisätään stringi listaan, joka sisältää kaikki rivit
                rivit.Add(rivi);

                //Tyhjennetään lista numeroista yhdessä rivissä
                riviNumerot.Clear();
            }

            //Palautetaan lista kaikista arvotuista riveistä
            return rivit;
        }

        //Tähtinumeroiden arvonta metodi
        //Huonosti toteutettu, saman koodin toistamista...
        //Olisi pitänyt tehdä vain yksi arvonta-metodi, jolle välitetään parametrit, mutta aikaa kului tehtävään jo niin kauan, etten jaksanut enää alkaa muuttamaan...
        public List<string> ArvoTahti()
        {
            List<string> rivit = new List<string>();
            List<int> riviNumerot = new List<int>();
            Random random = new Random();
            int numLkm = 2; int maxNum = 9;
            for (int i = 0; i < RiviLkm; i++)
            {
                for (int j = 0; j < numLkm; j++)
                {
                    int randomNumero = random.Next(1, maxNum);
                    for (int k = 0; k < riviNumerot.Count; k++)
                    {
                        if (randomNumero == riviNumerot[k])
                        {
                            randomNumero = random.Next(1, maxNum);
                            k = 0;
                        }
                    }
                    riviNumerot.Add(randomNumero);
                }
                string rivi = string.Join(",", riviNumerot.ToArray());
                rivit.Add(rivi);
                riviNumerot.Clear();
            }
            return rivit;
        }
    }
}
