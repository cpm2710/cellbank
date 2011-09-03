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
    [DataContract(Namespace = "")]
    public class WorkFlowInstance
    {
        [DataMember]
        public string Id;
        [DataMember]
        public string Title;
    }
    [CollectionDataContract(Name = "WorkFlowInstanceList", Namespace = "")]
    public class WorkFlowInstanceList : List<WorkFlowInstance>
    {

    }
}