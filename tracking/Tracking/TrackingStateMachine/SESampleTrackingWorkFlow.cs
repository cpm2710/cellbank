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
            //app.InstanceStore = InstanceStoreSingleton.Instance.InstanceStore;
            this.MakeAsyncSync();
        }
        public SESampleTrackingWorkFlow(string instanceId)
            : base()
        {
            SESampleWorkFlow wf = new SESampleWorkFlow();
            app = new WorkflowApplication(wf);
            app.InstanceStore = TrackingSqlWorkflowInstanceStore.generateOne();
            //app.InstanceStore = InstanceStoreSingleton.Instance.InstanceStore;
            Guid g=new Guid(instanceId);
            app.Load(g);
            this.MakeAsyncSync();
        }
        //public override void Persist()
        //{
        //    app.Persist();
        //}
        //private void OnWorkflowIdle(WorkflowApplicationIdleEventArgs args)
        //{
        //    currentBookmarks = args.Bookmarks;
        //    this.Persist();
        //}
        public override List<string> GetCandidateCommand()
        {
            ReadOnlyCollection<BookmarkInfo>  bis=this.app.GetBookmarks();
            List<string> candidateCommands=new List<string>();
            foreach(BookmarkInfo bi in bis){
                candidateCommands.Add(bi.BookmarkName);
            }
            return candidateCommands;
        }
    }
}
