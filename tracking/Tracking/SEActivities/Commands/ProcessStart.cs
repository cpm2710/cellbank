using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CommonResource;
using SEActivities.DataAccess;

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

        private string qFEStatus;

        public string QFEStatus
        {
            get { return qFEStatus; }
            set { qFEStatus = value; }
        }

        public override void execute()
        {
            Console.WriteLine("this is process start command");
            //Console.WriteLine("assigned to" + this.AssignTo);
            TeamInfo teamInfo = TeamInfoAccess.getTeam(0);
            string bugId = "N/A";
            using (PSDataAccess psDataAccess = new PSDataAccess(teamInfo.domain, teamInfo.product))
            {
                bugId = "bug12345";
            }
            
            TrackingDataContext tdc = new TrackingDataContext();
            CommonResource.Tracking t = new CommonResource.Tracking();
            t.wfname = this.WFName;
            t.bugid = bugId;
            t.wfinstanceid = new Guid(this.InstanceId);
            t.lastmodified = DateTime.Now;
            t.assignedto = this.AssignedTo;
            t.title = this.Title;
            t.qfestatus = this.qFEStatus;
            
            //t.qfestatus=
            t.lastmodifiedby = AuthenticationHelper.GetCurrentUser();
            tdc.Trackings.InsertOnSubmit(t);
            tdc.SubmitChanges();

        }
        public override void validate()
        {

        }
    }
}
