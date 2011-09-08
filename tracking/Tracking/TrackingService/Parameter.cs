using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;

namespace TrackingService
{
    [DataContract(Namespace = "")]
    public class Parameter
    {
        [DataMember]
        public string Name;
        [DataMember]
        public string Type;
    }
    [CollectionDataContract(Name = "ParameterList", Namespace = "")]
    public class ParameterList : List<Parameter>
    {

    }
}