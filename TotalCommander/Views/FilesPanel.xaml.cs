using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
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
using TotalCommander.Windows;

namespace TotalCommander.Views
{
    /// <summary>
    /// Logika interakcji dla klasy Panel.xaml
    /// </summary>
    public partial class FilesPanel : UserControl
    {
        bool sortFlag = false;
        public FilesPanel()
        {
            InitializeComponent();
            ComboBox_Ini();
            LoadFiles(PathComboBox.Text,false);
            PathBox.Text = PathComboBox.Text;
        }

        internal List<string> GetSelectedDirs()
        {
            List<string> selectedDirs = new List<string>();
            
            foreach(var dir in listBox.Items)
            {
                if (dir is DirectoryView)
                {
                    DirectoryView directoryView = (DirectoryView)dir;
                    if (directoryView.checkBox.IsChecked == true)
                    {
                        directoryView.checkBox.IsChecked = false;
                        selectedDirs.Add(directoryView.GetPath());
                    }
                   
                }
            }
            
            return selectedDirs;
        }

        internal List<string> GetSelectedFiles()
        {
            List<string> selectedFiles = new List<string>();

            foreach (var file in listBox.Items)
            {
                if(file is FileView)
                {
                    FileView fileView = (FileView)file;
                    if (fileView.checkBox.IsChecked == true)
                    {
                        fileView.checkBox.IsChecked = false;
                        selectedFiles.Add(fileView.GetPath());
                    }
                        
                }
            }
            return selectedFiles;
        }

        private void ComboBox_Ini()
        {
            DriveInfo[] theDrivers = DriveInfo.GetDrives();
                foreach (DriveInfo currentDrive in theDrivers)
                {
                        PathComboBox.Items.Add(currentDrive.Name);
                }
            PathComboBox.SelectedIndex = 0;
        }

        private void CopyButton_Click(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void NewDirButton_Click(object sender, RoutedEventArgs e)
        {
            NewDirectory newDirectory = new NewDirectory(PathBox.Text);
            newDirectory.ShowDialog();
            LoadFiles(PathBox.Text,false);
        }

        private void SortButton_Click(object sender, RoutedEventArgs e)
        {
            if (sortFlag == false)
            {
                LoadFiles(PathBox.Text, true);
                sortFlag = true;
            }
            else
            {
                LoadFiles(PathBox.Text, false);
                sortFlag = false;
            }
                       
        }
        public void LoadFiles(string path, bool sort)
        {
            
            if (!Directory.Exists(path))
            {
                MessageBox.Show("Błąd Ścieżki!");
                listBox.Items.Clear();
                return;
            }
            MyDirectory myDir = new MyDirectory(path);
            List<DiscElement> myElements = myDir.GetSubElements();
            listBox.Items.Clear();
            if (sort == true)
            {
                myElements = myElements.OrderBy(o => o.GetCreationTime()).ToList();
                List<DiscElement> myDirSorted = new List<DiscElement>();
                List<DiscElement> myFileSorted = new List<DiscElement>();
                foreach(DiscElement orderingElement in myElements)
                {
                    if(orderingElement is MyDirectory)
                    {
                        myDirSorted.Add(orderingElement);
                    }
                    else
                    {
                        myFileSorted.Add(orderingElement);
                    }
                }
                myElements.Clear();
                foreach(DiscElement myDirectory in myDirSorted)
                {
                    myElements.Add(myDirectory);
                }
                foreach(DiscElement myFile in myFileSorted)
                {
                    myElements.Add(myFile);
                }
            }
            foreach (DiscElement myElement in myElements)
            {
                if (myElement is MyDirectory)
                {
                    DirectoryView newFolderView = new DirectoryView((MyDirectory)myElement);
                    newFolderView.DirectoryChanged += OnDirectoryChanged;
                    newFolderView.DirectoryDelete += OnDeleteDirectory;
                    listBox.Items.Add(newFolderView);
                }
                else
                {
                    FileView newElementView = new FileView((MyFile)myElement);
                    newElementView.filedelete += OnDeleteClick;          
                    listBox.Items.Add(newElementView);
                }

            }

        }
        public void OnDirectoryChanged(object source, EventArgs e)
        {
            if (!(source is MyDirectory))
                return;
            MyDirectory directory = (MyDirectory)source;
            LoadFiles(directory.GetPath(),false);
            PathBox.Text = directory.GetPath();
        }

        private void OnDeleteClick(object source, EventArgs e)
        {
            MyFile myFile = (MyFile)source;
            DeleteAsk deleteAsk = new DeleteAsk(myFile.GetName());
            deleteAsk.ShowDialog();
            if (deleteAsk.DialogResult == true)
            {
                //File.Delete(myFile.GetPath());
            }
            //MessageBox.Show("Usunięto: " + myFile.GetPath());
            LoadFiles(PathBox.Text,false);
        }

        private void OnDeleteDirectory(object source, EventArgs e)
        {
            MyDirectory directory = (MyDirectory)source;
            DeleteAsk deleteAsk = new DeleteAsk(directory.GetName());
            deleteAsk.ShowDialog();
            if(deleteAsk.DialogResult == true)
            {
                if (Directory.Exists(directory.GetPath()))
                {
                    Directory.Delete(directory.GetPath(),true);
                }
            }
            //MessageBox.Show("Usunieto: " + directory.GetName());
            LoadFiles(PathBox.Text,false);
        }
        private void PathComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string text = (sender as ComboBox).SelectedItem as string;
            PathBox.Text = text;
            LoadFiles(text,false);
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            if (System.IO.Directory.GetParent(PathBox.Text) == null)
                return;
            string path = System.IO.Directory.GetParent(PathBox.Text).FullName;
            LoadFiles(path,false);
            PathBox.Text = path;
        }

        private void RefreshButton_Click(object sender, RoutedEventArgs e)
        {
            LoadFiles(PathBox.Text,false);
        }

        private void SzukajButton_Click(object sender, RoutedEventArgs e)
        {
            SearchFiles(PathBox.Text, SearchBox.Text);
        }

        private void SearchFiles(string path, string filename)
        {
            DirectoryInfo directoryInfo = new DirectoryInfo(path);
            FileInfo[] fileInfos = directoryInfo.GetFiles("*" + filename + "*");
            DirectoryInfo[] directoryInfos = directoryInfo.GetDirectories("*" + filename + "*");
            listBox.Items.Clear();
            MyDirectory myDir = new MyDirectory(path);
            List<DiscElement> myElements = myDir.GetSubElements();
            foreach (DirectoryInfo directory in directoryInfos)
            {
                foreach (DiscElement myElement in myElements)
                {
                    if (directory.Name == myElement.GetName())
                    {
                        DirectoryView newFolderView = new DirectoryView((MyDirectory)myElement);
                        newFolderView.DirectoryChanged += OnDirectoryChanged;
                        listBox.Items.Add(newFolderView);
                    }
                }

            }
            foreach (FileInfo file in fileInfos)
            { 
                foreach (DiscElement myElement in myElements)
                {
                    if(file.Name == myElement.GetName())
                    {
                        FileView newElementView = new FileView((MyFile)myElement);
                        listBox.Items.Add(newElementView);
                    }
                }
                    
            }
        }
    }
}
