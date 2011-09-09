using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using TrackingCommands;
using TrackingWorkFlow;
using CommonResource;

namespace TrackingService
{
    [ServiceContract]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Required)]
    public class TrackingService 
    {
        [OperationContract]
        [WebGet(UriTemplate = "/", ResponseFormat = WebMessageFormat.Json)]
        public WorkFlowInstanceList GetWorkFlowInstances()
        {
            WorkFlowInstanceList l = new WorkFlowInstanceList();

            //WorkFlowInstanceDataContext dataContext = new WorkFlowInstanceDataContext();
            //var workflowinstance = from b in dataContext.InstancesTables select b;
            return l;
        }
        [OperationContract]
        [WebGet(UriTemplate = "/{InstanceId}", ResponseFormat = WebMessageFormat.Json)]
        public WorkFlowInstance GetWorkFlowInstance(string InstanceId)
        {
            WorkFlowInstance wfi = new WorkFlowInstance();            
            TrackingDataContext trackingContext = new TrackingDataContext();
            Guid guid = new Guid(InstanceId);
            IQueryable<Tracking> trackingQuery =
                from tracking in trackingContext.Trackings
                where ((tracking.wfinstanceid == guid))
                select tracking;
            foreach (Tracking t in trackingQuery)
            {
                TrackingWorkFlowInteraction twfi = new TrackingWorkFlowInteraction();
                wfi = new WorkFlowInstance();
                List<string> candCmds=twfi.getCandidateCommands(t.wfname, InstanceId);
                CandidateCommandList ccl=new CandidateCommandList();
                ccl.AddRange(candCmds);
                wfi.CandidateCommandList = ccl;
                return wfi;
            }
            return null;
        }

        [OperationContract]
        [WebGet(UriTemplate = "/parameters/{CommandName}", ResponseFormat = WebMessageFormat.Json)]
        public ParameterList GetParameters(string CommandName)
        {
            ParameterList pList=new ParameterList();
            CommandInteraction ci = new CommandInteraction();
            List<string> requierdInputs=ci.getRequiredInputs(CommandName);
            foreach (string i in requierdInputs)
            {
                Parameter p = new Parameter();
                p.Name = i;
                pList.Add(p);
            }
            return pList;
        }

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/commands/{WFName}", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public WorkFlowInstance startWorkFlow(string WFName)
        {
            TrackingWorkFlowInteraction twfi = new TrackingWorkFlowInteraction();

            string id=twfi.startProcess(WFName);

            WorkFlowInstance wfi = new WorkFlowInstance();
            wfi.Id = id;
            List<string> candCmds = twfi.getCandidateCommands(WFName, id);
            CandidateCommandList ccl = new CandidateCommandList();
            ccl.AddRange(candCmds);
            wfi.CandidateCommandList = ccl;
            return wfi;
        }

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/commands", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public CommandInfo doCommand(CommandInfo CommandInfo)
        {

            CommandInteraction cmdInteraction = new CommandInteraction();
            Dictionary<string, string> paras = new Dictionary<string, string>();
            foreach (Parameter p in CommandInfo.ParameterList)
            {
                paras.Add(p.Name, p.Value);
            }
            cmdInteraction.executeCommand(CommandInfo.CommandName, paras);
            TrackingWorkFlowInteraction twfi = new TrackingWorkFlowInteraction();
            twfi.doCommand(CommandInfo);
            return CommandInfo;
        }
    }
}
