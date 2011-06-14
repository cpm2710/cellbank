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
            s.Title = "Users Management";
            s.ImgUrl = "/staticImages/users.png";
            s.AlertCount = "3";
            s.Description = "Users Management Component";
            l.Add(s);

            FunctionPoint s2 = new FunctionPoint();
            s2.Title = "Users Management";
            s2.ImgUrl = "/staticImages/users.png";
            s2.AlertCount = "3";
            s2.Description = "Users Management Component";
            l.Add(s2);

            return l;
        }
        // TODO: Add your service operations here
    }

    [DataContract(Namespace = "")]
    public class FunctionPoint
    {
        [DataMember]
        public string Title;
        [DataMember]
        public string ImgUrl;
        [DataMember]
        public string  AlertCount;
        [DataMember]
        public string Description;
    }
    [CollectionDataContract(Name = "FunctionPoints", Namespace = "")]
    public class FunctionPointList : List<FunctionPoint>
    {
    }
}
