// author: andyliuliming@outlook.com
using RotorsLib.Domain;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace RotorsLib.Helpers
{
    public static class DataInputHelper
    {
        public static string[] BuildServiceNames()
        {
            string msg = "building the service list...";
            Singleton<ReportMediator>.UniqueInstance.ReportStatus(msg);
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

        private static List<string> GetFileListRecursively(string directoryPath)
        {
            List<string> fileFullNames = new List<string>();
            DirectoryInfo di = new DirectoryInfo(directoryPath);

            FileInfo[] files = di.GetFiles();
            foreach (FileInfo file in files)
            {
                fileFullNames.Add(file.FullName);
            }
            DirectoryInfo[] subDirectories = di.GetDirectories();
            foreach (DirectoryInfo subDi in subDirectories)
            {
                fileFullNames.AddRange(GetFileListRecursively(subDi.FullName));
            }
            return fileFullNames;
        }


        public static FileItem[] BuildFileItems()
        {
            string msg = "building the file item list...";
            Singleton<ReportMediator>.UniqueInstance.ReportStatus(msg);
            List<FileItem> fileItemsList = new List<FileItem>();
            // get source file list.
            List<string> sourceFullNames = GetFileListRecursively(Constants.SourceRootPath);
            sourceFullNames.Sort();
            // get destination file list.
            List<string> targetFiles = new List<string>();

            try
            {
                NetworkCredential networkCredential = new NetworkCredential(Constants.UserName, Constants.PassWord, Constants.Domain);

                string sharePath = Constants.GacEssentialsPath.Substring(0, Constants.GacEssentialsPath.IndexOf(@"c$\Windows") + @"c$\Windows".Length);

                using (NetworkConnection nc = new NetworkConnection(sharePath, networkCredential))
                {
                    targetFiles.AddRange(GetFileListRecursively(Constants.System32EssentialsPath));
                    targetFiles.AddRange(GetFileListRecursively(Constants.GacEssentialsPath));
                }
            }
            catch (Exception e)
            {
                msg = string.Format("error building file items. {0}", e);
                Singleton<ReportMediator>.UniqueInstance.ReportStatus(msg, LogLevel.Warning);
            }

            targetFiles.Sort();


            // merge the two list into one array of FileItem
            foreach (string sourceFile in sourceFullNames)
            {
                if (sourceFile.EndsWith(".dll") || sourceFile.EndsWith(".exe"))
                {
                    string sourceFileName = ExtractFileName(sourceFile);

                    //Assembly sourceAssembly = Assembly.ReflectionOnlyLoadFrom(sourceFile);

                    ////Assembly.
                    foreach (string targetFile in targetFiles)
                    {
                        string destFileName = ExtractFileName(targetFile);
                        if (string.Equals(sourceFileName, destFileName, StringComparison.OrdinalIgnoreCase))
                        {
                            fileItemsList.Add(new FileItem(sourceFile, targetFile));
                        }
                        //Assembly targetAssembly = Assembly.ReflectionOnlyLoadFrom(sourceFile);
                        //if (string.Equals(sourceAssembly.FullName, targetAssembly.FullName))
                        //{
                        //    fileItemsList.Add(new FileItem(sourceFile, targetFile));
                        //}
                    }
                }                
            }

            return fileItemsList.ToArray();
        }

        private static string ExtractFileName(string fullName)
        {
            return fullName.Substring(fullName.LastIndexOf(@"\"));
        }
    }
}
