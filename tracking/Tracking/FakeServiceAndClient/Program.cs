﻿using System;
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
        static void testReadXaml()
        {
            SESampleProcess2 ss = new SESampleProcess2();

            string[] resources = typeof(SESampleProcess2).Assembly.GetManifestResourceNames();
            string resourceName = null;
            for (int i = 0; (i < resources.Length); i = (i + 1))
            {
                resourceName = resources[i];
                if ((resourceName.Contains(".SESampleProcess2.g.xaml") || resourceName.Equals("SESampleProcess2.g.xaml")))
                {
                    break;
                }
            }
            System.IO.Stream initializeXaml = typeof(SESampleProcess2).Assembly.GetManifestResourceStream(resourceName);
            Console.WriteLine(initializeXaml.ToString());
            System.Xml.XmlReader xmlReader = null;
            System.Xaml.XamlReader reader = null;
            System.Xaml.XamlObjectWriter objectWriter = null;
            try
            {
                //System.Xaml.XamlSchemaContext schemaContext = XamlStaticHelperNamespace._XamlStaticHelper.SchemaContext;
                xmlReader = System.Xml.XmlReader.Create(initializeXaml);
                System.Xaml.XamlXmlReaderSettings readerSettings = new System.Xaml.XamlXmlReaderSettings();
                readerSettings.LocalAssembly = System.Reflection.Assembly.GetExecutingAssembly();
                readerSettings.AllowProtectedMembersOnRoot = true;
                //reader = new System.Xaml.XamlXmlReader(xmlReader, schemaContext, readerSettings);
                System.Xaml.XamlObjectWriterSettings writerSettings = new System.Xaml.XamlObjectWriterSettings();
                //writerSettings.RootObjectInstance = this;
                //writerSettings.AccessLevel = System.Xaml.Permissions.XamlAccessLevel.PrivateAccessTo(typeof(SESampleProcess2));
                //objectWriter = new System.Xaml.XamlObjectWriter(schemaContext, writerSettings);
                //System.Xaml.XamlServices.Transform(reader, objectWriter);
            }
            finally
            {
                if ((xmlReader != null))
                {
                    ((System.IDisposable)(xmlReader)).Dispose();
                }
                if ((reader != null))
                {
                    ((System.IDisposable)(reader)).Dispose();
                }
                if ((objectWriter != null))
                {
                    ((System.IDisposable)(objectWriter)).Dispose();
                }
            }

            Console.WriteLine();
            //string resourceName = this.FindResource();
            //System.IO.Stream initializeXaml = typeof(SESampleProcess2).Assembly.GetManifestResourceStream(resourceName);
        }
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
            //WorkFlowInstance wfi=ts.startWorkFlow(ci);

            ci.InstanceId = "f3875fd4-8bf2-4774-a754-928fcef66e5c";
            ci.CommandName = "AssignToTriage";
            ci.ParameterList = ts.GetParameters(ci.CommandName); 
            ts.doCommand(ci);



            WorkFlowInstance wfi = ts.GetWorkFlowInstance(ci.InstanceId);
            Console.WriteLine("shi");
        }
        static void testStartProcess()
        {
            TrackingService.TrackingService ts = new TrackingService.TrackingService();
            CommandInfo ci = new CommandInfo();
            ci.WFName = "SESample2TrackingWorkFlow";
            //WorkFlowInstance wfi=ts.startWorkFlow(ci);

            //ci.InstanceId = "f3875fd4-8bf2-4774-a754-928fcef66e5c";
            ci.CommandName = "ProcessStart";
            ci.ParameterList = ts.GetParameters(ci.CommandName);
            ts.startWorkFlow(ci);



            WorkFlowInstance wfi = ts.GetWorkFlowInstance(ci.InstanceId);
            Console.WriteLine("shi");
        }
        static void Main(string[] args)
        {
            //testOffline();
            testReadXaml();
            //testStartProcess();
            //testOnline();


            AutoResetEvent ee = new AutoResetEvent(false);
            ee.WaitOne();

        }
    }
}
