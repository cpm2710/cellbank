using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TrackingCommands;

namespace FakeServiceAndClient
{
    class Program
    {
        static void Main(string[] args)
        {
            CommandInteraction ci = new CommandInteraction();
            List<string> requiredInputs=ci.getRequiredInputs("ProcessStart");

            
        }
    }
}
