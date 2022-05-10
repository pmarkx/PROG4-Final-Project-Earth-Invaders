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
using System.IO;

namespace UI
{
    /// <summary>
    /// Interaction logic for Score.xaml
    /// </summary>
    public partial class WindowScore : Window
    {
        public WindowScore()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            StreamWriter streamWriter=new StreamWriter("scores.txt",true);
            string asd = name.Text + " " + score.Content.ToString();
            streamWriter.WriteLine(name.Text+" "+score.Content.ToString());
            streamWriter.Close();
            Close();
        }
    }
}
