using GUI_20212202_DCQEB4;
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
using System.Windows.Shapes;

namespace UI
{
    /// <summary>
    /// Interaction logic for MenuWindow.xaml
    /// </summary>
    public partial class MenuWindow : Window
    {
        public MenuWindow()
        {
            InitializeComponent();
            myMediaElement.Play();
        }

        private void OnClickPlay(object sender, RoutedEventArgs e)
        {
            myMediaElement.Stop();
            new MainWindow().ShowDialog();
            Close();
        }
        private void OnClickHelp(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Arrows->Up & Down\nEsc->Exit\nP->Pause\nF5->Qucik Save\nF6->Quick Load","Controls",MessageBoxButton.OK,MessageBoxImage.Information);
        }
        private void OnClickCredits(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Papp Márk\nMajor Sándor\nKiss Levente", "Creators", MessageBoxButton.OK, MessageBoxImage.Information);
        }
        private void OnClickExit(object sender, RoutedEventArgs e)
        {

            Close();
        }

    }
}
