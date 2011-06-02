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
        private List<String> ownFolders;
        private List<String> peers;

        public List<String> OwnFolders
        {
            get { return ownFolders; }
            set { ownFolders = value; }
        }
        public List<String>Peers
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
        }
        public void deletePeer(String peer)
        {
            this.peers.Remove(peer);

            StreamWriter sw = new StreamWriter(peerConfig, false);
            foreach (string p in FileDiggerModel.getInstance().Peers)
            {
                sw.WriteLine(p);
            }
            sw.Close();
        }
        public void addPeer(String peer)
        {
            if (peer == null||peer.Trim().Equals(""))
            {
                return;
            }
            foreach (string p in FileDiggerModel.getInstance().Peers)
            {
                if (p.Equals(peer, StringComparison.InvariantCultureIgnoreCase))
                {
                    return;
                }
            }
            FileDiggerModel.getInstance().Peers.Add(peer);
            StreamWriter sw = new StreamWriter(this.peerConfig, true);
            sw.WriteLine(peer);
            sw.Close();
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
        private String peerConfig = "peerConfig.txt";
        private String sharedFolderConfig="sharedfolders.txt";
        private FileDiggerModel()
        {
            peers = new List<string>();
            ownFolders = new List<string>();

            if (!File.Exists(sharedFolderConfig))
            {
               FileStream fs= File.Create(sharedFolderConfig);
               fs.Close();
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
            if (!File.Exists(peerConfig))
            {
                FileStream fs=File.Create(peerConfig);
                fs.Close();
            }
            StreamReader peerSr = new StreamReader(peerConfig);
            string peerLine = null;
            while ((peerLine = peerSr.ReadLine()) != null)
            {
                if (!peerLine.Trim().Equals(""))
                {
                    peers.Add(peerLine);
                }
            }
            peerSr.Close();
        }
    }
    
    [ServiceContract(Namespace = "http://Microsoft.ServiceModel.Samples")]
    interface IFileDigger
    {
        [OperationContract]
        void addPeer(String peer);
        [OperationContract]
        void deletePeer(String peer);
        [OperationContract]
        List<String> findPeers();
        [OperationContract]
        long fetchFileSize(String fullName);
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
        public void addPeer(String peer)
        {
            FileDiggerModel.getInstance().addPeer(peer);
        }
        public void deletePeer(String peer)
        {
            FileDiggerModel.getInstance().deletePeer(peer);
        }
        public List<String> findPeers()
        {
            return FileDiggerModel.getInstance().Peers;
        }
        public List<String> findSharedFolders()
        {           
            return FileDiggerModel.getInstance().OwnFolders;
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
            fs.Seek(i * 1024*1024, SeekOrigin.Begin);
            byte []content=new byte[1024*1024];
            fs.Read(content, 0,1024*1024);
            fs.Close();
            return content;
        }
        public long fetchFileSize(String fullName)
        {
            FileInfo f = new FileInfo(fullName);
            return f.Length;
        }
    }

}
