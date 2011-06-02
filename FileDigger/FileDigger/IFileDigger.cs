using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.IO;
using System.Configuration;
using System.Diagnostics;

namespace FileDigger
{
    [ServiceContract(Namespace = "http://Microsoft.ServiceModel.Samples")]
    interface IFileDigger
    {
        [OperationContract]
        void ReportLive(int ip);
        [OperationContract]
        void addFolder(String folder);
        [OperationContract]
        List<String> findFile(String name);
        [OperationContract]
        byte[] fetchFile(String fullName,int i);
    }
    public class FileDiggerModel
    {
        private static System.Object lockThis = new System.Object();
        private static FileDiggerModel m;
        private List<int> peers;
        private List<String> ownFolders;

        public List<String> OwnFolders
        {
            get { return ownFolders; }
            set { ownFolders = value; }
        }
        public List<int> Peers
        {
            get { return peers; }
            set { peers = value; }
        }
        private static void doSync()
        {
            lock (lockThis)
            {
                m = new FileDiggerModel();
            }
        }
        public static FileDiggerModel getInstance()
        {
            if (m == null)
            {
                doSync();
            }
            return m;
        }
        public List<String> findFile(String name)
        {
            List<String> result = new List<string>();
            foreach(String folder in FileDiggerModel.getInstance().OwnFolders)
            {
                DirectoryInfo di = new DirectoryInfo(folder);
               result.AddRange(this.findFile(name, di));
            }
            return result;
        }
        public void addFolder(String folder)
        {
            foreach (string flder in this.ownFolders)
            {
                if (flder.Equals(folder, StringComparison.InvariantCultureIgnoreCase))
                {
                    return;
                }
            }
            this.ownFolders.Add(folder);
            Configuration config =
            ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

            config.AppSettings.Settings.Add("folder"+(ownFolders.Count+1),folder);
            config.Save(ConfigurationSaveMode.Modified);
            //EventLog eLog=new EventLog();
            //eLog.
            // Force a reload of a changed section.
            ConfigurationManager.RefreshSection("appSettings");
        }
        private List<String> findFile(String name, DirectoryInfo d)
        {
            List<String> result = new List<string>();
            FileInfo [] fis=d.GetFiles();
            foreach (FileInfo f in fis)
            {
                if (f.FullName.Contains(name))
                {
                    result.Add(f.FullName);
                }
            }
            DirectoryInfo [] dis=d.GetDirectories();
            foreach(DirectoryInfo di in dis)
            {
                result.AddRange(findFile(name, di));
            }            
            return result;
        }
        private FileDiggerModel()
        {
            peers = new List<int>();
            ownFolders = new List<string>();
            foreach (string flder in ConfigurationManager.AppSettings)
            {
                if (flder.StartsWith("folder"))
                {
                    this.ownFolders.Add(ConfigurationManager.AppSettings[flder]);
                }
            }            
        }
    }
    public class FileDigger : IFileDigger
    {
        public void ReportLive(int ip)
        {
            FileDiggerModel.getInstance().Peers.Add(ip);
        }
        public void addFolder(String folder){
            if (!FileDiggerModel.getInstance().OwnFolders.Contains(folder))
            {
                FileDiggerModel.getInstance().OwnFolders.Add(folder);
            }
        }
        public List<String> findFile(String name)
        {
            List<String> rslt = FileDiggerModel.getInstance().findFile(name);
            return rslt;
        }
        public byte[] fetchFile(String fullName,int i)
        {
            FileStream fs=File.Open(fullName,FileMode.Open);
            byte []content=new byte[1024*1024];
            fs.Read(content, i*1024 * 1024,1024*1024);
            fs.Close();
            return content;
        }
    }

}
