using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;

namespace CommonResource
{
    [DataContract(Namespace = "")]
    public class WorkFlowDefinition
    {
        [DataMember]
        public string WFName;
    }
    [CollectionDataContract(Name = "WorkFlowDefinitionList", Namespace = "")]
    public class WorkFlowDefinitionList : List<WorkFlowDefinition>
    {

    }
}