using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;

namespace TotalCommander
{
    public class MyFile:DiscElement
    {
        private string Name;
        private string Path; 

        public MyFile(string path)
        {
            this.Path = path;
            Name = path.Substring(path.LastIndexOf(@"\")+1);
        }
        
        public string GetPath()
        {
            return Path;
        }
        public string GetName()
        {
            return Name;
        }

        public DateTime GetCreationTime()
        {
            return File.GetCreationTime(GetPath());
        }
    }
}
