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
using System.Net;
using System.Web.Hosting;
using CommonResource;
using System.Web;
using Tracking;
using System.Diagnostics;
namespace TrackingService
{
    [ServiceContract]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Required)]
    public class TrackingService 
    {
        [OperationContract]
        [WebGet(UriTemplate = "/workflowdefinitions", ResponseFormat = WebMessageFormat.Json)]
        public WorkFlowDefinitionList GetWorkFlowDefinitions()
        {
            TrackingWorkFlowInteraction interaction = new TrackingWorkFlowInteraction();
            WorkFlowDefinitionList l=interaction.getWorkFlowDefinitions();
            return l;
        }
        [OperationContract]
        [WebGet(UriTemplate = "/workflowinstances", ResponseFormat = WebMessageFormat.Json)]
        public WorkFlowInstanceList GetWorkFlowInstances()
        {
            WorkFlowInstanceList l = new WorkFlowInstanceList();
            try
            {
                TrackingDataContext dataContext = new TrackingDataContext();

                IQueryable<CommonResource.Tracking> trackingQuery =
                    from tracking in dataContext.Trackings
                    select tracking;

                foreach (CommonResource.Tracking t in trackingQuery)
                {
                    using (TrackingWorkFlowInteraction interaction = new TrackingWorkFlowInteraction())
                    {
                        WorkFlowInstance wfi = new WorkFlowInstance();
                        wfi.BugId = t.bugid;
                        wfi.Id = t.wfinstanceid.ToString();
                        List<string> candiCmds = interaction.getCandidateCommands(t.wfname.Trim(), t.wfinstanceid.ToString());
                        wfi.Title = t.wfname;
                        CandidateCommandList ccl = new CandidateCommandList();
                        if (candiCmds != null)
                        {
                            foreach (string cmd in candiCmds)
                            {
                                ccl.Add(cmd);
                            }
                        }
                        wfi.CandidateCommandList = ccl;
                        l.Add(wfi);
                    }
                }
            }
            catch (Exception e)
            {
                TrackingLog.Log(e.ToString() + "!!" + e.Message);
            }
            return l;
        }
        [OperationContract]
        [WebGet(UriTemplate = "/workflowinstances/{InstanceId}", ResponseFormat = WebMessageFormat.Json)]
        public WorkFlowInstance GetWorkFlowInstance(string InstanceId)
        {
            WorkFlowInstance wfi = new WorkFlowInstance();            
            TrackingDataContext trackingContext = new TrackingDataContext();
            Guid guid = new Guid(InstanceId);
            IQueryable<CommonResource.Tracking> trackingQuery =
                from tracking in trackingContext.Trackings
                where ((tracking.wfinstanceid == guid))
                select tracking;
            foreach (CommonResource.Tracking t in trackingQuery)
            {
                TrackingWorkFlowInteraction twfi = new TrackingWorkFlowInteraction();
                wfi = new WorkFlowInstance();
                List<string> candCmds=twfi.getCandidateCommands(t.wfname.Trim(), InstanceId);
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
            try
            {
                List<string> requierdInputs = ci.getRequiredInputs(CommandName);
                foreach (string i in requierdInputs)
                {
                    Parameter p = new Parameter();
                    p.Name = i;
                    pList.Add(p);
                }
            }
            catch (Exception e)
            {
                throw new HttpException((int)HttpStatusCode.InternalServerError,e.Message);
            }
            return pList;
        }

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/workflowinstances", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public WorkFlowInstance startWorkFlow(CommandInfo CommandInfo)
        {
            WorkFlowInstance wfi = new WorkFlowInstance();
            try
            {
                string WFName = CommandInfo.WFName;
                TrackingWorkFlowInteraction twfi = new TrackingWorkFlowInteraction();
                string id = twfi.startProcess(WFName);
                TrackingDataContext tdc = new TrackingDataContext();
                CommonResource.Tracking t = new CommonResource.Tracking();
                t.wfname = CommandInfo.WFName;
                t.wfinstanceid = new Guid(id);
                tdc.Trackings.InsertOnSubmit(t);
                tdc.SubmitChanges();

                wfi.Id = id;
                List<string> candCmds = twfi.getCandidateCommands(WFName, id);
                CandidateCommandList ccl = new CandidateCommandList();
                ccl.AddRange(candCmds);
                wfi.CandidateCommandList = ccl;
            }
            catch (Exception e)
            {
                TrackingLog.Log(e.Message + "!!" + e.ToString());
            }
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
            cmdInteraction.executeCommand(CommandInfo.CommandName, paras);//this is do the real action in extension
            TrackingWorkFlowInteraction twfi = new TrackingWorkFlowInteraction();
            twfi.doCommand(CommandInfo);// this is just trigger the state machine(WF) to do one step

            return CommandInfo;
        }
    }
}
