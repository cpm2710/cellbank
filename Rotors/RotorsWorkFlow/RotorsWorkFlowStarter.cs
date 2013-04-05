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
        public void StartRotorsWorkFlow()
        {
            RotorsWorkFlow workFlow = new RotorsWorkFlow();

            WorkflowApplication wfa = new WorkflowApplication(workFlow);

            wfa.OnUnhandledException += new Func<WorkflowApplicationUnhandledExceptionEventArgs, UnhandledExceptionAction>((e) =>
            {
                Console.WriteLine(e.UnhandledException.ToString());

                if (WorkFlowEnded != null)
                {
                    WorkFlowEnded(this, null);
                }
                return UnhandledExceptionAction.Terminate;
            });
            wfa.Completed += new Action<WorkflowApplicationCompletedEventArgs>((e) =>
            {
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
