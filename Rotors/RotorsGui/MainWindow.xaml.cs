using RotorsLib;
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
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ListBox_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {

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
            Constants.MachineName = this.TargetMachineNameTextBox.Text;
            Constants.UserName = this.UserNameTextBox.Text;
            Constants.PassWord = this.PassWordTextBox.Text;
            Constants.SourceRootPath = this.BinaryHomeTextBox.Text;
            RotorsWorkFlow.RotorsWorkFlow workFlow = new RotorsWorkFlow.RotorsWorkFlow();

            WorkflowApplication wfa = new WorkflowApplication(workFlow);

            wfa.OnUnhandledException += new Func<WorkflowApplicationUnhandledExceptionEventArgs, UnhandledExceptionAction>((wfE) =>
            {
                Console.WriteLine(wfE.UnhandledException.ToString());
                return UnhandledExceptionAction.Terminate;
            });
            wfa.Completed += new Action<WorkflowApplicationCompletedEventArgs>((wfE) =>
            {
                Console.WriteLine("Completed" + wfE.ToString());
            });
            wfa.Idle += new Action<WorkflowApplicationIdleEventArgs>((wfE) =>
            {
                Console.WriteLine("Idle" + wfE.ToString());
            });

            wfa.Run();
        }

        private void BrowseBinaryHomeButton_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new FolderBrowserDialog();
            DialogResult result = dialog.ShowDialog();
        }
    }
}
