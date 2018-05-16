using NumericalIntegral.ViewModel;
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

namespace NumericalIntegral
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public SimpsonIntegralGraph ViewModel { get; private set; } = new SimpsonIntegralGraph();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void plotButton_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.Update(plotCanvas.ActualWidth);
        }

        private void alphaRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            ViewModel.SelectedParameter = SelectedParameter.Alpha;
        }

        private void bettaRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            ViewModel.SelectedParameter = SelectedParameter.Betta;
        }

        private void gammaRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            ViewModel.SelectedParameter = SelectedParameter.Gamma;
        }

        private void deltaRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            ViewModel.SelectedParameter = SelectedParameter.Delta;
        }

        private void epsilonRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            ViewModel.SelectedParameter = SelectedParameter.Epsilon;
        }
    }
}
