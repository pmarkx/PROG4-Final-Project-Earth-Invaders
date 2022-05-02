using Logic;
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
using UI.Controller;
using UI.VM;

namespace GUI_20212202_DCQEB4
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        GameController controller;
        public MainWindow()
        {
            InitializeComponent();
            myMediaElement.Play();
            GameLogic logic = new GameLogic();
            display.SetupModel(logic);
            controller = new GameController(logic);
            controller.UITimer.Tick += UITimer_Tick;
            //controller.GameTickHappened += Controller_GameTickHappened;
            controller.UITimer.Start();
        }

        private void UITimer_Tick(object? sender, EventArgs e)
        {
            controller.RefreshScoreTable(this.DataContext as MainWindowViewModel);
            display.InvalidateVisual();

        }
        private void Controller_GameTickHappened()
        {
            display.InvalidateVisual();

        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            display.Resize(new Size(grid.ActualWidth, grid.ActualHeight));
            //display.InvalidateVisual();

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            display.Resize(new Size(grid.ActualWidth, grid.ActualHeight));
            e.Handled = true;
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            controller.KeyPressed(e.Key);
            e.Handled = true;
            // display.InvalidateVisual();
        }
    }
}
