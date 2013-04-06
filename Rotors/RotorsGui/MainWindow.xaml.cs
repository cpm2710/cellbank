using RotorsLib;
using RotorsLib.Helpers;
using RotorsWorkFlow;
using System;
using System.Activities;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RotorsGui
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, IReportObserver
    {
        public MainWindow()
        {
            InitializeComponent();
            Singleton<ReportMediator>.UniqueInstance.RegisterReportObserver(Singleton<Logger>.UniqueInstance);
            Singleton<ReportMediator>.UniqueInstance.RegisterReportObserver(this);
        }

        private void StartMonitorButton_Click(object sender, RoutedEventArgs e)
        {
            FileSystemEventMonitor monitor = new FileSystemEventMonitor(this.BinaryHomeTextBox.Text);
            monitor.Triggered += monitor_Triggered;
            monitor.StartMonitoring();
        }

        void monitor_Triggered(object sender, EventArgs e)
        {
            Singleton<RotorsWorkFlowStarter>.UniqueInstance.StartRotorsWorkFlow();
        }

        private void ReplaceItButton_Click(object sender, RoutedEventArgs e)
        {
            this.ReplaceItButton.IsEnabled = false;
            Constants.MachineName = this.TargetMachineNameTextBox.Text;
            Constants.UserName = this.UserNameTextBox.Text;
            Constants.PassWord = this.PassWordTextBox.Text;
            Constants.SourceRootPath = this.BinaryHomeTextBox.Text;
            Constants.GacEssentialsPath = string.Format(Constants.GacEssentialsPathFormat, Constants.MachineName);
            Constants.System32EssentialsPath = string.Format(Constants.System32EssentialsPathFormat, Constants.MachineName);

            Singleton<RotorsWorkFlowStarter>.UniqueInstance.WorkFlowEnded += UniqueInstance_WorkFlowEnded;
            Singleton<RotorsWorkFlowStarter>.UniqueInstance.StartRotorsWorkFlow();
        }

        void UniqueInstance_WorkFlowEnded(object sender, EventArgs e)
        {
            this.Dispatcher.Invoke(() =>
            {
                this.ReplaceItButton.IsEnabled = true;
            });

        }

        private void BrowseBinaryHomeButton_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new FolderBrowserDialog();
            DialogResult result = dialog.ShowDialog();
            if (result == System.Windows.Forms.DialogResult.OK)
            {
                this.BinaryHomeTextBox.Text = dialog.SelectedPath;
            }
        }

        public void ReportStatus(string statusMsg, LogLevel logLevel = LogLevel.Information)
        {
            this.Dispatcher.Invoke(() =>
            {
                this.ActionStatus.Text = statusMsg;

                if (logLevel > LogLevel.Information)
                {
                    this.ErrorResultTextBlock.Text += ("\n" + statusMsg);
                }
            });
        }
    }
}
