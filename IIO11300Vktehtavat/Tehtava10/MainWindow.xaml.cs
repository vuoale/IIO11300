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

namespace Tehtava10
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<Pelaaja> pelaajat;

        public MainWindow()
        {
            InitializeComponent();
            GetData();
        }

        private void btnHae_Click(object sender, RoutedEventArgs e)
        {
            GetData();
        }

        private void btnTallenna_Click(object sender, RoutedEventArgs e)
        {
            //valittu Pelaaja-olio tallennetaan kantaan
            try
            {
                Pelaaja current = (Pelaaja)spPelaaja.DataContext;
                Liiga.UpdatePelaaja(current);
                MessageBox.Show(string.Format("Pelaaja {0} päivitetty kantaan onnistuneesti", current.ToString()));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void lbPelaajat_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //listboxissa valittu olio asetetaan stackpanelin datacontekstiksi
            spPelaaja.DataContext = lbPelaajat.SelectedItem;
        }

        private void GetData()
        {
            try
            {
                //haetaan pelaajat tietokannasta ORM = muutetaan pelaajat Pelaaja-olioiksi
                pelaajat = Liiga.GetPelaajat();
                lbPelaajat.DataContext = pelaajat;
                lbPelaajat.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
