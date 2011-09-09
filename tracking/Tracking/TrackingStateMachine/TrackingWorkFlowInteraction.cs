using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace TrackingWorkFlow
{
    public class TrackingWorkFlowInteraction
    {
        public List<string> getCandidateCommands(string wfName, string instanceId)
        {
            //List<string> candidateCommands = new List<string>();
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
                    //FieldInfo[] fields = t.GetFields();
                    //foreach (FieldInfo field in fields)
                    //{
                    //    requiredInputs.Add(field.Name);
                    //}
                }
            }
            return null;
        }
    }
}
