using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RotorsWorkFlow
{
    
    public class FileItem
    {
        public string SourceFullName { get; set; }
        public string DestinationFullName { get; set; }
        public override string ToString()
        {
            return SourceFullName + DestinationFullName;
        }
    }
}
