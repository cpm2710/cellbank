using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.ServiceModel;
using System.Configuration;
using System.Configuration.Install;
using System.Net;

namespace FileDigger
{
    

    public partial class FileDiggerAgentService : ServiceBase
    {
        public ServiceHost serviceHost = null;
        public FileDiggerAgentService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            if (serviceHost != null)
            {
                serviceHost.Close();
            }
            String hostString = Dns.GetHostName();
            IPHostEntry hostinfo = Dns.GetHostEntry(hostString);
            System.Net.IPAddress[] addresses = hostinfo.AddressList;
            String localIp=null;
            foreach (IPAddress address in addresses)
            {
                if (!address.IsIPv6LinkLocal)
                {
                    localIp = address.ToString();
                    break;
                }
            }
            WSHttpBinding ws = new WSHttpBinding();
            Uri baseAddress = new Uri("http://" + localIp + ":8000/ServiceModelSamples/service");
            
            serviceHost = new ServiceHost(typeof(FileDigger), baseAddress);
            
            serviceHost.Open();            
        }

        protected override void OnStop()
        {
            if (serviceHost != null)
            {
                serviceHost.Close();
                serviceHost = null;
            }
        }
    }
}
