using System;
using System.Data;
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

namespace Tehtava9
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnHae_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                dataGrid.DataContext = Tietokanta.GetData();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnUusi_Click(object sender, RoutedEventArgs e)
        {
            stackPanel.Visibility = Visibility.Visible;
        }

        private void btnLuo_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtEnimi.Text) && !string.IsNullOrWhiteSpace(txtSnimi.Text) && !string.IsNullOrWhiteSpace(txtOsoite.Text) && !string.IsNullOrWhiteSpace(txtPostinro.Text) && !string.IsNullOrWhiteSpace(txtKaupunki.Text))
            {
                try
                {
                    Tietokanta.InsertData(txtEnimi.Text, txtSnimi.Text, txtOsoite.Text, txtPostinro.Text, txtKaupunki.Text);
                    MessageBox.Show("Lisäys onnistui");
                    dataGrid.DataContext = Tietokanta.GetData();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
                MessageBox.Show("Täytä kaikki kentät");
        }

        private void btnPoista_Click(object sender, RoutedEventArgs e)
        {
            if (dataGrid.SelectedIndex > 0)
            {
                DataRowView drv = (DataRowView)dataGrid.SelectedItem;
                string sNimi = drv.Row[2].ToString();
                string eNimi = drv.Row[1].ToString();
                string id = drv.Row[0].ToString();

                MessageBoxResult mbr = MessageBox.Show("Haluatko varmasti poistaa asiakkaan " + eNimi + " " + sNimi + "?", "Poista asiakas", MessageBoxButton.YesNo);
                if (mbr == MessageBoxResult.Yes)
                {
                    try
                    {
                        Tietokanta.DeleteData(id);
                        MessageBox.Show("Poisto onnistui");
                        dataGrid.DataContext = Tietokanta.GetData();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
        }
    }
}
