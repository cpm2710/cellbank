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
namespace TrackingWorkFlow
{
    public class SESampleTrackingWorkFlow : TrackingWorkFlow
    {        
        
        public SESampleTrackingWorkFlow():base()
        {
            SESampleWorkFlow wf = new SESampleWorkFlow();            
            app = new WorkflowApplication(wf);
            app.InstanceStore = TrackingSqlWorkflowInstanceStore.generateOne();
            this.MakeAsyncSync();
        }
        public SESampleTrackingWorkFlow(string instanceId)
            : base()
        {
            SESampleWorkFlow wf = new SESampleWorkFlow();
            app = new WorkflowApplication(wf);
            app.InstanceStore = TrackingSqlWorkflowInstanceStore.generateOne();
            Guid g=new Guid(instanceId);
            app.Load(g);
            this.MakeAsyncSync();
        }
        public override List<string> GetCandidateCommand()
        {
            ReadOnlyCollection<BookmarkInfo>  bis=this.app.GetBookmarks();
            List<string> candidateCommands=new List<string>();
            foreach(BookmarkInfo bi in bis){
                candidateCommands.Add(bi.BookmarkName);
            }
            this.Unload();
            return candidateCommands;
        }
    }
}
