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
            CommandInfo cinfo = new CommandInfo();
            cinfo.CommandName = "shit";
            cinfo.InstanceId = "123";
            ParameterList pl=new ParameterList();
            pl.Add(new Parameter());
            pl[0].Name = "pname";
            pl[0].Type = "string";
            pl[0].Value = "value1";
            cinfo.ParameterList = pl;
            cinfo.WFName = "wf1";

            MemoryStream stream1 = new MemoryStream();
            DataContractJsonSerializer ser =
              new DataContractJsonSerializer(typeof(CommandInfo));
            ser.WriteObject(stream1, cinfo);
            stream1.Position = 0;
            StreamReader sr = new StreamReader(stream1);
            string instanceStr = sr.ReadToEnd();
            Console.WriteLine(instanceStr);

            CommandInteraction ci = new CommandInteraction();
            List<string> requiredInputs=ci.getRequiredInputs("ProcessStart");

            Dictionary<string, string> inputWithValues = new Dictionary<string, string>();
            foreach (string input in requiredInputs)
            {
                inputWithValues.Add(input, "st");                
            }
            ci.executeCommand("ProcessStart", inputWithValues);

            //GeneralDataSource psds = new GeneralDataSource();
            //psds.getValueCandidates("assignedto");
            //psds.getCandidate();

            //var connStr = @"Data Source=.\sqlexpress;Initial Catalog=WorkflowInstanceStore;Integrated Security=True;Pooling=False";
            //SqlWorkflowInstanceStore instanceStore = new SqlWorkflowInstanceStore(connStr);


            //6d64d963-b950-439a-aeaa-4b9b101528ab

            SESampleWorkFlow wf = new SESampleWorkFlow();

            WorkflowApplication app = new WorkflowApplication(wf);
            app.InstanceStore = InstanceStoreSingleton.Instance.InstanceStore;
            //app.InstanceStore = InstanceStoreSingleton.Instance.InstanceStore;
            // app.Load(new Guid("6d64d963-b950-439a-aeaa-4b9b101528ab"));

            ReadOnlyCollection<BookmarkInfo> oldBookmarks = app.GetBookmarks();


            SESampleTrackingWorkFlow m = new SESampleTrackingWorkFlow();
            m.app.Run();
            ReadOnlyCollection<BookmarkInfo> bookInfos = m.app.GetBookmarks();

            m.AcceptCommand(ChooseTransitionCommand.ProcessStart.ToString());
            while (m.CurrentBookmarks == null)
            {
                Thread.Sleep(1000);
            }
            Guid iId = m.app.Id;
            SESampleTrackingWorkFlow m2 = new SESampleTrackingWorkFlow();

            m2.app.Load(iId);
            ReadOnlyCollection<BookmarkInfo> bookInfos2 = m2.app.GetBookmarks();

            m.AcceptCommand(bookInfos2[0].BookmarkName);
            while (m.CurrentBookmarks == null)
            {
                Thread.Sleep(1000);
            }
            bookInfos2 = m.app.GetBookmarks();

            m.Persist();
            AutoResetEvent e = new AutoResetEvent(false);
            e.WaitOne();
            //app.Run();
            //app.Idle += new Action<WorkflowApplicationIdleEventArgs>(() => { });
            //WorkflowInvoker.in(wf);
            //ReadOnlyCollection<BookmarkInfo> bookmarkInfos=  m.CurrentBookmarks;
            // m.AcceptEvent(ChooseTransitionEvent.RequireMoreInformation.ToString());
            //m.app.ResumeBookmark(ChooseTransitionEvent.RequireMoreInformation.ToString(), new ChooseTransitionResult());

            //WorkflowInstanceQuery que = new WorkflowInstanceQuery();


            //ReadOnlyCollection<BookmarkInfo> bookInfos = app.GetBookmarks();
            //foreach (BookmarkInfo i in bookInfos)
            //{
            //    Console.WriteLine(i.BookmarkName);
            //}
            //app.ResumeBookmark(ChooseTransitionEvent.RequireMoreInformation.ToString(), new ChooseTransitionResult());

            //app.ResumeBookmark(ChooseTransitionEvent.ProvideMoreInformation.ToString(), new ChooseTransitionResult());

            //app.ResumeBookmark(ChooseTransitionEvent.RequireMoreInformation.ToString(), new ChooseTransitionResult());

            //app.ResumeBookmark(ChooseTransitionEvent.ProvideMoreInformation.ToString(), new ChooseTransitionResult());

            //app.ResumeBookmark(ChooseTransitionEvent.RequireMoreInformation.ToString(), new ChooseTransitionResult());

            //app.ResumeBookmark(ChooseTransitionEvent.ProvideMoreInformation.ToString(), new ChooseTransitionResult());

            //app.ResumeBookmark(ChooseTransitionEvent.RequireMoreInformation.ToString(), new ChooseTransitionResult());

            //app.ResumeBookmark(ChooseTransitionEvent.ProvideMoreInformation.ToString(), new ChooseTransitionResult());

            //app.ResumeBookmark(ChooseTransitionEvent.RequireMoreInformation.ToString(), new ChooseTransitionResult());

            //app.ResumeBookmark(ChooseTransitionEvent.ProvideMoreInformation.ToString(), new ChooseTransitionResult());

            //bookInfos = app.GetBookmarks();
            //foreach (BookmarkInfo i in bookInfos)
            //{
            //    Console.WriteLine(i.BookmarkName);
            //}
            //app.Run();
            //app.ResumeBookmark(ChooseTransitionEvent
        }
    }
}
