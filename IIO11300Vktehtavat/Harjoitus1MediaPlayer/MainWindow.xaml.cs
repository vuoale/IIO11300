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

namespace Harjoitus1MediaPlayer
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

        private void btnPlay_Click(object sender, RoutedEventArgs e)
        {
            //soitetaan käyttäjän valitsemaa mediatiedostoa
            string filu = @"D:\H8346\video.mp4";
            //tutkitaan onko tiedosto olemassa
            if (System.IO.File.Exists(filu))
                {
                    //MessageBox.Show("Soitetaan tiedosto: " + filu);
                    mediaElement.Source = new Uri(filu);
                    mediaElement.Play();
                } 
        }
    }
}
