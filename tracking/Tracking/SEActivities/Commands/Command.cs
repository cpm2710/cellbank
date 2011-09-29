using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TrackingCommands
{
    public abstract class Command
    {
        private string instanceId;

        public string InstanceId
        {
            get { return instanceId; }
            set { instanceId = value; }
        }
        private string wFName;

        public string WFName
        {
            get { return wFName; }
            set { wFName = value; }
        }
        public string AssignTo;
        public abstract void execute();
        public abstract void validate();
    }
}
