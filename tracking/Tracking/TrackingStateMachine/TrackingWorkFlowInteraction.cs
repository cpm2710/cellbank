using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using CommonResource;
namespace TrackingWorkFlow
{
    public class TrackingWorkFlowInteraction
    {
        public string startProcess(string wfName)
        {
            Assembly trackingWorkFlowAssembly = Assembly.Load("TrackingWorkFlow");
            Type[] types = trackingWorkFlowAssembly.GetTypes();
            foreach (Type t in types)
            {
                if (t.Name.Equals(wfName))
                {
                    ConstructorInfo ci = t.GetConstructor(new Type[] { });
                    object wf=ci.Invoke(new object[] { });
                    TrackingWorkFlow twf = (TrackingWorkFlow)wf;
                    twf.Start();
                    return twf.app.Id.ToString();
                    //List<string> candCmds=twf.GetCandidateCommand();
                    //return candCmds;
                }
            }
            return null;
        }
        public void doCommand(CommandInfo commandInfo)
        {

        }
        public List<string> getCandidateCommands(string wfName, string instanceId)
        {
            Assembly trackingWorkFlowAssembly=Assembly.Load("TrackingWorkFlow");
            Type[] types = trackingWorkFlowAssembly.GetTypes();
            List<string> requiredInputs = new List<string>();
            foreach (Type t in types)
            {
                if (t.Name.Equals(wfName))
                {
                  Type tt=  typeof(String);
                  ConstructorInfo ci = t.GetConstructor(new Type[]{tt});
                  object wf=ci.Invoke(new object[]{instanceId});
                  TrackingWorkFlow twf = (TrackingWorkFlow)wf;
                  return twf.GetCandidateCommand();
                }
            }
            return null;
        }
    }
}
