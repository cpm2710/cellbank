using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SEActivities;
using TrackingCommands;
using CommonResource;
using System.IO;
using System.Runtime.Serialization.Json;
using TrackingWorkFlow;
using System.Activities;
using System.Collections.ObjectModel;
using System.Activities.Hosting;
using System.Activities.DurableInstancing;
using System.Runtime.DurableInstancing;
using System.Threading;
namespace FakeServiceAndClient
{
    class Program
    {

        static void Main(string[] args)
        {
            TrackingService.TrackingService ts = new TrackingService.TrackingService();
            CommandInfo ci = new CommandInfo();
            ci.CommandName="AssignToTriage";
            ci.InstanceId = "c0c2abaa-6f65-46a2-9f47-686d672dc99b";
            ci.WFName = "SESampleTrackingWorkFlow";
            ci.ParameterList = new ParameterList();
            ci.ParameterList.Add(new Parameter("AssignTo","t-limliu"));
            ts.doCommand(ci);
            //WorkFlowInstance wfi=ts.GetWorkFlowInstance("966b0c2d-fcb9-4b94-931c-e86ed7c75657");

            //WorkFlowInstanceList wfil = ts.GetWorkFlowInstances();

            
            //CommandInfo ci=new CommandInfo();
            //ci.WFName="SESampleTrackingWorkFlow";
            //WorkFlowInstance startedOne=ts.startWorkFlow(ci);

            //foreach (WorkFlowInstance wfi in wfil)
            //{
            //    WorkFlowInstance gotWFI = ts.GetWorkFlowInstance(wfi.Id);
            //    CandidateCommandList commandsList = gotWFI.CandidateCommandList;
            //    Console.WriteLine(wfi.Id + ":");
            //    if (commandsList != null)
            //    {
            //        foreach (string cmd in commandsList)
            //        {
            //            Console.WriteLine(cmd);
            //        }
            //    }
            //}


            AutoResetEvent ee = new AutoResetEvent(false);
            ee.WaitOne();

        }
    }
}
