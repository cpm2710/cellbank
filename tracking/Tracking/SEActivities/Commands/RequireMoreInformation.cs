using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TrackingCommands
{
    class RequireMoreInformation:Command
    {
        public override void execute()
        {
            Console.WriteLine("this is process start command");
            Console.WriteLine("assigned to" + this.AssignTo);
        }
        public override void validate()
        {

        }
    }
}
