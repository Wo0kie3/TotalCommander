using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
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
using TotalCommander.Tools;
namespace TotalCommander
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string pathRight;
        string pathLeft;
        FileManager fileManager;
        public MainWindow()
        {
            this.fileManager = new FileManager(); 
            InitializeComponent();
            this.pathRight = rightPanel.PathBox.Text;
            this.pathLeft = leftPanel.PathBox.Text;
        }

        private void CopyRightSide_Click(object sender, RoutedEventArgs e)
        {
            CopyProcedure(rightPanel.GetSelectedDirs(), rightPanel.GetSelectedFiles(), leftPanel.PathBox.Text);
            leftPanel.LoadFiles(leftPanel.PathBox.Text, false);
        }

        private void CopyLeftSide_Click(object sender, RoutedEventArgs e)
        {
            CopyProcedure(leftPanel.GetSelectedDirs(),leftPanel.GetSelectedFiles(),rightPanel.PathBox.Text);
            rightPanel.LoadFiles(rightPanel.PathBox.Text, false);
        }

        public void CopyProcedure(List<string> dirs,List<string> files, string target)
        {
            foreach(string dir in dirs)
            {
                CopyDir(dir, target);
            }
            foreach(string file in files)
            {
                CopyFile(file, target);
            }
        }

        public void CopyFile(string source, string target)
        {
            string tgr = target +"\\"+ source.Substring(source.LastIndexOf(@"\") + 1);
            if(!File.Exists(tgr))
            {
                File.Copy(source, tgr);
            }
            else
            {
                MessageBox.Show(source.Substring(source.LastIndexOf(@"\") + 1)+" istnieje w podanym katalogu!");
            }  
         

        }
        public void CopyDir(string source, string target)
        {
            string newDir = target + "\\" + source.Substring(source.LastIndexOf(@"\") + 1);
            if (!Directory.Exists(newDir))
            {
                Directory.CreateDirectory(newDir);
                foreach (string subDir in Directory.GetDirectories(source))
                {
                    CopyDir(subDir, newDir);
                }
                foreach (string subFiles in Directory.GetFiles(source))
                {
                    CopyFile(subFiles, newDir);
                }
            }
        }
    }
}
