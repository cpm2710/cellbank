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
        static void testOffline()
        {
            SESampleProcess2 p2 = new SESampleProcess2();
            //SESampleWorkFlow p2 = new SESampleWorkFlow();
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

            //bookmarks = app.GetBookmarks();
            //rre.WaitOne();
            WorkflowInstanceExtensionManager extensions = app.Extensions;

            BookmarkResumptionResult result;


            //result= app.ResumeBookmark("ProcessStart", new ChooseTransitionResult());
            //rre.WaitOne();
            //bookmarks = app.GetBookmarks();

            app.Run();

            rre.WaitOne();
            bookmarks = app.GetBookmarks();


            result = app.ResumeBookmark("RequireMoreInformation", new ChooseTransitionResult());
            rre.WaitOne();
            bookmarks = app.GetBookmarks();
            //app.Run();

            result = app.ResumeBookmark("ProvideMoreInformation", new ChooseTransitionResult());
            rre.WaitOne();
            bookmarks = app.GetBookmarks();


            result = app.ResumeBookmark("AssignToInvestigation", new ChooseTransitionResult());
            rre.WaitOne();
            bookmarks = app.GetBookmarks();


            result = app.ResumeBookmark("AssignToTriage", new ChooseTransitionResult());
            rre.WaitOne();
            bookmarks = app.GetBookmarks();

            result = app.ResumeBookmark("FinishProcess", new ChooseTransitionResult());
            rre.WaitOne();
            bookmarks = app.GetBookmarks();

            // ChooseTransitionResult rslt=new ChooseTransitionResult();
            //// rslt.ChooseResult="Not Need";
            // result=app.ResumeBookmark("AssignToTriage", rslt);
            // rre.WaitOne();
            // result = app.ResumeBookmark("FinishProcess", rslt);
            // rre.WaitOne();

            bookmarks = app.GetBookmarks();

            Console.WriteLine();
        }

        static void testOnline()
        {
            TrackingService.TrackingService ts = new TrackingService.TrackingService();
            CommandInfo ci = new CommandInfo();
            ci.WFName = "SESample2TrackingWorkFlow";
            WorkFlowInstance wfi=ts.startWorkFlow(ci);
        }
        static void Main(string[] args)
        {
            //testOffline();


            testOnline();


            AutoResetEvent ee = new AutoResetEvent(false);
            ee.WaitOne();

        }
    }
}
