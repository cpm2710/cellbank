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
    public class SESample2TrackingWorkFlow : TrackingWorkFlow
    {        
        
        public SESample2TrackingWorkFlow():base()
        {
            SESampleProcess2 wf = new SESampleProcess2();            
            app = new WorkflowApplication(wf);
            app.InstanceStore = TrackingSqlWorkflowInstanceStore.generateOne();
            this.MakeAsyncSync();
        }
        public SESample2TrackingWorkFlow(string instanceId)
            : base()
        {
            SESampleProcess2 wf = new SESampleProcess2();
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
