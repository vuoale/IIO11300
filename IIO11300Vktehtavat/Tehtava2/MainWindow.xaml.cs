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
    }
}
