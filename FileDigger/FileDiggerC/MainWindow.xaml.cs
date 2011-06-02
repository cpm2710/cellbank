﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Net;
using System.ServiceModel;
using System.Net.NetworkInformation;
using System.Windows.Forms;
using System.Threading;

namespace FileDiggerC
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        FileDiggerService.FileDiggerClient localClient = new FileDiggerService.FileDiggerClient();
        public MainWindow()
        {
            InitializeComponent();
            this.refreshFolders();
            this.refreshPeers();
        }
       

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            String fileName=this.textBox1.Text;
            String hostString=Dns.GetHostName();
            IPHostEntry hostinfo = Dns.GetHostEntry(hostString);
            System.Net.IPAddress[] addresses = hostinfo.AddressList;
            List<Thread> threads = new List<Thread>();
            List<FileDiggerUtil> diggerUtils = new List<FileDiggerUtil>();

            foreach(IPAddress address in addresses)
            {
                if (!address.IsIPv6LinkLocal)
                {
                    String localIp = address.ToString();
                    localIp=localIp.Substring(0,localIp.LastIndexOf(".")+1);

                    string[] peerIps=localClient.findPeers();
                    foreach (string peerIp in peerIps)
                    {
                        String ip = peerIp;
                        FileDiggerUtil util = new FileDiggerUtil();
                        diggerUtils.Add(util);
                        util.Ip = ip;
                        util.FileName = this.textBox1.Text;
                        ThreadStart ts = new ThreadStart(util.digFiles);
                        Thread t = new Thread(ts);
                        t.Start();
                        threads.Add(t);
                    }
                    foreach(Thread t in threads){
                        t.Join();
                    }
                    break;
                }
            }
            foreach(FileDiggerUtil util in diggerUtils){
                if (util.Files != null)
                {
                    foreach(String f in util.Files)
                    {
                        this.搜到的文件.Items.Add(util.Ip + "--" + f);
                    }
                }
            }
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog d = new FolderBrowserDialog();
            d.ShowDialog();
            string selectedPath=d.SelectedPath;
            if (selectedPath != null)
            {
                localClient.addFolder(selectedPath);
            }
        }

        private void button3_Click(object sender, RoutedEventArgs e)
        {
            refreshFolders();
        }

        private void fileDoubleClicked(object sender, MouseButtonEventArgs e)
        {

        }

        private void button4_Click(object sender, RoutedEventArgs e)
        {
            string folder=(string)this.我的共享目录.SelectedItem;
            localClient.deleteSharedFolder(folder);
        }

        private void button6_Click(object sender, RoutedEventArgs e)
        {
            string peer=(string)this.我的伙伴.SelectedItem;
            localClient.deletePeer(peer);
        }
        private void refreshPeers()
        {
            this.我的伙伴.Items.Clear();
            string[] peers = localClient.findPeers();
            foreach (string p in peers)
            {
                this.我的伙伴.Items.Add(p);
            }
        }
        private void refreshFolders()
        {
            this.我的共享目录.Items.Clear();
            string[] sharedFolders = localClient.findSharedFolders();
            foreach (string sf in sharedFolders)
            {
                this.我的共享目录.Items.Add(sf);
            }
        }
        private void button5_Click(object sender, RoutedEventArgs e)
        {
            ipStruct p = new ipStruct();
            AddPeer ap = new AddPeer();
            ap.Peer = p;
            ap.ShowDialog();
            String ip=p.Ip;
            try
            {
                IPAddress addr = IPAddress.Parse(ip);
                localClient.addPeer(ip);
            }
            catch (Exception parseExce)
            {

            }           
        }

        private void button7_Click(object sender, RoutedEventArgs e)
        {
            refreshPeers();
        }

        private void button8_Click(object sender, RoutedEventArgs e)
        {
            string fileDescription=(string)this.搜到的文件.SelectedItem;
            string []fda=fileDescription.Split(new char[2]{'-','-'});
            string ip = fda[0].Trim();
            string fullName = fda[2].Trim();
            FileDiggerService.FileDiggerClient c = new FileDiggerService.FileDiggerClient();
            c.Endpoint.Address = new EndpointAddress("http://" + ip + ":8000/ServiceModelSamples/service");
            long size=c.fetchFileSize(fullName);
            int times = (int)(size / 1024);
            for (int i = 0; i < times; i++)
            {
                c.fetchFile(fullName, i);
            }
            
        }
    }
}
