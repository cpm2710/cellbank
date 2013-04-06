// author: andyliuliming@outlook.com
using RotorsLib.Exceptions;
using RotorsLib.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RotorsLib
{
    public class FileSystemEventMonitor
    {

        private int defaultTimeOut = 60000;

        public event EventHandler Triggered;

        private Timer timer = null;

        public FileSystemEventMonitor()
        {
            InitializeTimer();
        }

        private void InitializeTimer()
        {
            if (timer == null)
            {
                timer = new Timer(new TimerCallback((o) =>
                {
                    if (Triggered != null)
                    {
                        Triggered(this, null);
                    }
                }));
            }
        }
        


        public void StartMonitoring()
        {
           
            FileSystemWatcher watcher = new FileSystemWatcher();
            watcher.Path = Constants.SourceRootPath;
            /* Watch for changes in LastAccess and LastWrite times, and
               the renaming of files or directories. */
            watcher.NotifyFilter = NotifyFilters.LastAccess | NotifyFilters.LastWrite
               | NotifyFilters.FileName | NotifyFilters.DirectoryName;
            // Only watch text files.

            // Add event handlers.
            watcher.Changed += new FileSystemEventHandler(OnChanged);
            watcher.Created += new FileSystemEventHandler(OnChanged);
            watcher.Deleted += new FileSystemEventHandler(OnChanged);
            watcher.Renamed += new RenamedEventHandler(OnChanged);

            // Begin watching.
            watcher.EnableRaisingEvents = true;
        }

        public void StopMonitoring()
        {

        }



        // Define the event handlers. 
        private void OnChanged(object source, FileSystemEventArgs e)
        {
            Singleton<ReportMediator>.UniqueInstance.ReportStatus("File changes: " + e.FullPath + " ");
            timer.Change(defaultTimeOut, System.Threading.Timeout.Infinite);
        }
    }
}
