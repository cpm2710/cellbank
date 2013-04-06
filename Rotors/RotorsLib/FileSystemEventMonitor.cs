using RotorsLib.Exceptions;
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
        private string fileSystemPath;

        public string FileSystemPath
        {
            get { return fileSystemPath; }
            set { fileSystemPath = value; }
        }

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
        public FileSystemEventMonitor(string fileSystemPath)
        {
            this.fileSystemPath = fileSystemPath;
            InitializeTimer();
        }


        public void StartMonitoring()
        {
            if (string.IsNullOrEmpty(fileSystemPath))
            {
                throw new RotorsException("file system path is null or empty");
            }
            FileSystemWatcher watcher = new FileSystemWatcher();
            watcher.Path = fileSystemPath;
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
            Singleton<ReportMediator>.UniqueInstance.ReportStatus("File: " + e.FullPath + " " + e.ChangeType);
            timer.Change(defaultTimeOut, System.Threading.Timeout.Infinite);
        }
    }
}
