using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SEActivities;
using TrackingCommands;
using CommonResource;
using System.IO;
using System.Runtime.Serialization.Json;
using TrackingWorkFlow;
using System.Activities;
using System.Collections.ObjectModel;
using System.Activities.Hosting;
using System.Activities.DurableInstancing;
using System.Runtime.DurableInstancing;
using System.Threading;
namespace FakeServiceAndClient
{
    class Program
    {
        public WorkFlowDefinitionList GetWorkFlowDefinitions()
        {
            TrackingWorkFlowInteraction interaction = new TrackingWorkFlowInteraction();
            WorkFlowDefinitionList l = interaction.getWorkFlowDefinitions();
            return l;
        }
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
                List<string> candCmds = twfi.getCandidateCommands(t.wfname.Trim(), InstanceId);
                CandidateCommandList ccl = new CandidateCommandList();
                ccl.AddRange(candCmds);
                wfi.CandidateCommandList = ccl;
                return wfi;
            }
            return null;
        }

        public ParameterList GetParameters(string CommandName)
        {
            ParameterList pList = new ParameterList();
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
                //throw new HttpException((int)HttpStatusCode.InternalServerError, e.Message);
            }
            return pList;
        }

        public WorkFlowInstance startWorkFlow(CommandInfo CommandInfo)
        {
            WorkFlowInstance wfi = new WorkFlowInstance();
            try
            {
                string WFName = CommandInfo.WFName;
                using (
                TrackingWorkFlowInteraction twfi = new TrackingWorkFlowInteraction())
                {
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
            }
            catch (Exception e)
            {
                TrackingLog.Log(e.Message + "!!" + e.ToString());
            }
            return wfi;
        }

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
        static void Main(string[] args)
        {

            Program p = new Program();
            WorkFlowDefinitionList wflist = p.GetWorkFlowDefinitions();
            foreach (WorkFlowDefinition wfd in wflist)
            {
                CommandInfo ci = new CommandInfo();
                ci.WFName = wfd.WFName.Trim();
                WorkFlowInstance wfi= p.startWorkFlow(ci);
                break;
            }
            
            WorkFlowInstanceList wfilist = p.GetWorkFlowInstances();
            AutoResetEvent ee = new AutoResetEvent(false);
            ee.WaitOne();

        }
    }
}
