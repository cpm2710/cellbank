using System;
using System.Linq;
using System.Activities;
using System.Activities.Statements;
using System.Collections.Generic;
using System.Activities.DurableInstancing;


namespace WorkflowConsoleApplication1
{

    class Program
    {
        static void Main(string[] args)
        {
            var connStr = @"Data Source=.\sqlexpress;Initial Catalog=WorkflowInstanceStore;Integrated Security=True;Pooling=False";
            SqlWorkflowInstanceStore instanceStore = new SqlWorkflowInstanceStore(connStr);
            
            Workflow1 w = new Workflow1();
            
            //instanceStore.
            //IDictionary<string, object> xx=WorkflowInvoker.Invoke(new Workflow1());
            //ICollection<string> keys=xx.Keys;
            
            WorkflowApplication a = new WorkflowApplication(w);
            a.InstanceStore = instanceStore;
            a.GetBookmarks();
            //a.ResumeBookmark("Final State", new object());
           
            a.Run();
            //a.ResumeBookmark(

            
            //a.Persist();
            //a.Unload();
            //Guid id = a.Id;
            //WorkflowApplication b = new WorkflowApplication(w);
            //b.InstanceStore = instanceStore;
            //b.Load(id);
            //WorkflowApplication a=new WorkflowApplication(
        }
    }
}
