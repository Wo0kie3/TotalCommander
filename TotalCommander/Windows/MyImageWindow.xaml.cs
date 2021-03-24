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
    /// Logika interakcji dla klasy MyImageWindow.xaml
    /// </summary>
    public partial class MyImageWindow : Window
    {
        public MyImageWindow(string path)
        {
            InitializeComponent();
            SetImage(path);
            this.Title= path.Substring(path.LastIndexOf(@"\") + 1); 
        }

        private void SetImage(string path)
        {
            MyImage.Source = new BitmapImage(new Uri(path,UriKind.RelativeOrAbsolute));
            this.SizeToContent = SizeToContent.WidthAndHeight;
        }
    }
}
