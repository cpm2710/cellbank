using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using CommonResource;
using System.Collections.ObjectModel;
using System.Activities.Hosting;
namespace TrackingWorkFlow
{
    public class TrackingWorkFlowInteraction : IDisposable
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
                    string appId = null;
                    using (TrackingWorkFlow twf = (TrackingWorkFlow)ci.Invoke(new object[] { }))
                    {
                        twf.Start();
                        //twf.Persist();
                        //twf.Unload();//should make it the sync call
                        appId= twf.app.Id.ToString();
                    }
                    return appId;
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
                    using (TrackingWorkFlow twf = (TrackingWorkFlow)(ci.Invoke(new object[] { commandInfo.InstanceId })))
                    {
                        twf.AcceptCommand(commandInfo.CommandName);
                    }
                }
            }
        }
        public List<string> getCandidateCommands(string wfName, string instanceId)
        {
            Assembly trackingWorkFlowAssembly = Assembly.Load("TrackingWorkFlow");
            Type[] types = trackingWorkFlowAssembly.GetTypes();
            List<string> requiredInputs = new List<string>();
            foreach (Type t in types)
            {
                if (t.Name.Equals(wfName))
                {
                    Type tt = typeof(String);
                    ConstructorInfo ci = t.GetConstructor(new Type[] { tt });
                    List<string> bookmarkInfos = null;
                    using (TrackingWorkFlow twf = (TrackingWorkFlow)(ci.Invoke(new object[] { instanceId })))
                    {
                        bookmarkInfos = twf.GetCandidateCommand();                        
                    }
                    return bookmarkInfos;
                }
            }
            return null;
        }
        public WorkFlowDefinitionList getWorkFlowDefinitions()
        {
            WorkFlowDefinitionList l = new WorkFlowDefinitionList();
            Assembly trackingWorkFlowAssembly = Assembly.Load("TrackingWorkFlow");
            Type[] types = trackingWorkFlowAssembly.GetTypes();
            Type target = trackingWorkFlowAssembly.GetType("TrackingWorkFlow.TrackingWorkFlow");
            foreach (Type t in types)
            {
                if (t.IsSubclassOf(target))
                {
                    WorkFlowDefinition WFD = new WorkFlowDefinition();
                    WFD.WFName = t.Name;
                    l.Add(WFD);
                }
            }
            return l;
        }

        public void Dispose()
        {
        }
    }
}
