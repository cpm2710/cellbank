using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;

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
        private FileDiggerModel()
        {
            peers = new List<int>();
            ownFolders = new List<string>();
        }
    }
    public class FileDiggerService : IFileDigger
    {
       
        // Implement the ICalculator methods.
        public void ReportLive(int ip)
        {
            FileDiggerModel.getInstance().Peers.Add(ip);
        }
        public void addFolder(String folder){

        }
        public List<String> findFile(String name)
        {

            List<String> rslt = new List<string>();
            return rslt;
        }
    }

}
