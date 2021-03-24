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
    /// Logika interakcji dla klasy DeleteAsk.xaml
    /// </summary>
    public partial class DeleteAsk : Window
    {
        public DeleteAsk(string name)
        {
            InitializeComponent();
            NameBlock.Text = name;
        }

        private void YesButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        private void NoButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
