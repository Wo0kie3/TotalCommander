using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace TotalCommander
{
    public class MyDirectory:DiscElement
    {
        private string Name; 
        private string Path;

        public string GetPath()
        {
            return Path;
        }
        public MyDirectory(string path)
        {
            this.Path = path;
            Name = path.Substring(path.LastIndexOf(@"\")+1);
        }

        public DateTime GetCreationTime()
        {
            return Directory.GetCreationTime(Path);
        }
        public string GetName()
        {
            return Name;
        }   
        public List<DiscElement> GetSubElements()
        {
            string[] dPaths = Directory.EnumerateDirectories(Path).ToArray();
            string[] fPaths = Directory.EnumerateFiles(Path).ToArray();
            List<DiscElement> myElements = new List<DiscElement>();

            foreach (string dPath in dPaths)
            {
                myElements.Add(new MyDirectory(dPath));
            }

            foreach (string fPath in fPaths)
            {
                myElements.Add(new MyFile(fPath));
            }

            return myElements;
        }
    }
}
