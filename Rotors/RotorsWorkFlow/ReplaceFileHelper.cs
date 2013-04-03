using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RotorsWorkFlow
{
    public static class ReplaceFileHelper
    {
        public static void ReplaceFile(FileItem fileItem)
        {
            Console.WriteLine("replacing file begins: source: {0} destination: {1}", fileItem.SourceFullName, fileItem.DestinationFullName);
            Console.WriteLine("replacing file ends: source: {0} destination: {1}", fileItem.SourceFullName, fileItem.DestinationFullName);
        }
    }
}
