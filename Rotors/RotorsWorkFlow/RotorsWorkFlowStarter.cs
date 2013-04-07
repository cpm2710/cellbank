using RotorsLib;
// author: andyliuliming@outlook.com
using RotorsLib.Exceptions;
using RotorsLib.Helpers;
using System;
using System.Activities;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RotorsWorkFlow
{
    public class RotorsWorkFlowStarter
    {
        public event EventHandler WorkFlowEnded;

        private bool running = false;

        public bool Running
        {
            get { return running; }
            private set { running = value; }
        }
        private object workFlowLock = new object();

        public void StartRotorsWorkFlow(List<string> sourceFileList)
        {
            lock (workFlowLock)
            {
                if (running)
                {
                    throw new RotorsException("already one workflow is executing.");
                }
                else
                {
                    RotorsWorkFlowDefinition workFlow = new RotorsWorkFlowDefinition();

                    running = true;
                    IDictionary<string, object> variables = new Dictionary<string, object>();
                    if (sourceFileList != null)
                    {
                        variables = new Dictionary<string, object>();
                        variables.Add(Singleton<Constants>.UniqueInstance.FileVariableName, sourceFileList.ToArray());
                    }

                    WorkflowApplication wfa = new WorkflowApplication(workFlow, variables);

                    wfa.OnUnhandledException += new Func<WorkflowApplicationUnhandledExceptionEventArgs, UnhandledExceptionAction>((e) =>
                    {
                        running = false;
                        Console.WriteLine(e.UnhandledException.ToString());

                        if (WorkFlowEnded != null)
                        {
                            WorkFlowEnded(this, null);
                        }
                        return UnhandledExceptionAction.Terminate;
                    });
                    wfa.Completed += new Action<WorkflowApplicationCompletedEventArgs>((e) =>
                    {
                        running = false;
                        if (WorkFlowEnded != null)
                        {
                            WorkFlowEnded(this, null);
                        }
                        Console.WriteLine("Completed" + e.ToString());
                    });
                    wfa.Idle += new Action<WorkflowApplicationIdleEventArgs>((e) =>
                    {
                        Console.WriteLine("Idle" + e.ToString());
                    });

                    wfa.Run();
                }
            }

        }
    }
}
