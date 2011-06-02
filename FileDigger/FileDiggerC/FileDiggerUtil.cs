using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.NetworkInformation;
using System.ServiceModel;

namespace FileDiggerC
{
    class FileDiggerUtil
    {
        private String ip;

        public String Ip
        {
            get { return ip; }
            set { ip = value; }
        }
        private String fileName;

        public String FileName
        {
            get { return fileName; }
            set { fileName = value; }
        }
        private List<String> files;

        public List<String> Files
        {
            get { return files; }
            set { files = value; }
        }
        public void digFiles()
        {
            Ping p = new Ping();
            files = new List<string>();
            IPAddress addr = IPAddress.Parse(ip);
            PingReply reply = p.Send(addr);
            FileDiggerService.FileDiggerClient c = new FileDiggerService.FileDiggerClient();

            if (reply.Status != IPStatus.TimedOut)
            {
                c.Endpoint.Address = new EndpointAddress("http://" + ip + ":8000/ServiceModelSamples/service");
                try
                {
                    string[] tempFiles = c.findFile(fileName);
                    if (tempFiles != null)
                    {
                        files.AddRange(tempFiles);
                    }
                    c.Close();
                }
                catch (Exception exception)
                {
                    
                }
            }
        }
    }
}
