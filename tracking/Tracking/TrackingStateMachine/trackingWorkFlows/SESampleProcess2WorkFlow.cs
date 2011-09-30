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
    public class SESampleProcess2WorkFlow : TrackingWorkFlow
    {
        public SESampleProcess2WorkFlow(bool persist)
        {
            SESampleProcess2 wf = new SESampleProcess2();
            app = new WorkflowApplication(wf);
        }
        public SESampleProcess2WorkFlow():base()
        {
            SESampleProcess2 wf = new SESampleProcess2();            
            app = new WorkflowApplication(wf);
            //app.WorkflowDefinition
            app.InstanceStore = TrackingSqlWorkflowInstanceStore.generateOne();
            this.MakeAsyncSync();
        }
        public SESampleProcess2WorkFlow(string instanceId)
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
