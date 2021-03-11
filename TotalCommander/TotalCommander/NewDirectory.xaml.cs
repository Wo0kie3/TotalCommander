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

namespace TotalCommander
{
    /// <summary>
    /// Logika interakcji dla klasy NewDirectory.xaml
    /// </summary>
    public partial class NewDirectory : Window
    {
        string path;
        public NewDirectory(string path)
        {
            this.path = path;
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string root = path + "\\" + textBlock.Text;
            MessageBox.Show(root);
            if (!Directory.Exists(root))
            {
                Directory.CreateDirectory(root);
            }
            else
            {
                MessageBox.Show("Podany folder istnieje!");
            }
        }
    }
}
