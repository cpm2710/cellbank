using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Activities.DurableInstancing;

namespace TrackingWorkFlow
{
    class TrackingSqlWorkflowInstanceStore
    {
        public static string connStr = @"Data Source=.\sqlexpress;Initial Catalog=WorkflowInstanceStore;Integrated Security=True;Pooling=False";
        public static SqlWorkflowInstanceStore generateOne()
        {
            SqlWorkflowInstanceStore store = new SqlWorkflowInstanceStore(connStr);
            return store;
        }
    }
}
