﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TrackingCommands
{
    public class AssignToTriage : Command
    {
        public override void execute()
        {
            Console.WriteLine("this is process start command");
            Console.WriteLine("assigned to" + this.AssignTo);
            Console.WriteLine("changing the assignedto");
            Console.WriteLine("changed");

        }
        public override void validate()
        {

        }
    }
}
