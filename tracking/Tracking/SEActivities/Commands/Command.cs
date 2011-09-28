using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TrackingCommands
{
    public abstract class Command
    {
        private string InstanceId;
        public string AssignTo;
        public abstract void execute();
        public abstract void validate();
    }
}
