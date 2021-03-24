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

namespace TotalCommander.Windows
{
    /// <summary>
    /// Logika interakcji dla klasy MyTextWindow.xaml
    /// </summary>
    public partial class MyTextWindow : Window
    {
        public MyTextWindow(string text)
        {
            InitializeComponent();
            textBox.Text = text;
        }
    }
}
