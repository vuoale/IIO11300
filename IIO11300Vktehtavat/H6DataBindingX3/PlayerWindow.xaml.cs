using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Shapes;

namespace H6DataBindingX3
{
    /// <summary>
    /// Interaction logic for PlayerWindow.xaml
    /// </summary>
    public partial class PlayerWindow : Window
    {
        ObservableCollection<HockeyPlayer> pelaajat;
        int laskuri;
        public PlayerWindow()
        {
            InitializeComponent();
            IniMyStuff();
        }
        private void IniMyStuff()
        {
            pelaajat = Get3TestPlayers();
            dgPlayers.ItemsSource = pelaajat;
            laskuri = 0;
            SetData();
        }
        private void SetData()
        {
            myGrid.DataContext = pelaajat[laskuri];
        }
        private ObservableCollection<HockeyPlayer> Get3TestPlayers()
        {
            ObservableCollection<HockeyPlayer> temp = new ObservableCollection<HockeyPlayer>();
            temp.Add(new HockeyPlayer("Teemu Selänne", "8"));
            temp.Add(new HockeyPlayer("Jarkko Immonen", "28"));
            temp.Add(new HockeyPlayer("Ville Peltonen", "16"));
            return temp;
        }

        private void dgPlayers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if ((dgPlayers.SelectedIndex >= 0) & (dgPlayers.SelectedIndex <= pelaajat.Count -1))
            {
                laskuri = dgPlayers.SelectedIndex;
                SetData();
            }
        }
    }
}
