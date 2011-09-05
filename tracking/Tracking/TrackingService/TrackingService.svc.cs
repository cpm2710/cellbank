using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;

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
        public WorkFlowInstance GetWorkFlowInstances(string InstanceId)
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
                wfi = new WorkFlowInstance();
                return wfi;
            }
            return null;
        }
    }
}
