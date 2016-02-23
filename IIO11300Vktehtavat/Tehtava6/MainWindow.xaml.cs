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
using System.Xml.Linq;

namespace Tehtava6
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        XElement xe;

        public MainWindow()
        {
            InitializeComponent();

            try
            {
                //ladataan xml-tiedosto
                xe = XElement.Load(GetFileName());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {   
            try
            {
                // tyhjennetään vanhat pois datagridistä
                dgData.Items.Clear();

                if (comboBox.SelectedItem == null)
                {
                    //mitään maata ei valittu -> näytetään kaikki viinit
                    foreach (XElement viini in xe.Elements("wine"))
                    {
                            dgData.Items.Add(viini);
                    }
                }
                else
                {
                    //näytetään vain valitun maan viinit
                    string valittuMaa = comboBox.SelectedItem.ToString();
                    foreach (XElement viini in xe.Elements("wine"))
                    {
                        if (viini.Element("maa").Value == valittuMaa)
                        {
                            dgData.Items.Add(viini);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private string GetFileName()
        {
            //tiedostonimi App.Config-konfigurointitiedostoon
            return Tehtava6.Properties.Settings.Default.XMLTiedosto;
        }

        private void ComboBox_Loaded(object sender, RoutedEventArgs e)
        {
            List<string> maat = new List<string>();

            try
            {
                //maat comboboxiin, ei lisätä duplikaatteja      
                foreach (XElement maa in xe.Descendants("maa"))
                {
                    if (!maat.Contains(maa.Value))
                    {
                        maat.Add(maa.Value);
                    }
                }

                // ... Get the ComboBox reference.
                var comboBox = sender as ComboBox;

                // ... Assign the ItemsSource to the List.
                comboBox.ItemsSource = maat;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

    }
}
