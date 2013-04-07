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

    public class FilesChangedEventArgs : EventArgs
    {
        public List<string> FilesChanged { get; set; }
    }

    public class FileSystemEventMonitor
    {

        private int defaultTimeOut = 60000;

        public event EventHandler<FilesChangedEventArgs> Triggered;

        private Timer timer = null;
        private List<string> changedFiles = new List<string>();
        private object lockForFileList = new object();
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
                        FilesChangedEventArgs args = new FilesChangedEventArgs();
                        List<string> newList = new List<string>(changedFiles.Count);

                        changedFiles.ForEach((item) =>
                        {
                            newList.Add(item);
                        });
                        changedFiles.Clear();
                        args.FilesChanged = newList;
                        Triggered(this, args);
                    }
                }));
            }
        }



        public void StartMonitoring()
        {
            FileSystemWatcher watcher = new FileSystemWatcher();
            watcher.Path = Singleton<Constants>.UniqueInstance.SourceRootPath;
            /* Watch for changes in LastAccess and LastWrite times, and
               the renaming of files or directories. */
            watcher.NotifyFilter = 
                NotifyFilters.LastAccess|
                NotifyFilters.LastWrite|
                NotifyFilters.FileName|
                NotifyFilters.DirectoryName|
                NotifyFilters.Attributes;
            // Only watch text files.
            watcher.IncludeSubdirectories = true;
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
            timer.Change(System.Threading.Timeout.Infinite, System.Threading.Timeout.Infinite);
        }

        

        // Define the event handlers. 
        private void OnChanged(object source, FileSystemEventArgs e)
        {
            Singleton<ReportMediator>.UniqueInstance.ReportStatus("File changes: " + e.FullPath + " ");
            if (!changedFiles.Contains(e.FullPath) && e.FullPath.EndsWith(".dll") || e.FullPath.EndsWith(".exe"))
            {
                changedFiles.Add(e.FullPath);
            }
            timer.Change(defaultTimeOut, System.Threading.Timeout.Infinite);
        }
    }
}
