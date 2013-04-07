﻿// author: andyliuliming@outlook.com
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
            this.StartMonitorButton.Content = StartMonitor;
            Singleton<ReportMediator>.UniqueInstance.RegisterReportObserver(Singleton<Logger>.UniqueInstance);
            Singleton<ReportMediator>.UniqueInstance.RegisterReportObserver(this);
        }

        private const string StopMonitor = "Stop Monitor";
        private const string StartMonitor = "Start Monitoe";
        private void StartMonitorButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.Equals(this.StartMonitorButton.Content, StartMonitor))
            {
                InitializeConstants();
                Singleton<FileSystemEventMonitor>.UniqueInstance.Triggered -= monitor_Triggered;
                Singleton<FileSystemEventMonitor>.UniqueInstance.Triggered += monitor_Triggered;
                Singleton<FileSystemEventMonitor>.UniqueInstance.StartMonitoring();

                this.StartMonitorButton.Content = StopMonitor;
            }
            else
            {
                Singleton<FileSystemEventMonitor>.UniqueInstance.StopMonitoring();
                this.StartMonitorButton.Content = StartMonitor;
            }
        }

        void monitor_Triggered(object sender, FilesChangedEventArgs e)
        {
            Singleton<RotorsWorkFlowStarter>.UniqueInstance.StartRotorsWorkFlow(e.FilesChanged);
        }

        private void InitializeConstants()
        {
            Singleton<Constants>.UniqueInstance.MachineName = this.TargetMachineNameTextBox.Text;
            Singleton<Constants>.UniqueInstance.UserName = this.UserNameTextBox.Text;
            Singleton<Constants>.UniqueInstance.PassWord = this.PassWordTextBox.Text;
            Singleton<Constants>.UniqueInstance.SourceRootPath = this.BinaryHomeTextBox.Text;
        }

        private void ReplaceItButton_Click(object sender, RoutedEventArgs e)
        {
            this.ReplaceItButton.IsEnabled = false;

            InitializeConstants();
            Singleton<RotorsWorkFlowStarter>.UniqueInstance.WorkFlowEnded += UniqueInstance_WorkFlowEnded;
            Singleton<RotorsWorkFlowStarter>.UniqueInstance.StartRotorsWorkFlow(null);
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
