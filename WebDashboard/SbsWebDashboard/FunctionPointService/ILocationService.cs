using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.ServiceModel.Web;

namespace FunctionPointService
{
    [DataContract]
    public class UserLocation
    {
        [DataMember]
        public string UserName;
        [DataMember]
        public double Latitude;
        [DataMember]
        public double Longitude;
    }
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "ILocationService" in both code and config file together.
    [ServiceContract]
    public interface ILocationService
    {
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/userlocations", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        UserLocation reportUserLocation(UserLocation UserLocation);
    }
}
