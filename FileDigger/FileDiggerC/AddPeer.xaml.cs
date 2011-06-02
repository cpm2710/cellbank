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
using System.Windows.Shapes;

namespace FileDiggerC
{
    /// <summary>
    /// Interaction logic for AddPeer.xaml
    /// </summary>
    public partial class AddPeer : Window
    {
        private ipStruct peer;

        public ipStruct Peer
        {
            get { return peer; }
            set { peer = value; }
        }
        public AddPeer()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            peer.Ip = this.textBox1.Text;
            this.Close();
        }
    }
}
