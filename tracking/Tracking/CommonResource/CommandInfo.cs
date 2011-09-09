using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;

namespace CommonResource
{
    [DataContract(Namespace = "")]
    public class CommandInfo
    {
        [DataMember]
        public string WFName;
        [DataMember]
        public string InstanceId;
        [DataMember]
        public string CommandName;
        [DataMember]
        public ParameterList ParameterList;
    }
}