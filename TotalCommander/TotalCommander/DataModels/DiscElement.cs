using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TotalCommander
{
    public interface DiscElement
    {
        string GetName();

        string GetPath();

        DateTime GetCreationTime();
    }
}
