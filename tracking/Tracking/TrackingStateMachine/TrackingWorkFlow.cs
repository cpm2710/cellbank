using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Activities;
using SEActivities;
using System.Collections.ObjectModel;
using System.Activities.Hosting;
using System.Threading;

namespace TrackingWorkFlow
{
    public abstract class TrackingWorkFlow:IDisposable
    {
        public WorkflowApplication app;

        AutoResetEvent barrier = new AutoResetEvent(false);
        
        protected ReadOnlyCollection<BookmarkInfo> currentBookmarks;
        public ReadOnlyCollection<BookmarkInfo> CurrentBookmarks
        {
            get { return currentBookmarks; }
            set { this.currentBookmarks = value; }
        }
        protected void MakeAsyncSync()
        {
            //this.app.PersistableIdle = (e) =>
            //{
            //    return PersistableIdleAction.Unload;
            //};

            this.app.Unloaded = (e) =>
            {

                barrier.Set();

            };
        }
        public TrackingWorkFlow()
        {
            
            //this.app.Unloaded += this.OnWorkflowIdle;
            //this.app.PersistableIdle += this.OnWorkflowIdle;
        }
        //private void OnWorkflowIdle(WorkflowApplicationIdleEventArgs args)
        //{
        //    currentBookmarks = args.Bookmarks;
        //    this.Persist();
        //}
        //public virtual void Persist()
        //{
        //    if (app != null)
        //    {
        //        app.Persist();
        //        this.barrier.WaitOne();
        //    }
        //}
        public virtual void Unload()
        {
            if (app != null)
            {
                app.Unload();
                //app.Unload();
                this.barrier.WaitOne();
            }
        }
        public virtual void Start()
        {
            app.ResumeBookmark(ChooseTransitionCommand.ProcessStart.ToString(), new ChooseTransitionResult());
            app.Idle = (e) =>
            {
                this.barrier.Set();
            };
            this.barrier.WaitOne();
            //Thread.Sleep(5000);
            this.Unload();
        }
        public abstract List<string> GetCandidateCommand();
        public virtual void AcceptCommand(string commandName)
        {
            currentBookmarks = app.GetBookmarks();
            app.ResumeBookmark(commandName, new ChooseTransitionResult());
            this.Unload();
        }

        public void Dispose()
        {
            //this.app.Unload();
            //throw new NotImplementedException();
        }
    }
}
