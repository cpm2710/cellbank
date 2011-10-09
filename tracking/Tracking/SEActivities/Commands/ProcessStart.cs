using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CommonResource;

namespace TrackingCommands
{
    public class ProcessStart:Command
    {
        public string Title;
        public string Path;
        public string Issue__Type;
        public string Priority;
        public string Severity;
        public string Steps;
        public override void execute()
        {
            Console.WriteLine("this is process start command");
            //Console.WriteLine("assigned to" + this.AssignTo);

            string bugId = "bug12345";
            TrackingDataContext tdc = new TrackingDataContext();
            CommonResource.Tracking t = new CommonResource.Tracking();
            t.wfname = this.WFName;
            t.bugid = bugId;
            t.wfinstanceid = new Guid(this.InstanceId);
            tdc.Trackings.InsertOnSubmit(t);
            tdc.SubmitChanges();

        }
        public override void validate()
        {

        }
    }
}
