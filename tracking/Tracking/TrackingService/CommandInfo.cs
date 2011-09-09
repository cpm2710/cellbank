using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;

namespace TrackingService
{
    [DataContract(Namespace = "")]
    public class CommandInfo
    {
        [DataMember]
        public string WFName;
        [DataMember]
        public string CommandName;
        [DataMember]
        public ParameterList ParameterList;
    }
}