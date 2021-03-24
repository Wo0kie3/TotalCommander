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
using System.IO;
using TotalCommander.Windows;

namespace TotalCommander.Views
{
    /// <summary>
    /// Logika interakcji dla klasy FileView.xaml
    /// </summary>
    public partial class FileView : UserControl
    {
        public delegate void FiledeleteEventHandler(object sender, EventArgs e);

        public event FiledeleteEventHandler filedelete;

        private MyFile discElement { get; }
        public FileView(MyFile discElement)
        {
            InitializeComponent();
            this.discElement = discElement;
            NameBox.Text = discElement.GetName();
            DateBox.Text = discElement.GetCreationTime().ToShortDateString();
        }

        protected virtual void DeleteClick()
        {
            if (filedelete != null)
                filedelete(discElement, EventArgs.Empty);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DeleteClick();
        }

        public string GetPath()
        {
            return discElement.GetPath();
        }
        private void OpenButtun_Click(object sender, RoutedEventArgs e)
        {
            if(System.IO.Path.GetExtension(discElement.GetPath()) == ".txt")
            {
                string fileText = File.ReadAllText(discElement.GetPath(),Encoding.Default);
                MyTextWindow myTextWindow = new MyTextWindow(fileText);
                myTextWindow.ShowDialog();
            }
            else if(System.IO.Path.GetExtension(discElement.GetPath()) == ".png" ^ System.IO.Path.GetExtension(discElement.GetPath()) == ".jpg")
            {
                MyImageWindow myImageWindow = new MyImageWindow(discElement.GetPath());
                myImageWindow.ShowDialog();
            }
            else
            {
                System.Diagnostics.Process.Start(discElement.GetPath());
            }
               
        }
    }
}
