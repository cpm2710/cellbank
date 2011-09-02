using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Activities;
using System.Collections.ObjectModel;
using System.Activities.Hosting;
using System.Activities.DurableInstancing;
using System.Threading;
using SEActivities;
namespace TrackingStateMachine
{
    public class SETrackingMachine
    {
        public WorkflowApplication app;
        string connStr = @"Data Source=.\sqlexpress;Initial Catalog=WorkflowInstanceStore;Integrated Security=True;Pooling=False";
        SqlWorkflowInstanceStore instanceStore ;
        AutoResetEvent are = new AutoResetEvent(false);
        AutoResetEvent nextEventEvent = new AutoResetEvent(false);
        public SETrackingMachine()
        {
            QFEWorkFlow wf = new QFEWorkFlow(); 
            instanceStore = new SqlWorkflowInstanceStore(connStr);
            app = new WorkflowApplication(wf);
            app.InstanceStore = instanceStore;
            app.Idle += this.OnWorkflowIdle;            
        }
        public SETrackingMachine(string instanceId)
        {

        }
        private ReadOnlyCollection<BookmarkInfo> currentBookmarks;
        public ReadOnlyCollection<BookmarkInfo> CurrentBookmarks
        {
            get { return currentBookmarks; }
            set {this.currentBookmarks=value; }
        }
        private void OnWorkflowIdle(WorkflowApplicationIdleEventArgs args)
        {
            currentBookmarks = args.Bookmarks;
            //if (nextEvent == null)
            //{
            //    nextEventEvent.WaitOne();
            //}
            //app.ResumeBookmark(nextEvent, new ChooseTransitionResult());
            //nextEvent = null;
            //this.are.Reset();
        }
        private string nextEvent = null;
        public void AcceptEvent(string eventName)
        {
            currentBookmarks = null;
            app.ResumeBookmark(eventName, new ChooseTransitionResult());
            //are.WaitOne();
            //nextEvent = eventName;
            //nextEventEvent.Reset();
            //
        }
    }
}
