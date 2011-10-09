using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TrackingCommands
{
    class ProvideMoreInformation:Command
    {
        public override void execute()
        {
            Console.WriteLine("this is process start command");
            //Console.WriteLine("assigned to" + this.Assigned__To);
        }
        public override void validate()
        {

        }
    }
}
