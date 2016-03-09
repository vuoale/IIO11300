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

namespace H6DataBindingX3
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //modulitason muuttujat
        HockeyLeague smliiga;
        List<HockeyTeam> joukkueet;
        int clicked = 0;
        public MainWindow()
        {
            InitializeComponent();
            FillMyCombobox();
            smliiga = new HockeyLeague();
            joukkueet = smliiga.GetTeams();
        }

        private void FillMyCombobox()
        {
            cbCourses2.Items.Add("IIO12110 Ohjelmistotuotanto");
            cbCourses2.Items.Add("ZZ1234 Helppoa ruåtsia");
            cbCourses2.Items.Add("J2EE ");
        }
        private void btnSetUser_Click(object sender, RoutedEventArgs e)
        {
            //luetaan App.Configista UserName-asetus
            txtUsername.Text = "Hello: " + H6DataBindingX3.Properties.Settings.Default.UserName;
        }

        private void btnBind_Click(object sender, RoutedEventArgs e)
        {
            myGrid.DataContext = joukkueet;
        }

        private void btnForward_Click(object sender, RoutedEventArgs e)
        {
            clicked++;
            myGrid.DataContext = joukkueet[clicked];
        }

        private void btnBackward_Click(object sender, RoutedEventArgs e)
        {
            clicked--;
            myGrid.DataContext = joukkueet[clicked];
        }

        private void btnShow_Click(object sender, RoutedEventArgs e)
        {
            object item = cbCourses.SelectedValue;
            MessageBox.Show(item.ToString());
        }

        private void btnShowPlayers_Click(object sender, RoutedEventArgs e)
        {
            PlayerWindow win = new PlayerWindow();
            win.ShowDialog();
        }
    }
}
