using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;

namespace CommonResource
{
    [DataContract(Namespace = "")]
    public class WorkFlowInstance
    {
        [DataMember]
        public string Id;
        [DataMember]
        public string Title;
        [DataMember]
        public string BugId;
        [DataMember]
        public string WFName;
        [DataMember]
        public string AssignedTo;
        [DataMember]
        public string LastModified;
        [DataMember]
        public string LastModifiedBy;
        [DataMember]
        public string QFEStatus;
        [DataMember]
        public CandidateCommandList CandidateCommandList;
    }
    [CollectionDataContract(Name = "CandidateCommandList", Namespace = "")]
    public class CandidateCommandList : List<string>
    {
    }
    [CollectionDataContract(Name = "WorkFlowInstanceList", Namespace = "")]
    public class WorkFlowInstanceList : List<WorkFlowInstance>
    {

    }
}