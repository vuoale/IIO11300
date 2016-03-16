using System;
using System.Data;  //sisältää ADO;n perusluokat
using System.Data.SqlClient; //SQL Server spesifiset luokat
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

namespace Tehtava8
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DataTable dt;
        DataView dv;
        List<string> kaupungit;

        public MainWindow()
        {
            InitializeComponent();
            GetData();
            //IniMyStuff();
        }

        private void IniMyStuff()
        {
            //asetetaan kaupunkien nimet ComboBoxiin
            kaupungit = new List<string>();
            //VE1 kaupunkien nimet kovakoodattu
            //kaupungit.Add("Jyväskylä");
            //kaupungit.Add("Helsinki");
            //kaupungit.Add("New York");
            //VE2 käydään loopittamalla DataTable läpi
            string kaupunki = "";
            foreach (DataRow item in dt.Rows)
            {
                kaupunki = item[3].ToString();
                //lisätään kukin kaupunki vain kerran listaan
                if (!kaupungit.Contains(kaupunki))
                    kaupungit.Add(kaupunki);     
            }
            //VE3 LINQ:lla voi tehdä kyselyn tyypitettyyn DataTableen, huom ei kaikille DataTableille
            //joten ei toimi tässä
            //var result = (from c in dt
            //              select c.City).Distinct();
            //databindaus
            cbKaupungit.ItemsSource = kaupungit;
        }

        private void GetData()
        {
            try
            {
                using (SqlConnection conn =
                  new SqlConnection(Tehtava8.Properties.Settings.Default.Tietokanta))
                {
                    //yhteys
                    //dataadapter
                    string sql = "SELECT firstname, lastname, address, city FROM vCustomers";
                    SqlDataAdapter da = new SqlDataAdapter(sql, conn);
                    dt = new DataTable("dt");
                    da.Fill(dt);
                    dv = dt.DefaultView;
                    //sidotaan datatable UI-kontrolliin
                    listBox.DataContext = dt;
                    conn.Close();
                    IniMyStuff();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnHaeAsiakkaat_Click(object sender, RoutedEventArgs e)
        {
            GetData();
        }

        private void cbKaupungit_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //asetetaan DataView:llä filtteri
            dv.RowFilter = string.Format("City LIKE '{0}'", cbKaupungit.SelectedValue);
        }
    }
}
