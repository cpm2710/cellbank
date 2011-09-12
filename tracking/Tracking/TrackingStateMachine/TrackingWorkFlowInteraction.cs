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
                    using(TrackingWorkFlow twf=(TrackingWorkFlow)ci.Invoke(new object[] { })){
                        twf.Start();
                        twf.Persist();
                        twf.Unload();
                        return twf.app.Id.ToString();
                    }                    
                }
            }
            return null;
        }
        public void doCommand(CommandInfo commandInfo)
        {
            Assembly trackingWorkFlowAssembly = Assembly.Load("TrackingWorkFlow");
            Type[] types = trackingWorkFlowAssembly.GetTypes();
            foreach (Type t in types)
            {
                if (t.Name.Equals(commandInfo.WFName))
                {
                    Type tt = typeof(String);
                    ConstructorInfo ci = t.GetConstructor(new Type[] { tt });
                    object wf = ci.Invoke(new object[] { commandInfo.InstanceId });
                    TrackingWorkFlow twf = (TrackingWorkFlow)wf;
                    twf.AcceptCommand(commandInfo.CommandName);
                }
            }
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
                    using(TrackingWorkFlow twf=(TrackingWorkFlow)(ci.Invoke(new object[]{instanceId}))){
                        return twf.GetCandidateCommand();
                    }                  
                }
            }
            return null;
        }
    }
}
