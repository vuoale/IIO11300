using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
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

namespace Tehtava2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            // selvitetään kuluvan viikon numero ja laitetaan se oletus tiedoston nimeksi
            var culture = CultureInfo.CurrentCulture;
            var dateTimeInfo = DateTimeFormatInfo.GetInstance(culture);
            var dateTime = DateTime.Today;
            int weekNumber = culture.Calendar.GetWeekOfYear(dateTime, dateTimeInfo.CalendarWeekRule, dateTimeInfo.FirstDayOfWeek);
            txtFileName.Text = @"D:\Lottorivit" + weekNumber + ".txt";
        }

        private void ComboBox_Loaded(object sender, RoutedEventArgs e)
        {
            // ... A List.
            List<string> data = new List<string>();
            data.Add("Suomi");
            data.Add("VikingLotto");
            data.Add("Eurojackpot");

            // ... Get the ComboBox reference.
            var comboBox = sender as ComboBox;

            // ... Assign the ItemsSource to the List.
            comboBox.ItemsSource = data;

            // ... Make the first item selected.
            comboBox.SelectedIndex = 0;
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                JAMK.IT.IIO11300.Lotto lotto = new JAMK.IT.IIO11300.Lotto();
                lotto.RiviLkm = int.Parse(numberTextBox.Text);
                lotto.Tyyppi = comboBox.SelectedValue.ToString();
                textBox.Text = string.Join(Environment.NewLine, lotto.ArvoRivi());
                if (lotto.Tyyppi == "Eurojackpot")
                {
                    textBox2.Text = string.Join(Environment.NewLine, lotto.ArvoTahti());
                }
                else textBox2.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnTallenna_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //kirjoitetaan annettu teksti tiedostoon
                string filename = txtFileName.Text;
                if (filename.Length > 0)
                {
                    using (StreamWriter sw = File.CreateText(filename))
                    {
                        sw.WriteLine(textBox.Text);
                    }
                    MessageBox.Show("Rivit tallennettu onnistuneesti!");
                }   
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnTarkista_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // otetaan annettu oikea rivi ylös ja laitetaan sen (pilkulla erotellut) luvut taulukon paikkoihin
                string oikeaRivi = txtOikeaRivi.Text;
                int[] oikeatNumerot = Array.ConvertAll(oikeaRivi.Split(','), int.Parse);

                // alustetaan muuttujia
                int matches = 0;
                int riviNro = 0;

                // luetaan kaikki rivit tiedostosta
                var lines = File.ReadLines(txtFileName.Text);
                foreach (var line in lines)
                // käsitellään yksi rivi kerrallaan
                {
                    // laitetaan rivin (pilkulla erotellut) luvut taulukon paikkoihin
                    int[] arvotutNumerot = Array.ConvertAll(line.Split(','), int.Parse);

                    // lasketaan kahdessa taulukossa esiintyvien samojen lukujen määrä
                    matches = arvotutNumerot.Intersect(oikeatNumerot).Count();

                    // rivi käsitelty -> kasvatetaan rivinroa
                    riviNro++;

                    // näytetään tällä rivillä olleiden oikeiden lukujen määrä
                    MessageBox.Show("Rivi " + riviNro + " oikein: " + matches.ToString());

                    // resetoidaan seuraavaan rivin käsittelyä varten
                    matches = 0;
                } 
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
