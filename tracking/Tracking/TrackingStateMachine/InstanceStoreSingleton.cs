using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Activities.DurableInstancing;

namespace TrackingWorkFlow
{
    class InstanceStoreSingleton
    {
        private SqlWorkflowInstanceStore instanceStore;
        public SqlWorkflowInstanceStore InstanceStore
        {
            get
            {
                return instanceStore;
            }
            set
            {
                this.instanceStore = value;
            }
        }
        private InstanceStoreSingleton()
        {
            string connStr = @"Data Source=.\sqlexpress;Initial Catalog=WorkflowInstanceStore;Integrated Security=True;Pooling=False";
            instanceStore = new SqlWorkflowInstanceStore(connStr);
        }
        private static object syncRoot = new object();
        private static InstanceStoreSingleton instance;
        public static InstanceStoreSingleton Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                            instance = new InstanceStoreSingleton();
                    }
                }
                return instance;
            }
        }
    }
}
