using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Activities;
using System.Collections.ObjectModel;
using System.Activities.Hosting;

namespace TrackingStateMachine
{
    public class SETrackingMachine
    {
        public WorkflowApplication app;
        public SETrackingMachine()
        {
            QFEWorkFlow wf = new QFEWorkFlow();
            app = new WorkflowApplication(wf);
            app.Idle += this.OnWorkflowIdle;
            //app.Completed
        }
        private void OnWorkflowIdle(WorkflowApplicationIdleEventArgs args)
        {
            ReadOnlyCollection<BookmarkInfo> bookInfos = args.Bookmarks;

        }
    }
}
