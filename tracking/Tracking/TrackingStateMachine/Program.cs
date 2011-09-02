using System;
using System.Linq;
using System.Activities;
using System.Activities.Statements;
using SEActivities;
using System.Collections.ObjectModel;
using System.Activities.Hosting;
namespace TrackingStateMachine
{

    class Program
    {
        static void Main(string[] args)
        {
          QFEWorkFlow wf=new QFEWorkFlow();
          WorkflowApplication app = new WorkflowApplication(wf);
          app.Run();
          
          

          app.ResumeBookmark(ChooseTransitionEvent.RequireMoreInformation.ToString(), new ChooseTransitionResult());
          ReadOnlyCollection<BookmarkInfo> bookInfos = app.GetBookmarks();
          foreach (BookmarkInfo i in bookInfos)
          {
              Console.WriteLine(i.BookmarkName);
          }
          app.Run();
          bookInfos = app.GetBookmarks();
          foreach (BookmarkInfo i in bookInfos)
          {
              Console.WriteLine(i.BookmarkName);
          }
          app.Run();
            //app.ResumeBookmark(ChooseTransitionEvent

        }
    }
}
