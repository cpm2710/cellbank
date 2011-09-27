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
    interface ITrackingService 
    {
        [OperationContract]
        [WebGet(UriTemplate = "/workflowdefinitions", ResponseFormat = WebMessageFormat.Json)]
        WorkFlowDefinitionList GetWorkFlowDefinitions();
        [OperationContract]
        [WebGet(UriTemplate = "/workflowinstances", ResponseFormat = WebMessageFormat.Json)]
        WorkFlowInstanceList GetWorkFlowInstances();
        [OperationContract]
        [WebGet(UriTemplate = "/workflowinstances/{InstanceId}", ResponseFormat = WebMessageFormat.Json)]
        WorkFlowInstance GetWorkFlowInstance(string InstanceId);

        [OperationContract]
        [WebGet(UriTemplate = "/parameters/{CommandName}", ResponseFormat = WebMessageFormat.Json)]
        ParameterList GetParameters(string CommandName);
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/workflowinstances", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        WorkFlowInstance startWorkFlow(CommandInfo CommandInfo);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/commands", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        CommandInfo doCommand(CommandInfo CommandInfo);
    }
}
