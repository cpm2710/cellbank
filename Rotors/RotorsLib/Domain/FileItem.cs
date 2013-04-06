using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RotorsLib.Domain
{
    public class FileItem
    {
        public FileItem(string sourceFullName, string destinationFullName)
        {
            this.SourceFullName = sourceFullName; 
            this.DestinationFullName = destinationFullName;
        }
        public string SourceFullName { get; set; }
        /// <summary>
        /// Should in format \\devicename\c$\system32\sss.dll
        /// </summary>
        public string DestinationFullName { get; set; }
        public override string ToString()
        {
            return SourceFullName + DestinationFullName;
        }
    }
}
