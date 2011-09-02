using System;
using System.Linq;
using System.Activities;
using System.Activities.Statements;
using SEActivities;
using System.Collections.ObjectModel;
using System.Activities.Hosting;
using System.Activities.Tracking;
using System.Threading;
using System.Activities.DurableInstancing;
namespace TrackingStateMachine
{

    class Program
    {
        static void Main(string[] args)
        {
            //var connStr = @"Data Source=.\sqlexpress;Initial Catalog=WorkflowInstanceStore;Integrated Security=True;Pooling=False";
            //SqlWorkflowInstanceStore instanceStore = new SqlWorkflowInstanceStore(connStr);


            //6d64d963-b950-439a-aeaa-4b9b101528ab

            QFEWorkFlow wf = new QFEWorkFlow();

            WorkflowApplication app = new WorkflowApplication(wf);
            string connStr = @"Data Source=.\sqlexpress;Initial Catalog=WorkflowInstanceStore;Integrated Security=True;Pooling=False";
            SqlWorkflowInstanceStore instanceStore;
            instanceStore = new SqlWorkflowInstanceStore(connStr);

            app.InstanceStore = instanceStore;
            app.Load(new Guid("6d64d963-b950-439a-aeaa-4b9b101528ab"));

            ReadOnlyCollection<BookmarkInfo> oldBookmarks=app.GetBookmarks();


            SETrackingMachine m = new SETrackingMachine();
            m.app.Run();
            ReadOnlyCollection<BookmarkInfo> bookInfos = m.app.GetBookmarks();


            m.AcceptEvent(ChooseTransitionEvent.ProcessStart.ToString());
            while (m.CurrentBookmarks == null)
            {
                Thread.Sleep(1000);
            }
            ReadOnlyCollection<BookmarkInfo> bookInfos2=m.app.GetBookmarks();

            m.AcceptEvent(bookInfos2[0].BookmarkName);
            while (m.CurrentBookmarks == null)
            {
                Thread.Sleep(1000);
            }
            bookInfos2 = m.app.GetBookmarks();

            m.PersistMachine();
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
