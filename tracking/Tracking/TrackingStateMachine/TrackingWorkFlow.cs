using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Activities;
using SEActivities;
using System.Collections.ObjectModel;
using System.Activities.Hosting;

namespace TrackingStateMachine
{
    public abstract class TrackingWorkFlow
    {
        public WorkflowApplication app;
        private ReadOnlyCollection<BookmarkInfo> currentBookmarks;
        public ReadOnlyCollection<BookmarkInfo> CurrentBookmarks
        {
            get { return currentBookmarks; }
            set { this.currentBookmarks = value; }
        }
        public void AcceptEvent(string eventName)
        {
            currentBookmarks = null;
            app.ResumeBookmark(eventName, new ChooseTransitionResult());
        }
    }
}
