using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
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
                string service = string.Empty;
                while ((service = sr.ReadLine()) != null)
                {
                    services.Add(service);
                }
            }
            return services.ToArray();
        }
        //\\andliudevs\d$\winmain\fbl_srv4_sh_dev.binaries.amd64fre\Admin\ServerEssentials


        public static FileItem[] BuildFileItems()
        {
            // get source file list.
            List<string> sourceFiles = new List<string>();
            DirectoryInfo di = new DirectoryInfo(Constants.SourceRootPath);

            // get destination file list.


            //public const string System32EssentialsPath = @"\\andess1server\c$\windows\system32\Essentials";

            //public const string GacEssentialsPath = @"\\andess1server\c$\Windows\Microsoft.NET\assembly\GAC_MSIL";

            List<string> targetFiles = new List<string>();
            try
            {
                NetworkCredential networkCredential = new NetworkCredential(Constants.UserName, Constants.PassWord, Constants.Domain);
                string sharePath = @"\\andess1server\c$\windows";
                using (NetworkConnection nc = new NetworkConnection(sharePath, networkCredential))
                {
                    DirectoryInfo system32Dest = new DirectoryInfo(Constants.System32EssentialsPath);


                    DirectoryInfo gacDest = new DirectoryInfo(Constants.GacEssentialsPath);

                    //File.Copy(fileItem.SourceFullName, administrativeFullName, true);
                }
            }
            catch (Exception e)
            {
                Logger.Error("exception encounterred: {0}", e);
            }


            // merge the two list into one array of FileItem


            return new FileItem[] { new FileItem("a.dll", @"c:\windows\system32\Essentials\a.dll") };
        }
    }
}
