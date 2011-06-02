using System;
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

namespace FileDiggerC
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        FileDiggerService.FileDiggerClient localClient = new FileDiggerService.FileDiggerClient();

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            String fileName=this.textBox1.Text;
            String hostString=Dns.GetHostName();
            IPHostEntry hostinfo = Dns.GetHostEntry(hostString);
            System.Net.IPAddress[] addresses = hostinfo.AddressList;
            foreach(IPAddress address in addresses)
            {
                if (!address.IsIPv6LinkLocal)
                {
                    String localIp = address.ToString();
                    localIp=localIp.Substring(0,localIp.LastIndexOf(".")+1);
                    Ping p = new Ping();
                    for (int i = 100; i < 101; i++)
                    {
                        String ip = localIp + i;
                        IPAddress addr = IPAddress.Parse(ip);
                        PingReply reply = p.Send(addr);
                        FileDiggerService.FileDiggerClient c = new FileDiggerService.FileDiggerClient();

                        if (reply.Status != IPStatus.TimedOut)
                        {                            
                            c.Endpoint.Address = new EndpointAddress("http://" + ip + ":8000/ServiceModelSamples/service");
                            try
                            {
                                string[] files = c.findFile(this.textBox1.Text);
                                if (files != null && files.Length > 0)
                                {
                                    foreach (string f in files)
                                    {
                                        this.listView1.Items.Add(ip + "--" + f);
                                    }
                                    break;
                                }
                                c.Close();
                            }
                            catch (Exception exception)
                            {
                                this.textBox2.Text = this.textBox2.Text + exception + "\n";
                            }
                        }
                    }
                    break;
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
    }
}
