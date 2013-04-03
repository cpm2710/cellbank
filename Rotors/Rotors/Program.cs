using System;
using System.Activities;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rotors
{
    class Program
    {
        static void Main(string[] args)
        {
            WorkflowApplication wfa = new WorkflowApplication(new RotorsWorkFlow.RotorsWorkFlow());
            wfa.OnUnhandledException += new Func<WorkflowApplicationUnhandledExceptionEventArgs, UnhandledExceptionAction>((e) =>
            {
                Console.WriteLine(e.ToString());
                return UnhandledExceptionAction.Terminate;
            });
            wfa.Completed += new Action<WorkflowApplicationCompletedEventArgs>((e) =>
            {
                Console.WriteLine("ssss" + e.ToString());
            });
            wfa.Idle += new Action<WorkflowApplicationIdleEventArgs>((e) =>
            {
                Console.WriteLine("ssss2" + e.ToString());
            });

            wfa.Run();

            Console.Read();
        }
    }
}
