using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SEActivities;
using TrackingCommands;
namespace FakeServiceAndClient
{
    class Program
    {
        static void Main(string[] args)
        {
            CommandInteraction ci = new CommandInteraction();
            List<string> requiredInputs=ci.getRequiredInputs("ProcessStart");

            Dictionary<string, string> inputWithValues = new Dictionary<string, string>();
            foreach (string input in requiredInputs)
            {
                inputWithValues.Add(input, "st");                
            }
            ci.executeCommand("ProcessStart", inputWithValues);

            //GeneralDataSource psds = new GeneralDataSource();
            //psds.getValueCandidates("assignedto");
            //psds.getCandidate();
        }
    }
}
