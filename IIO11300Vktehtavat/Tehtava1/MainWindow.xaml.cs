/*
* Copyright (C) JAMK/IT/Esa Salmikangas
* This file is part of the IIO11300 course project.
* Created: 12.1.2016 Modified: 13.1.2016
* Authors: Aleksi Vuorela, Esa Salmikangas
*/
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

namespace Tehtava1
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

        private void btnCalculate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //lasketaan pinta-ala Ikkuna-olion avulla
                //luodaan luokasta olio
                JAMK.IT.IIO11300.Ikkuna ikk = new JAMK.IT.IIO11300.Ikkuna();
                ikk.Korkeus = double.Parse(txtIkkunaKorkeus.Text);
                ikk.Leveys = double.Parse(txtIkkunaLeveys.Text);
                //tulos käyttäjälle
                //VE metodilla
                //MessageBox.Show(ikk.LaskePintaAla().ToString());
                //VE property
                MessageBox.Show(ikk.PintaAla.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            /*
            double ikkunaLeveys = Convert.ToDouble(txtIkkunaLeveys.Text);
            double ikkunaKorkeus = Convert.ToDouble(txtIkkunaKorkeus.Text);
            double karmiLeveys = Convert.ToDouble(txtKarmiLeveys.Text);
            double result = 0;

            try
            {
                result = BusinessLogicWindow.CalculatePerimeter(ikkunaLeveys, ikkunaKorkeus);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                txtIkkunaPA.Text = Convert.ToString(result);
                txtKarmiPiiri.Text = Convert.ToString(ikkunaLeveys + ikkunaLeveys + ikkunaKorkeus + ikkunaKorkeus);
                txtKarmiPA.Text = Convert.ToString(ikkunaLeveys * karmiLeveys * 2 + (ikkunaKorkeus-karmiLeveys*2) * karmiLeveys * 2);
            }
            */
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

  }

  public class BusinessLogicWindow
    {
    /// <summary>
    /// CalculatePerimeter calculates the perimeter of a window
    /// </summary>
    public static double CalculatePerimeter(double ikkunaLeveys, double ikkunaKorkeus)
        {
            return ikkunaLeveys * ikkunaKorkeus;
        }
    }
}
