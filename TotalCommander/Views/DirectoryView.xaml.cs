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

namespace TotalCommander.Views
{
    /// <summary>
    /// Logika interakcji dla klasy DirectoryView.xaml
    /// </summary>
    public partial class DirectoryView : UserControl
    {
        public delegate void DirectoryChangedEventHandler(object sender, EventArgs e);
        public delegate void DirectoryDeleteEventHandler(object sender, EventArgs e);
        public event DirectoryChangedEventHandler DirectoryChanged;
        public event DirectoryDeleteEventHandler DirectoryDelete;
        private MyDirectory discElement { get; }
        public DirectoryView(MyDirectory discElement)
        {
            InitializeComponent();
            this.discElement = discElement;
            NameBox.Text = discElement.GetName();
            DateBox.Text = discElement.GetCreationTime().ToShortDateString();
        }

        public string GetPath()
        {
            return discElement.GetPath();
        }
        protected virtual void OnDirectoryChanged()
        {
            if (DirectoryChanged != null)
                DirectoryChanged(discElement, EventArgs.Empty);
        }

        protected virtual void OnDirectoryDelete()
        {
            if(DirectoryDelete != null)
            {
                DirectoryDelete(discElement, EventArgs.Empty);
            }
        }

        private void OpenButton_Click(object sender, RoutedEventArgs e)
        {
            OnDirectoryChanged();
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            OnDirectoryDelete();
        }

    }
}
