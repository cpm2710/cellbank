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
            foreach (String folder in FileDiggerModel.getInstance().OwnFolders)
            {
                try
                {
                    DirectoryInfo di = new DirectoryInfo(folder);
                    result.AddRange(this.findFile(name, di));
                }
                catch (Exception openException)
                {

                }
                
            }
            return result;
        }
        public void deleteSharedFolder(String folder)
        {
            string folderInRegular = folder.Replace("\\\\", "\\");
            folderInRegular = folderInRegular.Replace("\\", "\\\\");
            this.ownFolders.Remove(folderInRegular);
            
            StreamWriter sw = new StreamWriter(sharedFolderConfig,false);
            foreach (string flder in this.ownFolders)
            {
                sw.WriteLine(flder);
            }
            sw.Close();
            //foreach (string flder in this.ownFolders)
            //{
            //    if (flder.Equals(folderInRegular, StringComparison.InvariantCultureIgnoreCase))
            //    {
            //        this.ownFolders.Remove(flder);
            //    }
            //}
        }
        public void addFolder(String folder)
        {
            string folderInRegular = folder.Replace("\\\\", "\\");
            folderInRegular = folderInRegular.Replace("\\", "\\\\");
            foreach (string flder in this.ownFolders)
            {
                if (flder.Equals(folderInRegular, StringComparison.InvariantCultureIgnoreCase))
                {
                    return;
                }
            }
            this.ownFolders.Add(folderInRegular);
            StreamWriter sw = new StreamWriter(sharedFolderConfig,true);
            sw.WriteLine(folderInRegular);
            sw.Close();
        }
        private List<String> findFile(String name, DirectoryInfo d)
        {
            List<String> result = new List<string>();
            FileInfo[] fis = d.GetFiles();
            foreach (FileInfo f in fis)
            {
                if (f.FullName.Contains(name))
                {
                    result.Add(f.FullName);
                }
            }
            DirectoryInfo[] dis = d.GetDirectories();
            foreach (DirectoryInfo di in dis)
            {
                result.AddRange(findFile(name, di));
            }
            return result;
        }
        private String sharedFolderConfig="sharedfolders.txt";
        private FileDiggerModel()
        {
            peers = new List<int>();
            ownFolders = new List<string>();

            if (!File.Exists(sharedFolderConfig))
            {
                File.Create(sharedFolderConfig);
            }
            StreamReader sr = new StreamReader(sharedFolderConfig);
            string line = null;
            while ((line = sr.ReadLine()) != null)
            {
                if (!line.Trim().Equals(""))
                {
                    line = line.Replace("\\\\", "\\");
                    line = line.Replace("\\", "\\\\");
                    ownFolders.Add(line);
                }
            }
            sr.Close();
        }
    }
    
    [ServiceContract(Namespace = "http://Microsoft.ServiceModel.Samples")]
    interface IFileDigger
    {
        [OperationContract]
        void ReportLive(int ip);
        [OperationContract]
        void addFolder(String folder);
        [OperationContract]
        void deleteSharedFolder(String folder);
        [OperationContract]
        List<String> findFile(String name);
        [OperationContract]
        List<String> findSharedFolders();
        [OperationContract]
        byte[] fetchFile(String fullName,int i);
    }
   
    public class FileDigger : IFileDigger
    {
        public List<String> findSharedFolders()
        {           
            return FileDiggerModel.getInstance().OwnFolders;
        }
        public void ReportLive(int ip)
        {
            FileDiggerModel.getInstance().Peers.Add(ip);
        }
        public void addFolder(String folder){
            if (!FileDiggerModel.getInstance().OwnFolders.Contains(folder))
            {
                FileDiggerModel.getInstance().addFolder(folder);
            }
        }
        public List<String> findFile(String name)
        {
            List<String> rslt = FileDiggerModel.getInstance().findFile(name);
            return rslt;
        }
        public void deleteSharedFolder(String folder)
        {
            FileDiggerModel.getInstance().deleteSharedFolder(folder);
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
