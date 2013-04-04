using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RotorsWorkFlow.Helpers
{
    public static class DataInputHelper
    {
        public static string[] BuildServiceNames()
        {
            List<string> services = new List<string>();
            using (FileStream fs = File.Open(@".\Data\services.txt", FileMode.Open, FileAccess.Read))
            {
                StreamReader sr = new StreamReader(fs);
                string service=string.Empty;
                while ((service = sr.ReadLine()) != null)
                {
                    services.Add(service);
                }
            }
            return services.ToArray();
        }

        public static FileItem[] BuildFileItems()
        {
            return new FileItem[] { new FileItem("a", @"c:\windows\system32\Essentials\ConfigTasks.dll") };
        }
    }
}
