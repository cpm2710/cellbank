using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Activities;
using SEActivities;
using System.Collections.ObjectModel;
using System.Activities.Hosting;

namespace TrackingWorkFlow
{
    public abstract class TrackingWorkFlow
    {
        public WorkflowApplication app;
        protected ReadOnlyCollection<BookmarkInfo> currentBookmarks;
        public ReadOnlyCollection<BookmarkInfo> CurrentBookmarks
        {
            get { return currentBookmarks; }
            set { this.currentBookmarks = value; }
        }


        public virtual void Persist()
        {
            if (app != null)
            {
                app.Persist();
            }
        }

        public abstract List<string> GetCandidateCommand();
        public virtual void AcceptCommand(string commandName)
        {
            currentBookmarks = null;
            app.ResumeBookmark(commandName, new ChooseTransitionResult());
        }
    }
}
