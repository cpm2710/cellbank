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
using System.Activities.Tracking;
namespace FakeServiceAndClient
{
    class Program
    {

        static void Main(string[] args)
        {
            SESampleWorkFlow p2 = new SESampleWorkFlow();
            WorkflowApplication app = new WorkflowApplication(p2);
            AutoResetEvent rre = new AutoResetEvent(false);
            app.Idle = (e) =>
            {
                rre.Set();
            };
            //TrackingProfile trackingProfile = new TrackingProfile();

            ////trackingProfile.Queries.Add(new WorkflowInstanceQuery

            ////{

            ////    States = { "*"}

            ////});

            ////trackingProfile.Queries.Add(new ActivityStateQuery

            ////{

            ////    States = {"*" }

            ////});

            //trackingProfile.Queries.Add(new CustomTrackingQuery

            //{

            //   ActivityName="*",

            //   Name = "*"

            //});
            //SETrackingParticipant p = new SETrackingParticipant();
            //p.TrackingProfile = trackingProfile;
            //app.Extensions.Add(p);

            app.Completed = (e) =>
            {
                Console.WriteLine("shit");
            };
            ReadOnlyCollection<BookmarkInfo> bookmarks = null;

            WorkflowInstanceExtensionManager extensions=app.Extensions;

            BookmarkResumptionResult result;
            //bookmarks = app.GetBookmarks();

            result= app.ResumeBookmark("ProcessStart", new ChooseTransitionResult());
            rre.WaitOne();
            //app.Run();

            //bookmarks = app.GetBookmarks();


             //result=app.ResumeBookmark("RequireMoreInformation", new ChooseTransitionResult());
            //app.Run();

             //result = app.ResumeBookmark("ProvideMoreInformation", new ChooseTransitionResult());

            bookmarks = app.GetBookmarks();
            ChooseTransitionResult rslt=new ChooseTransitionResult();
           // rslt.ChooseResult="Not Need";
            result=app.ResumeBookmark("AssignToTriage", rslt);
            rre.WaitOne();
            result = app.ResumeBookmark("FinishProcess", rslt);
            rre.WaitOne();
            
            bookmarks = app.GetBookmarks();
            
            Console.WriteLine();

            //SESampleWorkFlow wf = new SESampleWorkFlow();
            //ReadOnlyCollection<BookmarkInfo> bookmarks = null;
            //WorkflowApplication app = new WorkflowApplication(wf);
            //app.Completed = (e) =>
            //{
            //    Console.WriteLine("completed");
            //};
            //app.ResumeBookmark("ProcessStart", new ChooseTransitionResult());

            //bookmarks = app.GetBookmarks();

            //app.ResumeBookmark("AssignToTriage",new ChooseTransitionResult());
            
            //bookmarks = app.GetBookmarks();

            //app.ResumeBookmark("FinishProcess", new ChooseTransitionResult());
            //bookmarks = app.GetBookmarks();
            //Console.WriteLine("shit");
            //TrackingService.TrackingService ts = new TrackingService.TrackingService();
            //CommandInfo ci = new CommandInfo();
            //ci.CommandName = "RequireMoreInformation";
            //ci.InstanceId = "27931dad-a839-4dec-aee2-8a7d2e6c672e";
            //ci.WFName = "SESampleTrackingWorkFlow";
            //ci.ParameterList = new ParameterList();
            //ci.ParameterList.Add(new Parameter("AssignTo","t-limliu"));
            //ts.doCommand(ci);


            //WorkFlowInstance wfi = ts.GetWorkFlowInstance("46033934-f29a-4075-a1d6-65a9a57db439");
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
