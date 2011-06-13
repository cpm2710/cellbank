using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace FunctionPointService
{
    [ServiceContract]
    public class FunctionPointService
    {
        [OperationContract]
        [WebGet(UriTemplate = "/", ResponseFormat = WebMessageFormat.Json)]
        public FunctionPointList GetRoot()
        {
            FunctionPointList l = new FunctionPointList();
            FunctionPoint s = new FunctionPoint();
            s.Name = "所有用户";
            l.Add(s);
            return l;
        }
        // TODO: Add your service operations here
    }

    [DataContract(Namespace = "")]
    public class FunctionPoint
    {
        [DataMember]
        public string Name;
    }
    [CollectionDataContract(Name = "FunctionPoints", Namespace = "")]
    public class FunctionPointList : List<FunctionPoint>
    {
    }
}
