using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiskLib
{
    public class DiskHelper
    {
        public static void ConvertDiskToVhd(string vhdPath)
        {
            string cwd = System.IO.Directory.GetCurrentDirectory();

            ProcessStartInfo psi=new ProcessStartInfo("disk2vhd.exe", " * "+vhdPath);
            Process p=Process.Start(psi);
            //p.Start();
            p.WaitForExit();

        }
    }
}
