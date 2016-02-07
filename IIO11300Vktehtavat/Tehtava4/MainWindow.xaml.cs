using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Tehtava4
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //luodaan kokoelma Pelaaja-olioille
        List<Pelaaja> pelaajat = new List<Pelaaja>();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void ComboBox_Loaded(object sender, RoutedEventArgs e)
        {
            // ... A List.
            List<string> seurat = new List<string>();
            seurat.Add("Blues");
            seurat.Add("HIFK");
            seurat.Add("HPK");
            seurat.Add("Ilves");
            seurat.Add("JYP");
            seurat.Add("KalPa");
            seurat.Add("KooKoo");
            seurat.Add("Kärpät");
            seurat.Add("Lukko");
            seurat.Add("Pelicans");
            seurat.Add("SaiPa");
            seurat.Add("Sport");
            seurat.Add("Tappara");
            seurat.Add("TPS");
            seurat.Add("Ässät");

            // ... Get the ComboBox reference.
            var seuraComboBox = sender as ComboBox;

            // ... Assign the ItemsSource to the List.
            seuraComboBox.ItemsSource = seurat;

            // ... Make the first item selected.
            seuraComboBox.SelectedIndex = 0;
        }

        private void uusiPelaajaButton_Click(object sender, RoutedEventArgs e)
        {
            //tarkistetaan että kaikissa kentissä on arvo 
            if (string.IsNullOrWhiteSpace(etunimiTextBox.Text) || string.IsNullOrWhiteSpace(sukunimiTextBox.Text) || string.IsNullOrWhiteSpace(siirtohintaTextBox.Text))
            {
                statusBar.Items.Clear();
                TextBlock txtb = new TextBlock();
                txtb.Text = "Täytä kaikki kentät.";
                statusBar.Items.Add(txtb);
            }
            else
            {
                //luodaan olio luokasta Pelaaja 
                Pelaaja uusiPelaaja = new Pelaaja(etunimiTextBox.Text, sukunimiTextBox.Text, float.Parse(siirtohintaTextBox.Text), seuraComboBox.Text);

                //tämän flägin avulla tarkistetaan samannimisen pelaajan olemassaolo kokoelmassa
                bool samaNimiFlag = false;

                //tarkistetaan ettei samannimistä pelaajaa ole jo Pelaajat -oliokokoelmassa
                foreach (Pelaaja vanhaPelaaja in pelaajat)
                {
                    if (uusiPelaaja.KokoNimi == vanhaPelaaja.KokoNimi)
                    {
                        samaNimiFlag = true;
                        statusBar.Items.Clear();
                        TextBlock txtb = new TextBlock();
                        txtb.Text = "Samanniminen pelaaja löytyy jo.";
                        statusBar.Items.Add(txtb);
                        break;
                    }
                }

                //jos kokoelma on tyhjä tai flägi on false (samannimistä pelaajaa ei löytynyt) -> luodaan uusi pelaaja
                if (pelaajat.Count == 0 || !samaNimiFlag)
                {
                    pelaajat.Add(uusiPelaaja);
                    statusBar.Items.Clear();
                    TextBlock txtb = new TextBlock();
                    txtb.Text = "Pelaaja lisätty.";
                    statusBar.Items.Add(txtb);
                }

                //kirjoitetaan kaikkien Pelaaja-olioiden EsitysNimi listBoxiin
                List<string> items = new List<string>();
                foreach (Pelaaja pelaaja in pelaajat)
                {
                    items.Add(pelaaja.EsitysNimi);
                }
                listBox.ItemsSource = items;
            }
        }

        private void lopetusButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void listBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            //jos mitään itemiä ei ole valittuna listBoxista, valitaan ensimmäinen itemi (jos sellainen on olemassa)
            if (listBox.SelectedItem == null)
            {
                if (listBox.Items.Count > 0)
                {
                    listBox.SelectedItem = listBox.Items.GetItemAt(0);
                }
                else
                {
                    return;
                }
            }

            //selvitetään, kuka pelaaja on tällä hetkellä valittuna listBoxista
            Pelaaja valittuPelaaja = new Pelaaja();
            foreach (Pelaaja pelaaja in pelaajat)
            {
                if (pelaaja.EsitysNimi == listBox.SelectedItem.ToString())
                {
                    valittuPelaaja = pelaaja;
                }
            }
            //kirjoitetaan valittuna olevan pelaajan tiedot kenttiin muokkausta varten
            etunimiTextBox.Text = valittuPelaaja.Etunimi;
            sukunimiTextBox.Text = valittuPelaaja.Sukunimi;
            siirtohintaTextBox.Text = valittuPelaaja.Siirtohinta.ToString();
            seuraComboBox.Text = valittuPelaaja.Seura;
        }

        private void talletaPelaajaButton_Click(object sender, RoutedEventArgs e)
        {
            //selvitetään, kuka pelaaja on tällä hetkellä valittuna listBoxista
            Pelaaja valittuPelaaja = new Pelaaja();
            foreach (Pelaaja pelaaja in pelaajat)
            {
                if (pelaaja.EsitysNimi == listBox.SelectedItem.ToString())
                {
                    valittuPelaaja = pelaaja;
                }
            }
            //kirjoitetaan uudet tiedot kentistä valitulle pelaajalle
            valittuPelaaja.Etunimi = etunimiTextBox.Text;
            valittuPelaaja.Sukunimi = sukunimiTextBox.Text;
            valittuPelaaja.Siirtohinta = float.Parse(siirtohintaTextBox.Text);
            valittuPelaaja.Seura = seuraComboBox.Text;

            //kirjoitetaan kaikkien Pelaaja-olioiden EsitysNimi listBoxiin
            List<string> items = new List<string>();
            foreach (Pelaaja pelaaja in pelaajat)
            {
                items.Add(pelaaja.EsitysNimi);
            }
            listBox.ItemsSource = items;

            statusBar.Items.Clear();
            TextBlock txtb = new TextBlock();
            txtb.Text = "Pelaajan tiedot tallennettu.";
            statusBar.Items.Add(txtb);
        }

        private void poistaPelaajaButton_Click(object sender, RoutedEventArgs e)
        {
            //selvitetään, kuka pelaaja on tällä hetkellä valittuna listBoxista
            Pelaaja valittuPelaaja = new Pelaaja();
            foreach (Pelaaja pelaaja in pelaajat)
            {
                if (pelaaja.EsitysNimi == listBox.SelectedItem.ToString())
                {
                    valittuPelaaja = pelaaja;
                }
            }
            //poistetaan valittu pelaaja kokoelmasta
            pelaajat.Remove(valittuPelaaja);

            //kirjoitetaan kaikkien Pelaaja-olioiden EsitysNimi listBoxiin
            List<string> items = new List<string>();
            foreach (Pelaaja pelaaja in pelaajat)
            {
                items.Add(pelaaja.EsitysNimi);
            }
            listBox.ItemsSource = items;

            statusBar.Items.Clear();
            TextBlock txtb = new TextBlock();
            txtb.Text = "Pelaaja poistettu.";
            statusBar.Items.Add(txtb);
        }

        private void kirjoitaPelaajatButton_Click(object sender, RoutedEventArgs e)
        {
            //kutsu tallennusmetodia
            try
            {
                Pelaaja.SaveDataToFile(pelaajat, @"D:\Pelaajat.txt");

                statusBar.Items.Clear();
                TextBlock txtb = new TextBlock();
                txtb.Text = @"Tiedot kirjoitettu onnistuneesti tiedostoon D:\Pelaajat.txt";
                statusBar.Items.Add(txtb);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
