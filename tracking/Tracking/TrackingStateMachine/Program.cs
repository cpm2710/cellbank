using System;
using System.Linq;
using System.Activities;
using System.Activities.Statements;
using SEActivities;
using System.Collections.ObjectModel;
using System.Activities.Hosting;
using System.Activities.Tracking;
namespace TrackingStateMachine
{

    class Program
    {
        static void Main(string[] args)
        {
            SETrackingMachine m = new SETrackingMachine();
            m.app.Run();

          //app.Run();
          //app.Idle += new Action<WorkflowApplicationIdleEventArgs>(() => { });
          //WorkflowInvoker.in(wf);
          //ReadOnlyCollection<BookmarkInfo> bookmarkInfos=  m.CurrentBookmarks;
            m.AcceptEvent(ChooseTransitionEvent.RequireMoreInformation.ToString());
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
