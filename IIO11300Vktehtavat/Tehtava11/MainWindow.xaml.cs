using System;
using System.Collections.Generic;
using System.Data.Entity;
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

namespace Tehtava11
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        SMLiigaEntities ctx;

        public MainWindow()
        {
            InitializeComponent();
            IniMyStuff();
        }

        public void IniMyStuff()
        {
            ctx = new SMLiigaEntities();
            ctx.Pelaajat.Load();
            GetData();
        }

        private void btnHae_Click(object sender, RoutedEventArgs e)
        {
            GetData();
        }

        private void btnTallenna_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ctx.SaveChanges();            
                GetData();
                MessageBox.Show("Tallennettu kantaan.");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void lbPelaajat_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            spPelaaja.DataContext = lbPelaajat.SelectedItem;
        }

        private void GetData()
        {
            try
            {
                List<Pelaaja> pelaajat = ctx.Pelaajat.ToList();
                lbPelaajat.DataContext = pelaajat;
                lbPelaajat.DisplayMemberPath = "Kokonimi";
                lbPelaajat.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnLuo_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Pelaaja uusiPelaaja;
                if (btnLuo.Content.ToString() == "Luo uusi pelaaja")
                {
                    uusiPelaaja = new Pelaaja();
                    spPelaaja.DataContext = uusiPelaaja;
                    btnLuo.Content = "Lisää kantaan";
                }
                else
                {
                    uusiPelaaja = (Pelaaja)spPelaaja.DataContext;
                    ctx.Pelaajat.Add(uusiPelaaja);
                    ctx.SaveChanges();
                    btnLuo.Content = "Luo uusi pelaaja";
                    GetData();
                    MessageBox.Show("Pelaaja " + uusiPelaaja.Kokonimi + " lisätty kantaan.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnPoista_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Pelaaja current = (Pelaaja)spPelaaja.DataContext;
                MessageBoxResult retval = MessageBox.Show("Haluatko varmasti poistaa pelaajan " + current.Kokonimi, "Poista pelaaja", MessageBoxButton.YesNo);
                if (retval == MessageBoxResult.Yes)
                {
                    ctx.Pelaajat.Remove(current);
                    ctx.SaveChanges();
                    GetData();
                    MessageBox.Show("Pelaaja " + current.Kokonimi + " poistettu.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}