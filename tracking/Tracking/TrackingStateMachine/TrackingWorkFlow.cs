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
        public TrackingWorkFlow()
        {

        }

        public virtual void Persist()
        {
            if (app != null)
            {
                app.Persist();
            }
        }
        public virtual void Unload()
        {
            if (app != null)
            {
                app.Unload();
            }
        }
        public virtual void Start()
        {
            app.ResumeBookmark(ChooseTransitionCommand.ProcessStart.ToString(), new ChooseTransitionResult());
        }
        public abstract List<string> GetCandidateCommand();
        public virtual void AcceptCommand(string commandName)
        {
            currentBookmarks = null;
            app.ResumeBookmark(commandName, new ChooseTransitionResult());
        }

        public void Dispose()
        {
            //throw new NotImplementedException();
        }
    }
}
