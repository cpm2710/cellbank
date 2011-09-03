using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TrackingCommands
{
    public class ProcessStart:Command
    {
        public override void execute()
        {
            Console.WriteLine("this is process start command");
        }
        public override void validate()
        {

        }
    }
}
