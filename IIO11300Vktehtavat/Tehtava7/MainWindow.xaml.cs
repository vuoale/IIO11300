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

namespace Tehtava7
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
        private void txtPassword_TextChanged(object sender, TextChangedEventArgs e)
        {
            string password = txtPassword.Text;
            int total = password.Length;
            int upperTotal = password.Count(c => char.IsUpper(c)); //LINQ
            int lowerTotal = password.Count(c => char.IsLower(c)); //LINQ
            int numberTotal = password.Count(c => char.IsNumber(c)); //LINQ
            int specialTotal = total - upperTotal - lowerTotal - numberTotal;

            tbLength.Text = "Merkkejä: " + total;
            tbUppercase.Text = "Isoja kirjaimia: " + upperTotal;
            tbLowercase.Text = "Pieniä kirjaimia: " + lowerTotal;
            tbNumbers.Text = "Numeroita: " + numberTotal;
            tbSpecial.Text = "Erikoismerkkejä: " + specialTotal;

            int types = 0;
            if (upperTotal != 0) types++;
            if (lowerTotal != 0) types++;
            if (numberTotal != 0) types++;
            if (specialTotal != 0) types++;

            if (total == 0)
            {
                tbPassword_Strength.Background = Brushes.LightGray;
                tbPassword_Strength.Text = "Anna salasana";
            }
            else if (total < 8 || types == 1)
            {
                // BAD
                tbPassword_Strength.Background = Brushes.Orange;
                tbPassword_Strength.Text = "Bad";
            }
            else if (total < 12 && types >= 2)
            {
                // FAIR
                tbPassword_Strength.Background = Brushes.Yellow;
                tbPassword_Strength.Text = "Fair";
            }
            else if (total < 16 && types >= 3)
            {
                // MODERATE
                tbPassword_Strength.Background = Brushes.LightGreen;
                tbPassword_Strength.Text = "Moderate";
            }
            else if (total >= 16 && types == 4)
            {
                // GOOD
                tbPassword_Strength.Background = Brushes.Green;
                tbPassword_Strength.Text = "Good";
            }
        }
    }
}
